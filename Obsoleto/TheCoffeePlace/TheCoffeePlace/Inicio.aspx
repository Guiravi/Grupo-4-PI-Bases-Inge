<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageTCP.master" AutoEventWireup="true" CodeFile="Inicio.aspx.cs" Inherits="Inicio" %>


<asp:content runat="server" ContentPlaceHolderID ="pageHead">

    <title>Inicio</title>
	<link rel="stylesheet" type="text/css" href="CssStyle/InicioCSS.css">

</asp:content>

<asp:content runat="server" ContentPlaceHolderID ="pageContent">

    
    <h1>The Coffee Place</h1>
    <ul>
    <li><p>Somos una comunidad de práctica con el enfoque de brindar una plataforma para que personas
        de distintos trasfondos sociales, económicos y culturales compartan sus experiencias y
        conocimientos en las distintas áreas disponibles en el sitio.       
    </p></li>
    <li><p>
        Este proceso de compartir ideas se desarrolla mediante la publicación de artículos de distintas
        fuentes por parte de nuestros miembros, con el objetivo de nutrir continuamente el conocimiento
        de los lectores, establecer lazos en una comunidad amigable y fomentar la creación de nuevos
        conocimientos y aportes mediante las relaciones entre cada integrante.
    </p></li>
    <li><p>
        ¡Bienvenidos a nuestra comunidad!
    </p></li>
    </ul>
    <img id="coffee" src="Imagenes/Sitio/coffeecup.png"/>

</asp:content>