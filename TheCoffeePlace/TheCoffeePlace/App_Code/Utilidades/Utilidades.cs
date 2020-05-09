using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Utilidades
/// </summary>

namespace TheCoffeePlace.Utilities
{
    static public class Utilidades
    { 
        static public bool EsTipoArchivo(string nombreArchivo, string tipoArchivo)
        {

            string extension = "";
            int contExt = nombreArchivo.Length - 1;
            for (int i = 0; i < tipoArchivo.Length; i++)
            {
                extension = nombreArchivo[contExt] + extension;
                contExt -= 1;
            }

            if (extension == tipoArchivo)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

		static public void VerPDF(Page pagina, string pathVirtualArchivoPDF)
		{
			WebClient User = new WebClient();
			string filePath = pagina.Server.MapPath(pathVirtualArchivoPDF);
			byte[] fileBuffer = User.DownloadData(filePath);
			if (fileBuffer != null)
			{
				pagina.Response.ContentType = "application/pdf";
				pagina.Response.AddHeader("content-length", fileBuffer.Length.ToString());
				pagina.Response.BinaryWrite(fileBuffer);
			}
		}

		static public void SetErrorMsg(Page page, string errorMsg, string redirectVirtualPath)
		{
			page.Session["ErrorMsg"] = errorMsg;
			page.Response.Redirect(redirectVirtualPath);
		}

    }
}

