<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrearArticulo.aspx.cs" Inherits="CrearArticulo" MasterPageFile="~/MasterPageTCP.master" %>


<asp:content runat="server" ContentPlaceHolderID ="pageHead">

    <title>Crear artículo</title>
	<link rel="stylesheet" type="text/css" href="CssStyle/CrearArticuloCSS.css">

</asp:content>

<asp:content runat="server" ContentPlaceHolderID ="pageContent">

    <form id="form" runat="server">		
    <ul id="opciones">
        <li id="artCorto">
            <h2>Artículo corto</h2>
            <p>Características:</p>
            <p class="carac">Los artículos cortos son escritos directamente en el sitio mediante un editor de texto. 
                Estos artículos pueden ser personalizados con varias herramientas disponibles en el editor.
                Además, si desea agregar imágenes puede hacerlo copiando y pegando de Internet, o si son
                imágenes que tiene en su computadora puede subirlas con la herramienta dada por el sitio.
                La edición se hace sobre el mismo editor de texto.
            </p>
            <asp:Button ID="btnCrearCorto" runat="server" Text="Crear" OnClick="btnCrearCorto_Click"  CssClass="btn"/>
        </li>

        <li id="artLargo">
            <h2>Artículo largo</h2>
            <p>Características:</p>
            <p class="carac">Los artículos largos son archivos subidos del formato Documentos de Microsoft Word únicamente, 
                no se acepta otro tipo de archivo. Estos artículos se despligan en la plataforma como un .pdf. Si desea editar
                este tipo de artículo, lo puede descargar del sitio y volver a subirlo con la versión editada.
            </p>
            <asp:Button ID="btnCrearLargo" runat="server" Text="Crear" OnClick="btnCrearLargo_Click" CssClass="btn"/>
        </li>
    </ul>
		
    </form>

</asp:content>
