﻿<%@ Page Language="C#" validateRequest="false" AutoEventWireup="true" CodeFile="EditarArticuloCorto.aspx.cs" Inherits="TheCoffeePlace.Views.EditarArticuloCorto
    "  MasterPageFile="~/MasterPageTCP.master" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>

<asp:content runat="server" ContentPlaceHolderID ="pageHead">
    
    <title> Escribir artículo</title>
<link rel="stylesheet" type="text/css" href="CssStyle/EscribirArticuloCSS.css">
</asp:content>


<asp:content runat="server" ContentPlaceHolderID ="pageContent">

    <form id="form" runat="server">

		<asp:label ID="lblUsername" runat="server" text="Manuelito01"></asp:label>
		
		<table class="auto-style1">
			<tr>
				<td>
					<p>Título</p>
					<asp:TextBox ID="txtTituloArticulo" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td>
					<p>Autor(es)</p>
					<asp:GridView ID="gvAutor" runat="server" AutoGenerateEditButton="True" HeaderStyle-CssClass="gvAutor" OnRowEditing="gvAutor_RowEditing" OnRowUpdating="gvAutor_RowUpdating" AutoGenerateDeleteButton="True" OnRowDeleted="gvAutor_RowDeleted" OnRowDeleting="gvAutor_RowDeleting">
					</asp:GridView>
					<br/>
                    <asp:TextBox ID="txtNuevoAutor" runat="server"></asp:TextBox>
					<asp:Button ID="btnAgregarAutor" runat="server" Text="Agregar autor" OnClick="btnAgregarAutor_Click"/>
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
					<p>Artículo<table class="auto-style1">
						<tr>
							<td class="auto-style4">
								<FTB:FreeTextBox ID="ftxtEditor" runat="server" ButtonSet="OfficeMac">
								</FTB:FreeTextBox>
							</td>
							<td class="auto-style3">
								<asp:CheckBoxList ID="chkblTopicos" runat="server"></asp:CheckBoxList>
							</td>
						</tr>
						</table>
					</p>
                </td>
			</tr>
			<tr>
				<td>
					<asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
    			</td>
			</tr>
		</table>

        <table class="auto-style1">
            <tr>
				    <td>
					    <p>Subir imágenes de su computadora, escoja un archivo .jpg o .png</p>
					    <asp:FileUpload ID="fileupImagen" runat="server" AllowMultiple="false" accept =".jpg, .png"/>
                        <br />
                        <asp:Label ID="lblErrorImagen" runat="server" Text="Error" Visible="false"></asp:Label>
				    </td>
		    </tr>
            <tr>
				    <td>
					    <asp:Button ID="btnSubir" runat="server" Text="Subir" OnClick="btnSubir_Click" />
				    </td>
		    </tr>
            <tr>
				    <td>
                        <h3>Tabla de Imágenes</h3>
                        <p>Puede arrastrar las imágenes subidas directamente a su artículo.</p>
					    <asp:GridView ID="gridviewImagenes" runat="server" HeaderStyle-BackColor ="DarkCyan" BorderColor="Black" AutoGenerateColumns="False" EmptyDataText ="No hay archivos subidos" OnRowDeleting="OnRowDeleting" DataKeysNames="idImagenPK">
                            <Columns>
                                <asp:BoundField DataField="idImagenPK" ItemStyle-CssClass="colID" HeaderStyle-CssClass="colID"/>
                                <asp:ImageField DataImageUrlField="rutaImagen" HeaderText="Imagen"></asp:ImageField>   
                                <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                            </Columns>
                         </asp:GridView>
				    </td>
		    </tr>
        </table>
    
   </form>

</asp:content>