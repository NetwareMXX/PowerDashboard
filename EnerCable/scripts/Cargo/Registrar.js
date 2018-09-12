//$(document).ready(function () {

//});
datosCaptura = {
    hdIdCargo: "0",
    txtCargo: "",


};
datosGrid = {
    Cargo: { type: "string" },
    Estatus: { type: "string" },
    IdCargo: { type: "string" }
};
_accionGrid = "/Cargo/ObtenerCargos";
_accion = "/Cargo/agregarCargos/";

datosInsercion = [
        { control: "txtCargo", requerido: 1, nombre: "Nombre del Cargo ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Cargo" },

        { control: "hdIdCargo", requerido: 0, nombre: "Id del Cargo ", defaultVal: "0", tipoControl: "Texto", columnaGrid: "IdCargo" },
        { control: "ddlEstatus", requerido: 0, nombre: "Estatus del Cargo ", defaultVal: "1", tipoControl: "Combo", columnaGrid: "IdEstatus" }



];
var _cargo = new Object();
datosAccion = {
    "cargo": _cargo
};
columnasGrid = [{
    command: { iconClass: "k-icon k-edit", name: "c", text: "", click: rowclick }, title: " ", width: "100px"
},

                 {
                     field: "Cargo",
                     title: "Cargo"

                 },

                 {
                     field: "Estatus",
                     title: "Estatus"
                 },
                 {
                     field: "IdCargo",
                     title: "IdCargo", hidden: true
                 },
                 {
                     field: "IdEstatus",
                     title: "IdEstatus", hidden: true
                 }

];

function recuperarDatos() {
    _cargo.IdCargo = $("#hdIdCargo").val();
    _cargo.Cargo = $("#txtCargo").val();
    _cargo.IdEstatus = $("#ddlEstatus").val();


}
function llenarDatos(dataItem) {
    $("#hdIdCargo").val(dataItem.IdCargo);
    $("#txtCargo").val(dataItem.Cargo);
    $("#ddlEstatus").selectpicker("val", dataItem.IdEstatus);

}

