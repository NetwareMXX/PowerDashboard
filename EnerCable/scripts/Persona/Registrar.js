var myWindow;
$(document).ready(function () {
    myWindow = $("#window");
    myWindow.kendoWindow({
        width: "600px",
        title: "Cargos",
        visible: false,
        actions: [
            "Pin",
            "Minimize",
            "Maximize",
            "Close"
        ],
        close: onClose
    });//.data("kendoWindow").center().open();

});
function onClose(e) {
    $("#hdIdPersona").val("0");
}
datosCaptura = {
    hdIdPersona: "0",
    txtNombre: "",
    txtPaterno: "",
    txtMaterno: ""

};
datosGrid = {
    Nombre: { type: "string" },
    Paterno: { type: "string" },
    Materno: { type: "string" },
    Estatus: { type: "string" },
    IdServicio: { type: "string" }
};
_accionGrid = "/Persona/ObtenerPersonas";
_accion = "/Persona/agregarPersonas/";

datosInsercion = [
        { control: "txtNombre", requerido: 1, nombre: "Nombre de la Persona ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Nombre" },
        { control: "txtPaterno", requerido: 1, nombre: "Apellido Paterno de la Persona ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Paterno" },
        { control: "txtMaterno", requerido: 0, nombre: "Apellido Materno de la Persona ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Materno" },
        { control: "hdIdPersona", requerido: 0, nombre: "Id de la Persona ", defaultVal: "0", tipoControl: "Texto", columnaGrid: "IdPersona" },
        { control: "ddlEstatus", requerido: 0, nombre: "Estatus del Servicio ", defaultVal: "1", tipoControl: "Combo", columnaGrid: "IdEstatus" }



];
var _persona = new Object();
datosAccion = {
    "persona": _persona
};
columnasGrid = [
    {
        command: [{ iconClass: "k-icon k-edit", name: "c", text: "", click: rowclick, title: "Editar", width: "30px" },
            { iconClass: "k-icon k-i-lock", name: "d", text: "", click: mostrarVentana, title: "Cargos", width: "30px" }
        ]
    },

                 {
                     field: "Nombre",
                     title: "Nombre"

                 },

                 {
                     field: "Paterno",
                     title: "Paterno"
                 },
                 {
                     field: "IdPersona",
                     title: "IdPersona", hidden: true
                 },
                 {
                     field: "Materno",
                     title: "Materno"
                 },
                 {
                     field: "Estatus",
                     title: "Estatus"
                 },
                 {
                     field: "IdEstatus",
                     title: "IdEstatus", hidden: true
                 }

];

function recuperarDatos() {
    _persona.IdPersona = $("#hdIdPersona").val();
    _persona.Nombre = $("#txtNombre").val();
    _persona.Paterno = $("#txtPaterno").val();
    _persona.Materno = $("#txtMaterno").val();
    _persona.IdEstatus = $("#ddlEstatus").val();


}
function llenarDatos(dataItem) {
    $("#hdIdPersona").val(dataItem.IdPersona);
    $("#txtNombre").val(dataItem.Nombre);
    $("#txtPaterno").val(dataItem.Paterno);
    $("#txtMaterno").val(dataItem.Materno);
    $("#ddlEstatus").selectpicker("val", dataItem.IdEstatus);

}
function mostrarVentana(e) {
    e.preventDefault();
    var grid = $("#miGrid").data("kendoGrid");
    grid.clearSelection();
    $(e.currentTarget).closest("tr").addClass('k-state-selected');
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    if (dataItem) {
        $("#hdIdPersona").val(dataItem.IdPersona);
        loadGridMenu(dataItem.IdPersona,dataItem.Nombre+" "+dataItem.Paterno+" "+dataItem.Materno);
    }

}
function loadGridMenu(idpersona,persona) {


    var parametros = {};
    parametros["IdPersona"] = 0;
    $.ajax({
        type: 'GET',
        url: _encabezado + "/Persona/obtenerCargosPorPersona?IdPersona=" + idpersona,
        dataType: 'json',
        beforeSend: function () { $("#tablaMenu").html(""); },
        data: '{IdPersona:' + 0 + '}',
        contentType: 'application/json; charset=utf-8',
        success: function (msg) {
            $("#tablaMenu").html(msg);
            var window = myWindow.data("kendoWindow");

            window.element.prev().find(".k-window-title").html("<b class='custom'>Cargos para " + persona + "</b>");

            $("#window").data("kendoWindow").center().open();

        },
        error: function (x, status, error) {
            alert("Ocurrio un Error: " + status + "nError: " + x.responseText);
        }, async: false


    });
}
function checkTodo(ch) {
    var c = ch.checked;
    $('.chkPer').prop('checked', c);
}
function recuperarMenu() {

    var _resultado = "";
    $('.chkPer').each(function (index, obj) {

        if (this.checked === true) {
            var _X = (this.id);

            _resultado += $("#" + _X).attr("valor") + ",";
        }
    });
    _resultado += "x";
    _resultado = _resultado.replace(",x", "");
    if (_resultado == "x") _resultado = "";
    return _resultado;
}
function guardarPermiso() {

    var _menus = recuperarMenu();

    $.ajax({
        type: 'POST',
        url: _encabezado + "/Persona/agregarCargos",
        dataType: 'json',
        beforeSend: function () { $("#btnGuardarPermisos").attr("disabled", "disabled"); },
        data: '{cargos:"' + _menus + '",idpersona:' + $("#hdIdPersona").val() + '}',
        contentType: 'application/json; charset=utf-8',
        success: function (msg) {
            $("#btnGuardarPermisos").removeAttr("disabled");
            $("#window").data("kendoWindow").close();
            $("#miGrid").data("kendoGrid").dataSource.read();
            $("#hdIdPersona").val("0");
        },
        error: function (x, status, error) {
            $("#hdIdPersona").val("0");
            alert("Ocurrio un Error: " + status + "nError: " + x.responseText);
        }, async: false
    });

    return false;
}
