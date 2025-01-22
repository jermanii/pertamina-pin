$(function () {
    const blankForm = document.getElementById('AddBlank');
    var blankValidator = FormValidation.formValidation(
        blankForm,
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
                'FungsiPicId': {
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
                'AgreementPartner.PartnerName': {
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
                'AgreementLokasiProyek.LokasiProyek': {
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
        }
    );
    //fungsiPicInput
    const submitBlankButton = document.getElementById('BlankSubmit');
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
                                AgreementPartners: [],
                                AgreementLokasiProyeks: [],
                                AgreementPicFungsis:[]
                            };
                            model.JudulPerjanjian = $("#judulPerjanjianInput").val();
                            model.JenisPerjanjianId = $("#jenisPerjanjianInput").val();
                            model.EntitasPertaminaEvent = $("#hshEntitasPertaminaInput").val();
                            model.PicFungsiId = $("#picInput").val();
                            model.PicLevelId = $("#picLevelId").val();
                            //$('.partner').each(function () {

                            //    var partnerName = $(this).find('#partnerInput').val();
                            //    var partner = {
                            //        PartnerName: partnerName,
                            //    };
                            //    model.AgreementPartners.push(partner);
                            //});
                            $('.partnerOpportunity').each(function () {
                                var partnerName = $(this).find('#partnerInputOpportunity').val();
                                    var partner = {
                                        PartnerName: partnerName
                                    };
                                    model.AgreementPartners.push(partner);
                            });
                            $('.lokasiProyek').each(function () {
                                var lokasiProyek = $(this).find('#lokasiProyekInput').val();
                                    var lokasi = {
                                        LokasiProyek: lokasiProyek
                                    };
                                    model.AgreementLokasiProyeks.push(lokasi);
                            });
                            $('.picFungsi').each(function () {

                                var picFungsi = $(this).find('#picFungsiMemberInput').val();
                                    var picFungsiLevelMember = $(this).find('#picLevelMemberInput').val();
                                    var pic = {
                                        PicFungsiId: picFungsi,
                                        PicLevelMemberId: picFungsiLevelMember
                                    };
                                    model.AgreementPicFungsis.push(pic);
                            });

                            model.NegaraMitraId = $("#negaraMitraInput").val();
                            model.ValuationToString = $("#nilaiProyekInput").val();
                            model.TanggalTtd = $("#rangeTanggalTtdInput").val();
                            model.TanggalBerakhir = $("#rangeTanggalBerakhirInput").val();
                            model.StatusBerlakuId = $("#statusBerlakuValueInput").val();
                            model.DiscussionStatusId = $("#statusDiskusiInput").val();
                            model.Scope = $("#scopeProjectInput").val();
                            model.StreamBusinessIds = $("#streamBusinessInput").val();
                            model.Progress = $("#progressInput").val();
                            model.FaktorKendalaId = $("#faktorKendalaInput").val();
                            model.KlasifikasiKendalaId = $("#klasifikasiKendalaInput").val();
                            model.DeskripsiKendala = $("#deskripsiKendalaInput").val();
                            model.TindakLanjut = $("#tindakLanjutInput").val();
                            model.SupportPemerintah = $("#dukunganPemerintahInput").val();
                            model.PotensiEskalasi = $("#potensialEskalasiInput").val();
                            model.CatatanTambahan = $("#catatanTambahanInput").val();
                            model.RelatedAgreementId = $("#relasiAgreementInput").val();
                            model.KementrianLembagaIds = $("#keterlibatanLembagaInput").val();
                            model.PotentialRevenuePerYearToString = $("#transactionValueInput").val();
                            model.CapexToString = $("#projectCostInput").val();
                            model.IsGtg = $(".checkbox-g2g").is(":checked");

                            model.TrafficLightId = $("#trafficLightInput").val();
                            model.TindakLanjut = $("#tindakLanjutInput").val();

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

    const draftBlankButton = document.getElementById('SaveDraftBlank');
    draftBlankButton.addEventListener('click', function (e) {
        e.preventDefault();
        var callBackUrlPartial = $(this).data("callbackurl");
        Swal.fire({
            text: "Are you sure you want to save this data as a Draft ?",
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
                draftBlankButton.setAttribute('data-kt-indicator', 'on');
                draftBlankButton.disabled = true;

                var urlSubmit = draftBlankButton.dataset.url;

                var model = {
                    AgreementPartners: [],
                    AgreementLokasiProyeks: [],
                    AgreementPicFungsis:[]

                };
                model.JudulPerjanjian = $("#judulPerjanjianInput").val();
                model.JenisPerjanjianId = $("#jenisPerjanjianInput").val();
                model.EntitasPertaminaEvent = $("#hshEntitasPertaminaInput").val();
                model.FungsiPicId = $("#fungsiPicInput").val();
                model.PicFungsiId = $("#picInput").val();
                model.PicLevelId = $("#picLevelId").val();
                $('.partnerOpportunity').each(function () {

                    var partnerName = $(this).find('#partnerInputOpportunity').val();
                    if (partnerName != '') {
                        var partner = {
                            PartnerName: partnerName
                        };
                        model.AgreementPartners.push(partner);
                    }
                    
                });
                $('.lokasiProyek').each(function () {
                    var lokasiProyek = $(this).find('#lokasiProyekInput').val();
                    if (lokasiProyek != '') {
                        var lokasi = {
                            LokasiProyek: lokasiProyek
                        };
                        model.AgreementLokasiProyeks.push(lokasi);
                    }
                });

                $('.picFungsi').each(function () {
                    var picFungsi = $(this).find('#picFungsiMemberInput').val();
                    if (picFungsi != null) {
                        var picFungsiLevelMember = $(this).find('#picLevelMemberInput').val();
                        var pic = {
                            PicFungsiId: picFungsi,
                            PicLevelMemberId: picFungsiLevelMember
                        };
                        model.AgreementPicFungsis.push(pic);
                    }
                    
                });
                model.NegaraMitraId = $("#negaraMitraInput").val();
                model.ValuationToString = $("#nilaiProyekInput").val();
                model.TanggalTtd = $("#rangeTanggalTtdInput").val();
                model.TanggalBerakhir = $("#rangeTanggalBerakhirInput").val();
                model.StatusBerlakuId = $("#statusBerlakuValueInput").val();
                model.DiscussionStatusId = $("#statusDiskusiInput").val();
                model.Scope = $("#scopeProjectInput").val();
                model.StreamBusinessIds = $("#streamBusinessInput").val();
                model.Progress = $("#progressInput").val();
                model.FaktorKendalaId = $("#faktorKendalaInput").val();
                model.KlasifikasiKendalaId = $("#klasifikasiKendalaInput").val();
                model.DeskripsiKendala = $("#deskripsiKendalaInput").val();
                model.TindakLanjut = $("#tindakLanjutInput").val();
                model.SupportPemerintah = $("#dukunganPemerintahInput").val();
                model.KementrianLembagaIds = $("#keterlibatanLembagaInput").val();

                model.PotensiEskalasi = $("#potensialEskalasiInput").val();
                model.CatatanTambahan = $("#catatanTambahanInput").val();
                model.RelatedAgreementId = $("#relasiAgreementInput").val();
                model.KementrianLembaga = $("#keterlibatanLembagaInput").val();

                model.PotentialRevenuePerYearToString = $("#transactionValueInput").val();
                model.CapexToString = $("#projectCostInput").val();
                model.IsGtg = $(".checkbox-g2g").is(":checked");

                model.TrafficLightId = $("#trafficLightInput").val();
                model.TindakLanjut = $("#tindakLanjutInput").val();

                $.ajax({
                    type: "POST",
                    url: urlSubmit,
                    data: model,
                    success: function (data) {
                        if (data.IsError) {
                            draftBlankButton.removeAttribute('data-kt-indicator');
                            draftBlankButton.disabled = false;
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
                            draftBlankButton.removeAttribute('data-kt-indicator');
                            draftBlankButton.disabled = false;
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

    const draftOpportunityButton = document.getElementById('SaveDraftOpportunity');
    draftOpportunityButton.addEventListener('click', function (e) {
        e.preventDefault();
        var callBackUrlPartial = $(this).data("callbackurl");
        Swal.fire({
            text: "Are you sure you want to save this data as a Draft?",
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
                draftOpportunityButton.setAttribute('data-kt-indicator', 'on');
                draftOpportunityButton.disabled = true;

                var urlSubmit = draftOpportunityButton.dataset.url;

                var model = {
                    AgreementPartners: [],
                    AgreementLokasiProyeks: []
                };
                model.JudulPerjanjian = $("#judulPerjanjianInputOpportunity").val();
                model.JenisPerjanjianId = $("#jenisPerjanjianInputOpportunity").val();
                model.EntitasPertaminaEvent = $("#hshEntitasPertaminaInputOpportunity").val();
                model.FungsiPicId = $("#fungsiPicInputOpportunity").val();
                model.StreamBusinessIds = $("#streamBusinessInputOpportunity").val();
                $('.partnerOpportunity').each(function () {

                    var partnerName = $(this).find('#partnerInputOpportunity').val();
                    var partner = {
                        PartnerName: partnerName
                    };
                    model.AgreementPartners.push(partner);
                });
                $('.lokasiProyekOpportunity').each(function () {
                    var lokasiProyek = $(this).find('#lokasiProyekInputOpportunity').val();

                    var lokasi = {
                        LokasiProyek: lokasiProyek
                    };
                    model.AgreementLokasiProyeks.push(lokasi);
                });
                model.NegaraMitraId = $("#negaraMitraInputOpportunity").val();
                model.NilaiProyek = $("#nilaiProyekInputOpportunity").val();
                model.TanggalTtd = $("#rangeTanggalTtdInputOpportunity").val();
                model.TanggalBerakhir = $("#rangeTanggalBerakhirInputOpportunity").val();
                model.StatusBerlakuId = $("#statusBerlakuValueInputOpportunity").val();
                model.DiscussionStatusId = $("#statusDiskusiInputOpportunity").val();
                model.Scope = $("#scopeProjectInputOpportunity").val();
                model.StreamBusinessIds = $("#streamBusinessInputOpportunity").val();
                model.Progress = $("#progressInputOpportunity").val();
                model.FaktorKendalaId = $("#faktorKendalaInputOpportunity").val();
                model.KlasifikasiKendalaId = $("#klasifikasiKendalaInputOpportunity").val();
                model.DeskripsiKendala = $("#deskripsiKendalaInputOpportunity").val();
                model.TindakLanjut = $("#tindakLanjutInputOpportunity").val();
                model.SupportPemerintah = $("#dukunganPemerintahInputOpportunity").val();
                model.KementrianLembagaIds = $("#keterlibatanLembagaInputOpportunity").val();

                model.PotensiEskalasi = $("#potensialEskalasiInputOpportunity").val();
                model.CatatanTambahan = $("#catatanTambahanInputOpportunity").val();
                model.RelatedAgreementId = $("#relasiAgreementInputOpportunity").val();
                model.KementrianLembaga = $("#keterlibatanLembagaInput").val();

                $.ajax({
                    type: "POST",
                    url: urlSubmit,
                    data: model,
                    success: function (data) {
                        if (data.IsError) {
                            draftOpportunityButton.removeAttribute('data-kt-indicator');
                            draftOpportunityButton.disabled = false;
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
                            draftOpportunityButton.removeAttribute('data-kt-indicator');
                            draftOpportunityButton.disabled = false;
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

    const draftInitiativeButton = document.getElementById('SaveDraftInitiative');
    draftInitiativeButton.addEventListener('click', function (e) {
        e.preventDefault();
        var callBackUrlPartial = $(this).data("callbackurl");
        Swal.fire({
            text: "Are you sure you want to save this data as a Draft?",
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
                draftInitiativeButton.setAttribute('data-kt-indicator', 'on');
                draftInitiativeButton.disabled = true;

                var urlSubmit = draftInitiativeButton.dataset.url;
                var model = {
                    AgreementPartners: [],
                    AgreementLokasiProyeks: []
                };
                model.JudulPerjanjian = $("#judulPerjanjianInputInitiative").val();
                model.JenisPerjanjianId = $("#jenisPerjanjianInputInitiative").val();
                model.EntitasPertaminaEvent = $("#hshEntitasPertaminaInputInitiative").val();
                model.FungsiPicId = $("#fungsiPicInputInitiative").val();
                $('.partnerInitiative').each(function () {

                    var partnerName = $(this).find('#partnerInputInitiative').val();
                    var partner = {
                        PartnerName: partnerName,
                    };
                    model.AgreementPartners.push(partner);
                });
                $('.lokasiProyekInitiative').each(function () {
                    var lokasiProyek = $(this).find('#lokasiProyekInputInitiative').val();

                    var lokasi = {
                        LokasiProyek: lokasiProyek
                    };
                    model.AgreementLokasiProyeks.push(lokasi);
                });
                model.NegaraMitraId = $("#negaraMitraInputInitiative").val();
                model.NilaiProyek = $("#nilaiProyekInputInitiative").val();
                model.TanggalTtd = $("#rangeTanggalTtdInputInitiative").val();
                model.TanggalBerakhir = $("#rangeTanggalBerakhirInputInitiative").val();
                model.StatusBerlakuId = $("#statusBerlakuValueInputInitiative").val();
                model.DiscussionStatusId = $("#statusDiskusiInputInitiative").val();
                model.Scope = $("#scopeProjectInputInitiative").val();
                model.StreamBusinessIds = $("#streamBusinessInputInitiative").val();
                model.Progress = $("#progressInputInitiative").val();
                model.FaktorKendalaId = $("#faktorKendalaInputInitiative").val();
                model.KlasifikasiKendalaId = $("#klasifikasiKendalaInputInitiative").val();
                model.DeskripsiKendala = $("#deskripsiKendalaInputInitiative").val();
                model.TindakLanjut = $("#tindakLanjutInputInitiative").val();
                model.SupportPemerintah = $("#dukunganPemerintahInputInitiative").val();
                model.KementrianLembagaIds = $("#keterlibatanLembagaInputInitiative").val();

                model.PotensiEskalasi = $("#potensialEskalasiInputInitiative").val();
                model.CatatanTambahan = $("#catatanTambahanInputInitiative").val();
                model.RelatedAgreementId = $("#relasiAgreementInputInitiative").val();
                model.KementrianLembaga = $("#keterlibatanLembagaInput").val();

                $.ajax({
                    type: "POST",
                    url: urlSubmit,
                    data: model,
                    success: function (data) {
                        if (data.IsError) {
                            draftInitiativeButton.removeAttribute('data-kt-indicator');
                            draftInitiativeButton.disabled = false;
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
                            draftInitiativeButton.removeAttribute('data-kt-indicator');
                            draftInitiativeButton.disabled = false;
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

    const opportunityForm = document.getElementById('AddOpportunity');
    var opportunityValidator = FormValidation.formValidation(
        opportunityForm,
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
                'FungsiPicId': {
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
        }
    );

    const submitOpportunityButton = document.getElementById('OpportunitySubmit');
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
                                AgreementPartners: [],
                                AgreementLokasiProyeks: []
                            };
                            model.JudulPerjanjian = $("#judulPerjanjianInputOpportunity").val();
                            model.JenisPerjanjianId = $("#jenisPerjanjianInputOpportunity").val();
                            model.EntitasPertaminaEvent = $("#hshEntitasPertaminaInputOpportunity").val();
                            model.FungsiPicId = $("#fungsiPicInputOpportunity").val(); 
                            $('.partnerOpportunity').each(function () {

                                var partnerName = $(this).find('#partnerInputOpportunity').val();
                                var partner = {
                                    PartnerName: partnerName
                                };
                                model.AgreementPartners.push(partner);
                            });
                            $('.lokasiProyekOpportunity').each(function () {
                                var lokasiProyek = $(this).find('#lokasiProyekInputOpportunity').val();

                                var lokasi = {
                                    LokasiProyek: lokasiProyek
                                };
                                model.AgreementLokasiProyeks.push(lokasi);
                            });
                            model.NegaraMitraId = $("#negaraMitraInputOpportunity").val();
                            model.NilaiProyek = $("#nilaiProyekInputOpportunity").val();
                            model.TanggalTtd = $("#rangeTanggalTtdInputOpportunity").val();
                            model.TanggalBerakhir = $("#rangeTanggalBerakhirInputOpportunity").val();
                            model.StatusBerlakuId = $("#statusBerlakuValueInputOpportunity").val();
                            model.DiscussionStatusId = $("#statusDiskusiInputOpportunity").val();
                            model.Scope = $("#scopeProjectInputOpportunity").val();
                            model.StreamBusinessIds = $("#streamBusinessInputOpportunity").val();
                            model.Progress = $("#progressInputOpportunity").val();
                            model.FaktorKendalaId = $("#faktorKendalaInputOpportunity").val();
                            model.KlasifikasiKendalaId = $("#klasifikasiKendalaInputOpportunity").val();
                            model.DeskripsiKendala = $("#deskripsiKendalaInputOpportunity").val();
                            model.TindakLanjut = $("#tindakLanjutInputOpportunity").val();
                            model.SupportPemerintah = $("#dukunganPemerintahInputOpportunity").val();
                            model.KementrianLembagaIds = $("#keterlibatanLembagaInputOpportunity").val();
                            model.PotensiEskalasi = $("#potensialEskalasiInputOpportunity").val();
                            model.CatatanTambahan = $("#catatanTambahanInputOpportunity").val();
                            model.RelatedAgreementId = $("#relasiAgreementInputOpportunity").val();

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

    const initiativeForm = document.getElementById('AddInitiative');
    var initiativeValidator = FormValidation.formValidation(
        initiativeForm,
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
                'FungsiPicId': {
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
        }
    );

    const submitInitiativeButton = document.getElementById('InitiativeSubmit');
    submitInitiativeButton.addEventListener('click', function (e) {
        e.preventDefault();
        var callBackUrlPartial = $(this).data("callbackurl");
        if (initiativeValidator) {
            initiativeValidator.validate().then(function (status) {
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
                            submitInitiativeButton.setAttribute('data-kt-indicator', 'on');
                            submitInitiativeButton.disabled = true;

                            var urlSubmit = submitInitiativeButton.dataset.url;

                            var model = {
                                AgreementPartners: [],
                                AgreementLokasiProyeks: []
                            };
                            model.JudulPerjanjian = $("#judulPerjanjianInputInitiative").val();
                            model.JenisPerjanjianId = $("#jenisPerjanjianInputInitiative").val();
                            model.EntitasPertaminaEvent = $("#hshEntitasPertaminaInputInitiative").val();
                            model.FungsiPicId = $("#fungsiPicInputInitiative").val();
                            $('.partnerInitiative').each(function () {

                                var partnerName = $(this).find('#partnerInputInitiative').val();
                                var partner = {
                                    PartnerName: partnerName,
                                };
                                model.AgreementPartners.push(partner);
                            });
                            $('.lokasiProyekInitiative').each(function () {
                                var lokasiProyek = $(this).find('#lokasiProyekInputInitiative').val();

                                var lokasi = {
                                    LokasiProyek: lokasiProyek
                                };
                                model.AgreementLokasiProyeks.push(lokasi);
                            });
                            model.NegaraMitraId = $("#negaraMitraInputInitiative").val();
                            model.NilaiProyek = $("#nilaiProyekInputInitiative").val();
                            model.TanggalTtd = $("#rangeTanggalTtdInputInitiative").val();
                            model.TanggalBerakhir = $("#rangeTanggalBerakhirInputInitiative").val();
                            model.StatusBerlakuId = $("#statusBerlakuValueInputInitiative").val();
                            model.DiscussionStatusId = $("#statusDiskusiInputInitiative").val();
                            model.Scope = $("#scopeProjectInputInitiative").val();
                            model.StreamBusinessIds = $("#streamBusinessInputInitiative").val();
                            model.Progress = $("#progressInputInitiative").val();
                            model.FaktorKendalaId = $("#faktorKendalaInputInitiative").val();
                            model.KlasifikasiKendalaId = $("#klasifikasiKendalaInputInitiative").val();
                            model.DeskripsiKendala = $("#deskripsiKendalaInputInitiative").val();
                            model.TindakLanjut = $("#tindakLanjutInputInitiative").val();
                            model.SupportPemerintah = $("#dukunganPemerintahInputInitiative").val();
                            model.PotensiEskalasi = $("#potensialEskalasiInputInitiative").val();
                            model.CatatanTambahan = $("#catatanTambahanInputInitiative").val();
                            model.RelatedAgreementId = $("#relasiAgreementInputInitiative").val();
                            model.KementrianLembagaIds = $("#keterlibatanLembagaInputInitiative").val();


                            $.ajax({
                                type: "POST",
                                url: urlSubmit,
                                data: model,
                                success: function (data) {
                                    if (data.IsError) {
                                        submitInitiativeButton.removeAttribute('data-kt-indicator');
                                        submitInitiativeButton.disabled = false;
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
                                        submitInitiativeButton.removeAttribute('data-kt-indicator');
                                        submitInitiativeButton.disabled = false;
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
    $('#addRepeaterLokasiProyek, #addRepeaterLokasiProyekOpportunity, #addRepeaterLokasiProyekInitiative').repeater({
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
    }).find('[data-repeater-delete]').prop('disabled', true);

    isFirstRepeater = true;
    $('#addRepeaterPartner, #addRepeaterPartnerOpportunity, #addRepeaterPartnerInitiative').repeater({
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
    }).find('[data-repeater-delete]').prop('disabled', true);

})
$(function () {
    $(".datepicker-tanggal").flatpickr({
        autoclose: true,
        format: 'm-d-Y',
        todayHighlight: true,
    });

    $("#jenisPerjanjianInput, #jenisPerjanjianInputOpportunity, #jenisPerjanjianInputInitiative").select2({
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
        },

    });
    $("#hshEntitasPertaminaInput, #hshEntitasPertaminaInputOpportunity, #hshEntitasPertaminaInputInitiative").select2({
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
    $("#fungsiPicInput, #fungsiPicInputOpportunity, #fungsiPicInputInitiative").select2({
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

    $('#trafficLightInput').select2({
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
            return $(
                `<i style="color:${item.color}" class="fa-solid fa-circle"></i> <span>${item.text}</span>  + `
            );
        }
    });

    //if (!valuePicInput) {
    //    $('#trafficLightInput').val(null).trigger('change');
    //}
    //else {
    //    ajaxPreselecting(url.getDdlPicFungsi + "?guid=" + valuePicInput, $('#picInput'));
    //}

    $("#negaraMitraInput, #negaraMitraInputOpportunity, #negaraMitraInputInitiative").select2({
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
    $("#statusDiskusiInput, #statusDiskusiInputOpportunity, #statusDiskusiInputInitiative").select2({
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
    $("#streamBusinessInput, #streamBusinessInputOpportunity, #streamBusinessInputInitiative").select2({
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
    $("#faktorKendalaInput, #faktorKendalaInputOpportunity, #faktorKendalaInputInitiative").select2({
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
    $("#klasifikasiKendalaInput, #klasifikasiKendalaInputOpportunity, #klasifikasiKendalaInputInitiative").select2({
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
    $("#keterlibatanLembagaInput, #keterlibatanLembagaInputOpportunity, #keterlibatanLembagaInputInitiative").select2({
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
    $("#relasiAgreementInput, #relasiAgreementInputOpportunity, #relasiAgreementInputInitiative").select2({
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
    $("#fungsiPicInput").select2({
        language: {
            inputTooShort: function (args) {
                return "Ketik minimal 3 karakter untuk pencarian partner atau judul perjanjian";
            }
        },
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

        },
        minimumInputLength: 3
    });
});
$(function () {
    $("#rangeTanggalTtdInput").flatpickr({
        dateFormat: "m-d-Y",
        onChange: function (selectedDates) {
            if (selectedDates[0]) {
                $("#rangeTanggalBerakhirInput").prop("readonly", false);
                $("#rangeTanggalBerakhirInput").val("").trigger("change");
                $("#statusBerlakuInput").val("").trigger("change");
                var minDate = selectedDates[0];
                $("#rangeTanggalBerakhirInput").flatpickr({
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
                                    $("#statusBerlakuInput").val(data.items.StatusName);
                                    $("#statusBerlakuValueInput").val(data.items.Id);
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
    $("#statusBerlakuInput").prop("readonly", true);

    $("#rangeTanggalTtdInputOpportunity").flatpickr({
        dateFormat: "m-d-Y",
        onChange: function (selectedDates) {
            if (selectedDates[0]) {
                $("#rangeTanggalBerakhirInputOpportunity").prop("readonly", false);
                $("#rangeTanggalBerakhirInputOpportunity").val("").trigger("change");
                $("#statusBerlakuInputOpportunity").val("").trigger("change");
                var minDate = selectedDates[0];
                $("#rangeTanggalBerakhirInputOpportunity").flatpickr({
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
                                    $("#statusBerlakuInputOpportunity").val(data.items.StatusName);
                                    $("#statusBerlakuValueInputOpportunity").val(data.items.Id);
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
    $("#statusBerlakuInputOpportunity").prop("readonly", true);

    $("#rangeTanggalTtdInputInitiative").flatpickr({
        dateFormat: "m-d-Y",
        onChange: function (selectedDates) {
            if (selectedDates[0]) {
                $("#rangeTanggalBerakhirInputInitiative").prop("readonly", false);
                $("#rangeTanggalBerakhirInputInitiative").val("").trigger("change");
                $("#statusBerlakuInputInitiative").val("").trigger("change");
                var minDate = selectedDates[0];
                $("#rangeTanggalBerakhirInputInitiative").flatpickr({
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
                                    $("#statusBerlakuInputInitiative").val(data.items.StatusName);
                                    $("#statusBerlakuValueInputInitiative").val(data.items.Id);
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
    $("#statusBerlakuInputInitiative").prop("readonly", true);



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
            })
                .on('change', function () {
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
});
$(document).ready(function () {
    $("#rangeTanggalBerakhirInputInitiative").prop("readonly", true);
    $("#rangeTanggalBerakhirInput").prop("readonly", true);
    $("#rangeTanggalBerakhirInputOpportunity").prop("readonly", true);

    $("#CreateBlankAgreement").hide();
    $("#CreateOpportunityAgreement").hide();
    $("#CreateInitiativeAgreement").hide();
    $("#ddlJudulInitiative").hide();
    $('#judulInputInitiative').attr("disabled", "disabled");
    $('#judulInput').select2().val("").trigger('change');
    $('#judulInput').attr("disabled", "disabled");
    $('#judulInput').select2().val("").trigger('change');
    $("#sumberPengisianInput").select2({
        ajax: {
            url: url.ddlAgreementForm,
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
        var value = e.target.value;
        $('#judulInput').select2().val("").trigger('change');
        $('#judulInputInitiative').select2().val("").trigger('change');
        $("#CreateBlankAgreement").hide();
        $("#CreateOpportunityAgreement").hide();
        $("#CreateInitiativeAgreement").hide();
        $('#btnCancel').show();

        if (value === "2") {
            $('#ddlJudulOpportunity').show();
            $('#ddlJudulInitiative').hide();
            $('#judulInput').removeAttr("disabled");
            $('#judulInput').select2().val("").trigger('change');
            $("#CreateOpportunityAgreement").hide();
            $("#CreateBlankAgreement").hide();
            $("#CreateInitiativeAgreement").hide();
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

                var opportunityGuid = e.target.value;
                $('#judulInput').removeAttr("disabled");
                $("#CreateBlankAgreement").hide();
                $("#CreateOpportunityAgreement").show();
                $("#CreateInitiativeAgreement").hide();
                $('#btnCancel').hide();

                $("#judulPerjanjianInputOpportunity").val("").trigger("change");
                $("#jenisPerjanjianInputOpportunity").val("").trigger("change");
                $("#hshEntitasPertaminaInputOpportunity").val("").trigger("change");
                $("#fungsiPicInputOpportunity").val("").trigger("change");
                $("#partnerInputOpportunity").val("").trigger("change");
                $("#negaraMitraInputOpportunity").val("").trigger("change");
                $("#nilaiProyekInputOpportunity").val("").trigger("change");
                $("#rangeTanggalTtdInputOpportunity").val("").trigger("change");
                $("#rangeTanggalBerakhirInputOpportunity").val("").trigger("change");
                $("#statusBerlakuInputOpportunity").val("").trigger("change");
                $("#statusDiskusiInputOpportunity").val("").trigger("change");
                $("#lokasiProyekInputOpportunity").val("").trigger("change");
                $("#scopeProjectInputOpportunity").val("").trigger("change");
                $("#streamBusinessInputOpportunity").val("").trigger("change");
                $("#progressInputOpportunity").val("").trigger("change");
                $("#faktorKendalaInputOpportunity").val("").trigger("change");
                $("#klasifikasiKendalaInputOpportunity").val("").trigger("change");
                $("#deskripsiKendalaInputOpportunity").val("").trigger("change");
                $("#tindakLanjutInputOpportunity").val("").trigger("change");
                $("#dukunganPemerintahInputOpportunity").val("").trigger("change");
                $("#keterlibatanLembagaInputOpportunity").val("").trigger("change");
                $("#potensialEskalasiInputOpportunity").val("").trigger("change");
                $("#catatanTambahanInputOpportunity").val("").trigger("change");
                $("#relasiAgreementInputOpportunity").val("").trigger("change");

                if (opportunityGuid) {
                    $.ajax({
                        url: url.getDdlOpportunityEntitasPertamina + "?guid=" + opportunityGuid,
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
                        url: url.getDdlOpportunityPicFungsi + "?guid=" + opportunityGuid,
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
                        url: url.getDdlOpportunityNegara + "?guid=" + opportunityGuid,
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
                        data: { guid: opportunityGuid },
                        success: function (result) {
                            var data = result.items;
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
                        data: { guid: opportunityGuid },
                        success: function (result) {
                            var data = result.items;
                            var partnerList = data.OpPartners;
                            $("#addRepeaterPartnerOpportunity .partnerOpportunity").remove();
                            partnerList.forEach(function (partner) {
                                var $repeaterItem = $("<div>").addClass("partnerOpportunity mb-2");
                                var $formGroup = $("<div>").addClass("fv-row form-group row");
                                var $colMd10 = $("<div>").addClass("col-md-10");
                                var $partnerInput = $("<input>").attr({
                                    type: "text",
                                    id: "partnerInputOpportunity",
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

                                $("#addRepeaterPartnerOpportunity [data-repeater-list='PartnerName']").append($repeaterItem);
                            });
                            $('#addRepeaterPartnerOpportunity').repeater({
                                initEmpty: false,

                                show: function () {
                                    $(this).slideDown();
                                },

                                hide: function (deleteElement) {
                                    $(this).slideUp(deleteElement);
                                }
                            });
                            $('#addRepeaterPartnerOpportunity').on('click', '[data-repeater-delete]', function () {
                                $(this).closest('.partnerOpportunity').slideUp(function () {
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
                        data: { guid: opportunityGuid },
                        success: function (result) {
                            var data = result.items;
                            var lokasiProyekList = data.OpLokasiProyeks;
                            $("#addRepeaterLokasiProyekOpportunity .lokasiProyekOpportunity").remove();
                            lokasiProyekList.forEach(function (lokasiProyek) {
                                var $repeaterItem = $("<div>").addClass("lokasiProyekOpportunity mb-2");
                                var $formGroup = $("<div>").addClass("fv-row form-group row");
                                var $colMd10 = $("<div>").addClass("col-md-10");
                                var $lokasiProyekInput = $("<input>").attr({
                                    type: "text",
                                    id: "lokasiProyekInputOpportunity",
                                    name: "LokasiProyek",
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
                            });
                            $('#addRepeaterLokasiProyekOpportunity').repeater({
                                initEmpty: false,

                                show: function () {
                                    $(this).slideDown();
                                },

                                hide: function (deleteElement) {
                                    $(this).slideUp(deleteElement);
                                }
                            });
                            $('#addRepeaterLokasiProyekOpportunity').on('click', '[data-repeater-delete]', function () {
                                $(this).closest('.lokasiProyekOpportunity').slideUp(function () {
                                    $(this).remove();
                                });
                            });
                        },
                        error: function (error) {
                            console.log(error);
                        }
                    });
                    $.ajax({
                        url: url.getDdlOpportunityStreamBusiness + "?guid=" + opportunityGuid,
                        method: 'GET',
                        success: function (data) {
                            if (data.items) {
                                var options = [];
                                $.each(data.items, function (index, item) {
                                    options.push(new Option(item.text, item.id, true, true));
                                });
                                console.log(data.items)
                                $('#streamBusinessInputOpportunity').empty().append(options).trigger('change');
                            }
                        }
                    });

                }
                else {
                    $("#CreateBlankAgreement").hide();
                    $("#CreateOpportunityAgreement").hide();
                    $("#CreateInitiativeAgreement").hide();
                    $('#btnCancel').show();
                    $("#judulPerjanjianInputOpportunity").val("").trigger("change");
                    $("#jenisPerjanjianInputOpportunity").val("").trigger("change");
                    $("#hshEntitasPertaminaInputOpportunity").val("").trigger("change");
                    $("#fungsiPicInputOpportunity").val("").trigger("change");
                    $("#initiativeStageInputOpportunity").val("").trigger("change");
                    $("#fungsiPicInputOpportunity").val("").trigger("change");
                    $("#partnerInputOpportunity").val("").trigger("change");
                    $("#negaraMitraInputOpportunity").val("").trigger("change");
                    $("#nilaiProyekInputOpportunity").val("").trigger("change");
                    $("#rangeTanggalTtdInputOpportunity").val("").trigger("change");
                    $("#rangeTanggalBerakhirInputOpportunity").val("").trigger("change");
                    $("#statusBerlakuInputOpportunity").val("").trigger("change");
                    $("#statusDiskusiInputOpportunity").val("").trigger("change");
                    $("#lokasiProyekInputOpportunity").val("").trigger("change");
                    $("#scopeProjectInputOpportunity").val("").trigger("change");
                    $("#streamBusinessInputOpportunity").val("").trigger("change");
                    $("#progressInputOpportunity").val("").trigger("change");
                    $("#faktorKendalaInputOpportunity").val("").trigger("change");
                    $("#klasifikasiKendalaInputOpportunity").val("").trigger("change");
                    $("#deskripsiKendalaInputOpportunity").val("").trigger("change");
                    $("#tindakLanjutInputOpportunity").val("").trigger("change");
                    $("#dukunganPemerintahInputOpportunity").val("").trigger("change");
                    $("#keterlibatanLembagaInputOpportunity").val("").trigger("change");
                    $("#catatanTambahanInputOpportunity").val("").trigger("change");
                    $("#relasiAgreementInputOpportunity").val("").trigger("change");

                }
            });
        }
        else if (value === "3") {
            $('#judulInputInitiative').attr("disabled", "disabled");
            $('#judulInput').attr("disabled", "disabled");
            $("#CreateBlankAgreement").show();
            $("#CreateOpportunityAgreement").hide();
            $("#CreateInitiativeAgreement").hide();
            $('#btnCancel').hide();
        }
        else if (value === "1") {
            $('#ddlJudulOpportunity').hide();
            $('#ddlJudulInitiative').show();
            $('#judulInputInitiative').removeAttr("disabled");
            $('#judulInputInitiative').select2().val("").trigger('change');
            $("#CreateOpportunityAgreement").hide();
            $("#CreateBlankAgreement").hide();
            $("#CreateInitiativeAgreement").hide();
            $('#btnCancel').show();
            $("#judulInputInitiative").select2({
                language: {
                    inputTooShort: function (args) {
                        return "Ketik minimal 3 karakter untuk pencarian judul";
                    }
                },
                ajax: {
                    url: url.ddlNamaProyekInitiative,
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
                var initiativeGuid = e.target.value;
                $("#CreateBlankAgreement").hide();
                $("#CreateOpportunityAgreement").hide();
                $("#CreateInitiativeAgreement").show();
                $('#btnCancel').hide();

                $("#judulPerjanjianInputInitiative").val("").trigger("change");
                $("#jenisPerjanjianInputInitiative").val("").trigger("change");
                $("#hshEntitasPertaminaInputInitiative").val("").trigger("change");
                $("#fungsiPicInputInitiative").val("").trigger("change");
                $("#partnerInputInitiative").val("").trigger("change");
                $("#negaraMitraInputInitiative").val("").trigger("change");
                $("#nilaiProyekInputInitiative").val("").trigger("change");
                $("#rangeTanggalTtdInputInitiative").val("").trigger("change");
                $("#rangeTanggalBerakhirInputInitiative").val("").trigger("change");
                $("#statusBerlakuInputInitiative").val("").trigger("change");
                $("#statusDiskusiInputInitiative").val("").trigger("change");
                $("#lokasiProyekInputInitiative").val("").trigger("change");
                $("#scopeProjectInputInitiative").val("").trigger("change");
                $("#streamBusinessInputInitiative").val("").trigger("change");
                $("#progressInputInitiative").val("").trigger("change");
                $("#faktorKendalaInputInitiative").val("").trigger("change");
                $("#klasifikasiKendalaInputInitiative").val("").trigger("change");
                $("#deskripsiKendalaInputInitiative").val("").trigger("change");
                $("#tindakLanjutInputInitiative").val("").trigger("change");
                $("#dukunganPemerintahInputInitiative").val("").trigger("change");
                $("#keterlibatanLembagaInputInitiative").val("").trigger("change");
                $("#potensialEskalasiInputInitiative").val("").trigger("change");
                $("#catatanTambahanInputInitiative").val("").trigger("change");
                $("#relasiAgreementInputInitiative").val("").trigger("change");

                if (initiativeGuid) {
                    $.ajax({
                        url: url.getDdlInitiativeEntitasPertamina + "?guid=" + initiativeGuid,
                        method: 'GET',
                        success: function (data) {
                            if (data.items) {
                                var options = [];
                                $.each(data.items, function (index, item) {
                                    options.push(new Option(item.text, item.id, true, true));
                                });
                                $('#hshEntitasPertaminaInputInitiative').empty().append(options).trigger('change');
                            }
                        }
                    });
                    $.ajax({
                        url: url.getDdlInitiativePicFungsi + "?guid=" + initiativeGuid,
                        method: 'GET',
                        success: function (data) {
                            if (data.items) {
                                var options = [];
                                $.each(data.items, function (index, item) {
                                    options.push(new Option(item.text, item.id, true, true));
                                });
                                $('#fungsiPicInputInitiative').empty().append(options).trigger('change');
                            }
                        }
                    });
                    $.ajax({
                        url: url.getDdlInitiativeNegara + "?guid=" + initiativeGuid,
                        method: 'GET',
                        success: function (data) {
                            if (data.items) {
                                var options = [];
                                $.each(data.items, function (index, item) {
                                    options.push(new Option(item.text, item.id, true, true));
                                });
                                $('#negaraMitraInputInitiative').empty().append(options).trigger('change');
                            }
                        }
                    });
                    $.ajax({
                        url: url.getDdlInitiativeStreamBusiness + "?guid=" + initiativeGuid,
                        method: 'GET',
                        success: function (data) {
                            if (data.items) {
                                var options = [];
                                $.each(data.items, function (index, item) {
                                    options.push(new Option(item.text, item.id, true, true));
                                });
                                $('#streamBusinessInputInitiative').empty().append(options).trigger('change');
                            }
                        }
                    });
                    $.ajax({
                        url: param.getInitiativeById,
                        method: 'GET',
                        data: { guid: initiativeGuid },
                        success: function (result) {
                            var data = result.items;
                            $("#nilaiProyekInputInitiative").val(data.NilaiProyek);
                            $("#scopeProjectInputInitiative").val(data.Scope);
                            $("#tindakLanjutInputInitiative").val(data.TindakLanjut);
                            $("#progressInputInitiative").val(data.Progress);
                            $("#catatanTambahanInputInitiative").val(data.CatatanTambahan);
                            $("#dukunganPemerintahInputInitiative").val(data.SupportPemerintah);
                        },
                        error: function (error) {
                            console.log(error);
                        }
                    });
                    $.ajax({
                        url: param.getInitiativeById,
                        method: 'GET',
                        data: { guid: initiativeGuid },
                        success: function (result) {
                            var data = result.items;
                            var partnerList = data.InitiativePartners;
                            $("#addRepeaterPartnerInitiative .partnerInitiative").remove();
                            partnerList.forEach(function (partner) {
                                var $repeaterItem = $("<div>").addClass("partnerInitiative mb-2");
                                var $formGroup = $("<div>").addClass("fv-row form-group row");
                                var $colMd10 = $("<div>").addClass("col-md-10");
                                var $partnerInput = $("<input>").attr({
                                    type: "text",
                                    id: "partnerInputInitiative",
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

                                $("#addRepeaterPartnerInitiative [data-repeater-list='PartnerName']").append($repeaterItem);
                            });
                            $('#addRepeaterPartnerInitiative').repeater({
                                initEmpty: false,

                                show: function () {
                                    $(this).slideDown();

                                },

                                hide: function (deleteElement) {
                                    $(this).slideUp(deleteElement);
                                }
                            });
                            $('#addRepeaterPartnerInitiative').on('click', '[data-repeater-delete]', function () {
                                $(this).closest('.partnerInitiative').slideUp(function () {
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
                        data: { guid: initiativeGuid },
                        success: function (result) {
                            var data = result.items;
                            var lokasiProyekList = data.InitiativeLokasiProyeks;
                            $("#addRepeaterLokasiProyekInitiative .lokasiProyekInitiative").remove();
                            lokasiProyekList.forEach(function (lokasiProyek) {
                                var $repeaterItem = $("<div>").addClass("lokasiProyekInitiative mb-2");
                                var $formGroup = $("<div>").addClass("fv-row form-group row");
                                var $colMd10 = $("<div>").addClass("col-md-10");
                                var $lokasiProyekInput = $("<input>").attr({
                                    type: "text",
                                    id: "lokasiProyekInputInitiative",
                                    name: "LokasiProyek",
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

                                $("#addRepeaterLokasiProyekInitiative [data-repeater-list='LokasiProyek']").append($repeaterItem);
                            });

                            var isFirstRepeater = true;
                            $('#addRepeaterLokasiProyekInitiative').repeater({

                                show: function () {
                                    $(this).slideDown();
                                },

                                hide: function (deleteElement) {
                                    $(this).slideUp(deleteElement);
                                }
                            });
                            $('#addRepeaterLokasiProyekInitiative').on('click', '[data-repeater-delete]', function () {
                                $(this).closest('.lokasiProyekInitiative').slideUp(function () {
                                    $(this).remove();
                                });
                            });
                        },
                        error: function (error) {
                            console.log(error);
                        }
                    });


                }
                else {
                    $("#CreateBlankAgreement").hide();
                    $("#CreateOpportunityAgreement").hide();
                    $("#CreateInitiativeAgreement").hide();
                    $('#btnCancel').show();
                    $("#judulPerjanjianInputInitiative").val("").trigger("change");
                    $("#jenisPerjanjianInputInitiative").val("").trigger("change");
                    $("#hshEntitasPertaminaInputInitiative").val("").trigger("change");
                    $("#fungsiPicInputInitiative").val("").trigger("change");
                    $("#initiativeStageInputInitiative").val("").trigger("change");
                    $("#fungsiPicInputInitiative").val("").trigger("change");
                    $("#partnerInputInitiative").val("").trigger("change");
                    $("#negaraMitraInputInitiative").val("").trigger("change");
                    $("#nilaiProyekInputInitiative").val("").trigger("change");
                    $("#rangeTanggalTtdInputInitiative").val("").trigger("change");
                    $("#rangeTanggalBerakhirInputInitiative").val("").trigger("change");
                    $("#statusBerlakuInputInitiative").val("").trigger("change");
                    $("#statusDiskusiInputInitiative").val("").trigger("change");
                    $("#lokasiProyekInputInitiative").val("").trigger("change");
                    $("#scopeProjectInputInitiative").val("").trigger("change");
                    $("#streamBusinessInputInitiative").val("").trigger("change");
                    $("#progressInputInitiative").val("").trigger("change");
                    $("#faktorKendalaInputInitiative").val("").trigger("change");
                    $("#klasifikasiKendalaInputInitiative").val("").trigger("change");
                    $("#deskripsiKendalaInputInitiative").val("").trigger("change");
                    $("#tindakLanjutInputInitiative").val("").trigger("change");
                    $("#dukunganPemerintahInputInitiative").val("").trigger("change");
                    $("#keterlibatanLembagaInputInitiative").val("").trigger("change");
                    $("#catatanTambahanInputInitiative").val("").trigger("change");
                    $("#relasiAgreementInputInitiative").val("").trigger("change");
                }
            });
        }
        else {
            $('#judulInputInitiative').attr("disabled", "disabled");
            $('#judulInput').attr("disabled", "disabled");
            $("#CreateBlankAgreement").hide();
            $("#CreateOpportunityAgreement").hide();
            $("#CreateInitiativeAgreement").hide();
            $('#btnCancel').show();
        }

    });
});



document.addEventListener('DOMContentLoaded', function () {
    const inputPotentialRevenue = document.getElementById('transactionValueInput');
    const inputProjectCost = document.getElementById('projectCostInput');
    const inputNilaiProyek= document.getElementById('nilaiProyekInput');

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


