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
        //La vista que interact�a con el controlador
        private IViewUserFaqs userView;
        //Vector para guardar informaci�n de FAQs.txt
        private string[] linesFaqs;
        //Vector para guardar informaci�n de NumCat.txt
        private string[] linesNumCat;

        public UserFaqController(IViewUserFaqs view)
        {
            userView = view;

            // Este m�todo se encarga de manipular la informaci�n necesaria para realizar cambios en los archivos de FAQ

        }

        public void SeeFaqs()
        {

            //Se crea el administrador de archivos para guardar la informaci�n de estos en vectores
            //Se crean los administradores de archivos para guardar la informaci�n de estos en vectores
            FAQsFileModel faqsFileModel = new FAQsFileModel();
            FAQsFileHandler faqsFileHandler = new FAQsFileHandler();
            linesFaqs = faqsFileHandler.FileInformation(faqsFileModel);
            NumCatFileModel numCatFileModel = new NumCatFileModel();
            NumCatFileHandler numCatFileHandler = new NumCatFileHandler();
            linesNumCat = numCatFileHandler.FileInformation(numCatFileModel);
            //Se va a actualizar el label para poner el t�tulo de faqs
            UpdateLabel(0, "<h1>FAQs</h1>");
            // Se llama al m�todo para mostrar los FAQs en la p�gina web
            ShowFaqsPage();
        }

        // Este m�todo  muestra los FAQs en la p�gina web
        public void ShowFaqsPage()
        {
            //la posici�n actual en el vector de n�meros de las posiciones de las categor�as
            int posNum = 0;
            //contador la cantidad de lineas totales del archivo Faqs.txt
            int totLines = 0;
            foreach (string line in linesNumCat)
            {   //indica que se va a poner una categor�a en la vista del miembro
                int firstInLine = 1;
                //Para asegurarse que almenos haya un n�mero en NumCat.txt
                if (linesNumCat[posNum] != "")
                {   //Se revisa cual es la �ltima l�nea de la categor�a actual y se reproducen la categor�a, preguntas y respuestas
                    int finish = Int32.Parse(linesNumCat[posNum]);
                    while (totLines < finish)
                    {
                        if (firstInLine == 1)
                        {
                            firstInLine = 0;
                            //Se actauliza la nueva categor�a, la nueva l�nea se actualiza en el vector de nuevas l�neas
                            UpdateLabel(1, "<h2>CATEGORY</h2>");
                            UpdateLabel(1, "<h3>" + linesFaqs[totLines] + "</h3>");
                            //Como ya se actualiz� la l�nea, se incrementan las posiciones de las l�neas
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
                }//Se actualiza el n�mero de l�nas por categor�a en el vector respectivo y se guarda como string
                posNum = posNum + 1;
            }
        }

        //Este m�todo se encarga de actualizar el label de la vista del miembro
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