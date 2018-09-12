
function AccountLoadFunctions() {
    "use strict";
    $("#CallLoginPage").click(function () { LoginPageCall(); });
    $("#CallLogin").click(function () { LoginEmployeeCall(); });
    $("#CallLogOff").click(function () { CallModalLogOff(); });
    
    //Buttons Menu
    $("#CallSearched").click(function () { CallSearched(); });

    $("#CallGeoLocalization").click(function () { CallMenuGeoLocalization(); });
    $("#CallAbout").click(function () { CallMenuAbout(); });
 
}

function CallModalLogOff() {
    $("#smallModalOptPassword").modal('hide');
    $("#smallModal").modal("show");
}

function CallModalOptPassword() {
    $("#smallModalOptPassword").modal("show");
}


function CallRedirect(control, action) {
    window.location = window.appPathAppName +  "/" + control + "/" + action;
    return false;
}

function CallSearched() {
    window.location = window.appPathAppName + "/Home/Searched";
}

function CallMenuGeoLocalization() {
    window.location = window.appPathAppName + "/Home/Index";
}

function CallMenuAbout() {
    window.location = window.appPathAppName +  "/About/Index";
}


function LoginCheckConditions() {
    //debugger;
    if (window.UserLogged) {
        LoginEnableForUpdate();
    } else {
        LoginDisableAll();
    }
}

function Login_LoadInfos() {
    LoginCheckConditions();
    AccountLoadFunctions();
}

function LoginPageCall() {
    window.location = window.appPathAppName +  "/Account/Login";
}

function CallActionLogOff() {
    window.location = window.appPathAppName +  "/Account/LogOff";
}

function LoginEmployeeCall() {
    var userName = $("#UserName").val();
    var password = $("#Password").val();
    $.ajax({
        async: false,
        cache: false,
        contentType: "application/json; charset=utf-8",
        //url: '@Url.Action("LoginAjax", "Account")',
        url: window.defineUrlController("Account", "LoginAjax"),
        data: JSON.stringify({ 'userName': userName, 'password': password, 'companyID': companyId}),
        beforeSend: function() {
            //openMsgLoading();
        },
        type: "post",
        success: function(response) {
            if (response.success) {
                window.displayValidationErrors(response, "alert alert-success", null);
                var timer = setTimeout(function () {
                    window.location = window.appPathAppName + "/Home/Index";
                    },
                    1000);
            } else {
                window.displayValidationErrors(response, "alert alert-danger", "#UserName");
            }
            
        },  
        error: function (response) {
            window.displayValidationErrors(response, "alert alert-danger", "#UserName");
        },
        complete: function() {
            //closeMsgLoading();
        }
    });
}


function LoginEnableForUpdate() {
    $("#OldPassword").removeAttr("disabled");
    $("#NewPassword").removeAttr("disabled");
    $("#ConfirmPassword").removeAttr("disabled");
}

function LoginDisableAll() {
    $("#OldPassword").attr("disabled", "disabled");
    $("#NewPassword").attr("disabled", "disabled");
    $("#ConfirmPassword").attr("disabled", "disabled");
}