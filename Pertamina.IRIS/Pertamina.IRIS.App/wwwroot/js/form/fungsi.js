﻿$(function () {
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
            { data: 'NameEntitasPertamina', "name": "NameEntitasPertamina" },
            { data: 'FungsiName', "name": "FungsiName" },
            { data: 'Description', "name": "Description" },
            {
                data: 'UpdateDate',
                "name": "UpdateDate",
                "render": function (data, type, row) {
                    if (data === null) {
                        return null
                    }
                    else {
                        data === "UpdatedDate";
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
                        "<a class='menu-link px-3' data-kt-docs-table-filter='edit_row' onclick='updateRedirect(\"" + data.Id + "\",\"" + data.EntitasPertaminaId + "\")'>" +
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

    $('button[data-toggle="detail-modal"]').click(function (event) {
        var urlPartial = $(this).data("url");
        var detailModal = $('#CreatePartialModal');
        let target = $(event.currentTarget).data('target');
        $.get(urlPartial).done(function (response) {
            detailModal.html(response);
            KTDrawer.createInstances();
            var drawerElement = document.querySelector(target);
            var drawer = KTDrawer.getInstance(drawerElement);

            drawer.toggle();

            const form = document.getElementById('CreateForm');
            var validator = FormValidation.formValidation(
                form,
                {
                    fields: {
                        'EntitasPertaminaId': {
                            validators: {
                                notEmpty: {
                                    message: 'field can not be empty!'
                                }
                            }
                        },
                        'FungsiName': {
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

            const submitButton = document.getElementById('CreateSubmit');
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

                                    var model = {};
                                    model.EntitasPertaminaId = $("#ddlEntitasPertaminaId").val();
                                    model.FungsiName = $("#fungsiNameInput").val();
                                    model.Description = $("#descriptionInput").val();

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

            $('#ddlEntitasPertaminaId').select2({
                ajax: {
                    url: url.ddlEntitasPertamina,
                    method: 'GET',
                    data: function (params) {
                        return {
                            query: params.term
                        };
                    },
                    processResults: function (data) {
                        return {
                            results: data.items
                        };
                    },
                    minimumInputLength: 2
                }
            });
        })
    })
});

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

function updateRedirect(guid, EntitasPertaminaId) {
    var urlPartial = param.updateGrid + "?guid=" + guid;
    var detailModal = $('#UpdatePartialModal');
    $.get(urlPartial).done(function (response) {
        detailModal.html(response);
        KTDrawer.createInstances();
        var drawerElement = document.querySelector("#UpdatePartialComponent");
        var drawer = KTDrawer.getInstance(drawerElement);

        drawer.toggle();
        const form = document.getElementById('UpdateForm');
        var validator = FormValidation.formValidation(
            form,
            {
                fields: {
                    'EntitasPertaminaId': {
                        validators: {
                            notEmpty: {
                                message: 'field can not be empty!'
                            }
                        }
                    },
                    'FungsiName': {
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

        const submitButton = document.getElementById('UpdateSubmit');
        submitButton.addEventListener('click', function (e) {
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
                                submitButton.setAttribute('data-kt-indicator', 'on');
                                submitButton.disabled = true;

                                var urlSubmit = submitButton.dataset.url;

                                var model = {};
                                model.Id = $("#Id").val();
                                model.EntitasPertaminaId = $("#ddlEntitasPertaminaIdUpdate").val();
                                model.FungsiName = $("#fungsiNameInput").val();
                                model.Description = $("#descriptionInput").val();

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

        $('#ddlEntitasPertaminaIdUpdate').select2({
            ajax: {
                url: url.ddlEntitasPertamina,
                method: 'GET',
                data: function (params) {
                    return {
                        query: params.term
                    };
                },
                processResults: function (data) {
                    return {
                        results: data.items
                    };
                },
                minimumInputLength: 2
            }
        });
        ajaxPreselecting(url.getDdlEntitasPertamina + "?guid=" + EntitasPertaminaId, $('#ddlEntitasPertaminaIdUpdate'));
    });
};

function ajaxPreselecting(urlAjaxPreselecting, ctrlClass) {
    $.ajax({
        type: 'GET',
        url: urlAjaxPreselecting
    }).then(function (data) {
        var option = new Option(data.items.text, data.items.id, true, true);
        ctrlClass.append(option).trigger('change');
        ctrlClass.trigger({
            type: 'select2:select',
            params: {
                data: data
            }
        });
    });
};