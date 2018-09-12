﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Geo.Localization.WebApp.Utils
{
    public class ValidationFields
    {
        public static List<string> GetModelStateErrors(ModelStateDictionary ModelState)
        {
            var errorMessages = new List<string>();

            var validationErrors = ModelState.Values.Select(x => x.Errors);
            validationErrors.ToList().ForEach(ve =>
            {
                var errorStrings = ve.Select(x => x.ErrorMessage);
                errorStrings.ToList().ForEach(em => { errorMessages.Add(em); });
            });

            return errorMessages;
        }
    }
}