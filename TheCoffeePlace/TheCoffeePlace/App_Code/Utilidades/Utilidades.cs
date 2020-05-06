﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;

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

    }
}
