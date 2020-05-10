using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Reflection;

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

        static public bool EsNombreDeArchivoCorto(string nombreArchivo, int tamano)
        {
            if (nombreArchivo.Length < tamano)
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
				string[] splitString = pathVirtualArchivoPDF.Split('/');
				string fileName = splitString[splitString.Length - 1];
				pagina.Response.ContentType = "application/pdf";
				pagina.Response.AddHeader("content-length", fileBuffer.Length.ToString());
				pagina.Response.AppendHeader("Content-Disposition", "filename=" + fileName);
				pagina.Response.BinaryWrite(fileBuffer);
			}
		}

		static public void SetErrorMsg(Page page, string errorMsg, string redirectVirtualPath)
		{
			page.Session["ErrorMsg"] = errorMsg;
			page.Response.Redirect(redirectVirtualPath);
		}


		public class LibreOfficeFailedException : Exception
		{
			public LibreOfficeFailedException(int exitCode)
				: base(string.Format("LibreOffice has failed with {0}", exitCode))
			{ }
		}

		static public void DocxToPDF(string docxVirtualPath)
		{
			string libreOfficePath = HttpContext.Current.Server.MapPath("~/libreoffice/program/soffice.exe");
			string docxFullPath = HttpContext.Current.Server.MapPath(docxVirtualPath);
			string pdfsFullPath = HttpContext.Current.Server.MapPath("~/ArticulosPDF");
			string stringArgs = "--convert-to pdf " + docxFullPath + " --outdir " + pdfsFullPath;
			ProcessStartInfo procStartInfo = new ProcessStartInfo(libreOfficePath, stringArgs);
			procStartInfo.RedirectStandardOutput = true;
			procStartInfo.UseShellExecute = false;
			procStartInfo.CreateNoWindow = true;
			procStartInfo.WorkingDirectory = Environment.CurrentDirectory;
			Process process = new Process() { StartInfo = procStartInfo, };
			process.Start();
			process.WaitForExit();

			if (process.ExitCode != 0)
			{
				throw new LibreOfficeFailedException(process.ExitCode);
			}
		}
    }
}

