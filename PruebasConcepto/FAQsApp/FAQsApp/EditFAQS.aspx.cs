using System.IO;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditFAQS : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{

	}

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        // Cargar FAQs.txt para modificarlo
        string filePathFaqs = "C:\\Users\\Admin\\Documents\\Grupo-4-PI-Bases-Inge\\PruebasConcepto\\FAQsApp\\FAQsApp\\FAQs.txt";
        string filePathNumCat = "C:\\Users\\Admin\\Documents\\Grupo-4-PI-Bases-Inge\\PruebasConcepto\\FAQsApp\\FAQsApp\\NumCat.txt";
        string filePath = "C:\\Users\\Admin\\Documents\\Grupo-4-PI-Bases-Inge\\PruebasConcepto\\FAQsApp\\FAQsApp\\pruebas.txt";
        //Se carga las líneas del archivo con las preguntas y respuestas
        string[] linesFaqs = File.ReadAllLines(filePathFaqs);
        string[] linesNumCat = File.ReadAllLines(filePathNumCat);
        //Se pasa la prioridad a un int para manipularlo como número
        if (category.Text != ""&& txtNuevaPregunta.Text != ""&& txtNuevaRespuesta.Text != "")
        {
            //Las prioridades de preguntas permitidas con de 1 a 20
            int categoryFound = 0;
            int lineCategoryFound = 0;
            int lineStart = -1;
            foreach (string line in linesFaqs)
            {
                if (linesFaqs[lineCategoryFound]== category.Text) {
                    categoryFound = 1;
                   
                    lineStart = lineCategoryFound;
                    File.AppendAllText(filePath, lineStart + Environment.NewLine);
                }
                lineCategoryFound = lineCategoryFound + 1;

            }
            //if (linesNumCat[posNum] != "")
            string[] newLinesFaqs = new string[lineCategoryFound + 3];
            //Se inicia nuevo html
            lblFAQsHTML.Text = "<h1>FAQs</h1>";
            int posNum = 0;
            int totLines = 0;
            int totNewLines = 0;
            foreach (string line in linesNumCat)
            {
                int firstInLine = 1;
                if (linesNumCat[posNum] != "") {
                    int finish = Int32.Parse(linesNumCat[posNum]);
                    while (totLines < finish)
                    {

                        if (firstInLine == 1)
                        {
                            firstInLine = 0;
                            lblFAQsHTML.Text += "<h2>CATEGORY</h2>";
                            lblFAQsHTML.Text += "<h3>" + linesFaqs[totLines] + "</h3>";
                            newLinesFaqs[totNewLines] = linesFaqs[totLines];
                            totLines = totLines + 1;
                            totNewLines = totNewLines + 1;
                            if (categoryFound == 1 && lineStart == totLines - 1)
                            {
                                lblFAQsHTML.Text += "<h3>QUESTION</h3>";
                                lblFAQsHTML.Text += "<h3>" + txtNuevaPregunta.Text + "</h3>";
                                newLinesFaqs[totNewLines] = txtNuevaPregunta.Text;
                                totNewLines = totNewLines + 1;
                                lblFAQsHTML.Text += "<h3>ANSWER</h3>";
                                lblFAQsHTML.Text += "<h3>" + txtNuevaRespuesta.Text + "</h3>";
                                newLinesFaqs[totNewLines] = txtNuevaRespuesta.Text;
                                totNewLines = totNewLines + 1;
                            }
                            
                        }
                        else
                        {
                            lblFAQsHTML.Text += "<h3>QUESTION</h3>";
                            lblFAQsHTML.Text += "<h3>" + linesFaqs[totLines] + "</h3>";
                            newLinesFaqs[totNewLines] = linesFaqs[totLines];
                            totLines = totLines + 1;
                            totNewLines = totNewLines + 1;
                            lblFAQsHTML.Text += "<h3>ANSWER</h3>";
                            lblFAQsHTML.Text += "<h3>" + linesFaqs[totLines] + "</h3>";
                            newLinesFaqs[totNewLines] = linesFaqs[totLines];
                            totLines = totLines + 1;
                            totNewLines = totNewLines + 1;
                        }

                    }
                }
              
                linesNumCat[posNum] = Convert.ToString(totNewLines);
                posNum = posNum + 1;
            }
            File.WriteAllLines(filePathNumCat, linesNumCat);
            if (categoryFound == 0) {

                lblFAQsHTML.Text += "<h2>CATEGORY</h2>";
                lblFAQsHTML.Text += "<h3>" + category.Text + "</h3>";
                newLinesFaqs[totNewLines] = category.Text;
                totNewLines = totNewLines + 1;
                lblFAQsHTML.Text += "<h3>QUESTION</h3>";
                lblFAQsHTML.Text += "<h3>" + txtNuevaPregunta.Text + "</h3>";
                newLinesFaqs[totNewLines] = txtNuevaPregunta.Text;
                totNewLines = totNewLines + 1;
                lblFAQsHTML.Text += "<h3>ANSWER</h3>";
                lblFAQsHTML.Text += "<h3>" + txtNuevaRespuesta.Text + "</h3>";
                newLinesFaqs[totNewLines] = txtNuevaRespuesta.Text;
                totNewLines = totNewLines + 1;
                File.AppendAllText(filePathNumCat, Convert.ToString(totNewLines));
            }
            File.WriteAllLines(filePathFaqs, newLinesFaqs);
            //for (int i = 0; i < 40; i = i + 2)
            //{
            //lblFAQsHTML.Text += "<h3>QUESTION</h3>";
            //Aquí estan las preguntas
            //lblFAQsHTML.Text += "<p>" + lines[i] + "</p>";
            //lblFAQsHTML.Text += "<h3>ANSWER</h3>";
            //Aquí estan las repuestas
            //lblFAQsHTML.Text += "<p>" + lines[i + 1] + "</p>";
            // }
            //Se reescribe todo en el archivo.txt con los faqs
            //File.WriteAllLines(filePath, lines);
        }
        else
        {
            //En caso de introducir un texto inválido
            lblFAQsHTML.Text = "<p>texto inválido</p>";
        }
    }	
}