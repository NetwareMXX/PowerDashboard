//$(document).ready(function () {

//});
datosCaptura = {
    hdIdUnidad: "0",
    txtUnidad: "",


};
datosGrid = {
    Unidad: { type: "string" },
    Estatus: { type: "string" },
    IdUnidad: { type: "string" }
};
_accionGrid = "/Unidad/ObtenerUnidades";
_accion = "/Unidad/agregarUnidad/";

datosInsercion = [
        { control: "txtUnidad", requerido: 1, nombre: "Nombre de la Unidad ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Unidad" },

        { control: "hdIdUnidad", requerido: 0, nombre: "Id de la Unidad ", defaultVal: "0", tipoControl: "Texto", columnaGrid: "IdUnidad" },
        { control: "ddlEstatus", requerido: 0, nombre: "Estatus de la Unidad ", defaultVal: "1", tipoControl: "Combo", columnaGrid: "IdEstatus" }



];
var _unidad = new Object();
datosAccion = {
    "unidad": _unidad
};
columnasGrid = [{
    command: { iconClass: "k-icon k-edit", name: "c", text: "", click: rowclick }, title: " ", width: "100px"
},

                 {
                     field: "Unidad",
                     title: "Unidad"

                 },

                 {
                     field: "Estatus",
                     title: "Estatus"
                 },
                 {
                     field: "IdUnidad",
                     title: "IdUnidad", hidden: true
                 },
                 {
                     field: "IdEstatus",
                     title: "IdEstatus", hidden: true
                 }

];

function recuperarDatos() {
    _unidad.IdUnidad = $("#hdIdUnidad").val();
    _unidad.Unidad = $("#txtUnidad").val();
    _unidad.IdEstatus = $("#ddlEstatus").val();


}
function llenarDatos(dataItem) {
    $("#hdIdUnidad").val(dataItem.IdUnidad);
    $("#txtUnidad").val(dataItem.Unidad);
    $("#ddlEstatus").selectpicker("val", dataItem.IdEstatus);

}