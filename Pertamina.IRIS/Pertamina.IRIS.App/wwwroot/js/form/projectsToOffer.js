
var dt;
var opportunityHolder = $("#ddlOpportunityHolder").val().join('_');
var negaraMitra = $("#ddlNegaraMitra").val().join('_');
var streamBusiness = $("#ddlStreamBusiness").val().join('_');
var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
var rangeCreateDate = $("#rangeCreateDate").val();

dt = $("#dataTable").DataTable({
    searchDelay: 500,
    processing: true,
    serverSide: true,
    retrieve: true,
    info: true,
    order: [],
    stateSave: false,
    ajax: {
        url: param.dataGrid + "?entitasPertamina=" + btoa(entitasPertamina) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&opportunityHolder=" + btoa(opportunityHolder) + "&rangeCreateDate=" + btoa(rangeCreateDate),
        type: "POST",
    },
    columns: [
        {
            data: 'RowStatus', "name": "RowStatus",
            "render": function (data, type, row) {
                if (data == false) {
                    return '<span class="badge badge-success badge-lg">Saved</span>';
                } else {
                    return '<span class="badge badge-danger badge-lg">Draft</span>';
                }
            }
        },
        { data: 'RowNamaProyek', "name": "RowNamaProyek" },
        { data: 'RowStreamBusiness', "name": "RowStreamBusiness" },
        { data: 'RowEntitasPertamina', "name": "RowEntitasPertamina" },
        { data: 'RowPartner', "name": "RowPartner" },
        { data: 'RowKawasanMitra', "name": "RowKawasanMitra" },
        { data: 'RowNegaraMitra', "name": "RowNegaraMitra" },
        { data: 'RowKesiapanProyek', "name": "RowKesiapanProyek" },
        { data: null }
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
                    "<div " + param.isEdit + " class='menu-item px-3'>" +
                    "<a class='menu-link px-3' data-kt-docs-table-filter='edit_row' onclick='editAlertRedirect(\"" + data.Id + "\")'>" +
                    "Edit" +
                    "</a>" +
                    "</div>" +
                    "<div " + param.isDelete + " class='menu-item px-3'>" +
                    "<a class='menu-link px-3' data-kt-docs-table-filter='delete_row' onclick='deleteAlertRedirect(\"" + data.Id + "\")'>" +
                    "Delete" +
                    "</a>" +
                    "</div>" +
                    "</div>"
                "";
            },
        }, {
            "targets": '_all',
            "defaultContent":"N/A"
        },
        {
            "targets": 0,
            "orderable": false
        }, {
            "targets": 2,
            "orderable": false
        }, {
            "targets": 3,
            "orderable": false
        }, {
            "targets": 4,
            "orderable": false
        }, {
            "targets": 5,
            "orderable": false
        }, {
            "targets": 6,
            "orderable": false
        }, {
            "targets": 7,
            "orderable": false
        }, {
            "targets": 8,
            "orderable": false
        }
    ],
});

dt.on('draw', function () {
    KTMenu.createInstances();
});

const filterSearch = document.querySelector('[data-kt-docs-table-filter="search"]');
filterSearch.addEventListener('keyup', function (e) {

    dt.search(e.target.value).draw();

    var opportunityHolder = $("#ddlOpportunityHolder").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.OpportunityHolderEncode = btoa(opportunityHolder);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.StrSearch = e.target.value;
    model.IsDraft = isDraftSelected;


    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.CountOpportunityHolder != null)
                data.CountOpportunityHolder.forEach(opportunityHolderLoop);

            $('.counter-opportunity').text('(' + data.CountOpportunity + ')');
            $('.counter-negaraMitra').text('(' + data.CountNegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});

// Event Select
$('#ddlOpportunityHolder').on('select2:select', function (e) {

    var opportunityHolder = $("#ddlOpportunityHolder").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var searchValue = $('#searchDt').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&opportunityHolder=" + btoa(opportunityHolder) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + (isDraftSelected);
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.OpportunityHolderEncode = btoa(opportunityHolder);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.CountOpportunityHolder != null)
                data.CountOpportunityHolder.forEach(opportunityHolderLoop);

            $('.counter-opportunity').text('(' + data.CountOpportunity + ')');
            $('.counter-negaraMitra').text('(' + data.CountNegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});
