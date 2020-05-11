<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageTCP.master" AutoEventWireup="true" CodeFile="MiPerfil.aspx.cs" Inherits="MiPerfil" %>

<asp:content runat="server" ContentPlaceHolderID ="pageHead">
    <title>Mi Perfil</title>
	<link rel="stylesheet" type="text/css" href="CssStyle/MiPerfilCSS.css">
</asp:Content>

<asp:Content ContentPlaceHolderID="pageContent" Runat="Server">
    <form id="form1" runat="server">
    
        <asp:button ID="btnCrearArticulo" runat="server" text="Crear Artículo" OnClick="btnCrearArticulo_Click" CssClass="btnFuncion"/>

        <asp:button ID="btnMemberFaqs" runat="server" text="Modificar FAQ" OnClick="btnMemberFaqs_Click" CssClass="btnFuncion"/>
    
        <asp:button ID="btnEnviarEmail" runat="server" text="Enviar E-mail" OnClick="btnEnviarEmail_Click" CssClass="btnFuncion"/>

        <div>
            <asp:button ID="EnviarMensaje" runat="server" text="Enviar un mensaje" OnClick="btnEnviarMensaje_Click"/>
        </div>

    </form>

</asp:Content>