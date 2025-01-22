$(function () {
    const form = document.getElementById('EditFormAgreement');
    var validator = FormValidation.formValidation(
        form,
        {
            fields: {
                'JudulPerjanjian': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'JenisPerjanjianId': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'EntitasPertaminaEvent': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'FungsiPicId': {
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
                'TanggalTtd': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'TanggalBerakhir': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'DiscussionStatusId': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'Scope': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'StreamBusinessIds': {
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
                'KlasifikasiKendalaId': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'FaktorKendalaId': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'TindakLanjut': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        },
                        stringLength: {
                            min: 50,
                            message: 'Please enter the column must be more than 50 characters.'
                        },
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
        });
    const updateButton = document.getElementById('UpdateSubmitAgreement');
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
                        confirmButtonText: "Yes",
                        cancelButtonText: 'No',
                        customClass: {
                            confirmButton: "btn btn-primary",
                            cancelButton: 'btn btn-danger'
                        }
                    }).then((result) => {
                        if (result.isConfirmed) {
                            updateButton.setAttribute('data-kt-indicator', 'on');
                            updateButton.disabled = true;

                            var urlSubmit = updateButton.dataset.url;
                            var id = $('#hdnAgreementId').val();

                            var model = {
                                Id: id,
                                AgreementPartners: [],
                                AgreementLokasiProyeks: [],
                                AgreementPicFungsis:[],
                                AgreementAddendums:[]
                            };

                            model.JudulPerjanjian = $("#judulPerjanjianInputEdit").val();
                            model.JenisPerjanjianId = $("#jenisPerjanjianInputEdit").val();
                            model.EntitasPertaminaEvent = $("#hshEntitasPertaminaInputEdit").val();
                            model.PicFungsiId = $("#editPicInput").val();
                            model.PicLevelId = $("#picLevelId").val();
                            $('.partnerEdit').each(function () {

                                var partnerName = $(this).find('#editpartnerInput').val();
                                var partner = {
                                    PartnerName: partnerName,
                                };
                                model.AgreementPartners.push(partner);
                            });
                            $('.lokasiProyekEdit').each(function () {
                                var lokasiProyek = $(this).find('#editlokasiProyekProvInput').val();

                                var lokasi = {
                                    LokasiProyek: lokasiProyek
                                };
                                model.AgreementLokasiProyeks.push(lokasi);
                            });
                            $('.editFungsiPicMember').each(function () {
                                const dateValue = $(this).val();
                                var picFungsi = $(this).find('#editPicFungsiMemberInput').val();
                                var picFungsiLevelMember = $(this).find('#editPicLevelMemberInput').val();
                                var pic = {
                                    PicFungsiId: dateValue,
                                    PicLevelMemberId: picFungsiLevelMember
                                };
                                model.AgreementPicFungsis.push(pic);
                            });
                            $('.addendumDateClass').each(function () {

                                const dateValue = $(this).val();
                                if (dateValue != '') {
                                    var addendum = {
                                        AddendumDate: dateValue
                                    };
                                    model.AgreementAddendums.push(addendum);
                                }
                            });

                            model.NegaraMitraId = $("#negaraMitraInputEdit").val();
                            model.ValuationToString = $("#nilaiProyekInputEdit").val();
                            model.TanggalTtd = $("#rangeTanggalTtdInputEdit").val();
                            model.TanggalBerakhir = $("#rangeTanggalBerakhirInputEdit").val();
                            model.StatusBerlakuId = $("#statusBerlakuValueInputEdit").val();
                            model.DiscussionStatusId = $("#statusDiskusiInputEdit").val();
                            model.Scope = $("#scopeProjectInputEdit").val();
                            model.StreamBusinessIds = $("#streamBusinessInputEdit").val();
                            model.Progress = $("#progressInputEdit").val();
                            model.FaktorKendalaId = $("#faktorKendalaInputEdit").val();
                            model.KlasifikasiKendalaId = $("#klasifikasiKendalaInputEdit").val();
                            model.DeskripsiKendala = $("#deskripsiKendalaInputEdit").val();
                            model.TindakLanjut = $("#tindakLanjutInputEdit").val();
                            model.SupportPemerintah = $("#dukunganPemerintahInputEdit").val();
                            model.PotensiEskalasi = $("#potensialEskalasiInputEdit").val();
                            model.CatatanTambahan = $("#catatanTambahanInputEdit").val();
                            model.RelatedAgreementId = $("#relasiAgreementInputEdit").val();
                            model.KementrianLembagaIds = $("#keterlibatanLembagaInputEdit").val();
                            model.PotentialRevenuePerYearToString = $("#transactionInputEdit").val();
                            model.CapexToString = $("#projectCostInputEdit").val();
                            model.IsGtg = $("#checkbox-g2g").is(":checked");

                            model.TrafficLightId = $("#editTrafficLightInput").val();
                            model.TindakLanjut = $("#editTindakLanjutInput").val();

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
$(document).ready(function () {
    var isAddendum = $('#hdnIsAddendumId').val();
    if (isAddendum == 'True?') {
        $("#rangeTanggalTtdInputEdit").prop("readonly", true);
        var guid = $('#hdnAgreementId').val();
        $.ajax({
            url: param.getAgreementById,
            method: 'GET',
            data: { guid: guid },
            success: function (result) {
                var addendumList = result.items.AgreementAddendums;

                if (addendumList.length > 0) {
                    $("#editAddendumDate .addendumDateEdit").remove();
                    addendumList.forEach(function (addendum) {
                        var $repeaterItem = $("<div>").addClass("addendumDateEdit mb-2");
                        var $formGroup = $("<div>").addClass("fv-row form-group row");
                        var $colLg51 = $("<div>").addClass("col-lg-5");
                        var $addendumSequenceInput = $("<input>").attr({
                            type: "text",
                            id: "sequenceTtdInputEdit",
                            class: "form-control form-control-solid addendumSequenceDateClass",
                            maxlength: 100,
                            placeholder: "Choose Signature Date",
                            readonly:"readonly",
                            value: addendum.AmandementBySequence,  // Set the value from the partner object
                        });
                        var $colLg52 = $("<div>").addClass("col-lg-5");
                        var $addendumDateInput = $("<input>").attr({
                            type: "text",
                            id: "amandementTtdInputEdit",
                            class: "form-control form-control-solid",
                            maxlength: 100,
                            placeholder: "Choose Signature Date",
                            readonly:"readonly",
                            value: addendum.AddendumDateToString,  // Set the value from the partner object
                        });
                        var $colMd2 = $("<div>").addClass("col-md-2");
                        var $deleteButton = $("<button>").addClass("border border-secondary btn btn-icon btn-flex btn-danger")
                            .attr("data-repeater-delete", "")
                            .attr("type", "button")
                            .html('<i class="la la-close"></i>');

                        $colLg51.append($addendumSequenceInput);
                        $colLg52.append($addendumDateInput);
                        //$colMd2.append($deleteButton);
                        $formGroup.append($colLg51, $colLg52, $colMd2);
                        $repeaterItem.append($formGroup);

                        $("#editAddendumDate [data-repeater-list='AddendumDate']").append($repeaterItem);
                    });

                    $('#editAddendumDate').repeater({
                        initEmpty: false,
                        show: function () {
                            $(this).slideDown();
                            //$('#editAddendumDate').find('[data-repeater-create]').removeClass('disabled');
                        },
                        hide: function (deleteElement) {
                            $(this).slideUp(deleteElement);
                        }
                    });
                    $('#editAddendumDate').on('click', '[data-repeater-delete]', function () {
                        $(this).closest('.addendumDateEdit').slideUp(function () {
                            $(this).remove();
                        });
                    });
                }
                
            },
            error: function (error) {
                console.log(error);
            }
        });
        isFirstRepeater = true;
        $('#editAddendumDate').repeater({
            initEmpty: false,

            show: function () {

                var repeaterItem = $(this);
                $(this).slideDown();
                if (isFirstRepeater) {
                    isFirstRepeater = false;
                }
                
                var id = $('#hdnAgreementId').val();
                var item = [];
                $('.addendumDateEdit').each(function (index) {
                    item = ({ index })
                });
                $.ajax({
                    url: param.getAdendumSequence,
                    method: 'GET',
                    data: { sequence: item.index, guid: id },
                    success: function (result) {
                       var min = result.items.AgreementAddendum.AddendumDateToString
                        updateFormSequence(result.items.SequenceAmandement);
                        if (result.items.SequenceAmandement.length <= 1) {
                            $('.addendumDateClass').flatpickr({
                                dateFormat: "m-d-Y",
                                minDate: min
                            });
                        }
                        
                    },
                    error: function (error) {
                        console.log(error);
                    }
                });


                function updateFormSequence(itemsSequence) {
                    itemsSequence.forEach(function (items, index) {
                        const elementSequence = $(`[name="AddendumDate[${index}][SequenceAmandement]"]`);
                        if (elementSequence.length) {
                            elementSequence.val(items);
                        }
                        const elementAmandement = $(`[name="AddendumDate[${index - 1}][AmandementDate]"]`);
                        if (elementAmandement.length) {
                            var currentValue = elementAmandement.val();
                            elementAmandement.flatpickr().destroy();
                            elementAmandement.prop("readonly", true).val(currentValue);
                        }
                    })
                };

                $(this).find('[data-repeater-delete]').css('display', 'none');
                $('#editAddendumDate').find('[data-repeater-create]').addClass('disabled');

                function updateMinDate() {
                    var a = {};
                    $('.addendumDateEdit .addendumDateClass').each(function (index) {
                        item = ($(this).val());
                        if (item != '') {
                            a = item
                        }
                    })
                    return a

                }

                function UpdateLabelAddendumList() {
                    $('.addendumDateClass').flatpickr({
                        dateFormat: "m-d-Y",
                        minDate: updateMinDate()
                    });
                }
                UpdateLabelAddendumList();
            },

            hide: function (deleteElement) {
                $(this).slideUp(deleteElement);
            }
        }).find('[data-repeater-delete]').prop('disabled', true);

        $(document).on('change', '.addendumDateClass', function () {

            var selectedValue = $(this).val();
            if (selectedValue !== '') {

                $('#editAddendumDate').find('[data-repeater-create]').removeClass('disabled');
                var currentValue = $(".addendumDateClass").val();
                $("#rangeTanggalTtdInputEdit").flatpickr().destroy();
                $("#rangeTanggalTtdInputEdit").prop("readonly", true).val(currentValue);

                $.ajax({
                    type: 'GET',
                    url: param.statusBerlakuCounting,
                    data: { endDate: selectedValue },
                    success: function (data) {
                        console.log(data);
                        $("#statusBerlakuInputEdit").val(data.items.StatusName);
                        $("#statusBerlakuValueInputEdit").val(data.items.Id);
                    },
                    error: function (error) {
                        console.log(error);
                    }
                });

            }
        });
    }
    else {
        isFirstRepeater = true;
        $('#editAddendumDate').repeater({
            initEmpty: false,
            show: function () {
                var repeaterItem = $(this);
                $(this).slideDown();
                
                UpdateLabelAddendum()
                $(this).find('[data-repeater-delete]').css('display', 'none');
                $('#editAddendumDate').find('[data-repeater-create]').addClass('disabled');

                var item = [];
                $('.addendumDateEdit').each(function (index) {
                    item = ({ index })
                })

                $.ajax({
                    url: param.getAdendumSequence,
                    method: 'GET',
                    data: { sequence: item.index },
                    success: function (result) {
                        result;
                        updateFormSequence(result.items.SequenceAmandement);
                    },
                    error: function (error) {
                        console.log(error);
                    }
                });
                function updateFormSequence(itemsSequence) {
                    itemsSequence.forEach(function (items, index) {
                        const elementSequence = $(`[name="AddendumDate[${index}][SequenceAmandement]"]`);
                        if (elementSequence.length) {
                            elementSequence.val(items);
                        }
                        const elementAmandement = $(`[name="AddendumDate[${index-1}][AmandementDate]"]`);
                        if (elementAmandement.length) {
                            var currentValue = elementAmandement.val();
                            elementAmandement.flatpickr().destroy();
                            elementAmandement.prop("readonly", true).val(currentValue);
                        }
                    })
                };
            },
            hide: function (deleteElement) {
                $(this).slideUp(deleteElement);
            }
        }).find('[data-repeater-delete]').css('display', 'none');
        var min = $("#rangeTanggalTtdInputEdit").val();
        var item = [];
        function updateMinDate() {
            var a = {};
            $('.addendumDateEdit .addendumDateClass').each(function (index) {
                item = ($(this).val());
                if (item != '') {
                    a =item
                }


            })
            return a
           
        }
        
        function UpdateLabelAddendum() {
            $('.addendumDateClass').flatpickr({
                dateFormat: "m-d-Y",
                minDate: updateMinDate()
            });
        }
       
        $('.addendumDateClass').flatpickr({
            dateFormat: "m-d-Y",
            minDate: min
        });
       
        $('.addendumDateEdit').each(function (index) {
            item = ({ index })
        });
        
        $.ajax({
            url: param.getAdendumSequence,
            data: { sequence: item },
            method: 'GET',
            success: function (result) {
                updateFormSequence(result.items.SequenceAmandement)
            },
            error: function (error) {
                console.log(error);
            }
        });
        function updateFormSequence(itemsSequence) {

            itemsSequence.forEach(function (items, index) {
                const element = $(`[name="AddendumDate[${index}][SequenceAmandement]"]`);
                if (element.length) {
                    element.val(items);
                } 
            })
        };

        $('#editAddendumDate').find('[data-repeater-create]').addClass('disabled');

        $(document).on('change', '#rangeTanggalTtdInputEdit', function () {
            var selectedValue = $(this).val();
            if (selectedValue != '') {

                $('#editAddendumDate').find('[data-repeater-delete]').addClass('disabled');
                $('#editAddendumDate').find('[data-repeater-create]').addClass('disabled');
                $(".addendumDateClass").flatpickr().destroy();
                $(".addendumDateClass").prop("readonly", true);

            }
        });
        $(document).on('change', '.addendumDateClass', function () {
            
            var selectedValue = $(this).val();
            if (selectedValue !== '') {

                $('#editAddendumDate').find('[data-repeater-create]').removeClass('disabled');
                var currentValue = $(".addendumDateClass").val();
                $("#rangeTanggalTtdInputEdit").flatpickr().destroy();
                $("#rangeTanggalTtdInputEdit").prop("readonly", true).val(currentValue);

                $.ajax({
                    type: 'GET',
                    url: param.statusBerlakuCounting,
                    data: { endDate: selectedValue },
                    success: function (data) {
                        console.log(data);
                        $("#statusBerlakuInputEdit").val(data.items.StatusName);
                        $("#statusBerlakuValueInputEdit").val(data.items.Id);
                    },
                    error: function (error) {
                        console.log(error);
                    }
                });

            }
        });
        $(document).on('click', '.data-repeater-create-amandement', function () {
            if ($('.data-repeater-delete-amandement').length >= 2) {
                var currentValue = $("#rangeTanggalTtdInputEdit").val();
                $("#rangeTanggalTtdInputEdit").flatpickr().destroy();
                $("#rangeTanggalTtdInputEdit").prop("readonly", true).val(currentValue);
            }
        });
        function updateRangeTanggalEdit() {
            $("#rangeTanggalTtdInputEdit").flatpickr({
                dateFormat: "m-d-Y",
                onChange: function (selectedDates) {
                    if (selectedDates[0]) {
                        $("#rangeTanggalBerakhirInputEdit").prop("readonly", false);
                        $("#rangeTanggalBerakhirInputEdit").val("").trigger("change");
                        $("#statusBerlakuInputEdit").val("").trigger("change");
                        var minDate = selectedDates[0];
                        $("#rangeTanggalBerakhirInputEdit").flatpickr({
                            dateFormat: "m-d-Y",
                            minDate: minDate,
                            onChange: function (selectedEndDate) {
                                if (selectedEndDate[0]) {
                                    var formattedSelectedEndDate = selectedEndDate[0].toLocaleDateString("en-US", { year: 'numeric', month: '2-digit', day: '2-digit' });
                                    $.ajax({
                                        type: 'GET',
                                        url: param.statusBerlakuCounting,
                                        data: { endDate: formattedSelectedEndDate },
                                        success: function (data) {
                                            console.log(data);
                                            $("#statusBerlakuInputEdit").val(data.items.StatusName);
                                            $("#statusBerlakuValueInputEdit").val(data.items.Id);
                                        },
                                        error: function (error) {
                                            console.log(error);
                                        }
                                    });
                                }
                            }
                        });
                    }
                }
            });
            $("#statusBerlakuInputEdit").prop("readonly", true);
        }

        updateRangeTanggalEdit();
    }
});
$(function () {
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
    }
    //Multiple
    $('#hshEntitasPertaminaInputEdit').select2({
        multiple: true,
        ajax: {
            url: url.ddlHshEntitasPertamina,
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
    var entitasId = $('#hdnEntitasId').val();
    if (entitasId) {
        multiple: true,
            $.ajax({
                url: url.getDdlAgreementEntitasPertamina + "?guid=" + entitasId,
                method: 'GET',
                success: function (data) {
                    if (data.items) {
                        var options = [];
                        $.each(data.items, function (index, item) {
                            options.push(new Option(item.text, item.id, true, true));
                        });
                        $('#hshEntitasPertaminaInputEdit').append(options).trigger('change');
                    }
                }
            });
    }
    var picId = $('#hdnPicFungsiId').val();
    $('#fungsiPicInputEdit').select2({
        multiple: true,
        ajax: {
            url: url.ddlPic,
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
    if (picId) {
        multiple: true,
            $.ajax({
                url: url.getDdlAgreementPicFungsi + "?guid=" + picId,
                method: 'GET',
                success: function (data) {
                    if (data.items) {
                        var options = [];
                        $.each(data.items, function (index, item) {
                            options.push(new Option(item.text, item.id, true, true));
                        });
                        $('#fungsiPicInputEdit').append(options).trigger('change');
                    }
                }
            });
    }

    var streamId = $('#hdnStreamBusinessId').val();
    $('#streamBusinessInputEdit').select2({
        multiple: true,
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
    if (streamId) {
        multiple: true,
            $.ajax({
                url: url.getDdlAgreementStreamBusiness + "?guid=" + streamId,
                method: 'GET',
                success: function (data) {
                    if (data.items) {
                        var options = [];
                        $.each(data.items, function (index, item) {
                            options.push(new Option(item.text, item.id, true, true));
                        });
                        $('#streamBusinessInputEdit').append(options).trigger('change');
                    }
                }
            });
    }

    var lembaga = $('#hdnKementrianLembagaId').val();
    $('#keterlibatanLembagaInputEdit').select2({
        multiple: true,
        ajax: {
            url: url.ddlKeterlibatanLembaga,
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
    if (lembaga) {
        multiple: true,
            $.ajax({
                url: url.getDdlAgreementKeterlibatanLembaga + "?guid=" + lembaga,
                method: 'GET',
                success: function (data) {
                    if (data.items) {
                        var options = [];
                        $.each(data.items, function (index, item) {
                            options.push(new Option(item.text, item.id, true, true));
                        });
                        $('#keterlibatanLembagaInputEdit').append(options).trigger('change');
                    }
                }
            });
    }

    //Single
    $('#jenisPerjanjianInputEdit').select2({
        ajax: {
            url: url.ddlJenisPerjanjian,
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
    var jenisperjanjianId = $('#hdnJenisPerjanjianId').val();
    if (jenisperjanjianId) {
            $.ajax({
                url: url.getDdlAgreementJenisPerjanjian + "?guid=" + jenisperjanjianId,
                method: 'GET',
                success: function (data) {
                    if (data.items) {
                        var options = [];
                        $.each(data.items, function (index, item) {
                            options.push(new Option(item.text, item.id, true, true));
                        });
                        $('#jenisPerjanjianInputEdit').append(options).trigger('change');
                    }
                }
            });
    }
    $('#negaraMitraInputEdit').select2({
        ajax: {
            url: url.ddlNegaraMitra,
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
    var negaraId = $('#hdnNegaraId').val();
    if (negaraId) {
        $.ajax({
            url: url.getDdlAgreementNegara + "?guid=" + negaraId,
            method: 'GET',
            success: function (data) {
                if (data.items) {
                    var options = [];
                    $.each(data.items, function (index, item) {
                        options.push(new Option(item.text, item.id, true, true));
                    });
                    $('#negaraMitraInputEdit').append(options).trigger('change');
                }
            }
        });
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


    $('#statusDiskusiInputEdit').select2({
        ajax: {
            url: url.ddlStatusDiscussion,
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
    var statusDiskusi = $('#hdnStatusDiskusiId').val();
    if (statusDiskusi) {
        $.ajax({
            url: url.getDdlAgreementStatusDiscussion + "?guid=" + statusDiskusi,
            method: 'GET',
            success: function (data) {
                if (data.items) {
                    var options = [];
                    $.each(data.items, function (index, item) {
                        options.push(new Option(item.text, item.id, true, true));
                    });
                    $('#statusDiskusiInputEdit').append(options).trigger('change');
                }
            }
        });
    }

    $('#faktorKendalaInputEdit').select2({
        ajax: {
            url: url.ddlFaktorKendala,
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
    var faktorKendala = $('#hdnFaktorKendalaId').val();
    if (faktorKendala) {
        $.ajax({
            url: url.getDdlAgreementFaktorKendala + "?guid=" + faktorKendala,
            method: 'GET',
            success: function (data) {
                if (data.items) {
                    var options = [];
                    $.each(data.items, function (index, item) {
                        options.push(new Option(item.text, item.id, true, true));
                    });
                    $('#faktorKendalaInputEdit').append(options).trigger('change');
                }
            }
        });
    }

    $('#klasifikasiKendalaInputEdit').select2({
        ajax: {
            url: url.ddlKlasifikasiKendala,
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
    var klasifikasiKendala = $('#hdnKlasifikasiKendalaId').val();
    if (klasifikasiKendala) {
        $.ajax({
            url: url.getDdlAgreementKlasifikasiKendala + "?guid=" + klasifikasiKendala,
            method: 'GET',
            success: function (data) {
                if (data.items) {
                    var options = [];
                    $.each(data.items, function (index, item) {
                        options.push(new Option(item.text, item.id, true, true));
                    });
                    $('#klasifikasiKendalaInputEdit').append(options).trigger('change');
                }
            }
        });
    }
    
    $('#relasiAgreementInputEdit').select2({
        language: {
            inputTooShort: function (args) {
                return "Ketik minimal 3 karakter untuk pencarian partner atau judul perjanjian";
            }
        },
        ajax: {
            url: url.ddlRelatedAgreement,
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
            
        },
        minimumInputLength: 3
    });
    $('#editTrafficLightInput').select2({
        ajax: {
            url: url.ddlTrafficLight,
            method: 'GET',
            data: function (params) {
                return {
                    query: params.term
                };
            },
            processResults: function (data) {
                let processedItems = data.items.map(item => {
                    return {
                        id: item.id,
                        text: item.text,
                        color: item.color
                    };
                });

                return {
                    results: processedItems
                };
            }
        },
        templateResult: function (item) {
            if (!item.id) {
                return item.text;
            }
            return $(
                `<i style="color:${item.color}" class="fa-solid fa-circle"></i> <span>${item.text}</span>  + `
            );
        },
        templateSelection: function (item) {
            var color = item.color ? item.color : $(item.element).data('color');
            return $(
                `<i style="color:${color}" class="fa-solid fa-circle"></i> <span>${item.text}</span>  + `
            );
        }
    });
    var trafficLight = $('#hdnTrafficLightId').val();
    if (trafficLight) {
        $.ajax({
            url: url.getDdlAgreementTrafficLight + "?guid=" + trafficLight,
            method: 'GET',
            success: function (data) {
                if (data.items) {
                    if (data.items && data.items.length > 0) {
                        var selectedItem = data.items.find(x => x.id.toLowerCase() === trafficLight);
                        var option = new Option(selectedItem.text, selectedItem.id, true, true);
                        $(option).data('color', selectedItem.color);
                        $('#editTrafficLightInput').append(option).trigger('change');
                    }
                    
                }
            }
        });
    }
    var agreementId = $('#hdnRelatedId').val();
    if (agreementId) {
        $.ajax({
            url: url.getDdlAgreementRelatedAgreement + "?guid=" + agreementId,
            method: 'GET',
            success: function (data) {
                if (data.items) {
                    var options = [];
                    $.each(data.items, function (index, item) {
                        options.push(new Option(item.text, item.id, true, true));
                    });
                    $('#relasiAgreementInputEdit').append(options).trigger('change');
                }
            }
        });
    }
    var checked = $("#checkbox-g2g").val();
    if (checked != 'on') {
        $("#checkbox-g2g").prop("checked", true); 
    }

    //repeater
    var guid = $('#hdnAgreementId').val();
    $.ajax({
        url: param.getAgreementById,
        method: 'GET',
        data: { guid: guid },
        success: function (result) {
            var data = result.items;
            console.log(data)

            var partnerList = data.AgreementPartners;
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
        url: param.getAgreementById,
        method: 'GET',
        data: { guid: guid },
        success: function (result) {
            var data = result.items;
            console.log(data)

            var lokasiProyekList = data.AgreementLokasiProyeks;
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

    var levelId = $('#picLevelMemberId').val();
    $.ajax({
        url: param.getAgreementById,
        method: 'GET',
        data: { guid: guid},

        success: function (result) {
            if (result.items.AgreementPicFungsis.length !== 0) {
                var data = result.items.AgreementPicFungsis;
                var PicName = data;
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


})
$(document).ready(function () {
    $("#rangeTanggalBerakhirInputEdit").prop("readonly", true);
});

document.addEventListener('DOMContentLoaded', function () {
    const inputPotentialRevenue = document.getElementById('transactionInputEdit');
    const inputProjectCost = document.getElementById('projectCostInputEdit');
    const inputNilaiProyek = document.getElementById('nilaiProyekInputEdit');

    inputPotentialRevenue.addEventListener('input', function () {
        let value = inputPotentialRevenue.value.replace(/\./g, '').replace(/[^0-9]/g, '');
        if (value === '') {
            inputPotentialRevenue.value = '';
        } else {
            inputPotentialRevenue.value = new Intl.NumberFormat('id-ID').format(parseInt(value));
        }
    });

    inputProjectCost.addEventListener('input', function () {
        let value = inputProjectCost.value.replace(/\./g, '').replace(/[^0-9]/g, '');

        if (value === '') {
            inputProjectCost.value = '';
            return;
        }

        inputProjectCost.value = new Intl.NumberFormat('id-ID').format(parseInt(value));
    });

    inputNilaiProyek.addEventListener('input', function () {
        let value = inputNilaiProyek.value.replace(/\./g, '').replace(/[^0-9]/g, '');

        if (value === '') {
            inputNilaiProyek.value = '';
            return;
        }

        inputNilaiProyek.value = new Intl.NumberFormat('id-ID').format(parseInt(value));
    });
});