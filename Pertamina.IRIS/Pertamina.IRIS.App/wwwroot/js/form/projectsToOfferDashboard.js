var dt;

dt = $("#dataTable").DataTable({
    searchDelay: 500,
    processing: true,
    serverSide: true,
    retrieve: true,
    info: true,
    order: [],
    stateSave: false,
    ajax: {
        url: param.dataGrid,
        type:"POST",
    },
    columns: [
        { data: 'RowNamaProyek', "name": "RowNamaProyek" },
        { data: 'PICLead', "name": "PICLead" },
        { data: 'HubHeadName', "name": "HubHeadName" },
        { data: 'PotentialRevenuePerYearToString', "name": "PotentialRevenuePerYear" },
        { data: 'CapexToString', "name": "Capex" },
        { data: 'Progress', "name": "Progress" },
        //{ data: null }
    ],
    "columnDefs": [{
        "targets": [1],
        "render": function (data, type, row, meta) {
            
            if (data != null && data != '-')
                return '<div class="symbol symbol-20px symbol-circle" data-bs-toggle="tooltip" title="' + data + '" style="display: inline-block; margin-right: 8px;">' +
                    '<span class="symbol-label bg-info text-inverse-info fw-bold">' + data.substring(0, 1) + '</span></div>' +
                    '<span class="text-gray-800 text-hover-primary mb-1">' + data + '</span>';
            else
                return data;
        }
    },
    {
        "targets": [2],
        "width":"150px",
        "render": function (data, type, row, meta) {
            if (data != null && data != '-')
                '<div class="symbol symbol-20px symbol-circle" data-bs-toggle="tooltip" title="' + data + '" style="display: inline-block; margin-right: 8px;">' +
                    '<span class="symbol-label bg-info text-inverse-info fw-bold">' + data.substring(0, 1) + '</span></div>' +
                    '<span class="text-gray-800 text-hover-primary mb-1">' + data + '</span>';
            else
                return data;
        }
    }]
})
dt.on('draw', function () {
    KTMenu.createInstances();
});
const filterSearch = document.querySelector('[data-kt-docs-table-filter="search"]');
filterSearch.addEventListener('keyup', function (e) {
    dt.search(e.target.value).draw();
});
$(function () {
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
});
document.addEventListener('DOMContentLoaded', function () {

    const popoverButton = document.querySelector('.popover-button');
    const popoverContainer = document.querySelector('.popover-container');

    popoverButton.addEventListener('click', function () {
        popoverContainer.classList.toggle('active');
    });

    document.addEventListener('click', function (e) {
        if (!popoverContainer.contains(e.target)) {
            popoverContainer.classList.remove('active');
        }
    });

});

$('a[data-toggle="detail-modal"]').click(function (event) {
    var urlPartial = $(this).data("url");
    var detailModal = $('#DetailPartialModal');
    let target = $(event.currentTarget).data('target');
    $.get(urlPartial).done(function (response) {
        detailModal.html(response);
        $(target).modal('show');
    });
})
