/*backgroundWorker*/

private void btnDesplegar_Click(object sender, EventArgs e)
        {
            
            rutaOrigen = txtRutaOrigen.Text;
            rutaDestino = txtRutadestino.Text;
            rutaBackup = txtRutaBackup.Text;
            servidorDespliegue = txtServidorDespliegue.Text;
            unidadDespliegue = Convert.ToString(cmbUnidadDespliegue.SelectedItem);
            usuario = txtUsuario.Text;
            password = txtContraseña.Text;
            dominio = Convert.ToString(cmbDominio.SelectedItem);
            chkimpersonificar = chkbImpersonificar.Checked;

            //reinicio los controles
            progressBar1.Style = ProgressBarStyle.Marquee;
            txtPanel.Text = string.Empty;
            
            if (labelDesplegar.Text == "Desplegar") {

                labelDesplegar.Text = "Desplegando";
                progressBar1.Value = 0;
                progressBar1.Visible = true;

                // Start the BackgroundWorker.
                backgroundWorker1.RunWorkerAsync();
            }
        }


private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
{
    //reportar el progreso del backgroundworker
    backgroundWorker1.ReportProgress(1000);

    if (chkimpersonificar)
    {
        //llamada a impersonificar
        impersonificar();
    }
    else {
    	//funcion a desplegar en modo asincrono
        setDespliegue();
    }
    
}

private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
{
    //añadir barra de carga el progreso del background worker
    if (e.ProgressPercentage == 1000)
    {
        progressBar1.Style = ProgressBarStyle.Marquee;
    }
    else
    {
        // progreso continuo cuando sabemos cuanto va tardar el progreso, se puede ver en funcion copyfiles
        progressBar1.Style = ProgressBarStyle.Continuous;
        progressBar1.Value = e.ProgressPercentage;
    }
    
}

private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
{
	// codigo a ejecutar cuando finaliza la llamada asincrona

    labelDesplegar.Text = "Desplegar";
    //progressBar1.Style = ProgressBarStyle.Blocks;
    progressBar1.Visible = false;
}

 private void copyFiles(string rutaOrigen, string rutaDestino) {

            string fileName = string.Empty;
            string destFile = string.Empty;
            string dirName = string.Empty;
            string destDir = string.Empty;
            string origDir = string.Empty;
            string[] files = System.IO.Directory.GetFiles(rutaOrigen);
            string[] dir = System.IO.Directory.GetDirectories(rutaOrigen);

            //entrar en las carpetas recursivamente
            for (int i = 0; i < dir.Length; i++) {
                backgroundWorker1.ReportProgress(((i + 1) * 100) / dir.Length);

                dirName = System.IO.Path.GetFileName(dir[i]);
                destDir = System.IO.Path.Combine(rutaDestino, dirName);
                origDir = System.IO.Path.Combine(rutaOrigen, dirName);
                if (!System.IO.Directory.Exists(destDir))
                {
                    try
                    {
                        System.IO.Directory.CreateDirectory(destDir);
                        UpdateTextColor("**Creando Dir..." + destDir + Environment.NewLine, Color.White);
                    }
                    catch (Exception ex)
                    {
                        Alerta("Error al crear directorio");
                    }
                }
                scrollAutomatico();
                copyFiles(origDir, destDir);
            }

            // Copy the files and overwrite destination files if they already exist.
            for (int i = 0; i < files.Length; i++) {

               	//reportar progreso para barra de carga (progressBar1.Style = ProgressBarStyle.Continuous)
                backgroundWorker1.ReportProgress(((i + 1) * 100) / files.Length);
                fileName = System.IO.Path.GetFileName(files[i]);
                destFile = System.IO.Path.Combine(rutaDestino, fileName);
                System.IO.File.Copy(files[i], destFile, true);

                UpdateTextColor("[", Color.White);
                UpdateTextColor("Copiando...", Color.Green);
                UpdateTextColor("] " + files[i] + Environment.NewLine, Color.White);
                scrollAutomatico();
            }

        }