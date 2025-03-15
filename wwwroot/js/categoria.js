var dataTable;

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $("#tblCategorias").DataTable({
        "ajax": {
            "url": "/Categorias/GetTodasCategorias",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "nombre", "width": "40%" },
            {
                "data": "fechaCreacion",
                "width": "20%",
                "render": function (data) {
                    return moment(data).format('YYYY/MM/DD');
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Categorias/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer;">
                                    <i class="fas fa-edit"></i> Editar
                                </a>
                                &nbsp;
                                <a onclick="Delete('/Categorias/Delete/${data}')" class="btn btn-danger text-white" style="cursor:pointer;">
                                    <i class="fas fa-trash-alt"></i> Borrar
                                </a>
                            </div>`;
                },
                "width": "20%"
            }
        ]
    })
}

// Funcion para delete
function Delete(url) {
    swal({
        title: "Esta seguro de querer borrar este registro?",
        text: "Esta accion no podra ser revertida!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        console.log(data.message);
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}