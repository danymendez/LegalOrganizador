
jQuery(document).ready(function () {
var ahora = new Date(jQuery.now());
var mesconcero = '0';
var diaconcero = '0';
var mesreal = ahora.getMonth() + 1;
if (mesreal < 11) {
    mesconcero = '0' + mesreal;
} else {
    mesconcero = mesreal.toString();

}

if (ahora.getDate() < 11) {
    diaconcero = '0' + ahora.getDate();
} else {
    diaconcero = ahora.getDate().toString();

}
    var fechaHoy = diaconcero + '/' + mesconcero + '/' +  ahora.getFullYear() + ' ' + ahora.getHours() + ':' + ahora.getMinutes();
jQuery.datetimepicker.setLocale('es');

jQuery('.date-time-getnow').val(fechaHoy);
jQuery('.date-time').datetimepicker({
    format: 'd/m/Y H:i'
});
jQuery('.date-time-getnow').datetimepicker({
    format: 'd/m/Y H:i'
    });

});
