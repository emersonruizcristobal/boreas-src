using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Service.Enum {
    public class EnumService {
        public static List<SelectListItem> ToSelectListItem<T>() {
            return System.Enum
                         .GetValues(typeof(T))
                         .Cast<T>()
                         .Select(item => new SelectListItem {
                             Text = SplitWords(item.ToString()),
                             Value = (Convert.ToInt32(item)).ToString()
                         })
                         .ToList();
        }

        public static List<TResult> ToEntities<TEnum, TResult>(Func<TEnum, TResult> mappings) {
            return System.Enum
                         .GetValues(typeof(TEnum))
                         .Cast<TEnum>()
                         .Select(mappings)
                         .ToList();
        }

        public static string SplitWords(string enumText) {
            TextInfo culturedString = new CultureInfo("en-US", false).TextInfo;
            var enumStrings         = System.Text.RegularExpressions.Regex.Replace(enumText, "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();

            return string.Format("{0}", enumStrings, culturedString.ToTitleCase(enumStrings));

        }


    }
}
