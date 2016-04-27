/*
* Redirigir los usuarios a otra página
*
*/

//desde codebehind
Response.Redirect("Buscadorpoliza.aspx?IDCLIENTE=" + parametroIdCliente, true);

//desde enlace HTML
<a href="Inicio.aspx"> Alta nuevo cliente</a>

//desde boton ASP
<asp:Button 
  ID="Button1" 
  PostBackUrl="~/TargetPage.aspx"
  runat="server"
  Text="Submit" />