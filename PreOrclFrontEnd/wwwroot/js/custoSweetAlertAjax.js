var urlRefrescar = "";
var urlDestino = "";

function GuardarNotificacion(selector, urlRefres, urlDest) {
urlRefrescar = urlRefres;
urlDestino = urlDest;
    //event.preventDefault();
    var isValid = jQuery(selector).valid();
    if (isValid) {
        swal({
            title: "¿Estas seguro de guardar la información?",
            text: "",
            icon: "/images/warning.png",
            closeOnClickOutside: false,
            buttons: {
                cancel: {
                    text: "Cancel",
                    value: null,
                    visible: true,
                    className: "btn-default",
                    closeModal: true,
                },
                confirm: {
                    text: "OK",
                    value: true,
                    visible: true,
                    className: "btn-primary",
                    closeModal: true
                }
            },
            dangerMode: false,
        })
            .then((willDelete) => {
                if (willDelete) {
                    jQuery(selector).submit();
                } else {
                    swal("La acción se ha cancelado");
                }
            });
    }
}

function GuardarNotificacionSetMsg(selector, urlRefres, urlDest,msjAdvertencia) {
    urlRefrescar = urlRefres;
    urlDestino = urlDest;
    //event.preventDefault();
    var isValid = jQuery(selector).valid();
    if (isValid) {
        swal({
            title: msjAdvertencia,
            text: "",
            icon: "/images/warning.png",
            closeOnClickOutside: false,
            buttons: {
                cancel: {
                    text: "Cancel",
                    value: null,
                    visible: true,
                    className: "btn-default",
                    closeModal: true,
                },
                confirm: {
                    text: "OK",
                    value: true,
                    visible: true,
                    className: "btn-primary",
                    closeModal: true
                }
            },
            dangerMode: false,
        })
            .then((willDelete) => {
                if (willDelete) {
                    jQuery(selector).submit();
                } else {
                    swal("La acción se ha cancelado");
                }
            });
    }
}

var results = jQuery("#Results");


var onBegin = function () {
    //swal({

    //    icon: "/images/loader.gif",
    //    title: "",
    //    text: "",
    //    closeOnClickOutside: false,
    //    button: false
    //});

    jQuery(function () {
        // Animate loader off screen
        jQuery(".se-pre-con").fadeIn();
    });

};

var onComplete = function () {
    results.html("");
    jQuery(function () {
        // Animate loader off screen
        jQuery(".se-pre-con").fadeOut("slow");
    });

};

var onSuccess = function (context) {

    var isAddNew = urlRefrescar !== "";

    swal({
        title: "La información se ha guardado correctamente",
        text: "¿Que desea hacer?",
        icon: "/images/warning.png",
        closeOnClickOutside: false,
        buttons: {
            cancel: {
                text: "Agregar otro Registro",
                value: null,
                visible: isAddNew,
                className: "btn-default",
                closeModal: true,
            },
            confirm: {
                text: "Salir",
                value: true,
                visible: true,
                className: "btn-primary",
                closeModal: true
            }
        },
        dangerMode: false
    })
        .then((willDelete) => {
            if (willDelete) {
                window.location.href = UrlDestino();
            } else {
                window.location.href = UrlRefrescar();
            }
        });
};

var onSuccessInModal = function (context) {

    var isAddNew = urlRefrescar !== "";

    swal({
        title: "La información se ha almacenado correctamente",
        text: "Click en Continuar para refrescar el listado",
        icon: "/images/warning.png",
        closeOnClickOutside: false,
        buttons: {
            confirm: {
                text: "Continuar",
                value: true,
                visible: true,
                className: "btn-primary",
                closeModal: true
            },
        },
        dangerMode: false
    })
        .then((willDelete) => {
            if (willDelete) {
                window.location.href = UrlDestino();
            } else {
                window.location.href = UrlRefrescar();
            }
        });
};

