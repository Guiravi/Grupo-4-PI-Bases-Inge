using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LaCafeteria.Models;
using LaCafeteria.Controllers;

namespace LaCafeteria.Pages
{
    public class BuscarModel : PageModel
    {
        public List<TopicoModel> listaTopicos { set; get; }

        public List<ArticuloModel> articulosResultado { set; get; }

        [BindProperty]
        public int tiposArticulo { get; set; }

        [BindProperty]
        public string tipoBusqueda { get; set; }

        [BindProperty]
        public string textoBusqueda { get; set; }

        [BindProperty]
        public List<string> listaTopicosSelec { set; get; }

        public TopicoController topicoController;

        public ArticuloController articuloController;

        public int cantResultados { set; get; }

        public BuscarModel()
        {
            topicoController = new TopicoController();
            articuloController = new ArticuloController();
            listaTopicos = topicoController.GetListaTopicos();

            listaTopicosSelec = new List<string>();
            articulosResultado = new List<ArticuloModel>();
            textoBusqueda = "";
            cantResultados = -1;
        }


        public void OnGet()
        {

        }

        public void OnPost()
        {
            SolicitudBusquedaModel solicitud;
            if (tipoBusqueda == "topicos")
            {
                string topicosSelec = "";

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
                solicitud = new SolicitudBusquedaModel(tipoBusqueda, "", tiposArticulo, textoBusqueda);
            }

            cantResultados = articulosResultado.Count;
            articulosResultado = articuloController.BuscarArticulo(solicitud);

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