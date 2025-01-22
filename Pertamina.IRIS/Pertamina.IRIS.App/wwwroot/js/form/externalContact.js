$(function () {
    var dt;
    dt = $("#dataTable").DataTable({
        searchDelay: 500,
        processing: true,
        serverSide: true,
        retrieve: true,
        info: true,
        order: [],
        stateSave: false,

        ajax: {
            url: param.dataGrid,
            method: "POST",
            dataType: "json"
        },
        columns: [
            { data: 'NamaCompany', "name": "NamaCompany" },
            { data: 'NamaPerson', "name": "NamaPerson" },
            { data: 'JabatanPerson', "name": "JabatanPerson" },
            { data: 'NoTelpPerson', "name": "NoTelpPerson" },
            { data: 'EmailPerson', "name": "EmailPerson" },
            {
                data: 'UpdateDate',
                "name": "UpdateDate",
                "render": function (data, type, row) {
                    if (data === null) {
                        return null
                    }
                    else {
                        data === "UpdateDate";
                        return moment(new Date(data)).format('DD/MM/YYYY');
                    }
                }
            },
            { data: null },
        ],
        columnDefs: [
            {
                targets: -1,
                data: null,
                orderable: false,
                className: 'text-end',
                render: function (data, type, row) {
                    return "" +
                        "<a class='btn btn-light btn-active-light-primary btn-sm' data-kt-menu-trigger='click' data-kt-menu-placement='bottom-end' data-kt-menu-flip='top-end'>" +
                        "Actions <i class='ki-duotone ki-down fs-5 ms-1'></i>" +
                        "</a>" +
                        "<div class='menu menu-sub menu-sub-dropdown menu-column menu-rounded menu-gray-600 menu-state-bg-light-primary fw-bold fs-7 w-125px py-4' data-kt-menu='true'>" +
                        "<div class='menu-item px-3'>" +
                        "<a class='menu-link px-3' data-kt-docs-table-filter='edit_row' onclick=\"location.href='/" + param.urlAction + "Read?guid=" + data.Id + "'\">" +
                        "View" +
                        "</a>" +
                        "</div>" +
                        "<div class='menu-item px-3'>" +
                        "<a class='menu-link px-3' data-kt-docs-table-filter='edit_row' onclick=\"location.href='/" + param.urlAction + "Duplicate?guid=" + data.Id + "'\">" +
                        "Duplicate" +
                        "</a>" +
                        "</div>" +
                        "<div class='menu-item px-3'>" +
                        "<a class='menu-link px-3' data-kt-docs-table-filter='edit_row' onclick=\"location.href='/" + param.urlAction + "Update?guid=" + data.Id + "'\">" +
                        "Edit" +
                        "</a>" +
                        "</div>" +
                        "<div class='menu-item px-3'>" +
                        "<a class='menu-link px-3' data-kt-docs-table-filter='delete_row' onclick='deleteAlertRedirect(\"" + data.Id + "\")'>" +
                        "Delete" +
                        "</a>" +
                        "</div>" +
                        "</div>"
                    "";
                },
            },
        ],
    });

    dt.on('draw', function () {
        KTMenu.createInstances();
    });

    const filterSearch = document.querySelector('[data-kt-docs-table-filter="search"]');
    filterSearch.addEventListener('keyup', function (e) {
        dt.search(e.target.value).draw();
    });

});

