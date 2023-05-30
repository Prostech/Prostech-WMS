using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.BLL.Helpers.ValueChecker
{
    public static class ValueCheckerHelper
    {
        public static bool IsNullOrZero(int? value)
        {
            return value == null || value.Value == 0;
        }

        public static bool IsNotNullOrZero(int? value)
        {
            return value != null && value.Value != 0;
        }

        public static bool IsNullOrEmpty(string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNotNullOrEmpty(string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrWhiteSpace(string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNotNullOrWhiteSpace(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNullOrEmpty<T>(List<T> list)
        {
            return list == null || list.Count == 0;
        }

        public static bool IsNotNullOrEmpty<T>(List<T> list)
        {
            return list != null && list.Count != 0;
        }
        public static bool IsNull(object value)
        {
            return value == null;
        }
        public static bool IsNotNull(object value)
        {
            return value != null;
        }
    }
}
