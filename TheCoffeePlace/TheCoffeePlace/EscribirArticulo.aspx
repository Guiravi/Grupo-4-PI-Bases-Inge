<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EscribirArticulo.aspx.cs" Inherits="EscribirArticulo"  %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<!DOCTYPE html>
<script runat="server">
</script>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Escribir artículo</title>
	<style type="text/css">
		.auto-style1 {
			width: 61%;
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
					<p>Artículo</p>
					<FTB:FreeTextBox ID="ftxtEditor" runat="server"></FTB:FreeTextBox>
    			</td>
			</tr>
			<tr>
				<td>
					<asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
    			</td>
			</tr>
		</table>

        <table class="auto-style1">

            <tr>
				    <td>
					    <p>Subir imágenes de su computadora, escoja un archivo .jpg o .png</p>
					    <asp:FileUpload ID="fileupImagen" runat="server" />
                        <br></br>
                        <asp:Label ID="lblErrorImagen" runat="server" Text="Error" Visible="false"></asp:Label>
				    </td>
		    </tr>
            <tr>
				    <td>
					    <asp:Button ID="btnSubir" runat="server" Text="Subir" />
				    </td>
		    </tr>
            <tr>
				    <td>
                        <p>Tabla de Imágenes</p>
					    <asp:GridView ID="gridviewImagenes" runat="server" CssClass="Grid" HeaderStyle-BackColor ="DarkCyan" BorderColor="Black" AutoGenerateColumns="False" EmptyDataText ="No hay archivos subidos" OnRowDeleting="OnRowDeleting">
                            <Columns>
                                <asp:BoundField DataField="NombreImagen" HeaderText="Nombre Imagen" />
                                <asp:ImageField DataImageUrlField="Imagen" HeaderText="Imagen"></asp:ImageField>   
                                <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                            </Columns>
                         </asp:GridView>
				    </td>
		    </tr>
        </table>
    
    </form>

    </body>
</html>