$(function () {

    const form = document.getElementById('CreateFormExternalContact');
    var validator = FormValidation.formValidation(
        form,
        {
            fields: {
                'NamaCompany': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'AlamatCompany': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'EmailCompany': {
                    validators: {
                        emailAddress: {
                            message: "The value is not a valid email address"
                        }
                    }
                },
                'Person.NamaPerson': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'Person.JabatanPerson': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'Person.EmailPerson': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'Person.NoTelpPerson': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                }
            },

            plugins: {
                trigger: new FormValidation.plugins.Trigger(),
                bootstrap: new FormValidation.plugins.Bootstrap5({
                    rowSelector: '.fv-row',
                    eleInvalidClass: '',
                    eleValidClass: ''
                })
            }
        }
    );

    const submitButton = document.getElementById('CreateSubmitExternalContact');
    submitButton.addEventListener('click', function (e) {
        e.preventDefault();
        var callBackUrlPartial = $(this).data("callbackurl");
        if (validator) {
            validator.validate().then(function (status) {
                if (status == 'Valid') {
                    Swal.fire({
                        text: "Are you sure want to submit this data ?",
                        icon: "info",
                        buttonsStyling: false,
                        showCancelButton: true,
                        confirmButtonText: "Yes, submit it!",
                        cancelButtonText: 'Nope, cancel it',
                        customClass: {
                            confirmButton: "btn btn-primary",
                            cancelButton: 'btn btn-danger'
                        }
                    }).then((result) => {
                        if (result.isConfirmed) {
                            submitButton.setAttribute('data-kt-indicator', 'on');
                            submitButton.disabled = true;

                            var urlSubmit = submitButton.dataset.url;

                            var persons = []; 

                            $('.pic').each(function () {
                                var namaPic = $(this).find('#namaPicInput').val();
                                var jabatanPic = $(this).find('#jabatanPicInput').val();
                                var emailPic = $(this).find('#emailPicInput').val();
                                var noTelpPic = $(this).find('#noTelpPicInput').val();

                                var person = {
                                    NamaPerson: namaPic,
                                    JabatanPerson: jabatanPic,
                                    EmailPerson: emailPic,
                                    NoTelpPerson: noTelpPic
                                };

                                persons.push(person);
                            });

                            var model = {
                                Id: $("#Id").val(),
                                NamaCompany: $("#namaCompanyInput").val(),
                                KoneksiCompany: $("#koneksiCompanyInput").val(),
                                AlamatCompany: $("#alamatCompanyInput").val(),
                                EmailCompany: $("#emailCompanyInput").val(),
                                Remark: $("#remarkInput").val(),
                                Persons: persons
                            };

                            $.ajax({
                                type: "POST",
                                url: urlSubmit,
                                data: model,
                                success: function (data) {
                                    if (data.IsError) {
                                        submitButton.removeAttribute('data-kt-indicator');
                                        submitButton.disabled = false;
                                        Swal.fire({
                                            text: data.ErrorMessage,
                                            icon: 'error',
                                            buttonsStyling: false,
                                            confirmButtonText: "Ok, got it!",
                                            customClass: {
                                                confirmButton: "btn btn-primary"
                                            }
                                        });
                                    }
                                    else {
                                        submitButton.removeAttribute('data-kt-indicator');
                                        submitButton.disabled = false;
                                        Swal.fire({
                                            text: "Form has been successfully submitted!",
                                            icon: "success",
                                            buttonsStyling: false,
                                            confirmButtonText: "Ok, got it!",
                                            customClass: {
                                                confirmButton: "btn btn-primary",

                                            }
                                        }).then((result) => {
                                            document.location = callBackUrlPartial;
                                        });;
                                    }
                                },
                                error: function () {
                                    alert("Error while inserting data");
                                }
                            });
                        };
                    });
                }
            });
        }
    });

    var isFirstRepeater = true; 

    $('#addRepeaterPic').repeater({
        initEmpty: false,
        show: function () {
            $(this).slideDown();

            if (isFirstRepeater) {
                $(this).find('[data-repeater-delete]').prop('disabled', false);
                isFirstRepeater = false; 
            }
        },
        hide: function (deleteElement) {
            $(this).slideUp(deleteElement);
        }
    });

    $('#addRepeaterPic [data-repeater-delete]').prop('disabled', true);
});

