using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheCoffeePlace.Views;
using System.Web.UI.WebControls;
using TheCoffeePlace.Models;

/// <summary>
/// Este es el controlador de los FAQs
/// 
/// </summary>
namespace TheCoffeePlace.Controllers
{
    public class FAQsController
    {
        private string category;
        private string answer;
        private string question;
        private Label label;
        //Vector para guardar información de FAQs.txt
        private string[] linesFaqs;
        //Vector para guardar nueva información de FAQs.txt
        private string[] newLinesFaqs;
        //Vector para guardar información de NumCat.txt
        private string[] linesNumCat;
        //Indica el número de línea donde estaba la categoría de la pregunta si es que la categoría existía
        private int lineCategoryFound;
        //Indica el número de líneas que tiene el artículo
        private int allLines;
        public FAQsController(string category, string answer, string question, Label label)
        {
            this.label = label;
            this.category = category;
            this.answer = answer;
            this.question = question;
            lineCategoryFound = -1;
            allLines = 0;
        }

        public FAQsController(Label label) {
            this.label = label;
        }

        public void SeeFAQs()
        {

            //Se crea el administrador de archivos para guardar la información de estos en vectores
            //Se crean los administradores de archivos para guardar la información de estos en vectores
            FAQsModel faqsModel = new FAQsModel();
            FAQsHandler faqsHandler = new FAQsHandler();
            linesFaqs = faqsHandler.FileInformation(1,faqsModel);
            linesNumCat = faqsHandler.FileInformation(2,faqsModel);
            //Se va a actualizar el label para poner el título de faqs
            UpdateLabel(0, "<h1>FAQs</h1>");
            // Se llama al método para mostrar los FAQs en la página web
            ShowFAQsPage();
        }

