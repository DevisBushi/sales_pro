var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/policyType",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "70%" },
            {
                "data": "id",
                "render": function (data) {
                    return ` <div class="text-center">
                                <a href="/Administration/type/upsert?id=${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                    Ndrysho </a>
                                <a class="btn btn-danger text-white" style="cursor:pointer; width:100px;" onclick=Delete('/api/policyType/'+${data})>
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