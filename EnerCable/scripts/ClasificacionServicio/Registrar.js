//$(document).ready(function () {

//});
datosCaptura = {
    hdIdClasificacion: "0",
    txtClasificacion: "",


};
datosGrid = {
    Clasificacion: { type: "string" },
    Estatus: { type: "string" },
    IdEstatus: { type: "string" },
    IdClasificacionServicio: { type: "string" }
};
_accionGrid = "/ClasificacionServicio/obtenerClasificaciones";
_accion = "/ClasificacionServicio/agregarClasificacion/";

datosInsercion = [
        { control: "txtClasificacion", requerido: 1, nombre: "Nombre de la Clasificacion ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Clasificacion" },
        { control: "hdIdClasificacion", requerido: 0, nombre: "Id de la Clasificacion ", defaultVal: "0", tipoControl: "Texto", columnaGrid: "IdClasificacionServicio" },
        { control: "ddlEstatus", requerido: 0, nombre: "Estatus de la Unidad ", defaultVal: "1", tipoControl: "Combo", columnaGrid: "IdEstatus" }



];
var _clasificacion = new Object();
datosAccion = {
    "clasificacion": _clasificacion
};
columnasGrid = [{
    command: { iconClass: "k-icon k-edit", name: "c", text: "", click: rowclick }, title: " ", width: "100px"
},

                 {
                     field: "Clasificacion",
                     title: "Clasificacion Servicio"

                 },

                 {
                     field: "Estatus",
                     title: "Estatus"
                 },
                 {
                     field: "IdClasificacionServicio",
                     title: "IdClasificacionServicio", hidden: true
                 },
                 {
                     field: "IdEstatus",
                     title: "IdEstatus", hidden: true
                 }

];

function recuperarDatos() {
    _clasificacion.IdClasificacionServicio = $("#hdIdClasificacion").val();
    _clasificacion.Clasificacion = $("#txtClasificacion").val();
    _clasificacion.IdEstatus = $("#ddlEstatus").val();


}
function llenarDatos(dataItem) {
    $("#hdIdClasificacion").val(dataItem.IdClasificacionServicio);
    $("#txtClasificacion").val(dataItem.Clasificacion);
    $("#ddlEstatus").selectpicker("val", dataItem.IdEstatus);

}