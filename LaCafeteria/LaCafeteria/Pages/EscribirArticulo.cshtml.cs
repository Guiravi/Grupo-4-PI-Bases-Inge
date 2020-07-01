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
        public List<CategoriaTopicoModel> listaCategoriaTopicos { set; get; }

        public List<MiembroModel> listaMiembros { set; get; }

        [BindProperty]
        public ArticuloModel articulo { set; get; }

        [BindProperty]
        public List<string> listaCategoriaTopicosArticulo { get; set; }

        [BindProperty]
        public List<string> listaMiembrosAutores { set; get; }

        public List<string[]> autoresViejos { get; set; }

        public List<RevisionModel> revisiones { get; set; }

        public string estadoAnterior { set; get; }


        //public CorreoController correoController;
        private InformacionCategoriaTopicoController informacionCategoriaTopicoController;
        private BuscadorMiembrosController buscadorMiembrosController;
        private InformacionArticuloController informacionArticuloController;
        private AlmacenadorArticuloController almacenadorArticuloController;
        private EditorArticuloController editorArticuloController;
        private CreadorNotificacionController creadorNotificacionController;

        public string inyeccion = "";
        [BindProperty(SupportsGet = true)]
        public int idArticuloPK { get; set; }

        public EscribirArticuloModel(IHostingEnvironment env) {
            creadorNotificacionController = new CreadorNotificacionController();
            informacionCategoriaTopicoController = new InformacionCategoriaTopicoController();
            buscadorMiembrosController = new BuscadorMiembrosController();
            informacionArticuloController = new InformacionArticuloController();
            almacenadorArticuloController = new AlmacenadorArticuloController();
            editorArticuloController = new EditorArticuloController();

            listaCategoriaTopicos = informacionCategoriaTopicoController.GetCategoriasYTopicos();
            listaMiembros = buscadorMiembrosController.GetListaMiembrosModel();
            listaMiembrosAutores = new List<string>();
            listaCategoriaTopicosArticulo = new List<string>();
            autoresViejos = new List<string[]>();
            articulo = new ArticuloModel();
            estadoAnterior = null;

            idArticuloPK = -1;
        }

        public IActionResult OnGet() {
            if ( Request.Cookies["usernamePK"] != null )
            {
                if ( idArticuloPK != -1 )
                {
                    articulo = informacionArticuloController.GetInformacionArticuloModel(idArticuloPK);

                    articulo.fechaPublicacion = Convertidor.CambiarFormatoFechaAMD(articulo.fechaPublicacion);

                    autoresViejos = informacionArticuloController.GetAutoresArticuloListaStringArray(idArticuloPK);

                    foreach ( string[] item in autoresViejos )
                    {
                        listaMiembrosAutores.Add(item[0]);
                    }

                    listaCategoriaTopicosArticulo = informacionArticuloController.GetCategoriaTopicosArticuloString(idArticuloPK);

                    TempData["idArticulo"] = idArticuloPK;

                    for ( int i = 0; i < autoresViejos.Count; i++ )
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

                    if ( articulo.estado == EstadoArticulo.EnCorrecciones )
                    {
                        revisiones = informacionArticuloController.GetRevisiones(idArticuloPK);
                        estadoAnterior = articulo.estado;
                    }
                } else
                {
                    TempData["idArticulo"] = -1;
                }
            } else
            {
                AvisosInmediatos.Set(this, "init_session_error", "Por favor inicie sesión para poder escribir el artículo", AvisosInmediatos.TipoAviso.Error);
                return Redirect("/Login");
            }
            return Page();
        }

        public IActionResult OnPostGuardar() {
            if ( EsValido() )
            {
                articulo.tipo = TipoArticulo.Corto;
                articulo.estado = EstadoArticulo.EnProgreso;
                almacenadorArticuloController.GuardarArticulo(articulo, listaMiembrosAutores, listaCategoriaTopicosArticulo);
                AvisosInmediatos.Set(this, "articuloGuardado", "Su articulo se guardó", AvisosInmediatos.TipoAviso.Exito);

                return Redirect("/MiPerfil");
            }

            return Page();
        }

        public IActionResult OnPostEditar() {
            if ( EsValido() )
            {
                articulo.articuloAID = idArticuloPK;
                articulo.tipo = TipoArticulo.Corto;
                articulo.estado = EstadoArticulo.EnProgreso;
                editorArticuloController.EditarArticulo(articulo, listaMiembrosAutores, listaCategoriaTopicosArticulo, "");
                AvisosInmediatos.Set(this, "articuloEditado", "Su articulo se editó correctamente", AvisosInmediatos.TipoAviso.Exito);

                return Redirect("/MiPerfil");
            }

            return Page();
        }

        public IActionResult OnPostEnviarRevision() {
            if ( EsValido() )
            {

                articulo.tipo = TipoArticulo.Corto;

                if ( estadoAnterior == EstadoArticulo.EnCorrecciones )
                {
                    articulo.estado = EstadoArticulo.EnRevision;
                } else
                {
                    articulo.estado = EstadoArticulo.RequiereRevision;
                }

                if ( TempData["idArticulo"] != null )
                {
                    articulo.articuloAID = (int) TempData["idArticulo"];
                }

                if ( articulo.articuloAID == -1 )
                {
                    almacenadorArticuloController.GuardarArticulo(articulo, listaMiembrosAutores, listaCategoriaTopicosArticulo);
                } else
                {
                    editorArticuloController.EditarArticulo(articulo, listaMiembrosAutores, listaCategoriaTopicosArticulo, "");
                }

                // Enviar una notificacion a los miembros nucleo
                if ( articulo.estado == EstadoArticulo.EnRevision )
                {
                    Notificacion notificacion = new Notificacion();
                    notificacion.mensaje = "El artículo " + articulo.titulo + " ha sido corregido. Por favor proceder a revisar las correcciones.";
                    notificacion.url = "/RevisarArticulo/" + articulo.articuloAID + "/" + articulo.tipo;

                    foreach ( RevisionModel revision in revisiones )
                    {
                        notificacion.usernameFK = revision.usernameMiemFK;
                        creadorNotificacionController.CrearNotificacion(notificacion);
                    }
                } else
                {
                    Notificacion notificacion = new Notificacion();
                    notificacion.mensaje = "Un artículo nuevo con título " + articulo.titulo + " requiere revisión para se publicado. Por favor indicar su interés de participar en este proceso.";
                    notificacion.url = "/ArticulosPorRevisar";

                    List<MiembroModel> nucleos = buscadorMiembrosController.GetListaMiembrosNucleoModel();
                    foreach ( MiembroModel miembroNucleo in nucleos)
                    {
                        notificacion.usernameFK = miembroNucleo.usernamePK;
                        creadorNotificacionController.CrearNotificacion(notificacion);
                    }
                }

                AvisosInmediatos.Set(this, "articuloEnviadoRev", "Su artículo fue enviado a revisión", AvisosInmediatos.TipoAviso.Exito);

                return Redirect("/MiPerfil");
            }

            return Page();
        }

        private bool EsValido() {
            bool esValido = true;

            if ( listaCategoriaTopicosArticulo.Count == 0 )
            {
                AvisosInmediatos.Set(this, "listaTopicosArticulo", "Debe seleccionar al menos un tópico para su artículo", AvisosInmediatos.TipoAviso.Error);
                esValido = false;
            }

            if ( listaMiembrosAutores.Count == 0 )
            {
                AvisosInmediatos.Set(this, "listaMiembrosAutores", "Debe seleccionar al menos un autor para su artículo", AvisosInmediatos.TipoAviso.Error);
                esValido = false;
            }

            return esValido && ModelState.IsValid;
        }
    }
}