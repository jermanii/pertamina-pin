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

    const form = document.getElementById('CreateFormOpportunity');
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

    const submitButton = document.getElementById('CreateSubmitOpportunity');
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
                                OpPartners: [],
                                OpLokasiProyeks: [],
                                OpPicFungsis: []
                            };

                            $('.partner').each(function () {

                                var partnerName = $(this).find('#partnerInput').val();

                                var partner = {
                                    PartnerName: partnerName,
                                };
                                model.OpPartners.push(partner);
                            });

                            $('.lokasiProyek').each(function () {
                                var lokasiProyek = $(this).find('#lokasiProyekProvInput').val();

                                var lokasi = {
                                    LokasiProyek: lokasiProyek
                                };
                                model.OpLokasiProyeks.push(lokasi);
                            });

                            $('.picFungsi').each(function () {
                                var picMember = $(this).find('#picFungsiMemberInput').val();

                                var pic = {
                                    PicFungsiId: picMember
                                };
                                model.OpPicFungsis.push(pic);
                            });

                            model.NamaProyek = $("#namaProyekInput").val();
                            model.Deliverables = $("#deliverableInput").val();
                            model.NilaiProyek = $("#nilaiProyekInput").val();
                            model.ProjectProfile = $("#projectProfileInput").val();
                            model.Progress = $("#progressInput").val();
                            model.Timeline = $("#timelineInput").val();
                            model.StreamBusinessId = $("#streamBusinessInput").val();
                            model.EntitasPertaminaId = $("#hshEntitasInput").val();
                            model.PicFungsiId = $("#picFungsiInput").val();
                            model.CapexToString = $("#capexInput").val();
                            model.PotentialRevenuePerYearToString = $("#potentialRevenueInput").val();
                            model.TypeOfPartnerNeeded = $("#timeOfPartnerNeeded").val();

                            model.KesiapanProyekId = $("#kesiapanProyekInput").val();
                            model.TargetMitraId = $("#targetMitraInput").val();
                            model.NegaraMitraId = $("#lokasiProyekNegaraInput").val();
                            model.CatatanTambahan = $("#catatanTambahanInput").val();
                            model.TindakLanjut = $("#tindakLanjutInput").val();

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
    var validatorSaveDraft = FormValidation.formValidation(
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
    const saveDraftButton = document.getElementById('SaveDraftOpportunity');
    saveDraftButton.addEventListener('click', function (e) {
        e.preventDefault();
        var callBackUrlPartial = $(this).data("callbackurl");
        if (validatorSaveDraft) {
            validatorSaveDraft.validate().then(function (status) {
                if (status == "Valid") {
                    Swal.fire({
                        text: "Are you sure want to save this data as a Draft ?",
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
                            saveDraftButton.setAttribute('data-kt-indicator', 'on');
                            saveDraftButton.disabled = true;

                            var urlSubmit = saveDraftButton.dataset.url;


                            var model = {
                                OpPartners: [],
                                OpLokasiProyeks: [],
                                OpPicFungsis: []
                            };

                            $('.partner').each(function () {

                                var partnerName = $(this).find('#partnerInput').val();

                                var partner = {
                                    PartnerName: partnerName,
                                };
                                model.OpPartners.push(partner);
                            });

                            $('.lokasiProyek').each(function () {
                                var lokasiProyek = $(this).find('#lokasiProyekProvInput').val();

                                var lokasi = {
                                    LokasiProyek: lokasiProyek
                                };
                                model.OpLokasiProyeks.push(lokasi);
                            });

                            $('.picFungsi').each(function () {
                                var picMember = $(this).find('#picFungsiMemberInput').val();

                                var pic = {
                                    PicFungsiId: picMember
                                };
                                model.OpPicFungsis.push(pic);
                            });

                            model.NamaProyek = $("#namaProyekInput").val();
                            model.Deliverables = $("#deliverableInput").val();
                            model.NilaiProyek = $("#nilaiProyekInput").val();
                            model.ProjectProfile = $("#projectProfileInput").val();
                            model.Progress = $("#progressInput").val();
                            model.Timeline = $("#timelineInput").val();
                            model.StreamBusinessId = $("#streamBusinessInput").val();
                            model.EntitasPertaminaId = $("#hshEntitasInput").val();
                            model.PicFungsiId = $("#picFungsiInput").val();
                            model.CapexToString = $("#capexInput").val();
                            model.PotentialRevenuePerYearToString = $("#potentialRevenueInput").val();
                            model.TypeOfPartnerNeeded = $("#timeOfPartnerNeeded").val();
                            model.KesiapanProyekId = $("#kesiapanProyekInput").val();
                            model.TargetMitraId = $("#targetMitraInput").val();
                            model.NegaraMitraId = $("#lokasiProyekNegaraInput").val();
                            model.CatatanTambahan = $("#catatanTambahanInput").val();
                            model.TindakLanjut = $("#tindakLanjutInput").val();

                            $.ajax({
                                type: "POST",
                                url: urlSubmit,
                                data: model,
                                success: function (data) {
                                    if (data.IsError) {
                                        saveDraftButton.removeAttribute('data-kt-indicator');
                                        saveDraftButton.disabled = false;
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
                                        saveDraftButton.removeAttribute('data-kt-indicator');
                                        saveDraftButton.disabled = false;
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
    $('#addRepeaterPartner').repeater({
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
    $('#addRepeaterPartner [data-repeater-delete]').prop('disabled', true);


    var isFirstRepeater = true;
    $('#addRepeaterLokasiProyek').repeater({
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
    $('#addRepeaterLokasiProyek [data-repeater-delete]').prop('disabled', true);


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

    $('#picFungsiInput').select2({
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

    $('#kesiapanProyekInput').select2({
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

    $('#lokasiProyekNegaraInput').select2({
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

    $('#targetMitraInput').select2({
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



});

$(function () {
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

    // PIC
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
})

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
    const inputRevenue = document.getElementById('potentialRevenueInput');
    const inputCapex = document.getElementById('capexInput');

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

