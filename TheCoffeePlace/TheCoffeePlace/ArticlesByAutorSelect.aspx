<!--Esta es la parte declarativa de la vista de ArticlesByAutorSelect-->
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ArticlesByAutorSelect.aspx.cs" Inherits="MemberFaqs" %>

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
			<asp:label ID="lblUsername" runat="server" text="Manuelito01"></asp:label>
		    </div>
            <table class="auto-style1">
			
                <tr>
                    <!--Esta es la opción para añadir la categoría de la pregunta correspondiente-->
                    <p>Ingrese el título de artículo que desee editar</p>
					<asp:TextBox ID="title" runat="server"></asp:TextBox>
				</tr>
				<tr>
					<td class="auto-style2">&nbsp;</td>
					<td>
        	<asp:Button ID="btnAdd" runat="server" Text="Editar" OnClick="BtnAddClick"/>
					&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
				</tr>
			</table>
    		
    </form>
</body>
</html>