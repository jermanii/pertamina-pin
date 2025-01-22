 document.addEventListener("DOMContentLoaded", function () { 
     $('#kt_modal_export').on('show.bs.modal', function (event) {
         var chk = document.getElementById("chkPPT");
         chk.click(); 
     });
});


/*Function Repeater*/
var isFirstRepeater = true;
$('#addRepeaterContent').repeater({
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
}).find('[data-repeater-delete]').css('display', 'none');
 

const exportButton = document.getElementById('btnExport');
exportButton.addEventListener('click', function (e) {  
    var model = {
        exportType: "",
        template: "",
        freeTextTemplate2:[]
    };
    var ErrorMessage = validateExport(model);
    if (ErrorMessage == "") { 
        if (model.exportType == "EXCEL") {
            Swal.fire({
                text: "Export been successfully!",
                icon: "success",
                buttonsStyling: false,
                confirmButtonText: "Ok, got it!",
                customClass: {
                    confirmButton: "btn btn-primary",

                }
            });
        }
        else {
            downloadPpt(model);
        } 
    }
    else {
        $("#validationExport").html(ErrorMessage);
    } 

}); 

function downloadPpt(model) {
    $.ajax({
        url: '/api/ApiDownloadPresentation/ExportPresentation',
        type: 'POST',
        data: JSON.stringify(model), // Serialize model to JSON
        contentType: 'application/json', // Inform the server that the data is JSON
        xhrFields: {
            responseType: 'blob' // Important for binary data
        },
        success: function (data) {
            const pptName = model.template == "1" ? "template1.pptx" : "template2.pptx";
            const url = window.URL.createObjectURL(new Blob([data]));
            const a = document.createElement('a');
            a.href = url;
            a.download = pptName;
            document.body.appendChild(a);
            a.click();
            document.body.removeChild(a);

            Swal.fire({
                text: "Export been successfully!",
                icon: "success",
                buttonsStyling: false,
                confirmButtonText: "Ok, got it!",
                customClass: {
                    confirmButton: "btn btn-primary",

                }
            })
        },
        error: function (xhr, status, error) {
            console.error('XHR:', xhr);
            console.error('Status:', status);
            console.error('Error:', error);
        }
    });

}

function validateExport(model) {
    var errorMsg = "";
    const chkExport = document.querySelectorAll('input[name="export"]');
    const isChkExportChecked = Array.from(chkExport).some((checkbox) => checkbox.checked);
    if (!isChkExportChecked) {
        errorMsg += "&#x2022; &nbsp; Silahkan pilih jenis export";
    }
    else {
        // Get the values of all selected checkboxes
        const selectedExport = Array.from(chkExport)
            .filter(checkbox => checkbox.checked) // Filter only checked checkboxes
            .map(checkbox => checkbox.value);    // Map to their values

        model.exportType = selectedExport[0];

        if (selectedExport[0] == "EXCEL") { 
            try {

            const statusBerlaku = $("#_ddlStatusBerlaku").val();
            const statusDiscussion = $("#_ddlStatusDiscussion").val();
            const agreementHolder = $("#_ddlAgreementHolder").val();
            const entitasPertamina = $("#_ddlEntitasPertamina").val();
            const kawasanMitra = $("#_ddlKawasanMitra").val();
            const negaraMitra = $("#_ddlNegaraMitra").val();
            const jenisPerjanjian = $("#_ddlJenisPerjanjian").val();
            const streamBusiness = $("#_ddlStreamBusiness").val(); 
            var rangeCreateDate = $("#_rangeCreateDate").val();
            var tanggalTtd = $("#_rangeTanggalTtd").val();
            var tanggalBerakhir = $("#_rangeTanggalBerakhir").val();
            var isDraft = $('#checkBoxIsDraft').prop("checked");
            var urlExcel = exportButton.dataset.urlexcel;
            window.location.href = urlExcel + '?statusBerlaku=' + statusBerlaku +
                '&statusDiscussion=' + statusDiscussion +
                '&agreementHolder=' + agreementHolder +
                '&entitasPertamina=' + entitasPertamina +
                '&kawasanMitra=' + kawasanMitra +
                '&negaraMitra=' + negaraMitra +
                '&jenisPerjanjian=' + jenisPerjanjian +
                '&streamBusiness=' + streamBusiness +
                '&tanggalTtd=' + tanggalTtd +
                '&tanggalBerakhir=' + tanggalBerakhir +
                    '&rangeCreateDate=' + rangeCreateDate;   
            } catch (error) {
                errorMsg += "&#x2022; &nbsp; Export Excel Error: " + error.message;
            }

        } else {
            const chkTemplate = document.querySelectorAll('input[name="template"]');
            const isChkTemplate = Array.from(chkTemplate).some((checkbox) => checkbox.checked);
            if (!isChkTemplate) {
                errorMsg += "&#x2022; &nbsp; Anda belum memilih template PPT";
            }
            else {
                const selectedTemplate = Array.from(chkTemplate)
                    .filter(checkbox => checkbox.checked) // Filter only checked checkboxes
                    .map(checkbox => checkbox.value);    // Map to their values
                model.template = selectedTemplate[0];

                if (model.template == "1") {
                    model.freeTextTemplate1 = $("#contentInputLNG").val();
                }
                else {
                    model.titleTemplate2 = $("#title").val();
                    $('.descriptions').each(function () {

                        var freeTextDesc = $(this).find('#contentInput').val(); 
                        model.freeTextTemplate2.push(freeTextDesc);
                    });
                }
            }
        } 
    } 
    

    return errorMsg;
}

