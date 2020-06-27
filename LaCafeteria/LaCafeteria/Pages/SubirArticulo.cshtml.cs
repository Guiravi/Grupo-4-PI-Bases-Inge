using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using LaCafeteria.Models;
using LaCafeteria.Controllers;
using LaCafeteria.Utilidades;
using Microsoft.AspNetCore.Hosting;

namespace LaCafeteria.Pages
{
    public class SubirArticuloModel : PageModel
    {
		public List<CategoriaTopicoModel> listaTopicos { set; get; }

		public List<MiembroModel> listaMiembros { set; get; }

		[BindProperty]
		public ArticuloModel articulo { set; get; }

		[BindProperty]
		public IFormFile archivoArticulo { get; set; }

		[BindProperty]
		public List<string> listaTopicosArticulo { get; set; }

		[BindProperty]
		public List<string> listaMiembrosAutores { set; get; }

        public List<string[]> autoresViejos { get; set; }

        //public CorreoController correoController;

        private BuscadorMiembrosController buscadorMiembrosController;
        private InformacionArticuloController informacionArticuloController;
        private DocumentosArticuloController documentosArticuloController;
        private AlmacenadorArticuloController almacenadorArticuloController;
        private EditorArticuloController editorArticuloController;
        private InformacionCategoriaTopicoController informacionCategoriaTopicoController;

        [BindProperty(SupportsGet = true)]
        public int idArticuloPK { get; set; }

        public string rutaCarpeta = "";
        public string nombreArchivo = "";

        public string inyeccion = "";

        public SubirArticuloModel(IHostingEnvironment env)
		{
            //correoController = new CorreoController(env);
            informacionCategoriaTopicoController = new InformacionCategoriaTopicoController();
            buscadorMiembrosController = new BuscadorMiembrosController();
            informacionArticuloController = new InformacionArticuloController();
            documentosArticuloController = new DocumentosArticuloController();
            almacenadorArticuloController = new AlmacenadorArticuloController();
            editorArticuloController = new EditorArticuloController();

            listaTopicos = informacionCategoriaTopicoController.GetCategoriasYTopicos();
			listaMiembros = buscadorMiembrosController.GetListaMiembrosModel();
            listaMiembrosAutores = new List<string>();
            listaTopicosArticulo = new List<string>();
            autoresViejos = new List<string[]>();
            articulo = new ArticuloModel();


            idArticuloPK = -1;
            rutaCarpeta = env.WebRootPath;
        }

		public IActionResult OnGet()
        {
            if (Request.Cookies["usernamePK"] != null)
            {
                if (idArticuloPK != -1)
                {
                    articulo = informacionArticuloController.GetInformacionArticuloModel(idArticuloPK);

                    articulo.fechaPublicacion = Convertidor.CambiarFormatoFechaAMD(articulo.fechaPublicacion);

                    listaTopicosArticulo = informacionArticuloController.GetTopicosArticuloListaString(idArticuloPK);

                    autoresViejos = informacionArticuloController.GetAutoresArticuloListaStringArray(idArticuloPK);
                    foreach (string[] item in autoresViejos)
                    {
                        listaMiembrosAutores.Add(item[0]);
                    }

                    documentosArticuloController.CargarArticuloDOCX(idArticuloPK, rutaCarpeta);

                    nombreArchivo = idArticuloPK + ".docx";

                    TempData["idArticulo"] = idArticuloPK;

                    for (int i = 0; i < autoresViejos.Count; i++)
                    {
                        inyeccion += "var select = document.getElementById('slctAutor');" + "\n" +
                            "var option = select[select.selectedIndex];" + "\n"+
                            "if (!miembrosAutores.includes('"+ autoresViejos[i][0] + "')) {" + "\n" +
                            "const div = document.createElement('div');" +
                            "const button = document.createElement('input');" + "\n" +
                            "button.type = \"button\";" + "\n" +
                            "button.value = \"x\";" + "\n" +
                            "button.toDelete = '" + autoresViejos[i][0] + "';" + "\n" +
                            "button.onclick = borrar;" + "\n" +
                            "miembrosAutores.push('" + autoresViejos[i][0] +"')" + "\n" +
                            "div.innerHTML = '<label>' + \'" + autoresViejos[i][1] +"\' + '</label><input type=\"hidden\" name=\"listaMiembrosAutores\" value=\"' + \'"+ autoresViejos[i][0] + "\' + '\"/>';" + "\n" +
                            "document.getElementById('autores').appendChild(div);" + "\n" +
                            "div.appendChild(button);" + "\n" +
                            "}\n";
                    }
                }
            }
            else
            {
                Notificaciones.Set(this, "init_session_error", "Por favor inicie sesión para poder subir el artículo", Notificaciones.TipoNotificacion.Error);
                return Redirect("/Login");
            }
            return Page();
        }

