using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
/// <summary>
/// Esta clase se encarga de manipular los archivos de FAQs y el modelo FAQsFileModel
/// </summary>
namespace LaCafeteria.Models.Handlers
{

    public class FAQsHandler
    {
        //Vector para guardar información de FAQs.txt
        private string[] linesFaqs;
        //Vector para guardar nueva información de FAQs.txt
        private string[] newLinesFaqs;
        //Vector para guardar información de NumCat.txt
        private string[] linesNumCat;
        private string[] newLinesNumCat;
        //Indica el número de línea donde estaba la categoría de la pregunta si es que la categoría existía
        private int lineCategoryFound;
        //Indica el número de líneas que tiene el artículo
        private int allLines;
        // Este es el constructor de la clase que se encarga de crear el manipulador de los archivos

        public FAQsHandler()
        {

        }

        public List<string> GetQuestions(FAQsModel fAQsModel, FAQsHandler faqsHandler, string categoria)
        {
            linesFaqs = faqsHandler.FileInformation(1, fAQsModel);
            linesNumCat = faqsHandler.FileInformation(2, fAQsModel);
            List<string> questions = new List<string>();
            int i = 0;
            int iNumCat = 0;

            
            if (linesFaqs.Count() > 0) {


                while (i< linesFaqs.Count() && iNumCat < linesNumCat.Count())
                {

                    if (i >= Int32.Parse(linesNumCat[iNumCat]) && iNumCat < linesNumCat.Count()) {
                        iNumCat++;
                        
                    }
                    if (linesFaqs[i] == categoria && iNumCat < linesNumCat.Count())
                    {
                        
                        i++;
                        while (i < Int32.Parse(linesNumCat[iNumCat]))
                        {
                            questions.Add(linesFaqs[i]);
                            i = i + 2;
                        }
                        i = linesFaqs.Count();
                    }
                    else {
                        i++;
                    }

                }
            }
           
            
            return questions;
        }
        public List<string> GetAnswers(FAQsModel fAQsModel, FAQsHandler faqsHandler, string categoria)
        {
            linesFaqs = faqsHandler.FileInformation(1, fAQsModel);
            linesNumCat = faqsHandler.FileInformation(2, fAQsModel);
            List<string> answers = new List<string>();
            int i = 0;
            int iNumCat = 0;

            if (linesFaqs.Count() > 0)
            {

                while (i < linesFaqs.Count() && iNumCat < linesNumCat.Count() && iNumCat < linesNumCat.Count())
                {


                    if (i >= Int32.Parse(linesNumCat[iNumCat]))
                    {
                        iNumCat++;

                    }
                    if (linesFaqs[i] == categoria && iNumCat < linesNumCat.Count())
                    {

                        i = i + 2;
                        while (i < Int32.Parse(linesNumCat[iNumCat]))
                        {
                            answers.Add(linesFaqs[i]);
                            i = i + 2;
                        }

                    }
                    else
                    {
                        i++;
                    }

                }
            }
            return answers;
        }
        public int RevisarPreguntaRepetida(string categoria,string pregunta, string respuesta)
        {
            int error = 0;
            int i = 0;
            int iNumCat = 0;
            
            if (linesFaqs.Count() > 0)
            {

                while (i < linesFaqs.Count() && iNumCat < linesNumCat.Count())
                {


                    if (i >= Int32.Parse(linesNumCat[iNumCat]) && iNumCat < linesNumCat.Count())
                    {
                        iNumCat++;
                        
                    }
                    if (linesFaqs[i] == categoria && iNumCat < linesNumCat.Count())
                    {
                        
                        i = i + 1;
                        while (i < Int32.Parse(linesNumCat[iNumCat] ))
                        {
                            if (linesFaqs[i] == pregunta) {
                                error = 2;
                                i = linesFaqs.Count();

                            }
                            i = i + 2;
                        }

                    }
                    else
                    {
                        i++;
                    }

                }
            }
            return error;
        }

        public List<string> GetCategories(FAQsModel fAQsModel, FAQsHandler faqsHandler)
        {
           
            linesFaqs = faqsHandler.FileInformation(1, fAQsModel);
            linesNumCat = faqsHandler.FileInformation(2, fAQsModel);
            List<string> categories = new List<string>();
            int i = 0;
            int iCat = 0;
            foreach (string line in linesFaqs)
            {
                if (i == 0)
                {
                    categories.Add(line);
                    
                }
                else
                {
                    if (i == Int32.Parse(linesNumCat[iCat]))
                    {
                        categories.Add(line);
                        iCat++;
                    }
                    else
                    {
                    }
                }
                i++;
            }
            return categories;
        }

