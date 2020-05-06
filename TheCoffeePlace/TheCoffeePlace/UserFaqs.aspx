<!--Esta es la parte declarativa de la vista de UserFaqs-->
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserFaqs.aspx.cs" Inherits="UserFaqs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
		<!--la variable lblFAQs será utilizada para mostrar las categorías, preguntas y respuestas en 
        la vista de UserFaqs-->
        <asp:Label ID="lblFAQs" runat="server" Text=""></asp:Label>
    </form>
</body>
</html>