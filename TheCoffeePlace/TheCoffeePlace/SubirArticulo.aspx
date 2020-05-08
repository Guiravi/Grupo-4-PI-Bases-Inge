<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubirArticulo.aspx.cs" Inherits="SubirArticulo"  MasterPageFile="~/MasterPageTCP.master" %>

<asp:content runat="server" ContentPlaceHolderID ="pageHead">

    <title> Subir artículo</title>
    <link rel="stylesheet" type="text/css" href="CssStyle/SubirArticuloCSS.css">

</asp:Content>


<asp:Content ContentPlaceHolderID="pageContent" Runat="Server">
    <form id="form" runat="server">
		<div>
			<asp:label ID="lblUsername" runat="server" text="Manuelito01"></asp:label>
		</div>
		<table>
			<tr>
				<td>
					<p>Título</p>
					<asp:TextBox ID="txtTituloArticulo" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td>
					<p>Resumen</p>
					<asp:TextBox ID="txtResumen" runat="server" TextMode="MultiLine"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td>
					<p>Seleccione un archivo </p>
					<asp:FileUpload ID="fuArticulo" runat="server" accept=".docx"/>
    			</td>
			</tr>
			<tr>
				<td>
					<asp:CheckBoxList ID="chkblTopicos" runat="server"></asp:CheckBoxList>
    			</td>
			</tr>
			<tr>
				<td>
					<asp:Button ID="btnSubir" runat="server" Text="Subir artículo" OnClick="btnGuardar_Click" />
    			</td>
			</tr>
		</table>
    </form>
</asp:Content>
