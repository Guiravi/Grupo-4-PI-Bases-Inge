﻿using System;
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

namespace LaCafeteria.Pages
{
    public class SubirArticuloModel : PageModel
    {
		public List<TopicoModel> listaTopicos { set; get; }

		public List<MiembroModel> listaMiembros { set; get; }

		[BindProperty]
		public ArticuloModel articulo { set; get; }

		[BindProperty]
		public IFormFile archivoArticulo { get; set; }

		[BindProperty]
		public List<string> listaTopicosArticulo { get; set; }

		[BindProperty]
		public List<string> listaMiembrosAutores { set; get; }

		public TopicoController topicoController;
		public MiembroController miembroController;
		public ArticuloController articuloController;

        [BindProperty(SupportsGet = true)]
        public int idArticuloPK { get; set; }

        public string rutaCarpeta = "";
        public string nombreArchivo = "";

        public SubirArticuloModel(IHostingEnvironment env)
		{
			topicoController = new TopicoController();
			miembroController = new MiembroController();
			articuloController = new ArticuloController();
			listaTopicos = topicoController.GetListaTopicos();
			listaMiembros = miembroController.GetListaMiembros();

            rutaCarpeta = env.WebRootPath;
        }

		public void OnGet()
        {
            if (idArticuloPK != null)
            {
                articulo = articuloController.GetArticuloModelResumen(idArticuloPK);

                articulo.fechaPublicacion = Convertidor.CambiarFormatoFechaAMD(articulo.fechaPublicacion);

                listaTopicosArticulo = topicoController.GetTopicosArticuloLista(idArticuloPK);

                articuloController.CargarArticuloDOCX(idArticuloPK, rutaCarpeta);

                nombreArchivo = idArticuloPK + ".docx";

                TempData["idArticulo"] = idArticuloPK;
            }
        }

		public void OnPostGuardar()
		{	
			if (EsValido())
			{
				articulo.tipo = TipoArticulo.Largo;
				articulo.estado = EstadoArticulo.EnProgreso;
				articuloController.GuardarArticulo(articulo, listaMiembrosAutores, listaTopicosArticulo);
				Notificaciones.Set(this, "articuloGuardado", "Su articulo se guardó", Notificaciones.TipoNotificacion.Exito);
			}
		}

        public void OnPostEditar()
        {
            if (EsValido())
            {
                articulo.tipo = TipoArticulo.Largo;
                articulo.estado = EstadoArticulo.EnProgreso;
                articulo.idArticuloPK = (int)TempData["idArticulo"];
                articuloController.EditarArticulo(articulo, listaMiembrosAutores, listaTopicosArticulo);
                Notificaciones.Set(this, "articuloGuardado", "Su articulo se guardó", Notificaciones.TipoNotificacion.Exito);
            }
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