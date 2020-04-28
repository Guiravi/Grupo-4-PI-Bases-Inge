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
		lblAccion.Text = "Pregunta modificada!";
		string filePath = "C:\\Users\\silvi0nsky\\Desktop\\U\\PI\\Grupo-4-PI-Bases-Inge\\PruebasConcepto\\FAQsApp\\FAQsApp\\FAQs.txt";
		string[] lines = File.ReadAllLines(filePath);

		// Modificar los FAQs para luego escribir los cambios en FAQs.txt

		lines[0] = txtNuevaPregunta.Text;
		lines[1] = txtNuevaRespuesta.Text;
		lblFAQsHTML.Text = "<h1>FAQs</h1>";

		foreach(string line in lines)
		{ 
			lblFAQsHTML.Text += "<p>" + line + "</p>";
		}

		File.WriteAllLines(filePath, lines);
		
	}	
}