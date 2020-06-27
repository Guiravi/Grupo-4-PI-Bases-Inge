<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberFaqs.aspx.cs" Inherits="MemberFaqs"  MasterPageFile="~/MasterPageTCP.master" %>

<asp:content runat="server" ContentPlaceHolderID ="pageHead">

    <title>FAQ's</title>
	<link rel="stylesheet" type="text/css" href="CssStyle/MemberFaqsCss.css">

</asp:content>

<asp:content runat="server" ContentPlaceHolderID ="pageContent">
    <form id="form1" runat="server">
			<table>
				<tr>
                    <!--Esta es la opción para añadir la categoría de la pregunta correspondiente-->
                    <td>Category</td>
					<td>
                        <!--la información de la categoría provista por el usuario se guarda en la variable category
                            para que pueda ser utilizada en el resto de las clases-->
                        <asp:TextBox ID="category" runat="server"></asp:TextBox>             	
                    </td>
				</tr>
                <tr>
					<!--Se pregunta por la nueva pregunta al miembro-->
                    <td>New question </td>
					<td>
        	            <!--la nueva pregunta se guarda en question para utilizarla en resto de las clases-->
                        <asp:TextBox ID="question" runat="server"></asp:TextBox>
        			</td>
				</tr>
				<tr>
					<!--Se pregunta por la nueva respuesta al miembro-->
                    <td >New answer</td>
					<td>
        	            <!--la nueva respuesta se guarda en answer para utilizarla en resto de las clases-->
                        <asp:TextBox ID="answer" runat="server"></asp:TextBox>
        			</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
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
</asp:content>