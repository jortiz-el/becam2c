/*
* ServicedComponents / operaciones / ServicioICARO.cs
 */
[AcaServiceMethod]
[AutoComplete]
public MethodRefResult<List<DiagnosticoProhibido>> BuscarDiagnosticoProhibidoes(long? idRealizador, string tipoOperacion, long? tipoAutorizacion, long? subtipoAutorizacion, long? codEspecialidad, long? codActo, string descEspecialidad, string descActo)
{
    return new GestionDiagnosticosProhibidos().BuscarDiagnosticoProhibidoes(idRealizador, tipoOperacion, tipoAutorizacion, subtipoAutorizacion, codEspecialidad, codActo, descEspecialidad, descActo);
}


/*
* ServiceLogic / Gestion / GestionGeneralICARO
 */
#region buscar diagnostico prohibido

        public MethodRefResult<List<DiagnosticoProhibido>> BuscarDiagnosticosProhibidos(long? idRealizador, string tipoOperacion, long? tipoAutorizacion, long? subtipoAutorizacion, long? codEspecialidad, long? codActo, string descEspecialidad, string descActo)
        {
            List<DiagnosticoProhibido> listaDiagnosticoProhibido = new List<DiagnosticoProhibido>();

            DbCommand objCommand;

            try
            {
                objCommand = builder.BuscarDiagnosticosProhibidos( idRealizador,  tipoOperacion,  tipoAutorizacion,  subtipoAutorizacion,  codEspecialidad,  codActo,  descEspecialidad,  descActo);
                /*
                * DatabaseHelper.GetEntitiesList<DiagnosticoProhibido>
                * Clase para Mapear directamente desde el objeto DiagnosticoProhibido [MapFrom("OPEDIA_PK")]
                */
                listaDiagnosticoProhibido = DatabaseHelper.GetEntitiesList<DiagnosticoProhibido>(Conexiones.BDAlhambra, objCommand);
            }
            catch (Exception ex)
            {
                MethodRefResult<List<DiagnosticoProhibido>>.Error(ex.Message);
            }

            return MethodRefResult<List<DiagnosticoProhibido>>.OK(listaDiagnosticoProhibido);
        
        }

        #endregion


/*
* CmdBuilder / Repositorio / DiagnosticosProhibidosCommandBuilder
 */
#region buscar DiagnosticoProhibido

