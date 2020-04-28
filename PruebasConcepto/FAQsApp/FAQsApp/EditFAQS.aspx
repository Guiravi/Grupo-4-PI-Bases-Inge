<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditFAQS.aspx.cs" Inherits="EditFAQS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<style type="text/css">
		.auto-style1 {
			width: 100%;
		}
		.auto-style2 {
			width: 141px;
		}
	</style>
</head>
<body>
    <form id="form1" runat="server">
			<table class="auto-style1">
				<tr>
					<td class="auto-style2">Nueva pregunta </td>
					<td>
        	<asp:TextBox ID="txtNuevaPregunta" runat="server"></asp:TextBox>
        			</td>
				</tr>
				<tr>
					<td class="auto-style2">Nueva respuesta</td>
					<td>
        	<asp:TextBox ID="txtNuevaRespuesta" runat="server"></asp:TextBox>
        			</td>
				</tr>
				<tr>
					<td class="auto-style2">&nbsp;</td>
					<td>
        	<asp:Button ID="btnAgregar" runat="server" Text="Modificar" OnClick="btnAgregar_Click"/>
					&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:Label ID="lblAccion" runat="server" Text=""></asp:Label>
					</td>
				</tr>
			</table>
    		<p>
			<asp:Label ID="lblFAQsHTML" runat="server" Text=""></asp:Label>
        </p>
    </form>
</body>
</html>
