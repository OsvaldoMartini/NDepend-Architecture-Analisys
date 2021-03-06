﻿jQuery.extend(jQuery.validator.messages, {
  required: "This field is required.",
  remote: "Please fill in this field.",
  email: "Please, Please enter a valid email address.",
  url: "Please, Please enter a valid URL.",
  date: "Please, write a valid date.",
  dateISO: "Please, write a valid date (ISO).",
  number: "Please, write a valid integer.",
  digits: "Please, write digits only.",
  creditcard: "Please, write a valid card number.",
  equalTo: "Please, write the same value again.",
  accept: "Please, write a value with an extension accepted.",
  maxlength: jQuery.validator.format("Please, do not write more than {0} characters."),
  minlength: jQuery.validator.format("Please, do not write less than {0} characters."),
  rangelength: jQuery.validator.format("Please, write a value between the characters {0} and {1}."),
  range: jQuery.validator.format("Please, write a value between {0} and {1}."),
  max: jQuery.validator.format("Please, write a value less than or equal to {0}."),
  min: jQuery.validator.format("Please, write a value greater than or equal to {0}."),
  UserName:jQuery.validator.format("Please, The {0} must be at least {1} characters long."),
  Password:jQuery.validator.format("Please, The {0} must be at least {1} characters long.")
});
