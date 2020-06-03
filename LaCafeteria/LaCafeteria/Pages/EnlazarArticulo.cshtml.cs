﻿using System;
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

namespace LaCafeteria.Pages
{
    public class EnlazarArticuloModel : PageModel
    {
        public List<TopicoModel> listaTopicos { set; get; }

        public List<MiembroModel> listaMiembros { set; get; }

        [BindProperty]
        public ArticuloModel articulo { set; get; }

        [BindProperty]
        public List<string> listaTopicosArticulo { get; set; }

        [BindProperty]
        public List<string> listaMiembrosAutores { set; get; }

        [BindProperty]
        [UrlAttribute(ErrorMessage = "Debe ingresar un URL válido")]
        public string url { get; set; }

        public List<string[]> autoresViejos { get; set; }
        public TopicoController topicoController;
        public MiembroController miembroController;
        public ArticuloController articuloController;
        public string inyeccion = "";
        [BindProperty(SupportsGet = true)]
        public int idArticuloPK { get; set; }

        public EnlazarArticuloModel()
        {
            topicoController = new TopicoController();
            miembroController = new MiembroController();
            articuloController = new ArticuloController();
            listaTopicos = topicoController.GetListaTopicos();
            listaMiembros = miembroController.GetListaMiembros();
            listaMiembrosAutores = new List<string>();
            listaTopicosArticulo = new List<string>();
            autoresViejos = new List<string[]>();
            idArticuloPK = -1;
        }

        public void OnGet()
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
                        "div.appendChild(button)" + "\n" +
                        "}\n";
                }
            }
            return;
        }

        public IActionResult OnPostGuardar()
        {
            if (EsValido())
            {
                articulo.tipo = TipoArticulo.Link;
                articulo.estado = EstadoArticulo.EnProgreso;
                articulo.contenido = url;
                articuloController.GuardarArticulo(articulo, listaMiembrosAutores, listaTopicosArticulo);
                Notificaciones.Set(this, "articuloGuardado", "Su articulo se guardó", Notificaciones.TipoNotificacion.Exito);

                return Redirect("/MiPerfil");
            }

            return Page();
        }

        public IActionResult OnPostEditar()
        {
            if (EsValido())
            {
                articulo.tipo = TipoArticulo.Link;
                articulo.estado = EstadoArticulo.EnProgreso;
                articulo.contenido = url;
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
                articulo.tipo = TipoArticulo.Link;
                articulo.estado = EstadoArticulo.RequiereRevision;
                articulo.contenido = url;
                articulo.idArticuloPK = (int)TempData["idArticulo"];
                if (articulo.idArticuloPK == -1)
                {
                    articuloController.GuardarArticulo(articulo, listaMiembrosAutores, listaTopicosArticulo);
                }
                else
                {
                    articuloController.EditarArticulo(articulo, listaMiembrosAutores, listaTopicosArticulo, "");
                }

                Notificaciones.Set(this, "articuloEnviadoRev", "Su artículo fue enviado a revisión", Notificaciones.TipoNotificacion.Exito);

                return Redirect("/MiPerfil");
            }

            return Page();
        }

        private bool EsValido()
        {
            bool esValido = true;

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