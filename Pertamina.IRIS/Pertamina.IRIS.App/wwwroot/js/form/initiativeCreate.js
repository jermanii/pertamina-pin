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
$(document).ready(function () {
    $("#CreateBlankInitiative").hide();
    $("#CreateOpportunityInitiative").hide();
    $('#judulInput').attr("disabled", "disabled");
    $('#judulInput').select2().val("").trigger('change');
    
    function ajaxPreselecting(urlAjaxPreselecting, ctrlClass) {
        $.ajax({
            type: 'GET',
            url: urlAjaxPreselecting
        }).then(function (data) {
            ctrlClass.find('option').remove();
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
    $("#sumberPengisianInput").select2({
        ajax: {
            url: url.ddlInitiativeForm,
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
    }).on("change", function (e) {
        $('#judulInput').select2().val("").trigger('change');
        $("#CreateBlankInitiative").hide();
        $("#CreateOpportunityInitiative").hide();
        $('#btnCancel').show();

        var value = $(this).val();
        if (value === "1") {
            $('#judulInput').removeAttr("disabled");
            $('#judulInput').select2().val("").trigger('change');
            $("#CreateOpportunityInitiative").hide();
            $("#CreateBlankInitiative").hide();
            $('#btnCancel').show();
            $("#judulInput").select2({
                language: {
                    inputTooShort: function (args) {
                        return "Ketik minimal 3 karakter untuk pencarian judul";
                    }
                },
                ajax: {
                    url: url.ddlNamaProyek,
                    method: 'GET',
                    data: function (params) {
                        return {
                            query: params.term,
                            value: value
                        };
                    },
                    processResults: function (data) {
                        return {
                            results: data.items
                        };
                    },
                    
                },
                minimumInputLength: 3
            }).on("change", function (e) {
                $('#judulInput').removeAttr("disabled");
                $("#CreateBlankInitiative").hide();
                $("#CreateOpportunityInitiative").show();
                $('#btnCancel').hide();

                var guid = $(this).val();
                if (guid) {
                    $.ajax({
                        url: url.getDdlOpportunityEntitasPertamina + "?guid=" + guid,
                        method: 'GET',
                        success: function (data) {
                            if (data.items) {
                                var options = [];
                                $.each(data.items, function (index, item) {
                                    options.push(new Option(item.text, item.id, true, true));
                                });
                                $('#hshEntitasPertaminaInputOpportunity').empty().append(options).trigger('change');
                            }
                        }
                    });
                    $.ajax({
                        url: url.getDdlOpportunityPicFungsi + "?guid=" + guid,
                        method: 'GET',
                        success: function (data) {
                            if (data.items) {
                                var options = [];
                                $.each(data.items, function (index, item) {
                                    options.push(new Option(item.text, item.id, true, true));
                                });
                                $('#fungsiPicInputOpportunity').empty().append(options).trigger('change');
                            }
                        }
                    });
                    $.ajax({
                        url: url.getDdlOpportunityNegara + "?guid=" + guid,
                        method: 'GET',
                        success: function (data) {
                            if (data.items) {
                                var options = [];
                                $.each(data.items, function (index, item) {
                                    options.push(new Option(item.text, item.id, true, true));
                                });
                                $('#negaraMitraInputOpportunity').empty().append(options).trigger('change');
                            }
                        }
                    });
                    $.ajax({
                        url: param.getOpportunityById,
                        method: 'GET',
                        data: { guid: guid },
                        success: function (result) {
                            var data = result.items;
                            console.log(data)
                            $("#nilaiProyekInputOpportunity").val(data.NilaiProyek);
                            $("#scopeProjectInputOpportunity").val(data.ProjectProfile);
                            $("#tindakLanjutInputOpportunity").val(data.TindakLanjut);
                            $("#progressInputOpportunity").val(data.Progress);
                            $("#catatanTambahanInputOpportunity").val(data.CatatanTambahan);
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
                            var partnerList = data.OpPartners;
                            $("#addRepeaterPartnerOpportunity .partnerOpportunity").remove();
                            partnerList.forEach(function (partner, index) {
                                var $repeaterItem = $("<div>").addClass("partnerOpportunity mb-2");
                                var $formGroup = $("<div>").addClass("fv-row form-group row");
                                var $colMd10 = $("<div>").addClass("col-md-10");
                                var $partnerInput = $("<input>").attr({
                                    type: "text",
                                    id: "partnerInputOpportunity",
                                    name: `PartnerName[${index}][InitiativePartner.PartnerName]`, // Set the input name with the index
                                    class: "form-control form-control-solid",
                                    maxlength: 100,
                                    placeholder: "Masukkan Partner",
                                    value: partner.PartnerName // Set the value for the input field
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

                                $("#addRepeaterPartnerOpportunity [data-repeater-list='PartnerName']").append($repeaterItem);

                                // Add event listener to the newly added input field
                                $partnerInput.on('change', function () {
                                    opportunityValidator.revalidateField($partnerInput);
                                });
                            });

                            $('#addRepeaterPartnerOpportunity').on('click', '[data-repeater-delete]', function () {
                                $(this).closest('.partnerOpportunity').slideUp(function () {
                                    $(this).remove();
                                    updatePartnerIndexing();
                                    updatePartnerArray();
                                    opportunityValidator.revalidateField('InitiativePartner.PartnerName[]');
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
                            var lokasiProyekList = data.OpLokasiProyeks;
                            $("#addRepeaterLokasiProyekOpportunity .lokasiProyekOpportunity").remove();
                            lokasiProyekList.forEach(function (lokasiProyek, index) {
                                var $repeaterItem = $("<div>").addClass("lokasiProyekOpportunity mb-2");
                                var $formGroup = $("<div>").addClass("fv-row form-group row");
                                var $colMd10 = $("<div>").addClass("col-md-10");
                                var $lokasiProyekInput = $("<input>").attr({
                                    type: "text",
                                    id: "lokasiProyekInputOpportunity",
                                    name: `LokasiProyek[${index}][InitiativeLokasiProyek.LokasiProyek]`, // Set the input name with the index
                                    class: "form-control form-control-solid",
                                    maxlength: 100,
                                    placeholder: "Masukkan Lokasi Proyek",
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

                                $("#addRepeaterLokasiProyekOpportunity [data-repeater-list='LokasiProyek']").append($repeaterItem);
                                $lokasiProyekInput.on('change', function () {
                                    opportunityValidator.revalidateField($lokasiProyekInput);
                                });
                            });

                            $('#addRepeaterLokasiProyekOpportunity').on('click', '[data-repeater-delete]', function () {
                                $(this).closest('.lokasiProyekOpportunity').slideUp(function () {
                                    $(this).remove();
                                    updateLokasiProyekIndexing();
                                    updateLokasiProyekArray();
                                    opportunityValidator.revalidateField('InitiativeLokasiProyek.LokasiProyek[]');
                                });
                            });
                        },
                        error: function (error) {
                            console.log(error);
                        }
                    });
                    $.ajax({
                        url: url.getDdlOpportunityStreamBusiness + "?guid=" + guid,
                        method: 'GET',
                        success: function (data) {
                            if (data.items) {
                                var options = [];
                                $.each(data.items, function (index, item) {
                                    options.push(new Option(item.text, item.id, true, true));
                                });
                                $('#streamBusinessInputOpportunity').empty().append(options).trigger('change');
                            }
                        }
                    });
                }
                else {
                    $("#CreateBlankInitiative").hide();
                    $("#CreateOpportunityInitiative").hide();
                    $("#judulinisiasiinput").val("").trigger("change");
                    $("#interestinput").val("").trigger("change");
                    $("#initiativestatusinput").val("").trigger("change");
                    $("#initiativetypeinput").val("").trigger("change");
                    $("#initiativestageinput").val("").trigger("change");
                    $("#fungsipicinput").val("").trigger("change");
                    $("#hshentitaspertaminainput").val("").trigger("change");
                    $("#partnerinput").val("").trigger("change");
                    $("#negaramitrainput").val("").trigger("change");
                    $("#nilaiproyekinput").val("").trigger("change");
                    $("#targetwaktuproyekinput").val("").trigger("change");
                    $("#lokasiproyekinput").val("").trigger("change");
                    $("#scopeprojectinput").val("").trigger("change");
                    $("#streambusinessinput").val("").trigger("change");
                    $("#progressinput").val("").trigger("change");
                    $("#isukendalainput").val("").trigger("change");
                    $("#tindakLanjutInput").val("").trigger("change");
                    $("#referalinput").val("").trigger("change");
                    $("#potensialeskalasiinput").val("").trigger("change");
                    $("#catatantambahaninput").val("").trigger("change");
                    $("#dukunganpemerintahinput").val("").trigger("change");
                    $("#valueforindonesiainput").val("").trigger("change");
                    $("#relasiagreementinput").val("").trigger("change");
                    $("#kementrianLembagaInput").val("").trigger("change");
                    $("#deskripsiKementrianLembagaInput").val("").trigger("change");
                }
            });

        }
        else if (value === "2") {
            $('#judulInput').attr("disabled", "disabled");
            $("#CreateBlankInitiative").show();
            $("#CreateOpportunityInitiative").hide();
            $('#btnCancel').hide();
        }
        else {
            $('#judulInput').attr("disabled", "disabled");
            $("#CreateBlankInitiative").hide();
            $("#CreateOpportunityInitiative").hide();
            $('#btnCancel').show();
        }
    });

});

$(function () {
    function updatePartnerIndexing() {
        $('#addRepeaterPartnerOpportunity .partnerOpportunity').each(function (index) {
            $(this).find('[name^="PartnerName"]').each(function () {
                var oldName = $(this).attr('name');
                var newName = oldName.replace(/\[\d+\]/, '[' + index + ']');
                $(this).attr('name', newName);
            });
        });
    }
    function updatePartnerArray() {
        var partnerArray = [];
        $('#addRepeaterPartnerOpportunity .partnerOpportunity').each(function () {
            var partnerName = $(this).find('[name^="PartnerName"]').val();
            partnerArray.push({ 'InitiativePartner.PartnerName': partnerName });
        });
        console.log(partnerArray);
    }

    $('#addRepeaterPartnerOpportunity').repeater({
        initEmpty: false,
        show: function () {
            $(this).slideDown();
            updatePartnerIndexing();
        },
        hide: function (deleteElement) {
            $(this).slideUp(deleteElement, function () {
                $(this).remove();
                updatePartnerIndexing();
                updatePartnerArray();
            });
        }
    });

    $('#addRepeaterLokasiProyekOpportunity').repeater({
        initEmpty: false,

        show: function () {
            $(this).slideDown();
            updateLokasiProyekIndexing();
        },

        hide: function (deleteElement) {
            $(this).slideUp(deleteElement);
            updateLokasiProyekIndexing();
            updateLokasiProyekArray();
        }
    });

    function updateLokasiProyekIndexing() {
        $('#addRepeaterLokasiProyekOpportunity .lokasiProyekOpportunity').each(function (index) {
            $(this).find('[name^="LokasiProyek"]').each(function () {
                var oldName = $(this).attr('name');
                var newName = oldName.replace(/\[\d+\]/, '[' + index + ']');
                $(this).attr('name', newName);
            });
        });
    }

    function updateLokasiProyekArray() {
        var lokasiProyekArray = [];
        $('#addRepeaterLokasiProyekOpportunity .lokasiProyekOpportunity').each(function () {
            var lokasiProyekName = $(this).find('[name^="LokasiProyek"]').val();
            lokasiProyekArray.push({ 'InitiativeLokasiProyek.LokasiProyek': lokasiProyekName });
        });
        console.log(lokasiProyekArray);
    }

    //Multiple
    var entitasId = $('#hdnEntitasId').val();
    $('#hshEntitasPertaminaInputEdit').select2({
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
    if (entitasId) {
        multiple: true,
            $.ajax({
                url: url.getDdlInitiativeEntitasPertamina + "?guid=" + entitasId,
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
                url: url.getDdlInitiativePicFungsi + "?guid=" + picId,
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
                url: url.getDdlInitiativeStreamBusiness + "?guid=" + streamId,
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

    var kementrianLembagaId = $('#hdnLembagaId').val();
    $("#kementrianLembagaInput, #kementrianLembagaInputEdit").select2({
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
    if (kementrianLembagaId) {
        multiple: true,
            $.ajax({
                url: url.getDdlInitiativeKeterlibatanLembaga + "?guid=" + kementrianLembagaId,
                method: 'GET',
                success: function (data) {
                    if (data.items) {
                        var options = [];
                        $.each(data.items, function (index, item) {
                            options.push(new Option(item.text, item.id, true, true));
                        });
                        $('#kementrianLembagaInputEdit').append(options).trigger('change');
                    }
                }
            });
    }
    //Single
    var interestId = $('#hdnInterestId').val();
    $('#interestInputEdit').select2({
        ajax: {
            url: url.ddlInterest,
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
    if (!interestId) {
        $('#interestInputEdit').val(null).trigger('change');
    }
    else {
        ajaxPreselecting(url.getDdlInterest + "?guid=" + interestId, $('#interestInputEdit'));
    }

    var typesId = $('#hdnTypesId').val();
    $('#initiativeTypeInputEdit').select2({
        ajax: {
            url: url.ddlInitiativeTypes,
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
    if (!typesId) {
        $('#initiativeTypeInputEdit').val(null).trigger('change');
    }
    else {
        ajaxPreselecting(url.getDdlInitiativeTypes + "?guid=" + typesId, $('#initiativeTypeInputEdit'));
    }

    var stagesId = $('#hdnStagesId').val();
    $('#initiativeStageInputEdit').select2({
        ajax: {
            url: url.ddlInitiativeStages,
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
    if (!stagesId) {
        $('#initiativeStageInputEdit').val(null).trigger('change');
    }
    else {
        ajaxPreselecting(url.getDdlInitiativeStages + "?guid=" + stagesId, $('#initiativeStageInputEdit'));
    }

    var statusId = $('#hdnStatusId').val();
    $('#initiativeStatusInputEdit').select2({
        ajax: {
            url: url.ddlInitiativeStatus,
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
    if (!statusId) {
        $('#initiativeStatusInputEdit').val(null).trigger('change');
    }
    else {
        ajaxPreselecting(url.getDdlInitiativeStatus + "?guid=" + statusId, $('#initiativeStatusInputEdit'));
    }

    var negaraId = $('#hdnNegaraId').val();
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
    ajaxPreselecting(url.getDdlNegaraMitra + "?guid=" + negaraId, $('#negaraMitraInputEdit'));

    var agreementId = $('#hdnRelatedId').val();
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
    ajaxPreselecting(url.getDdlRelatedAgreement + "?guid=" + agreementId, $('#relasiAgreementInputEdit'));

    //repeater
    var guid = $('#hdnInitiativeId').val();
    $.ajax({
        url: param.getInitiativeById,
        method: 'GET',
        data: { guid: guid },
        success: function (result) {
            var data = result.items;
            console.log(data)

            var partnerList = data.InitiativePartners;
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
        url: param.getInitiativeById,
        method: 'GET',
        data: { guid: guid },
        success: function (result) {
            var data = result.items;
            console.log(data)

            var lokasiProyekList = data.InitiativeLokasiProyeks;
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
})

$(function () {
    const form = document.getElementById('EditFormInitiative');
    var validator = FormValidation.formValidation(
        form,
        {
            fields: {
                'JudulInisiasi': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'InterestId': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'InitiativeStatusId': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'InitiativeTypesId': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'InitiativeStageId': {
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
                'Scope': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'StreamBusinessId': {
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
                'InitiativePartner.PartnerName': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'InitiativeLokasiProyek.LokasiProyek': {
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
        });
    const updateButton = document.getElementById('UpdateSubmitInitiative');
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
                            var id = $('#hdnInitiativeId').val();

                            var model = {
                                Id: id,
                                InitiativePartners: [],
                                InitiativeLokasiProyeks: []
                            };

                            $('.partnerEdit').each(function () {

                                var partnerName = $(this).find('#editpartnerInput').val();

                                var partner = {
                                    PartnerName: partnerName,
                                };
                                model.InitiativePartners.push(partner);
                            });

                            $('.lokasiProyekEdit').each(function () {

                                var lokasiProyek = $(this).find('#editlokasiProyekProvInput').val();

                                var lokasi = {
                                    LokasiProyek: lokasiProyek
                                };
                                model.InitiativeLokasiProyeks.push(lokasi);
                            });
                            model.OpportunityId = $("#judulInputEdit").val();
                            model.JudulInisiasi = $("#judulInisiasiInputEdit").val();
                            model.InterestId = $("#interestInputEdit").val();
                            model.InitiativeStageId = $("#initiativeStageInputEdit").val();
                            model.InitiativeTypesId = $("#initiativeTypeInputEdit").val();
                            model.InitiativeStatusId = $("#initiativeStatusInputEdit").val();
                            model.EntitasPertaminaId = $("#hshEntitasPertaminaInputEdit").val();
                            model.FungsiPicId = $("#fungsiPicInputEdit").val();
                            model.NegaraMitraId = $("#negaraMitraInputEdit").val();
                            model.NilaiProyek = $("#nilaiProyekInputEdit").val();
                            model.TargetWaktuProyek = $("#targetWaktuProyekInputEdit").val();
                            model.Scope = $("#scopeProjectInputEdit").val();
                            model.StreamBusinessId = $("#streamBusinessInputEdit").val();
                            model.Progress = $("#progressInputEdit").val();
                            model.IsuKendala = $("#isuKendalaInputEdit").val();
                            model.TindakLanjut = $("#tindakLanjutInputEdit").val();
                            model.Referal = $("#referalInputEdit").val();
                            model.PotensiEskalasi = $("#potensialEskalasiInputEdit").val();
                            model.CatatanTambahan = $("#catatanTambahanInputEdit").val();
                            model.SupportPemerintah = $("#dukunganPemerintahInputEdit").val();
                            model.ValueForIndonesia = $("#valueForIndonesiaInputEdit").val();
                            model.AgreementId = $("#relasiAgreementInputEdit").val();
                            model.KementrianLembagaId = $("#kementrianLembagaInputEdit").val();
                            model.DeskripsiKementrianLembaga = $("#deskripsiKementrianLembagaInputEdit").val();


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

$(function () {
    const blankForm = document.getElementById('AddInitiative');
    var blankValidator = FormValidation.formValidation(
        blankForm,
        {
            fields: {
                'JudulInisiasi': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'InterestId': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'InitiativeStatusId': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'InitiativeTypesId': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'InitiativeStageId': {
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
                'EntitasPertaminaId': {
                    validators: {
                        notEmpty: {
                            message: 'field can not be empty!'
                        }
                    }
                },
                'InitiativePartner.PartnerName': {
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
                'InitiativeLokasiProyek.LokasiProyek': {
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
                'StreamBusinessId': {
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

    const submitBlankButton = document.getElementById('InitiativeBlankCreateSubmit');
    submitBlankButton.addEventListener('click', function (e) {
        e.preventDefault();
        var callBackUrlPartial = $(this).data("callbackurl");
        if (blankValidator) {
            blankValidator.validate().then(function (status) {
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
                            submitBlankButton.setAttribute('data-kt-indicator', 'on');
                            submitBlankButton.disabled = true;

                            var urlSubmit = submitBlankButton.dataset.url;

                            var model = {
                                InitiativePartners: [],
                                InitiativeLokasiProyeks: []
                            };
                            model.JudulInisiasi = $("#judulInisiasiInput").val();
                            model.InterestId = $("#interestInput").val();
                            model.InitiativeStatusId = $("#initiativeStatusInput").val();
                            model.InitiativeTypesId = $("#initiativeTypeInput").val();
                            model.InitiativeStageId = $("#initiativeStageInput").val();
                            model.FungsiPicId = $("#fungsiPicInput").val();
                            model.EntitasPertaminaId = $("#hshEntitasPertaminaInput").val();
                            $('.partner').each(function () {

                                var partnerName = $(this).find('#partnerInput').val();
                                var partner = {
                                    PartnerName: partnerName,
                                };
                                model.InitiativePartners.push(partner);
                            });
                            $('.lokasiProyek').each(function () {
                                var lokasiProyek = $(this).find('#lokasiProyekInput').val();

                                var lokasi = {
                                    LokasiProyek: lokasiProyek
                                };
                                model.InitiativeLokasiProyeks.push(lokasi);
                            });
                            model.NegaraMitraId = $("#negaraMitraInput").val();
                            model.NilaiProyek = $("#nilaiProyekInput").val();
                            model.TargetWaktuProyek = $("#targetWaktuProyekInput").val();
                            model.LokasiProyek = $("#lokasiProyekInput").val();
                            model.Scope = $("#scopeProjectInput").val();
                            model.StreamBusinessId = $("#streamBusinessInput").val();
                            model.Progress = $("#progressInput").val();
                            model.IsuKendala = $("#isuKendalaInput").val();
                            model.TindakLanjut = $("#tindakLanjutInput").val();
                            model.Referal = $("#referalInput").val();
                            model.Research = $("#researchInput").val();
                            model.PotensiEskalasi = $("#potensialEskalasiInput").val();
                            model.CatatanTambahan = $("#catatanTambahanInput").val();
                            model.SupportPemerintah = $("#dukunganPemerintahInput").val();
                            model.ValueForIndonesia = $("#valueForIndonesiaInput").val();
                            model.AgreementId = $("#relasiAgreementInput").val();
                            model.KementrianLembagaId = $("#kementrianLembagaInput").val();
                            model.DeskripsiKementrianLembaga = $("#deskripsiKementrianLembagaInput").val();

                            $.ajax({
                                type: "POST",
                                url: urlSubmit,
                                data: model,
                                success: function (data) {
                                    if (data.IsError) {
                                        submitBlankButton.removeAttribute('data-kt-indicator');
                                        submitBlankButton.disabled = false;
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
                                        submitBlankButton.removeAttribute('data-kt-indicator');
                                        submitBlankButton.disabled = false;
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

    const draftButton = document.getElementById('SaveDraftInitiative');
    draftButton.addEventListener('click', function (e) {
        e.preventDefault();
        var callBackUrlPartial = $(this).data("callbackurl");
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
                draftButton.setAttribute('data-kt-indicator', 'on');
                draftButton.disabled = true;

                var urlSubmit = draftButton.dataset.url;

                var model = {
                    InitiativePartners: [],
                    InitiativeLokasiProyeks: []
                };
                model.JudulInisiasi = $("#judulInisiasiInput").val();
                model.InterestId = $("#interestInput").val();
                model.InitiativeStatusId = $("#initiativeStatusInput").val();
                model.InitiativeTypesId = $("#initiativeTypeInput").val();
                model.InitiativeStageId = $("#initiativeStageInput").val();
                model.FungsiPicId = $("#fungsiPicInput").val();
                model.EntitasPertaminaId = $("#hshEntitasPertaminaInput").val();
                $('.partner').each(function () {

                    var partnerName = $(this).find('#partnerInput').val();
                    var partner = {
                        PartnerName: partnerName,
                    };
                    model.InitiativePartners.push(partner);
                });
                $('.lokasiProyek').each(function () {
                    var lokasiProyek = $(this).find('#lokasiProyekInput').val();

                    var lokasi = {
                        LokasiProyek: lokasiProyek
                    };
                    model.InitiativeLokasiProyeks.push(lokasi);
                });
                model.NegaraMitraId = $("#negaraMitraInput").val();
                model.NilaiProyek = $("#nilaiProyekInput").val();
                model.TargetWaktuProyek = $("#targetWaktuProyekInput").val();
                model.LokasiProyek = $("#lokasiProyekInput").val();
                model.Scope = $("#scopeProjectInput").val();
                model.StreamBusinessId = $("#streamBusinessInput").val();
                model.Progress = $("#progressInput").val();
                model.IsuKendala = $("#isuKendalaInput").val();
                model.TindakLanjut = $("#tindakLanjutInput").val();
                model.Referal = $("#referalInput").val();
                model.Research = $("#researchInput").val();
                model.PotensiEskalasi = $("#potensialEskalasiInput").val();
                model.CatatanTambahan = $("#catatanTambahanInput").val();
                model.SupportPemerintah = $("#dukunganPemerintahInput").val();
                model.ValueForIndonesia = $("#valueForIndonesiaInput").val();
                model.AgreementId = $("#relasiAgreementInput").val();
                model.KementrianLembagaId = $("#kementrianLembagaInput").val();
                model.DeskripsiKementrianLembaga = $("#deskripsiKementrianLembagaInput").val();

                $.ajax({
                    type: "POST",
                    url: urlSubmit,
                    data: model,
                    success: function (data) {
                        if (data.IsError) {
                            draftButton.removeAttribute('data-kt-indicator');
                            draftButton.disabled = false;
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
                            draftButton.removeAttribute('data-kt-indicator');
                            draftButton.disabled = false;
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


    });

    const draftButtonOpportunity = document.getElementById('SaveDraftInitiativeOpportunity');
    draftButtonOpportunity.addEventListener('click', function (e) {
        e.preventDefault();
        var callBackUrlPartial = $(this).data("callbackurl");
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
                draftButtonOpportunity.setAttribute('data-kt-indicator', 'on');
                draftButtonOpportunity.disabled = true;

                var urlSubmit = draftButtonOpportunity.dataset.url;

                var model = {
                    InitiativePartners: [],
                    InitiativeLokasiProyeks: []
                };
                model.JudulInisiasi = $("#judulInisiasiInputOpportunity").val();
                model.InterestId = $("#interestInputOpportunity").val();
                model.InitiativeStatusId = $("#initiativeStatusInputOpportunity").val();
                model.InitiativeTypesId = $("#initiativeTypeInputOpportunity").val();
                model.InitiativeStageId = $("#initiativeStageInputOpportunity").val();
                model.FungsiPicId = $("#fungsiPicInputOpportunity").val();
                model.EntitasPertaminaId = $("#hshEntitasPertaminaInputOpportunity").val();
                $('.partnerOpportunity').each(function () {

                    var partnerNameOpportunity = $(this).find('#partnerInputOpportunity').val();
                    var partner = {
                        PartnerName: partnerNameOpportunity,
                    };
                    model.InitiativePartners.push(partner);
                });

                $('.lokasiProyekOpportunity').each(function () {
                    var lokasiProyekOpportunity = $(this).find('#lokasiProyekInputOpportunity').val();

                    var lokasi = {
                        LokasiProyek: lokasiProyekOpportunity
                    };
                    model.InitiativeLokasiProyeks.push(lokasi);
                });
                model.NegaraMitraId = $("#negaraMitraInputOpportunity").val();
                model.NilaiProyek = $("#nilaiProyekInputOpportunity").val();
                model.TargetWaktuProyek = $("#targetWaktuProyekInputOpportunity").val();
                model.LokasiProyek = $("#lokasiProyekInputOpportunity").val();
                model.Scope = $("#scopeProjectInputOpportunity").val();
                model.StreamBusinessId = $("#streamBusinessInputOpportunity").val();
                model.Progress = $("#progressInputOpportunity").val();
                model.IsuKendala = $("#isuKendalaInputOpportunity").val();
                model.TindakLanjut = $("#tindakLanjutInputOpportunity").val();
                model.Referal = $("#referalInputOpportunity").val();
                model.Research = $("#researchInputOpportunity").val();
                model.PotensiEskalasi = $("#potensialEskalasiInputOpportunity").val();
                model.CatatanTambahan = $("#catatanTambahanInputOpportunity").val();
                model.SupportPemerintah = $("#dukunganPemerintahInputOpportunity").val();
                model.ValueForIndonesia = $("#valueForIndonesiaInputOpportunity").val();
                model.AgreementId = $("#relasiAgreementInputOpportunity").val();                

                $.ajax({
                    type: "POST",
                    url: urlSubmit,
                    data: model,
                    success: function (data) {
                        if (data.IsError) {
                            draftButtonOpportunity.removeAttribute('data-kt-indicator');
                            draftButtonOpportunity.disabled = false;
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
                            draftButtonOpportunity.removeAttribute('data-kt-indicator');
                            draftButtonOpportunity.disabled = false;
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


    });

    const opportunityForm = document.getElementById('AddOpportunityInitiative');
    var opportunityValidator = FormValidation.formValidation(
        opportunityForm,
        {
            fields: {
                'JudulInisiasi': {
                    validators: {
                        notEmpty: {
                            message: 'field cannot be empty!'
                        }
                    }
                },
                'InterestId': {
                    validators: {
                        notEmpty: {
                            message: 'field cannot be empty!'
                        }
                    }
                },
                'InitiativeStatusId': {
                    validators: {
                        notEmpty: {
                            message: 'field cannot be empty!'
                        }
                    }
                },
                'InitiativeTypesId': {
                    validators: {
                        notEmpty: {
                            message: 'field cannot be empty!'
                        }
                    }
                },
                'InitiativeStageId': {
                    validators: {
                        notEmpty: {
                            message: 'field cannot be empty!'
                        }
                    }
                },
                'FungsiPicId': {
                    validators: {
                        notEmpty: {
                            message: 'field cannot be empty!'
                        }
                    }
                },
                'EntitasPertaminaId': {
                    validators: {
                        notEmpty: {
                            message: 'field cannot be empty!'
                        }
                    }
                },
                'NegaraMitraId': {
                    validators: {
                        notEmpty: {
                            message: 'field cannot be empty!'
                        }
                    }
                },
                'Scope': {
                    validators: {
                        notEmpty: {
                            message: 'field cannot be empty!'
                        }
                    }
                },
                'StreamBusinessId': {
                    validators: {
                        notEmpty: {
                            message: 'field cannot be empty!'
                        }
                    }
                },
                'Progress': {
                    validators: {
                        notEmpty: {
                            message: 'field cannot be empty!'
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

    const submitOpportunityButton = document.getElementById('InitiativeOpportunityCreateSubmit');
    submitOpportunityButton.addEventListener('click', function (e) {
        e.preventDefault();
        var callBackUrlPartial = $(this).data("callbackurl");
        if (opportunityValidator) {
            opportunityValidator.validate().then(function (status) {
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
                            submitOpportunityButton.setAttribute('data-kt-indicator', 'on');
                            submitOpportunityButton.disabled = true;

                            var urlSubmit = submitOpportunityButton.dataset.url;

                            var model = {
                                InitiativePartners: [],
                                InitiativeLokasiProyeks: []
                            };
                            model.OpportunityId = $("#judulInput").val();
                            model.JudulInisiasi = $("#judulInisiasiInputOpportunity").val();
                            model.InterestId = $("#interestInputOpportunity").val();
                            model.InitiativeStatusId = $("#initiativeStatusInputOpportunity").val();
                            model.InitiativeTypesId = $("#initiativeTypeInputOpportunity").val();
                            model.InitiativeStageId = $("#initiativeStageInputOpportunity").val();
                            model.FungsiPicId = $("#fungsiPicInputOpportunity").val();
                            model.EntitasPertaminaId = $("#hshEntitasPertaminaInputOpportunity").val();
                            $('.partnerOpportunity').each(function () {

                                var partnerNameOpportunity = $(this).find('#partnerInputOpportunity').val();
                                var partner = {
                                    PartnerName: partnerNameOpportunity,
                                };
                                model.InitiativePartners.push(partner);
                            });
                            $('.lokasiProyekOpportunity').each(function () {
                                var lokasiProyekOpportunity = $(this).find('#lokasiProyekInputOpportunity').val();

                                var lokasi = {
                                    LokasiProyek: lokasiProyekOpportunity
                                };
                                model.InitiativeLokasiProyeks.push(lokasi);
                            });
                            model.NegaraMitraId = $("#negaraMitraInputOpportunity").val();
                            model.NilaiProyek = $("#nilaiProyekInputOpportunity").val();
                            model.TargetWaktuProyek = $("#targetWaktuProyekInputOpportunity").val();
                            //model.LokasiProyek = $("#lokasiProyekInputOpportunity").val();
                            model.Scope = $("#scopeProjectInputOpportunity").val();
                            model.StreamBusinessId = $("#streamBusinessInputOpportunity").val();
                            model.Progress = $("#progressInputOpportunity").val();
                            model.IsuKendala = $("#isuKendalaInputOpportunity").val();
                            model.TindakLanjut = $("#tindakLanjutInputOpportunity").val();
                            model.Referal = $("#referalInputOpportunity").val();
                            model.Research = $("#researchInputOpportunity").val();
                            model.PotensiEskalasi = $("#potensialEskalasiInputOpportunity").val();
                            model.CatatanTambahan = $("#catatanTambahanInputOpportunity").val();
                            model.SupportPemerintah = $("#dukunganPemerintahInputOpportunity").val();
                            model.ValueForIndonesia = $("#valueForIndonesiaInputOpportunity").val();
                            model.AgreementId = $("#relasiAgreementInputOpportunity").val();                            

                            $.ajax({
                                type: "POST",
                                url: urlSubmit,
                                data: model,
                                success: function (data) {
                                    if (data.IsError) {
                                        submitOpportunityButton.removeAttribute('data-kt-indicator');
                                        submitOpportunityButton.disabled = false;
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
                                        submitOpportunityButton.removeAttribute('data-kt-indicator');
                                        submitOpportunityButton.disabled = false;
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


    $("#interestInput, #interestInputOpportunity").select2({
        ajax: {
            url: url.ddlInterest,
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
    $("#initiativeStatusInput, #initiativeStatusInputOpportunity").select2({
        ajax: {
            url: url.ddlInitiativeStatus,
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
    $("#initiativeTypeInput, #initiativeTypeInputOpportunity").select2({
        ajax: {
            url: url.ddlInitiativeTypes,
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
    $("#initiativeStageInput, #initiativeStageInputOpportunity").select2({
        ajax: {
            url: url.ddlInitiativeStages,
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
    $("#kawasanMitraInput, #kawasanMitraInputOpportunity").select2({
        ajax: {
            url: url.ddlKawasanMitra,
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
    $("#negaraMitraInput, #negaraMitraInputOpportunity").select2({
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
    $("#streamBusinessInput,#streamBusinessInputOpportunity ").select2({
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
    $("#fungsiPicInput,#fungsiPicInputOpportunity").select2({
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
    $("#relasiAgreementInput,#relasiAgreementInputOpportunity ").select2({
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
            minimumInputLength: 2
        },
        minimumInputLength: 3

    });
    $("#hshEntitasPertaminaInput, #hshEntitasPertaminaInputOpportunity, #hshEntitasPertaminaInputEdit").select2({
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
    $("#kementrianLembagaInput, #kementrianLembagaInputEdit").select2({
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

});
