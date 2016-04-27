//funcion estatica para llamar desde cualquier parte en el proyecto
public static void resgistrarError(string msjExcepcion, Page page)
{
    string script = @" alert('" + msjExcepcion.Replace("'", "").Replace("\n", "") + "');";
    ScriptManager.RegisterStartupScript(page, page.GetType(), "alerta", script, true);
}

//alerta javascript desde code behind
string script = @" alert('Cliente insertado correctamente ');";
ScriptManager.RegisterStartupScript(this, GetType(), "alerta", script, true);
limpiarFormulario();