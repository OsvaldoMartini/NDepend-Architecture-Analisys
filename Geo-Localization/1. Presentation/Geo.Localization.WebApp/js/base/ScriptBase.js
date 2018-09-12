var hash = null;
var touchMode = null;

function checkLocalHost() {
    var string = "localhost";
    return string.indexOf(window.location.hostname) != -1;
}


function openMsgLoading() {
    $("#alertWaiting").html("<strong>Wait is Loading!</strong> ....loading.").show();
}

function closeMsgLoading() {
    $("#alertWaiting").show().delay(300).fadeOut("slow");
}

function closeAllAlerts() {
    //$("#alertWaiting").hide();
    //$("#alertNotAuthorized").hide();
    //$("#alertSuccess").hide();
    //$("#alertDanger").hide();
}


function userNotAuthorized() {
    response.responseText = "<strong>Access Controlled</strong>.<br />User not allowed to access the information requested!";
    window.displayValidationErrors(response, "alert alert-danger", null);
}


function isEmpty(val) {
    return (val === undefined || val == null || val.length <= 0) ? true : false;
}

function defineUrlController(controller, action) {
    var suffix = "";
    if (!window.mvcExtensionless)
        suffix = ".mvc";
    var langBar = "";

    if (!isEmpty(lang)) {
        langBar = lang + "/";
    } else
        langBar = "";

    if (action == "")
        return window.urlContent + langBar + controller + suffix + "/";
    else
        return window.urlContent + langBar + controller + suffix + "/" + action;
}

function defineUrlControllerComplete(isHttps, ipServer, controller, action) {
    var protocol = (isHttps) ? "https://" : "http://";

    return protocol + ipServer + defineUrlController(controller, action);
}

function loadScript(url) {
    if (url != null)
        while (url.indexOf("//") >= 0)
            url = url.replace("//", "/");

    $.ajax({
        async: false,
        cache: false,
        url: url,
        dataType: "script",
        success: function (data, textStatus) { }
    });

}


var options = {
    "backdrop": "static"
}

function displayValidationErrors(response, classType, fieldFocus) {
    $("#idModalMessage").removeClass(classType);
    $("#idModalMessage").attr('class', classType);
    if (!isEmpty(response.responseText)) {

        $("#idModalMessage").html(response.responseText);
        var errors = response["errors"];
        if (errors != null) {
            var $ul = $("#idModalMessage");
            //$ul.empty();
            $.each(errors,
                function (idx, errorMessage) {
                    $ul.append("<li>" + errorMessage + "</li>");
                });
            //$ul.addClass(classType);
        }
        //$('#smallModalMessage').on('hidden.bs.modal', function () {
        //    setTimeout(function () { $('[data-target="#smallModalMessage"]').blur(); }, 10);
        //});
        //PopUp Message
        $("#smallModalMessage").modal("show").delay(2000);
        setTimeout(function () {
            $('#smallModalMessage').modal("hide");
            if (!isEmpty(fieldFocus))
                $(fieldFocus).focus();
            //setTimeout(function() {
            //        $("#idModalMessage").removeClass(classType);
            //    },
            //    2000);
        },
            1500);
    }
}



function displayValidationMsgErrors(strmsg, classType, fieldFocus) {
    $("#idModalMessage").removeClass(classType);
    $("#idModalMessage").attr('class', classType);
    if (!isEmpty(strmsg)) {

        $("#idModalMessage").html(strmsg);
        
        //$('#smallModalMessage').on('hidden.bs.modal', function () {
        //    setTimeout(function () { $('[data-target="#smallModalMessage"]').blur(); }, 10);
        //});
        //PopUp Message
        $("#smallModalMessage").modal("show").delay(2000);
        setTimeout(function () {
                $('#smallModalMessage').modal("hide");
                if (!isEmpty(fieldFocus))
                    $(fieldFocus).focus();
                //setTimeout(function() {
                //        $("#idModalMessage").removeClass(classType);
                //    },
                //    2000);
            },
            1500);
    }
}