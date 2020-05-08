<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserFaqs.aspx.cs" Inherits="UserFaqs" MasterPageFile="~/MasterPageTCP.master" %>

<asp:content runat="server" ContentPlaceHolderID ="pageHead">

    <title>FAQ's</title>
    <link rel="stylesheet" type="text/css" href="CssStyle/UserFaqsCSS.css">

</asp:Content>

<asp:Content ContentPlaceHolderID="pageContent" Runat="Server">
    <form id="form1" runat="server">
		<!--la variable lblFAQs será utilizada para mostrar las categorías, preguntas y respuestas en 
        la vista de UserFaqs-->
        <asp:Label ID="lblFAQs" runat="server" Text=""></asp:Label>
    </form>
</asp:Content>