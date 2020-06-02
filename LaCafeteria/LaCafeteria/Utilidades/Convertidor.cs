using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using System.Web;
using System.Net;
using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;

namespace LaCafeteria.Utilidades
{
    public static class Convertidor
    {

        static public void DocxConvertToPdf(string docxFilename, string rutaCarpeta)
        {
            string libreOfficePath = rutaCarpeta + "/LibreOfficePortable/App/libreoffice/program/soffice.exe";
            string docxFullPath = ( rutaCarpeta + "/ArticulosDOCX/" + docxFilename + ".docx");
            string pdfsFullPath = ( rutaCarpeta + "/ArticulosPDF");
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

        public class LibreOfficeFailedException : Exception
        {
            public LibreOfficeFailedException(int exitCode)
                : base(string.Format("LibreOffice has failed with {0}", exitCode))
            { }
        }


        public static string CambiarFormatoFechaAMD(string fechaVieja)
        {
            string[] fechaElementos = fechaVieja.Split("/");
            string fechaFormato = "";

            fechaFormato += fechaElementos[2] + "-";
            if (fechaElementos[0].Length == 1)
            {
                fechaFormato += "0" + fechaElementos[1] + "-";
            }
            else
            {
                fechaFormato += fechaElementos[0] + "-";
            }

            if (fechaElementos[1].Length == 1)
            {
                fechaFormato += "0" + fechaElementos[1];
            }
            else
            {
                fechaFormato += fechaElementos[1];
            }

            return fechaFormato;
        }

        public static string CambiarFormatoFechaDMA(string fechaVieja)
        {
            string[] fechaElementos = fechaVieja.Split("/");
            string fechaFormato = "";

            if (fechaElementos[1].Length == 1)
            {
                fechaFormato += "0" + fechaElementos[1] + "/";
            }
            else
            {
                fechaFormato += fechaElementos[1] + "/";
            }
            
            if (fechaElementos[0].Length == 1)
            {
                fechaFormato += "0" + fechaElementos[0] + "/";
            }
            else
            {
                fechaFormato += fechaElementos[0] + "/";
            }

            fechaFormato += fechaElementos[2];

            return fechaFormato;
        }
    }
}