var onSuccessDelete = function (context) {

    var isAddNew = urlRefrescar !== "";

    swal({
        title: "La información se ha Eliminado correctamente",
        text: "Click en salir para regresar a listado",
        icon: "/images/warning.png",
        closeOnClickOutside: false,
        buttons: {
            confirm: {
                text: "Salir",
                value: true,
                visible: true,
                className: "btn-primary",
                closeModal: true
            },
        },
        dangerMode: false
    })
        .then((willDelete) => {
            if (willDelete) {
                window.location.href = UrlDestino();
            } else {
                window.location.href = UrlRefrescar();
            }
        });
};

var onFailedDuplicatedAlert = function (context) {
    swal.close();
    jQuery("#contenedorAlert").html('<div class="alert alert-danger alert-dismissible fade show" role="alert" id="alertDuplicated" style="display:none">'+context.responseText+'<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button></div>');
    jQuery("#alertDuplicated").show();
    setTimeout(function () {
        jQuery(".alert").alert('close');
    }, 4000);
};

var onFailed = function (context) {
    console.log(JSON.stringify(context));
    var isAddNew = urlRefrescar !== "";
    var Title = "Ha Ocurrido un Error";
    //alert(context.responseText);
 
    if (context.responseText !== "") {
        Title = context.responseText;
    }
    swal({
        title: Title,
        text: "Click en Ok para ocultar el mensaje",
        icon: "/images/warning.png",
        closeOnClickOutside: false,
        buttons: {
            cancel: {
                text: "Ok",
                value: null,
                visible: true,
                className: "btn-default",
                closeModal: true,
            },
        },
        dangerMode: false
    });
        //.then((willDelete) => {
        //    if (willDelete) {
        //        window.location.href = UrlDestino();
        //    } else {
        //        window.location.href = UrlRefrescar();
        //    }
        //});
};

function UrlRefrescar() {
    return urlRefrescar;
}

function UrlDestino() {
    return urlDestino;
}

function GuardarForm(ctx) {
   
}

jQuery(".submit-formdata").on("submit", function () {
    //Code: Action (like ajax...)
    var form = jQuery(this);

    var formData = new FormData(this);
    //alert(jQuery("#Casos_FechaApertura").val());
    //var fechaapertura = new Date(jQuery("#Casos_FechaApertura").val());
    //alert(fechaapertura.toDateString());
    //alert(fechaapertura.getTime());
    //for (var pair of formData.entries()) {
    //    console.log(pair[0] + ', ' + pair[1]);
    //}
    formData.set('Casos.FechaApertura', jQuery("#Casos_FechaApertura").val());
 
    jQuery.ajax({
        url: form.attr("action"),
        beforeSend: function () {

                // Animate loader off screen
                jQuery(".se-pre-con").fadeIn();
       
        },
        success: function (data, textStatus, xhr) {
            swal({
                title: "La información se ha guardado correctamente",
                text: "¿Que desea hacer?",
                icon: "/images/warning.png",
                closeOnClickOutside: false,
                buttons: {
                    cancel: {
                        text: "Agregar otro Registro",
                        value: null,
                        visible: true,
                        className: "btn-default",
                        closeModal: true,
                    },
                    confirm: {
                        text: "Salir",
                        value: true,
                        visible: true,
                        className: "btn-primary",
                        closeModal: true
                    }
                },
                dangerMode: false
            })
                .then((willDelete) => {
                    if (willDelete) {
                        window.location.href = form.attr("action").replace("Create", "Index").replace("Edit", "Index");
                    } else {
                        window.location.href = form.attr("action");
                    }
                });
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //ua_func_error(jqXHR, textStatus, errorThrown, form);
        },
        complete: function (xhr, textStatus) {
            jQuery(".se-pre-con").fadeOut("slow");
        },
        data: formData,
        type: form.attr("method"),
        contentType: false,
        processData: false,
        async: true,
        cache: false
    });
    return false;
});
