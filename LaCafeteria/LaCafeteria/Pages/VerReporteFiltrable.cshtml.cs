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
    public class VerReporteFiltrableModel : PageModel
    {
        public List<DatosGraficoDona> miembrosPorPais;
        public List<DatosGraficoBarrasApilado> habilidadesPorIdioma;
        public List<DatosGraficoBarrasApilado> habilidadesPorPais;
        public List<DatosGraficoBarrasApilado> pasatiemposPorIdioma;
        public List<DatosGraficoBarrasApilado> pasatiemposPorPais;
        public List<DatosGraficoDona> miembrosPorRol;
        public List<DatosGraficoDona> articulosPorRol;
        public List<DatosTablaCategoriaTopicos> datosCatTopTodosRoles;
        public List<DatosTablaCategoriaTopicos> datosCatTopNucleo;
        public List<DatosTablaCategoriaTopicos> datosCatTopActivo;
        public List<DatosTablaCategoriaTopicos> datosCatTopPeriferico;
        public List<CategoriaTopicoModel> datosCatTopNoAsociadosTodos;
        public List<CategoriaTopicoModel> datosCatTopNoAsociadosNucleo;
        public List<CategoriaTopicoModel> datosCatTopNoAsociadosActivo;
        public List<CategoriaTopicoModel> datosCatTopNoAsociadosPeriferico;


        private InformacionMiembroController informacionMiembroController;
        private InformacionArticuloController informacionArticuloController;
        private InformacionCategoriaTopicoController informacionCategoriaTopicoController;

        public VerReporteFiltrableModel()
        {
            miembrosPorPais = new List<DatosGraficoDona>();
            habilidadesPorPais = new List<DatosGraficoBarrasApilado>();
            habilidadesPorIdioma = new List<DatosGraficoBarrasApilado>();
            pasatiemposPorPais = new List<DatosGraficoBarrasApilado>();
            pasatiemposPorIdioma = new List<DatosGraficoBarrasApilado>();
            miembrosPorPais = new List<DatosGraficoDona>();
            articulosPorRol = new List<DatosGraficoDona>();
            datosCatTopTodosRoles = new List<DatosTablaCategoriaTopicos>();
            datosCatTopNucleo = new List<DatosTablaCategoriaTopicos>();
            datosCatTopActivo = new List<DatosTablaCategoriaTopicos>();
            datosCatTopPeriferico = new List<DatosTablaCategoriaTopicos>();
            datosCatTopNoAsociadosTodos = new List<CategoriaTopicoModel>();
            datosCatTopNoAsociadosNucleo = new List<CategoriaTopicoModel>();
            datosCatTopNoAsociadosActivo = new List<CategoriaTopicoModel>();
            datosCatTopNoAsociadosPeriferico = new List<CategoriaTopicoModel>();

            informacionMiembroController = new InformacionMiembroController();
            informacionArticuloController = new InformacionArticuloController();
            informacionCategoriaTopicoController = new InformacionCategoriaTopicoController();
           
        }

        public void OnGet()
        {
            miembrosPorPais = informacionMiembroController.GetMiembrosPorPais();
            habilidadesPorPais = informacionMiembroController.GetHabilidadesPorPais();
            habilidadesPorIdioma = informacionMiembroController.GetHabilidadesPorIdioma();
            pasatiemposPorPais = informacionMiembroController.GetPasatiemposPorPais();
            pasatiemposPorIdioma = informacionMiembroController.GetPasatiemposPorIdioma();
            miembrosPorRol = informacionMiembroController.GetMiembrosPorRol();
            articulosPorRol = informacionArticuloController.GetArticulosPorRol();
            datosCatTopTodosRoles = informacionArticuloController.GetDatosTablaCategoriaTopicosPorRol("");
            datosCatTopNucleo = informacionArticuloController.GetDatosTablaCategoriaTopicosPorRol("Núcleo");
            datosCatTopActivo = informacionArticuloController.GetDatosTablaCategoriaTopicosPorRol("Activo");
            datosCatTopPeriferico = informacionArticuloController.GetDatosTablaCategoriaTopicosPorRol("Periférico");
            datosCatTopNoAsociadosTodos = informacionCategoriaTopicoController.GetCategoriasTopicosNoAsociadosRol("");
            datosCatTopNoAsociadosNucleo = informacionCategoriaTopicoController.GetCategoriasTopicosNoAsociadosRol("Núcleo");
            datosCatTopNoAsociadosActivo = informacionCategoriaTopicoController.GetCategoriasTopicosNoAsociadosRol("Activo");
            datosCatTopNoAsociadosPeriferico = informacionCategoriaTopicoController.GetCategoriasTopicosNoAsociadosRol("Periférico");

            foreach (CategoriaTopicoModel catTop in datosCatTopNoAsociadosTodos)
            {
                DatosTablaCategoriaTopicos datoVacio = new DatosTablaCategoriaTopicos("", catTop.nombreCategoriaPK, catTop.nombreTopicoPK, 0, 0, 0);
                datosCatTopTodosRoles.Add(datoVacio);
            }

            datosCatTopTodosRoles.Sort((p, q) => p.nombreCategoriaFK.CompareTo(q.nombreCategoriaFK));

            foreach (CategoriaTopicoModel catTop in datosCatTopNoAsociadosNucleo)
            {
                DatosTablaCategoriaTopicos datoVacio = new DatosTablaCategoriaTopicos("", catTop.nombreCategoriaPK, catTop.nombreTopicoPK, 0, 0, 0);
                datosCatTopNucleo.Add(datoVacio);
            }

            datosCatTopNucleo.Sort((p, q) => p.nombreCategoriaFK.CompareTo(q.nombreCategoriaFK));

            foreach (CategoriaTopicoModel catTop in datosCatTopNoAsociadosActivo)
            {
                DatosTablaCategoriaTopicos datoVacio = new DatosTablaCategoriaTopicos("", catTop.nombreCategoriaPK, catTop.nombreTopicoPK, 0, 0, 0);
                datosCatTopActivo.Add(datoVacio);
            }

            datosCatTopActivo.Sort((p, q) => p.nombreCategoriaFK.CompareTo(q.nombreCategoriaFK));

            foreach (CategoriaTopicoModel catTop in datosCatTopNoAsociadosPeriferico)
            {
                DatosTablaCategoriaTopicos datoVacio = new DatosTablaCategoriaTopicos("", catTop.nombreCategoriaPK, catTop.nombreTopicoPK, 0, 0, 0);
                datosCatTopPeriferico.Add(datoVacio);
            }

            datosCatTopPeriferico.Sort((p, q) => p.nombreCategoriaFK.CompareTo(q.nombreCategoriaFK));

        }      
       
    }
}