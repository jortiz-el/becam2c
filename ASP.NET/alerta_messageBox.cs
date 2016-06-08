 private void Alerta(string mensaje) {
    MessageBox.Show( mensaje, "Caption",
    MessageBoxButtons.OK, MessageBoxIcon.Question,
    MessageBoxDefaultButton.Button1,
    MessageBoxOptions.RtlReading
    | MessageBoxOptions.RightAlign);
}