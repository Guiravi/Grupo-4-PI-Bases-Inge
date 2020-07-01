using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Moq;
using LaCafeteria.Models;
using LaCafeteria.Controllers;
using LaCafeteria.Models.Handlers;
using LaCafeteria.Pages;

namespace LaCafeteriaTests
{
    [TestClass]
    public class BuscadorArticuloControllerTests
    {
        [TestMethod]
        public void TestsBuscarArticuloPorTopico() {
            // Arrange
            List<string> topicos = new List<string>();
            string tipoBusqueda = "topicos";
            topicos.Add("Fisica: Fisica Cuantica");
            topicos.Add("Fisica: Fisica Nuclear");
            int tipoArt = 0;
            string textB = "";

            SolicitudBusquedaModel solicitud = new SolicitudBusquedaModel(tipoBusqueda, topicos, tipoArt, textB);

            CategoriaTopicoModel categoriaTopico1 = new CategoriaTopicoModel();
            CategoriaTopicoModel categoriaTopico2 = new CategoriaTopicoModel();
            categoriaTopico1.nombreCategoriaPK = "Fisica";
            categoriaTopico1.nombreTopicoPK = "Fisica Cuantica";
            categoriaTopico2.nombreCategoriaPK = "Fisica";
            categoriaTopico2.nombreTopicoPK = "Fisica Nuclear";

            List<ArticuloModel> mockArticulos1 = new List<ArticuloModel> {
                new ArticuloModel{articuloAID = 1, titulo = "prueba", calificacionTotalMiem = 0, contenido = "contenido",
                    estado = "publicado", fechaPublicacion = "2020-06-20", resumen = "resumen", tipo = "Corto", visitas = 0},
                new ArticuloModel{articuloAID = 2, titulo = "prueba", calificacionTotalMiem = 0, contenido = "contenido",
                    estado = "publicado", fechaPublicacion = "2020-06-20", resumen = "resumen", tipo = "Corto", visitas = 0},
            };

            List<ArticuloModel> mockArticulos2 = new List<ArticuloModel> {
                new ArticuloModel{articuloAID = 3, titulo = "prueba", calificacionTotalMiem = 0, contenido = "contenido",
                    estado = "publicado", fechaPublicacion = "2020-06-20", resumen = "resumen", tipo = "Corto", visitas = 0},
                new ArticuloModel{articuloAID = 4, titulo = "prueba", calificacionTotalMiem = 0, contenido = "contenido",
                    estado = "publicado", fechaPublicacion = "2020-06-20", resumen = "resumen", tipo = "Corto", visitas = 0},
            };

            var mockDBHandler = new Mock<IBuscadorArticuloDBHandler>();
            mockDBHandler.Setup(x => x.GetArticulosPorTopicoYTipo(It.Is<CategoriaTopicoModel>(t => t.nombreCategoriaPK == "Fisica" && t.nombreTopicoPK== "Fisica Cuantica"), tipoArt)).Returns(mockArticulos1);
            mockDBHandler.Setup(x => x.GetArticulosPorTopicoYTipo(It.Is<CategoriaTopicoModel>(t => t.nombreCategoriaPK == "Fisica" && t.nombreTopicoPK == "Fisica Nuclear"), tipoArt)).Returns(mockArticulos2);

            var controller = new BuscadorArticuloController(mockDBHandler.Object);

            // Act
            var articulos = controller.BuscarArticulo(solicitud);

            // Assert
            Assert.AreEqual(articulos[0].articuloAID, 1);
            Assert.AreEqual(articulos[1].articuloAID, 2);
            Assert.AreEqual(articulos[2].articuloAID, 3);
            Assert.AreEqual(articulos[3].articuloAID, 4);
        }

        [TestMethod]
        public void TestsBuscarArticuloPorTitulo() {
            // Arrange
            List<string> topicos = new List<string>();
            string tipoBusqueda = "titulos";
            int tipoArt = 0;
            string textB = "Autor";

            SolicitudBusquedaModel solicitud = new SolicitudBusquedaModel(tipoBusqueda, topicos, tipoArt, textB);

            List<ArticuloModel> mockArticulos1 = new List<ArticuloModel> {
                new ArticuloModel{articuloAID = 1, titulo = "prueba", calificacionTotalMiem = 0, contenido = "contenido",
                    estado = "publicado", fechaPublicacion = "2020-06-20", resumen = "resumen", tipo = "Corto", visitas = 0},
                new ArticuloModel{articuloAID = 2, titulo = "prueba", calificacionTotalMiem = 0, contenido = "contenido",
                    estado = "publicado", fechaPublicacion = "2020-06-20", resumen = "resumen", tipo = "Corto", visitas = 0},
            };

            var mockDBHandler = new Mock<IBuscadorArticuloDBHandler>();
            mockDBHandler.Setup(x => x.GetArticulosPorTituloYTipo(textB, tipoArt)).Returns(mockArticulos1);
            
            var controller = new BuscadorArticuloController(mockDBHandler.Object);

            // Act
            var articulos = controller.BuscarArticulo(solicitud);

            // Assert
            Assert.AreEqual(articulos[0].articuloAID, 1);
            Assert.AreEqual(articulos[1].articuloAID, 2);
        }

        [TestMethod]
        public void TestsBuscarArticuloElse() {
            // Arrange
            List<string> topicos = new List<string>();
            string tipoBusqueda = "Any";
            int tipoArt = 0;
            string textB = "Autor";

            SolicitudBusquedaModel solicitud = new SolicitudBusquedaModel(tipoBusqueda, topicos, tipoArt, textB);

            List<ArticuloModel> mockArticulos1 = new List<ArticuloModel> {
                new ArticuloModel{articuloAID = 1, titulo = "prueba", calificacionTotalMiem = 0, contenido = "contenido",
                    estado = "publicado", fechaPublicacion = "2020-06-20", resumen = "resumen", tipo = "Corto", visitas = 0},
                new ArticuloModel{articuloAID = 2, titulo = "prueba", calificacionTotalMiem = 0, contenido = "contenido",
                    estado = "publicado", fechaPublicacion = "2020-06-20", resumen = "resumen", tipo = "Corto", visitas = 0},
            };

            var mockDBHandler = new Mock<IBuscadorArticuloDBHandler>();
            mockDBHandler.Setup(x => x.GetArticulosPorAutorYTipo(textB, tipoArt)).Returns(mockArticulos1);

            var controller = new BuscadorArticuloController(mockDBHandler.Object);

            // Act
            var articulos = controller.BuscarArticulo(solicitud);

            // Assert
            Assert.AreEqual(articulos[0].articuloAID, 1);
            Assert.AreEqual(articulos[1].articuloAID, 2);
        }
    }
}
