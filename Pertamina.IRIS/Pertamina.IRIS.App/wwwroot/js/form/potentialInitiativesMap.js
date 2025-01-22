am5.ready(function () {
    var hub = [];
    var originalColors = {}; // Object to store the initial colors of polygons

    // Create root element
    var root = am5.Root.new("chartdiv");
    root._logo.dispose();

    root.setThemes([am5themes_Animated.new(root)]);

    var chart = root.container.children.push(
        am5map.MapChart.new(root, {
            projection: am5map.geoNaturalEarth1()
        })
    );

    // Load world map data and set the color
    var polygonSeries = chart.series.push(
        am5map.MapPolygonSeries.new(root, {
            geoJSON: am5geodata_worldLow,
            exclude: ["AQ"],
            fill: am5.color(0xE5E8E8),
            stroke: am5.color(0x9e9e9d)
        })
    );

    polygonSeries.mapPolygons.template.setAll({
        tooltipText: "",
        templateField: "polygonSettings",
        interactive: true,
        tooltipHTML: ""
    });

    // Create point series
    var pointSeries = chart.series.push(
        am5map.MapPointSeries.new(root, {})
    );

    $.ajax({
        type: 'GET',
        url: param.urlGetCountriesMap,
        success: function (data) {
            console.log(data);
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

                // Store the initial color for each polygon
                originalColors[countryId[i]] = color;
            };

            hub = data.HubGrouppedCountries.map(x => {
                return {
                    geometry: { type: "Point", coordinates: [x.Hub.Longitude, x.Hub.Latitude] },
                    name: x.Hub.Name,
                    groups: x.GrouppedCountriesAcronym
                };
            });

            console.log(hub);

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
            polygonSeries.data.setAll(countryPolygon);

            pointSeries.data.setAll(hub);

            // Add bullets for the points
            pointSeries.bullets.push(function (root, series, dataItem) {
                var container = am5.Container.new(root, {
                    layout: root.verticalLayout,
                    centerX: am5.p50
                });

                // Create a container for the icon and label
                var labelContainer = container.children.push(
                    am5.Container.new(root, {
                        layout: root.verticalLayout,
                    })
                );

                // Add Circle icon
                labelContainer.children.push(
                    am5.Circle.new(root, {
                        radius: 8,
                        fill: am5.color(0x006cb8),
                        stroke: am5.color(0xFFFFFF),
                        strokeWidth: 1,
                        marginRight: 0
                    })
                );

                // Add label text using data from the point
                labelContainer.children.push(
                    am5.Label.new(root, {
                        text: dataItem.dataContext.name,
                        fontSize: 12,
                        fill: am5.color(0x000000),
                        textAlign: "center",
                        centerX: am5.p50,
                        y: 10
                    })
                );

                // Add hover interactions
                container.events.on("pointerover", function () {
                    highlightCountries(dataItem.dataContext.groups);
                });

                container.events.on("pointerout", function () {
                    resetCountries();
                });

                return am5.Bullet.new(root, {
                    sprite: container
                });
            });
        }
    });

    // Function to highlight countries based on group
    function highlightCountries(groups) {
        polygonSeries.mapPolygons.each(function (polygon) {
            var countryId = polygon.dataItem.dataContext.id;
            if (groups.includes(countryId)) {
                polygon.set("fill", am5.color(0x007bff)); // Highlight color
            }
        });
    }

    // Function to reset country colors based on the initial colors
    function resetCountries() {
        polygonSeries.mapPolygons.each(function (polygon) {
            var countryId = polygon.dataItem.dataContext.id;
            if (originalColors[countryId]) {
                polygon.set("fill", originalColors[countryId]); // Restore the initial color
            }
        });
    }

    // Close popover when clicking anywhere else on the map
    chart.seriesContainer.events.on("click", function (ev) {
        ev.originalEvent.stopPropagation(); // Prevent the event from bubbling up to document
    });
});
