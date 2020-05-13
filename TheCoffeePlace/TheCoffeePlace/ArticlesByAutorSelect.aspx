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
                    <asp:HyperLinkField Text="edit" DataNavigateUrlFields="idArticuloPK, tipo, usernameFK" DataNavigateUrlFormatString="~/EditarArticuloCorto.aspx?idArticuloPK={0}&tipo={1}&usernameFK={2}&descarga=0" HeaderText="Editar"/>
                     <asp:HyperLinkField Text="descarga" DataNavigateUrlFields="idArticuloPK, tipo, usernameFK" DataNavigateUrlFormatString="~/EditarArticuloCorto.aspx?idArticuloPK={0}&tipo={1}&usernameFK={2}&descarga=1" HeaderText="Descargar"/>

                </Columns>
                
			</asp:GridView>
               
			</table>
    		
    </form>
</body>
</html>