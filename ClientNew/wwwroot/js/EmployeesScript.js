$(document).ready(function () {
    let i = 1;
    var t = $("#tbEmployees").DataTable({
        "paging": true,
        "responsive": true,
        "lengthChange": true,
        "autoWidth": false,
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"],
        "searching": true,
        "ordering": true,
        "info": true,

        "ajax": {
            url: "https://localhost:7032/api/Employee/GetDetailedEmployee",
            type: "GET",
            "datatype": "json",
            "dataSrc": "data",

        },
        "columns": [
            {
                "data": null, orderable: false, render: function (data, type, row, meta) {
                    return i++;
                }
            },
            {
                data: null, render: function (data, type, row, meta) {
                    return row.firstName + ' ' + row.lastName;
                } },
            { data: "email" },
            { data: "department_Name" },
            {
                data: "status",
                render: function (data, type, row) {
                    if (type === 'display') {
                        if (data === true) {
                            return '<span class="badge badge-success d-flex justify-content-center">Active</span>';
                        } else {
                            return '<span class="badge badge-danger d-flex justify-content-center">Resign</span>';
                        }
                    }
                    return data;
                }
},
            {
                "data": null, orderable: false, render: function (data, type, row) {
                    return '<div class="text-center"> <button type="button" class="btn btn-primary btn-sm edit-button" data-tooltip="tooltip" title="Edit Employee" onclick="return GetById(\'' + row.nik + '\')" > <i class="fas fa-edit"></i> Edit</button>  <button type="button" class="btn btn-danger btn-sm remove-button" data-tooltip="tooltip" title="Hapus Employee" onclick="return Delete(\'' + row.nik + '\')"><i class="fas fa-trash"></i> Hapus</button> </div> '
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
    // Fungsi untuk mendapatkan data departemen 
    function Departments() {
        debugger
        $.ajax({
            url: 'https://localhost:7032/api/Department',
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                var select = $('#selectDepartment');
                select.empty();

                select.append($('<option>', {
                    value: '',
                    text: 'Choose...'
                }));

                response.allData.forEach(function (department) {
                    select.append($('<option>', {
                        value: department.dept_Id,
                        text: department.name
                    }));
                });

            },
        });
    }

    // Memanggil fungsi Departments untuk mengisi dropdown departemen 
    Departments();
});

$(document).ready(function () {
let i = 1;
var t = $("#tbActiveEmployees").DataTable({
    "paging": true,
    "responsive": true,
    "lengthChange": true,
    "autoWidth": false,
    "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"],
    "searching": true,
    "ordering": true,
    "info": true,

    "ajax": {
        url: "https://localhost:7032/api/Employee/Pegawaiaktif",
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
        {
            data: "fullname"
        },
        { data: "email" },
        { data: "department" },
        
        ], "order": [],

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

$(document).ready(function () {
    let i = 1;
    var t = $("#tbdeActiveEmployees").DataTable({
        "paging": true,
        "responsive": true,
        "lengthChange": true,
        "autoWidth": false,
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"],
        "searching": true,
        "ordering": true,
        "info": true,

        "ajax": {
            url: "https://localhost:7032/api/Employee/Pegawaideaktif",
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
            {
                data: "fullname"
            },
            { data: "email" },
            { data: "department" },

           ], "order": [],

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
function ResetText() {
    $('#exampleModalLabel').text("Employess")
    $('#employeeName').val("");
    $('#employeeLastName').val("");
    $('#employeeEmail').val("");
    $('#employeePhoneNumber').val("");
    $('#employeeAddress').val("");
    $('#employeeDepartment').val("");
    $('#employeeStatus').val("");
    $('#Edit').addClass("d-none")
    $('#Save').removeClass("d-none")
    /* $('#Save').hide() */
}
function Save() {
    debugger
    $('#exampleModal').modal("hide");
    var Employee = new Object();
    Employee.firstName = $('#employeeName').val();
    Employee.lastName = $('#employeeLastName').val();
    Employee.phoneNumber = $('#employeePhoneNumber').val();
    Employee.address = $('#employeeAddress').val();
    Employee.status = $('input[name="customRadio"]:checked').val() === "true";
    Employee.department_Id = $('#selectDepartment').val();
    
    if (!Employee.firstName || !Employee.lastName || !Employee.phoneNumber || !Employee.address || !Employee.department_Id || typeof Employee.status === 'undefined') {
        Swal.fire(
            'Gagal!',
            'Data Employee tidak boleh kosong',
            'warning'
        )
        return;
    }
    $.ajax({
        type: 'POST',
        url: 'https://localhost:7032/api/Employee',
        data: JSON.stringify(Employee),
        contentType: "application/json; charset=utf-8",
    }).then((result) => {
        if (result.status == 200) {
            Swal.fire({
                icon: 'success',
                title: 'Berhasil',
                text: result.response,
            })
            $('#tbEmployees').DataTable().ajax.reload();
        } else {
            alert("Data Gagal Ditambahkan")
        }
    })
}

function Delete(nik) {
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
                url: 'https://localhost:7032/api/Employee/' + nik,
                contentType: "application/json; charset=utf-8",
            }).then((result) => {
                if (result.status == 200) {

                    Swal.fire(
                        'Deleted!',
                        result.response,
                        'success'
                    )
                    $('#tbEmployees').DataTable().ajax.reload();
                } else {
                    alert("Data Gagal Dihapus")
                }

            })
        }
    })
}

function GetById(nik) {
    $.ajax({
        url: "https://localhost:7032/api/Employee/" + nik,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            var obj = result.get;
            $('#Id').val(obj.nik);
            $('#employeeName').val(obj.firstName);
            $('#employeeLastName').val(obj.lastName);
            $('#employeeEmail').val(obj.email);
            $('#employeePhoneNumber').val(obj.phoneNumber);
            $('#employeeAddress').val(obj.address);
            $('#employeeDepartment').val(obj.department_Id);
            $('#employeeStatus').val(obj.status);
            $('#exampleModalLabel').text("Edit Employee")
            $('#Save').addClass("d-none")
            $('#Edit').removeClass("d-none")
            $('#exampleModal').modal('show');

        }
    });
}
function Edit() {
    $('#exampleModal').modal("hide");
    var Employee = new Object();
    Employee.nik = $('#Id').val();
    Employee.firstName = $('#employeeName').val();
    Employee.lastName = $('#employeeLastName').val();
    Employee.email = $('#employeeEmail').val();
    Employee.phoneNumber = $('#employeePhoneNumber').val();
    Employee.address = $('#employeeAddress').val();
    Employee.department_Id = $('#employeeDepartment').val();
    Employee.status = $('input[name="customRadio"]:checked').val() === "true";

    if (Employee.name === "") {
        Swal.fire(
            'Gagal!',
            'Nama Department tidak boleh kosong',
            'warning'
        )
        return;
    }

    $.ajax({
        type: 'PUT',
        url: 'https://localhost:7032/api/Employee/' + Employee.nik,
        data: JSON.stringify(Employee),
        contentType: "application/json; charset=utf-8",
    }).then((result) => {
        if (result.status === 200) {
            Swal.fire({
                icon: 'success',
                title: 'Edited',
                text: result.response,
            });
        } else if (result.status === 201) {
            Swal.fire({
                icon: 'error',
                title: 'Gagal',
                text: result.response,
            });
        } else {
            // Tindakan lain jika respons tidak sesuai dengan yang diharapkan
            console.log(result); // Debug respons
        }

        $('#tbEmployees').DataTable().ajax.reload();
    }).fail((jqXHR, textStatus, errorThrown) => {
        // Tangani kesalahan permintaan AJAX di sini
        Swal.fire({
            icon: 'error',
            title: 'Kesalahan',
            text: 'Terjadi kesalahan saat mengirim permintaan AJAX: ' + errorThrown,
        });
    });
}





