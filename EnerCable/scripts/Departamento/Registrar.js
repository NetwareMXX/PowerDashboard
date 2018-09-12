//$(document).ready(function () {

//});
datosCaptura = {
    hdIdDepartamento: "0",
    txtDepartamento: "",
    txtGerente: "",
    txtCorreo: "",
    txtTelefono: ""



};
datosGrid = {
    Departamento: { type: "string" },
    NombreGerente: { type: "string" },
    Correo: { type: "string" },
    Telefono: { type: "string" },
    IdEstatus: { type: "string" },
    IdDepartamento: { type: "string" },
    Estatus: { type: "string" }

};
_accionGrid = "/Departamento/ObtenerDepartamentos";
_accion = "/Departamento/agregarDepartamento/";

datosInsercion = [
        { control: "txtDepartamento", requerido: 1, nombre: "Nombre del Departamento ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Departamento" },
         { control: "txtGerente", requerido: 1, nombre: "Nombre del Gerente ", defaultVal: "", tipoControl: "Texto", columnaGrid: "NombreGerente" },
         { control: "txtCorreo", requerido: 1, nombre: "Correo ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Correo" },
         { control: "txtTelefono", requerido: 1, nombre: "Telefono ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Telefono" },
        { control: "hdIdDepartamento", requerido: 0, nombre: "Id del Departamento ", defaultVal: "0", tipoControl: "Texto", columnaGrid: "IdDepartamento" },
        { control: "ddlEstatus", requerido: 0, nombre: "Estatus de la Unidad ", defaultVal: "1", tipoControl: "Combo", columnaGrid: "IdEstatus" }
];
var _departamento = new Object();
datosAccion = {
    "departamento": _departamento
};
columnasGrid = [{
    command: { iconClass: "k-icon k-edit", name: "c", text: "", click: rowclick }, title: " ", width: "100px"
},

                 {
                     field: "Departamento",
                     title: "Departamento"

                 },

                 {
                     field: "NombreGerente",
                     title: "Gerente"

                 },

                 {
                     field: "Telefono",
                     title: "Telefono"

                 },
                 {
                     field: "Correo",
                     title: "Correo"

                 },
                 {
                     field: "Estatus",
                     title: "Estatus"

                 },
                 {
                     field: "IdDepartamento",
                     title: "IdDepartamento", hidden: true
                 },
                 {
                     field: "IdEstatus",
                     title: "IdEstatus", hidden: true
                 }

];

function recuperarDatos() {
    _departamento.IdDepartamento = $("#hdIdDepartamento").val();
    _departamento.Departamento = $("#txtDepartamento").val();
    _departamento.NombreGerente = $("#txtGerente").val();
    _departamento.Correo = $("#txtCorreo").val();
    _departamento.Telefono = $("#txtTelefono").val();
    _departamento.IdEstatus = $("#ddlEstatus").val();


}
function llenarDatos(dataItem) {
    $("#hdIdDepartamento").val(dataItem.IdDepartamento);
    $("#txtDepartamento").val(dataItem.Departamento);
    $("#txtGerente").val(dataItem.NombreGerente);
    $("#txtCorreo").val(dataItem.Correo);
    $("#txtTelefono").val(dataItem.Telefono);
    $("#ddlEstatus").selectpicker("val", dataItem.IdEstatus);

}