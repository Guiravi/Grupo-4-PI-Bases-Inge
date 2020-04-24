<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EscribirArticulo.aspx.cs" Inherits="EscribirArticulo" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Escribir artículo</title>
	<style type="text/css">
		.auto-style1 {
			width: 100%;
		}
	</style>
</head>
<body>
    <form id="form" runat="server">
		<table class="auto-style1">
			<tr>
				<td>
					<p>Título</p>
					<asp:TextBox ID="txtNombreArticulo" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td>
					<p>Resumen</p>
					<asp:TextBox ID="txtResumen" runat="server" TextMode="MultiLine"></asp:TextBox>
				</td>

			</tr>
			<tr>
				<td>
					<p>Artículo</p>
					<FTB:FreeTextBox id="ftxtEditor" runat="Server" ></FTB:FreeTextBox>
    			</td>
			</tr>
			<tr>
				<td>
					<asp:Button ID="btnGuardar" runat="server" Text="Guardar" />
    			</td>
			</tr>
		</table>
    </form>
</body>
</html>
