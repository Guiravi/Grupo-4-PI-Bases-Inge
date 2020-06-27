using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheCoffeePlace.Views;
using TheCoffeePlace.Controllers;

public partial class EnviarEmail : System.Web.UI.Page, IView_EnviarEmail
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if ( !IsPostBack )
        {
            TextBoxAsunto.Text = string.Empty;
            TextBoxMensaje.Text = string.Empty;
            EnviarEmailController enviarEmailController = new EnviarEmailController();
            listaAutores = enviarEmailController.getListaAutores();
            DownListDestino.DataSource = listaAutores;
            DownListDestino.DataBind();
        }
    }

    protected void TextBoxAsunto_TextChanged(object sender, EventArgs e)
    {

    }

    protected void TextBoxMensaje_TextChanged(object sender, EventArgs e)
    {

    }

    protected void ResetButton_Click(object sender, EventArgs e)
    {
        TextBoxMensaje.Text = string.Empty;
    }

    protected void SendButton_Click(object sender, EventArgs e)
    {
        if (asunto != string.Empty && mensaje != string.Empty)
        {
            EnviarEmailController enviarEmailController = new EnviarEmailController();
            enviarEmailController.sendMail(this);
            labelNota.Text = "Mensaje enviado.";

            TextBoxAsunto.Text = string.Empty;
            TextBoxMensaje.Text = string.Empty;
        } else
        {
            labelNota.Text = "Por favor ingrese un mensaje y un asunto";
        }
    }

    private List<string> _listaAutores;
    public List<string> listaAutores
    {
        get { return _listaAutores; }
        set { _listaAutores = value; }
    }

    public string destino
    {
        get { return DownListDestino.SelectedValue; }
    }

    public string asunto
    {
        get { return TextBoxAsunto.Text; }
    }

    public string mensaje
    {
        get { return TextBoxMensaje.Text; }
    }

    protected void DownListDestino_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}