var myWindow, myWindow2;
$(document).ready(function () {
    myWindow = $("#window");
    myWindow.kendoWindow({
        width: "900px",
        title: "Cargos",
        visible: false,
        actions: [
            "Pin",
            "Minimize",
            "Maximize",
            "Close"
        ],
        close: onClose
    });
    myWindow2 = $("#window2");
    myWindow2.kendoWindow({
        width: "900px",
        title: "Cargos",
        visible: false,
        actions: [
            "Pin",
            "Minimize",
            "Maximize",
            "Close"
        ],
        close: onClose2
    });

    $('#ddlOpciones').on('change', function (e) {
        cambiarOpciones(this.value);
    });
    $('#ddlClaveServicio').on('change', function (e) {

        cambiarServicios(this.value);
    });
    $('#ddlCarpeta').on('change', function (e) {

        cambiarCarpetas(this.value);
    });



    $("#files").kendoUpload({
        multiple: false,
        async: {
            saveUrl: _encabezado + "/Folio/Save",
            //removeUrl: "/Seguridad/Remove",
            autoUpload: true
        },
        upload: function (e) {
            e.data = {
                idFolio: $("#hdIdFolio").val(), tipo: $("#ddlTipoArchivo").val(), carpeta: $("#ddlCarpeta").val(), evento: $("#ddlEventoArchivo").val(), orden: $("#ddlOrdenArchivo").val()
            }
        },
        complete: onComplete,
        error: onerrorAnexo,
        select: onSelectAnexo,
        success: onsuccesAnexo

    });
    var upload = $("#files").data("kendoUpload");
    upload.enable();


});
function onClose(e) {
    $("#hdIdFolio").val("0");
}
function onClose2(e) {
    $("#hdIdFolio").val("0");
}
datosCaptura = {
    txtFolio: "",
    hdIdFolio: "0",
    txtCiudad: "",
    txtEstado: "",
    txtCluster: "",
    txtOlt: "",
    txtClientesAfectados: "",
    txtFallaReportada: "",
    txtFechaDespacho: "",
    txtFechaProvista: "",
    txtLlegadaZona: "",
    txtHoraMedicion: "",
    txtUbicacion: "",
    txtCausa: "",
    txtUbicacionDanio: "",
    txtCoordenadasX: "",
    txtCoordenadasY: "",
    txtDescripcion: "",
    txtPotenciaInicial: "",
    txtPotenciaFinal: "",
    txtHoraReparacion: "",
    txtJustificacion: "",
    txtTextoAProcesar: "",
    txtServicios: "",
    txtCoordenadasCab: ""

};
datosGrid = {
    IdFolio: { type: "string" },
    Folio: { type: "string" },
    Ciudad: { type: "string" },
    Estado: { type: "string" },
    Cluster: { type: "string" },
    OLT: { type: "string" },
    ClientesAfectados: { type: "string" },
    FallaReportada: { type: "string" },
    FechaYHoraDeAsignacionDespacho: { type: "string" },
    FechaYHoraDeAsignacionProvista: { type: "string" },
    HoraDeLlegadaALaZona: { type: "string" },
    HoraDeLaPrimeraMedicion: { type: "string" },
    UbicacionDePrimerSegundoNivelYDerivacionConSusFibras: { type: "string" },
    CausaDelDano: { type: "string" },
    UbicacionDelDano: { type: "string" },
    CoordenadasDelDanoX: { type: "string" },
    CoordenadasDelDanoY: { type: "string" },
    DescripcionDeActividades: { type: "string" },
    PotencialInicia: { type: "string" },
    PotencialFinal: { type: "string" },
    FechaHoraFinalReparacion: { type: "string" },
    IdPersona_TecnicoAtiende: { type: "string" },
    IdProveedor: { type: "string" },
    IdPersona_Supervisor: { type: "string" },
    IdPersona_AtendioDespacho: { type: "string" },
    JustificacionDeSLA: { type: "string" },
    Departamento: { type: "string" },
    IdDepartamento: { type: "string" },
    Formato: { type: "string" },
    IdFormato: { type: "string" }





};
_accionGrid = "/Folio/obtenerFolios";
_accion = "/Folio/agregarFolios/";

