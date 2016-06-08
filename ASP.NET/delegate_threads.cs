 #region delegate threads for RunWorkerAsync

delegate void updateDelegate(string val, Color color);
delegate void updateScroll();
delegate void updateText(string val);

private void UpdateTextColor(string updateVal, Color color)
{
    if (txtPanel.InvokeRequired)
    {
        txtPanel.Invoke(new updateDelegate(UpdateTextColor), updateVal, color);
    }
    else
    {
        txtPanel.Select(txtPanel.TextLength, 0);
        txtPanel.SelectionColor = color;
        txtPanel.AppendText(updateVal);
    }
}

private void scrollAutomatico() {
    if (txtPanel.InvokeRequired)
    {
        txtPanel.Invoke(new updateScroll(scrollAutomatico));
    }
    else
    {
        txtPanel.SelectionStart = txtPanel.Text.Length;
        // scroll it automatically
        txtPanel.ScrollToCaret();
    }
}
private void UpdateTextInput(string updateVal)
{
    if (txtRutadestino.InvokeRequired)
    {
        txtRutadestino.Invoke(new updateText(UpdateTextInput), updateVal);
    }
    else
    {
        txtRutadestino.Text = updateVal;
    }
}
#endregion