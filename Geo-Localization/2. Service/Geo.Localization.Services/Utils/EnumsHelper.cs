using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Web.Mvc;

namespace Geo.Localization.Services.Utils
{
    #region [Enums]

    public static class EnumsHelper
    {
        public static string ToEnumIDByName<T>(T value)
        {
            return Enum.GetName(typeof(T), value);
        }

        public static T ToEnumById<T>(this int value) where T : struct
        {
            return (T) (object) value;
        }

        public static string ToEnumNameById<T>(this int value) where T : struct
        {
            return ((T) (object) value).ToString();
        }

        public static SelectList GetAllRoles<T>(int id = 0) where T : struct
        {
            var names = Enum.GetNames(typeof(T));
            var values = (T[]) Enum.GetValues(typeof(T));

            for (var i = 0; i < names.Length; i++)
                Debug.WriteLine(names[i], values[i]);


            var list = new List<SelectListItem>();

            if (typeof(T).IsEnum)
            {
                var enumValues = (T[]) Enum.GetValues(typeof(T));

                foreach (var item in enumValues)
                {
                    var index = Array.IndexOf(Enum.GetValues(item.GetType()), item);
                    var role = (int) Enum.GetValues(item.GetType()).GetValue(index);
                    list.Add(new SelectListItem {Value = role.ToString(), Text = item.ToString()});
                }
            }
            return new SelectList(list, "Value", "Text", id);
        }

        public static IEnumerable<TResult> SelectWhere<TSource, TResult>(this IEnumerable<TSource> source,
            Func<TSource, bool> filter, Func<TSource, int, TResult> selector)
        {
            var index = -1;
            foreach (var s in source)
            {
                checked
                {
                    ++index;
                }
                if (filter(s))
                    yield return selector(s, index);
            }
        }

        public enum UserRoles
        {
            [Display(Name = "Administrator")] Admin = 1,

            [Display(Name = "Supervisor")] Supervisor = 2,

            [Display(Name = "Support")] Support = 3,

            [Display(Name = "Customer Services")] CustomerServices = 4
        }



        #endregion
    }
}