datosInsercion = [
        { control: "txtFolio", requerido: 1, nombre: "Folio ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Folio" },
        { control: "txtCiudad", requerido: 1, nombre: "Ciudad ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Ciudad" },
        { control: "txtEstado", requerido: 1, nombre: "Estado ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Estado" },
        { control: "txtCluster", requerido: 1, nombre: "Cluster ", defaultVal: "", tipoControl: "Texto", columnaGrid: "Cluster" },
        { control: "txtOlt", requerido: 1, nombre: "Olt ", defaultVal: "", tipoControl: "Texto", columnaGrid: "OLT" },
        { control: "txtClientesAfectados", requerido: 1, nombre: "Clientes Afectados ", defaultVal: "", tipoControl: "Texto", columnaGrid: "ClientesAfectados" },
        { control: "txtFallaReportada", requerido: 1, nombre: "Falla Reportada ", defaultVal: "", tipoControl: "Texto", columnaGrid: "FallaReportada" },
        { control: "txtFechaDespacho", requerido: 1, nombre: "Fecha Despacho ", defaultVal: "", tipoControl: "Texto", columnaGrid: "FechaYHoraDeAsignacionDespacho" },
        { control: "txtFechaProvista", requerido: 1, nombre: "Fecha Provista ", defaultVal: "", tipoControl: "Texto", columnaGrid: "FechaYHoraDeAsignacionProvista" },
        { control: "txtLlegadaZona", requerido: 1, nombre: "Llegada Zona ", defaultVal: "", tipoControl: "Texto", columnaGrid: "HoraDeLlegadaALaZona" },
        { control: "txtHoraMedicion", requerido: 1, nombre: "Hora Medicion ", defaultVal: "", tipoControl: "Texto", columnaGrid: "HoraDeLaPrimeraMedicion" },
        { control: "txtUbicacion", requerido: 1, nombre: "Ubicacion ", defaultVal: "", tipoControl: "Texto", columnaGrid: "UbicacionDePrimerSegundoNivelYDerivacionConSusFibras" },
        { control: "txtCausa", requerido: 1, nombre: "Causa ", defaultVal: "", tipoControl: "Texto", columnaGrid: "CausaDelDano" },
        { control: "txtUbicacionDanio", requerido: 1, nombre: "Ubicacion del Daño ", defaultVal: "", tipoControl: "Texto", columnaGrid: "UbicacionDelDano" },
        { control: "txtCoordenadasX", requerido: 1, nombre: "Coordenadas X ", defaultVal: "", tipoControl: "Texto", columnaGrid: "CoordenadasDelDanoX" },
        { control: "txtCoordenadasY", requerido: 1, nombre: "Coordenadas Y ", defaultVal: "", tipoControl: "Texto", columnaGrid: "CoordenadasDelDanoY" },
        { control: "txtDescripcion", requerido: 1, nombre: "Descripcion ", defaultVal: "", tipoControl: "Texto", columnaGrid: "DescripcionDeActividades" },
        { control: "txtPotenciaInicial", requerido: 1, nombre: "Potencia Inicial ", defaultVal: "", tipoControl: "Texto", columnaGrid: "PotencialInicia" },
        { control: "txtPotenciaFinal", requerido: 1, nombre: "Potencia Final ", defaultVal: "", tipoControl: "Texto", columnaGrid: "FechaHoraFinalReparacion" },
        { control: "txtHoraReparacion", requerido: 1, nombre: "Hora Reparacion ", defaultVal: "", tipoControl: "Texto", columnaGrid: "FechaHoraFinalReparacion" },
        { control: "txtJustificacion", requerido: 1, nombre: "Justificacion ", defaultVal: "", tipoControl: "Texto", columnaGrid: "JustificacionDeSLA" },
        { control: "hdIdFolio", requerido: 0, nombre: "Id del Folio ", defaultVal: "0", tipoControl: "Texto", columnaGrid: "IdFolio" },
        { control: "ddlTecnicoAtiende", requerido: 0, nombre: "Tecnico Atiende ", defaultVal: "1", tipoControl: "Combo", columnaGrid: "IdPersona_TecnicoAtiende" },
        { control: "ddlSupervisor", requerido: 0, nombre: "Supervisor ", defaultVal: "1", tipoControl: "Combo", columnaGrid: "IdPersona_Supervisor" },
        { control: "ddlDespacho", requerido: 0, nombre: "Despacho ", defaultVal: "1", tipoControl: "Combo", columnaGrid: "IdPersona_AtendioDespacho" },
        { control: "ddlProveedor", requerido: 0, nombre: "Proveedor ", defaultVal: "1", tipoControl: "Combo", columnaGrid: "IdProveedor" },
        { control: "ddlFormato", requerido: 0, nombre: "Formato ", defaultVal: "1", tipoControl: "Combo", columnaGrid: "IdFormato" },
        { control: "txtServicios", requerido: 0, nombre: "Servicios ", defaultVal: "", tipoControl: "Texto", columnaGrid: "CausaDelDano" },
        { control: "txtCoordenadasCab", requerido: 0, nombre: "Coordenadas ", defaultVal: "", tipoControl: "Texto", columnaGrid: "CausaDelDano" }

];
var _folio = new Object();

