<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
		img {
			width: 50px;
			height: 50px;
		}
	</style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:FileUpload ID="fileUpImagen" runat="server" accept =".jpg, .png"/>
        <p>
        <asp:Button ID="btnSubir" runat="server" Text="Subir" OnClick="BtnSubir_Click" Height="34px" Width="63px" />
        </p>
        <p>
            <asp:Label ID="lblAvisoError" runat="server" Text="Error"></asp:Label>
        </p>
        <asp:GridView ID="gridviewImagenes" runat="server" CssClass="Grid" HeaderStyle-BackColor ="DarkCyan" BorderColor="Black" AutoGenerateColumns="False" EmptyDataText ="No hay archivos subidos" OnRowDeleting="OnRowDeleting">
            <Columns>
                <asp:BoundField DataField="NombreImagen" HeaderText="Nombre Imagen" />
                <asp:ImageField DataImageUrlField="Imagen" HeaderText="Imagen"></asp:ImageField>   
                <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
            </Columns>
        </asp:GridView>
    </form>
    </body>
</html>