        void BorrarQYA(FAQsModel faqsModel, FAQsHandler faqsHandler, string categoria, string pregunta, string respuesta) {
            
            int i = 0;
            int iNumCat = 0;

            if (linesFaqs.Count() > 0)
            {
                newLinesNumCat[0] = linesNumCat[0]; 
                while (i < linesFaqs.Count() && iNumCat < linesNumCat.Count())
                {


                    if (i >= Int32.Parse(linesNumCat[iNumCat]) && iNumCat < linesNumCat.Count())
                    {
                        iNumCat++;
                        newLinesNumCat[iNumCat] = linesNumCat[iNumCat];

                    }
                    if (linesFaqs[i] == categoria && iNumCat < linesNumCat.Count())
                    {
                        newLinesFaqs[i] = linesFaqs[i];
                        i = i + 1;
                        while (i < Int32.Parse(linesNumCat[iNumCat]))
                        {
                            if (linesFaqs[i] == pregunta)
                            {
                                if (linesFaqs.Count() == 3)
                                {
                                    ReajustarCasoBase(i-1,iNumCat,1);
                                    i = linesFaqs.Count();
                                }

                                else
                                {
                                    if (i + 2 >= linesFaqs.Count()) {

                                        if (linesFaqs[i - 1] == categoria)
                                        {
                                            ReajustarCasoBase(i-1, iNumCat, 1);

                                        }
                                        else {
                                            ReajustarCasoBase(i, iNumCat, 2);
                                          
                                        }
                                        
                                        i = linesFaqs.Count();
                                    }
                                    else
                                    {
                                        int posicionesMenos = DeterminarPosicionesMenos(i, categoria, iNumCat);
                                        int iNicial = i;
                                        i = DeterminarPosActual(posicionesMenos, i);
                                        ReajustarLinesNumCat(iNumCat, posicionesMenos);
                                        ReajustarLinesFaqs(i, posicionesMenos);
                                        MoverINumCat(iNumCat, posicionesMenos,iNicial,categoria);
                                        i = linesFaqs.Count();
                                    }
                                    
                                }

                            }
                            if (i < linesFaqs.Count())
                            {
                                newLinesFaqs[i] = linesFaqs[i];
                                newLinesFaqs[i + 1] = linesFaqs[i + 1];
                            }
                            
                            i = i + 2;
                        }

                    }
                    else
                    {
                        newLinesFaqs[i] = linesFaqs[i];
                       
                        i++;
                    }

                }
            }
            
        }
        public void ReajustarCasoBase(int start,int iNumCat,int option) {
            if (option == 1)
            {
                newLinesNumCat[iNumCat] = "";
            }
            else {
                int numCat = Int32.Parse(linesNumCat[iNumCat]);
                int nuevoResultado = numCat - 2;
                newLinesNumCat[iNumCat] = nuevoResultado.ToString();
            }
            for (int i = start; i < linesFaqs.Count();i++)
            {
                newLinesFaqs[i] = "";
            }
        }
        public int DeterminarPosActual(int posicionesMenos, int i) {
            if (posicionesMenos == 3)
            {
                i = i - 1;
            }
            return i;
        }
        public void ReajustarLinesFaqs(int i, int posicionesMenos)
        {
            int viejoI = i + posicionesMenos;
            while (viejoI< linesFaqs.Count())
            {
                
                newLinesFaqs[i] = linesFaqs[viejoI];
                i++;
                viejoI++;
            }
        }
        public void MoverINumCat(int iNumCat, int posicionesMenos,int i,string categoria) {
            int oldINumCat = iNumCat + 1;
            if (linesFaqs[i - 1] == categoria) {
                if (iNumCat + 1 < linesNumCat.Count())
                {
                    int num =Int32.Parse(linesNumCat[iNumCat]);
                    if (i + 2 == Int32.Parse(linesNumCat[iNumCat + 1]))
                    {
                        while (iNumCat < linesNumCat.Count() && oldINumCat < linesNumCat.Count())
                        {

                            newLinesNumCat[iNumCat] = linesNumCat[oldINumCat];
                            iNumCat++;
                            oldINumCat++;
                        }
                    }
                    else
                    {
                        while (iNumCat < linesNumCat.Count())
                        {

                            newLinesNumCat[iNumCat] = linesNumCat[iNumCat];
                            iNumCat++;
                            oldINumCat++;
                        }
                    }
                }

                else {
                    while (iNumCat < linesNumCat.Count())
                    {

                        newLinesNumCat[iNumCat] = linesNumCat[iNumCat];
                        iNumCat++;
                        oldINumCat++;
                    }
                }
                
            }
            else
            {
                while (iNumCat < linesNumCat.Count() )
                {

                    newLinesNumCat[iNumCat] = linesNumCat[iNumCat];
                    iNumCat++;
                    oldINumCat++;
                }
            }   
        }
        public void ReajustarLinesNumCat(int iNumCat, int posicionesMenos)
        {
            while (iNumCat < linesNumCat.Count())
            {
                int numCat = Int32.Parse(linesNumCat[iNumCat]);
                int nuevoResultado = numCat - posicionesMenos;
                linesNumCat[iNumCat] = nuevoResultado.ToString();
                iNumCat++;
            }
        }
        public int DeterminarPosicionesMenos(int i, string categoria,int iNumCat) {
            int determinacion = 2;
            if (iNumCat +1 < linesNumCat.Count()) {
                int pos = Int32.Parse(linesNumCat[iNumCat ]);
                if (linesFaqs[i - 1] == categoria && (i + 2 == pos)) {
                    determinacion = 3;
                }
            }
                return determinacion;
        }
        public int Borrar(FAQsModel faqsModel, FAQsHandler faqsHandler, string categoria, string pregunta, string respuesta)
        {
            int error = 0;
            linesFaqs = faqsHandler.FileInformation(1, faqsModel);
            linesNumCat = faqsHandler.FileInformation(2, faqsModel);
            //El miembro debe de haber llenado todos los campos para modificar FAQs
            if ((categoria != "" && pregunta != "" && respuesta != "") &&(categoria != null && pregunta != null && respuesta != null))
            {
                if (RevisarPreguntaRepetida(categoria, pregunta, respuesta) == 0) {
                    error = 3;
                }
               
                if (error != 3)
                {

                    //Se busca si la categoria existía previamente(1)
                    //int categoryFound = SearchCategory(linesFaqs,categoria);
                    allLines = linesFaqs.Count();
                    //Se le da a newLinesFaqs un tamaño adecuado para poder aguantar la introducción de las nuevas líneas
                    newLinesFaqs = new String[allLines];

                    newLinesNumCat = new String[linesNumCat.Count()];
                    newLinesNumCat[0] = "";
        // contador de la catidad de nuevas líneas que va a tener el archivo Faqs.txt actualizado
                    BorrarQYA(faqsModel, faqsHandler, categoria, pregunta, respuesta);
                    //Se actualiza NumCat.txt
                    faqsHandler.WriteAllLines(2, newLinesNumCat, faqsModel);
                    //Se actualiza FAQs.txt
                    faqsHandler.WriteAllLines(1, newLinesFaqs, faqsModel);
                    
                }
            }
            else
            {
                error = 1;
            }
            return error;


        }
        public int Agregar(FAQsModel faqsModel, FAQsHandler faqsHandler, string categoria, string pregunta, string respuesta) {
            int error = 0;
            linesFaqs = faqsHandler.FileInformation(1, faqsModel);
            linesNumCat = faqsHandler.FileInformation(2, faqsModel);
            //El miembro debe de haber llenado todos los campos para modificar FAQs
            if ((categoria != "" && pregunta != "" && respuesta != "") && (categoria != null && pregunta != null && respuesta != null))
            {
                error = RevisarPreguntaRepetida(categoria, pregunta, respuesta);
                if (error != 2)
                {

                    //Se busca si la categoria existía previamente(1)
                    int categoryFound = SearchCategory(linesFaqs,categoria);
                    allLines = linesFaqs.Count();
                    //Se le da a newLinesFaqs un tamaño adecuado para poder aguantar la introducción de las nuevas líneas
                    newLinesFaqs = new String[allLines + 3];
                    // contador de la catidad de nuevas líneas que va a tener el archivo Faqs.txt actualizado
                    int totNewLines = 0;
                    totNewLines = UpdateNewLines(categoryFound, totNewLines,pregunta,respuesta);
                    //Se actualiza NumCat.txt

                    if (linesNumCat.Count() != 0)
                    {
                        faqsHandler.WriteAllLines(2, linesNumCat, faqsModel);
                    }
                        //Se revisa si hay que agregar una nueva categoría junto con la pregunta y respuesta
                    AddNewCategory(totNewLines, categoryFound, faqsModel, faqsHandler,categoria,pregunta,respuesta);
                    if (linesNumCat.Count() != 0)
                    {
                        //Se actualiza FAQs.txt

                        faqsHandler.WriteAllLines(1, newLinesFaqs, faqsModel);
                    }
                }    
            }
            else
            {
                error = 1;
            }
            return error;
        }

