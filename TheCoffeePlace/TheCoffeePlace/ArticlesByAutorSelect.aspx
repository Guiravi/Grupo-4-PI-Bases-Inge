<!--Esta es la parte declarativa de la vista de ArticlesByAutorSelect-->
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ArticlesByAutorSelect.aspx.cs" Inherits="ArticlesByAutorSelect" %>

<!DOCTYPE html>
<!--Esta es la parte declarativa de la vista de ArticlesByAutorSelect-->
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
			<div class="auto-style2">
			<asp:label ID="username" runat="server" text="Manuelito01"></asp:label>
		    </div>
            <table class="auto-style1">
			    <asp:GridView ID="articles" AutoGenerateColumns="false" runat="server" AllowPAging="true"  OnPageIndexChanging="gvArticulos_PageIndexChanging" PageSize="20"  CssClass="grid"
                PagerStyle-CssClass="pgr" HeaderStyle-CssClass="header">
				<Columns>
                    <asp:BoundField DataField="titulo" HeaderText="titulo" />
                    <asp:BoundField DataField="fechaPublicacion" HeaderText="FechaPublicacion"/>   
                    <asp:HyperLinkField Text="edit" DataNavigateUrlFields="idArticuloPK, titulo, resumen, tipo, nombreAutor, usernameFK" DataNavigateUrlFormatString="~/EditarArticuloCorto.aspx?idArticuloPK={0}&titulo={1}&resumen={2}&tipo={3}&nombreAutor={4}&usernameFK={5}" HeaderText="Editar"/>
                    </Columns>
                
			</asp:GridView>
               
			</table>
    		
    </form>
</body>
</html>