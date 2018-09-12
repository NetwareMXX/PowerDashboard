//$(document).ready(function () {

//});
datosCaptura = {
    hdIdServicio: "0",
    txtClave: "",
    txtDescripcion: "",
    txtPrecioUnitario: "",
    txtMaterial:""

};
datosGrid = {
    Descripcion: { type: "string" },
    Clave: { type: "string" },
    PrecioUnitario: { type: "string" },
    Estatus: { type: "string" },
    Unidad: { type: "string" },
    IdServicio: { type: "string" },
    IdClasificacionServicio: { type: "string" },
    Clasificacion: { type: "string" },
    MaterialNSC: { type: "string" }
};
_accionGrid = "/Servicio/ObtenerServicios";
_accion = "/Servicio/agregarServicio/";

datosInsercion = [
        { control: "txtClave", requerido: 1, nombre: "Clave del Servicio ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Clave" },
        { control: "txtDescripcion", requerido: 1, nombre: "Descripcion del Servicio ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Descripcion" },
        { control: "txtMaterial", requerido: 1, nombre: "Material NSC del Servicio ", defaultVal: "", tipoControl: "Texto", columnaGrid: "MaterialNSC" },
        { control: "txtPrecioUnitario", requerido: 0, nombre: "Precio Unitario del Servicio ", defaultVal: "", tipoControl: "Texto", columnaGrid: "PrecioUnitario" },
        { control: "hdIdServicio", requerido: 0, nombre: "Id del Servicio ", defaultVal: "0", tipoControl: "Texto", columnaGrid: "IdServicio" },
        { control: "ddlUnidad", requerido: 0, nombre: "Unidad del Servicio ", defaultVal: "1", tipoControl: "Combo", columnaGrid: "IdUnidad" },
        { control: "ddlClasificacion", requerido: 0, nombre: "Clasificacion del Servicio ", defaultVal: "0", tipoControl: "Combo", columnaGrid: "IdClasificacionServicio" },
        { control: "ddlEstatus", requerido: 0, nombre: "Estatus del Servicio ", defaultVal: "1", tipoControl: "Combo", columnaGrid: "IdEstatus" }



];
var _servicio = new Object();
datosAccion = {
    "servicio": _servicio
};
columnasGrid = [{
    command: { iconClass: "k-icon k-edit", name: "c", text: "", click: rowclick }, title: " ", width: "100px"
},

                 {
                     field: "Clave",
                     title: "Clave"

                 },

                 {
                     field: "Descripcion",
                     title: "Descripcion"
                 },
                 {
                     field: "IdServicio",
                     title: "IdServicio", hidden: true
                 },
                 {
                     field: "PrecioUnitario",
                     title: "Precio Unitario"
                 },

                 {
                     field: "Unidad",
                     title: "Unidad"
                 },
                 {
                     field: "MaterialNSC",
                     title: "Material NSC"
                 },
                 {
                     field: "Clasificacion",
                     title: "Clasificacion"
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
                 },
                 {
                     field: "IdUnidad",
                     title: "IdUnidad", hidden: true
                 }

];

function recuperarDatos() {
    _servicio.IdServicio = $("#hdIdServicio").val();
    _servicio.Clave = $("#txtClave").val();
    _servicio.Descripcion = $("#txtDescripcion").val();
    _servicio.MaterialNSC = $("#txtMaterial").val();
    _servicio.PrecioUnitario = $("#txtPrecioUnitario").val();

    _servicio.IdClasificacionServicio = $("#ddlClasificacion").val();
    _servicio.IdUnidad = $("#ddlUnidad").val();
    _servicio.IdEstatus = $("#ddlEstatus").val();

}
function llenarDatos(dataItem) {
    $("#hdIdServicio").val(dataItem.IdServicio);
    $("#txtClave").val(dataItem.Clave);
    $("#txtDescripcion").val(dataItem.Descripcion);
    $("#txtMaterial").val(dataItem.MaterialNSC);
    $("#txtPrecioUnitario").val(dataItem.PrecioUnitario);
    $("#ddlEstatus").selectpicker("val", dataItem.IdEstatus);
    $("#ddlUnidad").selectpicker("val", dataItem.IdUnidad);
    $("#ddlClasificacion").selectpicker("val", dataItem.IdClasificacionServicio);

}