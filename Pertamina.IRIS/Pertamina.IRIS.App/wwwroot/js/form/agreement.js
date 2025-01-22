// Table

var dt;
var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
var streamBusiness = $("#ddlStreamBusiness").val().join('_');
var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
var negaraMitra = $("#ddlNegaraMitra").val().join('_');
var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
var agreementHolder = $("#ddlAgreementHolder").val().join('_');
var rangeTanggalTtd = $("#rangeTanggalTtd").val();
var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
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
        url: param.dataGrid + "?entitasPertamina=" + btoa(entitasPertamina) + "&jenisPerjanjian=" + btoa(jenisPerjanjian) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&statusBerlaku=" + btoa(statusBerlaku) + "&statusDiscussion=" + btoa(statusDiscussion) + "&agreementHolder=" + btoa(agreementHolder) + "&rangeTanggalTtd=" + btoa(rangeTanggalTtd) + "&rangeTanggalBerakhir=" + btoa(rangeTanggalBerakhir) + "&rangeCreateDate=" + btoa(rangeCreateDate),
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
        { data: 'KodeAgreement', "name": "KodeAgreement" },
        { data: 'JudulPerjanjian', "name": "JudulPerjanjian" },
        { data: 'RowPartner', "name": "RowPartner" },
        { data: 'RowEntitasPertamina', "name": "RowEntitasPertamina" },
        { data: 'RowStreamBusiness', "name": "RowStreamBusiness" },
        { data: 'RowJenisPerjanjian', "name": "RowJenisPerjanjian" },
        { data: 'RowNegaraMitra', "name": "RowNegaraMitra" },
        { data: 'RowKawasanMitra', "name": "RowKawasanMitra" },
        {
            data: 'RowStatusBerlaku', "name": "RowStatusBerlaku"
            , "render": function (data, type, row) {
                return '<span class="badge badge-' + row.RowStatusBerlakuColorName + ' badge-lg" style="background-color:' + row.RowStatusBerlakuColorHexa + '">' + data + '</span>';
            }
        },
        {
            data: 'RowDiscussionStatus', "name": "RowDiscussionStatus"
            , "render": function (data, type, row) {
                return '<span class="badge badge-' + row.RowDiscussionStatusColorName + ' badge-lg" style="background-color:' + row.RowDiscussionStatusColorHexa + '">' + data + '</span>';
            }
        },
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
                    "<a class='menu-link px-3' data-kt-docs-table-filter='edit_row' onclick='viewRedirect(\"" + data.Id + "\")'>" +
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
        }, {
            "targets": 9,
            "orderable": false
        }, {
            "targets": 10,
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

    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
    var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
    var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
    var agreementHolder = $("#ddlAgreementHolder").val().join('_');
    var rangeTanggalTtd = $("#rangeTanggalTtd").val();
    var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var isG2GSelected = $("#checkBoxIsG2G").prop("checked");
    var isStrategicPartnerShipSelected = $("#checkBoxIsStrategicPartnerShip").prop("checked");
    var isBdCoreBusinessSelected = $("#checkBoxIsBdCoreBusiness").prop("checked");
    var isGreenBusinessSelected = $("#checkBoxIsGreenBusiness").prop("checked");
    var rangeCreateDate = $("#rangeCreateDate").val();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.JenisPerjanjianEncode = btoa(jenisPerjanjian);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.KawasanMitraEncode = btoa(kawasanMitra);
    model.StatusBerlakuEncode = btoa(statusBerlaku);
    model.StatusDiscussionEncode = btoa(statusDiscussion);
    model.AgreementHolderEncode = btoa(agreementHolder);
    model.RangeTanggalTtdEncode = btoa(rangeTanggalTtd);
    model.RangeTanggalBerakhirEncode = btoa(rangeTanggalBerakhir);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.IsGtg = isG2GSelected;
    model.IsBdCoreBusinessDecode = isBdCoreBusinessSelected;
    model.IsStrategicPartnerShipDecode = isStrategicPartnerShipSelected;
    model.IsGreenBusinessDecode = isGreenBusinessSelected;
    model.StrSearch = e.target.value;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data.StatusBerlaku.forEach(statusBerlakuLoop);
            data.DiscussionStatus.forEach(discussionStatusLoop);
            data.AgreementHolder.forEach(agreementHolderLoop);
            $('.counter-perjanjian').text('(' + data.Perjanjian + ')');
            $('.counter-negaraMitra').text('(' + data.NegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});

