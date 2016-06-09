/* Metodo para cargar combobox leyendo desde XML guardado en App.config */

private void cargarComboPerfiles()
{
    try
    {
        cmbAplicaciones.Items.Clear();

        Perfiles perfiles = null;

        #region generar lista de prueba
        
        //perfiles = new Perfiles();
        //icaro.rutaOrigen = "C:\\Users\\AlbizuriD\\Desktop\\AppDespliegue";
        //icaro.rutaDestino = "C:\\Users\\AlbizuriD\\Desktop\\despliegue_1";
        //icaro.rutaBackup = "C:\\Users\\AlbizuriD\\Desktop\\despliegue_2";
        //icaro.despliegueLocal = false;
        //icaro.servidorDespliegue ="VDINET94";
        //icaro.unidadDespliegue = "C";
        //icaro.impersonificar = true;
        //icaro.usuario = "joao";
        //icaro.contrasenia = "1234";
        //icaro.dominio = "grupoadeslas2k3";
        //icaro.perfil = "ICARO";

        //perfiles.perfiles.Add(perfil_vacio);
        //perfiles.perfiles.Add(icaro);

        ////metodo Serialize() para pasar un Objeto a XML
        //string serie = perfiles.Serialize();

        #endregion

        string perfilesLeidos = System.Configuration.ConfigurationManager.AppSettings["PERFILES"];

        //Deserializar XML para transformar en objeto de la clase Perfiles
        XmlSerializer serializer = new XmlSerializer(typeof(Perfiles));
        //crear un objeto xmlreader desde un string y deserializar 
        XmlReader xmlReader = XmlReader.Create(new StringReader(perfilesLeidos));
        perfiles = serializer.Deserialize(xmlReader) as Perfiles;

        cmbAplicaciones.DataSource = perfiles.perfiles;
    }
    catch (Exception ex)
    { }
}


////////////////////////////////
/////////////////////////////// App.config
//////////////////////////////


<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5" />
    </startup>

<appSettings>
    <add key="PERFILES" value="
&lt;Perfiles&gt;
  &lt;perfiles&gt;
    &lt;Perfil&gt;
      &lt;despliegueLocal&gt;false&lt;/despliegueLocal&gt;
      &lt;reiniciarIIS&gt;false&lt;/reiniciarIIS&gt;
      &lt;impersonificar&gt;false&lt;/impersonificar&gt;
    &lt;/Perfil&gt;
    &lt;Perfil&gt;
      &lt;perfil&gt;ICARO&lt;/perfil&gt;
      &lt;rutaOrigen&gt;C:\Users\AlbizuriD\Desktop\AppDespliegue&lt;/rutaOrigen&gt;
      &lt;rutaDestino&gt;C:\Users\AlbizuriD\Desktop\despliegue_1&lt;/rutaDestino&gt;
      &lt;rutaBackup&gt;C:\Users\AlbizuriD\Desktop\despliegue_2&lt;/rutaBackup&gt;
      &lt;despliegueLocal&gt;false&lt;/despliegueLocal&gt;
      &lt;reiniciarIIS&gt;false&lt;/reiniciarIIS&gt;
      &lt;servidorDespliegue&gt;VDINET94&lt;/servidorDespliegue&gt;
      &lt;unidadDespliegue&gt;C&lt;/unidadDespliegue&gt;
      &lt;impersonificar&gt;true&lt;/impersonificar&gt;
      &lt;usuario&gt;joao&lt;/usuario&gt;
      &lt;contrasenia&gt;1234&lt;/contrasenia&gt;
      &lt;dominio&gt;grupoadeslas2k3&lt;/dominio&gt;
    &lt;/Perfil&gt; 
    &lt;Perfil&gt;
      &lt;perfil&gt;ICARO INT&lt;/perfil&gt;
      &lt;rutaOrigen&gt;C:\Users\AlbizuriD\Desktop\AppDespliegue&lt;/rutaOrigen&gt;
      &lt;rutaDestino&gt;C:\Users\AlbizuriD\Desktop\despliegue_1&lt;/rutaDestino&gt;
      &lt;rutaBackup&gt;C:\Users\AlbizuriD\Desktop\despliegue_2&lt;/rutaBackup&gt;
      &lt;despliegueLocal&gt;false&lt;/despliegueLocal&gt;
      &lt;reiniciarIIS&gt;false&lt;/reiniciarIIS&gt;
      &lt;servidorDespliegue&gt;VDINET94&lt;/servidorDespliegue&gt;
      &lt;unidadDespliegue&gt;C&lt;/unidadDespliegue&gt;
      &lt;impersonificar&gt;true&lt;/impersonificar&gt;
      &lt;usuario&gt;joao&lt;/usuario&gt;
      &lt;contrasenia&gt;1234&lt;/contrasenia&gt;
      &lt;dominio&gt;grupoadeslas2k3&lt;/dominio&gt;
    &lt;/Perfil&gt;
  &lt;/perfiles&gt;
&lt;/Perfiles&gt;"/>
</appSettings>


</configuration>