$(function () {

    const form = document.getElementById('EditFormExternalContact');
    var validator = FormValidation.formValidation(
        form,
        {
            fields: {
                'NamaCompany': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'AlamatCompany': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'EmailCompany': {
                    validators: {
                        emailAddress: {
                            message: "The value is not a valid email address"
                        }
                    }
                },
                'NamaPerson': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'JabatanPerson': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'EmailPerson': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'NoTelpPerson': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                }
            },

            plugins: {
                trigger: new FormValidation.plugins.Trigger(),
                bootstrap: new FormValidation.plugins.Bootstrap5({
                    rowSelector: '.fv-row',
                    eleInvalidClass: '',
                    eleValidClass: ''
                })
            }
        }
    );

    const updateButton = document.getElementById('UpdateSubmitExternalContact');
    updateButton.addEventListener('click', function (e) {
        e.preventDefault();
        var callBackUrlPartial = $(this).data("callbackurl");
        if (validator) {
            validator.validate().then(function (status) {
                if (status == 'Valid') {
                    Swal.fire({
                        text: "Are you sure want to update this data ?",
                        icon: "info",
                        buttonsStyling: false,
                        showCancelButton: true,
                        confirmButtonText: "Yes, update it!",
                        cancelButtonText: 'Nope, cancel it',
                        customClass: {
                            confirmButton: "btn btn-primary",
                            cancelButton: 'btn btn-danger'
                        }
                    }).then((result) => {
                        if (result.isConfirmed) {
                            updateButton.setAttribute('data-kt-indicator', 'on');
                            updateButton.disabled = true;

                            var urlSubmit = updateButton.dataset.url;
                            var id = $('#hdnExternalContactId').val();

                            var model = {
                                Id: id
                            };

                            model.NamaCompany = $("#editnamaCompanyInput").val();
                            model.AlamatCompany = $("#editalamatCompanyInput").val();
                            model.EmailCompany = $("#editemailCompanyInput").val();
                            model.KoneksiCompany = $("#editkoneksiCompanyInput").val();
                            model.Remark = $("#editremarkInput").val();
                            model.NamaPerson = $("#editnamaPicInput").val();
                            model.JabatanPerson = $("#editjabatanPicInput").val();
                            model.EmailPerson = $("#editemailPicInput").val();
                            model.NoTelpPerson = $("#editnoTelpPicInput").val();

                            $.ajax({
                                type: "POST",
                                url: urlSubmit,
                                data: model,
                                success: function (data) {
                                    if (data.IsError) {
                                        updateButton.removeAttribute('data-kt-indicator');
                                        updateButton.disabled = false;
                                        Swal.fire({
                                            text: data.ErrorMessage,
                                            icon: 'error',
                                            buttonsStyling: false,
                                            confirmButtonText: "Ok, got it!",
                                            customClass: {
                                                confirmButton: "btn btn-primary"
                                            }
                                        });
                                    }
                                    else {
                                        updateButton.removeAttribute('data-kt-indicator');
                                        updateButton.disabled = false;
                                        Swal.fire({
                                            text: "Form has been successfully updated!",
                                            icon: "success",
                                            buttonsStyling: false,
                                            confirmButtonText: "Ok, got it!",
                                            customClass: {
                                                confirmButton: "btn btn-primary",

                                            }
                                        }).then((result) => {
                                            document.location = callBackUrlPartial;
                                        });;
                                    }
                                },
                                error: function () {
                                    alert("Error while inserting data");
                                }
                            });
                        };
                    });
                }
            });
        }
    });

});

