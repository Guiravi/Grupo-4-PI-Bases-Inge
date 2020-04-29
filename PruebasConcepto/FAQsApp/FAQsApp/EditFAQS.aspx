<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditFAQS.aspx.cs" Inherits="EditFAQS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<style type="text/css">
		.auto-style1 {
			width: 100%;
		}
		.auto-style2 {
			width: 141px;
		}
	</style>
</head>
<body>
    <form id="form1" runat="server">
			<table class="auto-style1">
				<tr>
                    <!--Se pregunta por el número de prioridad de la nueva pregunta y respuesta al miembro-->
                    <td class="auto-style2">Inserte el numero de prioridad</td>
					<td>
                        <!--la prioridad se guarda en opcionNueva para utilizarla en C#-->
                        <asp:TextBox ID="opcionNueva" runat="server"></asp:TextBox>             	
                    </td>
				</tr>
                <tr>
					<!--Se pregunta por la nueva pregunta al miembro-->
                    <td class="auto-style2">Nueva pregunta </td>
					<td>
        	            <!--la nueva pregunta se guarda en txtNuevaPregunta para utilizarla en C#-->
                        <asp:TextBox ID="txtNuevaPregunta" runat="server"></asp:TextBox>
        			</td>
				</tr>
				<tr>
					<!--Se pregunta por la nueva respuesta al miembro-->
                    <td class="auto-style2">Nueva respuesta</td>
					<td>
        	            <!--la nueva respuesta se guarda en txtNuevaRespuesta para utilizarla en C#-->
                        <asp:TextBox ID="txtNuevaRespuesta" runat="server"></asp:TextBox>
        			</td>
				</tr>
				<tr>
					<td class="auto-style2">&nbsp;</td>
					<td>
        	<asp:Button ID="btnAgregar" runat="server" Text="Modificar" OnClick="btnAgregar_Click"/>
					&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
				</tr>
			</table>
    		<p>
			<asp:Label ID="lblFAQsHTML" runat="server" Text=""></asp:Label>
        </p>
    </form>
</body>
</html>
