/*
* control de errores y listado de los mismos con validationSummary ASP
*/

/*
* ForeColor= color de letras
* DisplayMode= modo de despliegue al mostrar mensajes (BulletList|List|SingleParagraph)
* ValidationGroup= el mismo grupo de validacion para todos los controles validator para que se muestren
*/
<asp:ValidationSummary ID="ValidationSummary1" class="rojo" runat="server" ForeColor="white" DisplayMode="List"
	HeaderText="ERRORES DE VALIDACION:" ValidationGroup="vgErrores" />

// validator customizable	
<asp:CustomValidator ID="CustomValidator2" runat="server" Display="None" ValidationGroup="vgErrores"
	></asp:CustomValidator>
// setear el CustomValidator desde codebehind	
CustomValidator2.ErrorMessage = "Error el nif de cliente ya esta registrado";
CustomValidator2.IsValid = false;

/*
* validator required
* controlToValidate= ID del control a validar
* ErrorMessage= Mensaje de error que se muestra en el validationSummary
* Display= modo de despliegue (static|Dynamic|none)
*/
<asp:RequiredFieldValidator ID="RequiredFieldValidator1"
    runat="server" ErrorMessage="- Nombre es obligatorio" 
    controlToValidate="txtname" ForeColor="Red" Display="Dynamic" 
    ValidationGroup="vgErrores"
    >*</asp:RequiredFieldValidator>

// RegularExpressionValidator validador de expreciones RegExp   
<asp:RegularExpressionValidator ID="RegularExpressionValidator1"
    runat="server" ErrorMessage="- Nombre no puede ser numerico" 
    ControlToValidate="txtname" ForeColor="Red" Display="Dynamic" ValidationExpression="^[a-zA-Z_áéíóúñ\s]*$"
    ValidationGroup="vgErrores"
    >*</asp:RegularExpressionValidator>

/*
* CustomValidator con funcion JavaScript
* ClientValidationFunction= funcion en cliente a llamar javascript
*/
<asp:CustomValidator ID="CustomValidator1" 
	runat="server" ErrorMessage="- Fecha de nacimiento no tiene formato (dd/mm/aaaa)"
	ControlToValidate="txtfecha_nac"
	ClientValidationFunction="validDateASP" ForeColor="Red"
	ValidationGroup="vgErrores"
>*</asp:CustomValidator>
/*
* funcion JavaScript llamada desde CustomValidator
*/
function validDateASP(source, args) {
    var id = $("<%= txtfecha_nac.ClientID %>");
    var valid = validaFechaDDMMAAAA(id.value);
    id.setCustomValidity((valid || (!(id.attributes.required) && id.value === "")) ? "" : "Invalid field.");
    args.IsValid = valid;
}

/*
* CustomValidator con funcion en servidor code behind
*/
<asp:CustomValidator ID="ddlValidator" runat="server" Text="" 
    onservervalidate="ddlValidator_ServerValidate" />
    
 protected void ddlValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (myDDL1.SelectedItem.Text = "Blue")
            if (myDDL2.SelectedItem.Text = "")
            {
                args.IsValid = false;
                ddlValidator.Text = "Please select a value.";
            }
            else
                args.IsValid = true;
    }