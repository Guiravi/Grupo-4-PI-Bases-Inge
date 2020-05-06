<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BuscarArticulos.aspx.cs" Inherits="BuscarArticulos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        	<br />
			<asp:TextBox ID="txtTopico" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
			<asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
			<br />
			<asp:GridView ID="gvArticulos" AutoGenerateColumns="false" runat="server">
				<Columns>
                    <asp:BoundField DataField="titulo" HeaderText="Titulo"/>   
                    <asp:BoundField DataField="resumen" HeaderText="Resumen"/>   
                    <asp:BoundField DataField="nombreAutor" HeaderText="Username"/>   
                    <asp:BoundField DataField="fechaPublicacion" HeaderText="FechaPublicacion"/>   
                </Columns>
			</asp:GridView>
        </div>
    </form>
</body>
</html>
