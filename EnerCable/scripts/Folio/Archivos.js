function mostrarArchivos(e) {
    e.preventDefault();
    var grid = $("#miGrid").data("kendoGrid");
    grid.clearSelection();
    $(e.currentTarget).closest("tr").addClass('k-state-selected');
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    if (dataItem) {
        $("#hdIdFolio").val(dataItem.IdFolio);
        loadArchivos(dataItem.IdFolio, dataItem.Folio, dataItem.IdDepartamento);
    }

}
function mostrarArchivos2(e) {
    e.preventDefault();
    var grid = $("#miGrid").data("kendoGrid");
    grid.clearSelection();
    $(e.target).parent().addClass('k-state-selected');
    var dataItem = grid.dataItem($(e.target).parent()[0]);
    if (dataItem) {
        $("#hdIdFolio").val(dataItem.IdFolio);
        loadArchivos(dataItem.IdFolio, dataItem.Folio, dataItem.IdDepartamento);
    }

}
function loadArchivos(idfolio, folio, iddepartamento) {

    $('#ddlOpciones').selectpicker('val', "0");
    $('#ddlOpciones').selectpicker('refresh');
    $("#divArchivos").hide();
    $("#divDeptos").show();
    if (iddepartamento != "0") {
        $('#ddlDepartamentoModal').selectpicker('val', iddepartamento);
        $('#ddlDepartamentoModal').selectpicker('refresh');
    }


    $('#ddlCarpeta').selectpicker('val', "1");
    $('#ddlCarpeta').selectpicker('refresh');

    $('#ddlTipoArchivo').selectpicker('val', "1");
    $('#ddlTipoArchivo').selectpicker('refresh');
    $("#btnLimpiar").hide();

    var window = myWindow2.data("kendoWindow");
    window.element.prev().find(".k-window-title").html("<b class='custom'>Opciones para el Folio: " + folio + "</b>");

    pintarTablaArchivos(idfolio);
    $("#window2").data("kendoWindow").center().open();

}
function cambiarOpciones(opcion) {

    if (opcion == "0") {
        $("#divArchivos").hide();
        $("#divDeptos").show();
    }
    else if (opcion == "1") {
        $("#divArchivos").show();
        $("#divDeptos").hide();
    }
    return false;
}
function cambiarCarpetas(opcion) {
    if (opcion == "3") {
        $("#divReporteFotografico").show();
    }
    else  {
        $("#divReporteFotografico").hide();
    }
    return false;
}
function guardarDepartamento() {
    if ($("#ddlOpciones").val() == "0") {
        guardarDep();
    }
    else if ($("#ddlOpciones").val() == "1") {
        guardarArchivo();
    }
    return false;
}
function guardarDep() {

    var url = _encabezado + "/Folio/agregarDepto/";
    var datosAccion = {
        "IdFolio": $("#hdIdFolio").val(),
        "Depto": $("#ddlDepartamentoModal").val()
    };
    var self = this;
    axios.post(url, datosAccion)
            .then(function (response) {
                if (response.data.Result == "OK") {

                    swal("Registro Guardado Exitosamente!", "", "success");
                    $("#window2").data("kendoWindow").close();
                    $("#miGrid").data("kendoGrid").dataSource.read();
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
function guardarArchivo() {

    return false;
}

////////////////////////ARCHIVOS
function eliminarFolio(idarchivo, idfolio, idcarpeta) {
    swal({
        title: "Esta Seguro de Eliminar el Archivo ?",
        text: "Una Vez Eliminado el Archivo No Se Podrá Revertir",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Si, Deseo Eliminar!",
        closeOnConfirm: false
    },
          function () {
              var url = _encabezado + "/Folio/eliminarFolio/";
              var datosAccion = {
                  "IdArchivo": idarchivo,
                  "IdFolio": idfolio,
                  "IdCarpeta": idcarpeta

              };
              var self = this;
              axios.post(url, datosAccion)
                      .then(function (response) {
                          if (response.data.Result == "OK") {

                              swal("Registro Borrado Exitosamente!", "", "success");
                              pintarTablaArchivos(idfolio);
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
function onsuccesAnexo(e) {


    if (e.response.Evento == "Error") {

        $(".k-file[data-uid='" + e.files[0].uid + "']").removeClass('k-file-success').addClass('k-file-error');
        $(".k-file[data-uid='" + e.files[0].uid + "']").html("");


        var _span = "<span class='k-progress' style='width: 100%;'></span>";
        _span += "<span class='k-icon k-i-xlsx'>uploaded</span>";
        _span += "<span class='k-texto'>" + e.response.Archivo + ": " + e.response.status + "</span>";
        _span += "<strong class='k-upload-status'><span class='k-upload-pct'>0 Filas</span><button type='button' class='0k-button k-button-bare k-upload-action' style='display: none;'></button></strong>";
        $(".k-file[data-uid='" + e.files[0].uid + "']").html(_span);


    }
    else if (e.response.Evento == "OK") {
        var _span = "<span class='k-progress' style='width: 100%;'></span>";
        _span += "<span class='k-icon k-i-xlsx'>uploaded</span>";
        _span += "<span class='k-texto'>" + e.response.Archivo + ": Importado Correctamente</span>";
        _span += "<strong class='k-upload-status'><span class='k-upload-pct'>" + e.response.Archivo + "</span><button type='button' class='0k-button k-button-bare k-upload-action' style='display: none;'></button></strong>";
        $(".k-file[data-uid='" + e.files[0].uid + "']").html(_span);
        pintarTablaArchivos(e.response.Folio);
    }



}
function onerrorAnexo(e) {
    alert('Error en carga de Archivo ' + e.Evento);
}

function onSelectAnexo(e) {
    var files = e.files
    var acceptedFiles;

    if ($("#ddlTipoArchivo").val() == "1") {
        acceptedFiles = [".png", ".PNG", ".jpg", ".JGP", ".jpeg", ".JPEG"];
    }
    else if ($("#ddlTipoArchivo").val() == "2") {
        acceptedFiles = [".doc", ".docx", ".DOC", ".DOCX"];
    }
    else if ($("#ddlTipoArchivo").val() == "3") {
        acceptedFiles = [".xls", ".xlsx", ".XLS", ".XLSX"];
    }
    else if ($("#ddlTipoArchivo").val() == "4") {
        acceptedFiles = [".pdf", ".PDF"];
    }

    var isAcceptedImageFormat = ($.inArray(files[0].extension, acceptedFiles)) != -1


    if (e.files.length > 1) {
        alert("Solo Puede Cargar 1 Archivo");
        e.preventDefault();
    }


    if (!isAcceptedImageFormat) {
        e.preventDefault();
        alert("El Tipo de Archivo Permitido es en Formato Excel");
    }


}


function onComplete() {

    $("#btnLimpiar").show();


}
function pintarTablaArchivos(idfolio) {
    $.ajax({
        type: 'GET',
        url: _encabezado + "/Folio/obtenerArchivos?IdFolio=" + idfolio,
        dataType: 'json',
        beforeSend: function () { $("#tablaArchivos").html(""); },
        data: '{IdFolio:' + 0 + '}',
        contentType: 'application/json; charset=utf-8',
        success: function (msg) {
            $("#tablaArchivos").html(msg);

        },
        error: function (x, status, error) {
            $("#tablaArchivos").html("");
            alert("Ocurrio un Error: " + status + "nError: " + x.responseText);
        }, async: false


    });
}

