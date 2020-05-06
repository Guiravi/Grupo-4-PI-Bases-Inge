using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheCoffeePlace.Views;
using System.Web.UI.WebControls;
using TheCoffeePlace.Models;

/// <summary>
/// Este es el controlador de UserFaqs
/// </summary>
namespace TheCoffeePlace.Controllers
{
    public class UserFaqController
    {
        //La vista que interactúa con el controlador
        private IViewUserFaqs userView;
        //Vector para guardar información de FAQs.txt
        private string[] linesFaqs;
        //Vector para guardar información de NumCat.txt
        private string[] linesNumCat;

        public UserFaqController(IViewUserFaqs view)
        {
            userView = view;

            // Este método se encarga de manipular la información necesaria para realizar cambios en los archivos de FAQ

        }

        public void SeeFaqs()
        {

            //Se crea el administrador de archivos para guardar la información de estos en vectores
            //Se crean los administradores de archivos para guardar la información de estos en vectores
            FAQsFileModel faqsFileModel = new FAQsFileModel();
            FAQsFileHandler faqsFileHandler = new FAQsFileHandler();
            linesFaqs = faqsFileHandler.FileInformation(faqsFileModel);
            NumCatFileModel numCatFileModel = new NumCatFileModel();
            NumCatFileHandler numCatFileHandler = new NumCatFileHandler();
            linesNumCat = numCatFileHandler.FileInformation(numCatFileModel);
            //Se va a actualizar el label para poner el título de faqs
            UpdateLabel(0, "<h1>FAQs</h1>");
            // Se llama al método para mostrar los FAQs en la página web
            ShowFaqsPage();
        }

        // Este método  muestra los FAQs en la página web
        public void ShowFaqsPage()
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

        //Este método se encarga de actualizar el label de la vista del miembro
        public void UpdateLabel(int option, string text)
        {
            if (option < 1)
            {
                //Se reinicia el contenido del label
                userView.Label.Text = text;
            }
            else
            {
                //se agrega contenido al label
                userView.Label.Text += text;
            }
        }
    }
}