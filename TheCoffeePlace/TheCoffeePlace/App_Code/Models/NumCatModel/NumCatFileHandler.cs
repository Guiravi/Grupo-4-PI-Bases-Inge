using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.IO;
/// <summary>
/// Esta clase se encarga de manipular el archivo NumCat.txt y el modelo NumCatModel
/// </summary>
namespace TheCoffeePlace.Models
{

    public class NumCatFileHandler
    {
        // Este es el constructor de la clase que se encarga de crear el manipulador de los archivos

        public NumCatFileHandler()
        {

        }
        // Este método devuelve un vector de string para manipular los datos de un archivo NumCat.txt
        public string[] FileInformation(NumCatFileModel numCatFileModel)
        {
            return numCatFileModel.linesNumCat;
        }
        // Este método rescribe todo el archivo según un vector de strings
        public void WriteAllLines(string[] newLines, NumCatFileModel numCatFileModel)
        {

            File.WriteAllLines(numCatFileModel.filePathNumCat, newLines);
        }
        // Este método escribe una nueva línea en el archivo 
        public void WriteLastLine(string newLine, NumCatFileModel numCatFileModel)
        {
            File.AppendAllText(numCatFileModel.filePathNumCat, newLine);

        }
    }
}