function ExportSelect(checkbox) { 
     
    // Deselect all checkboxes
    const chkExport = document.querySelectorAll('input[name="export"]');
    const isChkExportChecked = Array.from(chkExport).some((checkbox) => checkbox.checked);
    if (!isChkExportChecked) {
        $("#sectionTemplate").addClass('d-none');
        $(".template1").addClass('d-none');
        $(".template2").addClass('d-none');
    }
    else {
        chkExport.forEach((box) => {
            if (box !== checkbox) {
                box.checked = false;
            }
            else {
                $("#sectionTemplate").addClass('d-none');
                if (box.value == "PPT") {
                    $("#sectionTemplate").removeClass('d-none');
                    $("#sectionTemplateExcel").addClass('d-none');
                }
                else {
                    $("#sectionTemplateExcel").removeClass('d-none'); 
                    $("#sectionTemplate").addClass('d-none');
                    $(".template1").addClass('d-none');
                    $(".template2").addClass('d-none');
                }
            }
        });
    } 
    
}

function TemplateSelect(checkbox) {
     
    const chkTemplate = document.querySelectorAll('input[name="template"]');
    const isChkTemplate = Array.from(chkTemplate).some((checkbox) => checkbox.checked);
    if (!isChkTemplate) {
        $(".template1").addClass('d-none');
        $(".template2").addClass('d-none');
    }
    else {
        chkTemplate.forEach((box) => {
            if (box !== checkbox) {
                box.checked = false;
            }
            else { 
                
                if (box.value == "2") {
                    $(".template2").removeClass('d-none');
                    $(".template1").addClass('d-none');
                }
                else {
                    $(".template2").addClass('d-none');
                    $(".template1").removeClass('d-none');
                }
            }
        });
    }

}

// Date Range
$(function () {
    $("#_rangeTanggalTtd").daterangepicker({
        autoUpdateInput: false,
        drops: 'up',
        locale: {
            cancelLabel: 'Cancel'
        }
    });

    $("#_rangeTanggalBerakhir").daterangepicker({
        autoUpdateInput: false,
        drops: 'up',  
        locale: {
            cancelLabel: 'Cancel'
        }
    });

    $("#_rangeCreateDate").daterangepicker({
        autoUpdateInput: false,
        drops: 'up',
        locale: {
            cancelLabel: 'Cancel'
        }
    });

});


