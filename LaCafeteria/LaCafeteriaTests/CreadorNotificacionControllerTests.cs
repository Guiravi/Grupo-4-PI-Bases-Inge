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
    public class CreadorNotificacionControllerTests
    {
        [TestMethod]
        public void TestsCrearMiembroTrue() {
            // Arrange
            Notificacion notificacion = new Notificacion()
            {
                mensaje = "hola",
                url = "www.google.com",
                usernameFK = "kwang"
            };

            var mockDBHandler = new Mock<ICreadorNotificacionDBHandler>();
            mockDBHandler.Setup(x => x.CrearNotificacion(notificacion)).Verifiable();

            var controller = new CreadorNotificacionController(mockDBHandler.Object);

            // Act
            controller.CrearNotificacion(notificacion);

            // Assert
            mockDBHandler.Verify(x => x.CrearNotificacion(notificacion), Times.Once);
        }

        [TestMethod]
        public void TestsCrearMiembroFalse() {
            // Arrange
            Notificacion notificacion = null;


            var mockDBHandler = new Mock<ICreadorNotificacionDBHandler>();
            mockDBHandler.Setup(x => x.CrearNotificacion(notificacion)).Verifiable();

            var controller = new CreadorNotificacionController(mockDBHandler.Object);

            // Act
            controller.CrearNotificacion(notificacion);

            // Assert
            mockDBHandler.Verify(x => x.CrearNotificacion(notificacion), Times.Never);
        }
    }
}