$(function () {

    const form = document.getElementById('DuplicateFormExternalContact');
    var validator = FormValidation.formValidation(
        form,
        {
            fields: {
                'NamaCompany': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'AlamatCompany': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'EmailCompany': {
                    validators: {
                        emailAddress: {
                            message: "The value is not a valid email address"
                        }
                    }
                },
                'Person.NamaPerson': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'Person.JabatanPerson': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'Person.EmailPerson': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'Person.NoTelpPerson': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                }
            },

            plugins: {
                trigger: new FormValidation.plugins.Trigger(),
                bootstrap: new FormValidation.plugins.Bootstrap5({
                    rowSelector: '.fv-row',
                    eleInvalidClass: '',
                    eleValidClass: ''
                })
            }
        }
    );

    const addDuplicateButton = document.getElementById('AddDuplicateSubmitExternalContact');
    addDuplicateButton.addEventListener('click', function (e) {
        e.preventDefault();
        var callBackUrlPartial = $(this).data("callbackurl");
        if (validator) {
            validator.validate().then(function (status) {
                if (status == 'Valid') {
                    Swal.fire({
                        text: "Are you sure want to submit this data ?",
                        icon: "info",
                        buttonsStyling: false,
                        showCancelButton: true,
                        confirmButtonText: "Yes, submit it!",
                        cancelButtonText: 'Nope, cancel it',
                        customClass: {
                            confirmButton: "btn btn-primary",
                            cancelButton: 'btn btn-danger'
                        }
                    }).then((result) => {
                        if (result.isConfirmed) {
                            addDuplicateButton.setAttribute('data-kt-indicator', 'on');
                            addDuplicateButton.disabled = true;

                            var urlSubmit = addDuplicateButton.dataset.url;
                            var id = $('#hdnExternalContactId').val();

                            var persons = []; // Array untuk menyimpan data person

                            $('.picDuplicate').each(function () {
                                var namaPic = $(this).find('#duplicatenamaPicInput').val();
                                var jabatanPic = $(this).find('#duplicatejabatanPicInput').val();
                                var emailPic = $(this).find('#duplicateemailPicInput').val();
                                var noTelpPic = $(this).find('#duplicatenoTelpPicInput').val();

                                var person = {
                                    NamaPerson: namaPic,
                                    JabatanPerson: jabatanPic,
                                    EmailPerson: emailPic,
                                    NoTelpPerson: noTelpPic
                                };

                                persons.push(person);
                            });

                            var model = {
                                Id: id,
                                NamaCompany: $("#duplicatenamaCompanyInput").val(),
                                KoneksiCompany: $("#duplicatekoneksiCompanyInput").val(),
                                AlamatCompany: $("#duplicatealamatCompanyInput").val(),
                                EmailCompany: $("#duplicateemailCompanyInput").val(),
                                Remark: $("#duplicateremarkInput").val(),
                                Persons: persons
                            };


                            $.ajax({
                                type: "POST",
                                url: urlSubmit,
                                data: model,
                                success: function (data) {
                                    if (data.IsError) {
                                        addDuplicateButton.removeAttribute('data-kt-indicator');
                                        addDuplicateButton.disabled = false;
                                        Swal.fire({
                                            text: data.ErrorMessage,
                                            icon: 'error',
                                            buttonsStyling: false,
                                            confirmButtonText: "Ok, got it!",
                                            customClass: {
                                                confirmButton: "btn btn-primary"
                                            }
                                        });
                                    }
                                    else {
                                        addDuplicateButton.removeAttribute('data-kt-indicator');
                                        addDuplicateButton.disabled = false;
                                        Swal.fire({
                                            text: "Form has been successfully submitted!",
                                            icon: "success",
                                            buttonsStyling: false,
                                            confirmButtonText: "Ok, got it!",
                                            customClass: {
                                                confirmButton: "btn btn-primary",

                                            }
                                        }).then((result) => {
                                            document.location = callBackUrlPartial;
                                        });;
                                    }
                                },
                                error: function () {
                                    alert("Error while inserting data");
                                }
                            });
                        };
                    });
                }
            });
        }
    });

});

function onlynum(idName) {
    var ip = document.getElementById(idName);
    var res = ip.value;

    if (res != '') {
        if (isNaN(res)) {
            ip.value = "";
            return false;
        } else {
            return true
        }
    }
}

