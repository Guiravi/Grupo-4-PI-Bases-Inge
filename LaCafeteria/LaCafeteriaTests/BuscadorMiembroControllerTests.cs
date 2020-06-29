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
    public class BuscadorMiembroControllerTests
    {
        [TestMethod]
        public void TestsGetListaMiembrosString() {

            List<MiembroModel> mockMiembros = new List<MiembroModel> {
                new MiembroModel{nombre = "Kevin", apellido1 = "Wang", apellido2 = "Qiu", usernamePK = "kwang"},
                new MiembroModel{nombre = "Silvio", apellido1 = "Salazar", usernamePK = "silvi0nsky"}
            };

            var mockDBHandler = new Mock<IBuscadorMiembroDBHandler>();
            mockDBHandler.Setup(x => x.GetListaMiembros()).Returns(mockMiembros);

            var controller = new BuscadorMiembrosController(mockDBHandler.Object);

            // Act
            var miembros = controller.GetListaMiembrosString();

            // Assert
            Assert.AreEqual(miembros[0], "Kevin Wang Qiu (kwang)");
            Assert.AreEqual(miembros[1], "Silvio Salazar (silvi0nsky)");
        }
    }
}
