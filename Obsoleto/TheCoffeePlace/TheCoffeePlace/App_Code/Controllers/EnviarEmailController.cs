using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mime;
using System.Net.Mail;
using System.Text;
using TheCoffeePlace.Views;
using TheCoffeePlace.Models;

/// <summary>
/// Summary description for EnviarEmailController
/// </summary>
/// 
namespace TheCoffeePlace.Controllers
{
    public class EnviarEmailController
    {
        private List<AutorModel> listaAutores;

        public EnviarEmailController()
        {
            AutorDBHandler autorHandler = new AutorDBHandler();
            listaAutores = autorHandler.getListaAutores();
        }

        public List<string> getListaAutores()
        {
            List<string> stringAutores = new List<string>();

            foreach ( AutorModel autor in listaAutores )
            {
                stringAutores.Add(autor.nombre + " " + autor.apellido1 + " " + autor.apellido2 + " (" + autor.usernamePK + ")");
            }

            return stringAutores;
        }

        public void sendMail(IView_EnviarEmail view)
        {
            string destino = view.destino;
            string usernameDestino = destino.Split('(', ')')[1];

            AutorModel autorDestino = listaAutores.Find(x => x.usernamePK == usernameDestino);
            string nombreCompleto = autorDestino.nombre + " " + autorDestino.apellido1 + " " + autorDestino.apellido2;
            string emailAddress = autorDestino.email;

            string asunto = "[TheCoffeePlace] " + view.asunto;
            string mensajeHtml = "<h3>Estimado " + nombreCompleto + ",</h3>" +
                "<p><i>Has recibido un mensaje de The Coffee Place: </i><br/><br/>" + view.mensaje +
                "<br/><br/><i> Para responder, por favor hacerlo desde nuestro sitio web. </i><br/><br/></p>";

            MailAddress fromAddress = new MailAddress("thecoffeeplace2020@gmail.com", "The Coffee Place");
            MailAddress toAddress = new MailAddress(emailAddress, nombreCompleto);

            const string fromPassword = "thecoffeeplace";

            var smtp = new SmtpClient {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };


            LinkedResource resource = new LinkedResource(HttpContext.Current.Server.MapPath("~/Imagenes/Sitio/logoTCP.png"), "image/png");
            resource.ContentId = Guid.NewGuid().ToString();
            string htmlImage = @"<img src='cid:" + resource.ContentId + @"'/>";

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mensajeHtml + htmlImage, Encoding.UTF8, MediaTypeNames.Text.Html);
            htmlView.LinkedResources.Add(resource);

            using ( var message = new MailMessage(fromAddress, toAddress) {
                Subject = asunto,
                IsBodyHtml = true,
            } )
            {
                message.AlternateViews.Add(htmlView);
                smtp.Send(message);
            }

        }
    }
}