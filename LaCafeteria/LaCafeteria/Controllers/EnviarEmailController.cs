﻿using System;
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
using LaCafeteria.Models.Handlers;

namespace LaCafeteria.Controllers
{
    public class EnviarEmailController
    {
        private BuscadorMiembroDBHandler buscadorMiembroDBHandler;

        private List<MiembroModel> listaMiembros;
        private string rutaCarpeta { get; set; }

        public EnviarEmailController(IHostingEnvironment env) {
            buscadorMiembroDBHandler = new BuscadorMiembroDBHandler();
            listaMiembros = buscadorMiembroDBHandler.GetListaMiembros();
            rutaCarpeta = env.WebRootPath;
        }

        public void EnviarMail(string destino, string remitente, string asunto, string mensaje, string filePath) {
            string usernameDestino = "";
            MiembroModel miembroDestino;
            MiembroModel miembroRemitente;
            string nombreCompletoDestino = "";
            string nombreCompletoRemitente = "";
            string emailAddress = "";

            miembroRemitente = listaMiembros.Find(x => x.usernamePK == remitente);
            nombreCompletoRemitente = miembroRemitente.nombre + " " + miembroRemitente.apellido1 + " " + miembroRemitente.apellido2;

            if ( !destino.Contains("@") )
            {
                usernameDestino = destino.Split('(', ')')[1];

                miembroDestino = listaMiembros.Find(x => x.usernamePK == usernameDestino);

                nombreCompletoDestino = miembroDestino.nombre + " " + miembroDestino.apellido1 + " " + miembroDestino.apellido2;
                emailAddress = miembroDestino.email;
            } else
            {
                emailAddress = destino;
                nombreCompletoDestino = destino;
            }

            asunto = "[LaCafeteria] " + asunto;
            string mensajeHtml = "<h3>Estimado " + nombreCompletoDestino + ",</h3>" +
                "<p><i>Has recibido un mensaje de la comunidad La Cafeteria del miembro " + nombreCompletoRemitente + " (" + remitente + ") : </i><br/><br/>" + mensaje +
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

            var message = new MailMessage(fromAddress, toAddress);

            message.Subject = asunto;
            message.IsBodyHtml = true;

            if ( filePath != "" )
            {
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(filePath);
                message.Attachments.Add(attachment);
            }

            message.AlternateViews.Add(htmlView);
            smtp.Send(message);



            /*if ( filePath != "" )
            {
                message.Attachments.Clear();
                message.Attachments.Dispose();
                message.Dispose();
                message = null;
                smtp.Dispose();
                System.IO.File.Delete(filePath);
            }*/
        }
    }
}
