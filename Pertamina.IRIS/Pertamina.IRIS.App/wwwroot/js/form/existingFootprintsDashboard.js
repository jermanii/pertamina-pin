$(function () {
    loadDataTable();
});

function loadDataTable(negaraMitra, streamBusiness, entitasPertamina) {
    var dt;

    if (!negaraMitra)
        negaraMitra = "";

    if (!streamBusiness)
        streamBusiness = "";

    if (!entitasPertamina)
        entitasPertamina = "";

    dt = $("#dataTable").DataTable({
        searchDelay: 500,
        processing: true,
        serverSide: true,
        retrieve: true,
        info: true,
        order: [],
        stateSave: false,
        ajax: {
            url: param.dataGrid + '?negaraMitra=' + negaraMitra + '&streamBusiness=' + streamBusiness + '&entitasPertamina=' + entitasPertamina,
            type: "POST",
            dataType: "json"
        },
        columns: [
            { data: 'CompanyName', "name": "CompanyName" },
            { data: 'PicFungsiName', "name": "PicFungsiName" },
            { data: 'HubHeadName', "name": "HubHeadName" },
            { data: 'StreamBusinessName', "name": "StreamBusinessName" },
            { data: 'TotalAsset', "name": "TotalAsset" },
            { data: 'Revenue', "name": "Revenue" },
            { data: 'Ebitda', "name": "Ebitda" },
            { data: 'Year', "name": "Year" },
            { data: 'NamaNegara', "name": "NamaNegara" },
        ],
        "columnDefs": [{
            "targets": [1],
            "render": function (data, type, row, meta) {
                if (data != null)
                    return '<div class="symbol symbol-20px symbol-circle me-2" data-bs-toggle="tooltip" title="' + data + '">' +
                        '<span class="symbol-label bg-info text-inverse-info fw-bold">' + data.substring(0, 1) + '</span></div>' +
                        '<span class="text-gray-800 text-hover-primary mb-1"  data-clipboard-text="' + row.PicFungsiEmail + ',' + row.PicFungsiPhone +
                        '" data-bs-toggle="tooltip" title="' + row.PicFungsiEmail + ',' + row.PicFungsiPhone + '">' + data + '</span>';
            }
        },
        {
            "targets": [2],
            "render": function (data, type, row, meta) {
                if (data != null)
                    return '<div class="symbol symbol-20px symbol-circle me-2" data-bs-toggle="tooltip" title="' + data + '">' +
                        '<span class="symbol-label bg-info text-inverse-info fw-bold">' + data.substring(0, 1) + '</span></div>' +
                        '<span class="text-gray-800 text-hover-primary mb-1">' + data + '</span>';
            }
        },
        {
            "targets": '_all',
            "defaultContent": "N/A"
        }, {
            targets: [4, 5, 6], // Apply to the 2nd column (Revenue)
            render: function (data, type, row) {
                return formatEconomic(data);
            }
        }]
    });
    dt.on('draw', function () {
        KTMenu.createInstances();
    });

    const filterSearch = document.querySelector('[data-kt-docs-table-filter="search"]');
    filterSearch.addEventListener('keyup', function (e) {
        dt.search(e.target.value).draw();
    });

    function formatEconomic(value) {
        if (value)
            value = formatNumber(value.toFixed(2));

        return value;
    }

    function formatNumber(num) {
        const parts = num.toString().split('.');
        parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ',');
        return parts.join('.');
    }
};

