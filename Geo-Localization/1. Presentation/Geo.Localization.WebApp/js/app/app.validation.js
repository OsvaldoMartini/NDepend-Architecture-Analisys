var app = angular.module("geoLocApp", ["bootstrap.angular.validation", "ui.bootstrap"]);


app.config([
    'bsValidationConfigProvider', function (bsValidationConfigProvider) {
        //debugger;
        //bsValidationConfigProvider.global.setValidateFieldsOn('display');
        bsValidationConfigProvider.global.addSuccessClass = false;
        bsValidationConfigProvider.global.setValidateFieldsOn('submit');
        bsValidationConfigProvider.global.setDisplayErrorsAs('tooltip');
        bsValidationConfigProvider.global.tooltipAppendToBody = false;
        bsValidationConfigProvider.global.errorMessagePrefix =
            '<span class="glyphicon glyphicon-warning-sign"></span> &nbsp;';
        //bsValidationConfigProvider.global.messages.title = jQuery.validator.messages.required;

        bsValidationConfigProvider.global.messages.required = jQuery.validator.messages.required;
       // bsValidationConfigProvider.global.messages.email = jQuery.validator.messages.email;
    }
]);


app.controller("geoLocController", function ($scope) {
    $scope.name = "";

});
