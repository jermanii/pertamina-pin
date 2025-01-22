//EditForm
$(document).ready(function () {
    const form = document.getElementById('EditFormExistingFootprints');
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
    const submitButton = document.getElementById('UpdateSubmitExistingFootprints');
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


                            $('.editOM').each(function (index) {
                                var year = $(this).find('.year-operatingMetrics').val();
                                var revenue = $(this).find('.revenue-operatingMetrics').val();
                                var totalAsset = $(this).find('.totalAssets-operatingMetrics').val();
                                var ebitda = $(this).find('.ebitda-operatingMetrics').val();

                                var operatingMetric = {
                                    Year: year,
                                    RevenueToString: revenue,
                                    TotalAssetToString: totalAsset,
                                    EbitdaToString: ebitda
                                };

                                model.OperatingMetricss.push(operatingMetric);
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
                $("#editRepeaterPartner .editPartner").remove();
                PartnerName.forEach(function (Partner) {
                    var $repeaterItem = $("<div>").addClass("editPartner got mb-2");
                    var $formGroup = $("<div>").addClass("fv-row form-group row");
                    var $input = $("<input>").attr({
                        type: "hidden",
                        value: Partner.Id,
                        id: "hdnEntitasInput"
                    });
                    var $colLg5 = $("<div>").addClass("fv-row col-lg-5");
                    var $partnersInput = $("<input>").attr({
                        type: "text",
                        id: "editPartnerInput",
                        class: "form-control form-control-solid",
                        maxlength: 100,
                        placeholder: "Enter Partner",
                        value: Partner.Partners
                    });
                    var $colLg52 = $("<div>").addClass("fv-row col-lg-5");
                    var $locationInput = $("<input>").attr({
                        type: "text",
                        id: "editPartnerLocation",
                        class: "form-control form-control-solid",
                        maxlength: 100,
                        placeholder: "Enter Partner Location",
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
                    $("#editRepeaterPartner [data-repeater-list='EditPartnerName']").append($repeaterItem);
                });

                $('#editRepeaterPartner').repeater({
                    initEmpty: false,

                    show: function () {
                        $(this).slideDown();


                    },

                    hide: function (deleteElement) {
                        $(this).slideUp(deleteElement);
                    }
                }).find('.editPartner').each(function (index) {
                    if (index === 0) {
                        $(this).find('[data-repeater-delete]').css('display', 'none');
                    }
                });
                $('#editRepeaterPartner').on('click', '[data-repeater-delete]', function () {
                    $(this).closest('.editPartner').slideUp(function () {
                        $(this).remove();
                    });
                });
            }

        },
        error: function (error) {
            console.log(error);
        }
    });

    $('#editRepeaterPartner').repeater({
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
        $('#editRepeaterPartner [data-repeater-item]').each(function (index) {
            var a = $('#editRepeaterPartner .got')
            if (a.length != 0) {
                $(this).find('label[for="editPartnerInput"]').hide();
                $(this).find('label[for="editPartnerLocation"]').hide();
            } else {
                if (index > 0) {
                    $(this).find('label[for="editPartnerInput"]').hide();
                    $(this).find('label[for="editPartnerLocation"]').hide();
                }
            }
        });
    };

    //PIC
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

    //Operating Metrics
    $.ajax({
        url: param.getExistingFootprintsById,
        method: 'GET',
        data: { guid: guid, levelId: levelId },

        success: function (result) {
            if (result.items.OperatingMetricss.length !== 0) {
                var data = result.items;
                var OMName = data.OperatingMetricss;
                $("#editOperatingMetrics .editOM").remove();

                OMName.forEach(function (OM, index) {
                    var $repeaterItem = $("<div>").addClass("editOM mb-2").attr("data-repeater-item", true);
                    var $formGroup = $("<div>").addClass("fv-row form-group row");

                    var $input = $("<input>").attr({
                        type: "hidden",
                        value: OM.Id,
                        id: "hdnOMInput"
                    });
                    var $colLg5 = $("<div>").addClass("fv-row col-lg-2").attr("style", "width:250px");
                    var $editYearOperatingMetrics = $("<select>").attr({
                        type: "number",
                        id: "editYearOperatingMetrics" + index,
                        class: "form-control year-operatingMetrics",
                        value: OM.Year
                    });
                    var $colLg51 = $("<div>").addClass("fv-row col-lg-2").attr("style", "width:250px");
                    var $editRevenueOperatingMetrics = $("<input>").attr({
                        type: "text",
                        id: "editRevenueOperatingMetrics" + index,
                        class: "form-control revenue-operatingMetrics",
                        value: OM.RevenueToString
                    });
                    var $colLg52 = $("<div>").addClass("fv-row col-lg-2").attr("style", "width:250px");
                    var $editTotalAssetOperatingMetrics = $("<input>").attr({
                        type: "text",
                        maxlength: 15,
                        id: "editTotalAssetOperatingMetrics" + index,
                        class: "form-control  totalAssets-operatingMetrics",
                        value: OM.TotalAssetToString
                    });
                    var $colLg53 = $("<div>").addClass("fv-row col-lg-2").attr("style", "width:250px");
                    var $editEbitdaOperatingMetrics = $("<input>").attr({
                        type: "text",
                        id: "editEbitdaOperatingMetrics" + index,
                        class: "form-control ebitda-operatingMetrics",
                        value: OM.EbitdaToString
                    });

                    var $colLg2 = $("<div>").addClass("fv-row col-lg-1 d-flex align-items-end");
                    var $deleteButton = $("<button>").addClass("border border-secondary btn btn-icon btn-flex btn-danger")
                        .attr("data-repeater-delete", "")
                        .attr("type", "button")
                        .html('<i class="la la-close"></i>');


                    $colLg5.append($editYearOperatingMetrics);
                    $colLg51.append($editRevenueOperatingMetrics);
                    $colLg52.append($editTotalAssetOperatingMetrics);
                    $colLg53.append($editEbitdaOperatingMetrics);
                    $colLg2.append($deleteButton);
                    $formGroup.append($input, $colLg5, $colLg51, $colLg52, $colLg53, $colLg2);
                    $repeaterItem.append($formGroup);
                    $("#editOperatingMetrics [data-repeater-list='EditOperatingMetrics']").append($repeaterItem);

                    const valueYear = OM.Year;
                    const selectElement = $('#editYearOperatingMetrics' + index);
                    selectElement.select2({
                        ajax: {
                            url: url.ddlYear,
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
                    const inputRevenue = document.getElementById('editRevenueOperatingMetrics' + index);
                    const inputTotalAssets = document.getElementById('editTotalAssetOperatingMetrics' + index);
                    const inputEbitda = document.getElementById('editEbitdaOperatingMetrics' + index);
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
                    ajaxPreselecting(param.getYearOperatingMetricById + "?guid=" + guid + "&year=" + valueYear, selectElement);
                    $('#editOperatingMetrics').find('.editOM').each(function (index) {
                        if (index === 0) {
                            $(this).find('[data-repeater-delete]').css('display', 'none');
                        }
                    });
                });

                $('#editOperatingMetrics').find('[data-repeater-create]').removeClass('disabled');



            } else {
                const selectElement = $('#editYearOperatingMetrics');
                selectElement.select2({
                    ajax: {
                        url: url.ddlYear,
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
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
    $('#editOperatingMetrics').repeater({
        initEmpty: false,
        show: function () {

            var repeaterItem = $(this);
            $(this).slideDown();

            $(this).find('[data-repeater-delete]').prop('disabled', false);
            $('#editOperatingMetrics').find('[data-repeater-create]').addClass('disabled');

            repeaterItem.find('.year-operatingMetricsEdit').select2({
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
            })
        },
        hide: function (deleteElement) {
            $(this).slideUp(deleteElement);
        }
    }).find('[data-repeater-delete]').css('display', 'none');

    $(document).on('input', '#editOperatingMetrics', function () {

        $('.editOM').each(function (index) {
            var selectedYear = $(this).find('.year-operatingMetricsEdit').val();
            var selectedYearRepeater = $(this).find('.year-operatingMetrics').val();
            var selectedRevenue = $(this).find('.revenue-operatingMetrics').val();
            var selectedTotalAssets = $(this).find('.totalAssets-operatingMetrics').val();
            var selectedEbitda = $(this).find('.ebitda-operatingMetrics').val();

            if (selectedRevenue != '' && selectedTotalAssets != '' && selectedEbitda != '') {
                if (selectedYear == undefined && selectedYearRepeater) {

                    $('#editOperatingMetrics').find('[data-repeater-create]').removeClass('disabled');

                }
                if (selectedYear == undefined && !selectedYearRepeater) {

                    $('#editOperatingMetrics').find('[data-repeater-create]').addClass('disabled');
                    return false;

                }
                if (selectedYear) {
                    $('#editOperatingMetrics').find('[data-repeater-create]').removeClass('disabled');
                }
            } else {
                $('#editOperatingMetrics').find('[data-repeater-create]').addClass('disabled');
                return false;
            }
        });
    });

});
document.addEventListener('DOMContentLoaded', function () {
    const repeaterContainer = document.getElementById('editOperatingMetrics');

    repeaterContainer.addEventListener('input', function (event) {
        if (event.target.classList.contains('revenue-operatingMetrics') ||
            event.target.classList.contains('totalAssets-operatingMetrics') ||
            event.target.classList.contains('ebitda-operatingMetrics')) {

            let value = event.target.value.replace(/\./g, '').replace(/[^0-9]/g, '');

            if (value === '') {
                event.target.value = '';
            } else {
                event.target.value = new Intl.NumberFormat('id-ID').format(parseInt(value));
            }
        }
    });
});

$(document).ready(function () {
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
    $('#editStreamBusinessInput').select2({
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
        $('#editStreamBusinessInput').val(null).trigger('change');
    } else {
        ajaxPreselecting(url.getDdlStreamBusiness + "?guid=" + valueStreamBusinessInput, $('#editStreamBusinessInput'));
    }
    var valueEntitasInput = $('#hdnEntitasInput').val();
    $('#editHshEntitasInput').select2({
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
        $('#editHshEntitasInput').val(null).trigger('change');
    }
    else {
        ajaxPreselecting(url.getDdlEntitasPertamina + "?guid=" + valueEntitasInput, $('#editHshEntitasInput'));
    }
    var valueNegaraMitraInput = $('#hdnNegaraMitra').val();
    $('#editHshNegaraMitraInput').select2({
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
        $('#editHshNegaraMitraInput').val(null).trigger('change');
    }
    else {
        ajaxPreselecting(url.getDdlNegaraMitraOnlyNegara + "?guid=" + valueNegaraMitraInput, $('#editHshNegaraMitraInput'));
    }
    var valueHub = $('#hdnHub').val();
    $('#editHubName').select2({
        placeholder: "Pilih RC PIC ...",
        //dropdownParent: $('#editAssigneeModal'),
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
    });
    if (!valueHub) {
        $('#editHubName').val(null).trigger('change');
    }
    else {
        ajaxPreselecting(url.getDdlHub + "?guid=" + valueHub, $('#editHubName'));
        $.ajax({
            method: 'GET',
            data: { selectedValue: valueHub },
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
    }
    $('#editHubName').on('change', function () {
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
    function clearFormData() {
        // Replace these lines with your code to clear the form fields
        $('#editHshHubHeadInput').val('');

    }
    function updateFormBasedOnSelectedHubHead(hubHeadData) {
        if (hubHeadData) {
            $('#editHshHubHeadInput').val(hubHeadData.HubHeadName);
            $('#editHubId').val(hubHeadData.HubId);
            $('#editHubHeadId').val(hubHeadData.HubHeadId);
        }
        //$('#EditNamaPimpinan').val(picData.NamaPimpinan);
    }

    var valuePicInput = $('#hdnPicInput').val();
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
        $('#editPicLevelInput').val('');
    }
    function updateFormPicLevelLead(picLevelData) {
        $('#editPicLevelInput').val(picLevelData.text);
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
        $('#editPicLevelMemberInput').val('');
    }
    function updateFormPicLevelMember(picLevelData) {
        $('#editPicLevelMemberInput').val(picLevelData.text);
    }
});