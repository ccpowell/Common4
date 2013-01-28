using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace DRCOG.Common.Util
{
    public static class EnumHelper
    {
        public static List<T> EnumToList<T>()
        {
            Type enumType = typeof(T);

            // Can't use type constraints on value types, so have to do check like this
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            Array enumValArray = Enum.GetValues(enumType);

            List<T> enumValList = new List<T>(enumValArray.Length);

            foreach (int val in enumValArray)
            {
                enumValList.Add((T)Enum.Parse(enumType, val.ToString()));
            }

            return enumValList;
        }

        /// <summary>
        /// Returns the enum name with spaces between camel cased letters. 
        /// Example:  MyEnum.SystemAdministrator becomes "System Administrator".
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToPrettyString(this Enum value)
        {
            string[] camelCasedWords = Regex.Split(value.ToString(), @"(?<!^)(?=[A-Z])");
            StringBuilder sb = new StringBuilder();
            foreach (string word in camelCasedWords)
            {
                string element = (sb.Length == 0) ? word : " " + word;
                sb.Append(element);
            }
            return sb.ToString();
        }

        public static string ToPrettyLowerString(this Enum value)
        {
            return ToPrettyString(value).ToLower();
        }

        /// <summary>
        /// Returns the description attribute for the enum if one exists. Otherwise, returns an empty string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToDescriptionString(this Enum value)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

        /// <summary>
        /// Validates the value against the real enumeration. Assumes underlying type is <c>int</c>.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <example><code>
        /// int someInt = 3; <br/>
        /// bool isValid = (MyEnum)someInt).IsValidEnumValue();
        /// </code></example>
        public static bool IsValidEnumValue(this Enum value)
        {
            int actualValue = (int)System.Enum.ToObject(value.GetType(), value);
            foreach (int enumValue in Enum.GetValues(value.GetType()))
            {
                if (enumValue == actualValue)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Validates the value against the real enumeration.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <example><code>
        /// int someInt = 3; <br/>
        /// bool isValid = (MyEnum)someInt).IsValidEnumValue&gt;int&lt;();
        /// </code></example>
        public static bool IsValidEnumValue<T>(this Enum value)
        {
            T actualValue = (T)System.Enum.ToObject(value.GetType(), value);
            foreach (T enumValue in Enum.GetValues(value.GetType()))
            {
                if (enumValue.Equals(actualValue))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Validates that name is a name in the enum</c>.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <example><code>
        /// int someInt = 3; <br/>
        /// bool isValid = (MyEnum)someInt).IsValidEnumValue();
        /// </code></example>
        public static bool IsValidEnumName(this Enum myEnum, string name)
        {
            //Enum.GetNames(typeof(HposAccEnum)).Contains<string>(val.ToString())
            return Enum.GetNames(myEnum.GetType()).Contains<string>(name);


            //int actualValue = (int)System.Enum.ToObject(value.GetType(), value);
            //foreach (int enumValue in Enum.GetValues(value.GetType()))
            //{
            //    if (enumValue == actualValue)
            //    {
            //        return true;
            //    }
            //}
            //return false;
        }

        /// <summary>
        /// Gets the underlying enum value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static T GetValue<T>(this Enum value)
        {
            return (T)Enum.ToObject(value.GetType(), value);
        }

        public static TEnum ParseAsEnum<TEnum>(this string x) where TEnum : struct
        {
            return ParseAsEnum<TEnum>(x, true);
        }

        public static TEnum ParseAsEnum<TEnum>(this string x, bool ignoreCase) where TEnum : struct
        {
            return (TEnum)Enum.Parse(typeof(TEnum), x, ignoreCase);
        }

        public static bool TryParse<TEnum>(string value, bool ignoreCase, out TEnum result) where TEnum : struct
        {
            result = default(TEnum);
            int intEnumValue;
            if (Int32.TryParse(value, out intEnumValue))
            {
                if (Enum.IsDefined(typeof(TEnum), intEnumValue))
                {
                    result = (TEnum)(object)intEnumValue;
                    return true;
                }
            }
            return false;
        }
    }
}