// Event
$('#ddlEntitasPertamina').on('select2:select', function (e) {

    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
    var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
    var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
    var agreementHolder = $("#ddlAgreementHolder").val().join('_');
    var rangeTanggalTtd = $("#rangeTanggalTtd").val();
    var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var isG2GSelected = $("#checkBoxIsG2G").prop("checked");
    var isStrategicPartnerShipSelected = $("#checkBoxIsStrategicPartnerShip").prop("checked");
    var isBdCoreBusinessSelected = $("#checkBoxIsBdCoreBusiness").prop("checked");
    var isGreenBusinessSelected = $("#checkBoxIsGreenBusiness").prop("checked");
    var searchValue = $('#searchInput').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&jenisPerjanjian=" + btoa(jenisPerjanjian) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&statusBerlaku=" + btoa(statusBerlaku) + "&statusDiscussion=" + btoa(statusDiscussion) + "&agreementHolder=" + btoa(agreementHolder) + "&rangeTanggalTtd=" + btoa(rangeTanggalTtd) + "&rangeTanggalBerakhir=" + btoa(rangeTanggalBerakhir) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + isDraftSelected + "&isG2G=" + isG2GSelected + "&isStrategicPartnerShip=" + isStrategicPartnerShipSelected + "&isBdCoreBusiness=" + isBdCoreBusinessSelected + "&isGreenBusiness=" + isGreenBusinessSelected;
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.JenisPerjanjianEncode = btoa(jenisPerjanjian);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.KawasanMitraEncode = btoa(kawasanMitra);
    model.StatusBerlakuEncode = btoa(statusBerlaku);
    model.StatusDiscussionEncode = btoa(statusDiscussion);
    model.AgreementHolderEncode = btoa(agreementHolder);
    model.RangeTanggalTtdEncode = btoa(rangeTanggalTtd);
    model.RangeTanggalBerakhirEncode = btoa(rangeTanggalBerakhir);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.IsGtg = isG2GSelected;
    model.IsBdCoreBusinessDecode = isBdCoreBusinessSelected;
    model.IsStrategicPartnerShipDecode = isStrategicPartnerShipSelected;
    model.IsGreenBusinessDecode = isGreenBusinessSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data.StatusBerlaku.forEach(statusBerlakuLoop);
            data.DiscussionStatus.forEach(discussionStatusLoop);
            data.AgreementHolder.forEach(agreementHolderLoop);
            $('.counter-perjanjian').text('(' + data.Perjanjian + ')');
            $('.counter-negaraMitra').text('(' + data.NegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});
$('#ddlEntitasPertamina').on("select2:unselect", function (e) {
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
    var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
    var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
    var agreementHolder = $("#ddlAgreementHolder").val().join('_');
    var rangeTanggalTtd = $("#rangeTanggalTtd").val();
    var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var isG2GSelected = $("#checkBoxIsG2G").prop("checked");
    var isStrategicPartnerShipSelected = $("#checkBoxIsStrategicPartnerShip").prop("checked");
    var isBdCoreBusinessSelected = $("#checkBoxIsBdCoreBusiness").prop("checked");
    var isGreenBusinessSelected = $("#checkBoxIsGreenBusiness").prop("checked");
    var searchValue = $('#searchInput').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&jenisPerjanjian=" + btoa(jenisPerjanjian) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&statusBerlaku=" + btoa(statusBerlaku) + "&statusDiscussion=" + btoa(statusDiscussion) + "&agreementHolder=" + btoa(agreementHolder) + "&rangeTanggalTtd=" + btoa(rangeTanggalTtd) + "&rangeTanggalBerakhir=" + btoa(rangeTanggalBerakhir) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + isDraftSelected + "&isG2G=" + isG2GSelected + "&isStrategicPartnerShip=" + isStrategicPartnerShipSelected + "&isBdCoreBusiness=" + isBdCoreBusinessSelected + "&isGreenBusiness=" + isGreenBusinessSelected;
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.JenisPerjanjianEncode = btoa(jenisPerjanjian);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.KawasanMitraEncode = btoa(kawasanMitra);
    model.StatusBerlakuEncode = btoa(statusBerlaku);
    model.StatusDiscussionEncode = btoa(statusDiscussion);
    model.AgreementHolderEncode = btoa(agreementHolder);
    model.RangeTanggalTtdEncode = btoa(rangeTanggalTtd);
    model.RangeTanggalBerakhirEncode = btoa(rangeTanggalBerakhir);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.IsGtg = isG2GSelected;
    model.IsBdCoreBusinessDecode = isBdCoreBusinessSelected;
    model.IsStrategicPartnerShipDecode = isStrategicPartnerShipSelected;
    model.IsGreenBusinessDecode = isGreenBusinessSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data.StatusBerlaku.forEach(statusBerlakuLoop);
            data.DiscussionStatus.forEach(discussionStatusLoop);
            data.AgreementHolder.forEach(agreementHolderLoop);
            $('.counter-perjanjian').text('(' + data.Perjanjian + ')');
            $('.counter-negaraMitra').text('(' + data.NegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});
$('#ddlStreamBusiness').on('select2:select', function (e) {
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
    var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
    var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
    var agreementHolder = $("#ddlAgreementHolder").val().join('_');
    var rangeTanggalTtd = $("#rangeTanggalTtd").val();
    var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var isG2GSelected = $("#checkBoxIsG2G").prop("checked");
    var isStrategicPartnerShipSelected = $("#checkBoxIsStrategicPartnerShip").prop("checked");
    var isBdCoreBusinessSelected = $("#checkBoxIsBdCoreBusiness").prop("checked");
    var isGreenBusinessSelected = $("#checkBoxIsGreenBusiness").prop("checked");
    var searchValue = $('#searchInput').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&jenisPerjanjian=" + btoa(jenisPerjanjian) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&statusBerlaku=" + btoa(statusBerlaku) + "&statusDiscussion=" + btoa(statusDiscussion) + "&agreementHolder=" + btoa(agreementHolder) + "&rangeTanggalTtd=" + btoa(rangeTanggalTtd) + "&rangeTanggalBerakhir=" + btoa(rangeTanggalBerakhir) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + isDraftSelected + "&isG2G=" + isG2GSelected + "&isStrategicPartnerShip=" + isStrategicPartnerShipSelected + "&isBdCoreBusiness=" + isBdCoreBusinessSelected + "&isGreenBusiness=" + isGreenBusinessSelected;
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.JenisPerjanjianEncode = btoa(jenisPerjanjian);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.KawasanMitraEncode = btoa(kawasanMitra);
    model.StatusBerlakuEncode = btoa(statusBerlaku);
    model.StatusDiscussionEncode = btoa(statusDiscussion);
    model.AgreementHolderEncode = btoa(agreementHolder);
    model.RangeTanggalTtdEncode = btoa(rangeTanggalTtd);
    model.RangeTanggalBerakhirEncode = btoa(rangeTanggalBerakhir);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.IsGtg = isG2GSelected;
    model.IsBdCoreBusinessDecode = isBdCoreBusinessSelected;
    model.IsStrategicPartnerShipDecode = isStrategicPartnerShipSelected;
    model.IsGreenBusinessDecode = isGreenBusinessSelected;
    model.StrSearch = searchValue;
    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data.StatusBerlaku.forEach(statusBerlakuLoop);
            data.DiscussionStatus.forEach(discussionStatusLoop);
            data.AgreementHolder.forEach(agreementHolderLoop);
            $('.counter-perjanjian').text('(' + data.Perjanjian + ')');
            $('.counter-negaraMitra').text('(' + data.NegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});
$('#ddlStreamBusiness').on("select2:unselect", function (e) {
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
    var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
    var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
    var agreementHolder = $("#ddlAgreementHolder").val().join('_');
    var rangeTanggalTtd = $("#rangeTanggalTtd").val();
    var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var isG2GSelected = $("#checkBoxIsG2G").prop("checked");
    var isStrategicPartnerShipSelected = $("#checkBoxIsStrategicPartnerShip").prop("checked");
    var isBdCoreBusinessSelected = $("#checkBoxIsBdCoreBusiness").prop("checked");
    var isGreenBusinessSelected = $("#checkBoxIsGreenBusiness").prop("checked");
    var searchValue = $('#searchInput').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&jenisPerjanjian=" + btoa(jenisPerjanjian) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&statusBerlaku=" + btoa(statusBerlaku) + "&statusDiscussion=" + btoa(statusDiscussion) + "&agreementHolder=" + btoa(agreementHolder) + "&rangeTanggalTtd=" + btoa(rangeTanggalTtd) + "&rangeTanggalBerakhir=" + btoa(rangeTanggalBerakhir) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + isDraftSelected + "&isG2G=" + isG2GSelected + "&isStrategicPartnerShip=" + isStrategicPartnerShipSelected + "&isBdCoreBusiness=" + isBdCoreBusinessSelected + "&isGreenBusiness=" + isGreenBusinessSelected;
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.JenisPerjanjianEncode = btoa(jenisPerjanjian);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.KawasanMitraEncode = btoa(kawasanMitra);
    model.StatusBerlakuEncode = btoa(statusBerlaku);
    model.StatusDiscussionEncode = btoa(statusDiscussion);
    model.AgreementHolderEncode = btoa(agreementHolder);
    model.RangeTanggalTtdEncode = btoa(rangeTanggalTtd);
    model.RangeTanggalBerakhirEncode = btoa(rangeTanggalBerakhir);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.IsGtg = isG2GSelected;
    model.IsBdCoreBusinessDecode = isBdCoreBusinessSelected;
    model.IsStrategicPartnerShipDecode = isStrategicPartnerShipSelected;
    model.IsGreenBusinessDecode = isGreenBusinessSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data.StatusBerlaku.forEach(statusBerlakuLoop);
            data.DiscussionStatus.forEach(discussionStatusLoop);
            data.AgreementHolder.forEach(agreementHolderLoop);
            $('.counter-perjanjian').text('(' + data.Perjanjian + ')');
            $('.counter-negaraMitra').text('(' + data.NegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});
$('#ddlJenisPerjanjian').on('select2:select', function (e) {
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
    var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
    var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
    var agreementHolder = $("#ddlAgreementHolder").val().join('_');
    var rangeTanggalTtd = $("#rangeTanggalTtd").val();
    var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var isG2GSelected = $("#checkBoxIsG2G").prop("checked");
    var isStrategicPartnerShipSelected = $("#checkBoxIsStrategicPartnerShip").prop("checked");
    var isBdCoreBusinessSelected = $("#checkBoxIsBdCoreBusiness").prop("checked");
    var isGreenBusinessSelected = $("#checkBoxIsGreenBusiness").prop("checked");
    var searchValue = $('#searchInput').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&jenisPerjanjian=" + btoa(jenisPerjanjian) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&statusBerlaku=" + btoa(statusBerlaku) + "&statusDiscussion=" + btoa(statusDiscussion) + "&agreementHolder=" + btoa(agreementHolder) + "&rangeTanggalTtd=" + btoa(rangeTanggalTtd) + "&rangeTanggalBerakhir=" + btoa(rangeTanggalBerakhir) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + isDraftSelected + "&isG2G=" + isG2GSelected + "&isStrategicPartnerShip=" + isStrategicPartnerShipSelected + "&isBdCoreBusiness=" + isBdCoreBusinessSelected + "&isGreenBusiness=" + isGreenBusinessSelected;
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.JenisPerjanjianEncode = btoa(jenisPerjanjian);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.KawasanMitraEncode = btoa(kawasanMitra);
    model.StatusBerlakuEncode = btoa(statusBerlaku);
    model.StatusDiscussionEncode = btoa(statusDiscussion);
    model.AgreementHolderEncode = btoa(agreementHolder);
    model.RangeTanggalTtdEncode = btoa(rangeTanggalTtd);
    model.RangeTanggalBerakhirEncode = btoa(rangeTanggalBerakhir);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.IsGtg = isG2GSelected;
    model.IsBdCoreBusinessDecode = isBdCoreBusinessSelected;
    model.IsStrategicPartnerShipDecode = isStrategicPartnerShipSelected;
    model.IsGreenBusinessDecode = isGreenBusinessSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data.StatusBerlaku.forEach(statusBerlakuLoop);
            data.DiscussionStatus.forEach(discussionStatusLoop);
            data.AgreementHolder.forEach(agreementHolderLoop);
            $('.counter-perjanjian').text('(' + data.Perjanjian + ')');
            $('.counter-negaraMitra').text('(' + data.NegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});
$('#ddlJenisPerjanjian').on("select2:unselect", function (e) {
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
    var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
    var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
    var agreementHolder = $("#ddlAgreementHolder").val().join('_');
    var rangeTanggalTtd = $("#rangeTanggalTtd").val();
    var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var isG2GSelected = $("#checkBoxIsG2G").prop("checked");
    var isStrategicPartnerShipSelected = $("#checkBoxIsStrategicPartnerShip").prop("checked");
    var isBdCoreBusinessSelected = $("#checkBoxIsBdCoreBusiness").prop("checked");
    var isGreenBusinessSelected = $("#checkBoxIsGreenBusiness").prop("checked");
    var searchValue = $('#searchInput').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&jenisPerjanjian=" + btoa(jenisPerjanjian) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&statusBerlaku=" + btoa(statusBerlaku) + "&statusDiscussion=" + btoa(statusDiscussion) + "&agreementHolder=" + btoa(agreementHolder) + "&rangeTanggalTtd=" + btoa(rangeTanggalTtd) + "&rangeTanggalBerakhir=" + btoa(rangeTanggalBerakhir) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + isDraftSelected + "&isG2G=" + isG2GSelected + "&isStrategicPartnerShip=" + isStrategicPartnerShipSelected + "&isBdCoreBusiness=" + isBdCoreBusinessSelected + "&isGreenBusiness=" + isGreenBusinessSelected;
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.JenisPerjanjianEncode = btoa(jenisPerjanjian);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.KawasanMitraEncode = btoa(kawasanMitra);
    model.StatusBerlakuEncode = btoa(statusBerlaku);
    model.StatusDiscussionEncode = btoa(statusDiscussion);
    model.AgreementHolderEncode = btoa(agreementHolder);
    model.RangeTanggalTtdEncode = btoa(rangeTanggalTtd);
    model.RangeTanggalBerakhirEncode = btoa(rangeTanggalBerakhir);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.IsGtg = isG2GSelected;
    model.IsBdCoreBusinessDecode = isBdCoreBusinessSelected;
    model.IsStrategicPartnerShipDecode = isStrategicPartnerShipSelected;
    model.IsGreenBusinessDecode = isGreenBusinessSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data.StatusBerlaku.forEach(statusBerlakuLoop);
            data.DiscussionStatus.forEach(discussionStatusLoop);
            data.AgreementHolder.forEach(agreementHolderLoop);
            $('.counter-perjanjian').text('(' + data.Perjanjian + ')');
            $('.counter-negaraMitra').text('(' + data.NegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});
$('#ddlNegaraMitra').on('select2:select', function (e) {
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
    var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
    var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
    var agreementHolder = $("#ddlAgreementHolder").val().join('_');
    var rangeTanggalTtd = $("#rangeTanggalTtd").val();
    var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var isG2GSelected = $("#checkBoxIsG2G").prop("checked");
    var isStrategicPartnerShipSelected = $("#checkBoxIsStrategicPartnerShip").prop("checked");
    var isBdCoreBusinessSelected = $("#checkBoxIsBdCoreBusiness").prop("checked");
    var isGreenBusinessSelected = $("#checkBoxIsGreenBusiness").prop("checked");
    var searchValue = $('#searchInput').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&jenisPerjanjian=" + btoa(jenisPerjanjian) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&statusBerlaku=" + btoa(statusBerlaku) + "&statusDiscussion=" + btoa(statusDiscussion) + "&agreementHolder=" + btoa(agreementHolder) + "&rangeTanggalTtd=" + btoa(rangeTanggalTtd) + "&rangeTanggalBerakhir=" + btoa(rangeTanggalBerakhir) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + isDraftSelected + "&isG2G=" + isG2GSelected + "&isStrategicPartnerShip=" + isStrategicPartnerShipSelected + "&isBdCoreBusiness=" + isBdCoreBusinessSelected + "&isGreenBusiness=" + isGreenBusinessSelected;
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.JenisPerjanjianEncode = btoa(jenisPerjanjian);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.KawasanMitraEncode = btoa(kawasanMitra);
    model.StatusBerlakuEncode = btoa(statusBerlaku);
    model.StatusDiscussionEncode = btoa(statusDiscussion);
    model.AgreementHolderEncode = btoa(agreementHolder);
    model.RangeTanggalTtdEncode = btoa(rangeTanggalTtd);
    model.RangeTanggalBerakhirEncode = btoa(rangeTanggalBerakhir);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.IsGtg = isG2GSelected;
    model.IsBdCoreBusinessDecode = isBdCoreBusinessSelected;
    model.IsStrategicPartnerShipDecode = isStrategicPartnerShipSelected;
    model.IsGreenBusinessDecode = isGreenBusinessSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data.StatusBerlaku.forEach(statusBerlakuLoop);
            data.DiscussionStatus.forEach(discussionStatusLoop);
            data.AgreementHolder.forEach(agreementHolderLoop);
            $('.counter-perjanjian').text('(' + data.Perjanjian + ')');
            $('.counter-negaraMitra').text('(' + data.NegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});
$('#ddlNegaraMitra').on("select2:unselect", function (e) {
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
    var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
    var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
    var agreementHolder = $("#ddlAgreementHolder").val().join('_');
    var rangeTanggalTtd = $("#rangeTanggalTtd").val();
    var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var isG2GSelected = $("#checkBoxIsG2G").prop("checked");
    var isStrategicPartnerShipSelected = $("#checkBoxIsStrategicPartnerShip").prop("checked");
    var isBdCoreBusinessSelected = $("#checkBoxIsBdCoreBusiness").prop("checked");
    var isGreenBusinessSelected = $("#checkBoxIsGreenBusiness").prop("checked");
    var searchValue = $('#searchInput').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&jenisPerjanjian=" + btoa(jenisPerjanjian) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&statusBerlaku=" + btoa(statusBerlaku) + "&statusDiscussion=" + btoa(statusDiscussion) + "&agreementHolder=" + btoa(agreementHolder) + "&rangeTanggalTtd=" + btoa(rangeTanggalTtd) + "&rangeTanggalBerakhir=" + btoa(rangeTanggalBerakhir) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + isDraftSelected + "&isG2G=" + isG2GSelected + "&isStrategicPartnerShip=" + isStrategicPartnerShipSelected + "&isBdCoreBusiness=" + isBdCoreBusinessSelected + "&isGreenBusiness=" + isGreenBusinessSelected;
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.JenisPerjanjianEncode = btoa(jenisPerjanjian);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.KawasanMitraEncode = btoa(kawasanMitra);
    model.StatusBerlakuEncode = btoa(statusBerlaku);
    model.StatusDiscussionEncode = btoa(statusDiscussion);
    model.AgreementHolderEncode = btoa(agreementHolder);
    model.RangeTanggalTtdEncode = btoa(rangeTanggalTtd);
    model.RangeTanggalBerakhirEncode = btoa(rangeTanggalBerakhir);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.IsGtg = isG2GSelected;
    model.IsBdCoreBusinessDecode = isBdCoreBusinessSelected;
    model.IsStrategicPartnerShipDecode = isStrategicPartnerShipSelected;
    model.IsGreenBusinessDecode = isGreenBusinessSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data.StatusBerlaku.forEach(statusBerlakuLoop);
            data.DiscussionStatus.forEach(discussionStatusLoop);
            data.AgreementHolder.forEach(agreementHolderLoop);
            $('.counter-perjanjian').text('(' + data.Perjanjian + ')');
            $('.counter-negaraMitra').text('(' + data.NegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});
$('#ddlKawasanMitra').on('select2:select', function (e) {
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
    var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
    var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
    var agreementHolder = $("#ddlAgreementHolder").val().join('_');
    var rangeTanggalTtd = $("#rangeTanggalTtd").val();
    var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var isG2GSelected = $("#checkBoxIsG2G").prop("checked");
    var isStrategicPartnerShipSelected = $("#checkBoxIsStrategicPartnerShip").prop("checked");
    var isBdCoreBusinessSelected = $("#checkBoxIsBdCoreBusiness").prop("checked");
    var isGreenBusinessSelected = $("#checkBoxIsGreenBusiness").prop("checked");
    var searchValue = $('#searchInput').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&jenisPerjanjian=" + btoa(jenisPerjanjian) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&statusBerlaku=" + btoa(statusBerlaku) + "&statusDiscussion=" + btoa(statusDiscussion) + "&agreementHolder=" + btoa(agreementHolder) + "&rangeTanggalTtd=" + btoa(rangeTanggalTtd) + "&rangeTanggalBerakhir=" + btoa(rangeTanggalBerakhir) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + isDraftSelected + "&isG2G=" + isG2GSelected + "&isStrategicPartnerShip=" + isStrategicPartnerShipSelected + "&isBdCoreBusiness=" + isBdCoreBusinessSelected + "&isGreenBusiness=" + isGreenBusinessSelected;
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.JenisPerjanjianEncode = btoa(jenisPerjanjian);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.KawasanMitraEncode = btoa(kawasanMitra);
    model.StatusBerlakuEncode = btoa(statusBerlaku);
    model.StatusDiscussionEncode = btoa(statusDiscussion);
    model.AgreementHolderEncode = btoa(agreementHolder);
    model.RangeTanggalTtdEncode = btoa(rangeTanggalTtd);
    model.RangeTanggalBerakhirEncode = btoa(rangeTanggalBerakhir);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.IsGtg = isG2GSelected;
    model.IsBdCoreBusinessDecode = isBdCoreBusinessSelected;
    model.IsStrategicPartnerShipDecode = isStrategicPartnerShipSelected;
    model.IsGreenBusinessDecode = isGreenBusinessSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data.StatusBerlaku.forEach(statusBerlakuLoop);
            data.DiscussionStatus.forEach(discussionStatusLoop);
            data.AgreementHolder.forEach(agreementHolderLoop);
            $('.counter-perjanjian').text('(' + data.Perjanjian + ')');
            $('.counter-negaraMitra').text('(' + data.NegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});
$('#ddlKawasanMitra').on("select2:unselect", function (e) {
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
    var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
    var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
    var agreementHolder = $("#ddlAgreementHolder").val().join('_');
    var rangeTanggalTtd = $("#rangeTanggalTtd").val();
    var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var isG2GSelected = $("#checkBoxIsG2G").prop("checked");
    var isStrategicPartnerShipSelected = $("#checkBoxIsStrategicPartnerShip").prop("checked");
    var isBdCoreBusinessSelected = $("#checkBoxIsBdCoreBusiness").prop("checked");
    var isGreenBusinessSelected = $("#checkBoxIsGreenBusiness").prop("checked");
    var searchValue = $('#searchInput').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&jenisPerjanjian=" + btoa(jenisPerjanjian) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&statusBerlaku=" + btoa(statusBerlaku) + "&statusDiscussion=" + btoa(statusDiscussion) + "&agreementHolder=" + btoa(agreementHolder) + "&rangeTanggalTtd=" + btoa(rangeTanggalTtd) + "&rangeTanggalBerakhir=" + btoa(rangeTanggalBerakhir) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + isDraftSelected + "&isG2G=" + isG2GSelected + "&isStrategicPartnerShip=" + isStrategicPartnerShipSelected + "&isBdCoreBusiness=" + isBdCoreBusinessSelected + "&isGreenBusiness=" + isGreenBusinessSelected;
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.JenisPerjanjianEncode = btoa(jenisPerjanjian);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.KawasanMitraEncode = btoa(kawasanMitra);
    model.StatusBerlakuEncode = btoa(statusBerlaku);
    model.StatusDiscussionEncode = btoa(statusDiscussion);
    model.AgreementHolderEncode = btoa(agreementHolder);
    model.RangeTanggalTtdEncode = btoa(rangeTanggalTtd);
    model.RangeTanggalBerakhirEncode = btoa(rangeTanggalBerakhir);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.IsGtg = isG2GSelected;
    model.IsBdCoreBusinessDecode = isBdCoreBusinessSelected;
    model.IsStrategicPartnerShipDecode = isStrategicPartnerShipSelected;
    model.IsGreenBusinessDecode = isGreenBusinessSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data.StatusBerlaku.forEach(statusBerlakuLoop);
            data.DiscussionStatus.forEach(discussionStatusLoop);
            data.AgreementHolder.forEach(agreementHolderLoop);
            $('.counter-perjanjian').text('(' + data.Perjanjian + ')');
            $('.counter-negaraMitra').text('(' + data.NegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});
$('#ddlStatusBerlaku').on('select2:select', function (e) {
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
    var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
    var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
    var agreementHolder = $("#ddlAgreementHolder").val().join('_');
    var rangeTanggalTtd = $("#rangeTanggalTtd").val();
    var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var isG2GSelected = $("#checkBoxIsG2G").prop("checked");
    var isStrategicPartnerShipSelected = $("#checkBoxIsStrategicPartnerShip").prop("checked");
    var isBdCoreBusinessSelected = $("#checkBoxIsBdCoreBusiness").prop("checked");
    var isGreenBusinessSelected = $("#checkBoxIsGreenBusiness").prop("checked");
    var searchValue = $('#searchInput').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&jenisPerjanjian=" + btoa(jenisPerjanjian) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&statusBerlaku=" + btoa(statusBerlaku) + "&statusDiscussion=" + btoa(statusDiscussion) + "&agreementHolder=" + btoa(agreementHolder) + "&rangeTanggalTtd=" + btoa(rangeTanggalTtd) + "&rangeTanggalBerakhir=" + btoa(rangeTanggalBerakhir) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + isDraftSelected + "&isG2G=" + isG2GSelected + "&isStrategicPartnerShip=" + isStrategicPartnerShipSelected + "&isBdCoreBusiness=" + isBdCoreBusinessSelected + "&isGreenBusiness=" + isGreenBusinessSelected;
    dt.ajax.url(param.dataGridEvent + uriAll).load();
    console.log('Test trigger change');

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.JenisPerjanjianEncode = btoa(jenisPerjanjian);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.KawasanMitraEncode = btoa(kawasanMitra);
    model.StatusBerlakuEncode = btoa(statusBerlaku);
    model.StatusDiscussionEncode = btoa(statusDiscussion);
    model.AgreementHolderEncode = btoa(agreementHolder);
    model.RangeTanggalTtdEncode = btoa(rangeTanggalTtd);
    model.RangeTanggalBerakhirEncode = btoa(rangeTanggalBerakhir);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.IsGtg = isG2GSelected;
    model.IsBdCoreBusinessDecode = isBdCoreBusinessSelected;
    model.IsStrategicPartnerShipDecode = isStrategicPartnerShipSelected;
    model.IsGreenBusinessDecode = isGreenBusinessSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data.StatusBerlaku.forEach(statusBerlakuLoop);
            data.DiscussionStatus.forEach(discussionStatusLoop);
            data.AgreementHolder.forEach(agreementHolderLoop);
            $('.counter-perjanjian').text('(' + data.Perjanjian + ')');
            $('.counter-negaraMitra').text('(' + data.NegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});
$('#ddlStatusBerlaku').on("select2:unselect", function (e) {
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
    var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
    var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
    var agreementHolder = $("#ddlAgreementHolder").val().join('_');
    var rangeTanggalTtd = $("#rangeTanggalTtd").val();
    var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var isG2GSelected = $("#checkBoxIsG2G").prop("checked");
    var isStrategicPartnerShipSelected = $("#checkBoxIsStrategicPartnerShip").prop("checked");
    var isBdCoreBusinessSelected = $("#checkBoxIsBdCoreBusiness").prop("checked");
    var isGreenBusinessSelected = $("#checkBoxIsGreenBusiness").prop("checked");
    var searchValue = $('#searchInput').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&jenisPerjanjian=" + btoa(jenisPerjanjian) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&statusBerlaku=" + btoa(statusBerlaku) + "&statusDiscussion=" + btoa(statusDiscussion) + "&agreementHolder=" + btoa(agreementHolder) + "&rangeTanggalTtd=" + btoa(rangeTanggalTtd) + "&rangeTanggalBerakhir=" + btoa(rangeTanggalBerakhir) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + isDraftSelected + "&isG2G=" + isG2GSelected + "&isStrategicPartnerShip=" + isStrategicPartnerShipSelected + "&isBdCoreBusiness=" + isBdCoreBusinessSelected + "&isGreenBusiness=" + isGreenBusinessSelected;
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.JenisPerjanjianEncode = btoa(jenisPerjanjian);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.KawasanMitraEncode = btoa(kawasanMitra);
    model.StatusBerlakuEncode = btoa(statusBerlaku);
    model.StatusDiscussionEncode = btoa(statusDiscussion);
    model.AgreementHolderEncode = btoa(agreementHolder);
    model.RangeTanggalTtdEncode = btoa(rangeTanggalTtd);
    model.RangeTanggalBerakhirEncode = btoa(rangeTanggalBerakhir);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.IsGtg = isG2GSelected;
    model.IsBdCoreBusinessDecode = isBdCoreBusinessSelected;
    model.IsStrategicPartnerShipDecode = isStrategicPartnerShipSelected;
    model.IsGreenBusinessDecode = isGreenBusinessSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data.StatusBerlaku.forEach(statusBerlakuLoop);
            data.DiscussionStatus.forEach(discussionStatusLoop);
            data.AgreementHolder.forEach(agreementHolderLoop);
            $('.counter-perjanjian').text('(' + data.Perjanjian + ')');
            $('.counter-negaraMitra').text('(' + data.NegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});
$('#ddlStatusDiscussion').on('select2:select', function (e) {
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
    var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
    var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
    var agreementHolder = $("#ddlAgreementHolder").val().join('_');
    var rangeTanggalTtd = $("#rangeTanggalTtd").val();
    var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var isG2GSelected = $("#checkBoxIsG2G").prop("checked");
    var isStrategicPartnerShipSelected = $("#checkBoxIsStrategicPartnerShip").prop("checked");
    var isBdCoreBusinessSelected = $("#checkBoxIsBdCoreBusiness").prop("checked");
    var isGreenBusinessSelected = $("#checkBoxIsGreenBusiness").prop("checked");
    var searchValue = $('#searchInput').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&jenisPerjanjian=" + btoa(jenisPerjanjian) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&statusBerlaku=" + btoa(statusBerlaku) + "&statusDiscussion=" + btoa(statusDiscussion) + "&agreementHolder=" + btoa(agreementHolder) + "&rangeTanggalTtd=" + btoa(rangeTanggalTtd) + "&rangeTanggalBerakhir=" + btoa(rangeTanggalBerakhir) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + isDraftSelected + "&isG2G=" + isG2GSelected + "&isStrategicPartnerShip=" + isStrategicPartnerShipSelected + "&isBdCoreBusiness=" + isBdCoreBusinessSelected + "&isGreenBusiness=" + isGreenBusinessSelected;
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.JenisPerjanjianEncode = btoa(jenisPerjanjian);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.KawasanMitraEncode = btoa(kawasanMitra);
    model.StatusBerlakuEncode = btoa(statusBerlaku);
    model.StatusDiscussionEncode = btoa(statusDiscussion);
    model.AgreementHolderEncode = btoa(agreementHolder);
    model.RangeTanggalTtdEncode = btoa(rangeTanggalTtd);
    model.RangeTanggalBerakhirEncode = btoa(rangeTanggalBerakhir);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.IsGtg = isG2GSelected;
    model.IsBdCoreBusinessDecode = isBdCoreBusinessSelected;
    model.IsStrategicPartnerShipDecode = isStrategicPartnerShipSelected;
    model.IsGreenBusinessDecode = isGreenBusinessSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data.StatusBerlaku.forEach(statusBerlakuLoop);
            data.DiscussionStatus.forEach(discussionStatusLoop);
            data.AgreementHolder.forEach(agreementHolderLoop);
            $('.counter-perjanjian').text('(' + data.Perjanjian + ')');
            $('.counter-negaraMitra').text('(' + data.NegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});
$('#ddlStatusDiscussion').on("select2:unselect", function (e) {
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
    var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
    var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
    var agreementHolder = $("#ddlAgreementHolder").val().join('_');
    var rangeTanggalTtd = $("#rangeTanggalTtd").val();
    var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var isG2GSelected = $("#checkBoxIsG2G").prop("checked");
    var isStrategicPartnerShipSelected = $("#checkBoxIsStrategicPartnerShip").prop("checked");
    var isBdCoreBusinessSelected = $("#checkBoxIsBdCoreBusiness").prop("checked");
    var isGreenBusinessSelected = $("#checkBoxIsGreenBusiness").prop("checked");
    var searchValue = $('#searchInput').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&jenisPerjanjian=" + btoa(jenisPerjanjian) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&statusBerlaku=" + btoa(statusBerlaku) + "&statusDiscussion=" + btoa(statusDiscussion) + "&agreementHolder=" + btoa(agreementHolder) + "&rangeTanggalTtd=" + btoa(rangeTanggalTtd) + "&rangeTanggalBerakhir=" + btoa(rangeTanggalBerakhir) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + isDraftSelected + "&isG2G=" + isG2GSelected + "&isStrategicPartnerShip=" + isStrategicPartnerShipSelected + "&isBdCoreBusiness=" + isBdCoreBusinessSelected + "&isGreenBusiness=" + isGreenBusinessSelected;
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.JenisPerjanjianEncode = btoa(jenisPerjanjian);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.KawasanMitraEncode = btoa(kawasanMitra);
    model.StatusBerlakuEncode = btoa(statusBerlaku);
    model.StatusDiscussionEncode = btoa(statusDiscussion);
    model.AgreementHolderEncode = btoa(agreementHolder);
    model.RangeTanggalTtdEncode = btoa(rangeTanggalTtd);
    model.RangeTanggalBerakhirEncode = btoa(rangeTanggalBerakhir);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.IsGtg = isG2GSelected;
    model.IsBdCoreBusinessDecode = isBdCoreBusinessSelected;
    model.IsStrategicPartnerShipDecode = isStrategicPartnerShipSelected;
    model.IsGreenBusinessDecode = isGreenBusinessSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data.StatusBerlaku.forEach(statusBerlakuLoop);
            data.DiscussionStatus.forEach(discussionStatusLoop);
            data.AgreementHolder.forEach(agreementHolderLoop);
            $('.counter-perjanjian').text('(' + data.Perjanjian + ')');
            $('.counter-negaraMitra').text('(' + data.NegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});
$('#ddlAgreementHolder').on('select2:select', function (e) {
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
    var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
    var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
    var agreementHolder = $("#ddlAgreementHolder").val().join('_');
    var rangeTanggalTtd = $("#rangeTanggalTtd").val();
    var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var isG2GSelected = $("#checkBoxIsG2G").prop("checked");
    var isStrategicPartnerShipSelected = $("#checkBoxIsStrategicPartnerShip").prop("checked");
    var isBdCoreBusinessSelected = $("#checkBoxIsBdCoreBusiness").prop("checked");
    var isGreenBusinessSelected = $("#checkBoxIsGreenBusiness").prop("checked");
    var searchValue = $('#searchInput').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&jenisPerjanjian=" + btoa(jenisPerjanjian) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&statusBerlaku=" + btoa(statusBerlaku) + "&statusDiscussion=" + btoa(statusDiscussion) + "&agreementHolder=" + btoa(agreementHolder) + "&rangeTanggalTtd=" + btoa(rangeTanggalTtd) + "&rangeTanggalBerakhir=" + btoa(rangeTanggalBerakhir) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + isDraftSelected + "&isG2G=" + isG2GSelected + "&isStrategicPartnerShip=" + isStrategicPartnerShipSelected + "&isBdCoreBusiness=" + isBdCoreBusinessSelected + "&isGreenBusiness=" + isGreenBusinessSelected;
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.JenisPerjanjianEncode = btoa(jenisPerjanjian);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.KawasanMitraEncode = btoa(kawasanMitra);
    model.StatusBerlakuEncode = btoa(statusBerlaku);
    model.StatusDiscussionEncode = btoa(statusDiscussion);
    model.AgreementHolderEncode = btoa(agreementHolder);
    model.RangeTanggalTtdEncode = btoa(rangeTanggalTtd);
    model.RangeTanggalBerakhirEncode = btoa(rangeTanggalBerakhir);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.IsGtg = isG2GSelected;
    model.IsBdCoreBusinessDecode = isBdCoreBusinessSelected;
    model.IsStrategicPartnerShipDecode = isStrategicPartnerShipSelected;
    model.IsGreenBusinessDecode = isGreenBusinessSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data.StatusBerlaku.forEach(statusBerlakuLoop);
            data.DiscussionStatus.forEach(discussionStatusLoop);
            data.AgreementHolder.forEach(agreementHolderLoop);
            $('.counter-perjanjian').text('(' + data.Perjanjian + ')');
            $('.counter-negaraMitra').text('(' + data.NegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});
$('#ddlAgreementHolder').on("select2:unselect", function (e) {
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
    var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
    var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
    var agreementHolder = $("#ddlAgreementHolder").val().join('_');
    var rangeTanggalTtd = $("#rangeTanggalTtd").val();
    var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var isG2GSelected = $("#checkBoxIsG2G").prop("checked");
    var isStrategicPartnerShipSelected = $("#checkBoxIsStrategicPartnerShip").prop("checked");
    var isBdCoreBusinessSelected = $("#checkBoxIsBdCoreBusiness").prop("checked");
    var isGreenBusinessSelected = $("#checkBoxIsGreenBusiness").prop("checked");
    var searchValue = $('#searchInput').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&jenisPerjanjian=" + btoa(jenisPerjanjian) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&statusBerlaku=" + btoa(statusBerlaku) + "&statusDiscussion=" + btoa(statusDiscussion) + "&agreementHolder=" + btoa(agreementHolder) + "&rangeTanggalTtd=" + btoa(rangeTanggalTtd) + "&rangeTanggalBerakhir=" + btoa(rangeTanggalBerakhir) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + isDraftSelected + "&isG2G=" + isG2GSelected + "&isStrategicPartnerShip=" + isStrategicPartnerShipSelected + "&isBdCoreBusiness=" + isBdCoreBusinessSelected + "&isGreenBusiness=" + isGreenBusinessSelected;
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.JenisPerjanjianEncode = btoa(jenisPerjanjian);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.KawasanMitraEncode = btoa(kawasanMitra);
    model.StatusBerlakuEncode = btoa(statusBerlaku);
    model.StatusDiscussionEncode = btoa(statusDiscussion);
    model.AgreementHolderEncode = btoa(agreementHolder);
    model.RangeTanggalTtdEncode = btoa(rangeTanggalTtd);
    model.RangeTanggalBerakhirEncode = btoa(rangeTanggalBerakhir);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.IsGtg = isG2GSelected;
    model.IsBdCoreBusinessDecode = isBdCoreBusinessSelected;
    model.IsStrategicPartnerShipDecode = isStrategicPartnerShipSelected;
    model.IsGreenBusinessDecode = isGreenBusinessSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data.StatusBerlaku.forEach(statusBerlakuLoop);
            data.DiscussionStatus.forEach(discussionStatusLoop);
            data.AgreementHolder.forEach(agreementHolderLoop);
            $('.counter-perjanjian').text('(' + data.Perjanjian + ')');
            $('.counter-negaraMitra').text('(' + data.NegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});

function statusBerlakuLoop(value, index, array) {
    $('.counter-statusBerlaku-' + value.Id).text('(' + value.Count + ')');
};
function discussionStatusLoop(value, index, array) {
    $('.counter-discussionStatus-' + value.Id).text('(' + value.Count + ')');
};
function agreementHolderLoop(value, index, array) {
    $('.counter-agreementHolder-' + value.Id).text('(' + value.Count + ')');
};

$("#rangeTanggalTtd").change(function () {
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
    var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
    var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
    var agreementHolder = $("#ddlAgreementHolder").val().join('_');
    var rangeTanggalTtd = $("#rangeTanggalTtd").val();
    var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var isG2GSelected = $("#checkBoxIsG2G").prop("checked");
    var isStrategicPartnerShipSelected = $("#checkBoxIsStrategicPartnerShip").prop("checked");
    var isBdCoreBusinessSelected = $("#checkBoxIsBdCoreBusiness").prop("checked");
    var isGreenBusinessSelected = $("#checkBoxIsGreenBusiness").prop("checked");
    var searchValue = $('#searchInput').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&jenisPerjanjian=" + btoa(jenisPerjanjian) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&statusBerlaku=" + btoa(statusBerlaku) + "&statusDiscussion=" + btoa(statusDiscussion) + "&agreementHolder=" + btoa(agreementHolder) + "&rangeTanggalTtd=" + btoa(rangeTanggalTtd) + "&rangeTanggalBerakhir=" + btoa(rangeTanggalBerakhir) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + isDraftSelected + "&isG2G=" + isG2GSelected + "&isStrategicPartnerShip=" + isStrategicPartnerShipSelected + "&isBdCoreBusiness=" + isBdCoreBusinessSelected + "&isGreenBusiness=" + isGreenBusinessSelected;
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.JenisPerjanjianEncode = btoa(jenisPerjanjian);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.KawasanMitraEncode = btoa(kawasanMitra);
    model.StatusBerlakuEncode = btoa(statusBerlaku);
    model.StatusDiscussionEncode = btoa(statusDiscussion);
    model.AgreementHolderEncode = btoa(agreementHolder);
    model.RangeTanggalTtdEncode = btoa(rangeTanggalTtd);
    model.RangeTanggalBerakhirEncode = btoa(rangeTanggalBerakhir);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.IsGtg = isG2GSelected;
    model.IsBdCoreBusinessDecode = isBdCoreBusinessSelected;
    model.IsStrategicPartnerShipDecode = isStrategicPartnerShipSelected;
    model.IsGreenBusinessDecode = isGreenBusinessSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data.StatusBerlaku.forEach(statusBerlakuLoop);
            data.DiscussionStatus.forEach(discussionStatusLoop);
            data.AgreementHolder.forEach(agreementHolderLoop);
            $('.counter-perjanjian').text('(' + data.Perjanjian + ')');
            $('.counter-negaraMitra').text('(' + data.NegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});
$("#rangeTanggalBerakhir").change(function () {
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
    var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
    var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
    var agreementHolder = $("#ddlAgreementHolder").val().join('_');
    var rangeTanggalTtd = $("#rangeTanggalTtd").val();
    var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var isG2GSelected = $("#checkBoxIsG2G").prop("checked");
    var isStrategicPartnerShipSelected = $("#checkBoxIsStrategicPartnerShip").prop("checked");
    var isBdCoreBusinessSelected = $("#checkBoxIsBdCoreBusiness").prop("checked");
    var isGreenBusinessSelected = $("#checkBoxIsGreenBusiness").prop("checked");
    var searchValue = $('#searchInput').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&jenisPerjanjian=" + btoa(jenisPerjanjian) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&statusBerlaku=" + btoa(statusBerlaku) + "&statusDiscussion=" + btoa(statusDiscussion) + "&agreementHolder=" + btoa(agreementHolder) + "&rangeTanggalTtd=" + btoa(rangeTanggalTtd) + "&rangeTanggalBerakhir=" + btoa(rangeTanggalBerakhir) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + isDraftSelected + "&isG2G=" + isG2GSelected + "&isStrategicPartnerShip=" + isStrategicPartnerShipSelected + "&isBdCoreBusiness=" + isBdCoreBusinessSelected + "&isGreenBusiness=" + isGreenBusinessSelected;
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.JenisPerjanjianEncode = btoa(jenisPerjanjian);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.KawasanMitraEncode = btoa(kawasanMitra);
    model.StatusBerlakuEncode = btoa(statusBerlaku);
    model.StatusDiscussionEncode = btoa(statusDiscussion);
    model.AgreementHolderEncode = btoa(agreementHolder);
    model.RangeTanggalTtdEncode = btoa(rangeTanggalTtd);
    model.RangeTanggalBerakhirEncode = btoa(rangeTanggalBerakhir);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.IsGtg = isG2GSelected;
    model.IsBdCoreBusinessDecode = isBdCoreBusinessSelected;
    model.IsStrategicPartnerShipDecode = isStrategicPartnerShipSelected;
    model.IsGreenBusinessDecode = isGreenBusinessSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data.StatusBerlaku.forEach(statusBerlakuLoop);
            data.DiscussionStatus.forEach(discussionStatusLoop);
            data.AgreementHolder.forEach(agreementHolderLoop);
            $('.counter-perjanjian').text('(' + data.Perjanjian + ')');
            $('.counter-negaraMitra').text('(' + data.NegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});
$("#rangeCreateDate").change(function () {
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
    var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
    var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
    var agreementHolder = $("#ddlAgreementHolder").val().join('_');
    var rangeTanggalTtd = $("#rangeTanggalTtd").val();
    var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var isG2GSelected = $("#checkBoxIsG2G").prop("checked");
    var isStrategicPartnerShipSelected = $("#checkBoxIsStrategicPartnerShip").prop("checked");
    var isBdCoreBusinessSelected = $("#checkBoxIsBdCoreBusiness").prop("checked");
    var isGreenBusinessSelected = $("#checkBoxIsGreenBusiness").prop("checked");
    var searchValue = $('#searchInput').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&jenisPerjanjian=" + btoa(jenisPerjanjian) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&statusBerlaku=" + btoa(statusBerlaku) + "&statusDiscussion=" + btoa(statusDiscussion) + "&agreementHolder=" + btoa(agreementHolder) + "&rangeTanggalTtd=" + btoa(rangeTanggalTtd) + "&rangeTanggalBerakhir=" + btoa(rangeTanggalBerakhir) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + isDraftSelected + "&isG2G=" + isG2GSelected + "&isStrategicPartnerShip=" + isStrategicPartnerShipSelected + "&isBdCoreBusiness=" + isBdCoreBusinessSelected + "&isGreenBusiness=" + isGreenBusinessSelected;
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.JenisPerjanjianEncode = btoa(jenisPerjanjian);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.KawasanMitraEncode = btoa(kawasanMitra);
    model.StatusBerlakuEncode = btoa(statusBerlaku);
    model.StatusDiscussionEncode = btoa(statusDiscussion);
    model.AgreementHolderEncode = btoa(agreementHolder);
    model.RangeTanggalTtdEncode = btoa(rangeTanggalTtd);
    model.RangeTanggalBerakhirEncode = btoa(rangeTanggalBerakhir);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.IsGtg = isG2GSelected;
    model.IsBdCoreBusinessDecode = isBdCoreBusinessSelected;
    model.IsStrategicPartnerShipDecode = isStrategicPartnerShipSelected;
    model.IsGreenBusinessDecode = isGreenBusinessSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data.StatusBerlaku.forEach(statusBerlakuLoop);
            data.DiscussionStatus.forEach(discussionStatusLoop);
            data.AgreementHolder.forEach(agreementHolderLoop);
            $('.counter-perjanjian').text('(' + data.Perjanjian + ')');
            $('.counter-negaraMitra').text('(' + data.NegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});

//Event Filter Draft
$('#checkBoxIsDraft, #checkBoxIsG2G, #checkBoxIsStrategicPartnerShip, #checkBoxIsBdCoreBusiness, #checkBoxIsGreenBusiness ').change(function () {

    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
    var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
    var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
    var agreementHolder = $("#ddlAgreementHolder").val().join('_');
    var rangeTanggalTtd = $("#rangeTanggalTtd").val();
    var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var isG2GSelected = $("#checkBoxIsG2G").prop("checked");
    var isStrategicPartnerShipSelected = $("#checkBoxIsStrategicPartnerShip").prop("checked");
    var isBdCoreBusinessSelected = $("#checkBoxIsBdCoreBusiness").prop("checked");
    var isGreenBusinessSelected = $("#checkBoxIsGreenBusiness").prop("checked");
    var searchValue = $('#searchInput').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&jenisPerjanjian=" + btoa(jenisPerjanjian) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&statusBerlaku=" + btoa(statusBerlaku) + "&statusDiscussion=" + btoa(statusDiscussion) + "&agreementHolder=" + btoa(agreementHolder) + "&rangeTanggalTtd=" + btoa(rangeTanggalTtd) + "&rangeTanggalBerakhir=" + btoa(rangeTanggalBerakhir) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + isDraftSelected + "&isG2G=" + isG2GSelected + "&isStrategicPartnerShip=" + isStrategicPartnerShipSelected + "&isBdCoreBusiness=" + isBdCoreBusinessSelected + "&isGreenBusiness=" + isGreenBusinessSelected;
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.JenisPerjanjianEncode = btoa(jenisPerjanjian);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.KawasanMitraEncode = btoa(kawasanMitra);
    model.StatusBerlakuEncode = btoa(statusBerlaku);
    model.StatusDiscussionEncode = btoa(statusDiscussion);
    model.AgreementHolderEncode = btoa(agreementHolder);
    model.RangeTanggalTtdEncode = btoa(rangeTanggalTtd);
    model.RangeTanggalBerakhirEncode = btoa(rangeTanggalBerakhir);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.IsGtg = isG2GSelected;
    model.IsBdCoreBusinessDecode = isBdCoreBusinessSelected;
    model.IsStrategicPartnerShipDecode = isStrategicPartnerShipSelected;
    model.IsGreenBusinessDecode = isGreenBusinessSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data.StatusBerlaku.forEach(statusBerlakuLoop);
            data.DiscussionStatus.forEach(discussionStatusLoop);
            data.AgreementHolder.forEach(agreementHolderLoop);
            $('.counter-perjanjian').text('(' + data.Perjanjian + ')');
            $('.counter-negaraMitra').text('(' + data.NegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});

//Event Tanggal TTD
$('input[name="RangeTanggalTtd"]').on('apply.daterangepicker', function (ev, picker) {
    console.log(ev);
    $(this).val(picker.startDate.format('MM/DD/YYYY') + ' - ' + picker.endDate.format('MM/DD/YYYY'));
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
    var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
    var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
    var agreementHolder = $("#ddlAgreementHolder").val().join('_');
    var rangeTanggalTtd = $("#rangeTanggalTtd").val();
    var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var isG2GSelected = $("#checkBoxIsG2G").prop("checked");
    var isStrategicPartnerShipSelected = $("#checkBoxIsStrategicPartnerShip").prop("checked");
    var isBdCoreBusinessSelected = $("#checkBoxIsBdCoreBusiness").prop("checked");
    var isGreenBusinessSelected = $("#checkBoxIsGreenBusiness").prop("checked");
    var searchValue = $('#searchInput').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&jenisPerjanjian=" + btoa(jenisPerjanjian) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&statusBerlaku=" + btoa(statusBerlaku) + "&statusDiscussion=" + btoa(statusDiscussion) + "&agreementHolder=" + btoa(agreementHolder) + "&rangeTanggalTtd=" + btoa(rangeTanggalTtd) + "&rangeTanggalBerakhir=" + btoa(rangeTanggalBerakhir) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + isDraftSelected + "&isG2G=" + isG2GSelected + "&isStrategicPartnerShip=" + isStrategicPartnerShipSelected + "&isBdCoreBusiness=" + isBdCoreBusinessSelected + "&isGreenBusiness=" + isGreenBusinessSelected;
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.JenisPerjanjianEncode = btoa(jenisPerjanjian);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.KawasanMitraEncode = btoa(kawasanMitra);
    model.StatusBerlakuEncode = btoa(statusBerlaku);
    model.StatusDiscussionEncode = btoa(statusDiscussion);
    model.AgreementHolderEncode = btoa(agreementHolder);
    model.RangeTanggalTtdEncode = btoa(rangeTanggalTtd);
    model.RangeTanggalBerakhirEncode = btoa(rangeTanggalBerakhir);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.IsGtg = isG2GSelected;
    model.IsBdCoreBusinessDecode = isBdCoreBusinessSelected;
    model.IsStrategicPartnerShipDecode = isStrategicPartnerShipSelected;
    model.IsGreenBusinessDecode = isGreenBusinessSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data.StatusBerlaku.forEach(statusBerlakuLoop);
            data.DiscussionStatus.forEach(discussionStatusLoop);
            data.AgreementHolder.forEach(agreementHolderLoop);
            $('.counter-perjanjian').text('(' + data.Perjanjian + ')');
            $('.counter-negaraMitra').text('(' + data.NegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});
$('input[name="RangeTanggalTtd"]').on('cancel.daterangepicker', function (ev, picker) {
    $(this).val('');
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
    var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
    var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
    var agreementHolder = $("#ddlAgreementHolder").val().join('_');
    var rangeTanggalTtd = $("#rangeTanggalTtd").val();
    var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var isG2GSelected = $("#checkBoxIsG2G").prop("checked");
    var isStrategicPartnerShipSelected = $("#checkBoxIsStrategicPartnerShip").prop("checked");
    var isBdCoreBusinessSelected = $("#checkBoxIsBdCoreBusiness").prop("checked");
    var isGreenBusinessSelected = $("#checkBoxIsGreenBusiness").prop("checked");
    var searchValue = $('#searchInput').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&jenisPerjanjian=" + btoa(jenisPerjanjian) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&statusBerlaku=" + btoa(statusBerlaku) + "&statusDiscussion=" + btoa(statusDiscussion) + "&agreementHolder=" + btoa(agreementHolder) + "&rangeTanggalTtd=" + btoa(rangeTanggalTtd) + "&rangeTanggalBerakhir=" + btoa(rangeTanggalBerakhir) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + isDraftSelected + "&isG2G=" + isG2GSelected + "&isStrategicPartnerShip=" + isStrategicPartnerShipSelected + "&isBdCoreBusiness=" + isBdCoreBusinessSelected + "&isGreenBusiness=" + isGreenBusinessSelected;
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.JenisPerjanjianEncode = btoa(jenisPerjanjian);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.KawasanMitraEncode = btoa(kawasanMitra);
    model.StatusBerlakuEncode = btoa(statusBerlaku);
    model.StatusDiscussionEncode = btoa(statusDiscussion);
    model.AgreementHolderEncode = btoa(agreementHolder);
    model.RangeTanggalTtdEncode = btoa(rangeTanggalTtd);
    model.RangeTanggalBerakhirEncode = btoa(rangeTanggalBerakhir);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.IsGtg = isG2GSelected;
    model.IsBdCoreBusinessDecode = isBdCoreBusinessSelected;
    model.IsStrategicPartnerShipDecode = isStrategicPartnerShipSelected;
    model.IsGreenBusinessDecode = isGreenBusinessSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data.StatusBerlaku.forEach(statusBerlakuLoop);
            data.DiscussionStatus.forEach(discussionStatusLoop);
            data.AgreementHolder.forEach(agreementHolderLoop);
            $('.counter-perjanjian').text('(' + data.Perjanjian + ')');
            $('.counter-negaraMitra').text('(' + data.NegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});

//Event Tanggal Berakhir
$('input[name="RangeTanggalBerakhir"]').on('apply.daterangepicker', function (ev, picker) {
    $(this).val(picker.startDate.format('MM/DD/YYYY') + ' - ' + picker.endDate.format('MM/DD/YYYY'));
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
    var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
    var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
    var agreementHolder = $("#ddlAgreementHolder").val().join('_');
    var rangeTanggalTtd = $("#rangeTanggalTtd").val();
    var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var isG2GSelected = $("#checkBoxIsG2G").prop("checked");
    var isStrategicPartnerShipSelected = $("#checkBoxIsStrategicPartnerShip").prop("checked");
    var isBdCoreBusinessSelected = $("#checkBoxIsBdCoreBusiness").prop("checked");
    var isGreenBusinessSelected = $("#checkBoxIsGreenBusiness").prop("checked");
    var searchValue = $('#searchInput').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&jenisPerjanjian=" + btoa(jenisPerjanjian) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&statusBerlaku=" + btoa(statusBerlaku) + "&statusDiscussion=" + btoa(statusDiscussion) + "&agreementHolder=" + btoa(agreementHolder) + "&rangeTanggalTtd=" + btoa(rangeTanggalTtd) + "&rangeTanggalBerakhir=" + btoa(rangeTanggalBerakhir) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + isDraftSelected + "&isG2G=" + isG2GSelected + "&isStrategicPartnerShip=" + isStrategicPartnerShipSelected + "&isBdCoreBusiness=" + isBdCoreBusinessSelected + "&isGreenBusiness=" + isGreenBusinessSelected;
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.JenisPerjanjianEncode = btoa(jenisPerjanjian);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.KawasanMitraEncode = btoa(kawasanMitra);
    model.StatusBerlakuEncode = btoa(statusBerlaku);
    model.StatusDiscussionEncode = btoa(statusDiscussion);
    model.AgreementHolderEncode = btoa(agreementHolder);
    model.RangeTanggalTtdEncode = btoa(rangeTanggalTtd);
    model.RangeTanggalBerakhirEncode = btoa(rangeTanggalBerakhir);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.IsGtg = isG2GSelected;
    model.IsBdCoreBusinessDecode = isBdCoreBusinessSelected;
    model.IsStrategicPartnerShipDecode = isStrategicPartnerShipSelected;
    model.IsGreenBusinessDecode = isGreenBusinessSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data.StatusBerlaku.forEach(statusBerlakuLoop);
            data.DiscussionStatus.forEach(discussionStatusLoop);
            data.AgreementHolder.forEach(agreementHolderLoop);
            $('.counter-perjanjian').text('(' + data.Perjanjian + ')');
            $('.counter-negaraMitra').text('(' + data.NegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});
$('input[name="RangeTanggalBerakhir"]').on('cancel.daterangepicker', function (ev, picker) {
    $(this).val('');
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
    var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
    var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
    var agreementHolder = $("#ddlAgreementHolder").val().join('_');
    var rangeTanggalTtd = $("#rangeTanggalTtd").val();
    var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var isG2GSelected = $("#checkBoxIsG2G").prop("checked");
    var isStrategicPartnerShipSelected = $("#checkBoxIsStrategicPartnerShip").prop("checked");
    var isBdCoreBusinessSelected = $("#checkBoxIsBdCoreBusiness").prop("checked");
    var isGreenBusinessSelected = $("#checkBoxIsGreenBusiness").prop("checked");
    var searchValue = $('#searchInput').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&jenisPerjanjian=" + btoa(jenisPerjanjian) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&statusBerlaku=" + btoa(statusBerlaku) + "&statusDiscussion=" + btoa(statusDiscussion) + "&agreementHolder=" + btoa(agreementHolder) + "&rangeTanggalTtd=" + btoa(rangeTanggalTtd) + "&rangeTanggalBerakhir=" + btoa(rangeTanggalBerakhir) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + isDraftSelected + "&isG2G=" + isG2GSelected + "&isStrategicPartnerShip=" + isStrategicPartnerShipSelected + "&isBdCoreBusiness=" + isBdCoreBusinessSelected + "&isGreenBusiness=" + isGreenBusinessSelected;
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.JenisPerjanjianEncode = btoa(jenisPerjanjian);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.KawasanMitraEncode = btoa(kawasanMitra);
    model.StatusBerlakuEncode = btoa(statusBerlaku);
    model.StatusDiscussionEncode = btoa(statusDiscussion);
    model.AgreementHolderEncode = btoa(agreementHolder);
    model.RangeTanggalTtdEncode = btoa(rangeTanggalTtd);
    model.RangeTanggalBerakhirEncode = btoa(rangeTanggalBerakhir);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.IsGtg = isG2GSelected;
    model.IsBdCoreBusinessDecode = isBdCoreBusinessSelected;
    model.IsStrategicPartnerShipDecode = isStrategicPartnerShipSelected;
    model.IsGreenBusinessDecode = isGreenBusinessSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data.StatusBerlaku.forEach(statusBerlakuLoop);
            data.DiscussionStatus.forEach(discussionStatusLoop);
            data.AgreementHolder.forEach(agreementHolderLoop);
            $('.counter-perjanjian').text('(' + data.Perjanjian + ')');
            $('.counter-negaraMitra').text('(' + data.NegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});

//Event DateRange
$('input[name="RangeCreateDate"]').on('apply.daterangepicker', function (ev, picker) {
    $(this).val(picker.startDate.format('MM/DD/YYYY') + ' - ' + picker.endDate.format('MM/DD/YYYY'));
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
    var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
    var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
    var agreementHolder = $("#ddlAgreementHolder").val().join('_');
    var rangeTanggalTtd = $("#rangeTanggalTtd").val();
    var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var isG2GSelected = $("#checkBoxIsG2G").prop("checked");
    var isStrategicPartnerShipSelected = $("#checkBoxIsStrategicPartnerShip").prop("checked");
    var isBdCoreBusinessSelected = $("#checkBoxIsBdCoreBusiness").prop("checked");
    var isGreenBusinessSelected = $("#checkBoxIsGreenBusiness").prop("checked");
    var searchValue = $('#searchInput').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&jenisPerjanjian=" + btoa(jenisPerjanjian) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&statusBerlaku=" + btoa(statusBerlaku) + "&statusDiscussion=" + btoa(statusDiscussion) + "&agreementHolder=" + btoa(agreementHolder) + "&rangeTanggalTtd=" + btoa(rangeTanggalTtd) + "&rangeTanggalBerakhir=" + btoa(rangeTanggalBerakhir) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + isDraftSelected + "&isG2G=" + isG2GSelected + "&isStrategicPartnerShip=" + isStrategicPartnerShipSelected + "&isBdCoreBusiness=" + isBdCoreBusinessSelected + "&isGreenBusiness=" + isGreenBusinessSelected;
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.JenisPerjanjianEncode = btoa(jenisPerjanjian);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.KawasanMitraEncode = btoa(kawasanMitra);
    model.StatusBerlakuEncode = btoa(statusBerlaku);
    model.StatusDiscussionEncode = btoa(statusDiscussion);
    model.AgreementHolderEncode = btoa(agreementHolder);
    model.RangeTanggalTtdEncode = btoa(rangeTanggalTtd);
    model.RangeTanggalBerakhirEncode = btoa(rangeTanggalBerakhir);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.IsGtg = isG2GSelected;
    model.IsBdCoreBusinessDecode = isBdCoreBusinessSelected;
    model.IsStrategicPartnerShipDecode = isStrategicPartnerShipSelected;
    model.IsGreenBusinessDecode = isGreenBusinessSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data.StatusBerlaku.forEach(statusBerlakuLoop);
            data.DiscussionStatus.forEach(discussionStatusLoop);
            data.AgreementHolder.forEach(agreementHolderLoop);
            $('.counter-perjanjian').text('(' + data.Perjanjian + ')');
            $('.counter-negaraMitra').text('(' + data.NegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});
$('input[name="RangeCreateDate"]').on('cancel.daterangepicker', function (ev, picker) {
    $(this).val('');
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
    var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
    var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
    var agreementHolder = $("#ddlAgreementHolder").val().join('_');
    var rangeTanggalTtd = $("#rangeTanggalTtd").val();
    var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var searchValue = $('#searchInput').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&jenisPerjanjian=" + btoa(jenisPerjanjian) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&statusBerlaku=" + btoa(statusBerlaku) + "&statusDiscussion=" + btoa(statusDiscussion) + "&agreementHolder=" + btoa(agreementHolder) + "&rangeTanggalTtd=" + btoa(rangeTanggalTtd) + "&rangeTanggalBerakhir=" + btoa(rangeTanggalBerakhir) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + isDraftSelected + "&isG2G=" + isG2GSelected + "&isStrategicPartnerShip=" + isStrategicPartnerShipSelected + "&isBdCoreBusiness=" + isBdCoreBusinessSelected + "&isGreenBusiness=" + isGreenBusinessSelected;
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.JenisPerjanjianEncode = btoa(jenisPerjanjian);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.KawasanMitraEncode = btoa(kawasanMitra);
    model.StatusBerlakuEncode = btoa(statusBerlaku);
    model.StatusDiscussionEncode = btoa(statusDiscussion);
    model.AgreementHolderEncode = btoa(agreementHolder);
    model.RangeTanggalTtdEncode = btoa(rangeTanggalTtd);
    model.RangeTanggalBerakhirEncode = btoa(rangeTanggalBerakhir);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
    model.IsDraft = isDraftSelected;
    model.IsGtg = isG2GSelected;
    model.IsBdCoreBusinessDecode = isBdCoreBusinessSelected;
    model.IsStrategicPartnerShipDecode = isStrategicPartnerShipSelected;
    model.IsGreenBusinessDecode = isGreenBusinessSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data.StatusBerlaku.forEach(statusBerlakuLoop);
            data.DiscussionStatus.forEach(discussionStatusLoop);
            data.AgreementHolder.forEach(agreementHolderLoop);
            $('.counter-perjanjian').text('(' + data.Perjanjian + ')');
            $('.counter-negaraMitra').text('(' + data.NegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
});

//Clear Tanggal TTD
function clearDateRange() {
    $("#rangeTanggalTtd").val("");
    $("#rangeTanggalBerakhir").val("");
    $("#rangeCreateDate").val("");
    var entitasPertamina = $("#ddlEntitasPertamina").val().join('_');
    var streamBusiness = $("#ddlStreamBusiness").val().join('_');
    var jenisPerjanjian = $("#ddlJenisPerjanjian").val().join('_');
    var negaraMitra = $("#ddlNegaraMitra").val().join('_');
    var kawasanMitra = $("#ddlKawasanMitra").val().join('_');
    var statusBerlaku = $("#ddlStatusBerlaku").val().join('_');
    var statusDiscussion = $("#ddlStatusDiscussion").val().join('_');
    var agreementHolder = $("#ddlAgreementHolder").val().join('_');
    var rangeTanggalTtd = $("#rangeTanggalTtd").val();
    var rangeTanggalBerakhir = $("#rangeTanggalBerakhir").val();
    var rangeCreateDate = $("#rangeCreateDate").val();
    var isDraftSelected = $("#checkBoxIsDraft").prop("checked");
    var isG2GSelected = $("#checkBoxIsG2G").prop("checked");
    var isStrategicPartnerShipSelected = $("#checkBoxIsStrategicPartnerShip").prop("checked");
    var isBdCoreBusinessSelected = $("#checkBoxIsBdCoreBusiness").prop("checked");
    var isGreenBusinessSelected = $("#checkBoxIsGreenBusiness").prop("checked");
    var searchValue = $('#searchInput').val();

    var uriAll = "?entitasPertamina=" + btoa(entitasPertamina) + "&jenisPerjanjian=" + btoa(jenisPerjanjian) + "&streamBusiness=" + btoa(streamBusiness) + "&negaraMitra=" + btoa(negaraMitra) + "&kawasanMitra=" + btoa(kawasanMitra) + "&statusBerlaku=" + btoa(statusBerlaku) + "&statusDiscussion=" + btoa(statusDiscussion) + "&agreementHolder=" + btoa(agreementHolder) + "&rangeTanggalTtd=" + btoa(rangeTanggalTtd) + "&rangeTanggalBerakhir=" + btoa(rangeTanggalBerakhir) + "&rangeCreateDate=" + btoa(rangeCreateDate) + "&isDraft=" + isDraftSelected + "&isG2G=" + isG2GSelected + "&isStrategicPartnerShip=" + isStrategicPartnerShipSelected + "&isBdCoreBusiness=" + isBdCoreBusinessSelected + "&isGreenBusiness=" + isGreenBusinessSelected;
    dt.ajax.url(param.dataGridEvent + uriAll).load();

    var model = {}
    model.EntitasPertaminaEncode = btoa(entitasPertamina);
    model.StreamBusinessEncode = btoa(streamBusiness);
    model.JenisPerjanjianEncode = btoa(jenisPerjanjian);
    model.NegaraMitraEncode = btoa(negaraMitra);
    model.KawasanMitraEncode = btoa(kawasanMitra);
    model.StatusBerlakuEncode = btoa(statusBerlaku);
    model.StatusDiscussionEncode = btoa(statusDiscussion);
    model.AgreementHolderEncode = btoa(agreementHolder);
    model.RangeTanggalTtdEncode = btoa(rangeTanggalTtd);
    model.RangeTanggalBerakhirEncode = btoa(rangeTanggalBerakhir);
    model.RangeCreateDateEncode = btoa(rangeCreateDate);
 model.IsDraft = isDraftSelected;
    model.IsGtg = isG2GSelected;
    model.IsBdCoreBusinessDecode = isBdCoreBusinessSelected;
    model.IsStrategicPartnerShipDecode = isStrategicPartnerShipSelected;
    model.IsGreenBusinessDecode = isGreenBusinessSelected;
    model.StrSearch = searchValue;

    $.ajax({
        type: "POST",
        url: param.dataCountSearch,
        data: JSON.stringify(model),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            data.StatusBerlaku.forEach(statusBerlakuLoop);
            data.DiscussionStatus.forEach(discussionStatusLoop);
            data.AgreementHolder.forEach(agreementHolderLoop);
            $('.counter-perjanjian').text('(' + data.Perjanjian + ')');
            $('.counter-negaraMitra').text('(' + data.NegaraMitra + ')');
        },
        error: function (request, status, errorThrown) {
            alert(status);
        }
    });
    location.reload();
}


//$(function () {
//    $("#rangeTanggalTtd").flatpickr({
//        dateFormat: "d-m-Y",
//        mode: "range"
//    });
//    $("#rangeTanggalBerakhir").flatpickr({
//        dateFormat: "d-m-Y",
//        mode: "range"
//    });
//    $("#rangeCreateDate").flatpickr({
//        dateFormat: "d-m-Y",
//        mode: "range"
//    });
//});

// Date Range
$(function () {
    $("#rangeTanggalTtd").daterangepicker({
        autoUpdateInput: false,
        locale: {
            cancelLabel: 'Cancel'
        }
    });

    $("#rangeTanggalBerakhir").daterangepicker({
        autoUpdateInput: false,
        locale: {
            cancelLabel: 'Cancel'
        }
    });

    $("#rangeCreateDate").daterangepicker({
        autoUpdateInput: false,
        locale: {
            cancelLabel: 'Cancel'
        }
    });

})



// Dropdown List
$(function () {
    $('#ddlStatusBerlaku').select2({
        ajax: {
            url: url.ddlStatusBerlaku,
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
    $('#ddlStatusDiscussion').select2({
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
        },
        maximumSelectionLength: 5
    });
    $('#ddlAgreementHolder').select2({
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
    $('#ddlKawasanMitra').select2({
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
    $('#ddlJenisPerjanjian').select2({
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

    if (selectedFilter) {
        var decodedFilter = JSON.parse(fromUrlSafeBase64(selectedFilter));
        var filterValue = '';
        var urls = '';
        Object.keys(decodedFilter).forEach(function (key, index) {              
            switch (key) {
                case "#ddlStreamBusiness":
                    urls = url.ddlStreamBusiness;
                        break;
                case "#ddlJenisPerjanjian":
                    urls = url.ddlJenisPerjanjian;
                        break;
                case "#ddlNegaraMitra":
                    urls = url.ddlNegaraMitraWithoutKawasan;
                        break;
                case "#ddlKawasanMitra":
                    urls = url.ddlKawasanMitra;
                        break;
                case "#ddlStatusBerlaku":
                    urls = url.ddlStatusBerlaku;
                    break;
                case "#ddlStatusDiscussion":
                    urls = url.ddlStatusDiscussion;
                    break;
                case "#ddlAgreementHolder":
                    urls = url.ddlHsh;
                    break;
            }
            if (key.includes('ddl')) {
                selectedFilterDdl(urls).then(x => {
                    filterValue = decodedFilter[key].split('_');
                    if (x.items && x.items.length > 0) {
                        var selectedItems = x.items.filter(x => filterValue.includes(x.id));
                        selectedItems.forEach(item => {
                            var option = new Option(item.text, item.id, true, true);
                            $(`${key}`).append(option);
                        });

                        $(`${key}`).trigger('select2:select');
                    }
                })
            }
            else {
                if (typeof decodedFilter[key] === 'boolean') {
                    $(`${key}`).prop('checked', decodedFilter[key]);
                    $(`${key}`).trigger('change');
                }
                else {
                    $(`${key}`).val(decodedFilter[key]);
                    $(`${key}`).daterangepicker().apply();
                }
            }
        });
    }
});

function selectedFilterDdl(urls) {
    return new Promise(function (resolve, reject) {
        $.ajax({
            url: urls,
            method: 'GET',
            success: resolve,
            error: (xhr, status, error) => reject(error)
        });
    });
}
//function clearDateRange() {
//    const $flatpickrRangeTanggalTtd = $("#rangeTanggalTtd").flatpickr();
//    const $flatpickrRangeTanggalBerakhir = $("#rangeTanggalBerakhir").flatpickr();
//    const $flatpickrRangeCreateDate = $("#rangeCreateDate").flatpickr();

//    $flatpickrRangeTanggalTtd.clear();
//    $flatpickrRangeTanggalBerakhir.clear();
//    $flatpickrRangeCreateDate.clear();

//    $("#rangeTanggalTtd").flatpickr({
//        dateFormat: "d-m-Y",
//        mode: "range"
//    });
//    $("#rangeTanggalBerakhir").flatpickr({
//        dateFormat: "d-m-Y",
//        mode: "range"
//    });
//    $("#rangeCreateDate").flatpickr({
//        dateFormat: "d-m-Y",
//        mode: "range"
//    });
//}


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
    var jsonFilter = JSON.stringify(setFilterValue());
    var encodedFilter = toUrlSafeBase64(jsonFilter);
    $.ajax({
        type: "GET",
        url: param.checkCompanyCode + "?guid=" + guid,
        data: { encodedFilters: encodedFilter },
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

function setFilterValue() {
    var model = {};

    $('#ddlEntitasPertamina').val().length > 0 ? model['#ddlEntitasPertamina'] = $('#ddlEntitasPertamina').val().join('_') : '';
    $('#ddlStreamBusiness').val().length > 0 ? model['#ddlStreamBusiness'] = $('#ddlStreamBusiness').val().join('_') : '';
    $('#ddlJenisPerjanjian').val().length > 0 ? model['#ddlJenisPerjanjian'] = $('#ddlJenisPerjanjian').val().join('_') : '';
    $('#ddlNegaraMitra').val().length > 0 ? model['#ddlNegaraMitra'] = $('#ddlNegaraMitra').val().join('_') : '';
    $('#ddlKawasanMitra').val().length > 0 ? model['#ddlKawasanMitra'] = $('#ddlKawasanMitra').val().join('_') : '';
    $('#ddlStatusBerlaku').val().length > 0 ? model['#ddlStatusBerlaku'] = $('#ddlStatusBerlaku').val().join('_') : '';
    $('#ddlStatusDiscussion').val().length > 0 ? model['#ddlStatusDiscussion'] = $('#ddlStatusDiscussion').val().join('_') : '';
    $('#ddlAgreementHolder').val().length > 0 ? model['#ddlAgreementHolder'] = $('#ddlAgreementHolder').val().join('_') : '';
    $('#ddlAgreementHolder').val().length > 0 ? model['#ddlAgreementHolder'] = $('#ddlAgreementHolder').val().join('_') : '';
    $('#ddlAgreementHolder').val().length > 0 ? model['#ddlAgreementHolder'] = $('#ddlAgreementHolder').val().join('_') : '';
    $("#rangeTanggalTtd").val().length > 0 ? model['#rangeTanggalTtd'] = $("#rangeTanggalTtd").val() : '';
    $("#rangeTanggalBerakhir").val().length > 0 ? model['#rangeTanggalBerakhir'] = $("#rangeTanggalBerakhir").val() : '';
    $("#rangeCreateDate").val().length > 0 ? model['#rangeCreateDate'] = $("#rangeCreateDate").val() : '';
    $("#checkBoxIsDraft").prop('checked') ? model['#checkBoxIsDraft'] = true : false;

    return model;

}

function viewRedirect(guid) {    
    var urls = `${param.prefix}ApiAgreement/GetRedirectViewFilter?guid=${guid}`;
    var jsonFilter = JSON.stringify(setFilterValue());
    var encodedFilter = toUrlSafeBase64(jsonFilter);
    $.ajax({
        type: "GET",
        url: urls,
        data: { encodedFilters: encodedFilter },
        success: function (data) {
            window.location.href = param.picname + data.redirectUrl;
        },
        error: function () {
            alert("Error while deleting data");
        }
    });
}

function toUrlSafeBase64(str) {
    return btoa(str).replace(/\+/g, '-').replace(/\//g, '_').replace(/=+$/, '');
}

function fromUrlSafeBase64(str) {
    str = str.replace(/-/g, '+').replace(/_/g, '/');
    while (str.length % 4) str += '=';
    return atob(str);
}