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
    public class InformacionArticuloControllerTests
    {
        [TestMethod]
        public void TestsGetAutoresDeArticuloString() {
            int id = 5;

            List<MiembroModel> mockMiembros = new List<MiembroModel> {
                new MiembroModel{nombre = "Kevin", apellido1 = "Wang", apellido2 = "Qiu", usernamePK = "kwang"},
                new MiembroModel{nombre = "Silvio", apellido1 = "Salazar", usernamePK = "silvi0nsky"}
            };

            var mockDBHandler = new Mock<IInformacionArticuloDBHandler>();
            mockDBHandler.Setup(x => x.GetAutoresDeArticulo(id)).Returns(mockMiembros);

            var controller = new InformacionArticuloController(mockDBHandler.Object);

            // Act
            var autores = controller.GetAutoresDeArticuloString(id);

            // Assert
            Assert.AreEqual(autores, "Kevin Wang Qiu, Silvio Salazar");
        }

        [TestMethod]
        public void TestsGetAutoresDeArticuloListaStringArray() {
            int id = 5;

            List<MiembroModel> mockMiembros = new List<MiembroModel> {
                new MiembroModel{nombre = "Kevin", apellido1 = "Wang", apellido2 = "Qiu", usernamePK = "kwang"},
                new MiembroModel{nombre = "Silvio", apellido1 = "Salazar", usernamePK = "silvi0nsky"}
            };

            var mockDBHandler = new Mock<IInformacionArticuloDBHandler>();
            mockDBHandler.Setup(x => x.GetAutoresDeArticulo(id)).Returns(mockMiembros);

            var controller = new InformacionArticuloController(mockDBHandler.Object);

            // Act
            var autores = controller.GetAutoresArticuloListaStringArray(id);

            // Assert
            Assert.AreEqual(autores[0][0], "kwang");
            Assert.AreEqual(autores[0][1], "Kevin Wang Qiu");
            Assert.AreEqual(autores[1][0], "silvi0nsky");
            Assert.AreEqual(autores[1][1], "Silvio Salazar");
        }

        [TestMethod]
        public void TestsGetRevisoresDeArticulo() {
            int id = 5;

            List<string> mockRevisores = new List<string> {
               "Kevin Wang Qiu",
               "Marco Mora"
            };

            var mockDBHandler = new Mock<IInformacionArticuloDBHandler>();
            mockDBHandler.Setup(x => x.GetRevisoresDeArticulo(id)).Returns(mockRevisores);

            var controller = new InformacionArticuloController(mockDBHandler.Object);

            // Act
            var revisores = controller.GetRevisoresDeArticulo(id);

            // Assert
            Assert.AreEqual(revisores, "Kevin Wang Qiu, Marco Mora");
        }
    }
}