function deleteAlertRedirect(guid) {
    Swal.fire({
        text: "Are you sure want to delete this data? ",
        icon: "info",
        buttonsStyling: false,
        showCancelButton: true,
        confirmButtonText: "Yes, delete it!",
        cancelButtonText: 'Nope, cancel it',
        customClass: {
            confirmButton: "btn btn-primary",
            cancelButton: 'btn btn-danger'
        }
    }).then((result) => {

        if (result.isConfirmed) {
            var urlDelete = param.deleteGrid + "?guid=" + guid;
            $.ajax({
                type: "GET",
                url: urlDelete,
                success: function (data) {
                    if (data.IsError) {
                        Swal.fire({
                            text: data.ErrorMessage,
                            icon: 'error',
                            buttonsStyling: false,
                            confirmButtonText: "Ok, got it!",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        });
                    }
                    else {
                        Swal.fire({
                            text: "Data has been successfully deleted!",
                            icon: "success",
                            buttonsStyling: false,
                            confirmButtonText: "Ok, got it!",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        }).then((result) => {
                            location.reload();
                        });
                    }
                },
                error: function () {
                    alert("Error while deleting data");
                }
            });
        }
    });
}
Dropzone.autoDiscover = false;

$(document).ready(function () {

    $("#dropzone").dropzone({
        url: param.importData,
        autoProcessQueue: false,
        parallelUploads: 1,
        maxFilesize: 10,
        acceptedFiles: ".xlsx, .xls",
        addRemoveLinks: false,

        init: function () {

            // Event saat tombol "Upload" diklik
            $("#uploadButton").click(function () {
                dropzone.processQueue(); // Mulai proses upload
            });

            var dropzone = this;
            dropzone.on("error", function (file, errorMessage, xhr) {
                // Handle respons error
                console.log("Error uploading file");
                console.log(errorMessage);

                // Tampilkan popup SweetAlert dengan pesan error dari respons JSON
                Swal.fire({
                    title: "Error",
                    html: errorMessage,
                    icon: "error",
                    buttonsStyling: false,
                    confirmButtonText: "Ok, got it!",
                    customClass: {
                        confirmButton: "btn btn-primary",

                    }
                });
            });

            dropzone.on("success", function (file, response) {
                // Handle respons setelah upload berhasil
                console.log("File uploaded successfully");
                console.log(response);

                // Tampilkan popup SweetAlert
                if (response && response.IsError) {
                    // Tampilkan pesan error jika IsError = true
                    Swal.fire({
                        title: "Upload failed, please fix your data and re-upload again.",
                        html: response.ErrorMessage,
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: "Ok, got it!",
                        customClass: {
                            confirmButton: "btn btn-primary",
                        },
                    }).then((result) => {
                        if (result.isConfirmed) {
                            $("#importModal").modal("hide");
                            location.reload();
                        }
                    });
                } else {
                    // Tampilkan popup SweetAlert jika upload berhasil
                    Swal.fire({
                        title: "Success",
                        html: "File uploaded successfully",
                        icon: "success",
                        buttonsStyling: false,
                        confirmButtonText: "Ok, got it!",
                        customClass: {
                            confirmButton: "btn btn-primary",
                        },
                    }).then((result) => {
                        if (result.isConfirmed) {
                            $("#importModal").modal("hide");
                            location.reload();
                        }
                    });
                }
            });

            $("#btnClose").click(function () {
                var dropzone = Dropzone.forElement("#dropzone");
                dropzone.removeAllFiles();
            });
        }
    });



var isFirstRepeater = true;
$('#duplicateRepeaterPic').repeater({
    initEmpty: false,
    show: function () {
        $(this).slideDown();

        if (isFirstRepeater) {
            $(this).find('[data-repeater-delete]').prop('disabled', false);
            isFirstRepeater = false;
        }
    },
    hide: function (deleteElement) {
        $(this).slideUp(deleteElement);
    }
});
$('#duplicateRepeaterPic [data-repeater-delete]').prop('disabled', true);
});

