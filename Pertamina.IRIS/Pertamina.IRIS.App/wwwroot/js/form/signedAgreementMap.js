//New
am5.ready(function () {

    var root = am5.Root.new("chartdiv");
    root._logo.dispose();
    root.setThemes([
        am5themes_Animated.new(root)
    ]);

    var chart = root.container.children.push(
        am5map.MapChart.new(root, {
            projection: am5map.geoNaturalEarth1()
        })
    );

    var polygonSeries = chart.series.push(
        am5map.MapPolygonSeries.new(root, {
            geoJSON: am5geodata_worldLow,
            exclude: ["AQ", "Antartica"],
            stroke: am5.color(0x9e9e9d),
            fill: am5.color(0xE5E8E8),
        })
    );

    polygonSeries.mapPolygons.template.setAll({
        tooltipText: "",
        templateField: "polygonSettings",
        interactive: true,
        tooltipHTML: ""
    });

    polygonSeries.set("tooltip", am5.Tooltip.new(root, {
        getFillFromSprite: false,
        background: am5.Rectangle.new(root, {
            fill: am5.color(0xfecdce)
        })
    }));

    polygonSeries.mapPolygons.template.states.create("hover", {
        fill: am5.color('#942222'),
        stroke: am5.color(0x297373)
    });

    innerFunction(false);

    function innerFunction(isFilter, negara, stream, entitas) {
        if (isFilter) {
            $.ajax({
                type: 'GET',
                url: param.urlGetCountriesMap + '?isFilter=' + isFilter + '&negara=' + (negara == null ? "" : negara) + '&stream=' + (stream == null ? "" : stream) + '&entitas=' + (entitas == null ? "" : entitas),
                success: function (data) {
                    let countryId = data.CountriesAcronym;
                    let countryPolygon = [];
                    let pointers = data.Pointers;
                    const color = am5.color(0xB6CAD8);

                    for (let i = 0; i < countryId.length; i++) {
                        countryPolygon.push({
                            id: countryId[i],
                            polygonSettings: {
                                fill: color
                            }
                        });
                    };

                    chart.goHome();
                    var polygonSeries = chart.series.push(
                        am5map.MapPolygonSeries.new(root, {
                            geoJSON: am5geodata_worldLow,
                            exclude: ["AQ", "Antartica"],
                            stroke: am5.color(0x9e9e9d),
                            fill: am5.color(0xE5E8E8),
                        })
                    );

                    polygonSeries.mapPolygons.template.setAll({
                        tooltipText: "",
                        templateField: "polygonSettings",
                        interactive: true,
                        tooltipHTML: ""
                    });

                    polygonSeries.set("tooltip", am5.Tooltip.new(root, {
                        getFillFromSprite: false,
                        background: am5.Rectangle.new(root, {
                            fill: am5.color(0xfecdce)
                        })
                    }));

                    polygonSeries.mapPolygons.template.states.create("hover", {
                        fill: am5.color('#942222'),
                        stroke: am5.color(0x297373)
                    });

                    polygonSeries.mapPolygons.template.events.on("pointerover", function (ev) {
                        var dataContext = ev.target.dataItem.dataContext;

                        if (countryId.includes(dataContext.id)) {
                            const subHolding = pointers.filter(pointer => pointer.CountriesAcronym == dataContext.id);
                            const distinctSubHolding = Array.from(new Set(subHolding.map(JSON.stringify)))
                                .map(JSON.parse);

                            let listSubHolding = "";
                            for (let i = 0; i < distinctSubHolding.length; i++) {
                                listSubHolding += '<li>' + distinctSubHolding[i].SubHolding + '</li>';
                            };

                            ev.target.set("tooltipHTML", "<div style='color:black'>"
                                + dataContext.name
                                + "<strong> :</strong>"
                                + "<br><ul>"
                                + listSubHolding
                                + "</ul></div>");
                            ev.target.showTooltip();
                        }
                    });
                    polygonSeries.mapPolygons.template.events.on("click", function (ev) {
                        var dataContext = ev.target.dataItem.dataContext;
                        $('#CountryAcronym').val(dataContext.id);
                        var negaraMitra = $('#ddlNegaraMitra').val();
                        var streamBusiness = $('#ddlStreamBusiness').val();
                        var entitasPertamina = $('#ddlEntitasPertamina').val();
                        if (!negaraMitra && !streamBusiness && !entitasPertamina) {
                            isFilter = false;
                        } else {
                            isFilter = true;
                        }

                        const clickColor = am5.color(0x0099cc);
                        countryPolygon = [];
                        for (let i = 0; i < countryId.length; i++) {
                            if (countryId[i] == dataContext.id) {
                                countryPolygon.push({
                                    id: countryId[i],
                                    polygonSettings: {
                                        fill: clickColor
                                    }
                                });
                            } else {
                                countryPolygon.push({
                                    id: countryId[i],
                                    polygonSettings: {
                                        fill: color
                                    }
                                });
                            }
                        };
                        polygonSeries.data.setAll(countryPolygon);

                        $.ajax({
                            url: param.urlHighPriority + '?isHigh=' + false + '&isScroll=' + false + '&isSort=' + false + '&isFilter=' + isFilter + '&isClickMap=' + true
                                + '&negara=' + negaraMitra + '&stream=' + streamBusiness + '&entitas=' + entitasPertamina + '&countryAcronym=' + dataContext.id
                                + '&category=' + '&order=' + '&pageCount=' + 1,
                            type: 'GET',
                            success: function (result) {
                                $("#HighPriorityView").html(result);
                                loadDetailPartial();
                            },
                            error: function (xhr, status, error) {
                                alert("Error: " + error);
                            }
                        });

                        $('.headerHighPriority').text("Clickable Map Signed Agreement");
                        $(".scrollHighPriorirty").scrollTop(0, 0);

                        $.ajax({
                            url: param.urlMetrics + '?negara=' + negaraMitra + '&stream=' + streamBusiness + '&entitas=' + entitasPertamina
                                + '&isClickMap=' + true + '&countryAcronym=' + dataContext.id,
                            type: 'GET',
                            success: function (result) {
                                $("#MetricsView").html(result);
                            },
                            error: function (xhr, status, error) {
                                alert("Error: " + error);
                            }
                        });
                    });
                    polygonSeries.data.setAll(countryPolygon);
                }
            });
        } else {
            $.ajax({
                type: 'GET',
                url: param.urlGetCountriesMap,
                success: function (data) {
                    let countryId = data.CountriesAcronym;
                    let countryPolygon = [];
                    let pointers = data.Pointers;
                    const color = am5.color(0xB6CAD8);

                    for (let i = 0; i < countryId.length; i++) {
                        countryPolygon.push({
                            id: countryId[i],
                            polygonSettings: {
                                fill: color
                            }
                        });
                    };

                    chart.goHome();

                    var polygonSeries = chart.series.push(
                        am5map.MapPolygonSeries.new(root, {
                            geoJSON: am5geodata_worldLow,
                            exclude: ["AQ", "Antartica"],
                            stroke: am5.color(0x9e9e9d),
                            fill: am5.color(0xE5E8E8),
                        })
                    );

                    polygonSeries.mapPolygons.template.setAll({
                        tooltipText: "",
                        templateField: "polygonSettings",
                        interactive: true,
                        tooltipHTML: ""
                    });

                    polygonSeries.set("tooltip", am5.Tooltip.new(root, {
                        getFillFromSprite: false,
                        background: am5.Rectangle.new(root, {
                            fill: am5.color(0xfecdce)
                        })
                    }));

                    polygonSeries.mapPolygons.template.states.create("hover", {
                        fill: am5.color('#942222'),
                        stroke: am5.color(0x297373)
                    });

                    polygonSeries.mapPolygons.template.events.on("pointerover", function (ev) {
                        var dataContext = ev.target.dataItem.dataContext;

                        if (countryId.includes(dataContext.id)) {
                            const subHolding = pointers.filter(pointer => pointer.CountriesAcronym == dataContext.id);
                            const distinctSubHolding = Array.from(new Set(subHolding.map(JSON.stringify)))
                                .map(JSON.parse);

                            let listSubHolding = "";
                            for (let i = 0; i < distinctSubHolding.length; i++) {
                                listSubHolding += '<li>' + distinctSubHolding[i].SubHolding + '</li>';
                            };

                            ev.target.set("tooltipHTML", "<div style='color:black'>"
                                + dataContext.name
                                + "<strong> :</strong>"
                                + "<br><ul>"
                                + listSubHolding
                                + "</ul></div>");
                            ev.target.showTooltip();
                        }
                    });
                    polygonSeries.mapPolygons.template.events.on("click", function (ev) {
                        var dataContext = ev.target.dataItem.dataContext;

                        const clickColor = am5.color(0x0099cc);
                        countryPolygon = [];
                        for (let i = 0; i < countryId.length; i++) {
                            if (countryId[i] == dataContext.id) {
                                countryPolygon.push({
                                    id: countryId[i],
                                    polygonSettings: {
                                        fill: clickColor
                                    }
                                });
                            } else {
                                countryPolygon.push({
                                    id: countryId[i],
                                    polygonSettings: {
                                        fill: color
                                    }
                                });
                            }
                        };
                        polygonSeries.data.setAll(countryPolygon);

                        $.ajax({
                            url: param.urlHighPriority + '?isHigh=' + false + '&isScroll=' + false + '&isSort=' + false + '&isFilter=' + false + '&isClickMap=' + true
                                + '&negara=' + '&stream=' + '&entitas=' + '&countryAcronym=' + dataContext.id
                                + '&category=' + '&order=' + '&pageCount=' + 1,
                            type: 'GET',
                            success: function (result) {
                                $("#HighPriorityView").html(result);
                                loadDetailPartial();
                            },
                            error: function (xhr, status, error) {
                                alert("Error: " + error);
                            }
                        });

                        $('#CountryAcronym').val(dataContext.id);
                        var negaraMitra = $('#ddlNegaraMitra').val();
                        var streamBusiness = $('#ddlStreamBusiness').val();
                        var entitasPertamina = $('#ddlEntitasPertamina').val();
                        $('.headerHighPriority').text("Clickable Map Signed Agreement");
                        $(".scrollHighPriorirty").scrollTop(0, 0);

                        $.ajax({
                            url: param.urlMetrics + '?negara=' + negaraMitra + '&stream=' + streamBusiness + '&entitas=' + entitasPertamina
                                + '&isClickMap=' + true + '&countryAcronym=' + dataContext.id,
                            type: 'GET',
                            success: function (result) {
                                $("#MetricsView").html(result);
                            },
                            error: function (xhr, status, error) {
                                alert("Error: " + error);
                            }
                        });
                    });
                    polygonSeries.data.setAll(countryPolygon);
                }
            });
        }
    }

    $('#ddlNegaraMitra,#ddlStreamBusiness,#ddlEntitasPertamina').on('change', function (e) {
        $(".scrollHighPriorirty").scrollTop(0, 0);
        $('#PageCountSort').val('0');
        $('#CountryAcronym').val(null);
        var negaraMitra = $('#ddlNegaraMitra').val();
        var streamBusiness = $('#ddlStreamBusiness').val();
        var entitasPertamina = $('#ddlEntitasPertamina').val();

        $('#ddlSignedAgreementSortingHighPriority').val(null).trigger('change');
        $('input[name="radioSorting"]').prop('checked', false);
        $('#dataTable').DataTable().destroy();
        $('#dataTable tbody').empty();
        var rowCount = 0;
        if (negaraMitra == null && streamBusiness == null && entitasPertamina == null) {
            loadDataTable();

            $('.headerHighPriority').text("Strategic Signed Agreement");

            innerFunction(false);

            $.ajax({
                url: param.urlHighPriority + '?isHigh=' + true + '&isScroll=' + false + '&isSort=' + false + '&isFilter=' + false
                    + '&negara=' + '&stream=' + '&entitas='
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
        }
        else {
            loadDataTable(negaraMitra, streamBusiness, entitasPertamina);

            $('.headerHighPriority').text("Filter Signed Agreement");

            innerFunction(true, negaraMitra, streamBusiness, entitasPertamina);

            $.ajax({
                url: param.urlHighPriority + '?isHigh=' + false + '&isScroll=' + false + '&isSort=' + false + '&isFilter=' + true
                    + '&negara=' + negaraMitra + '&stream=' + streamBusiness + '&entitas=' + entitasPertamina
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
                url: param.urlMetrics + '?negara=' + negaraMitra + '&stream=' + streamBusiness + '&entitas=' + entitasPertamina,
                type: 'GET',
                success: function (result) {
                    $("#MetricsView").html(result);
                },
                error: function (xhr, status, error) {
                    alert("Error: " + error);
                }
            });
        };
    });
});