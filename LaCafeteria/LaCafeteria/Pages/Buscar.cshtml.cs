using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Models;
using LaCafeteria.Controllers;
using LaCafeteria.Utilidades;

namespace LaCafeteria.Pages
{
    public class BuscarModel : PageModel
    {
        public List<CategoriaTopicoModel> listaTopicos { set; get; }

        public List<ArticuloModel> articulosResultado { set; get; }

        [BindProperty]
        public int tiposArticulo { get; set; }

        [BindProperty]
        public string tipoBusqueda { get; set; }

        [BindProperty]
        public string textoBusqueda { get; set; }

        [BindProperty]
        public List<string> listaTopicosSelec { set; get; }

        private InformacionCategoriaTopicoController informacionCategoriaTopicoController;
        private BuscadorArticuloController buscadorArticuloController;


        public int cantResultados { set; get; }  

        [BindProperty(SupportsGet = true)]
        public int indice { set; get; }
        public int articulosPorPagina { set; get; } = 8;
        public int totalPaginas;

        [BindProperty(SupportsGet = true)]
        public int todos { set; get; } = 0;


        public BuscarModel()
        {
            informacionCategoriaTopicoController = new InformacionCategoriaTopicoController();
            buscadorArticuloController = new BuscadorArticuloController();
            listaTopicos = informacionCategoriaTopicoController.GetCategoriasYTopicos();

            listaTopicosSelec = new List<string>();
            articulosResultado = new List<ArticuloModel>();
            textoBusqueda = "";
            cantResultados = -1;
        }


        public void OnGet()
        {
            if (indice != 0)
            {
                if (todos != 0)
                {
                    articulosResultado = buscadorArticuloController.GetTodosArticulos();
                    cantResultados = articulosResultado.Count;
                    articulosResultado = PaginarResultados(articulosResultado, indice, articulosPorPagina);
                    totalPaginas = (int)Math.Ceiling(decimal.Divide(cantResultados, articulosPorPagina));                  
                }
                else
                {
                    tipoBusqueda = (string)TempData["tipoBusqueda"];
                    string topicosSelec = (string)TempData["topicos"];
                    tiposArticulo = (int)TempData["tiposArticulo"];

                    if (tipoBusqueda == "topicos")
                    {
                        textoBusqueda = (string)TempData["topicos"];
                    }
                    else
                    {
                        textoBusqueda = (string)TempData["textoBusqueda"];
                    }

                    SolicitudBusquedaModel solicitud = new SolicitudBusquedaModel(tipoBusqueda, topicosSelec,
                    tiposArticulo, textoBusqueda);

                    articulosResultado = buscadorArticuloController.BuscarArticulo(solicitud);
                    cantResultados = articulosResultado.Count;
                    articulosResultado = PaginarResultados(articulosResultado, indice, articulosPorPagina);
                    totalPaginas = (int)Math.Ceiling(decimal.Divide(cantResultados, articulosPorPagina));

                    TempData["tipoBusqueda"] = solicitud.tipoBusqueda;
                    TempData["topicos"] = solicitud.topicos;
                    TempData["tiposArticulo"] = solicitud.tiposArticulo;
                    TempData["textoBusqueda"] = solicitud.textoBusqueda;
                }                              
            }
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult OnPost()
        {
            
            SolicitudBusquedaModel solicitud;
            if (tipoBusqueda == "topicos")
            {
                string topicosSelec = "";

                if (listaTopicosSelec.Count == 0)
                {
                    Notificaciones.Set(this, "ningunTopico", "Debe ingresar algún tópico de la lista", Notificaciones.TipoNotificacion.Error);
                    return Page();
                }

                for (int i = 0; i < listaTopicosSelec.Count; i++)
                {
                    if (i != listaTopicosSelec.Count - 1)
                        topicosSelec += listaTopicosSelec[i] + ",";
                    else
                        topicosSelec += listaTopicosSelec[i];
                    
                }
                
                solicitud = new SolicitudBusquedaModel(tipoBusqueda, topicosSelec, tiposArticulo, "");
            }
            else
            {
                if (textoBusqueda == null)
                {
                    Notificaciones.Set(this, "busquedaVacio", "Debe ingresar texto en la barra de búsqueda", Notificaciones.TipoNotificacion.Error);
                    return Page();
                }
                else
                {
                    solicitud = new SolicitudBusquedaModel(tipoBusqueda, "", tiposArticulo, textoBusqueda);
                }               
               
            }      
            
            TempData["tipoBusqueda"] = solicitud.tipoBusqueda;
            TempData["topicos"] = solicitud.topicos;
            TempData["tiposArticulo"] = solicitud.tiposArticulo;
            TempData["textoBusqueda"] = solicitud.textoBusqueda;

            return Redirect("/Buscar/1");
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult OnPostTodos()
        {
            return Redirect("/Buscar/1/1");
        }

        private List<ArticuloModel> PaginarResultados(List<ArticuloModel> resultados, int currentPage, int pageSize = 8)
        {
            return resultados.OrderBy(d => d.fechaPublicacion).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

    }

    public class SolicitudBusquedaModel
    {
        public string tipoBusqueda { get; set; }

        public string topicos { get; set; }

        public int tiposArticulo { get; set; }

        public string textoBusqueda { get; set; }

        public SolicitudBusquedaModel(string tipoBus, string tops, int tiposArt, string textB)
        {
            tipoBusqueda = tipoBus;

            topicos = tops;

            tiposArticulo = tiposArt;

            textoBusqueda = textB;

        }

    }

}