        // Este método  muestra los FAQs en la página web
        public void ShowFAQsPage()
        {
            //la posición actual en el vector de números de las posiciones de las categorías
            int posNum = 0;
            //contador la cantidad de lineas totales del archivo Faqs.txt
            int totLines = 0;
            foreach (string line in linesNumCat)
            {   //indica que se va a poner una categoría en la vista 
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
                            UpdateLabel(1, "<h2>CATEGORY</h2>");
                            UpdateLabel(1, "<h3>" + linesFaqs[totLines] + "</h3>");
                            //Como ya se actualizó la línea, se incrementan las posiciones de las líneas
                            totLines = totLines + 1;
                        }
                        else
                        {
                            UpdateLabel(1, "<h3>QUESTION</h3>");
                            UpdateLabel(1, "<p>" + linesFaqs[totLines] + "</p>");
                            totLines = totLines + 1;
                            UpdateLabel(1, "<h3>ANSWER</h3>");
                            UpdateLabel(1, "<p>" + linesFaqs[totLines] + "</p>");
                            totLines = totLines + 1;
                        }
                    }
                }//Se actualiza el número de línas por categoría en el vector respectivo y se guarda como string
                posNum = posNum + 1;
            }
        }

        // Este método se encarga de manipular la información necesaria para realizar cambios en los archivos de FAQs
        public void UploadFAQs()
        {
            //Se crean los administradores de archivos para guardar la información de estos en vectores
            FAQsModel faqsModel = new FAQsModel();
            FAQsHandler faqsHandler = new FAQsHandler();
            linesFaqs = faqsHandler.FileInformation(1, faqsModel);
            linesNumCat = faqsHandler.FileInformation(2, faqsModel);
            //El miembro debe de haber llenado todos los campos para modificar FAQs
            if (category != "" && question != "" && answer != "")
            {
                //Se busca si la categoria existía previamente(1)
                int categoryFound = SearchCategory(linesFaqs);
                //Se va a actualizar el label para poner el título de faqs
                UpdateLabel(0, "<h1>FAQs</h1>");
                //Se le da a newLinesFaqs un tamaño adecuado para poder aguantar la introducción de las nuevas líneas
                newLinesFaqs = new String[allLines + 3];
                // contador de la catidad de nuevas líneas que va a tener el archivo Faqs.txt actualizado
                int totNewLines = 0;
                totNewLines = UpdateNewLines(categoryFound, totNewLines);
                //Se actualiza NumCat.txt
                faqsHandler.WriteAllLines(2,linesNumCat, faqsModel);
                //Se revisa si hay que agregar una nueva categoría junto con la pregunta y respuesta
                AddNewCategory(totNewLines, categoryFound,faqsModel, faqsHandler);
                //Se actualiza FAQs.txt
                faqsHandler.WriteAllLines(1,newLinesFaqs, faqsModel);
            }
            else
            {
                UpdateLabel(0, "<p>Please fill all the options</p>");
            }
        }

        // Este método revisa si hay que agregar una nueva categoría, y si es así se agrega con las nuevas preguntas y respuestas
        public void AddNewCategory(int totNewLines, int categoryFound, FAQsModel faqsModel, FAQsHandler faqsHandler)
        {
            if (categoryFound == 0)
            {
                UpdateLabel(1, "<h2>CATEGORY</h2>");
                UpdateLabel(1, "<h3>" + category + "</h3>");
                newLinesFaqs[totNewLines] = category;
                totNewLines = totNewLines + 1;
                UpdateLabel(1, "<h3>QUESTION</h3>");
                UpdateLabel(1, "<p>" + question + "</p>");
                newLinesFaqs[totNewLines] = question;
                totNewLines = totNewLines + 1;
                UpdateLabel(1, "<h3>ANSWER</h3>");
                UpdateLabel(1, "<p>" + answer + "</p>");
                newLinesFaqs[totNewLines] = answer;
                totNewLines = totNewLines + 1;
                //Se agrega un nuevo número de línea a NumCat.txt ya que hay una nueva categoria
                faqsHandler.WriteLastLine(2,Convert.ToString(totNewLines), faqsModel);
            }
        }

        // Este método actualiza la información de las nuevas líneas que se van a introducir
        public int UpdateNewLines(int categoryFound, int totNewLines)
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
                            UpdateLabel(1, "<h2>CATEGORY</h2>");
                            UpdateLabel(1, "<h3>" + linesFaqs[totLines] + "</h3>");
                            newLinesFaqs[totNewLines] = linesFaqs[totLines];
                            //Como ya se actualizó la línea, se incrementan las posiciones de las líneas
                            totLines = totLines + 1;
                            totNewLines = totNewLines + 1;
                            if (categoryFound == 1 && lineCategoryFound == totLines - 1)
                            {   //Si la categoría existe y se llegó a la línea donde se encuentra,
                                //se actaulizan las preguntas y respuestas, la nuevas líneas se actualizan
                                //en el vector de nuevas líneas
                                UpdateLabel(1, "<h3>QUESTION</h3>");
                                UpdateLabel(1, "<p>" + question + "</p>");
                                newLinesFaqs[totNewLines] = question;
                                totNewLines = totNewLines + 1;
                                UpdateLabel(1, "<h3>ANSWER</h3>");
                                UpdateLabel(1, "<p>" + answer + "</p>");
                                newLinesFaqs[totNewLines] = answer;
                                totNewLines = totNewLines + 1;
                            }
                        }
                        else
                        {
                            UpdateLabel(1, "<h3>QUESTION</h3>");
                            UpdateLabel(1, "<p>" + linesFaqs[totLines] + "</p>");
                            newLinesFaqs[totNewLines] = linesFaqs[totLines];
                            totLines = totLines + 1;
                            totNewLines = totNewLines + 1;
                            UpdateLabel(1, "<h3>ANSWER</h3>");
                            UpdateLabel(1, "<p>" + linesFaqs[totLines] + "</p>");
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
        public int SearchCategory(string[] linesFaqs)
        {
            //No se encuentra
            int categoryFound = 0;
            foreach (string line in linesFaqs)
            {
                if (linesFaqs[allLines] == category)
                {
                    //Se encuentra
                    categoryFound = 1;
                    lineCategoryFound = allLines;
                }
                allLines = allLines + 1;
            }
            return categoryFound;
        }

        // Este método se encarga de actualizar el label de la vista del miembro
        public void UpdateLabel(int option, string text)
        {
            if (option < 1)
            {
                //Se reinicia el contenido del label
                label.Text = text;
            }
            else
            {
                //se agrega contenido al label
                label.Text += text;
            }
        }
    }
}