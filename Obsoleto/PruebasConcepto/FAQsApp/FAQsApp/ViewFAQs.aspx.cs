using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewFAQs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Cargar FAQs.txt para modificarlo
        string filePathFaqs = "C:\\Users\\Admin\\Documents\\Grupo-4-PI-Bases-Inge\\PruebasConcepto\\FAQsApp\\FAQsApp\\FAQs.txt";
        string filePathNumCat = "C:\\Users\\Admin\\Documents\\Grupo-4-PI-Bases-Inge\\PruebasConcepto\\FAQsApp\\FAQsApp\\NumCat.txt";
        //Se carga las líneas del archivo con las preguntas y respuestas
        string[] linesFaqs = File.ReadAllLines(filePathFaqs);
        string[] linesNumCat = File.ReadAllLines(filePathNumCat);
        //Se pasa la prioridad a un int para manipularlo como número
        //if (linesNumCat[posNum] != "")
        //Se inicia nuevo html
        lblFAQs.Text = "<h1>FAQs</h1>";
        int posNum = 0;
        int totLines = 0;
        int totNewLines = 0;
        foreach (string line in linesNumCat)
        {
            int firstInLine = 1;
            if (linesNumCat[posNum] != "")
            {
                int finish = Int32.Parse(linesNumCat[posNum]);
                while (totLines < finish)
                {
                    if (firstInLine == 1)
                    {
                        firstInLine = 0;
                        lblFAQs.Text += "<h2>CATEGORY</h2>";
                        lblFAQs.Text += "<h3>" + linesFaqs[totLines] + "</h3>";
                        totLines = totLines + 1;

                    }
                    else
                    {
                        lblFAQs.Text += "<h3>QUESTION</h3>";
                        lblFAQs.Text += "<h3>" + linesFaqs[totLines] + "</h3>";
                        totLines = totLines + 1;
                        lblFAQs.Text += "<h3>ANSWER</h3>";
                        lblFAQs.Text += "<h3>" + linesFaqs[totLines] + "</h3>";
                        totLines = totLines + 1;

                    }


                }

                linesNumCat[posNum] = Convert.ToString(totNewLines);
                posNum = posNum + 1;
            }
        }

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


}