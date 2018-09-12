var dataSearched = null
function InitializeSearchMap() {

    google.maps.visualRefresh = true;
    var splitedtPos = geoOffiLoc.split(',');
    var wService = new google.maps.LatLng(splitedtPos[0], splitedtPos[1]);
    var mapInitialOptions = {
        zoom: 5,
        center: wService,
        mapTypeId: google.maps.MapTypeId.G_NORMAL_MAP
    };

    // This makes the div with id "map_canvas" a google map
    var map = new google.maps.Map(document.getElementById("mapSearch"), mapInitialOptions);

    // Pin "marker"
    var markers = new google.maps.Marker({
        position: wService,
        map: map,
        title: 'Geo Localization'
    });

    // Places Pin Different for Geo Localization
    markers.setIcon('http://maps.google.com/mapfiles/ms/icons/green-dot.png');

    if (isEmpty(dataSearched))
        CallGetAllSearched();

    //var dataSearched = CallGetAllSearched();
    //GeoCodeSearch();
    // Using the JQuery "each" selector to iterate through the JSON list and drop marker pins
    if (!isEmpty(dataSearched)) {
        $.each(dataSearched,
                function (i, item) {
                    var marker = new google.maps.Marker({
                        'position': new google.maps.LatLng(item.GeoLong, item.GeoLat),
                        'map': map,
                        'title': item.PlaceName
                    });

                    // Make the marker-pin blue!
                    marker.setIcon('http://maps.google.com/mapfiles/ms/icons/blue-dot.png');

                    // put in some information about each json object - in this case, the opening hours.
                    var infowindow = new google.maps.InfoWindow({
                        content: "<div class='infoDiv'><h2>" +
                            item.PlaceName +
                            "</h2>" +
                            "<div><h4>Opening hours: " +
                            item.OpeningHours +
                            "</h4></div></div>"
                    });

                    // finally hook up an "OnClick" listener to the map so it pops up out info-window when the marker-pin is clicked!
                    google.maps.event.addListener(marker,
                        'click',
                        function () {
                            infowindow.open(map, marker);
                        });

                });
    }
}


function initAutocomplete() {
    google.maps.visualRefresh = true;
    var splitedtPos = geoOffiLoc.split(',');
    var wService = new google.maps.LatLng(splitedtPos[0], splitedtPos[1]);
    var mapInitialOptions = {
        zoom: 14,
        center: wService,
        mapTypeId: google.maps.MapTypeId.G_NORMAL_MAP
    };

    // "mapSearch" 
    var map = new google.maps.Map(document.getElementById("mapSearch"), mapInitialOptions);

    // Pin "marker"
    var markers = new google.maps.Marker({
        position: wService,
        map: map,
        title: 'Geo Localization'
    });

    // Places Pin Different for Geo Localization
    markers.setIcon('http://maps.google.com/mapfiles/ms/icons/green-dot.png');

    // Create the search box and link it to the UI element.
    var input = document.getElementById('inputSearch');
    var searchBox = new google.maps.places.SearchBox(input);
    map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

    // Bias the SearchBox results towards current map's viewport.
    map.addListener('bounds_changed', function () {
        searchBox.setBounds(map.getBounds());
        $('#LocalName').val(input.value);
    });

    var markers = [];
    // Listen for the event fired when the user selects a prediction and retrieve
    // more details for that place.
    searchBox.addListener('places_changed', function () {
        var places = searchBox.getPlaces();
        $('#LocalName').val(input.value);

        if (places.length == 0) {
            return;
        }

        // Clear out the old markers.
        markers.forEach(function (marker) {
            marker.setMap(null);
        });
        markers = [];

        // For each place, get the icon, name and location.
        var bounds = new google.maps.LatLngBounds();
        places.forEach(function (place) {
            if (!place.geometry) {
                console.log("Returned place contains no geometry");
                return;
            }
            var icon = {
                url: place.icon,
                size: new google.maps.Size(71, 71),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(17, 34),
                scaledSize: new google.maps.Size(25, 25)
            };

            // Create a marker for each place.
            markers.push(new google.maps.Marker({
                map: map,
                icon: icon,
                title: place.name,
                position: place.geometry.location
            }));

            if (place.geometry.viewport) {
                // Only geocodes have viewport.
                bounds.union(place.geometry.viewport);
            } else {
                bounds.extend(place.geometry.location);
            }
        });
        map.fitBounds(bounds);
    });
}
