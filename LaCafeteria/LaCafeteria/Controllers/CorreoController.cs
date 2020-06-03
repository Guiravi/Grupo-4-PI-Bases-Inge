using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using LaCafeteria.Models;

namespace LaCafeteria.Controllers
{
    public class CorreoController
    {
        public MiembroDBHandler miembroDBHandler;
        public List<MiembroModel> listaMiembros;

        public string rutaCarpeta;
        public string rutaAdjuntos;

        public CorreoController(IHostingEnvironment env) {
            miembroDBHandler = new MiembroDBHandler();
            listaMiembros = miembroDBHandler.GetListaMiembros();
            rutaCarpeta = env.WebRootPath;
            rutaAdjuntos = env.WebRootPath + "/Correos";
        }

        public List<string> getListaMiembrosString() {
            List<string> stringMiembros = new List<string>();

            foreach ( MiembroModel miembro in listaMiembros )
            {
                stringMiembros.Add(miembro.nombre + " " + miembro.apellido1 + " " + miembro.apellido2 + " (" + miembro.usernamePK + ")");
            }

            return stringMiembros;
        }

        public void sendMail(string destino, string remitente, string asunto, string mensaje) {
            string usernameDestino = destino.Split('(', ')')[1];

            MiembroModel miembroDestino = listaMiembros.Find(x => x.usernamePK == usernameDestino);
            MiembroModel miembroRemitente = listaMiembros.Find(x => x.usernamePK == remitente);
            string nombreCompletoDestino = miembroDestino.nombre + " " + miembroDestino.apellido1 + " " + miembroDestino.apellido2;
            string nombreCompletoRemitente = miembroRemitente.nombre + " " + miembroRemitente.apellido1 + " " + miembroRemitente.apellido2;
            string emailAddress = miembroDestino.email;

            asunto = "[LaCafeteria] " + asunto;
            string mensajeHtml = "<h3>Estimado " + nombreCompletoDestino + ",</h3>" +
                "<p><i>Has recibido un mensaje de la comunidad La Cafeteria del miembro " + nombreCompletoRemitente + ": </i><br/><br/>" + mensaje +
                "<br/><br/><i> Para responder, por favor hacerlo desde nuestro sitio web. </i><br/><br/></p>";

            MailAddress fromAddress = new MailAddress("thecoffeeplace2020@gmail.com", "La Cafeteria");
            MailAddress toAddress = new MailAddress(emailAddress, nombreCompletoDestino);

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

            using ( var message = new MailMessage(fromAddress, toAddress)
            {
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