datosAccion = {
    "folio": _folio
};
columnasGrid = [
     /*{
         command: [{ iconClass: "k-icon k-edit", name: "c", text: "", click: rowclick, title: "Editar", width: "30px" },
             { iconClass: "k-icon k-i-lock", name: "d", text: "", click: mostrarVentana, title: "Servicios", width: "30px" },
             { iconClass: "k-icon k-i-attachment", name: "e", text: "", click: mostrarArchivos, title: "Archivos", width: "30px" },
         ]
     },*/

                 {
                     field: "Folio",
                     title: "Folio"

                 },
                 {
                     field: "Departamento",
                     title: "Depto"

                 },
                 {
                     field: "Formato",
                     title: "Formato"

                 },

                 {
                     field: "Cluster",
                     title: "Cluster"

                 },

                 {
                     field: "DescripcionDeActividades",
                     title: "Descripcion"
                 },
                 {
                     field: "Atendio",
                     title: "Atendio"
                 },
                 {
                     field: "Proveedor",
                     title: "Proveedor"
                 },
                 {
                     field: "IdFolio",
                     title: "IdFolio", hidden: true
                 },
                 {
                     field: "IdDepartamento",
                     title: "IdDepartamento", hidden: true
                 }
                 ,
                 {
                     field: "IdFormato",
                     title: "IdFormato", hidden: true
                 }

];

function recuperarDatos() {

    _folio.IdFolio = $("#hdIdFolio").val();
    _folio.Folio = $("#txtFolio").val();
    _folio.Ciudad = $("#txtCiudad").val();
    _folio.Estado = $("#txtEstado").val();
    _folio.Cluster = $("#txtCluster").val();
    _folio.OLT = $("#txtOlt").val();
    _folio.ClientesAfectados = $("#txtClientesAfectados").val();
    _folio.FallaReportada = $("#txtFallaReportada").val();
    _folio.FechaYHoraDeAsignacionDespacho = $("#txtFechaDespacho").val();
    _folio.FechaYHoraDeAsignacionProvista = $("#txtFechaProvista").val();
    _folio.HoraDeLlegadaALaZona = $("#txtLlegadaZona").val();
    _folio.HoraDeLaPrimeraMedicion = $("#txtHoraMedicion").val();
    _folio.UbicacionDePrimerSegundoNivelYDerivacionConSusFibras = $("#txtUbicacion").val();
    _folio.CausaDelDano = $("#txtCausa").val();
    _folio.UbicacionDelDano = $("#txtUbicacionDanio").val();
    _folio.CoordenadasDelDanoX = $("#txtCoordenadasX").val();
    _folio.CoordenadasDelDanoY = $("#txtCoordenadasY").val();
    _folio.DescripcionDeActividades = $("#txtDescripcion").val();
    _folio.PotencialInicia = $("#txtPotenciaInicial").val();
    _folio.PotencialFinal = $("#txtPotenciaFinal").val();
    _folio.FechaHoraFinalReparacion = $("#txtHoraReparacion").val();
    _folio.JustificacionDeSLA = $("#txtJustificacion").val();

    _folio.IdPersona_TecnicoAtiende = $("#ddlTecnicoAtiende").val();
    _folio.IdPersona_Supervisor = $("#ddlSupervisorx").val();
    _folio.IdPersona_AtendioDespacho = $("#ddlDespachox").val();
    _folio.IdProveedor = $("#ddlProveedor").val();
    _folio.IdFormato = $("#ddlFormato").val();
    

    _folio.TrabajosRealizados = $("#txtServicios").val();
    _folio.CoordenadasCab = $("#txtCoordenadasCab").val();
}

