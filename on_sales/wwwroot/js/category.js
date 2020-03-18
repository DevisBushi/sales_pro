var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/category",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "40%" },
            { "data": "dispOrder", "width": "30%" },
            {
                "data": "id",
                "render": function (data) {
                    return ` <div class="text-center">
                                <a href="/Administration/Category/upsert?id=${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                     Ndrysho </a>
                                <a class="btn btn-danger text-white" style="cursor:pointer; width:100px;" onclick=Delete('/api/category/'+${data})>
                                     Fshij </a>
                    </div>`;
                }, "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "Nuk u gjeten te dhena.."
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Jeni te sigurt qe do e fshini?",
        text: "Nuk mund ti ktheni me te dhenat!",
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
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}