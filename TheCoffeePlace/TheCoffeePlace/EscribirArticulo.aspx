<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EscribirArticulo.aspx.cs" Inherits="EscribirArticulo" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Escribir artículo</title>
</head>
<body>
    <form id="form" runat="server">
		<FTB:FreeTextBox id="ftxtEditor" runat="Server" ></FTB:FreeTextBox>
    </form>
</body>
</html>