function pintarCajasDeTexto() {

    try {
        var url = _encabezado + "/Folio/obtenerObjeto";
        var self = this;
        var _folio = new Object();
        _folio.Folio = $("#txtTextoAProcesar").val();
        var datosAccionx = {
            "Fol": _folio
        };
        axios.post(url, datosAccionx)
                .then(function (response) {
                    if (response.data.Success == "OK") {

                        /*  if (response.data.Tabla == "No Se Encontraron Errores") {
                              swal(response.data.Tabla, "", "success");
                          }
                          else swal(response.data.Tabla, "", "error");
  
                          */



                        $("#txtFolio").val(response.data.Result.Folio);
                        $("#txtCiudad").val(response.data.Result.Ciudad);
                        $("#txtEstado").val(response.data.Result.Estado);
                        $("#txtCluster").val(response.data.Result.Cluster);
                        $("#txtOlt").val(response.data.Result.OLT);
                        $("#txtClientesAfectados").val(response.data.Result.ClientesAfectados);
                        $("#txtFallaReportada").val(response.data.Result.FallaReportada);
                        $("#txtFechaDespacho").val(response.data.Result.FechaYHoraDeAsignacionDespacho);
                        $("#txtFechaProvista").val(response.data.Result.FechaYHoraDeAsignacionProvista);
                        $("#txtLlegadaZona").val(response.data.Result.HoraDeLlegadaALaZona);
                        $("#txtHoraMedicion").val(response.data.Result.HoraDeLaPrimeraMedicion);
                        $("#txtUbicacion").val(response.data.Result.UbicacionDePrimerSegundoNivelYDerivacionConSusFibras);
                        $("#txtCausa").val(response.data.Result.CausaDelDano);
                        $("#txtUbicacionDanio").val(response.data.Result.UbicacionDelDano);
                        $("#txtCoordenadasX").val(response.data.Result.CoordenadasDelDanoX);
                        $("#txtCoordenadasY").val(response.data.Result.CoordenadasDelDanoY);
                        $("#txtDescripcion").val(response.data.Result.DescripcionDeActividades);
                        $("#txtPotenciaInicial").val(response.data.Result.PotencialInicia);
                        $("#txtPotenciaFinal").val(response.data.Result.PotencialFinal);
                        $("#txtHoraReparacion").val(response.data.Result.FechaHoraFinalReparacion);
                        $("#txtJustificacion").val(response.data.Result.JustificacionDeSLA);

                        if (response.data.Result.IdPersona_TecnicoAtiende > 0) $("#ddlTecnicoAtiende").selectpicker("val", response.data.Result.IdPersona_TecnicoAtiende);
                        if (response.data.Result.IdPersona_Supervisor > 0) $("#ddlSupervisorx").selectpicker("val", response.data.Result.IdPersona_Supervisor);
                        if (response.data.Result.IdPersona_AtendioDespacho > 0) $("#ddlDespachox").selectpicker("val", response.data.Result.IdPersona_AtendioDespacho);
                        if (response.data.Result.IdProveedor > 0) $("#ddlProveedor").selectpicker("val", response.data.Result.IdProveedor);
                        //if (response.data.Result.IdFormato > 0) $("#ddlFormato").selectpicker("val", response.data.Result.IdFormato);

                        $("#txtServicios").val(response.data.Result.TrabajosRealizados);
                        $("#txtCoordenadasCab").val(response.data.Result.CoordenadasCab);


                        $("#aPanel").click();
                    }
                    else {
                        swal("Ocurrio un Error!", response.data.Result, "error");
                    }
                })
                .catch(function (error) {
                    swal("Ocurrio un Error!", error, "error");
                });



    } catch (ex) {
        alert(ex);
    }

    return false;

}