        // Este método revisa si hay que agregar una nueva categoría, y si es así se agrega con las nuevas preguntas y respuestas
        public void AddNewCategory(int totNewLines, int categoryFound, FAQsModel faqsModel, FAQsHandler faqsHandler, string categoria, string pregunta, string respuesta )
        {
            if (categoryFound == 0)
                
            {
               
                newLinesFaqs[totNewLines] = categoria;
                totNewLines = totNewLines + 1;
 
                newLinesFaqs[totNewLines] = pregunta;
                totNewLines = totNewLines + 1;
                newLinesFaqs[totNewLines] = respuesta;
                totNewLines = totNewLines + 1;
                //Se agrega un nuevo número de línea a NumCat.txt ya que hay una nueva categoria
                if (linesFaqs.Count() == 0)
                {
                    File.WriteAllLines(faqsModel.filePathFaqs, newLinesFaqs);
                    string[] newNumCat = new string[1];
                    newNumCat[0] = "3";
                    File.WriteAllLines(faqsModel.filePathNumCat, newNumCat);

                }
                else
                {
                    faqsHandler.WriteLastLine(2, Convert.ToString(totNewLines), faqsModel);
                }
                
            }
        }

        // Este método actualiza la información de las nuevas líneas que se van a introducir
        public int UpdateNewLines(int categoryFound, int totNewLines,string pregunta,string respuesta)
        {
            //la posición actual en el vector de números de las posiciones de las categorías
            int posNum = 0;
            //contador la cantidad de lineas totales del archivo Faqs.txt
            int totLines = 0;
            foreach (string line in linesNumCat)
            {   //indica que se va a poner una categoría en la vista del miembro
                int firstInLine = 1;
                //Para asegurarse que almenos haya un número en NumCat.txt
                if (linesNumCat[posNum] != "")
                {   //Se revisa cual es la última línea de la categoría actual y se reproducen la categoría, preguntas y respuestas
                    int finish = Int32.Parse(linesNumCat[posNum]);
                    while (totLines < finish)
                    {
                        if (firstInLine == 1)
                        {
                            firstInLine = 0;
                            //Se actauliza la nueva categoría, la nueva línea se actualiza en el vector de nuevas líneas
                            newLinesFaqs[totNewLines] = linesFaqs[totLines];
                            //Como ya se actualizó la línea, se incrementan las posiciones de las líneas
                            totLines = totLines + 1;
                            totNewLines = totNewLines + 1;
                            if (categoryFound == 1 && lineCategoryFound == totLines - 1)
                            {   //Si la categoría existe y se llegó a la línea donde se encuentra,
                                //se actaulizan las preguntas y respuestas, la nuevas líneas se actualizan
                                //en el vector de nuevas líneas

                                newLinesFaqs[totNewLines] = pregunta;
                                totNewLines = totNewLines + 1;

                                newLinesFaqs[totNewLines] = respuesta;
                                totNewLines = totNewLines + 1;
                            }
                        }
                        else
                        { 
                            newLinesFaqs[totNewLines] = linesFaqs[totLines];
                            totLines = totLines + 1;
                            totNewLines = totNewLines + 1;
                            newLinesFaqs[totNewLines] = linesFaqs[totLines];
                            totLines = totLines + 1;
                            totNewLines = totNewLines + 1;
                        }
                    }
                }//Se actualiza el número de línas por categoría en el vector respectivo y se guarda como string
                linesNumCat[posNum] = Convert.ToString(totNewLines);
                posNum = posNum + 1;
            }
            return totNewLines;
        }

