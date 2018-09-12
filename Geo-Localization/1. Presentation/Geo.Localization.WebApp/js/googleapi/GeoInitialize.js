var placeSearch, autocomplete;

function initialize() {
    InitializeSearchMap();
    autocomplete = new google.maps.places.Autocomplete(document.getElementById('autocomplete'));
    google.maps.event.addListener(autocomplete, 'place_changed', function () {
        PrePareToInsert();
    });
    if (isEmpty(dataSearched))
        CallGetAllSearched();
}

function geolocate() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var geolocation = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
            autocomplete.setBounds(new google.maps.LatLngBounds(geolocation, geolocation));

        });
    }
}

function PrePareToInsert() {
    var fromAutoComplete = document.getElementById('autocomplete');
    var localName = document.getElementById('LocalName');
    GetCoordenates();
}