function mostrarVentana(e) {
    e.preventDefault();
    var grid = $("#miGrid").data("kendoGrid");
    grid.clearSelection();
    $(e.currentTarget).closest("tr").addClass('k-state-selected');
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    if (dataItem) {
        $("#hdIdFolio").val(dataItem.IdFolio);
        loadGridMenu(dataItem.IdFolio, dataItem.Folio);
    }

}
function loadGridMenu(idfolio, folio) {


    var parametros = {};
    parametros["IdPersona"] = 0;
    $.ajax({
        type: 'GET',
        url: _encabezado + "/Folio/obtenerTrabajos?IdFolio=" + idfolio,
        dataType: 'json',
        beforeSend: function () { $("#tablaMenu").html(""); },
        data: '{IdFolio:' + 0 + '}',
        contentType: 'application/json; charset=utf-8',
        success: function (msg) {
            $("#tablaMenu").html(msg);
            limpiarTrabajo();
            var window = myWindow.data("kendoWindow");

            window.element.prev().find(".k-window-title").html("<b class='custom'>Trabajos Realizados para el Folio: " + folio + "</b>");

            $("#window").data("kendoWindow").center().open();

        },
        error: function (x, status, error) {
            alert("Ocurrio un Error: " + status + "nError: " + x.responseText);
        }, async: false


    });
}
function loadTrabajo(idtrabajo) {
    $.ajax({
        type: 'GET',
        url: _encabezado + "/Folio/obtenerTrabajoById?IdTrabajo=" + idtrabajo,
        dataType: 'json',
        beforeSend: function () { },
        data: '{IdFolio:' + 0 + '}',
        contentType: 'application/json; charset=utf-8',
        success: function (msg) {
            $('#ddlClaveServicio').selectpicker('val', msg.IdServicio);
            $('#ddlClaveServicio').selectpicker('refresh');


            $('#txtConsecutivoServicio').val(msg.Consecutivo);
            $('#txtDescripcionServicio').val(msg.Descripcion);

            $('#txtCantidadServicio').val(msg.Cantidad);
            $('#txtPrecioUnitarioServicio').val(msg.PrecioUnitario);

            $('#ddlUnidadServicio').selectpicker('val', msg.IdUnidad);
            $('#ddlUnidadServicio').selectpicker('refresh');

            $("#hdIdTrabajo").val(msg.IdTrabajoRealizado);

        },
        error: function (x, status, error) {
            alert("Ocurrio un Error: " + status + "nError: " + x.responseText);
        }, async: false


    });
    return false;

}
function guardarTrabajo() {

    var _trabajo = new Object();

    _trabajo.IdFolio = $("#hdIdFolio").val();
    _trabajo.IdServicio = $("#ddlClaveServicio").val();
    _trabajo.Cantidad = $("#txtCantidadServicio").val();
    _trabajo.IdUnidad = $("#ddlUnidadServicio").val();
    _trabajo.PrecioUnitario = $("#txtPrecioUnitarioServicio").val();
    _trabajo.Descripcion = $("#txtDescripcionServicio").val();
    _trabajo.IdTrabajoRealizado = $("#hdIdTrabajo").val();
    _trabajo.Consecutivo = $("#txtConsecutivoServicio").val();

    var url = _encabezado + "/Folio/agregarTraabjo/";
    var datosAccion = {
        "trabajo": _trabajo
    };
    var self = this;
    axios.post(url, datosAccion)
            .then(function (response) {
                if (response.data.Result == "OK") {

                    swal("Registro Guardado Exitosamente!", "", "success");
                    limpiarTrabajo();
                    CargarTrabajos(_trabajo.IdFolio);
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
function limpiarTrabajo() {

    $("#txtCantidadServicio").val("");
    $("#txtPrecioUnitarioServicio").val("");
    $("#txtDescripcionServicio").val("");
    $("#hdIdTrabajo").val("0");
    $("#txtConsecutivoServicio").val("");
}

function CargarTrabajos(idfolio) {
    var parametros = {};
    parametros["IdPersona"] = 0;
    $.ajax({
        type: 'GET',
        url: _encabezado + "/Folio/obtenerTrabajos?IdFolio=" + idfolio,
        dataType: 'json',
        beforeSend: function () { $("#tablaMenu").html(""); },
        data: '{IdFolio:' + 0 + '}',
        contentType: 'application/json; charset=utf-8',
        success: function (msg) {
            $("#tablaMenu").html(msg);
        },
        error: function (x, status, error) {
            alert("Ocurrio un Error: " + status + "nError: " + x.responseText);
        }, async: true


    });
}

function eliminarTrabajo(idtrabajo, idfolio) {


    swal({
        title: "Esta Seguro de Eliminar el Trabajo ?",
        text: "Una Vez Eliminado el Trabajo No Se Podrá Revertir",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Si, Deseo Eliminar!",
        closeOnConfirm: false
    },
      function () {
          var url = _encabezado + "/Folio/eliminarTrabajo/";
          var datosAccion = {
              "idTrabajo": idtrabajo
          };
          var self = this;
          axios.post(url, datosAccion)
                  .then(function (response) {
                      if (response.data.Result == "OK") {

                          swal("Registro Borrado Exitosamente!", "", "success");
                          limpiarTrabajo();
                          CargarTrabajos(idfolio);
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

function cambiarServicios(idservicio) {

    $.ajax({
        type: 'GET',
        url: _encabezado + "/Folio/obtenerServiciosById?Id=" + idservicio,
        dataType: 'json',
        beforeSend: function () { $("#txtDescripcionServicio").val(""); },
        data: '{IdFolio:' + 0 + '}',
        contentType: 'application/json; charset=utf-8',
        success: function (msg) {
            $("#txtDescripcionServicio").val(msg.Descripcion);
        },
        error: function (x, status, error) {
            alert("Ocurrio un Error: " + status + "nError: " + x.responseText);
        }, async: true


    });
    return false;
}
function llenarDatos(dataItem) {

    $("#hdIdFolio").val(dataItem.IdFolio);
    $("#txtFolio").val(dataItem.Folio);
    $("#txtCiudad").val(dataItem.Ciudad);
    $("#txtEstado").val(dataItem.Estado);
    $("#txtCluster").val(dataItem.Cluster);
    $("#txtOlt").val(dataItem.OLT);
    $("#txtClientesAfectados").val(dataItem.ClientesAfectados);
    $("#txtFallaReportada").val(dataItem.FallaReportada);
    $("#txtFechaDespacho").val(dataItem.FechaYHoraDeAsignacionDespacho);
    $("#txtFechaProvista").val(dataItem.FechaYHoraDeAsignacionProvista);
    $("#txtLlegadaZona").val(dataItem.HoraDeLlegadaALaZona);
    $("#txtHoraMedicion").val(dataItem.HoraDeLaPrimeraMedicion);
    $("#txtUbicacion").val(dataItem.UbicacionDePrimerSegundoNivelYDerivacionConSusFibras);
    $("#txtCausa").val(dataItem.CausaDelDano);
    $("#txtUbicacionDanio").val(dataItem.UbicacionDelDano);
    $("#txtCoordenadasX").val(dataItem.CoordenadasDelDanoX);
    $("#txtCoordenadasY").val(dataItem.CoordenadasDelDanoY);
    $("#txtDescripcion").val(dataItem.DescripcionDeActividades);
    $("#txtPotenciaInicial").val(dataItem.PotencialInicia);
    $("#txtPotenciaFinal").val(dataItem.PotencialFinal);
    $("#txtHoraReparacion").val(dataItem.FechaHoraFinalReparacion);
    $("#txtJustificacion").val(dataItem.JustificacionDeSLA);

    $("#ddlTecnicoAtiende").selectpicker("val", dataItem.IdPersona_TecnicoAtiende);
    $("#ddlSupervisorx").selectpicker("val", dataItem.IdPersona_Supervisor);

    $("#ddlDespachox").selectpicker("val", dataItem.IdPersona_AtendioDespacho);
    $("#ddlProveedor").selectpicker("val", dataItem.IdProveedor);
    $("#ddlFormato").selectpicker("val", dataItem.IdFormato);

    //dataItem.TrabajosRealizados = $("#txtServicios").val();
    //dataItem.CoordenadasCab = $("#txtCoordenadasCab").val();



}



function mostrarVentana2(e) {
    e.preventDefault();
    var grid = $("#miGrid").data("kendoGrid");
    grid.clearSelection();
    //$(e.currentTarget).closest("tr").addClass('k-state-selected');
    $(e.target).parent().addClass('k-state-selected');
    var dataItem = grid.dataItem($(e.target).parent()[0]);
    if (dataItem) {
        $("#hdIdFolio").val(dataItem.IdFolio);
        loadGridMenu(dataItem.IdFolio, dataItem.Folio);
    }

}


function VerReporte(e) {
    e.preventDefault();
    var grid = $("#miGrid").data("kendoGrid");
    grid.clearSelection();
    //$(e.target).parent()[0].addClass('k-state-selected');
    $(e.target).parent().addClass('k-state-selected');
    var dataItem = grid.dataItem($(e.target).parent()[0]);
    if (dataItem) {
        var IdFolio = dataItem.IdFolio;
        var _reporte = reporteFolio + IdFolio;
        PopupCenter(_reporte, 'Reporte del Folio ' + IdFolio, '900', '500');
    }
    else {
        swal("Ocurrio un Error al Seleccionar el Registro!", "", "error");

    }




    return false;
}
function PopupCenter(url, title, w, h) {
    // Fixes dual-screen position                         Most browsers      Firefox
    var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : window.screenX;
    var dualScreenTop = window.screenTop != undefined ? window.screenTop : window.screenY;

    var width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
    var height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

    var left = ((width / 2) - (w / 2)) + dualScreenLeft;
    var top = ((height / 2) - (h / 2)) + dualScreenTop;

    var newWindow = window.open(url, title, 'scrollbars=yes, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);

    // Puts focus on the newWindow
    if (window.focus) {
        newWindow.focus();
    }
}



