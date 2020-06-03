using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Models;
using LaCafeteria.Utilidades;
using System.Web;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using LaCafeteria.Controllers;
using LaCafeteria.Utilidades;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Hosting;

namespace LaCafeteria.Pages
{
	public class EscribirArticuloModel : PageModel
	{	
		public List<TopicoModel> listaTopicos { set; get; }

		public List<MiembroModel> listaMiembros { set; get; }

		[BindProperty]
		public ArticuloModel articulo { set; get; }

		[BindProperty]
		public List<string> listaTopicosArticulo { get; set; }

		[BindProperty]
		public List<string> listaMiembrosAutores { set; get; }

        public List<string[]> autoresViejos { get; set; }
		public TopicoController topicoController;
		public MiembroController miembroController;
		public ArticuloController articuloController;
        public CorreoController correoController;
         public string inyeccion = "";
        [BindProperty(SupportsGet = true)]
        public int idArticuloPK { get; set; }

        public EscribirArticuloModel(IHostingEnvironment env)
		{
			topicoController = new TopicoController();
			miembroController = new MiembroController();
			articuloController = new ArticuloController();
            correoController = new CorreoController(env);
			listaTopicos = topicoController.GetListaTopicos();
			listaMiembros = miembroController.GetListaMiembros();
            listaMiembrosAutores = new List<string>();
            listaTopicosArticulo = new List<string>();
            autoresViejos = new List<string[]>();
            articulo = new ArticuloModel();

            idArticuloPK = -1;
        }

		public IActionResult OnGet()
        {
            if (Request.Cookies["usernamePK"] != null)
            {
                if (idArticuloPK != -1)
                {
                    articulo = articuloController.GetArticuloModelResumen(idArticuloPK);

                    articulo.fechaPublicacion = Convertidor.CambiarFormatoFechaAMD(articulo.fechaPublicacion);

                    autoresViejos = miembroController.GetAutoresArticuloLista(idArticuloPK);
                    foreach (string[] item in autoresViejos)
                    {
                        listaMiembrosAutores.Add(item[0]);
                    }

                    listaTopicosArticulo = topicoController.GetTopicosArticuloLista(idArticuloPK);

                    TempData["idArticulo"] = idArticuloPK;

                    for (int i = 0; i < autoresViejos.Count; i++)
                    {
                        inyeccion += "var select = document.getElementById('slctAutor');" + "\n" +
                            "var option = select[select.selectedIndex];" + "\n" +
                            "if (!miembrosAutores.includes('" + autoresViejos[i][0] + "')) {" + "\n" +
                            "const div = document.createElement('div');" +
                            "const button = document.createElement('input');" + "\n" +
                            "button.type = \"button\";" + "\n" +
                            "button.value = \"x\";" + "\n" +
                            "button.toDelete = '" + autoresViejos[i][0] + "';" + "\n" +
                            "button.onclick = borrar;" + "\n" +
                            "miembrosAutores.push('" + autoresViejos[i][0] + "')" + "\n" +
                            "div.innerHTML = '<label>' + \'" + autoresViejos[i][1] + "\' + '</label><input type=\"hidden\" name=\"listaMiembrosAutores\" value=\"' + \'" + autoresViejos[i][0] + "\' + '\"/>';" + "\n" +
                            "document.getElementById('autores').appendChild(div);" + "\n" +
                            "div.appendChild(button);" + "\n" +
                            "}\n";
                    }
                }
            }
            else
            {
                Notificaciones.Set(this, "init_session_error","Por favor inicie sesión para poder escribir el artículo", Notificaciones.TipoNotificacion.Error);
                return Redirect("/Login");
            }
            return Page();
        }

		public IActionResult OnPostGuardar()
		{	
			if(EsValido())
			{	
				articulo.tipo = TipoArticulo.Corto;
				articulo.estado = EstadoArticulo.EnProgreso;
				articuloController.GuardarArticulo(articulo, listaMiembrosAutores, listaTopicosArticulo);
				Notificaciones.Set(this, "articuloGuardado", "Su articulo se guardó", Notificaciones.TipoNotificacion.Exito);

                return Redirect("/MiPerfil");
			}

            return Page();
		}

		public IActionResult OnPostEditar()
		{	
			if(EsValido())
			{	
				articulo.tipo = TipoArticulo.Corto;
				articulo.estado = EstadoArticulo.EnProgreso;
				articuloController.EditarArticulo(articulo, listaMiembrosAutores, listaTopicosArticulo, "");
                Notificaciones.Set(this, "articuloEditado", "Su articulo se editó correctamente", Notificaciones.TipoNotificacion.Exito);

                return Redirect("/MiPerfil");
            }

            return Page();
		}

        public IActionResult OnPostEnviarRevision()
        {
            if (EsValido())
            {
                    articulo.tipo = TipoArticulo.Corto;
                    articulo.estado = EstadoArticulo.RequiereRevision;
                    articulo.idArticuloPK = (int) TempData["idArticulo"];
                    if ( articulo.idArticuloPK == -1 )
                    {
                        articuloController.GuardarArticulo(articulo, listaMiembrosAutores, listaTopicosArticulo);
                    } else
                    {
                        articuloController.EditarArticulo(articulo, listaMiembrosAutores, listaTopicosArticulo, "");
                    }

                    correoController.sendNecesitaRevision(articulo.titulo);

                    Notificaciones.Set(this, "articuloEnviadoRev", "Su artículo fue enviado a revisión", Notificaciones.TipoNotificacion.Exito);

                    return Redirect("/MiPerfil");
            }

            return Page();
        }

        private bool EsValido()
		{
			bool esValido = true;

			if(listaTopicosArticulo.Count == 0)
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