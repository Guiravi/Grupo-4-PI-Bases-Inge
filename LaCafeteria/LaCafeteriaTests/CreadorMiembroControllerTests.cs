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
    public class CreadorMiembroControllerTests
    {
        [TestMethod]
        public void TestsCrearMiembroTrue() {
            // Arrange
            MiembroModel miembro = new MiembroModel()
            {
                usernamePK = "kwang",
                nombre = "Kevin",
                apellido1 = "Wang",
                email = "kwang@hola.com",

            };

            var mockDBHandler = new Mock<ICreadorMiembroDBHandler>();
            mockDBHandler.Setup(x => x.CrearMiembro(miembro)).Verifiable();

            var controller = new CreadorMiembrosController(mockDBHandler.Object);

            // Act
            controller.CrearMiembro(miembro);

            // Assert
            mockDBHandler.Verify(x => x.CrearMiembro(miembro), Times.Once);
        }

        [TestMethod]
        public void TestsCrearMiembroFalse() {
            // Arrange
            MiembroModel miembro = null;

            var mockDBHandler = new Mock<ICreadorMiembroDBHandler>();
            mockDBHandler.Setup(x => x.CrearMiembro(miembro)).Verifiable();

            var controller = new CreadorMiembrosController(mockDBHandler.Object);

            // Act
            controller.CrearMiembro(miembro);

            // Assert
            mockDBHandler.Verify(x => x.CrearMiembro(miembro), Times.Never);
        }
    }
}
