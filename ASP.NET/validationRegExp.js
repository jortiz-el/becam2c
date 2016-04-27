
var NAME = /^[A-Za-z\_\-\.\s\xF1\xD1]+$/,
    EMAIL = /^[a-z0-9._%+\-]+@[a-z0-9.\-]+\.[a-z]{2,4}$/,
    FONE = /^(8|9)[0-9]{8}$/,
    MOVIL = /^(6|7)[0-9]{8}$/,
    POSTCODE = /^([0-4][0-9]{4}|[5][0-2][0-9]{3})$/,
    NIFNIE = /^((([X-Z]{1})([-]?)(\d{7})([-]?)([A-Z]{1}))|((\d{8})([-]?)([A-Z]{1}))|(([ABEH][0-9]{8})|([KPQS][0-9]{7}[A-J])|([CDFGJLMNRUVW][0-9]{7}[0-9A-J])))$/,
    NUMPOLIZA = /^[0-9]{0,9}$/,
    NUMBERPLATE = /^(([A-Z]{1,2}\-\d{4}\-([A-N]|[O-Z]){1,2})|(\d{4}\-([A-N]|[O-Z]){3}))$/i;

function $(id) {
    "use strict";
    return document.getElementById(id);
}

function validInput(patt, id) {
    "use strict";
    var valid = patt.test(id.value);
    id.setCustomValidity((valid || (!(id.attributes.required) && id.value === "")) ? "" : "Invalid field.");
}
function validPhone(patt1, patt2, id) {
    "use strict";
    var valid = patt1.test(id.value);
    var valid2 = patt2.test(id.value);
    id.setCustomValidity((valid || valid2 || (!(id.attributes.required) && id.value === "")) ? "" : "Invalid field.");
}


//eventos de escucha en tienmpo real de inputs
function events() {
            "use strict";
            $("<%= txtname.ClientID %>").addEventListener("keyup", function () { validInput(NAME, $("<%= txtname.ClientID %>")); }, false);
            $("<%= txtsurname.ClientID %>").addEventListener("keyup", function () { validInput(NAME, $("<%= txtsurname.ClientID %>")); }, false);
            $("<%= txtemail.ClientID %>").addEventListener("keyup", function () { validInput(EMAIL, $("<%= txtemail.ClientID %>")); }, false);
            $("<%= txttelefono.ClientID %>").addEventListener("keyup", function () { validPhone(FONE, MOVIL, $("<%= txttelefono.ClientID %>")); }, false);
            $("<%= txtpost_code.ClientID %>").addEventListener("keyup", function () { validInput(POSTCODE, $("<%= txtpost_code.ClientID %>")); }, false);
            $("<%= txtnif.ClientID %>").addEventListener("keyup", function () { validInput(NIFNIE, $("<%= txtnif.ClientID %>")); }, false);
            $("<%= txtfecha_nac.ClientID %>").addEventListener("keyup", function () { validDate($("<%= txtfecha_nac.ClientID  %>")); }, false);

            //validacion buscador

            //$("country").addEventListener("change", country, false);


        }

window.onload = events;



function validDate(id) {

    var valid = validaFechaDDMMAAAA(id.value);
    id.setCustomValidity((valid || (!(id.attributes.required) && id.value === "")) ? "" : "Invalid field.");

}

/*
Function validaFechaDDMMAAAA

Devuelve true si la fecha recibida es correcta y false si no es correcta

Parametros: 
- fecha: string con la fecha en formato dd/mm/yyyy
*/
function validaFechaDDMMAAAA(fecha) {
    var dtChBag = "/ :";
    var dtCh = "/";
    var dtCh2 = ":";

    var minYear = 1900;
    var maxYear = 2100;
    function isInteger(s) {
        var i;
        for (i = 0; i < s.length; i++) {
            var c = s.charAt(i);
            if (((c < "0") || (c > "9"))) return false;
        }
        return true;
    }

    function stripCharsInBag(s, bag) {
        var i;
        var returnString = "";
        for (i = 0; i < s.length; i++) {
            var c = s.charAt(i);
            if (bag.indexOf(c) == -1) returnString += c;
        }
        return returnString;
    }

    function daysInFebruary(year) {
        return (((year % 4 == 0) && ((!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28);
    }

    function DaysArray(n) {
        for (var i = 1; i <= n; i++) {
            this[i] = 31
            if (i == 4 || i == 6 || i == 9 || i == 11) { this[i] = 30 }
            if (i == 2) { this[i] = 29 }
        }
        return this
    }

    function isDate(dtStr) {
        var daysInMonth = DaysArray(12)
        var pos1 = dtStr.indexOf(dtCh)
        var pos2 = dtStr.indexOf(dtCh, pos1 + 1)
        var strDay = dtStr.substring(0, pos1)
        var strMonth = dtStr.substring(pos1 + 1, pos2)
        var strYear
        var strHour
        var strMinutes;
        var strSeconds
        if (dtStr.indexOf(dtCh2) < 0) {
            strYear = dtStr.substring(pos2 + 1)
        }
        else {
            strYear = dtStr.substring(pos2 + 1, dtStr.indexOf(" "))
            strHour = dtStr.substring(dtStr.indexOf(" ") + 1, dtStr.indexOf(dtCh2))

            if (dtStr.indexOf(dtCh2, dtStr.indexOf(dtCh2) + 1) < 0) {
                strMinutes = dtStr.substring(dtStr.indexOf(dtCh2) + 1)
            }
            else {
                strMinutes = dtStr.substring(dtStr.indexOf(dtCh2) + 1, dtStr.indexOf(dtCh2, dtStr.indexOf(dtCh2) + 1))
                strSeconds = dtStr.substring(dtStr.indexOf(dtCh2, dtStr.indexOf(dtCh2) + 1) + 1)
            }
        }

        if (strMonth.length == 1 || strDay.length == 1)
            return false

        strYr = strYear
        if (strDay.charAt(0) == "0" && strDay.length > 1) strDay = strDay.substring(1)
        if (strMonth.charAt(0) == "0" && strMonth.length > 1) strMonth = strMonth.substring(1)
        for (var i = 1; i <= 3; i++) {
            if (strYr.charAt(0) == "0" && strYr.length > 1) strYr = strYr.substring(1)
        }



        month = parseInt(strMonth)
        day = parseInt(strDay)
        year = parseInt(strYr)
        if (pos1 == -1 || pos2 == -1) {
            return false
        }
        if (strMonth.length < 1 || month < 1 || month > 12) {
            return false
        }
        if (strDay.length < 1 || day < 1 || day > 31 || (month == 2 && day > daysInFebruary(year)) || day > daysInMonth[month]) {
            return false
        }
        if (strYear.length != 4 || year == 0 || year < minYear || year > maxYear) {
            return false
        }
        if (dtStr.indexOf(dtCh, pos2 + 1) != -1 || isInteger(stripCharsInBag(dtStr, dtChBag)) == false) {
            return false
        }

        if (strHour > 23 || strMinutes > 59 || strSeconds > 59 || isInteger(stripCharsInBag(dtStr, dtChBag)) == false) {
            return false
        }

        return true
    }

    if (isDate(fecha)) {
        return true;
    } else {
        return false;
    }
}