$('#ddlOpportunityHolder').on('select2:unselect', function (e) {

    var opportunityHolder = $("#ddlOpportunityHolder").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var searchValue = $('#searchDt').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&opportunityHolder=" + btoa(opportunityHolder) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + (isDraftSelected);
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.OpportunityHolderEncode = btoa(opportunityHolder);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.CountOpportunityHolder != null)
                data.CountOpportunityHolder.forEach(opportunityHolderLoop);

            $('.counter-opportunity').text('(' + data.CountOpportunity + ')');
            $('.counter-negaraMitra').text('(' + data.CountNegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});
$('#ddlNegaraMitra').on('select2:select', function (e) {

    var opportunityHolder = $("#ddlOpportunityHolder").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var searchValue = $('#searchDt').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&opportunityHolder=" + btoa(opportunityHolder) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + (isDraftSelected);
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.OpportunityHolderEncode = btoa(opportunityHolder);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.CountOpportunityHolder != null)
                data.CountOpportunityHolder.forEach(opportunityHolderLoop);

            $('.counter-opportunity').text('(' + data.CountOpportunity + ')');
            $('.counter-negaraMitra').text('(' + data.CountNegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});
$('#ddlNegaraMitra').on('select2:unselect', function (e) {

    var opportunityHolder = $("#ddlOpportunityHolder").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var searchValue = $('#searchDt').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&opportunityHolder=" + btoa(opportunityHolder) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + (isDraftSelected);
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.OpportunityHolderEncode = btoa(opportunityHolder);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.CountOpportunityHolder != null)
                data.CountOpportunityHolder.forEach(opportunityHolderLoop);

            $('.counter-opportunity').text('(' + data.CountOpportunity + ')');
            $('.counter-negaraMitra').text('(' + data.CountNegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});
$('#ddlStreamBusiness').on('select2:select', function (e) {

    var opportunityHolder = $("#ddlOpportunityHolder").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var searchValue = $('#searchDt').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&opportunityHolder=" + btoa(opportunityHolder) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + (isDraftSelected);
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.OpportunityHolderEncode = btoa(opportunityHolder);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.isDraft = isDraftSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.CountOpportunityHolder != null)
                data.CountOpportunityHolder.forEach(opportunityHolderLoop);

            $('.counter-opportunity').text('(' + data.CountOpportunity + ')');
            $('.counter-negaraMitra').text('(' + data.CountNegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});
$('#ddlStreamBusiness').on('select2:unselect', function (e) {

    var opportunityHolder = $("#ddlOpportunityHolder").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var searchValue = $('#searchDt').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&opportunityHolder=" + btoa(opportunityHolder) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + (isDraftSelected);;
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.OpportunityHolderEncode = btoa(opportunityHolder);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.isDraft = isDraftSelected;
    model.StrSearch = searchValue;


    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.CountOpportunityHolder != null)
                data.CountOpportunityHolder.forEach(opportunityHolderLoop);

            $('.counter-opportunity').text('(' + data.CountOpportunity + ')');
            $('.counter-negaraMitra').text('(' + data.CountNegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});
$('#ddlEntitasPertamina').on('select2:select', function (e) {

    var opportunityHolder = $("#ddlOpportunityHolder").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var searchValue = $('#searchDt').val();


    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&opportunityHolder=" + btoa(opportunityHolder) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + (isDraftSelected);
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.OpportunityHolderEncode = btoa(opportunityHolder);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.isDraft = isDraftSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.CountOpportunityHolder != null)
                data.CountOpportunityHolder.forEach(opportunityHolderLoop);

            $('.counter-opportunity').text('(' + data.CountOpportunity + ')');
            $('.counter-negaraMitra').text('(' + data.CountNegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});
$('#ddlEntitasPertamina').on('select2:unselect', function (e) {

    var opportunityHolder = $("#ddlOpportunityHolder").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var searchValue = $('#searchDt').val();


    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&opportunityHolder=" + btoa(opportunityHolder) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + (isDraftSelected);
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.OpportunityHolderEncode = btoa(opportunityHolder);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.isDraft = isDraftSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.CountOpportunityHolder != null)
                data.CountOpportunityHolder.forEach(opportunityHolderLoop);

            $('.counter-opportunity').text('(' + data.CountOpportunity + ')');
            $('.counter-negaraMitra').text('(' + data.CountNegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});

