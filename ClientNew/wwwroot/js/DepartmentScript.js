$(document).ready(function () {
    let i = 1;
   var t = $("#tbDepartments").DataTable({
        "paging": true,
        "responsive": true,
        "lengthChange": true,
        "autoWidth": false,
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"],
        "searching": true,
        "ordering": true,
        "info": true,

        "ajax": {
            url: "https://localhost:7032/api/Department",
            type: "GET",
            "datatype": "json",
            "dataSrc": "allData",

        },
        "columns": [
            {
                "data": null, orderable: false, render: function (data, type, row, meta) {
                    return i++;
                }
            },
          
            { data: "name" },
            {
                "data": null, orderable: false, render: function (data, type, row) {
                    return '<div class="text-center"> <button type="button" class="btn btn-primary btn-sm edit-button" data-tooltip="tooltip" title="Edit Department" onclick="return GetById(\'' + row.dept_Id + '\')" > <i class="fas fa-edit"></i> Edit</button>  <button type="button" class="btn btn-danger btn-sm remove-button" data-tooltip="tooltip" title="Hapus Department" onclick="return Delete(\'' + row.dept_Id + '\')"><i class="fas fa-trash"></i> Hapus</button> </div> '
                }
            }], "order": [],

    });
    t.on('order.dt search.dt', function () {
        t.column(0, {
            search: 'applied',
            order: 'applied'
        }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
});
    


function Save() {
    $('#exampleModal').modal("hide");
    var Department = new Object();
    Department.name = $('#DepartmentName').val();
    if (Department.name === "") {
        Swal.fire(
            'Gagal!',
            'Nama Department tidak boleh kosong',
            'warning'
        )
        return;
    }
    $.ajax({
        type: 'POST',
        url: 'https://localhost:7032/api/Department',
        data: JSON.stringify(Department),
        contentType: "application/json; charset=utf-8",
    }).then((result) => {
        debugger;
        if (result.status == 200) {
            Swal.fire({
                icon: 'success',
                title: 'Berhasil',
                text: result.response,
            })
            $('#tbDepartments').DataTable().ajax.reload();
        } else {
            alert("Data Gagal Ditambahkan")
        }
    })
    
}

function ResetText() {
    $('#exampleModalLabel').text("Department")
    $('#DepartmentName').val("")
    $('#Edit').addClass("d-none")
    $('#Save').removeClass("d-none") 
    /* $('#Save').hide() */
}
function GetById(dept_Id) {
    $.ajax({
        url: "https://localhost:7032/api/Department/" + dept_Id,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            var obj = result.get;
            $('#Id').val(obj.dept_Id);
            $('#DepartmentName').val(obj.name);
            $('#exampleModalLabel').text("Edit Department")
            $('#Save').addClass("d-none")
            $('#Edit').removeClass("d-none")
            $('#exampleModal').modal('show');
            
        }
    });
}

function Edit() {
    $('#exampleModal').modal("hide");
    var Department = new Object();
    Department.dept_Id = $('#Id').val();
    Department.name = $('#DepartmentName').val();
    if (Department.name === "") {
        Swal.fire(
            'Gagal!',
            'Nama Department tidak boleh kosong',
            'warning'
        )
        return;
    }
    $.ajax({
        type: 'PUT',
        url: 'https://localhost:7032/api/Department/' + Department.dept_Id, 
        data: JSON.stringify(Department),
        contentType: "application/json; charset=utf-8",
    }).then((result) => {
        if (result.status == 200) {
            Swal.fire({
                icon: 'success',
                title: 'Edited',
                text: result.response,
            })
            $('#tbDepartments').DataTable().ajax.reload();
        } else {
            alert("Data Gagal Ditambahkan")
        }
    })
}

function Delete(dept_Id) {
    $('#exampleModal').modal("hide");
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'DELETE',
                url: 'https://localhost:7032/api/Department/' + dept_Id,
                contentType: "application/json; charset=utf-8",
            }).then((result) => {
                if (result.status == 200) {

                    Swal.fire(
                        'Deleted!',
                        result.response,
                        'success'
                    )
                    $('#tbDepartments').DataTable().ajax.reload();
                } else {
                    alert("Data Gagal Dihapus")
                }

            })
        }
    })
}

    

$(document).ajaxComplete(function () {
    $('[data-tooltips="tooltip"]').tooltip({
        trigger: 'hover'
    });
    $('[data-tooltip="tooltip"]').tooltip({
        trigger: 'hover'
    });
});


