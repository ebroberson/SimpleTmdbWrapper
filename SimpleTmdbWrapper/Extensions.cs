using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;

namespace SimpleTmdbWrapper
{
    public static class Extensions
    {
        public static IEnumerable<string> GetActiveFlagDescriptions(this Enum flags)
        {
            var result = new List<string>();
            var fields = flags.GetType().GetFields().Where(f => (int)f.GetValue(flags) != 0 &&
                                                                flags.HasFlag((Enum)Enum.Parse(flags.GetType(), f.GetValue(flags).ToString(), true)) &&
                                                                f.GetCustomAttributes(typeof(DescriptionAttribute), false).Length != 0);

            result.AddRange(fields.Select(f => f.GetCustomAttributes(typeof(DescriptionAttribute), false)
                                                .Cast<DescriptionAttribute>()
                                                .FirstOrDefault()
                                                .Description));

            return result;
        }

        public static string GetDescription(this Enum value)
        {
            var description = value.GetType()
                                   .GetField(value.ToString())
                                   .GetCustomAttributes(typeof(DescriptionAttribute), false)
                                   .Cast<DescriptionAttribute>()
                                   .FirstOrDefault()
                                   .Description;

            return description;
        }
    }
}
