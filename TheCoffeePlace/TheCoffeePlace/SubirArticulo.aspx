<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubirArticulo.aspx.cs" Inherits="SubirArticulo"  %>
<!DOCTYPE html>
<script runat="server">
</script>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Escribir artículo</title>
	<style type="text/css">
		.auto-style1 {
			width: 100%;
		}
		.auto-style2 {
			margin-left: 960px;
		}
	</style>
</head>
<body>
    <form id="form" runat="server">
		<div class="auto-style2">
			<asp:label ID="lblUsername" runat="server" text="Manuelito01"></asp:label>
		</div>
		<table class="auto-style1">
			<tr>
				<td>
					<p>Título</p>
					<asp:TextBox ID="txtTituloArticulo" runat="server"></asp:TextBox>
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
					<p>Seleccione un archivo </p>
					<asp:FileUpload ID="fuArticulo" runat="server" accept=".docx"/>
    			</td>
			</tr>
			<tr>
				<td>
					<asp:Button ID="btnSubir" runat="server" Text="Subir artículo" OnClick="btnGuardar_Click" />
    			</td>
			</tr>
		</table>
    </form>
</body>
</html>
