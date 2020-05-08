<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrearArticulo.aspx.cs" Inherits="CrearArticulo" MasterPageFile="~/MasterPageTCP.master" %>


<asp:content runat="server" ContentPlaceHolderID ="pageHead">

    <title> Crear artículo</title>
	<link rel="stylesheet" type="text/css" href="CssStyle/CrearArticuloCSS.css">

</asp:content>

<asp:content runat="server" ContentPlaceHolderID ="pageContent">

    <form id="form" runat="server">
		
    	<table align="center">
			<tr>
				<td><h2>Articulo corto</h2></td>
				<td><h2>Articulo largo</h2></td>
			</tr>
			<tr>
				<td class="auto-style2">Caracteristicas:</td>
				<td class="auto-style2">Caractersticas:</td>
			</tr>
			<tr>
				<td> <asp:Button ID="btnCrearCorto" runat="server" Text="Crear" OnClick="btnCrearCorto_Click"  /> </td>
				<td> <asp:Button ID="btnCrearLargo" runat="server" Text="Crear" OnClick="btnCrearLargo_Click" /> </td>
			</tr>
		</table>
		
    </form>

</asp:content>
