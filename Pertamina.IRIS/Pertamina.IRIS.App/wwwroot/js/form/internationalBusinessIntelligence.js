
/*Bind data dropdown*/
$(function () {
    $('#filterByStream').select2({
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
    }).on("change", function (e) {
        var model = prepareDataRequestFilter();

        $.ajax({
            url: urlFilter,
            type: 'POST',
            data: model,
            success: function (result) {
                bindRelatedDocument(result);
                if (!model.StreamBusinessId
                    && !model.EntitasPertaminaId
                    && model.CountriesCoveredSelected.length == 0
                    && !model.TypeStudyId
                    && !model.SearchBar) {
                    BindDataResetIBIView();
                }
            }
        });

    });

    $('#filterByTypeOfStudy').select2({
        ajax: {
            url: url.ddlTypeOfStudy,
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
        var model = prepareDataRequestFilter();

        $.ajax({
            url: urlFilter,
            type: 'POST',
            data: model,
            success: function (result) {
                bindRelatedDocument(result);
                if (!model.StreamBusinessId
                    && !model.EntitasPertaminaId
                    && model.CountriesCoveredSelected.length == 0
                    && !model.TypeStudyId
                    && !model.SearchBar) {
                    BindDataResetIBIView();
                }
            }
        });

    });

    $("#filterByOwnerEntity").select2({
        ajax: {
            url: url.ddlOwnerEntity,
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
        var model = prepareDataRequestFilter();

        $.ajax({
            url: urlFilter,
            type: 'POST',
            data: model,
            success: function (result) {
                bindRelatedDocument(result);
                if (!model.StreamBusinessId
                    && !model.EntitasPertaminaId
                    && model.CountriesCoveredSelected.length == 0
                    && !model.TypeStudyId
                    && !model.SearchBar) {
                    BindDataResetIBIView();
                }
            }
        });

    });

    $('#filterByCountries').select2({
        multiple: true,
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
    }).on("change", function (e) {
        var model = prepareDataRequestFilter();

        $.ajax({
            url: urlFilter,
            type: 'POST',
            data: model,
            success: function (result) {
                bindRelatedDocument(result);
                if (!model.StreamBusinessId
                    && !model.EntitasPertaminaId
                    && model.CountriesCoveredSelected.length == 0
                    && !model.TypeStudyId
                    && !model.SearchBar) {
                    BindDataResetIBIView();
                }
            }
        });
    });

    GetDisclaimerModal();

    bindButtonEvent();

    var popOver = $('.popover');
    if (popOver.children().length <= 0) {
        popOver.hide();       
    }

    document.querySelector('.app-root').dispatchEvent(new Event('closeSpinner'));

});
function GetDisclaimerModal() {
    var modal = $('#disclaimer-modal');
    modal.modal('show');
    setTimeout(() => {
        modal.modal('hide');
    }, 60000);
    //$.ajax({
    //    url: urlGetDisclaimer,
    //    type: 'GET',
    //    success: function (result) {
    //        /*modal.html(result);*/
    //        //var target = $('#disclaimer-modal');
    //        target.modal('show');

    //    }
    //});
}

function showListDoc(el) {
    document.querySelector('.app-root').dispatchEvent(new Event('loadSpinner'));
    var urlPartial = el.dataset.url;
    var detailModal = $('#PreviewListDocumentPartialModal');
    $.ajax({
        url: urlPartial,
        type: 'GET',
        success: function (result) {
            document.querySelector('.app-root').dispatchEvent(new Event('closeSpinner'));
            detailModal.html(result);
            $('#preview-list-modal').modal('show');
        }
    });
}


//Map
am5.ready(function () {
    // Create root element
    var el = $("#map-ibi");

    if (el.length > 0) {
        var root = am5.Root.new("map-ibi");
        root._logo.dispose();

        // Set themes
        root.setThemes([
            am5themes_Animated.new(root)
        ]);

        var chart = root.container.children.push(
            am5map.MapChart.new(root, {
                panX: "none",
                panY: "none",
                wheelX: "none",
                wheelY: "none",
                pinchZoom: false,
                projection: am5map.geoNaturalEarth1()
            })
        );

        // Load world map data and set the color
        var polygonSeries = chart.series.push(
            am5map.MapPolygonSeries.new(root, {
                geoJSON: am5geodata_worldLow,
                exclude: ["AQ"],
                fill: am5.color(0xE5E8E8), // Set the map color to #E5E8E8 0x9e9e9d
                stroke: am5.color(0x9e9e9d)
            })
        );

        polygonSeries.mapPolygons.template.setAll({
            tooltipText: "{name}",
            toggleKey: "active",
            interactive: true,
        });

        var currentActivePolygon = null;
        // Add hover event
        polygonSeries.mapPolygons.template.events.on("pointerover", function (ev) {
            var data = ev.target.dataItem.dataContext;

            if (data.relevantDocs != undefined && data.relevantDocs) {
                currentActivePolygon = ev.target;
                currentActivePolygon.set("fill", am5.color(0xB6CAD8));

                var popover = document.getElementById('popover');
                if (popover.style.display == 'none' || popover.style.display == '') {
                    currentActivePolygon.set("active", false);
                    currentActivePolygon.set("inactive", true);
                }
            }
        });

        // Add pointerout event
        polygonSeries.mapPolygons.template.events.on("pointerout", function (ev) {
            currentActivePolygon = ev.target;
            if (currentActivePolygon.get("inactive")) {
                currentActivePolygon.states.applyAnimate("default");
                currentActivePolygon.set("fill", am5.color(0xE5E8E8));
            }

        });


        // Create popover on click
        polygonSeries.mapPolygons.template.events.on("click", function (ev) {
            var data = ev.target.dataItem.dataContext;

            if (data.relevantDocs != undefined && data.relevantDocs) {

                currentActivePolygon = ev.target;
                currentActivePolygon.set("active", true);
                currentActivePolygon.set("inactive", false);
                currentActivePolygon.set("fill", am5.color(0xB6CAD8));

                // Use the original event to get the position of the click
                var svgPoint = {
                    x: ev.originalEvent.layerX,
                    y: ev.originalEvent.layerY + 50
                };

                showPopover(data, svgPoint);
            }
        });

        var arrCountries = [];
        $.each(dataMapIBI, function (index, item) {
            arrCountries.push({
                id: item.CountryAcronyms,
                name: item.NegaraMitraName,
                flag: item.Flag,
                gdp: item.Gdp,
                gdpPerCapita: item.GdpPerCapita,
                oilReserves: item.OilGasReserves,
                oilProduction: item.OilProduction,
                population: item.Population,
                relevantDocs: item.DocumentCount,
                idIBI: item.InternationalBusinessIntelligenceId
            });
        });

        polygonSeries.data.setAll(arrCountries);

        // // Close popover when clicking anywhere else on the map
        chart.seriesContainer.events.on("click", function (ev) {
            ev.originalEvent.stopPropagation(); // Prevent the event from bubbling up to document
        });

        // Function to show the popover
        function showPopover(data, svgPoint) {

            var popover = document.getElementById('popover');
            popover.innerHTML = `
			<div class="popover-header">
			<span><img src="${param.prefixPic}${data.flag}" alt="country" width="20"> ${data.name}</span>
			<button id="closePopover" type="button" class="btn-close" aria-label="Close"></button>
			</div>

			<div class="popover-body">
			<div class="row info-box">
				<div class="col-12"><span>${data.population}</span>Population</div> <!-- Full-width column for Population -->
				<div class="col-6"><span>${data.gdp}</span>GDP</div>
				<div class="col-6"><span>${data.gdpPerCapita}</span>GDP Per Capita</div>
				<div class="col-6"><span>${data.oilReserves}</span>O&G Reserves</div>
				<div class="col-6"><span>${data.oilProduction}</span>Oil Production</div>
			</div>
			</div>

			<div class="popover-footer fw-bold pointer relevant-doc" data-idibi="${data.idIBI}">
			<span><i class="bi bi-file-earmark-text-fill"></i> ${data.relevantDocs} Relevant Documents</span>
			<a><i class="bi bi-arrow-right-circle"></i></a>
			</div>`;

            // Position the popover relative to the clicked point
            popover.style.position = "absolute";
            popover.style.left = svgPoint.x + 'px';
            popover.style.top = svgPoint.y + 'px';
            popover.style.display = 'block';

            // Add event listener to the close button

            document.getElementById('closePopover').addEventListener('click', function () {
                popover.style.display = 'none';
                resetAllPolygonColors();
            });

            $(document).on("click", ".relevant-doc", function () {
                var idIBI = $(this).data("idibi");
                var model = {
                    SearchBar: "",
                    EntitasPertaminaId: "",
                    StreamBusinessId: "",
                    TypeStudyId: "",
                    CountriesCoveredSelected: [],
                    CountriesCoveredSelectedMap: []
                };

                // Split the string into an array of IDs
                var idArray = idIBI.split(',');

                // Loop through the array and push each ID into the param array
                idArray.forEach(function (id) {
                    model.CountriesCoveredSelectedMap.push(id.trim());
                });

                $.ajax({
                    url: urlFilter,
                    type: 'POST',
                    data: model,
                    success: function (result) {
                        $('.filter-mode').empty();
                        bindRelatedDocument(result);
                        document.getElementById('sectionFilter').scrollIntoView({ behavior: 'smooth', block: 'start' });
                    }
                });
            });

        }
    }

    function resetAllPolygonColors() {
        polygonSeries.mapPolygons.each(function (polygon) {
            polygon.set("fill", am5.color(0xE5E8E8)); // Reset to default fill color
            polygon.set("active", false);
            polygon.set("inactive", true);
        });
    }

}); // end am5.ready()

//IBI display in slide mode
function initializeOwlCarousel() {
    $('.owl-carousel').owlCarousel({
        rtl: false,
        loop: false,
        margin: 10,
        nav: false,
        dots: true,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 3
            },
            1000: {
                items: 4
            }
        }
    });
}


