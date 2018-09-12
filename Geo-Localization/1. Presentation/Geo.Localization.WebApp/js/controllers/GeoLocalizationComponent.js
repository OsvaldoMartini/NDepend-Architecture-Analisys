var position = {};

function GeoLocalizationLoadFunctions() {
    "use strict";
    //Buttons 
    $("#InsertLocalSearched").click(function () { CallInsertLocal(); });

}

function CallGetAllSearched() {

		  $.ajax({
            async: false,
            cache: false,
            contentType: "application/json; charset=utf-8",
            //url: '@Url.Action("LoginAjax", "Account")',
            url: window.defineUrlController("Home", "GetAllSearched"),
            data: JSON.stringify({ 'companyId': companyId}),
            beforeSend: function () {
                //openMsgLoading();
            },
            type: "post",
            success: function (response) {
                if (response.success) {
                    //window.displayValidationErrors(response, "alert alert-success", null);
	/*					
					dataSearched = [
						{ "Id": 1, "PlaceName": "Liverpool Museum", "OpeningHours": "9-5, M-F", "GeoLong": "51.521437", "GeoLat": "-2.979919" },
						{ "Id": 2, "PlaceName": "Merseyside Maritime Museum ", "OpeningHours": "9-1,2-5, M-F", "GeoLong": "51.518952", "GeoLat": "-0.081437" },
						{ "Id": 3, "PlaceName": "Walker Art Gallery", "OpeningHours": "9-7, M-F", "GeoLong": "51.521831", "GeoLat": "-0.143975" },
						{ "Id": 4, "PlaceName": "National Conservation Centre", "OpeningHours": "10-6, M-F", "GeoLong": "53.407511", "GeoLat": "-2.984683" }
					];
*/
					dataSearched = response.data;

                } else {
                    window.displayValidationErrors(response, "alert alert-danger", "#LocalName");
					dataSearched = null;
                }

            },
            error: function (response) {
                window.displayValidationErrors(response, "alert alert-danger", "#LocalName");
            },
            complete: function () {
                //closeMsgLoading();
            }
        });


}



function GetCoordenates() {
    var address = $("#autocomplete").val();
    var localName = address;

    if (!isEmpty(address)) {

        position = {};
        $.ajax({
            url: 'http://maps.google.com/maps/api/geocode/json',
            type: 'GET',
            data: {
                address: address,
                sensor: false
            },
            async: false,
            success: function (result) {

                try {
                    position.lat = result.results[0].geometry.location.lat;
                    position.lng = result.results[0].geometry.location.lng;
                    $('#LocalName').val(localName + ", Lat:" + position.lat + ", Lng:" + position.lng);

                } catch (err) {
                    position = null;
                    $('#LocalName').val(localName + ", Lat:Unknow, Lng:Unknow");
                }

            },
            error: function (response) {
                position = null;
                $('#LocalName').val(localName + ", Lat:Unknow, Lng:Unknow");
            },
            complete: function () {
                //closeMsgLoading();
            }

        });
        return position;

    }
}



function CallInsertLocal() {
    var localName = $("#autocomplete").val();
	
    if (isEmpty(position)) {
        window.displayValidationMsgErrors("Latitude and Longitude Cannot be Null!", "alert alert-danger", "#LocalName");
    }

    if (!isEmpty(localName)) {
        $.ajax({
            async: false,
            cache: false,
            contentType: "application/json; charset=utf-8",
            //url: '@Url.Action("LoginAjax", "Account")',
            url: window.defineUrlController("Home", "SaveLocalSearched"),
            data: JSON.stringify({ 'LocalName': localName, 'Lat': position.lat, 'Lng': position.lng }),
            beforeSend: function () {
                //openMsgLoading();
            },
            type: "post",
            success: function (response) {
                if (response.success) {
                    window.displayValidationErrors(response, "alert alert-success", null);
					//To Force the Reload
					dataSearched = null;
					InitializeSearchMap();

                } else {
                    window.displayValidationErrors(response, "alert alert-danger", "#LocalName");
                }

            },
            error: function (response) {
                window.displayValidationErrors(response, "alert alert-danger", "#LocalName");
            },
            complete: function () {
                //closeMsgLoading();
            }
        });


    } else {
        return false;
    }
}

