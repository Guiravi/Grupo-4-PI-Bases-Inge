<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BuscarArticulos.aspx.cs" Inherits="BuscarArticulos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Buscar Artículos</title>
    <style type="text/css">
		.auto-style1 {
			width: 100%;
		}
		.auto-style2 {
			margin-left: 960px;
		}
		img {
			width: 50px;
			height: 50px;
		}
	    .colID {
	        display: none;
        }

        .btnMargin {
            margin-left: 70px;
            margin-bottom: 5px;
        }

        .radlistMargin {
            margin-bottom: 10px;
        }

        .gridMargin {
            margin-top: 10px;
        }
        
	</style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        	<br />
			<asp:TextBox ID="txtTopico" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
			<asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
			<br />
            <br />
			<div style="width:250px;height:auto;border:2px solid #000;" >
                <h2>Opciones de Búsqueda</h2>
                <h4 style="margin-left: 5px">  Tipo:  <asp:CheckBox ID="chkbTipoCorto" runat="server" Text="Corto (0)"/><asp:CheckBox ID="chkbTipoLargo" runat="server" Text="Largo (1)"/></h4>
                <asp:RadioButtonList ID="radbtnltTopico" runat="server" CssClass="radlistMargin">
                    <asp:ListItem> Búsqueda por tópicos </asp:ListItem>
                    <asp:ListItem> Búsqueda por títulos </asp:ListItem>
                    <asp:ListItem> Búsqueda por autores </asp:ListItem>   
                </asp:RadioButtonList>
                <asp:Button ID="btnMostrarTodos" runat="server" Text="Mostrar todos" CssClass="btnMargin" />
			</div>
			<asp:GridView ID="gvArticulos" AutoGenerateColumns="false" runat="server" CssClass="gridMargin">
				<Columns>
                    <asp:BoundField DataField="idArticuloPK" ItemStyle-CssClass="colID" HeaderStyle-CssClass="colID"/>
                    <asp:BoundField DataField="tipo" HeaderText="Tipo"/>  
                    <asp:HyperLinkField DataTextField="titulo" DataNavigateUrlFields="idArticuloPK" DataNavigateUrlFormatString="~/EscribirArticulo.aspx?idArticuloPK={0}" HeaderText="Título"/>
                    <asp:BoundField DataField="resumen" HeaderText="Resumen"/>   
                    <asp:BoundField DataField="nombreAutor" HeaderText="Autor"/>   
                    <asp:BoundField DataField="fechaPublicacion" HeaderText="FechaPublicacion"/>   
                </Columns>
			</asp:GridView>
        </div>
    </form>
</body>
</html>
