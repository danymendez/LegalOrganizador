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
    swal({

        icon: "/images/loader.gif",
        title: "",
        text: "",
        closeOnClickOutside: false,
        button: false
    });
};

var onComplete = function () {
    results.html("");
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


var onFailed = function (context) {
    var isAddNew = urlRefrescar !== "";

    swal({
        title: "Ha Ocurrido un Error",
        text: "Click en salir",
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

function UrlRefrescar() {
    return urlRefrescar;
}

function UrlDestino() {
    return urlDestino;
}



