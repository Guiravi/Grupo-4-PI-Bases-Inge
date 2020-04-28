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
		string filePath = "C:\\Users\\silvi0nsky\\Desktop\\U\\PI\\Grupo-4-PI-Bases-Inge\\PruebasConcepto\\FAQsApp\\FAQsApp\\FAQs.txt";
		lblFAQs.Text = File.ReadAllText(filePath);
	}
}