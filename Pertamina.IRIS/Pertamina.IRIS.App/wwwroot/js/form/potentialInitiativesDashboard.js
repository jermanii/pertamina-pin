// Table
var dt;
var entitasPertamina = "";
var streamBusiness = "";
var negaraMitra = "";
var kawasanMitra = "";
var initiativeHolder = "";
var initiativeStage = "";
var initiativeStatus = "";
var rangeCreateDate = "";

dt = $("#dataTable").DataTable({
    searchDelay: 500,
    processing: true,
    serverSide: true,
    retrieve: true,
    info: true,
    order: [],
    stateSave: false,
    ajax: {
        url: param.dataGrid + "?entitasPertamina=" + btoa(entitasPertamina) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&initiativeHolder=" + btoa(initiativeHolder) + "&initiativeStage=" + btoa(initiativeStage) + "&initiativeStatus=" + btoa(initiativeStatus) + "&rangeCreateDate=" + btoa(rangeCreateDate),
        type: "POST",
    },
    columns: [
        { data: 'RowJudulInitiative', "name": "RowJudulInitiative" },
        { data: 'RowPicLeadName', "name": "RowPicLeadName" },
        { data: 'RowHubHeadName', "name": "RowHubHeadName" },
        { data: 'RowTargetMoU', "name": "RowTargetMoU" },
        { data: 'RowTargetDefinitive', "name": "RowTargetDefinitive" },
        { data: 'RowPotentialRevenue', "name": "RowPotentialRevenue" },
        { data: 'RowCapex', "name": "RowCapex" },
        { data: 'RowStatusMilestone', "name": "RowStatusMilestone" },
        { data: 'RowSupportRequired', "name": "RowSupportRequired" }
    ],
    "columnDefs": [{
        "targets": [1],
        "render": function (data, type, row, meta) {
            if (data !== "-" && data != null) {

                return '<div class="symbol symbol-20px symbol-circle" data-bs-toggle="tooltip" title="' + data + '">' +
                    '<span class="symbol-label bg-info text-inverse-info fw-bold">' + data.substring(0, 1) + '</span></div>' +
                    '<span class="text-gray-800 text-hover-primary mb-1">' + data + '</span>';
            }
            return '<span class="text-gray-800 text-hover-primary mb-1">-</span>';
        }
    },
    {
        "targets": [2],
        "render": function (data, type, row, meta) {
            if (data !== "-" && data != null) {

                return '<div class="symbol symbol-20px symbol-circle" data-bs-toggle="tooltip" title="' + data + '">' +
                    '<span class="symbol-label bg-info text-inverse-info fw-bold">' + data.substring(0, 1) + '</span></div>' +
                    '<span class="text-gray-800 text-hover-primary mb-1">' + data + '</span>';
            }
            return '<span class="text-gray-800 text-hover-primary mb-1">-</span>';
        }
    }]
});


$('#sort-table').on('click', function () {
    var table = $("#dataTable").DataTable();
    var order = table.order();
    var orderDirection = "";
    if (order.length == 0) {
        orderDirection = "asc";
    }
    else {
        if (order[0][1] == "asc") {
            orderDirection = "desc";
        }
        else {
            orderDirection = "asc";
        }
    }
    table.order([0, orderDirection]).draw();
})

const filterSearch = document.querySelector('[data-kt-docs-table-filter="search"]');
filterSearch.addEventListener('keyup', function (e) {
    dt.search(this.value).draw(); 
});


dt.on('draw', function () {
    KTMenu.createInstances();
});

dt.on('draw', function () {
    KTMenu.createInstances();
});
$('a[data-toggle="detail-modal"]').click(function (event) {
    console.log(event);
    var urlPartial = $(this).data("url");
    var detailModal = $('#DetailPartialModal');
    let target = $(event.currentTarget).data('target');
    $.get(urlPartial).done(function (response) {
        detailModal.html(response);
        $(target).modal('show');
    });
    //$.get(urlPartial).done(function (response) {
    //    detailModal.html(response);                     
    //})
});

document.addEventListener('DOMContentLoaded', function () {
    // Get the button and popover container
    const popoverButton = document.querySelector('.popover-button');
    const popoverContainer = document.querySelector('.popover-container');

    // Toggle the popover on button click
    popoverButton.addEventListener('click', function () {
        popoverContainer.classList.toggle('active');
    });

    // Close the popover when clicking outside of it
    document.addEventListener('click', function (e) {
        if (!popoverContainer.contains(e.target)) {
            popoverContainer.classList.remove('active');
        }
    });
});


$('#all-metrics-modal').on('shown.bs.modal', function () {
    const countupElements = this.querySelectorAll('[data-kt-countup="true"]');
    countupElements.forEach((el) => {
        const countValue = el.getAttribute('data-kt-countup-value');
        const suffix = el.getAttribute('data-kt-countup-suffix') || '';

        let count = 0;
        const duration = 2000; // 2 seconds for the count up
        const stepTime = Math.abs(Math.floor(duration / countValue));

        // Avoid count up if countValue is 0
        if (parseInt(countValue) === 0) {
            el.innerText = countValue + suffix;
            return;
        }

        const interval = setInterval(() => {
            count++;
            el.innerText = count + suffix; // Use the incremented count

            if (count >= countValue) {
                clearInterval(interval);
                el.innerText = countValue + suffix; // Ensure it ends exactly at countValue
            }
        }, stepTime);
    });
});


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
});