$(function () {
    $("#ddlNegaraMitra").select2({
        ajax: {
            url: url.ddlNegaraMitraWithoutKawasan,
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
    $("#ddlEntitasPertamina").select2({
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
    $("#ddlStreamBusiness").select2({
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
    $("#ddlExistingFootprintsSortingHighPriority").select2({
        ajax: {
            url: url.ddlExistingFootprintsSortingHighPriority,
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

function clipboardHeadEmail() {
    const target = document.getElementById('clipboardEmail');
    clipboard = new ClipboardJS(target);
    clipboard.on('success', function (e) {
        navigator.clipboard.writeText(e.text);
        toastr.success('Email success copy to clipboard!');
        e.clearSelection();
        $('[data-bs-toggle="tooltip"]').tooltip('hide');
    });
    clipboard.on('error', function (e) {
        toastr.error('Failed Copy to Clipboard');
    });
}

function clipboardPicHeadEmail() {
    const target = document.getElementById('clipboardPicEmail');
    clipboard = new ClipboardJS(target);
    clipboard.on('success', function (e) {
        navigator.clipboard.writeText(e.text);
        toastr.success('Email success copy to clipboard!');
        e.clearSelection();
        $('[data-bs-toggle="tooltip"]').tooltip('hide');
    });
    clipboard.on('error', function (e) {
        toastr.error('Failed Copy to Clipboard');
    });
}

function clipboardContactNumber() {
    const target = document.getElementById('clipboardContactNumber');
    clipboard = new ClipboardJS(target);
    clipboard.on('success', function (e) {
        navigator.clipboard.writeText(e.text);
        toastr.success('Contact Number success copy to clipboard!');
        e.clearSelection();
        $('[data-bs-toggle="tooltip"]').tooltip('hide');
    });
    clipboard.on('error', function (e) {
        toastr.error('Failed Copy to Clipboard');
    });
}

function clipboardPicContactNumber() {
    const target = document.getElementById('clipboardPicContactNumber');
    clipboard = new ClipboardJS(target);
    clipboard.on('success', function (e) {
        navigator.clipboard.writeText(e.text);
        toastr.success('Contact Number success copy to clipboard!');
        e.clearSelection();
        $('[data-bs-toggle="tooltip"]').tooltip('hide');
    });
    clipboard.on('error', function (e) {
        toastr.error('Failed Copy to Clipboard');
    });
}

function loadDetailPartial() {
    $('a[data-toggle="details-partial-modal"]').click(function (event) {
        var urlPartial = $(this).data("url");
        var detailModal = $('#DetailPartialModal');
        let target = $(event.currentTarget).data('target');
        $.get(urlPartial).done(function (response) {
            detailModal.html(response);
            $(target).modal('show');
            clipboardHeadEmail();
            clipboardContactNumber();
            clipboardPicHeadEmail();
            clipboardPicContactNumber();
            $('[data-bs-toggle="tooltip"]').tooltip();
        });
    });
};

$(function () {
    $.ajax({
        url: param.urlHighPriority + '?isHigh=' + true + '&isFilter=' + false + '&isSort=' + false
            + '&isScroll=' + false + '&isClickMap=' + false
            + '&countryAcronym=' + '&negara=' + '&stream=' + '&entitas='
            + '&category=' + '&order=' + '&pageCount=' + 1,
        type: 'GET',
        success: function (result) {
            $("#HighPriorityView").html(result);
            $('.lazy-loading').attr('hidden', 'hidden');
            loadDetailPartial();
        },
        error: function (xhr, status, error) {
            alert("Error: " + error);
        }
    });

    $.ajax({
        url: param.urlMetrics,
        type: 'GET',
        success: function (result) {
            $("#MetricsView").html(result);
        },
        error: function (xhr, status, error) {
            alert("Error: " + error);
        }
    });

    $(".resetButtonSorting").on("click", function () {
        $(".scrollHighPriorirty").scrollTop(0, 0);
        $('#PageCountSort').val('0');
        $('#CountryAcronym').val(null);
        $('#ddlExistingFootprintsSortingHighPriority').val(null).trigger('change');
        $('input[name="radioSorting"]').prop('checked', false);

        let isHigh;
        let isFilter;
        let negaraMitra = $('#ddlNegaraMitra').val();
        let streamBusiness = $('#ddlStreamBusiness').val();
        let entitasPertamina = $('#ddlEntitasPertamina').val();
        if (!negaraMitra && !streamBusiness && !entitasPertamina) {
            isHigh = true;
            isFilter = false;
            $('.headerHighPriority').text("High Priority Exisiting Footprints");
        } else {
            isHigh = false;
            isFilter = true;
            $('.headerHighPriority').text("Filtered Exisiting Footprints");
        }

        $.ajax({
            url: param.urlHighPriority + '?isHigh=' + isHigh + '&isFilter=' + isFilter + '&isSort=' + false
                + '&isScroll=' + false + '&isClickMap=' + false
                + '&countryAcronym=' + '&negara=' + negaraMitra + '&stream=' + streamBusiness + '&entitas=' + entitasPertamina
                + '&category=' + '&order=' + '&pageCount=' + 1,
            type: 'GET',
            success: function (result) {
                $("#HighPriorityView").html(result);
                $('.lazy-loading').attr('hidden', 'hidden');
                loadDetailPartial();
            },
            error: function (xhr, status, error) {
                alert("Error: " + error);
            }
        });
        $.ajax({
            url: param.urlMetrics,
            type: 'GET',
            success: function (result) {
                $("#MetricsView").html(result);
            },
            error: function (xhr, status, error) {
                alert("Error: " + error);
            }
        });
    });

    $(".applyButtonSorting").on("click", function () {
        $(".scrollHighPriorirty").scrollTop(0, 0);
        $('.headerHighPriority').text("Sorted Existing Footprints");
        $('#CountryAcronym').val(null);
        var categorySelect = $('#ddlExistingFootprintsSortingHighPriority').val();
        var sortOrder = $('input[name="radioSorting"]:checked').val();

        let isFilter;
        let negaraMitra = $('#ddlNegaraMitra').val();
        let streamBusiness = $('#ddlStreamBusiness').val();
        let entitasPertamina = $('#ddlEntitasPertamina').val();
        if (!negaraMitra && !streamBusiness && !entitasPertamina) {
            isFilter = false
        } else {
            isFilter = true
        }

        $.ajax({
            url: param.urlHighPriority + '?isHigh=' + false + '&isFilter=' + isFilter + '&isSort=' + true
                + '&isScroll=' + false + '&isClickMap=' + false
                + '&countryAcronym=' + '&negara=' + negaraMitra + '&stream=' + streamBusiness + '&entitas=' + entitasPertamina
                + '&category=' + categorySelect + '&order=' + sortOrder + '&pageCount=' + 1,
            type: 'GET',
            success: function (result) {
                $("#HighPriorityView").html(result);
                $('.lazy-loading').attr('hidden', 'hidden');
                loadDetailPartial();
            },
            error: function (xhr, status, error) {
                alert("Error: " + error);
            }
        });
    });
});

$(window).on('scroll', function () {
    $('.menu-sub-dropdown').removeClass('show');
});

$('.scrollHighPriorirty').on("scroll", function () {
    const div = $(this);
    var recordCount = $('#RecordHighPriorityCountSort').val();
    var allRecordCount = $('#AllRecordHighPriorityCountSort').val();

    if ((div.scrollTop() + div.innerHeight() > div[0].scrollHeight) && (recordCount != allRecordCount) && ($('.lazy-loading').attr('hidden') !== undefined)) {
        $('.lazy-loading').removeAttr('hidden');

        var categorySelect = $('#ddlExistingFootprintsSortingHighPriority').val();
        var sortOrder = $('input[name="radioSorting"]:checked').val();
        var rowCount = $('#PageCountSort').val();

        let isFilter;
        let isClickMap;
        let negaraMitra = $('#ddlNegaraMitra').val();
        let streamBusiness = $('#ddlStreamBusiness').val();
        let entitasPertamina = $('#ddlEntitasPertamina').val();
        let countryAcronym = $('#CountryAcronym').val();

        if (!negaraMitra && !streamBusiness && !entitasPertamina) {
            isFilter = false;
        } else {
            isFilter = true;
        }

        if (!countryAcronym) {
            isClickMap = false;
        } else {
            isClickMap = true;
        }

        $.ajax({
            url: param.urlHighPriority + '?isHigh=' + false + '&isFilter=' + isFilter + '&isSort=' + true
                + '&isScroll=' + true + '&isClickMap=' + isClickMap
                + '&countryAcronym=' + countryAcronym + '&negara=' + negaraMitra + '&stream=' + streamBusiness + '&entitas=' + entitasPertamina
                + '&category=' + categorySelect + '&order=' + sortOrder + '&pageCount=' + rowCount,
            type: 'GET',
            success: function (result) {
                $("#HighPriorityView").html(result);
                $('.lazy-loading').attr('hidden', 'hidden');
                loadDetailPartial();
            },
            error: function (xhr, status, error) {
                alert("Error: " + error);
            }
        });
    }
});