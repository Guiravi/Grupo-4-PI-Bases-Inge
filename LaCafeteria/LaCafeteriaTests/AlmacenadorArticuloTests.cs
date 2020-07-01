using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using LaCafeteria.Models;
using LaCafeteria.Controllers;
using LaCafeteria.Models.Handlers;
using LaCafeteria.Pages;


namespace LaCafeteriaTests
{
    [TestClass]
    public class AlmacenadorArticuloTests
    {
        private static List<CategoriaTopicoModel> listaModels;
        private static List<string> listaAutores;
        private static List<string> listaTopicosString;
        private static ArticuloModel articulo;

        public AlmacenadorArticuloTests() {
            CategoriaTopicoModel cattop1 = new CategoriaTopicoModel();
            CategoriaTopicoModel cattop2 = new CategoriaTopicoModel();

            cattop1.nombreCategoriaPK = "Fisica";
            cattop1.nombreCategoriaPK = "Fisica nuclear";
            cattop2.nombreCategoriaPK = "Medicina";
            cattop2.nombreTopicoPK = "Medicina nuclear";

            listaModels = new List<CategoriaTopicoModel>();
            listaModels.Add(cattop1);
            listaModels.Add(cattop2);

            listaAutores = new List<string>();
            listaAutores.Add("kwang");
            listaAutores.Add("kev");

            listaTopicosString = new List<string>();
            listaTopicosString.Add("Fisica: Fisica nuclear");
            listaTopicosString.Add("Medicina: Medicina nuclear");

            articulo = new ArticuloModel();
            articulo.contenido = "Blablablabla";
            articulo.fechaPublicacion = "2020/5/3";
        }

        [TestMethod]
        public void TestsAlmacenarArticuloTrue() {
            // Arrange

            var mockDBHandler = new Mock<IAlmacenadorArticuloDBHandler>();
            mockDBHandler.Setup(x => x.GuardarArticulo(It.IsAny<ArticuloModel>(), It.IsAny<List<string>>(), It.IsAny<List<CategoriaTopicoModel>>())).Verifiable();

            var controller = new AlmacenadorArticuloController(mockDBHandler.Object);

            // Act
            controller.GuardarArticulo(articulo, listaAutores, listaTopicosString);

            // Assert
            mockDBHandler.Verify(x => x.GuardarArticulo(It.IsAny<ArticuloModel>(), It.IsAny<List<string>>(), It.IsAny<List<CategoriaTopicoModel>>()), Times.Once);
        }

        [TestMethod]
        public void TestsAlmacenarArticuloNull() {
            // Arrange

            var mockDBHandler = new Mock<IAlmacenadorArticuloDBHandler>();
            mockDBHandler.Setup(x => x.GuardarArticulo(It.IsAny<ArticuloModel>(), It.IsAny<List<string>>(), It.IsAny<List<CategoriaTopicoModel>>())).Verifiable();

            var controller = new AlmacenadorArticuloController(mockDBHandler.Object);

            // Act
            controller.GuardarArticulo(null, listaAutores, listaTopicosString);

            // Assert
            mockDBHandler.Verify(x => x.GuardarArticulo(It.IsAny<ArticuloModel>(), It.IsAny<List<string>>(), It.IsAny<List<CategoriaTopicoModel>>()), Times.Never);
        }

        [TestMethod]
        public void TestsAlmacenarArticuloCount0() {
            // Arrange

            var mockDBHandler = new Mock<IAlmacenadorArticuloDBHandler>();
            mockDBHandler.Setup(x => x.GuardarArticulo(It.IsAny<ArticuloModel>(), It.IsAny<List<string>>(), It.IsAny<List<CategoriaTopicoModel>>())).Verifiable();

            var controller = new AlmacenadorArticuloController(mockDBHandler.Object);

            // Act
            controller.GuardarArticulo(articulo, new List<string>() , listaTopicosString);

            // Assert
            mockDBHandler.Verify(x => x.GuardarArticulo(It.IsAny<ArticuloModel>(), It.IsAny<List<string>>(), It.IsAny<List<CategoriaTopicoModel>>()), Times.Never);
        }
    }
}
