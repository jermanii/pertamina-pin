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


$(function () {
    const form = document.getElementById('EditFormOpportunity');
    var validator = FormValidation.formValidation(
        form,
        {
            fields: {
                'NamaProyek': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'OpStreamBusiness.StreamBusinessId': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'OpNegaraMitra.NegaraMitraId': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'OpEntitasPertamina.EntitasPertaminaId': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'ProjectProfile': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'OpKesiapanProyek.KesiapanProyekId': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'Progress': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'TypeOfPartnerNeeded': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'OpTargetMitra.TargetMitraId': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
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
    const updateButton = document.getElementById('UpdateSubmitOpportunity');
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
                            var id = $('#hdnOpportunityId').val();

                            var model = {
                                Id: id,
                                OpPartners: [],
                                OpLokasiProyeks: [],
                                 OpPicFungsis: []
                            };

                            $('.partnerEdit').each(function () {

                                var partnerName = $(this).find('#editpartnerInput').val();

                                var partner = {
                                    PartnerName: partnerName,
                                };
                                model.OpPartners.push(partner);
                            });

                            $('.lokasiProyekEdit').each(function () {

                                var lokasiProyek = $(this).find('#editlokasiProyekProvInput').val();

                                var lokasi = {
                                    LokasiProyek: lokasiProyek
                                };
                                model.OpLokasiProyeks.push(lokasi);
                            });
                            $('.editPic').each(function (index) {
                                var picFungsiMember = $(this).find('#editPicFungsiMemberInput').val();
                                var picFungsiMemberAjax = $(this).find('#editPicFungsiMemberInput' + index).val();
                                if (picFungsiMember == undefined && picFungsiMemberAjax != undefined) {
                                    var pic = {
                                        PicFungsiId: picFungsiMemberAjax,

                                    };
                                }
                                if (picFungsiMemberAjax == undefined && picFungsiMember != undefined) {
                                    var pic = {
                                        PicFungsiId: picFungsiMember,
                                    };
                                }
                                if (pic != undefined) {
                                    model.OpPicFungsis.push(pic);
                                }


                            });

                            model.NamaProyek = $("#editnamaProyekInput").val();
                            model.Deliverables = $("#editdeliverableInput").val();
                            model.NilaiProyek = $("#editnilaiProyekInput").val();
                            model.ProjectProfile = $("#editprojectProfileInput").val();
                            model.Progress = $("#editprogressInput").val();
                            model.Timeline = $("#edittimelineInput").val();
                            model.StreamBusinessId = $("#editstreamBusinessInput").val();
                            model.EntitasPertaminaId = $("#edithshEntitasInput").val();
                            model.PicFungsiId = $("#editPicInput").val();
                            model.KesiapanProyekId = $("#editkesiapanProyekInput").val();
                            model.TargetMitraId = $("#edittargetMitraInput").val();
                            model.NegaraMitraId = $("#editlokasiProyekNegaraInput").val();
                            model.CatatanTambahan = $("#editcatatanTambahanInput").val();
                            model.TindakLanjut = $("#editTindakLanjutInput").val();
                            model.CapexToString = $("#editCapexInput").val();
                            model.PotentialRevenuePerYearToString = $("#editPotentialRevenueInput").val();
                            model.TypeOfPartnerNeeded = $("#editTypeOfPartnerNeeded").val();

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


$(document).ready(function () {
    //get streambusiness
    var streamBusinessId = $('#hdnStreamBusinessId').val();
    $('#editstreamBusinessInput').select2({
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
    if (!streamBusinessId) {
        $('#editstreamBusinessInput').val(null).trigger('change');
    }
    else {
        ajaxPreselecting(url.getDdlStreamBusiness + "?guid=" + streamBusinessId, $('#editstreamBusinessInput'));
    }

    var entitasId = $('#hdnEntitasId').val();
    $('#edithshEntitasInput').select2({
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
    if (entitasId) {
        $.ajax({
            url: url.getDdlOpportunityEntitasPertamina + "?guid=" + entitasId,
            method: 'GET',
            success: function (data) {
                if (data.items) {
                    var options = [];
                    $.each(data.items, function (index, item) {
                        options.push(new Option(item.text, item.id, true, true));
                    });
                    $('#edithshEntitasInput').append(options).trigger('change');
                }
            }
        });
    }

    var picFungsiId = $('#hdnPicFungsiId').val();
    $('#editpicInput').select2({
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
    if (picFungsiId) {
        $.ajax({
            url: url.getDdlOpportunityPicFungsi + "?guid=" + picFungsiId,
            method: 'GET',
            success: function (data) {
                if (data.items) {
                    var options = [];
                    $.each(data.items, function (index, item) {
                        options.push(new Option(item.text, item.id, true, true));
                    });
                    $('#editpicInput').append(options).trigger('change');
                }
            }
        });
    }

    var kesiapanId = $('#hdnKesiapanId').val();
    $('#editkesiapanProyekInput').select2({
        ajax: {
            url: url.ddlOpportunityKesiapanProyek,
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
    if (kesiapanId) {
        $.ajax({
            url: url.getDdlOpportunityKesiapanProyek + "?guid=" + kesiapanId,
            method: 'GET',
            success: function (data) {
                if (data.items) {
                    var options = [];
                    $.each(data.items, function (index, item) {
                        options.push(new Option(item.text, item.id, true, true));
                    });
                    $('#editkesiapanProyekInput').append(options).trigger('change');
                }
            }
        });
    }

    var negaraId = $('#hdnNegaraMitraId').val();
    $('#editlokasiProyekNegaraInput').select2({
        ajax: {
            url: url.ddlOpportunityNegara,
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


    if (negaraId) {
        $.ajax({
            url: url.getDdlOpportunityNegara + "?guid=" + negaraId,
            method: 'GET',
            success: function (data) {
                if (data.items) {
                    var options = [];
                    $.each(data.items, function (index, item) {
                        options.push(new Option(item.text, item.id, true, true));
                    });
                    $('#editlokasiProyekNegaraInput').append(options).trigger('change');
                }
            }
        });
    }

    var valuePicInput = $('#hdnPicLeadId').val();
    $('#editPicInput').select2({
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
        $('#editPicInput').val(null).trigger('change');
    }
    else {
        ajaxPreselecting(url.getDdlPicFungsi + "?guid=" + valuePicInput, $('#editPicInput'));
    }

    var targetId = $('#hdnTargetMitraId').val();
    $('#edittargetMitraInput').select2({
        ajax: {
            url: url.ddlOpportunityTargetMitra,
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
    if (targetId) {
        $.ajax({
            url: url.getDdlOpportunityTargetMitra + "?guid=" + targetId,
            method: 'GET',
            success: function (data) {
                if (data.items) {
                    var options = [];
                    $.each(data.items, function (index, item) {
                        options.push(new Option(item.text, item.id, true, true));
                    });
                    $('#edittargetMitraInput').append(options).trigger('change');
                }
            }
        });
    }

    var guid = $('#hdnOpportunityId').val();
    $.ajax({
        url: param.getOpportunityById,
        method: 'GET',
        data: { guid: guid },
        success: function (result) {
            var data = result.items;
            console.log(data),
                console.log(url)

            var partnerList = data.OpPartners;
            $("#editRepeaterPartner .partnerEdit").remove();
            partnerList.forEach(function (partner) {
                var $repeaterItem = $("<div>").addClass("partnerEdit mb-2");
                var $formGroup = $("<div>").addClass("fv-row form-group row");
                var $colMd10 = $("<div>").addClass("col-md-10");
                var $partnerInput = $("<input>").attr({
                    type: "text",
                    id: "editpartnerInput",
                    class: "form-control form-control-solid",
                    maxlength: 100,
                    placeholder: "Masukkan Partner",
                    value: partner.PartnerName // Set the value from the partner object
                });
                var $colMd2 = $("<div>").addClass("col-md-2");
                var $deleteButton = $("<button>").addClass("border border-secondary btn btn-icon btn-flex btn-danger")
                    .attr("data-repeater-delete", "")
                    .attr("type", "button")
                    .html('<i class="la la-close"></i>');

                $colMd10.append($partnerInput);
                $colMd2.append($deleteButton);
                $formGroup.append($colMd10, $colMd2);
                $repeaterItem.append($formGroup);

                $("#editRepeaterPartner [data-repeater-list='PartnerName']").append($repeaterItem);
            });

            // Inisialisasi repeater setelah menambahkan item
            $('#editRepeaterPartner').repeater({
                initEmpty: false,
                show: function () {
                    $(this).slideDown();
                },
                hide: function (deleteElement) {
                    $(this).slideUp(deleteElement);
                }
            });
            $('#editRepeaterPartner').on('click', '[data-repeater-delete]', function () {
                $(this).closest('.partnerEdit').slideUp(function () {
                    $(this).remove();
                });
            });


        },
        error: function (error) {
            console.log(error);
        }
    });

    $.ajax({
        url: param.getOpportunityById,
        method: 'GET',
        data: { guid: guid },

        success: function (result) {
            var data = result.items;
            console.log(data)
            console.log(url)
            var lokasiProyekList = data.OpLokasiProyeks;
            $("#editRepeaterLokasiProyek .lokasiProyekEdit").remove();
            lokasiProyekList.forEach(function (lokasiProyek) {
                var $repeaterItem = $("<div>").addClass("lokasiProyekEdit mb-2");
                var $formGroup = $("<div>").addClass("fv-row form-group row");
                var $colMd10 = $("<div>").addClass("col-md-10");
                var $lokasiProyekInput = $("<input>").attr({
                    type: "text",
                    id: "editlokasiProyekProvInput",
                    class: "form-control form-control-solid",
                    maxlength: 100,
                    placeholder: "Masukkan Lokasi Proyek (Provinsi)",
                    value: lokasiProyek.LokasiProyek // Set the value from the partner object
                });
                var $colMd2 = $("<div>").addClass("col-md-2");
                var $deleteButton = $("<button>").addClass("border border-secondary btn btn-icon btn-flex btn-danger")
                    .attr("data-repeater-delete", "")
                    .attr("type", "button")
                    .html('<i class="la la-close"></i>');

                $colMd10.append($lokasiProyekInput);
                $colMd2.append($deleteButton);
                $formGroup.append($colMd10, $colMd2);
                $repeaterItem.append($formGroup);

                $("#editRepeaterLokasiProyek [data-repeater-list='LokasiProyek']").append($repeaterItem);
            });

            $('#editRepeaterLokasiProyek').repeater({
                initEmpty: false,

                show: function () {
                    $(this).slideDown();
                },

                hide: function (deleteElement) {
                    $(this).slideUp(deleteElement);
                }
            });
            $('#editRepeaterLokasiProyek').on('click', '[data-repeater-delete]', function () {
                $(this).closest('.lokasiProyekEdit').slideUp(function () {
                    $(this).remove();
                });
            });
        },
        error: function (error) {
            console.log(error);
        }
    });

    $('#editRepeaterPartner').repeater({
        initEmpty: false,

        show: function () {
            $(this).slideDown();
        },

        hide: function (deleteElement) {
            $(this).slideUp(deleteElement);
        }
    });

    $('#editRepeaterLokasiProyek').repeater({
        initEmpty: false,

        show: function () {
            $(this).slideDown();
        },

        hide: function (deleteElement) {
            $(this).slideUp(deleteElement);
        }
    });
    //PIC
    var guid = $('#hdnOpportunityId').val();
    var levelId = $('#picLevelMemberId').val();
    $.ajax({
        url: param.getOpportunityById,
        method: 'GET',
        data: { guid: guid },

        success: function (result) {
            if (result.items.OpPicFungsis.length !== 0) {
                var data = result.items;
                var PicName = data.OpPicFungsis;
                $("#editRepeaterPic .editPic").remove();

                PicName.forEach(function (Pic, index) {
                    var $repeaterItem = $("<div>").addClass("editPic mb-2").attr("data-repeater-item", true);
                    var $formGroup = $("<div>").addClass("fv-row form-group row");

                    var $input = $("<input>").attr({
                        type: "hidden",
                        value: Pic.Id,
                        id: "hdnPicInput"
                    });

                    var $colLg5 = $("<div>").addClass("fv-row col-lg-5 editPicFungsi");
                    var $editPicFungsiMemberInput = $("<select>").attr({
                        id: "editPicFungsiMemberInput" + index,
                        class: "form-select form-select-solid form-select-md fw-semibold editFungsiPicMember",
                        "aria-label": "Pilih Nama PIC - Nama Fungsi - Perusahaan"
                    });

                    var $colLg52 = $("<div>").addClass("fv-row col-lg-5");
                    var $editPicLevelMemberInput = $("<input>").attr({
                        id: "editPicLevelMemberInput",
                        name: "EditPicMemberLevelId",
                        type: "text",
                        class: "form-control form-control-solid editLevelPicMember",
                        maxlength: 6000,
                        placeholder: "Pilih Nama Hub Head",
                        readonly: true
                    });


                    var $colLg2 = $("<div>").addClass("col-lg-2 d-flex align-items-end");
                    var $deleteButton = $("<button>").addClass("border border-secondary btn btn-icon btn-flex btn-danger")
                        .attr("data-repeater-delete", "")
                        .attr("type", "button")
                        .html('<i class="la la-close"></i>');


                    $colLg5.append($editPicFungsiMemberInput);
                    $colLg52.append($editPicLevelMemberInput);
                    $colLg2.append($deleteButton);
                    $formGroup.append($input, $colLg5, $colLg52, $colLg2);
                    $repeaterItem.append($formGroup);
                    $("#editRepeaterPic [data-repeater-list='EditPicName']").append($repeaterItem);

                    const valuePicLevelMember = levelId;
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
                            $('#editPicLevelMemberInput').val('');
                        }
                        function updateFormPicLevelMember(picLevelData) {
                            $(".editLevelPicMember").val(picLevelData.text);
                        }
                    };
                    const valuePicInput = Pic.PicFungsiId;
                    const selectElement = $('#editPicFungsiMemberInput' + index);
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
                    ajaxPreselecting(url.getDdlPicFungsi + "?guid=" + valuePicInput, selectElement);
                });

                $('#editRepeaterPic').find('[data-repeater-create]').removeClass('disabled');
                $('#editRepeaterPic').find('.editPic').each(function (index) {
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

    $('#editRepeaterPic').repeater({
        initEmpty: false,
        show: function () {

            var repeaterItem = $(this);
            $(this).slideDown();

            $(this).find('[data-repeater-delete]').prop('disabled', false);
            $('#editRepeaterPic').find('[data-repeater-create]').addClass('disabled');

            $.ajax({
                url: url.getDdlPicLevel + "/?guid=" + levelId,
                method: 'GET',
                data: { selectedValue: levelId },
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
                $('#picLevelMemberId').val('');
            }
            function updateFormPicLevelMember(picLevelData) {
                $(".editLevelPicMember").val(picLevelData.text);
            }
            repeaterItem.find('.editFungsiPicMember').select2({
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
            })
                .on('change', function () {
                    var selectedValue = $(this).val();
                    if (selectedValue != '') {

                        $('#editRepeaterPic').find('[data-repeater-create]').removeClass('disabled');
                    }
                });
        },
        hide: function (deleteElement) {
            $(this).slideUp(deleteElement);
        }


    }).find('[data-repeater-delete]').css('display', 'none');
    $('#editRepeaterPic .editFungsiPicMember').select2({
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

            $('#editRepeaterPic').find('[data-repeater-create]').removeClass('disabled');

        }
    });

    $('#editRepeaterPic').find('[data-repeater-create]').addClass('disabled');



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



document.addEventListener('DOMContentLoaded', function () {
    const inputRevenue = document.getElementById('editPotentialRevenueInput');
    const inputCapex = document.getElementById('editCapexInput');

    inputRevenue.addEventListener('input', function () {
        let value = inputRevenue.value.replace(/\./g, '').replace(/[^0-9]/g, '');
        if (value === '') {
            inputRevenue.value = '';
        } else {
            inputRevenue.value = new Intl.NumberFormat('id-ID').format(parseInt(value));
        }
    });

    inputCapex.addEventListener('input', function () {
        let value = inputCapex.value.replace(/\./g, '').replace(/[^0-9]/g, '');

        if (value === '') {
            inputCapex.value = '';
        } else {
            inputCapex.value = new Intl.NumberFormat('id-ID').format(parseInt(value));
        }

        inputTotalAssets.value = new Intl.NumberFormat('id-ID').format(parseInt(value));
    });
});
