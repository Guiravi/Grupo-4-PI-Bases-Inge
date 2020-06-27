<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BuscarArticulos.aspx.cs" Inherits="BuscarArticulos" MasterPageFile="~/MasterPageTCP.master" %>

<asp:content runat="server" ContentPlaceHolderID ="pageHead">
    
    <title>Buscar Artículos</title>
    <link rel="stylesheet" type="text/css" href="CssStyle/BuscarArticulosCSS.css">

</asp:content>

<asp:content runat="server" ContentPlaceHolderID ="pageContent">
    <form id="form1" runat="server">
        <div id="buscador">
			<asp:TextBox ID="txtBusqueda" runat="server" CssClass="txtB"></asp:TextBox>
			<asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" CssClass="btn"/>
        </div>
		<div id="opciones">
             <h2>Opciones de Búsqueda</h2>
             <h4>  Tipo:  <asp:CheckBox ID="chkbTipoCorto" runat="server" Text="Corto (0)"/><asp:CheckBox ID="chkbTipoLargo" runat="server" Text="Largo (1)"/></h4>
             <asp:RadioButtonList ID="radbtnltTopico" runat="server" CssClass="liOp">
                <asp:ListItem Value = "1"> Búsqueda por tópicos </asp:ListItem>
                <asp:ListItem Value = "2"> Búsqueda por títulos </asp:ListItem>
                <asp:ListItem Value = "3"> Búsqueda por autores </asp:ListItem>   
             </asp:RadioButtonList>
             <asp:Button ID="btnMostrarTodos" runat="server" Text="Mostrar todos" CssClass="btnMT" OnClick="btnMostrarTodos_Click" />
		</div>
			<asp:GridView ID="gvArticulos" AutoGenerateColumns="false" runat="server" AllowPAging="true" PageSize="20" OnPageIndexChanging="gvArticulos_PageIndexChanging" CssClass="grid"
                PagerStyle-CssClass="pgr" HeaderStyle-CssClass="header">
				<Columns>
                    <asp:BoundField DataField="idArticuloPK" ItemStyle-CssClass="colID" HeaderStyle-CssClass="colID"/>
                    <asp:BoundField DataField="tipo" HeaderText="Tipo"/>  
                    <asp:HyperLinkField DataTextField="titulo" DataNavigateUrlFields="idArticuloPK" DataNavigateUrlFormatString="~/VerResumen.aspx?idArticuloPK={0}" HeaderText="Título" ItemStyle-CssClass="titulo"/>
                    <asp:BoundField DataField="resumen" HeaderText="Resumen" ItemStyle-CssClass="resumen"/>   
                    <asp:BoundField DataField="nombreAutor" HeaderText="Autor(es)"/>   
                    <asp:BoundField DataField="fechaPublicacion" HeaderText="FechaPublicacion"/>   
                </Columns>
			</asp:GridView>
        
    </form>
</asp:content>
