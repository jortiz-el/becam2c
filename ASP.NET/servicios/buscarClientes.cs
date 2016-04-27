
/*
* ServicedComponents / operaciones / SolicitudesICARO
 */
[AcaServiceMethod]
[AutoComplete]
public ClassClienteGenerico buscarClientes(string nombre, string apellidos, string nif, string fechaNac)
{
    return new GestionGeneralICARO().buscarClientes(nombre, apellidos, nif, fechaNac);
}


/*
* ServiceLogic / Gestion / GestionGeneralICARO
 */
#region buscador 

public ClassClienteGenerico buscarClientes(string nombre, string apellidos, string nif, string fechaNac)
{
    //crear una nueva lista (coleccion) para añadir clientes
    ClassClienteGenerico resultadoBusqueda = new ClassClienteGenerico();

    /*
    * crear objeto IDataReader para leer los resultados de la consulta
    * mediante el metodo ExecuteReader recuperamos la select
    * se guardan en la clase clientes con sus atributos
     */

    DbCommand objCommand;
    IDataReader readerClientes = null;

    try
    {
        objCommand = builder.buscarCliente(nombre, apellidos, nif, fechaNac);
        readerClientes = Conexiones.BDAlhambra.ExecuteReader(objCommand);

        while (readerClientes.Read())
        {
            Cliente cliente = new Cliente();

            cliente.id = !string.IsNullOrEmpty(readerClientes["id"].ToString()) ? Int32.Parse(readerClientes["id"].ToString()): int.MinValue;
            cliente.nombre = !string.IsNullOrEmpty(readerClientes["nombre"].ToString()) ? readerClientes["nombre"].ToString() : string.Empty;
            cliente.apellidos = !string.IsNullOrEmpty(readerClientes["apellidos"].ToString()) ? readerClientes["apellidos"].ToString() : string.Empty;
            cliente.sexo = !string.IsNullOrEmpty(readerClientes["sexo"].ToString()) ? readerClientes["sexo"].ToString() : string.Empty;
            cliente.nif = !string.IsNullOrEmpty(readerClientes["nif"].ToString()) ? readerClientes["nif"].ToString() : string.Empty;
            cliente.fecha_nacimiento = !string.IsNullOrEmpty(readerClientes["fecha_nacimiento"].ToString()) ? DateTime.Parse(readerClientes["fecha_nacimiento"].ToString()) : DateTime.MinValue;
            cliente.fecha_alta = !string.IsNullOrEmpty(readerClientes["fecha_alta"].ToString()) ? DateTime.Parse(readerClientes["fecha_alta"].ToString()) : DateTime.MinValue;
            cliente.fecha_baja = !string.IsNullOrEmpty(readerClientes["fecha_baja"].ToString()) ? DateTime.Parse(readerClientes["fecha_baja"].ToString()) : DateTime.MinValue;
            cliente.email = !string.IsNullOrEmpty(readerClientes["email"].ToString()) ? readerClientes["email"].ToString() : string.Empty;
            cliente.telefono = !string.IsNullOrEmpty(readerClientes["telefono"].ToString()) ? readerClientes["telefono"].ToString() : string.Empty;
            cliente.direccion = !string.IsNullOrEmpty(readerClientes["direccion"].ToString()) ? readerClientes["direccion"].ToString() : string.Empty;
            cliente.poblacion = !string.IsNullOrEmpty(readerClientes["poblacion"].ToString()) ? readerClientes["poblacion"].ToString() : string.Empty;
            cliente.provincia = !string.IsNullOrEmpty(readerClientes["provincia"].ToString()) ? readerClientes["provincia"].ToString() : string.Empty;
            cliente.codigo_postal = !string.IsNullOrEmpty(readerClientes["codigo_postal"].ToString()) ? Int32.Parse(readerClientes["codigo_postal"].ToString()): int.MinValue;

            //se añade el cliente a la lista de clientes
            resultadoBusqueda.clientes.Add(cliente);
        }
    }
    catch (Exception ex)
    {
        resultadoBusqueda.excepcion = ex.Message;  
    }
    finally
    {
        if (readerClientes != null)
        {
            readerClientes.Close();
        }
    }
    return resultadoBusqueda;
}
#endregion


/*
* CmdBuilder / Repositorio / GeneralCommandBuilder
 */
public System.Data.Common.DbCommand buscarCliente(string nombre, string apellidos, string nif, string fechaNac)
{

/*
* se crea un flag para ver si el comienzo del string esta realizado
* se buscan resultados por varios input
* hay posibilidad de poner un * lo que hace que se pueda remplazar por % para usar la clausula like
 */

bool flag = false;
string whereSql = "";

if (nombre != "")
{
    if (nombre.EndsWith("*"))
    {
        whereSql += " UPPER(nombre) like UPPER('" + nombre.Replace("*", "%") + "')";
    }
    else
        whereSql += " UPPER(nombre) = UPPER('" + nombre + "')";
        flag = true;

}
if (apellidos != "")
{
    if (flag )
    {
        whereSql += apellidos.EndsWith("*") ? " and UPPER(apellidos) like UPPER('" + apellidos.Replace("*", "%") + "')" : " and UPPER(apellidos) = UPPER('" + apellidos + "')";
    }
    else
    {
        whereSql += apellidos.EndsWith("*") ? " UPPER(apellidos) like UPPER('" + apellidos.Replace("*", "%") + "')" : " UPPER(apellidos) = UPPER('" + apellidos + "')";
        flag = true;
    }
}
if (nif != "")
{
    if (flag )
    {
        whereSql += nif.EndsWith("*") ? " and nif like '" + nif.Replace("*", "%") + "'" : " and nif = '" + nif + "'";
    }
    else
    {
        whereSql += nif.EndsWith("*") ? " nif like '" + nif.Replace("*", "%") + "'" : " nif = '" + nif + "'";
        flag = true;
    }
}
if (fechaNac != "")
{
    if (flag )
    {
        whereSql += fechaNac.EndsWith("*") ? " and fecha_nacimiento like '" + fechaNac.Replace("*", "%") + "'" : " and fecha_nacimiento = '" + fechaNac + "'";
    }
    else
    {
        whereSql += fechaNac.EndsWith("*") ? " fecha_nacimiento like '" + fechaNac.Replace("*", "%") + "'" : " fecha_nacimiento = '" + fechaNac + "'";
        flag = true;
    }
}


OracleCommand command = new OracleCommand(string.Format(selectBuscarCliente, whereSql));
return command;
}


/*
* consulta select con variable {0}
 */
private const string selectBuscarCliente = @"SELECT id, nombre, apellidos, nif, sexo, fecha_nacimiento , fecha_alta , fecha_baja , email, telefono, direccion, poblacion, provincia, codigo_postal
                                                               FROM CLIENTES WHERE {0}";