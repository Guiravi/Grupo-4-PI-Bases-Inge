<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrearArticulo.aspx.cs" Inherits="CrearArticulo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Crear artículo</title>
	<style type="text/css">
		.auto-style1 {
			width: 100%;
			height: 100%;
		}
		.auto-style2 {
			height: 23px;
		}
	</style>
</head>
<body>
    <form id="form" runat="server">
		
    	<table align="center" class="auto-style1">
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
</body>
</html>