//Show Filter data IBI

function bindRelatedDocument(res) {    
    $('#sectionFilter').html(res);
    reloadFunctions();
    bindButtonEvent();
}
function prepareDataRequestFilter() {
    var model = {
        SearchBar: "",
        EntitasPertaminaId: "",
        StreamBusinessId: "",
        TypeStudyId: "",
        CountriesCoveredSelected: [],
        CountriesCoveredSelectedMap: []
    };

    model.SearchBar = $("#searchBar").val();
    model.StreamBusinessId = $("#filterByStream").val();
    model.EntitasPertaminaId = $("#filterByOwnerEntity").val();
    model.CountriesCoveredSelected = $("#filterByCountries").val();
    model.TypeStudyId = $("#filterByTypeOfStudy").val();

    return model;
}


//Search Bar Fuction
$(function () {
    let typingTimer;                // Timer identifier
    let doneTypingInterval = 300;   // Time in milliseconds

    $('#searchBar').on('input', function () {
        clearTimeout(typingTimer);  // Clear the previous timer

        var query = $(this).val();  // Get the value of the input
        var model = prepareDataRequestFilter();
        model.searchBar = query;

        if (query.length >= 3) {
            typingTimer = setTimeout(function () {
                // Perform the AJAX request after user stops typing
                $.ajax({
                    url: urlSearch,
                    type: 'POST',
                    data: model,
                    success: function (result) {
                        $('.filter-mode').empty();
                        $('.search-mode').hide();
                        bindRelatedDocument(result);
                    },
                    error: function (xhr, status, error) {
                        console.error('Search failed: ', error);
                    }
                });
            }, doneTypingInterval);
        }
        else {
            if (!query || query.trim() === "") {
                BindDataResetIBIView();
            }
        }
    });

    $("#btn-bookmark").click(function () {
        const urlParams = new URLSearchParams(window.location.search);
        const id = urlParams.get('guid');
        $.ajax({
            url: urlBookmark,
            data: {
                guid: id
            },
            type: 'POST',
            success: function (result) {
                window.location.replace(`${urlDetailIbi}?guid=${result.guid}`);
            },
            error: function (xhr, status, error) {
                console.error('Search failed: ', error);
            }
        });
    })

    $("#btn-delete-ibi").click(function () {        
        var idIbi = $(this).data("idibi");
        var urlDelete = `${urlDeleteIbi}?guid=${idIbi}`;
        Swal.fire({
            text: "Are you sure want to delete this data ?",
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
                document.querySelector('.app-root').dispatchEvent(new Event('loadSpinner'));
                $.ajax({
                    url: urlDelete,
                    type: 'PATCH',
                    success: function (result) {
                        if (result.isSuccess) {
                            window.location.href = param.mainPage;
                        }
                    },
                    error: function (xhr, status, error) {
                        document.querySelector('.app-root').dispatchEvent(new Event('closeSpinner'));
                        console.error('Search failed: ', error);
                        document.querySelector('.app-root').dispatchEvent(new Event('closeSpinner'));
                    }
                });
            }
        })
    });

    var exclamationIconProps = document.querySelectorAll('#fa-exclamation-ibi')[0];
    if (exclamationIconProps) {
        document.querySelectorAll('#fa-exclamation-ibi')[0].style.setProperty("color", exclamationIconProps.dataset.color, "important");
    }
});

