var myWindow;
$(document).ready(function () {
    myWindow = $("#window");
    myWindow.kendoWindow({
        width: "800px",
        height:"600px",
        title: "Servicios",
        visible: false,
        actions: [
            "Pin",
            "Minimize",
            "Maximize",
            "Close"
        ],
        close: onClose
        //close: onClose
    });//.data("kendoWindow").center().open();
    $('#ddlClaveServicio').selectpicker();
});
function onClose(e) {
    $("#hdIdFormato").val("0");
}
datosCaptura = {
    hdIdFormato: "0",
    txtFormato: "",


};
datosGrid = {
    Formato: { type: "string" },
    Estatus: { type: "string" },
    IdFormato: { type: "string" }
};
_accionGrid = "/Formato/obtenerFormatos";
_accion = "/Formato/agregarFormato/";

datosInsercion = [
        { control: "txtFormato", requerido: 1, nombre: "Nombre del Formato ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Formato" },

        { control: "hdIdFormato", requerido: 0, nombre: "Id del Formato ", defaultVal: "0", tipoControl: "Texto", columnaGrid: "IdFormato" },
        { control: "ddlEstatus", requerido: 0, nombre: "Estatus del Formato ", defaultVal: "1", tipoControl: "Combo", columnaGrid: "IdEstatus" }



];
var _formato = new Object();
datosAccion = {
    "formato": _formato
};
columnasGrid = [{
    
    command: [{ iconClass: "k-icon k-edit", name: "c", text: "", click: rowclick, title: "", width: "30px" },
           { iconClass: "k-icon k-i-connector", name: "d", text: "", click: mostrarVentana, title: "Permisos", width: "30px" }
    ]
},

                 {
                     field: "Formato",
                     title: "Formato"

                 },

                 {
                     field: "Estatus",
                     title: "Estatus"
                 },
                 {
                     field: "IdFormato",
                     title: "IdFormato", hidden: true
                 },
                 {
                     field: "IdEstatus",
                     title: "IdEstatus", hidden: true
                 }

];

function recuperarDatos() {
    _formato.IdFormato = $("#hdIdFormato").val();
    _formato.Formato = $("#txtFormato").val();
    _formato.IdEstatus = $("#ddlEstatus").val();


}
function llenarDatos(dataItem) {
    $("#hdIdFormato").val(dataItem.IdFormato);
    $("#txtFormato").val(dataItem.Formato);
    $("#ddlEstatus").selectpicker("val", dataItem.IdEstatus);

}
function mostrarVentana(e) {
    e.preventDefault();
    var grid = $("#miGrid").data("kendoGrid");
    grid.clearSelection();
    $(e.currentTarget).closest("tr").addClass('k-state-selected');
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    if (dataItem) {
        $("#hdIdFormato").val(dataItem.IdFormato);
        loadGridMenu(dataItem.IdFormato);
    }

}
function loadGridMenu(idperfil) {


    var parametros = {};
    parametros["IdPerfil"] = 0;
    $.ajax({
        type: 'GET',
        url: _encabezado + "/Formato/obtenerServicios?IdFormato=" + idperfil,
        dataType: 'json',
        beforeSend: function () { $("#tablaMenu").html(""); },
        data: '{IdPerfil:' + 0 + '}',
        contentType: 'application/json; charset=utf-8',
        success: function (msg) {
            $("#tablaMenu").html(msg);

            var window = myWindow.data("kendoWindow");
            window.element.prev().find(".k-window-title").html("<b class='custom'>Agregar Servicios </b>");
            //window.element.prev().find(".k-window-title").html("<b class='custom'>Servicios para el Formato " + perfil + "</b>");

            $("#window").data("kendoWindow").center().open();

        },
        error: function (x, status, error) {
            alert("Ocurrio un Error: " + status + "nError: " + x.responseText);
        }, async: false


    });
}
function guardarServicio() {

    
    var url = _encabezado + "/Formato/agregarServicio/";
    var datosAccion = {
        "IdFormato": $("#hdIdFormato").val(),
        "IdServicio": $("#ddlClaveServicio").val()
    };
    var self = this;
    axios.post(url, datosAccion)
            .then(function (response) {
                if (response.data.Result == "OK") {

                    swal("Registro Guardado Exitosamente!", "", "success");
                    loadGridMenu($("#hdIdFormato").val());
                }
                else {
                    swal("Ocurrio un Error!", response.data.Result, "error");
                }
            })
            .catch(function (error) {
                swal("Ocurrio un Error!", error, "error");
            });
    return false;
}
function eliminarTrabajo(idservicio, idformato) {


    swal({
        title: "Esta Seguro de Eliminar el Servicio ?",
        text: "Una Vez Eliminado el Trabajo No Se Podrá Revertir",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Si, Deseo Eliminar!",
        closeOnConfirm: false
    },
      function () {
          var url = _encabezado + "/Formato/eliminarServicio/";
          var datosAccion = {
              "idServicio":  idservicio,
              "idFormato": idformato
          };
          var self = this;
          axios.post(url, datosAccion)
                  .then(function (response) {
                      if (response.data.Result == "OK") {

                          swal("Registro Borrado Exitosamente!", "", "success");
                          
                          loadGridMenu(idformato);
                      }
                      else {
                          swal("Ocurrio un Error!", response.data.Result, "error");
                      }
                  })
                  .catch(function (error) {
                      swal("Ocurrio un Error!", error, "error");
                  });
      });

    return false;
}