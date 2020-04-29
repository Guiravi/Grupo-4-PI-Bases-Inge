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
		string filePath = "C:\\Users\\Admin\\Documents\\Grupo-4-PI-Bases-Inge\\PruebasConcepto\\FAQsApp\\FAQsApp\\FAQs.txt";
        //Se carga las líneas del archivo con las preguntas y respuestas
        string[] lines = File.ReadAllLines(filePath);

        //Se inicia nuevo html                                             
        lblFAQs.Text = "<h1>FAQs</h1>";
        lblFAQs.Text += "<h2> TOP 20 QUESTIONS </h2>";
        //Se imprimen todas las preguntas y respuestas en el html
        for (int i = 0; i < 40; i = i + 2)
        {
            lblFAQs.Text += "<h3>QUESTION</h3>";
            //Aquí estan las preguntas
            lblFAQs.Text += "<p>" + lines[i] + "</p>";
            lblFAQs.Text += "<h3>ANSWER</h3>";
            //Aquí estan las repuestas
            lblFAQs.Text += "<p>" + lines[i + 1] + "</p>";
        }

    }
}