<!--Esta es la parte declarativa de la vista de MemberFaqs-->
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberFaqs.aspx.cs" Inherits="MemberFaqs" %>

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
                    <!--Esta es la opción para añadir la categoría de la pregunta correspondiente-->
                    <td class="auto-style2">Category</td>
					<td>
                        <!--la información de la categoría provista por el usuario se guarda en la variable category
                            para que pueda ser utilizada en el resto de las clases-->
                        <asp:TextBox ID="category" runat="server"></asp:TextBox>             	
                    </td>
				</tr>
                <tr>
					<!--Se pregunta por la nueva pregunta al miembro-->
                    <td class="auto-style2">New question </td>
					<td>
        	            <!--la nueva pregunta se guarda en question para utilizarla en resto de las clases-->
                        <asp:TextBox ID="question" runat="server"></asp:TextBox>
        			</td>
				</tr>
				<tr>
					<!--Se pregunta por la nueva respuesta al miembro-->
                    <td class="auto-style2">New answer</td>
					<td>
        	            <!--la nueva respuesta se guarda en answer para utilizarla en resto de las clases-->
                        <asp:TextBox ID="answer" runat="server"></asp:TextBox>
        			</td>
				</tr>
				<tr>
					<td class="auto-style2">&nbsp;</td>
					<td>
        	<asp:Button ID="btnAdd" runat="server" Text="Modify" OnClick="BtnAddClick"/>
					&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
				</tr>
			</table>
    		<p>
			    <!--la variable lblFAQsHTML será utilizada para mostrar las categorías, preguntas y respuestas en  
                    la vista de MemberFaqs-->
                <asp:Label ID="lblFAQsHTML" runat="server" Text=""></asp:Label>
        </p>
    </form>
</body>
</html>