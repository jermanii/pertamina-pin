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
            { data: 'JudulPerjanjian', name: 'JudulPerjanjian' },
            { data: 'PicFungsiName', name: 'PicFungsiName' },
            { data: 'EntitasPertamina', name: 'EntitasPertamina' },
            { data: 'KodeAgreement', name: 'KodeAgreement' },
            { data: 'TanggalTtd', name: 'TanggalTtd' },
            { data: 'PotentialRevenuePerYearFormat', name: 'PotentialRevenuePerYearFormat' },
            { data: 'CapexFormat', name: 'CapexFormat' }
        ],
        "columnDefs": [
            { className: 'ps-4', targets: 0 },
            {
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
                "targets": '_all',
                "defaultContent": "N/A"
            },
            { orderable: false, targets: [3, 4, 5, 6] },
            {
                targets: [5, 6],
                render: function (data, type, row) {
                    return formatEconomic(data);
                }
            },
            { width: '200px', targets: [0] }
        ]
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

//first page
$(function () {
    $.ajax({
        url: param.urlHighPriority + '?isHigh=' + true + '&isScroll=' + false + '&isSort=' + false + '&isFilter=' + false + '&isClickMap=' + false
            + '&negara=' + '&stream=' + '&entitas=' + '&countryAcronym='
            + '&category=' + null + '&order=' + null + '&pageCount=' + 1,
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

//select2 ddl
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
    $("#ddlSignedAgreementSortingHighPriority").select2({
        ajax: {
            url: url.ddlSignedAgreementSortingHighPriority,
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

$(window).on('scroll', function () {
    $('.menu-sub-dropdown').removeClass('show');
});

$('.scrollHighPriorirty').on("scroll", function () {
    const div = $(this);
    var recordCount = $('#RecordHighPriorityCountSort').val();
    var allRecordCount = $('#AllRecordHighPriorityCountSort').val();

    if (div.scrollTop() + div.innerHeight() > div[0].scrollHeight && (recordCount != allRecordCount) && ($('.lazy-loading').attr('hidden') !== undefined)) {
        $('.lazy-loading').removeAttr('hidden');

        var categorySelect = $('#ddlSignedAgreementSortingHighPriority').val();
        var sortOrder = $('input[name="radioSorting"]:checked').val();
        var rowCount = $('#PageCountSort').val();

        let isHigh;
        let isFilter;
        let isClickMap;
        let isSort;
        let negaraMitra = $('#ddlNegaraMitra').val();
        let streamBusiness = $('#ddlStreamBusiness').val();
        let entitasPertamina = $('#ddlEntitasPertamina').val();
        let countryAcronym = $('#CountryAcronym').val();

        if ((!negaraMitra && !streamBusiness && !entitasPertamina)) {
            isHigh = true;
            isFilter = false;
        } else {
            isHigh = false;
            isFilter = true;
        }

        if (!countryAcronym) {
            isClickMap = false;
        } else {
            isClickMap = true;
        }

        if (!categorySelect && !sortOrder) {
            isSort = false;
        }
        else {
            isSort = true;
        }

        $.ajax({
            url: param.urlHighPriority + '?isHigh=' + isHigh + '&isScroll=' + true + '&isSort=' + isSort + '&isFilter=' + isFilter + '&isClickMap=' + isClickMap
                + '&negara=' + negaraMitra + '&stream=' + streamBusiness + '&entitas=' + entitasPertamina + '&countryAcronym=' + countryAcronym
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

//sort button
$(function () {
    $(".resetButtonSorting").on("click", function () {
        $(".scrollHighPriorirty").scrollTop(0, 0);
        $('#PageCountSort').val('0');
        $('#ddlSignedAgreementSortingHighPriority').val(null).trigger('change');
        $('input[name="radioSorting"]').prop('checked', false);

        let isHigh;
        let isFilter;
        let isClickMap;
        let negaraMitra = $('#ddlNegaraMitra').val();
        let streamBusiness = $('#ddlStreamBusiness').val();
        let entitasPertamina = $('#ddlEntitasPertamina').val();
        let countryAcronym = $('#CountryAcronym').val();

        if ((!negaraMitra && !streamBusiness && !entitasPertamina)) {
            isHigh = true;
            isFilter = false;
            $('.headerHighPriority').text("Strategic Signed Agreement");
        } else {
            isHigh = false;
            isFilter = true;
            $('.headerHighPriority').text("Filtered Signed Agreement");
        }

        if (!countryAcronym) {
            isClickMap = false;
        } else {
            isHigh = false;
            isClickMap = true;
            $('.headerHighPriority').text("Clickable Map Signed Agreement");
        }

        $.ajax({
            url: param.urlHighPriority + '?isHigh=' + isHigh + '&isScroll=' + false + '&isSort=' + false + '&isFilter=' + isFilter + '&isClickMap=' + isClickMap
                + '&negara=' + negaraMitra + '&stream=' + streamBusiness + '&entitas=' + entitasPertamina + '&countryAcronym=' + countryAcronym
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
    });
    $(".applyButtonSorting").on("click", function () {
        $(".scrollHighPriorirty").scrollTop(0, 0);
        $('.headerHighPriority').text("Sorted Signed Agreement");
        var categorySelect = $('#ddlSignedAgreementSortingHighPriority').val();
        var sortOrder = $('input[name="radioSorting"]:checked').val();

        let isHigh;
        let isFilter;
        let isClickMap;
        let negaraMitra = $('#ddlNegaraMitra').val();
        let streamBusiness = $('#ddlStreamBusiness').val();
        let entitasPertamina = $('#ddlEntitasPertamina').val();
        let countryAcronym = $('#CountryAcronym').val();

        if ((!negaraMitra && !streamBusiness && !entitasPertamina)) {
            isHigh = true;
            isFilter = false;
        } else {
            isHigh = false;
            isFilter = true;
        }

        if (!countryAcronym) {
            isClickMap = false;
        } else {
            isHigh = false;
            isClickMap = true;
        }

        $.ajax({
            url: param.urlHighPriority + '?isHigh=' + isHigh + '&isScroll=' + false + '&isSort=' + true + '&isFilter=' + isFilter + '&isClickMap=' + isClickMap
                + '&negara=' + negaraMitra + '&stream=' + streamBusiness + '&entitas=' + entitasPertamina + '&countryAcronym=' + countryAcronym
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

function loadDetailPartial() {
    $('a[data-toggle="details-partial-modal"]').click(function (event) {
        var urlPartial = $(this).data("url");
        var detailModal = $('#DetailPartialModal');
        let target = $(event.currentTarget).data('target');
        $.get(urlPartial).done(function (response) {
            detailModal.html(response);
            $(target).modal('show');
            $('[data-bs-toggle="tooltip"]').tooltip();
        });
    });
};