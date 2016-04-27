
/*
* ServicedComponents / operaciones / SolicitudesICARO
 */
[AcaServiceMethod]
[AutoComplete]
public string modificarCliente(int id, string nombre, string apellidos, string nif, string sexo, DateTime fecha, string mail, string telefono, string direccion, string poblacion, string provincia, int? codigo_postal)
{
    return new GestionGeneralICARO().modificarCliente(id, nombre, apellidos, nif, sexo, fecha, mail, telefono, direccion, poblacion, provincia, codigo_postal);

}


/*
* ServiceLogic / Gestion / GestionGeneralICARO
 */
#region modificar cliente

public string modificarCliente(int id, string nombre, string apellidos, string nif, string sexo, DateTime fecha, string mail, string telefono, string direccion, string poblacion, string provincia, int? codigo_postal)
{
    string cli = string.Empty;
    
    try
    {
        using (DbCommand objCommand = builder.modificarCliente(id, nombre, apellidos, nif, sexo, fecha, mail, telefono, direccion, poblacion, provincia, codigo_postal))
        {
            Conexiones.BDAlhambra.ExecuteNonQuery(objCommand);
        }

    }
    catch (Exception ex)
    {
        cli = ex.Message;
    }

    return cli;

}
#endregion


/*
* CmdBuilder / Repositorio / GeneralCommandBuilder
 */
public System.Data.Common.DbCommand modificarCliente(int id, string nombre, string apellidos, string nif, string sexo, DateTime fecha, string mail, string telefono, string direccion, string poblacion, string provincia, int? codigo_postal)
{
    //Constructor
    OracleCommand command = new OracleCommand(updateModificarCliente);

    //Parametros Update
    command.AddParameter("id", OracleType.Number, 15, id);
    command.AddRefParameter("nombre", OracleType.VarChar, 30, nombre);
    command.AddRefParameter("apellidos", OracleType.VarChar, 60, apellidos);
    command.AddRefParameter("nif", OracleType.VarChar, 30, nif);
    command.AddRefParameter("sexo", OracleType.VarChar, 1, sexo);
    command.AddParameter("fecha_nac", OracleType.DateTime, fecha);
    command.AddRefParameter("email", OracleType.VarChar, 30, mail);
    command.AddRefParameter("telefono", OracleType.VarChar, 30, telefono);
    command.AddRefParameter("direccion", OracleType.VarChar, 30, direccion);
    command.AddRefParameter("poblacion", OracleType.VarChar, 30, poblacion);
    command.AddRefParameter("provincia", OracleType.VarChar, 30, provincia);
    //command.AddRefParameter("domicilio", OracleType.VarChar, 30, domi);
    command.AddNullableParameter("cod", OracleType.Number, 15, codigo_postal);

    return command;

}


/*
* consulta update
 */
private const string updateModificarPoliza = @"UPDATE POLIZAS                                                                   
                                                SET 
                                                    n_poliza = :n_poliza,
                                                    nombre = :nombre,
                                                    producto = :producto,
                                                    ramo = :ramo,
                                                    tarjeta = :tarjeta,
                                                    matricula = :matricula
                                                WHERE id = :id";