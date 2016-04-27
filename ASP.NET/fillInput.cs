/*
* Rellenar inputs desde datos en servidor
*
*/
Cliente cliente = listaClientes.clientes[0];

txtname.Value = cliente.nombre;
txtsurname.Value = cliente.apellidos;
txtnif.Value = cliente.nif;
rdbsexo_H.Checked = cliente.sexo == "H" ? true : false;
rdbsexo_M.Checked = cliente.sexo == "M" ? true : false;
txtfecha_nac.Value = Convert.ToString(cliente.fecha_nacimiento);
txtemail.Value = cliente.email;
txttelefono.Value = cliente.telefono;
txtadress.Value = cliente.direccion;
//seleccionar indice dropDownList segun su value
ddlpoblacion.SelectedIndex = ddlpoblacion.Items.IndexOf(ddlpoblacion.Items.FindByValue(cliente.poblacion));
ddlprovincia.SelectedIndex = ddlprovincia.Items.IndexOf(ddlprovincia.Items.FindByValue(cliente.provincia));
txtpost_code.Value = Convert.ToString(cliente.codigo_postal) == "-2147483648" ? "" : Convert.ToString(cliente.codigo_postal);
//añadir atributo a un elemento HTML
txtnif.Attributes.Add("readonly", "readonly");