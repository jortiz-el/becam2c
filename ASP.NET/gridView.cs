/*
* Ejemplo gridView
*/

<asp:GridView ID="GridView1" runat="server" CellPadding="4" EnableModelValidation="True"
    ForeColor="#333333" GridLines="None" OnRowCommand="GridView1_OnRowCommand"
    OnRowDataBound="GridView1_RowDataBound">

<AlternatingRowStyle BackColor="White" />
<Columns>

    <asp:TemplateField>
    <ItemTemplate>
        <div id="btneliminarasociacion">
            <asp:Button ID="Button1" runat="server" CausesValidation="false" CommandName="SendMail"
            Text="Eliminar" CommandArgument='<%# Eval("id") %>' />
        </div>
    </ItemTemplate>
    </asp:TemplateField>

</Columns>
<EditRowStyle BackColor="#7C6F57" />
<FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
<HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
<PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
<RowStyle BackColor="#E3EAEB" />
<SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
</asp:GridView>


//esconder columna desde code behind si no recibe una QueryString con ID del cliente
GridView1.Columns[0].Visible = !string.IsNullOrEmpty(Page.Request.QueryString["IDCLIENTE"]);


/*
* OnRowDataBound funcion de code behind
* se ejecuta cuando se inicializa el gridView 
* usar si se rellena el gridView con datos Dinamicamente desde servidor
* e.Row.Cells[1].Visible = esconde celdas de la fila
*/
protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
{
    e.Row.Cells[1].Visible = false;

    if (e.Row.Cells[9].Text.ToString() == "01/01/0001 0:00:00")
    {
        e.Row.Cells[9].Text = "";
    }

    if (e.Row.Cells[6].Text.ToString() == "0" || e.Row.Cells[6].Text.ToString() == "-2147483648") 
    {
        e.Row.Cells[6].Text = "";
    }
}

/*
* OnRowCommand funcion de code behind
* se ejecuta solo si el control tiene CommandName="SendMail"
* en este ejemplo da de bajas polizas asociadas al cliente desde la columna del gridView
*/
protected void GridView1_OnRowCommand(object sender, GridViewCommandEventArgs e)
{
    if (e.CommandName != "SendMail") return;
    int id_poliza = Convert.ToInt32(e.CommandArgument);
    int id_cliente = Convert.ToInt32(parametroIdCliente);

    ClassBajas clientesPolizas = servicio.selectBajaClientesPolizas(id_cliente, id_poliza);

    if (string.IsNullOrEmpty(clientesPolizas.ex))
    {
        //ver si existe baja en clientes_polizas
        if (clientesPolizas.bajas > 0)
        {
            //la baja ya existe
            CustomValidator2.ErrorMessage = "Esta poliza ya ha sido dada de baja: ";
            CustomValidator2.IsValid = false;

        }
        else { 

            //la baja no existe, dar de baja relacion poliza cliente
            string excepcion = servicio.eliminarAsociacionPoliza(id_cliente, id_poliza);

            if (string.IsNullOrEmpty(excepcion))
            {

                //poliza dada de baja correctamente
                string script = @" alert('Poliza dada de baja correctamente');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alerta", script, true);
            }
            else
            {
                //error al dar de baja la poliza
                CustomValidator2.ErrorMessage = "Error al dar de baja la poliza: " + excepcion;
                CustomValidator2.IsValid = false;
            }
        }
    }
    else { 

        //error al seleccionar bajas de clientes_polizas
        CustomValidator2.ErrorMessage = "Error al verificar baja la poliza: " + clientesPolizas.ex;
        CustomValidator2.IsValid = false;
    }
}



/*
* Rellenar datos desde code behind
*/
//se guarda en una clase la lista de polizas para rellenar el gridView
ClassPolizaGenerico listaPolizas = new ClassPolizaGenerico();
listaPolizas = servicio.SeleccionarPolizas(parametroIdCliente);

//obtener los datos de la lista y pasarselos al gridview
GridView1.DataSource = listaPolizas.polizas;
//cargar la lista en el gridview
GridView1.DataBind();