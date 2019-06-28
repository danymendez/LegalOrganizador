jQuery(function () {
    jQuery('#bootstrap-data-table2').DataTable({
        "language": {
            "lengthMenu": "Mostrando _MENU_ registros por página",
            "zeroRecords": "Lo sentimos ningún registro encontrado",
            "info": "Mostrando _PAGE_ de _PAGES_",
            "infoEmpty": "No se encontraron registros",
            "infoFiltered": "(filtrado de _MAX_ registros)",
            "paginate": {
                "previous": "Anterior",
                "next": "Siguiente"
            },
            "search": "Buscar"
        }
    });
    jQuery('#bootstrap-data-table-export').DataTable();
});