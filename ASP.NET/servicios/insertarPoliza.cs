
/*
* ServicedComponents / operaciones / SolicitudesICARO
 */
[AcaServiceMethod]
[AutoComplete]
public ClassGenerico InsertarPoliza(int num_poliza, string name_poliza, string producto, string ramo, int? tarjeta, string matricula)
{
    return new GestionGeneralICARO().InsertarPolizas(num_poliza, name_poliza, producto, ramo, tarjeta, matricula);
}


/*
* ServiceLogic / Gestion / GestionGeneralICARO
 */
#region insertar polizas

public ClassGenerico InsertarPolizas(int num_poliza, string name_poliza, string producto, string ramo, int? tarjeta, string matricula)
{
    ClassGenerico poliza = new ClassGenerico();

    try
    {
        //Se guardan los datos del cliente 
        using (DbCommand objCommand = builder.InsertarPoliza(num_poliza, name_poliza, producto, ramo, tarjeta, matricula))
        {
        	/*
        	* para recuperar el ID insertado actual
        	* usar el metodo ExecuteScalar ( no ExecuteNonQuery ) para ejecutar la consulta
        	* y obtenerlo a travez de objCommand.Parameters
        	 */

            Conexiones.BDAlhambra.ExecuteScalar(objCommand);

            //recuperacion de ID insertado actualmente
            poliza.id = objCommand.Parameters["id_Poliza"].Value.ToString();
        }
    }
    catch (Exception ex)
    {
        poliza.excepcion = ex.Message;
    }
    return poliza;
}
#endregion


/*
* CmdBuilder / Repositorio / GeneralCommandBuilder
 */
public System.Data.Common.DbCommand InsertarPoliza(int num_poliza, string name_poliza, string producto, string ramo, int? tarjeta, string matricula)
{
    OracleCommand command = new OracleCommand(insertPoliza);

    command.AddParameter("num_poliza", OracleType.Number, 15, num_poliza);
    command.AddRefParameter("name_poliza", OracleType.VarChar, 30, name_poliza);
    command.AddRefParameter("producto", OracleType.VarChar, 30, producto);
    command.AddRefParameter("ramo", OracleType.VarChar, 30, ramo);
    command.AddNullableParameter("tarjeta", OracleType.Number, 15, tarjeta);
    command.AddRefParameter("matricula", OracleType.VarChar, 30, matricula);

    // añadir OracleParameter para recuperar ID poliza solicitado en la consulta 
    OracleParameter idPoliza = new OracleParameter("id_Poliza", OracleType.Number);
    idPoliza.Size = 3;
    idPoliza.Direction = System.Data.ParameterDirection.ReturnValue;
    command.Parameters.Add(idPoliza);

    return command;

}

/*
* usar  RETURNING ID INTO :id_Poliza en el insert para recuperar el ID actual de ingreso
 */
private const string insertPoliza = @"INSERT INTO POLIZAS (ID, n_poliza, nombre, producto, ramo, tarjeta, matricula, fecha_alta)
                                                   VALUES (clientes_seq.NEXTVAL, :num_poliza, :name_poliza, :producto, :ramo, :tarjeta, :matricula , SYSDATE)
                                                   RETURNING ID INTO :id_Poliza";