public System.Data.Common.DbCommand BuscarDiagnosticosProhibidos(long? idRealizador, string tipoOperacion, long? tipoAutorizacion, long? subtipoAutorizacion, long? codEspecialidad, long? codActo, string descEspecialidad, string descActo)
{
    bool flag = false;
    string whereSql = string.Empty;

    tipoOperacion = tipoOperacion.Trim();
   
    if (idRealizador.HasValue){
        whereSql += " REAPRO_PK ='" + idRealizador + "'";
        flag = true;
    }
    if (!string.IsNullOrEmpty(tipoOperacion))
    {
        if (flag)
        {
            whereSql += tipoOperacion.EndsWith("*")? " and UPPER(OPEDIA_TP_TIPO) like UPPER('" + tipoOperacion.Replace("*", "%") + "')" : " and UPPER(OPEDIA_TP_TIPO) = UPPER('" + tipoOperacion + "')";
        }
        else
        {
            whereSql += tipoOperacion.EndsWith("*") ? " UPPER(OPEDIA_TP_TIPO) like UPPER('" + tipoOperacion.Replace("*", "%") + "')" : " UPPER(OPEDIA_TP_TIPO) = UPPER('" + tipoOperacion + "')";
            flag = true;
        }
    }
    if (tipoAutorizacion.HasValue)
    {
        if (flag)
        {
         whereSql += " and OPEDIA_TP_AUTORIZACION ='"+tipoAutorizacion+"'";
        }else
        {
         whereSql += " OPEDIA_TP_AUTORIZACION ='"+tipoAutorizacion+"'";
            flag = true;
        }
    }
    if (subtipoAutorizacion.HasValue)
    {
         if (flag)
        {
         whereSql +=  " and OPEDIA_TP_SUBAUTORIZACION ='" + subtipoAutorizacion + "'";
        }else
        {
         whereSql += " OPEDIA_TP_SUBAUTORIZACION ='" + subtipoAutorizacion + "'";
             flag = true;
        }
    }
    if (codEspecialidad.HasValue)
    {
        if (flag)
        {
            whereSql += " and OPEDIA_TP_ESPECIALIDAD ='" + codEspecialidad + "'";
        }
        else
        {
            whereSql += " OPEDIA_TP_ESPECIALIDAD ='" + codEspecialidad + "'";
            flag = true;
        }
    }
    if (codActo.HasValue)
    {
        if (flag)
        {
            whereSql += " and OPEDIA_TP_ACTO ='" + codActo + "'";
        }
        else
        {
            whereSql += " OPEDIA_TP_ACTO ='" + codActo + "'";
            flag = true;
        }
    }

    if (!string.IsNullOrEmpty(descEspecialidad))
    {
        if (flag)
        {
            whereSql += descEspecialidad.EndsWith("*") ? " and UPPER(ESPECI_DS_DESCRIPCION) like UPPER('" + descEspecialidad.Replace("*", "%") + "')" : " and UPPER(ESPECI_DS_DESCRIPCION) = UPPER('" + descEspecialidad + "')";
        }
        else
        {
            whereSql += descEspecialidad.EndsWith("*") ? " UPPER(ESPECI_DS_DESCRIPCION) like UPPER('" + descEspecialidad.Replace("*", "%") + "')" : " UPPER(ESPECI_DS_DESCRIPCION) = UPPER('" + descEspecialidad + "')";
            flag = true;
        }
    }
    if (!string.IsNullOrEmpty(descActo))
    {
        if (flag)
        {
            whereSql += descActo.EndsWith("*") ? " and UPPER(ACTO_DS_DESCRIPCION) like UPPER('" + descActo.Replace("*", "%") + "')" : " and UPPER(ACTO_DS_DESCRIPCION) = UPPER('" + descActo + "')";
        }
        else
        {
            whereSql += descActo.EndsWith("*") ? " UPPER(ACTO_DS_DESCRIPCION) like UPPER('" + descActo.Replace("*", "%") + "')" : " UPPER(ACTO_DS_DESCRIPCION) = UPPER('" + descActo + "')";
            flag = true;
        }
    }

    OracleCommand command = new OracleCommand(string.Format(selectBuscarDiagnosticoProhibido, whereSql));
    return command;

}

#endregion


/*
* consulta select con variable {0}
 */
private const string selectBuscarDiagnosticoProhibido = @" SELECT opedia_pk,
                                                                  REAPRO_PK,
                                                                    OPEDIA_TP_TIPO,
                                                                    OPEDIA_TP_ESPECIALIDAD,
                                                                    ESPECI_DS_DESCRIPCION,
                                                                    OPEDIA_TP_ACTO,
                                                                    ACTO_DS_DESCRIPCION,
                                                                    OPEDIA_TP_AUTORIZACION,
                                                                    DECODE (OPEDIA_TP_AUTORIZACION,
                                                                        '2616', 'Prueba medica',
                                                                        '2617', 'Ingreso',
                                                                        '2618', 'Rehabilitacion',
                                                                        '2619', 'Continuacion',
                                                                        OPEDIA_TP_AUTORIZACION)
                                                                    AS DESC_TIPOAUTORIZACION,
                                                                    OPEDIA_TP_SUBAUTORIZACION,
                                                                DECODE (OPEDIA_TP_SUBAUTORIZACION,
                                                                        '1178', 'Ambulante',
                                                                        '1180', 'Ingreso',
                                                                        '1181', 'Tecnicas especiales',
                                                                        '1179', 'Hospital de dia',
                                                                        '1138', 'Prueba',
                                                                        '1139', 'Medico ingreso',
                                                                        '1140', 'Matrona',
                                                                        '1141', 'Anestesia',
                                                                        '1142', 'Ayudante',
                                                                        '1143', 'Sanatorio',
                                                                        '1144', 'Otras pruebas',
                                                                        '1145', 'Aut. Protesis',
                                                                        '1146', 'Prueba con complementos',
                                                                        '1147', 'For-Fait',
                                                                        OPEDIA_TP_SUBAUTORIZACION)
                                                                    AS DESC_SUBTIPOAUTORIZACION
                                                            FROM ICTOPEDIA INNER JOIN NOTESPECI ON (OPEDIA_TP_ESPECIALIDAD = ESPECI_PK)
                                                                    LEFT JOIN NOTACTO ON (OPEDIA_TP_ACTO = ACTO_PK)
                                                                    WHERE {0}";