﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerResumen.aspx.cs" Inherits="VerResumenes.VerResumen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Resumen de Artículo</title>

    <style>
        .divider {
            width: 500px;
            height: auto;
            display: inline-block;
        }

        .textTitle {
            font-weight: bold;
            font-size: 24px;
        }

        .text {
            font-size: 24px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center">
            <asp:Label ID="labelTituloArticulo" runat="server" Text="Label" Style="display: inline-block; font-weight: normal; font-size: 42px"></asp:Label>
        </div>
        <p>
            <asp:Label ID="labelPalabraAutor" runat="server" Text="Autor(es): " class="textTitle"></asp:Label>
            <asp:Label ID="labelAutor" runat="server" Text="Label" class="text"></asp:Label>
        </p>

        <p>
            <asp:Label ID="labelPalabraTopicos" runat="server" Text="Tópico(s): " class="textTitle"></asp:Label>
            <asp:Label ID="labelTopicos" runat="server" Text="Label" class="text"></asp:Label>
        </p>

        <p>
            <asp:Label ID="labelPalabraResumen" runat="server" Text="Resumen: " class="textTitle"></asp:Label>
        </p>

        <p>
            <asp:Label ID="labelResumen" runat="server" Text="Label" class="text"></asp:Label>
        </p>

        <div style="text-align:center">
            <asp:Button ID="buttonVerArticulo" class="text" runat="server" Text="Ver artículo completo" OnClick="buttonVerArticulo_Click"/>
        </div>
    </form>


</body>
</html>
