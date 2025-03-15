var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#tblUsuarios").DataTable({
        "ajax": {
            "url": "/Usuarios/GetTodosUsuarios",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "50%" },
            { "data": "nombre", "width": "25%" },
            { "data": "userName", "width": "25%" }
        ]
    });
}