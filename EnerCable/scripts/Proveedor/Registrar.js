//$(document).ready(function () {

//});
datosCaptura = {
    hdIdProveedor: "0",
    txtProveedor: "",
    

};
datosGrid = {
    Proveedor: { type: "string" },
    Estatus: { type: "string" },
    IdProveedor: { type: "string" }
};
_accionGrid = "/Proveedor/ObtenerProveedores";
_accion = "/Proveedor/agregarProveedor/";

datosInsercion = [
        { control: "txtProveedor", requerido: 1, nombre: "Nombre del Proveedor ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Proveedor" },
        
        { control: "hdIdProveedor", requerido: 0, nombre: "Id del Proveedor ", defaultVal: "0", tipoControl: "Texto", columnaGrid: "IdPersona" },
        { control: "ddlEstatus", requerido: 0, nombre: "Estatus del Proveedor ", defaultVal: "1", tipoControl: "Combo", columnaGrid: "IdEstatus" }



];
var _proveedor = new Object();
datosAccion = {
    "proveedor": _proveedor
};
columnasGrid = [{
    command: { iconClass: "k-icon k-edit", name: "c", text: "", click: rowclick }, title: " ", width: "100px"
},

                 {
                     field: "Proveedor",
                     title: "Proveedor"

                 },

                 {
                     field: "Estatus",
                     title: "Estatus"
                 },
                 {
                     field: "IdProveedor",
                     title: "IdProveedor", hidden: true
                 },
                 {
                     field: "IdEstatus",
                     title: "IdEstatus", hidden: true
                 }

];

function recuperarDatos() {
    _proveedor.IdProveedor = $("#hdIdProveedor").val();
    _proveedor.Proveedor = $("#txtProveedor").val();
    _proveedor.IdEstatus = $("#ddlEstatus").val();


}
function llenarDatos(dataItem) {
    $("#hdIdProveedor").val(dataItem.IdProveedor);
    $("#txtProveedor").val(dataItem.Proveedor);
    $("#ddlEstatus").selectpicker("val", dataItem.IdEstatus);

}