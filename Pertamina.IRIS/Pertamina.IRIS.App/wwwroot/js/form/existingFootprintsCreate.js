//CreateForm

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
            { data: 'Year', "name": "Year" },
            { data: 'Revenue', "name": "Revenue" },
            { data: 'TotalAsset', "name": "TotalAsset" },
            { data: 'Ebitda', "name": "Ebitda" },
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
                        "<a class='menu-link px-3' data-kt-docs-table-filter='edit_row' onclick='updateRedirect(\"" + data.Id + "\")'>" +
                        "Edit" +
                        "</a>" +
                        "</div>" +
                        "<div class='menu-item px-3'>" +
                        "<a class='menu-link px-3' data-kt-docs-table-filter='delete_row' onclick='deleteAlertRedirect(\"" + data.Id + "\", \"" + data.ExistingFootprintsId + "\")'>" +
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
            //var drawerElement = document.querySelector(target);
            var drawerElement = document.querySelector(target);
            var drawer = KTDrawer.getInstance(drawerElement);

            drawer.toggle();

            const form = document.getElementById('CreateForm');
            var validator = FormValidation.formValidation(
                form,
                {
                    fields: {
                        'OperatingMetrics.Year': {
                            validators: {
                                notEmpty: {
                                    message: 'field can not be empty!'
                                }
                            }
                        },
                        'OperatingMetrics.Revenue': {
                            validators: {
                                notEmpty: {
                                    message: 'field can not be empty!'
                                }
                            }
                        },
                        'OperatingMetrics.TotalAsset': {
                            validators: {
                                notEmpty: {
                                    message: 'field can not be empty!'
                                }
                            }
                        },
                        'OperatingMetrics.Ebitda': {
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

                                    var model = {
                                        PicFungsis: [],
                                        Partnerss: [],
                                        Heads: [],
                                        OperatingMetrics: {}
                                    };
                                    var createdDate = $("#createdDate").val();
                                    if (createdDate != '') {
                                        $('.editPartner').each(function () {

                                            var partners = $(this).find('#editPartnerInput').val();
                                            var location = $(this).find('#editPartnerLocation').val();
                                            var partner = {
                                                Partners: partners,
                                                Location: location
                                            };
                                            model.Partnerss.push(partner);
                                        });
                                        var isPicLevelMemberId = $('#picLevelMemberId').val();
                                        $('.editPic').each(function (index) {
                                            var picFungsiMember = $(this).find('#editPicFungsiMemberInput').val();
                                            var picFungsiMemberAjax = $(this).find('#editPicFungsiMemberInput' + index).val();
                                            var picLevelMember = isPicLevelMemberId;
                                            if (picFungsiMember == undefined && picFungsiMemberAjax != undefined) {
                                                var pic = {
                                                    PicFungsiId: picFungsiMemberAjax,
                                                    PicLevelId: picLevelMember,
                                                };
                                            }
                                            if (picFungsiMemberAjax == undefined && picFungsiMember != undefined) {
                                                var pic = {
                                                    PicFungsiId: picFungsiMember,
                                                    PicLevelId: picLevelMember,
                                                };
                                            }

                                            if (pic != undefined) {
                                                if (pic.PicLevelId == null) {
                                                    pic.PicLevelId = isPicLevelMemberId
                                                }
                                                model.PicFungsis.push(pic);
                                            }
                                        });



                                        model.Id = $("#hdnExistingFootprintsId").val();
                                        model.StreamBusinessId = $("#editStreamBusinessInput").val();
                                        model.EntitasPertaminaId = $("#editHshEntitasInput").val();
                                        model.HubHeadId = $("#editHubHeadId").val();
                                        model.HubId = $("#editHubId").val();
                                        model.HubName = $("#editHshHubHeadInput").val();
                                        model.PicFungsiId = $("#editPicInput").val();
                                        model.PicLevelId = $("#editPicLevelInput").val();
                                        model.NegaraMitraId = $("#editHshNegaraMitraInput").val();

                                    } else {
                                        $('.partner').each(function () {

                                            var partners = $(this).find('#partnerInput').val();
                                            var location = $(this).find('#partnerLocation').val();
                                            var partner = {
                                                Partners: partners,
                                                Location: location
                                            };
                                            model.Partnerss.push(partner);
                                        });
                                        var isPicLevelMemberId = $('#picLevelMemberId').val();
                                        $('.pic').each(function (index) {

                                            var picFungsiMember = $(this).find('#picFungsiMemberInput').val();
                                            var picFungsiMemberAjax = $(this).find('#picFungsiMemberInput' + index).val();
                                            var picLevelMember = isPicLevelMemberId;
                                            if (picFungsiMember == undefined && picFungsiMemberAjax != undefined) {
                                                var pic = {
                                                    PicFungsiId: picFungsiMemberAjax,
                                                    PicLevelId: picLevelMember,
                                                };
                                            }
                                            if (picFungsiMemberAjax == undefined && picFungsiMember != undefined) {
                                                var pic = {
                                                    PicFungsiId: picFungsiMember,
                                                    PicLevelId: picLevelMember,
                                                };
                                            }

                                            if (pic != undefined) {
                                                if (pic.PicLevelId == null) {
                                                    pic.PicLevelId = isPicLevelMemberId
                                                }
                                                model.PicFungsis.push(pic);
                                            }
                                        });

                                        model.Id = $("#existingFootprintId").val();
                                        model.StreamBusinessId = $("#streamBusinessInput").val();
                                        model.EntitasPertaminaId = $("#hshEntitasInput").val();
                                        model.HubHeadId = $("#HubHeadId").val();
                                        model.HubId = $("#HubId").val();
                                        model.HubName = $("#hshHubHeadInput").val();
                                        model.PicFungsiId = $("#picInput").val();
                                        model.PicLevelId = $("#picLevelId").val();
                                        model.NegaraMitraId = $("#hshNegaraMitraInput").val();

                                    }
                                    $('.addOM').each(function () {

                                        var year = $("#yearOperatingMetrics").val();
                                        var revenue = $("#revenueOperatingMetrics").val();
                                        var totalAsset = $("#totalAssetOperatingMetrics").val();
                                        var ebitda = $("#ebitdaOperatingMetrics").val();
                                        var id = $("#existingFootprintId").val();
                                        var operatingMetric = {
                                            ExistingFootprintsId: id,
                                            Year: year,
                                            Revenue: revenue,
                                            TotalAsset: totalAsset,
                                            Ebitda: ebitda
                                        };

                                        model.OperatingMetrics.push(operatingMetric);

                                    });
                                    //operating metric model


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
        })
    })
});
function deleteAlertRedirect(operatingMetricId, guid) {
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
            var model = {
                PicFungsis: [],
                Partnerss: [],
                Heads: [],
                OperatingMetrics: {}
            };
            var createdDate = $("#createdBy").val();
            if (createdDate != undefined || createdDate == '') {
                $('.editPartner').each(function () {

                    var partners = $(this).find('#editPartnerInput').val();
                    var location = $(this).find('#editPartnerLocation').val();
                    var partner = {
                        Partners: partners,
                        Location: location
                    };
                    model.Partnerss.push(partner);
                });
                var isPicLevelMemberId = $('#picLevelMemberId').val();
                $('.editPic').each(function (index) {
                    var picFungsiMember = $(this).find('#editPicFungsiMemberInput').val();
                    var picFungsiMemberAjax = $(this).find('#editPicFungsiMemberInput' + index).val();
                    var picLevelMember = isPicLevelMemberId;
                    if (picFungsiMember == undefined && picFungsiMemberAjax != undefined) {
                        var pic = {
                            PicFungsiId: picFungsiMemberAjax,
                            PicLevelId: picLevelMember,

                        };
                    }
                    if (picFungsiMemberAjax == undefined && picFungsiMember != undefined) {
                        var pic = {
                            PicFungsiId: picFungsiMember,
                            PicLevelId: picLevelMember,
                        };
                    }
                    if (pic != undefined) {
                        if (pic.PicLevelId == null) {
                            pic.PicLevelId = isPicLevelMemberId
                        }
                        model.PicFungsis.push(pic);
                    }


                });

                model.Id = $("#hdnExistingFootprintsId").val();
                model.StreamBusinessId = $("#editStreamBusinessInput").val();
                model.EntitasPertaminaId = $("#editHshEntitasInput").val();
                model.HubHeadId = $("#editHubHeadId").val();
                model.HubId = $("#editHubId").val();
                model.HubName = $("#editHshHubHeadInput").val();
                model.PicFungsiId = $("#editPicInput").val();
                model.PicLevelId = $("#picLevelId").val();
                model.NegaraMitraId = $("#editHshNegaraMitraInput").val();
            } else {
                $('.partner').each(function () {

                    var partners = $(this).find('#partnerInput').val();
                    var location = $(this).find('#partnerLocation').val();
                    var partner = {
                        Partners: partners,
                        Location: location
                    };
                    model.Partnerss.push(partner);
                });
                var isPicLevelMemberId = $('#picLevelMemberId').val();
                $('.pic').each(function (index) {

                    var picFungsiMember = $(this).find('#picFungsiMemberInput').val();
                    var picFungsiMemberAjax = $(this).find('#picFungsiMemberInput' + index).val();
                    var picLevelMember = isPicLevelMemberId;
                    if (picFungsiMember == undefined && picFungsiMemberAjax != undefined) {
                        var pic = {
                            PicFungsiId: picFungsiMemberAjax,
                            PicLevelId: picLevelMember,
                        };
                    }
                    if (picFungsiMemberAjax == undefined && picFungsiMember != undefined) {
                        var pic = {
                            PicFungsiId: picFungsiMember,
                            PicLevelId: picLevelMember,
                        };
                    }

                    if (pic != undefined) {
                        if (pic.PicLevelId == null) {
                            pic.PicLevelId = isPicLevelMemberId
                        }
                        model.PicFungsis.push(pic);
                    }
                });

                model.Id = guid;
                model.StreamBusinessId = $("#streamBusinessInput").val();
                model.EntitasPertaminaId = $("#hshEntitasInput").val();
                model.HubHeadId = $("#HubHeadId").val();
                model.HubId = $("#HubId").val();
                model.HubName = $("#hshHubHeadInput").val();
                model.PicFungsiId = $("#picInput").val();
                model.PicLevelId = $("#picLevelId").val();
                model.NegaraMitraId = $("#hshNegaraMitraInput").val();
            }

            //operating metric model
            var operatingMetric = {
                Id: operatingMetricId,
                ExistingFootprintsId: guid,
            };

            model.OperatingMetrics = operatingMetric;

            var urlDelete = param.deleteGrid;
            $.ajax({
                type: "POST",
                url: urlDelete,
                data: JSON.stringify(model),
                contentType: "application/json; charset=utf-8",
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
$(function () {
    const form = document.getElementById('CreateFormExistingFootprints');
    var validator = FormValidation.formValidation(
        form,
        {
            fields: {
                'StreamBusinessId': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'EntitasPertaminaId': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'NegaraMitraId': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'HubName': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'Head.HubHeadId': {
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
    const submitButton = document.getElementById('CreateSubmitExistingFootprints');
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
                            var model = {
                                PicFungsis: [],
                                Partnerss: [],
                                Heads: [],
                                OperatingMetricss: []
                            };

                            $('.partner').each(function () {

                                var partners = $(this).find('#partnerInput').val();
                                var location = $(this).find('#partnerLocation').val();
                                //var idPartner = $(this).find('#hdnPartnerId').val();
                                var existingFootprintId = $(this).find('#hdnExistingFootprintsId').val();
                                var partner = {
                                    Partners: partners,
                                    Location: location,
                                    //Id: idPartner,
                                    ExistingFootPrintsId: existingFootprintId
                                };
                                model.Partnerss.push(partner);
                            });


                            var isPicLevelMemberId = $('#picLevelMemberId').val();
                            $('.pic').each(function () {

                                var picFungsiMember = $(this).find('.fungsiPicMember').val();
                                var picLevelMember = isPicLevelMemberId
                                var pic = {
                                    PicFungsiId: picFungsiMember,
                                    PicLevelId: picLevelMember,
                                };

                                if (pic.PicLevelId == null) {
                                    pic.PicLevelId == isPicLevelMemberId;
                                }
                                model.PicFungsis.push(pic);
                            });

                            $('.addOM').each(function (index) {
                                var selectedYear = $(this).find('.yearOM').val();
                                var selectedRevenue = $(this).find('.revenueOM').val();
                                var selectedTotalAssets = $(this).find('.totalAssetsOM').val();
                                var selectedEbitda = $(this).find('.EbitdaOM').val();

                                var om = {
                                    Year: selectedYear,
                                    RevenueToString: selectedRevenue,
                                    TotalAssetToString: selectedTotalAssets,
                                    EbitdaToString: selectedEbitda
                                }
                                model.OperatingMetricss.push(om);
                            });

                            model.Id = $("#hdnExistingFootprintsId").val();
                            model.StreamBusinessId = $("#streamBusinessInput").val();
                            model.EntitasPertaminaId = $("#hshEntitasInput").val();
                            model.HubHeadId = $("#HubHeadId").val();
                            model.HubId = $("#HubId").val();
                            model.HubName = $("#hshHubHeadInput").val();
                            model.PicFungsiId = $("#picInput").val();
                            model.PicLevelId = $("#picLevelInput").val();
                            model.NegaraMitraId = $("#hshNegaraMitraInput").val();


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
    var valueStreamBusinessInput = $('#hdnStreamBusinessInput').val();
    $('#streamBusinessInput').select2({
        ajax: {
            url: url.ddlStreamBusiness,
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

    if (!valueStreamBusinessInput) {
        $('#streamBusinessInput').val(null).trigger('change');
    } else {
        ajaxPreselecting(url.getDdlStreamBusiness + "?guid=" + valueStreamBusinessInput, $('#streamBusinessInput'));
    }
    var valueEntitasInput = $('#hdnEntitasInput').val();
    $('#hshEntitasInput').select2({
        ajax: {
            url: url.ddlOpportunityEntitasPertamina,
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
    if (!valueEntitasInput) {
        $('#hshEntitasInput').val(null).trigger('change');
    }
    else {
        ajaxPreselecting(url.getDdlEntitasPertamina + "?guid=" + valueEntitasInput, $('#hshEntitasInput'));
    }
    var valueNegaraMitraInput = $('#hdnNegaraMitra').val();
    $('#hshNegaraMitraInput').select2({
        ajax: {
            url: url.ddlNegaraMitraOnlyNegara,
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
    if (!valueNegaraMitraInput) {
        $('#hshNegaraMitraInput').val(null).trigger('change');
    }
    else {
        ajaxPreselecting(url.getDdlNegaraMitraOnlyNegara + "?guid=" + valueNegaraMitraInput, $('#hshNegaraMitraInput'));
    }
    var valuePicInput = $('#hdnPicInput').val();
    $('#picInput').select2({
        ajax: {
            url: url.ddlOpportunityPic,
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
    if (!valuePicInput) {
        $('#picInput').val(null).trigger('change');
    }
    else {
        ajaxPreselecting(url.getDdlPicFungsi + "?guid=" + valuePicInput, $('#picInput'));
    }



    var valuePicLevelInput = $('#picLevelId').val();
    $.ajax({
        url: url.getDdlPicLevel + "/?guid=" + valuePicLevelInput,
        method: 'GET',
        data: { selectedValue: valuePicLevelInput },
        beforeSend: function () {
            clearFormDataPicLevelLead();
        },
        success: function (data) {
            updateFormPicLevelLead(data.items);
        },
        error: function (error) {
            console.error('Error retrieving PIC details:', error);
        }
    });
    function clearFormDataPicLevelLead() {
        $('#picLevelInput').val('');
    }
    function updateFormPicLevelLead(picLevelData) {
        $('#picLevelInput').val(picLevelData.text);
    }
    var valuePicLevelMemberInput = $('#picLevelMemberId').val();
    $.ajax({
        url: url.getDdlPicLevel + "/?guid=" + valuePicLevelMemberInput,
        method: 'GET',
        data: { selectedValue: valuePicLevelMemberInput },
        beforeSend: function () {
            clearFormDataPicLevelMember();
        },
        success: function (data) {
            updateFormPicLevelMember(data.items);
        },
        error: function (error) {
            console.error('Error retrieving PIC details:', error);
        }
    });
    function clearFormDataPicLevelMember() {
        $('#picLevelMemberInput').val('');
    }
    function updateFormPicLevelMember(picLevelData) {
        $('#picLevelMemberInput').val(picLevelData.text);
    }

    var valueHub = $('#hdnHub').val();
    $('#hubName').select2({
        placeholder: "Pilih RC PIC ...",
        ajax: {
            url: url.ddlHub,
            method: 'GET',
            data: function (params) {
                return {
                    query: params.term,
                };
            },
            processResults: function (data) {
                return {
                    results: data.items
                };
            },
            minimumInputLength: 2
        }
    }).on('change', function () {
        var selectedValue = $(this).val();
        $.ajax({
            url: url.getHubHeadByHubId + "/?guid=" + selectedValue,
            method: 'GET',
            data: { selectedValue: selectedValue },
            beforeSend: function () {
                clearFormData();
            },
            success: function (data) {
                updateFormBasedOnSelectedHubHead(data.items);
            },
            error: function (error) {
                console.error('Error retrieving PIC details:', error);
            }
        });
    });
    if (!valueHub || valueHub === emptyGuid) {
        $('#hubName').val(null).trigger('change');
    }
    else {
        ajaxPreselecting(url.getDdlHub + "?guid=" + valueHub, $('#hubName'));
    };
    function clearFormData() {
        // Replace these lines with your code to clear the form fields
        $('#hshHubHeadInput').val('');

    }
    function updateFormBasedOnSelectedHubHead(hubHeadData) {
        $('#hshHubHeadInput').val(hubHeadData.HubHeadName);
        $('#HubId').val(hubHeadData.HubId);
        $('#HubHeadId').val(hubHeadData.HubHeadId);
    }




    // REPEATER
    //Partner
    var guid = $('#hdnExistingFootprintsId').val();
    $.ajax({
        url: param.getExistingFootprintsById,
        method: 'GET',
        data: { guid: guid },

        success: function (result) {
            var data = result.items;
            console.log(data)
            console.log(url)
            var PartnerName = data.Partnerss;
            if (PartnerName.length != 0) {
                $("#addRepeaterPartner .partner").remove();
                PartnerName.forEach(function (Partner, index) {
                    var $repeaterItem = $("<div>").addClass("partner got mb-2");
                    var $formGroup = $("<div>").addClass("fv-row form-group row");
                    var $input = $("<input>").attr({
                        type: "hidden",
                        value: Partner.Id,
                        id: "hdnEntitasInput"
                    });
                    var $colLg5 = $("<div>").addClass("fv-row col-lg-5");
                    if (index == 0) {
                        var $label5 = $("<label>").attr({
                            for: "partnerInput",
                            class: "required form-label"
                        }).text("Partner(s)");
                        $colLg5.append($label5);
                    }

                    var $partnersInput = $("<input>").attr({
                        type: "text",
                        id: "partnerInput",
                        class: "form-control form-control-solid partnersFill",
                        maxlength: 100,
                        placeholder: "Masukkan Nama Partner",
                        value: Partner.Partners
                    });
                    var $colLg52 = $("<div>").addClass("fv-row col-lg-5");
                    if (index == 0) {
                        var $label52 = $("<label>").attr({
                            for: "partnerLocation",
                            class: "required form-label"
                        }).text("Partner Location");

                        $colLg52.append($label52);
                    }

                    var $locationInput = $("<input>").attr({
                        type: "text",
                        id: "partnerLocation",
                        class: "form-control form-control-solid locationFill",
                        maxlength: 100,
                        placeholder: "Masukkan Location Partner",
                        value: Partner.Location
                    });
                    var $colLg2 = $("<div>").addClass("col-lg-2");
                    var $deleteButton = $("<button>").addClass("border border-secondary btn btn-icon btn-flex btn-danger")
                        .attr("data-repeater-delete", "")
                        .attr("type", "button")
                        .html('<i class="la la-close"></i>');


                    $colLg5.append($partnersInput);

                    $colLg52.append($locationInput);
                    $colLg2.append($deleteButton);
                    $formGroup.append($input, $colLg5, $colLg52, $colLg2);
                    $repeaterItem.append($formGroup);
                    $("#addRepeaterPartner [data-repeater-list='PartnerName']").append($repeaterItem);
                });

                $('#addRepeaterPartner').repeater({
                    initEmpty: false,

                    show: function () {
                        $(this).slideDown();


                    },

                    hide: function (deleteElement) {
                        $(this).slideUp(deleteElement);
                    }
                }).find('.partner').each(function (index) {
                    if (index === 0) {
                        $(this).find('[data-repeater-delete]').css('display', 'none');
                    }
                });
                $('#addRepeaterPartner').on('click', '[data-repeater-delete]', function () {
                    $(this).closest('.partner').slideUp(function () {
                        $(this).remove();
                    });
                });
            };

        },
        error: function (error) {
            console.log(error);
        }
    });
    $('#addRepeaterPartner').repeater({
        initEmpty: false,

        show: function () {
            $(this).slideDown();
            //$('#addRepeaterPartner').find('[data-repeater-create]').addClass('disabled'); 
            UpdateLabelPartner()
        },

        hide: function (deleteElement) {
            $(this).slideUp(deleteElement);
            UpdateLabelPartner()
        }
    }).find('[data-repeater-delete]').css('display', 'none');
    function UpdateLabelPartner() {
        $('#addRepeaterPartner [data-repeater-item]').each(function (index) {
            var a = $('#addRepeaterPartner .got')
            if (a.length != 0) {
                $(this).find('label[for="partnerInputs"]').hide();
                $(this).find('label[for="partnerLocation"]').hide();
            } else {
                if (index > 0) {
                    $(this).find('label[for="partnerInputs"]').hide();
                    $(this).find('label[for="partnerLocation"]').hide();
                }
            }
        });
    };

    // PIC
    var guid = $('#hdnExistingFootprintsId').val();
    var levelId = $('#picLevelMemberId').val();
    $.ajax({
        url: param.getExistingFootprintsById,
        method: 'GET',
        data: { guid: guid, levelId: levelId },

        success: function (result) {
            if (result.items.PicFungsis.length !== 0) {
                var data = result.items;
                var PicName = data.PicFungsis;
                $("#addRepeaterPic .pic").remove();

                PicName.forEach(function (Pic, index) {
                    var $repeaterItem = $("<div>").addClass("pic mb-2").attr("data-repeater-item", true);
                    var $formGroup = $("<div>").addClass("fv-row form-group row");

                    var $input = $("<input>").attr({
                        type: "hidden",
                        value: Pic.Id,
                        id: "hdnPicInput"
                    });

                    var $colLg5 = $("<div>").addClass("fv-row col-lg-5 picFungsi");
                    var $picFungsiMemberInput = $("<select>").attr({
                        id: "picFungsiMemberInput" + index,
                        class: "form-select form-select-solid form-select-md fw-semibold fungsiPicMember",
                        "aria-label": "Pilih Nama PIC - Nama Fungsi - Perusahaan"
                    });

                    var $colLg52 = $("<div>").addClass("fv-row col-lg-5");
                    var $picLevelMemberInput = $("<input>").attr({
                        id: "picLevelMemberInput",
                        name: "PicMemberLevelId",
                        type: "text",
                        class: "form-control form-control-solid levelPicMember",
                        maxlength: 6000,
                        placeholder: "Pilih Nama Hub Head",
                        readonly: true
                    });


                    var $colLg2 = $("<div>").addClass("col-lg-2 d-flex align-items-end");
                    var $deleteButton = $("<button>").addClass("border border-secondary btn btn-icon btn-flex btn-danger")
                        .attr("data-repeater-delete", "")
                        .attr("type", "button")
                        .html('<i class="la la-close"></i>');


                    $colLg5.append($picFungsiMemberInput);
                    $colLg52.append($picLevelMemberInput);
                    $colLg2.append($deleteButton);
                    $formGroup.append($input, $colLg5, $colLg52, $colLg2);
                    $repeaterItem.append($formGroup);
                    $("#addRepeaterPic [data-repeater-list='PicName']").append($repeaterItem);

                    const valuePicLevelMember = Pic.PicLevelId
                    if (valuePicLevelMember) {
                        $.ajax({
                            url: url.getDdlPicLevel + "/?guid=" + valuePicLevelMember,
                            method: 'GET',
                            beforeSend: function () {
                                clearFormDataPicLevelMember();
                            },
                            success: function (data) {
                                updateFormPicLevelMember(data.items);
                            },
                            error: function (error) {
                                console.error('Error retrieving PIC details:', error);
                            }
                        });
                        function clearFormDataPicLevelMember() {
                            $('#picLevelMemberInput').val('');
                        }
                        function updateFormPicLevelMember(picLevelData) {
                            $(".levelPicMember").val(picLevelData.text);
                        }
                    };
                    const valuePicInput = Pic.PicFungsiId;
                    const selectElement = $('#picFungsiMemberInput' + index);
                    selectElement.select2({
                        ajax: {
                            url: url.ddlOpportunityPic,
                            method: 'GET',
                            data: function (params) {
                                return { query: params.term };
                            },
                            processResults: function (data) {
                                return { results: data.items };
                            },
                            minimumInputLength: 2
                        }
                    });
                    ajaxPreselecting(url.getDdlPicFungsi + "?guid=" + valuePicInput, selectElement);
                });

                $('#addRepeaterPic').find('[data-repeater-create]').removeClass('disabled');
                $('#addRepeaterPic').find('.pic').each(function (index) {
                    if (index === 0) {
                        $(this).find('[data-repeater-delete]').css('display', 'none');
                    }
                });
            }
        },
        error: function (error) {
            console.log(error);
        }
    });

    var valuePicLevelMemberInput = $('#picLevelMemberId').val();
    $('#addRepeaterPic').repeater({
        initEmpty: false,
        show: function () {

            var repeaterItem = $(this);
            $(this).slideDown();

            $(this).find('[data-repeater-delete]').prop('disabled', false);
            $('#addRepeaterPic').find('[data-repeater-create]').addClass('disabled');

            var valuePicLevelMemberInput = $('#picLevelMemberId').val();
            $.ajax({
                url: url.getDdlPicLevel + "/?guid=" + valuePicLevelMemberInput,
                method: 'GET',
                data: { selectedValue: valuePicLevelMemberInput },
                beforeSend: function () {
                    clearFormDataPicLevelMember();
                },
                success: function (data) {
                    updateFormPicLevelMember(data.items);
                },
                error: function (error) {
                    console.error('Error retrieving PIC details:', error);
                }
            });
            function clearFormDataPicLevelMember() {
                $('#picLevelMemberInput').val('');
            }
            function updateFormPicLevelMember(picLevelData) {
                $(".levelPicMember").val(picLevelData.text);
            }
            repeaterItem.find('.fungsiPicMember').select2({
                ajax: {
                    url: url.ddlOpportunityPic,
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
            }).on('change', function () {
                var selectedValue = $(this).val();
                if (selectedValue != '') {

                    $('#addRepeaterPic').find('[data-repeater-create]').removeClass('disabled');
                }
            });
        },
        hide: function (deleteElement) {
            $(this).slideUp(deleteElement);
        }
    }).find('[data-repeater-delete]').css('display', 'none');
    $('#addRepeaterPic .fungsiPicMember').select2({
        ajax: {
            url: url.ddlOpportunityPic,
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
    }).on('change', function () {
        var selectedValue = $(this).val();
        if (selectedValue != '') {

            $('#addRepeaterPic').find('[data-repeater-create]').removeClass('disabled');

        }
    });

    $('#addRepeaterPic').find('[data-repeater-create]').addClass('disabled');

    //OperatingMetric
    $('#addOperatingMetrics').repeater({
        initEmpty: false,
        show: function () {

            var repeaterItem = $(this);
            $(this).slideDown();
            $('#addOperatingMetrics').find('[data-repeater-create]').addClass('disabled');
            repeaterItem.find('.yearOM').select2({
                placeholder: "Pilih Year...",
                ajax: {
                    url: url.ddlYear,
                    method: 'GET',
                    data: function (params) {
                        return {
                            query: params.term,
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
        },
        hide: function (deleteElement) {
            $(this).slideUp(deleteElement);
        }
    }).find('[data-repeater-delete]').css('display', 'none');
    $(document).on('input', '#addOperatingMetrics', function () {

        $('.addOM').each(function (index) {
            var selectedYear = $(this).find('.yearOM').val();
            var selectedRevenue = $(this).find('.revenueOM').val();
            var selectedTotalAssets = $(this).find('.totalAssetsOM').val();
            var selectedEbitda = $(this).find('.EbitdaOM').val();

            if (selectedYear != '' && selectedRevenue != '' && selectedTotalAssets != '' && selectedEbitda != '') {

                $('#addOperatingMetrics').find('[data-repeater-create]').removeClass('disabled');

            } else {
                $('#addOperatingMetrics').find('[data-repeater-create]').addClass('disabled');
                return false;
            }
        });
    });

    $('#addOperatingMetrics').find('[data-repeater-create]').addClass('disabled');

    $('#addOperatingMetrics .yearOM').select2({
        placeholder: "Pilih Year...",
        ajax: {
            url: url.ddlYear,
            method: 'GET',
            data: function (params) {
                return {
                    query: params.term,
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
});

document.addEventListener('DOMContentLoaded', function () {
    const inputRevenue = document.getElementById('revenueOperatingMetrics');
    const inputTotalAssets = document.getElementById('totalAssetOperatingMetrics');
    const inputEbitda = document.getElementById('ebitdaOperatingMetrics');

    inputRevenue.addEventListener('input', function () {
        let value = inputRevenue.value.replace(/\./g, '').replace(/[^0-9]/g, '');
        if (value === '') {
            inputRevenue.value = '';
        } else {
            inputRevenue.value = new Intl.NumberFormat('id-ID').format(parseInt(value));
        }
    });

    inputTotalAssets.addEventListener('input', function () {
        let value = inputTotalAssets.value.replace(/\./g, '').replace(/[^0-9]/g, '');

        if (isNaN(value)) {
            inputTotalAssets.value = '0';
            return;
        }

        inputTotalAssets.value = new Intl.NumberFormat('id-ID').format(parseInt(value));
    });

    inputEbitda.addEventListener('input', function () {
        let value = inputEbitda.value.replace(/\./g, '').replace(/[^0-9]/g, '');

        if (isNaN(value)) {
            inputEbitda.value = '0';
            return;
        }

        inputEbitda.value = new Intl.NumberFormat('id-ID').format(parseInt(value));
    });
});

