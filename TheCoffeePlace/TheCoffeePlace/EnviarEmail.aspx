<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnviarEmail.aspx.cs" Inherits="EnviarEmail" MasterPageFile="~/MasterPageTCP.master" %>

<asp:Content runat="server" ContentPlaceHolderID="pageHead">
    <title>Enviar Mensaje</title>
    <link rel="stylesheet" type="text/css" href="CssStyle/EnviarEmailCss.css">
</asp:Content>

<asp:Content ContentPlaceHolderID="pageContent" runat="Server">
    <form id="form1" runat="server">
        <div style="text-align: center; margin-bottom: 40px">
            <asp:Label ID="labelTitulo" runat="server" Style="font-size: 42px" Text="Enviar mensaje a otro usuario"></asp:Label>
        </div>
        <div>
            <table style="margin-left:auto;margin-right:auto">
                <tr>
                    <th style="text-align: left; margin-right: 10px">
                        <asp:Label ID="labelPalabraUsuario" runat="server" Text="Usuario Destino: " CssClass="textTitle"></asp:Label>
                    </th>
                    <th style="width: 400px">
                        <asp:DropDownList ID="DownListDestino" runat="server" style="margin-left: 0px;font-size:24px" Width="400px" OnSelectedIndexChanged="DownListDestino_SelectedIndexChanged">
                        </asp:DropDownList>
                    </th>
                </tr>
                <tr>
                    <th style="text-align: left; margin-right: 10px">
                        <asp:Label ID="labelPalabraAsunto" runat="server" Text="Asunto: " CssClass="textTitle"></asp:Label>
                    </th>
                    <th style="width: 400px">
                        <asp:TextBox ID="TextBoxAsunto" runat="server" OnTextChanged="TextBoxAsunto_TextChanged" Width="400px" Style="font-size: 20px"></asp:TextBox>
                    </th>
                </tr>
                <tr>
                    <th style="text-align: left">
                        <asp:Label ID="labelPalabraMensaje" runat="server" Text="Mensaje: " CssClass="textTitle"></asp:Label>
                    </th>
                </tr>

                <tr>
                    <th colspan="2">
                        <asp:TextBox ID="TextBoxMensaje" runat="server" Height="200px" OnTextChanged="TextBoxMensaje_TextChanged" Rows="20" TextMode="MultiLine" Width="600px" Style="font-size: 20px"></asp:TextBox>
                    </th>
                </tr>
            </table>
        </div>

        <div style="text-align: center">

            <asp:Button ID="ResetButton" runat="server" Text="Reiniciar Mensaje" CssClass="text" OnClick="ResetButton_Click" />
            <div style="display:inline-block; margin-left:100px"></div>
            <asp:Button ID="SendButton" runat="server" Text="Enviar Mensaje" CssClass="text" OnClick="SendButton_Click" />

        </div>

        <div style="text-align:center; margin-top:50px">
            <asp:Label ID="labelNota" runat="server" Text="El usuario destino recibirá el mensaje por email." CssClass="text"></asp:Label>
        </div>
    </form>
</asp:Content>