function BindDataResetIBIView() {
    $.ajax({
        url: urlResetFilterIBI,
        type: 'POST',
        success: function (result) {
            $('.filter-mode').empty();
            $('.search-mode').show();
            bindRelatedDocument(result);
        },
        error: function (xhr, status, error) {
            console.error('Search failed: ', error);
        }
    });
}

function BindDataAllIBIView() {
    $.ajax({
        url: urlGetAllIBI,
        type: 'POST',
        success: function (result) {
            $('.filter-mode').empty();
            $('.search-mode').show();
            bindRelatedDocument(result);
        },
        error: function (xhr, status, error) {
            console.error('View All failed: ', error);
        }
    });
}

function initializeFunctionality() {
    $(".ibi-download").off("click").on("click", function () {
        document.querySelector('.app-root').dispatchEvent(new Event('loadSpinner'));
        var idIBI = $(this).data("idibi");
        var title = $(this).data("title");
        DownloadFile(url.downloadFile + '?idIBI=' + idIBI + '&title=' + title + '&feature=IBI');        
    });

    $(".view-all").off("click").on("click", function () {
        BindDataAllIBIView();
    });
}

function DownloadFile(strUrl) {
    document.querySelector('.app-root').dispatchEvent(new Event('closeSpinner'));
    window.open(strUrl, "_blank");
}