		public IActionResult OnPostGuardar()
		{	
			if (EsValido())
			{
				articulo.tipo = TipoArticulo.Largo;
				articulo.estado = EstadoArticulo.EnProgreso;
                almacenadorArticuloController.GuardarArticulo(articulo, listaMiembrosAutores, listaTopicosArticulo);
				Notificaciones.Set(this, "articuloGuardado", "Su artículo se guardó correctamente", Notificaciones.TipoNotificacion.Exito);

                return Redirect("/MiPerfil");
			}

            return Page();
		}

        public IActionResult OnPostEditar()
        {
            if (EsValido())
            {
                articulo.tipo = TipoArticulo.Largo;
                articulo.estado = EstadoArticulo.EnProgreso;
                articulo.articuloAID = (int)TempData["idArticulo"];
                editorArticuloController.EditarArticulo(articulo, listaMiembrosAutores, listaTopicosArticulo, rutaCarpeta);
                Notificaciones.Set(this, "articuloEditado", "Su artículo se editó correctamente", Notificaciones.TipoNotificacion.Exito);

                return Redirect("/MiPerfil");
            }

            return Page();
        }

        public IActionResult OnPostEnviarRevision()
        {
            if (EsValido())
            {               
                articulo.tipo = TipoArticulo.Largo;
                articulo.estado = EstadoArticulo.RequiereRevision;
                if (TempData["idArticulo"] != null)
                {
                    articulo.articuloAID = (int)TempData["idArticulo"];
                }
                if (articulo.articuloAID ==  -1)
                {
                    almacenadorArticuloController.GuardarArticulo(articulo, listaMiembrosAutores, listaTopicosArticulo);
                }
                else
                {
                    editorArticuloController.EditarArticulo(articulo, listaMiembrosAutores, listaTopicosArticulo, rutaCarpeta);
                }

                //correoController.sendNecesitaRevision(articulo.titulo);

                Notificaciones.Set(this, "articuloEnviadoRev", "Su artículo fue enviado a revisión", Notificaciones.TipoNotificacion.Exito);

                return Redirect("/MiPerfil");
            }

            return Page();
        }

        private bool EsValido()
		{
			bool esValido = true;

			if(archivoArticulo == null)
			{
				esValido = false;
				Notificaciones.Set(this, "archivoArticulo", "Debe seleccionar un archivo para subir", Notificaciones.TipoNotificacion.Error);
			}
			else
			{
				BinaryReader binaryReader = new BinaryReader(archivoArticulo.OpenReadStream());
		
				articulo.contenido = System.Convert.ToBase64String(binaryReader.ReadBytes((int) archivoArticulo.Length), 0, (int) archivoArticulo.Length);
				ModelState.Remove("articulo.contenido");
			}


			if (listaTopicosArticulo.Count == 0)
			{
				Notificaciones.Set(this, "listaTopicosArticulo", "Debe seleccionar al menos un tópico para su artículo", Notificaciones.TipoNotificacion.Error);
				esValido = false;
			}

			if (listaMiembrosAutores.Count == 0)
			{
				Notificaciones.Set(this, "listaMiembrosAutores", "Debe seleccionar al menos un autor para su artículo", Notificaciones.TipoNotificacion.Error);
				esValido = false;
			}

			return esValido && ModelState.IsValid;
		}

    }
}