using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace RDK.Core.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Returns description of enum value if attached with enum member through DiscriptionAttribute
        /// </summary>        
        /// <returns>Description of enum value</returns>
        /// <see cref="DescriptionAttribute"/>
        public static string ToDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] descriptions = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return (descriptions != null && descriptions.Length > 0) ? descriptions[0].Description : field.Name;
        }

        /// <summary>
        /// Returns the value with the type of the enum value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static dynamic GetValue(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            Type type = Enum.GetUnderlyingType(value.GetType());

            return Convert.ChangeType(field.GetValue(value), type);
        }

        /// <summary>
        /// Returns true if enum matches any of the given values
        /// </summary>
        /// <param name="value">Value to match</param>
        /// <param name="values">Values to match against</param>
        /// <returns>Return true if matched</returns>
        public static bool In(this Enum value, params Enum[] values)
        {
            return values.Any(v => v == value);
        }
    }
}
