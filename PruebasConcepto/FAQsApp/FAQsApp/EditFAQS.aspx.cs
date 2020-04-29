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
		string filePath = "C:\\Users\\Admin\\Documents\\Grupo-4-PI-Bases-Inge\\PruebasConcepto\\FAQsApp\\FAQsApp\\FAQs.txt";
        //Se carga las líneas del archivo con las preguntas y respuestas
        string[] lines = File.ReadAllLines(filePath);
        //Se pasa la prioridad a un int para manipularlo como número
        int prioridad = Int32.Parse(opcionNueva.Text);
        //Las prioridades de preguntas permitidas con de 1 a 20
        if (prioridad > 0 && prioridad < 21)
        {
            int posicion = (prioridad - 1) * 2;
            lines[posicion] = txtNuevaPregunta.Text;
            lines[posicion + 1] = txtNuevaRespuesta.Text;

            //Se inicia nuevo html
            lblFAQsHTML.Text = "<h1>FAQs</h1>";
            lblFAQsHTML.Text += "<h2> TOP 20 QUESTIONS </h2>";
            for (int i = 0; i < 40; i = i + 2)
            {
                lblFAQsHTML.Text += "<h3>QUESTION</h3>";
                //Aquí estan las preguntas
                lblFAQsHTML.Text += "<p>" + lines[i] + "</p>";
                lblFAQsHTML.Text += "<h3>ANSWER</h3>";
                //Aquí estan las repuestas
                lblFAQsHTML.Text += "<p>" + lines[i + 1] + "</p>";
            }
            //Se reescribe todo en el archivo.txt con los faqs
            File.WriteAllLines(filePath, lines);
        }
        else {
            //En caso de introducir una prioridad inválida
            lblFAQsHTML.Text += "<p>opción de proridad denegada</p>";
        }
    }	
}