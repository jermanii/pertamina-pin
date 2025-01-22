
$(function () {
    var dt;
    var entitasPertaminaId = $('#ddlEntitasPertamina').val();
    var projectTypeId = $('#ddlStreamBusiness').val();
    var totalAssetsMinOp = $('#ddlMinTotalAssets').val();
    var totalAssetsMaxOp = $('#ddlMaxTotalAssets').val();
    var revenueMinOp = $('#ddlMinRevenue').val();
    var revenueMaxOp = $('#ddlMaxRevenue').val();
    var ebitdaMinOp = $('#ddlMinEbitda').val();
    var ebitdaMaxOp = $('#ddlMaxEbitda').val();
    var yearOp = $('#ddlYear').val();
    var partnerCountryId = $('#ddlNegaraMitra').val();

    dt = $("#dataTable").DataTable({
        searchDelay: 500,
        processing: true,
        serverSide: true,
        retrieve: true,
        info: true,
        order: [],
        stateSave: false,
        ajax: {
            url: param.dataGrid + '?entitasPertaminaId=' + btoa(entitasPertaminaId) + '&projectTypeId=' + btoa(projectTypeId) + '&totalAssetsOp=' + '&totalAssetsMinOp=' + btoa(totalAssetsMinOp) + '&totalAssetsMaxOp=' + btoa(totalAssetsMaxOp) + '&revenueMinOp=' + btoa(revenueMinOp) + '&revenueMaxOp=' + btoa(revenueMaxOp) + '&ebitdaMinOp=' + btoa(ebitdaMinOp) + '&ebitdaMaxOp=' + btoa(ebitdaMaxOp) + '&yearOp=' + btoa(yearOp) + '&partnerCountryId=' + btoa(partnerCountryId) ,
            type: "POST",
            dataType: "json"
        },
        columns: [
            { data: 'GridEntitas', "name": "GridEntitas" },
            { data: 'GridPicFungsiName', "name": "GridPicFungsiName" },
            { data: 'GridHubHeadName', "name": "GridHubHeadName" },
            { data: 'GridProjectType', "name": "GridProjectType" },
            { data: 'GridTotalAssetToString', "name": "GridTotalAssetToString" },
            { data: 'GridRevenueToString', "name": "GridRevenueToString" },
            { data: 'GridEbitdaToString', "name": "GridEbitdaToString" },
            { data: 'GridYearToString', "name": "GridYearToString" },
            { data: 'GridNegaraMitra', "name": "GridNegaraMitra" },
            { data: null },
        ],
        columnDefs: [
            {
                targets: -1,
                data: null,
                orderable: false,
                className: 'text-center',
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
            }, {
                targets: [4, 5, 6], // Apply to the 2nd column (Revenue)
                render: function (data, type, row) {
                    return formatEconomic(data);
                }
            }
        ],
    });
    dt.on('draw', function () {
        KTMenu.createInstances();
    });

    const filterSearch = document.querySelector('[data-kt-docs-table-filter="search"]');
    filterSearch.addEventListener('keyup', function (e) {
        dt.search(e.target.value).draw();
    });

    $('#ddlEntitasPertamina, #ddlStreamBusiness, #ddlYear, #ddlNegaraMitra').on('change', function (e) {
        var entitasPertaminaId = $('#ddlEntitasPertamina').val().join('_');
        var projectTypeId = $('#ddlStreamBusiness').val().join('_');
        var totalAssetsMinOp = $('#ddlMinTotalAssets').val();
        var totalAssetsMaxOp = $('#ddlMaxTotalAssets').val();
        var revenueMinOp = $('#ddlMinRevenue').val();
        var revenueMaxOp = $('#ddlMaxRevenue').val();
        var ebitdaMinOp = $('#ddlMinEbitda').val();
        var ebitdaMaxOp = $('#ddlMaxEbitda').val();
        var yearOp = $('#ddlYear').val().join('_');
        var partnerCountryId = $('#ddlNegaraMitra').val().join('_');
        var urlEvent = '?entitasPertaminaId=' + btoa(entitasPertaminaId) + '&projectTypeId=' + btoa(projectTypeId) + '&totalAssetsMinOp=' + btoa(totalAssetsMinOp) + '&totalAssetsMaxOp=' + btoa(totalAssetsMaxOp) + '&revenueMinOp=' + btoa(revenueMinOp) + '&revenueMaxOp=' + btoa(revenueMaxOp) + '&ebitdaMinOp=' + btoa(ebitdaMinOp) + '&ebitdaMaxOp=' + btoa(ebitdaMaxOp) + '&yearOp=' + btoa(yearOp) + '&partnerCountryId=' + btoa(partnerCountryId);
        dt.ajax.url(param.dataGridEvent + urlEvent).load();


        var model = {};
        model.EntitasIdDecode = btoa(entitasPertaminaId);
        model.ProjectTypeIdDecode = btoa(projectTypeId);
        model.EbitdaMinDecode = btoa(ebitdaMinOp);
        model.EbitdaMaxDecode = btoa(ebitdaMaxOp);
        model.TotalAssetMinDecode = btoa(totalAssetsMinOp);
        model.TotalAssetMaxDecode = btoa(totalAssetsMaxOp);
        model.RevenueMinDecode = btoa(revenueMinOp);
        model.RevenueMaxDecode = btoa(revenueMaxOp);
        model.YearDecode = btoa(yearOp);
        model.NegaraMitraIdDecode = btoa(partnerCountryId);

        $.ajax({
            type: "POST",
            url: param.dataCountSearchGrid,
            data: JSON.stringify(model),
            contentType: "application/json; charshet=utf-8",
            dataType: "json",
            success: function (data) {
            }
        });
    });

    $('#ddlMinTotalAssets,#ddlMaxTotalAssets, #ddlMinRevenue, #ddlMaxRevenue, #ddlMinEbitda,#ddlMaxEbitda').on('input', function (e) {
        var entitasPertaminaId = $('#ddlEntitasPertamina').val().join('_');
        var projectTypeId = $('#ddlStreamBusiness').val().join('_');
        var totalAssetsMinOp = $('#ddlMinTotalAssets').val();
        var totalAssetsMaxOp = $('#ddlMaxTotalAssets').val();
        var revenueMinOp = $('#ddlMinRevenue').val();
        var revenueMaxOp = $('#ddlMaxRevenue').val();
        var ebitdaMinOp = $('#ddlMinEbitda').val();
        var ebitdaMaxOp = $('#ddlMaxEbitda').val();
        var yearOp = $('#ddlYear').val().join('_');
        var partnerCountryId = $('#ddlNegaraMitra').val().join('_');
        var urlEvent = '?entitasPertaminaId=' + btoa(entitasPertaminaId) + '&projectTypeId=' + btoa(projectTypeId) + '&totalAssetsMinOp=' + btoa(totalAssetsMinOp) + '&totalAssetsMaxOp=' + btoa(totalAssetsMaxOp) + '&revenueMinOp=' + btoa(revenueMinOp) + '&revenueMaxOp=' + btoa(revenueMaxOp) + '&ebitdaMinOp=' + btoa(ebitdaMinOp) + '&ebitdaMaxOp=' + btoa(ebitdaMaxOp) + '&yearOp=' + btoa(yearOp) + '&partnerCountryId=' + btoa(partnerCountryId);
        dt.ajax.url(param.dataGridEvent + urlEvent).load();


        var model = {};
        model.EntitasIdDecode = btoa(entitasPertaminaId);
        model.ProjectTypeIdDecode = btoa(projectTypeId);
        model.EbitdaMinDecode = btoa(ebitdaMinOp);
        model.EbitdaMaxDecode = btoa(ebitdaMaxOp);
        model.TotalAssetMinDecode = btoa(totalAssetsMinOp);
        model.TotalAssetMaxDecode = btoa(totalAssetsMaxOp);
        model.RevenueMinDecode = btoa(revenueMinOp);
        model.RevenueMaxDecode = btoa(revenueMaxOp);
        model.YearDecode = btoa(yearOp);
        model.NegaraMitraIdDecode = btoa(partnerCountryId);

        $.ajax({
            type: "POST",
            url: param.dataCountSearchGrid,
            data: JSON.stringify(model),
            contentType: "application/json; charshet=utf-8",
            dataType: "json",
            success: function (data) {
            }
        });
    });





    $('#ddlStreamBusiness').select2({
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

    $('#ddlEntitasPertamina').select2({
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
    $('#ddlNegaraMitra').select2({
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
    $('#ddlPicFungsi').select2({
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

    $('#ddlHubLead').select2({
        ajax: {
            url: url.ddlHubHead,
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

    $('#ddlYear').select2({
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

var dt;
dt = $("#dataTableDetails").DataTable({
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
    ],
    columnDefs: [
        {
            targets: [1, 2, 3], // Apply to the 2nd column (Revenue)
            render: function (data, type, row) {
                return formatEconomic(data);
            }
        }
    ],
});


function formatEconomic(value) {
    if (value != 'N/A')
        value = formatNumber(Number(value).toFixed(2));

    return value;
}

function formatNumber(num) {
    const parts = num.toString().split('.'); // Pisahkan angka dengan desimal
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ','); // Tambahkan koma pada ribuan
    return parts.join('.'); // Gabungkan kembali bagian integer dan desimal
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