// Dropdown List
$(function () {

    $('#_ddlStatusBerlaku').select2({
        ajax: {
            url: '/api/ApiDropDownList/DdlStatusBerlaku',
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
        maximumSelectionLength: 5
    });
    $('#_ddlStatusDiscussion').select2({
        ajax: {
            url: '/api/ApiDropDownList/DdlStatusDiscussion',
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
        maximumSelectionLength: 5
    });
    $('#_ddlAgreementHolder').select2({
        ajax: {
            url: '/api/ApiDropDownList/DdlHsh',
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
        maximumSelectionLength: 5
    });
    $('#_ddlEntitasPertamina').select2({
        ajax: {
            url: '/api/ApiDropDownList/DdlEntitasPertaminaGrid',
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
        maximumSelectionLength: 5
    });
    $('#_ddlKawasanMitra').select2({
        ajax: {
            url: '/api/ApiDropDownList/DdlKawasanMitra',
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
        maximumSelectionLength: 5
    });
    $('#_ddlNegaraMitra').select2({
        ajax: {
            url: '/api/ApiDropDownList/DdlNegaraMitraWithoutKawasan',
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
        maximumSelectionLength: 5
    });
    $('#_ddlJenisPerjanjian').select2({
        ajax: {
            url: '/api/ApiDropDownList/DdlJenisPerjanjian',
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
        maximumSelectionLength: 5
    });
    $('#_ddlStreamBusiness').select2({
        ajax: {
            url: '/api/ApiDropDownList/DdlStreamBusiness',
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
        maximumSelectionLength: 5
    });
}); 

function resetAllFilter() {
    $('#_ddlStatusBerlaku').val(null).trigger('change'); // Clear the selected values
    $('#_ddlStatusDiscussion').val(null).trigger('change'); // Clear the selected values
    $('#_ddlAgreementHolder').val(null).trigger('change'); // Clear the selected values
    $('#_ddlEntitasPertamina').val(null).trigger('change'); // Clear the selected values
    $('#_ddlKawasanMitra').val(null).trigger('change'); // Clear the selected values
    $('#_ddlNegaraMitra').val(null).trigger('change'); // Clear the selected values
    $('#_ddlJenisPerjanjian').val(null).trigger('change'); // Clear the selected values
    $('#_ddlStreamBusiness').val(null).trigger('change'); // Clear the selected values     
}

//Event Tanggal TTD
$('input[name="RangeTanggalTtd"]').on('apply.daterangepicker', function (ev, picker) {
    $(this).val(picker.startDate.format('MM/DD/YYYY') + ' - ' + picker.endDate.format('MM/DD/YYYY'));
});
$('input[name="RangeTanggalTtd"]').on('cancel.daterangepicker', function (ev, picker) {
    $(this).val(''); 
});

//Event Tanggal Berakhir
$('input[name="RangeTanggalBerakhir"]').on('apply.daterangepicker', function (ev, picker) {
    $(this).val(picker.startDate.format('MM/DD/YYYY') + ' - ' + picker.endDate.format('MM/DD/YYYY'));
});
$('input[name="RangeTanggalBerakhir"]').on('cancel.daterangepicker', function (ev, picker) {
    $(this).val('');  
});

//Event DateRange
$('input[name="RangeCreateDate"]').on('apply.daterangepicker', function (ev, picker) {
    $(this).val(picker.startDate.format('MM/DD/YYYY') + ' - ' + picker.endDate.format('MM/DD/YYYY'));
});
$('input[name="RangeCreateDate"]').on('cancel.daterangepicker', function (ev, picker) {
    $(this).val(''); 
});
 
//Clear Tanggal TTD
function clearDateRange() {
    $("#rangeTanggalTtd").val("");
    $("#_rangeTanggalBerakhir").val("");
    $("#_rangeCreateDate").val(""); 
}


