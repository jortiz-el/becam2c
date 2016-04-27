
//funcion estatica para llamar desde cualquier parte en el proyecto

public static void resgistrarError(string msjExcepcion, Page page)
{
    string script = @" alert('" + msjExcepcion.Replace("'", "").Replace("\n", "") + "');";
    ScriptManager.RegisterStartupScript(page, page.GetType(), "alerta", script, true);
}