        //Este método se encarga de buscar el número de línea donde estaba la categoría de la pregunta si es que la categoría existía
        public int SearchCategory(string[] linesFaqs, string categoria)
        {
            //No se encuentra
            int categoryFound = 0;
            foreach (string line in linesFaqs)
            {
                if (linesFaqs[allLines] == categoria)
                {
                    //Se encuentra
                    categoryFound = 1;
                    lineCategoryFound = allLines;
                }
                allLines = allLines + 1;
            }
            return categoryFound;
        }

        

        // Este método devuelve un vector de string para manipular los datos de un archivo 
        public string[] FileInformation(int option, FAQsModel faqsModel)
        {
            if (option == 1)
            {
                return faqsModel.linesFaqs;
            }
            else
            {
                return faqsModel.linesNumCat;
            }

        }
        // Este método rescribe todo el archivo según un vector de strings
        public void WriteAllLines(int option, string[] newLines, FAQsModel faqsModel)
        {
           

            string path;
            if (option == 1)
            {
                path = faqsModel.filePathFaqs;

            }
            else
            {
                path = faqsModel.filePathNumCat;

            }


            if (newLines[0] == "" || newLines[0] == null)
            
                File.Create(path).Close();
            
            else
            {
                int i = 0;
                using (StreamWriter writer = new StreamWriter(path))
                {
                    while (i < newLines.Count())
                    {

                        if (newLines[i] != "" && newLines[i] != null)
                        {
                            writer.WriteLine(newLines[i]);

                        }

                        i++;
                    }
                }
            }
        }

        // Este método escribe una nueva línea en el archivo 
        public void WriteLastLine(int option, string newLine, FAQsModel faqsModel)
        {
            if (option == 1)
            {
                File.AppendAllText(faqsModel.filePathFaqs, newLine);
            }
            else
            {
                File.AppendAllText(faqsModel.filePathNumCat, newLine);
            }
        }
    }
}