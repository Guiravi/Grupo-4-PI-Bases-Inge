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
    public class CalificadorControllerTests
    {
        [DataTestMethod]
        [DataRow("username", 1, -1)]
        [DataRow("username", 1, 0)]
        [DataRow("username", 1, 1)]
        public void TestsBuscarArticuloPorTopicoTrue(string username, int idArticulo, int calificacion) {
            // Arrange
            var mockDBHandler = new Mock<ICalificadorDeArticulosDBHandler>();
            mockDBHandler.Setup(x => x.CalificarArticulo(username, idArticulo, calificacion)).Verifiable();

            var controller = new CalificadorDeArticuloController(mockDBHandler.Object);

            // Act
            controller.CalificarArticulo(username, idArticulo, calificacion);

            // Assert
            mockDBHandler.Verify(x => x.CalificarArticulo(username, idArticulo, calificacion), Times.Once);
        }

        [DataTestMethod]
        [DataRow("username", 1, -2)]
        [DataRow("username", 1, 3)]
        [DataRow("username", 1, 10)]
        public void TestsBuscarArticuloPorTopicoFalse(string username, int idArticulo, int calificacion) {
            // Arrange
            var mockDBHandler = new Mock<ICalificadorDeArticulosDBHandler>();
            mockDBHandler.Setup(x => x.CalificarArticulo(username, idArticulo, calificacion)).Verifiable();

            var controller = new CalificadorDeArticuloController(mockDBHandler.Object);

            // Act
            controller.CalificarArticulo(username, idArticulo, calificacion);

            // Assert
            mockDBHandler.Verify(x => x.CalificarArticulo(username, idArticulo, calificacion), Times.Never);
        }
    }
}
