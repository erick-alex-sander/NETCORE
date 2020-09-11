var table = null

function callTable() {
    table = $('#myTable').DataTable({
        "ajax": {
            'url': "/Employees/Load",
            'type': "GET",
            'dataType': "json",
            'dataSrc': ""
        },
        "columns": [
            {
                "width": "5%",
                "data": "id", defaultContent: ''
            },
            { "data": "firstName" },
            { "data": "lastname" },
            { "data": "email" },
            { "data": "address" },
            {"data": "phoneNumber"},
            {
                "data": "createdDate",
                "render": function (jsonDate) {
                    var date = moment(jsonDate).format("DD MMMM YYYY HH:mm:ss");
                    return date;
                }
            },
            {
                "data": "updatedDate",
                "render": function (jsonDate) {
                    if (!moment(jsonDate).isBefore("1000-01-01")) {
                        var date = moment(jsonDate).format("DD MMMM YYYY HH:mm:ss");
                        return date;
                    }
                    return "Not updated yet";
                }
            },
            {
                "sortable": false,
                "render": function myFunction(data, type, row) {
                    $('[data-toggle="tooltip"]').tooltip();
                    return '<button id="info" class="btn btn-primary" data-toggle="tooltip" data-placement="left" title="Update" onClick="Info(\'' + row.userName + '\')"><i class="fas fa-info"></i></button>' +
                        '&nbsp;' +
                        '<button id="delete" class="btn btn-danger" data-toggle="tooltip" data-placement="right" title="Delete" onClick="Delete(\'' + row.userName + '\')"><i class="fa fa-trash"></i></button>'
                }
            }],
        "columnDefs": [{
            "searchable": false,
            "orderable": false,
            "targets": 0
        }],
        "order": [[1, 'asc']]
    });
    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
}

$(document).ready(function () {
    debugger;
    callTable();
});

function Info(id) {
    debugger;
    $('#detail').modal('show');
    $.ajax({
        url: "/Employees/Load/" + id,
        type: "get",
        success: function (response) {
            var fullName = response.firstName + ' ' + response.lastname;
            $('#fullName').text(fullName);
            $('#birthDate').text(moment(response.birthDate).format("DD MMMM YYYY"));
            $('#address').text(response.address);
            $('#university').text(response.university);
            $('#skill').text(response.skill);
        }
    });
};

function Delete(id) {
    debugger;
    swal.fire({
        title: "Are you sure?",
        text: "This cannot be undone!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        reverseButtons: true,

    }).then((willDelete) => {
        if (willDelete.isConfirmed) {
            $.ajax({
                url: "/Employees/Delete/" + id,
                type: "post",
                success: function (result) {

                }
            });
            swal.fire("Your file has been deleted!", {
                icon: "success",
            });
            table.ajax.reload(null, false);
        } else {

        }
    });
};