//Event Filter Draft
$('#checkBoxIsDraft').change(function () {
    var opportunityHolder = $("#ddlOpportunityHolder").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var strSearch = $("#searchDt").val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&opportunityHolder=" + btoa(opportunityHolder) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + (isDraftSelected);
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {
        EntitasPertaminaEncode: btoa(entitasPertamina),
        StreamBusinessEncode: btoa(streamBusiness),
        NegaraMitraEncode: btoa(negaraMitra),
        OpportunityHolderEncode: btoa(opportunityHolder),
        RangeCreateDateEncode: btoa(rangeCreateDate),
        IsDraft: isDraftSelected,
        StrSearch : strSearch
    };

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.CountOpportunityHolder != null)
                data.CountOpportunityHolder.forEach(opportunityHolderLoop);

            $('.counter-opportunity').text('(' + data.CountOpportunity + ')');
            $('.counter-negaraMitra').text('(' + data.CountNegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});


// Event DateRange
$('input[name="RangeCreateDate"]').on('apply.daterangepicker', function (ev, picker) {
    $(this).val(picker.startDate.format('MM/DD/YYYY') + ' - ' + picker.endDate.format('MM/DD/YYYY'));
    var opportunityHolder = $("#ddlOpportunityHolder").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var searchValue = $('#searchDt').val();


    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&opportunityHolder=" + btoa(opportunityHolder) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + (isDraftSelected);
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.OpportunityHolderEncode = btoa(opportunityHolder);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.CountOpportunityHolder != null)
                data.CountOpportunityHolder.forEach(opportunityHolderLoop);

            $('.counter-opportunity').text('(' + data.CountOpportunity + ')');
            $('.counter-negaraMitra').text('(' + data.CountNegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});

$('input[name="RangeCreateDate"]').on('cancel.daterangepicker', function (ev, picker) {
    $(this).val('');
    var opportunityHolder = $("#ddlOpportunityHolder").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var searchValue = $('#searchDt').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&opportunityHolder=" + btoa(opportunityHolder) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + (isDraftSelected);
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.OpportunityHolderEncode = btoa(opportunityHolder);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.CountOpportunityHolder != null)
                data.CountOpportunityHolder.forEach(opportunityHolderLoop);

            $('.counter-opportunity').text('(' + data.CountOpportunity + ')');
            $('.counter-negaraMitra').text('(' + data.CountNegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});

// Loop
function opportunityHolderLoop(value, index, array) {
    $('.counter-opportunityHolder-' + value.Id).text('(' + value.Count + ')');
};


// Clear Date Range
function clearDateRange() {
    $("#rangeCreateDate").val("");
    var opportunityHolder = $("#ddlOpportunityHolder").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var searchValue = $('#searchDt').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&opportunityHolder=" + btoa(opportunityHolder) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + (isDraftSelected);
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.OpportunityHolderEncode = btoa(opportunityHolder);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.CountOpportunityHolder != null)
                data.CountOpportunityHolder.forEach(opportunityHolderLoop);

            $('.counter-opportunity').text('(' + data.CountOpportunity + ')');
            $('.counter-negaraMitra').text('(' + data.CountNegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
    location.reload();
}

// DDL
$(function () {
    $('#ddlOpportunityHolder').select2({
        ajax: {
            url: url.ddlHsh,
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
    $('#ddlNegaraMitra').select2({
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
        },
        maximumSelectionLength: 5
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
        },
        maximumSelectionLength: 5
    });
    $('#ddlEntitasPertamina').select2({
        ajax: {
            url: url.ddlEntitasPertaminaGrid,
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

// Date Range
//$(function () {
//    $("#rangeCreateDate").flatpickr({
//        dateFormat: "d-m-Y",
//        mode: "range"
//    });
//});

//// Clear Date Range
//function clearDateRange() {
//    const $flatpickrRangeCreateDate = $("#rangeCreateDate").flatpickr();

//    $flatpickrRangeCreateDate.clear();

//    $("#rangeCreateDate").flatpickr({
//        dateFormat: "d-m-Y",
//        mode: "range"
//    });
//}

$(function () {
    $("#rangeCreateDate").daterangepicker({
        autoUpdateInput: false,
        locale: {
            cancelLabel: 'Cancel'
        }
    });
})


function resetAllFilter() {
    location.reload();
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

function editAlertRedirect(guid) {
    var urlEdit = param.hrefUrlAction + "?guid=" + guid;
    $.ajax({
        type: "GET",
        url: param.checkCompanyCode + "?guid=" + guid,
        success: function (data) {
            if (data.items.IsError) {
                Swal.fire({
                    text: data.items.ErrorMessage,
                    icon: 'error',
                    buttonsStyling: false,
                    confirmButtonText: "Ok, got it!",
                    customClass: {
                        confirmButton: "btn btn-primary"
                    }
                });
            }
            else {
                window.location.href = urlEdit;
            }
        },
        error: function () {
            alert("Error while deleting data");
        }
    });
}