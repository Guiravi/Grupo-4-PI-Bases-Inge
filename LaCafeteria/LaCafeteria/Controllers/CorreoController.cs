using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using LaCafeteria.Models;

namespace LaCafeteria.Controllers
{
    public class CorreoController
    {
        public MiembroDBHandler miembroDBHandler;

        public List<MiembroModel> listaNucleos;








        public void sendNecesitaRevision(string titulo) {
            string usernameDestino = "";
            string nombreCompletoDestino = "";
            string nombreCompletoRemitente = "";
            string emailAddress = "";

            string asunto = "[LaCafeteria] Un nuevo artículo requiere revisión";

            string mensajeHtml = "<h3>Estimado " + nombreCompletoDestino + ",</h3>" +
                "<p><i>Se ha subido un nuevo artículo a la comunidad La Cafeteria. El título es: </i><br/><br/>" + titulo +
                "<br/><br/><i> Para ver más detalles, por favor visitar el sitio web. </i><br/><br/></p>";

            listaNucleos = miembroDBHandler.GetListaNucleos();

            MailAddress fromAddress = new MailAddress("thecoffeeplace2020@gmail.com", "La Cafeteria");

            const string fromPassword = "thecoffeeplace";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            LinkedResource resource = new LinkedResource(rutaCarpeta + "/images/TCP.png", "image/png");
            resource.ContentId = Guid.NewGuid().ToString();
            string htmlImage = @"<img src='cid:" + resource.ContentId + @"'/>";

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mensajeHtml + htmlImage, Encoding.UTF8, MediaTypeNames.Text.Html);
            htmlView.LinkedResources.Add(resource);

            var message = new MailMessage();

            message.From = fromAddress;

            foreach ( MiembroModel nucleo in listaNucleos)
            {
                message.To.Add(new MailAddress(nucleo.email));
            }

            message.Subject = asunto;
            message.IsBodyHtml = true;

            message.AlternateViews.Add(htmlView);
            smtp.Send(message);
        }

    }
}