// Call the function to initialize
if ($('.owl-carousel').length > 0) {
    initializeOwlCarousel();
}
initializeFunctionality();


function reloadFunctions() {
    initializeFunctionality();
    initializeOwlCarousel();
}

async function downloadFile(id) {
    try {
        var urls = `${url.downloadSingleFile}?guid=${id}&feature=${param.featureName}`;
        var response = await fetch(urls);

        if (!response.ok) {
            throw new Error('File not found or failed to download');
        }

        const blob = await response.blob();

        const contentDisposition = response.headers.get('Content-Disposition');
        const contentType = response.headers.get('Content-Type');

        let fileName = '';
        if (contentDisposition) {
            const fileNameMatch = contentDisposition.match(/filename="([^"]+)"/);
            if (fileNameMatch && fileNameMatch[1]) {
                fileName = decodeURIComponent(fileNameMatch[1]);
            }
        }

        if (contentType.includes('image/') || contentType.includes('pdf')) {
            const previewContainer = $('#FileModal');

            let embedEl = '';
            if (contentType.includes('image/')) {
                embedEl = `<img src="${URL.createObjectURL(blob)}" alt="Image" style="max-width:100%;">`;
            } else if (contentType.includes('pdf')) {
                embedEl = `<iframe src="${URL.createObjectURL(blob)}" width="100%" height="500px"></iframe>`;
            }

            const modalContent = `
                <div class="modal fade" id="preview-doc-modal" tabindex="-1" role="dialog">
                    <div class="modal-dialog modal-xl modal-dialog-centered" role="document">
                        <div class="modal-content bg-light">
                            <div class="modal-header">
                                <h5 class="modal-title">Preview Documents</h5>
                                <button type="button" class="btn btn-white" aria-label="Close" onclick="closePreviewModal()">
                                    <i class="far fa-times-circle fa-2xl" style="font-size: 20px"></i>
                                </button>
                            </div>
                            <div class="modal-body">
                                ${embedEl}
                            </div>
                        </div>
                    </div>
                </div>`;

            const existingModal = document.getElementById('preview-doc-modal');
            if (existingModal) {
                existingModal.remove();
            }

            document.body.insertAdjacentHTML('beforeend', modalContent);

            previewContainer.append(modalContent);

            $('#preview-list-modal').modal('hide');
            $('#preview-doc-modal').modal('show');
        } else {
            const a = document.createElement('a');
            a.href = URL.createObjectURL(blob);
            a.download = fileName || 'downloaded-file';
            a.click();
            URL.revokeObjectURL(a.href);
        }
    } catch (err) {
        console.error('Error:', err);
    }
}

function closePreviewModal() {
    $('#preview-doc-modal').modal('hide');

    $('#preview-list-modal').modal('show');
}

function bindButtonEvent() {    

    var el = document.querySelectorAll('.app-main');
    var anchors = '';
    if (el) {
        anchors = document.querySelectorAll('.app-main a');
    }
    else {
        anchors = document.querySelectorAll('.app-content a');
    }    

    anchors.forEach(anchor => {
        if (!anchor.classList.contains('ibi-download') && anchor.id !== 'btn-delete-ibi' && anchor.id !== 'btn-preview-file') {
            anchor.addEventListener('click', function (event) {
                document.querySelector('.app-root').dispatchEvent(new Event('loadSpinner'));
            });
        }
    });
}