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
    swal({
        title: "La información se ha guardado correctamente",
        text: "¿Que desea hacer?",
        icon: "/images/warning.png",
        closeOnClickOutside: false,
        buttons: {
            cancel: {
                text: "Agregar otra persona",
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
                window.location.href = UrlDestino();
            } else {
                window.location.href = UrlRefrescar();
            }
        });
};

var onFailed = function (context) {
    alert("Failed");
};

function UrlRefrescar() {
    return urlRefrescar;
}

function UrlDestino() {
    return urlDestino;
}



