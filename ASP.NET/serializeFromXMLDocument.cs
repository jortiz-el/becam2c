/* Metodo para cargar combobox leyendo desde fichero XML guardado proyecto*/

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

        //Deserializar XML para transformar en objeto de la clase Perfiles
        XmlSerializer serializer = new XmlSerializer(typeof(Perfiles));
        //crear un objeto xmlreader desde un fichero xml
        FileStream perfilesLeidos = new FileStream("config.xml", FileMode.Open);
        XmlReader xmlReader = XmlReader.Create(perfilesLeidos);
        perfiles = serializer.Deserialize(xmlReader) as Perfiles;

        cmbAplicaciones.DataSource = perfiles.perfiles;
    }
    catch (Exception ex)
    { }
}

