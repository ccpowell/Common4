using System;

namespace DRCOG.Common.Util
{
	partial class GenericParsing
	{
		/// <summary>Converts the string representation of the specified type to its equivalent of the specified type, parsing nullable value types as the value type.</summary>
		/// <param name="s">A string containing the value to convert.</param>
		/// <param name="type">The type to which the string is to be converted.</param>
		/// <returns>A value of the specified type equivalent to the value in <paramref name="s"/>.</returns>
		/// <remarks>Nullable types are parsed as the underlying value type.  Enum types are parsed using the Enum helper functions.</remarks>
		public static object SmartParse(this string s, Type type)
		{
			if (type == null)
				throw new ArgumentNullException("type");

			UnwrapNullableType(ref type);

			if (type.IsEnum)
			{
				if (s == null)
					throw new ArgumentNullException("s");

				return Enum.Parse(type, s);
			}

			return Parse(s, type);
		}

		/// <summary>Converts the string representation of type <typeparamref name="T"/> to its equivalent of type <typeparamref name="T"/>, parsing nullable value types as the value type.</summary>
		/// <typeparam name="T">The type of the value to convert.</typeparam>
		/// <param name="s">A string containing the value to convert.</param>
		/// <returns>A value of the specified type equivalent to the value in <paramref name="s"/>.</returns>
		/// <remarks>Nullable types are parsed as the underlying value type.  Enum types are parsed using the Enum helper functions.</remarks>
		public static T SmartParse<T>(this string s)
		{
            if (String.IsNullOrEmpty(s)) return default(T);
			return (T)SmartParse(s, typeof(T));
		}

		/// <summary>Converts the string representation of the specified type to its equivalent of the specified type.  A return value indicates whether the conversion succeeded.</summary>
		/// <param name="s">A string containing the value to convert.</param>
		/// <param name="type">The type to which the string is to be converted.</param>
		/// <param name="result">When this method returns, contains the value converted from <paramref name="s"/> if the conversion succeeded, or the default declared value for the type if the conversion failed.</param>
		/// <returns>true if <paramref name="s"/> was converted successfully; otherwise, false.</returns>
		/// <remarks>Nullable types are parsed as the underlying value type.  Enum types are parsed using the Enum helper functions.</remarks>
		public static bool SmartTryParse(this string s, Type type, out object result)
		{
			if (type == null)
				throw new ArgumentNullException("type");

			UnwrapNullableType(ref type);

			if (type.IsEnum)
			{
				if (s == null || !Enum.IsDefined(type, s))
				{
					result = null;
					return false;
				}

				result = Enum.Parse(type, s);
				return true;
			}

			return TryParse(s, type, out result);
		}

		/// <summary>Converts the string representation of type <typeparamref name="T"/> to its equivalent of type <typeparamref name="T"/>.  A return value indicates whether the conversion succeeded.</summary>
		/// <typeparam name="T">The type of the value to convert.</typeparam>
		/// <param name="s">A string containing the value to convert.</param>
		/// <param name="result">When this method returns, contains the value converted from <paramref name="s"/> if the conversion succeeded, or the default declared value for the type if the conversion failed.</param>
		/// <returns>true if <paramref name="s"/> was converted successfully; otherwise, false.</returns>
		/// <remarks>Nullable types are parsed as the underlying value type.  Enum types are parsed using the Enum helper functions.</remarks>
		public static bool SmartTryParse<T>(this string s, out T result)
		{
			result = default(T);
			object temp;
			bool success = SmartTryParse(s, typeof(T), out temp);
			if (success)
				result = (T)temp;
			return success;
		}

		/// <summary>Converts the string representation of the specified type to its equivalent of the specified type, returning a default value if conversion fails.</summary>
		/// <param name="s">A string containing the value to convert.</param>
		/// <param name="type">The type to which the string is to be converted.</param>
		/// <param name="defaultValue">The default value to use when conversion fails.</param>
		/// <returns>A value of the specified type equivalent to the value in <paramref name="s"/>; if conversion fails, the value of <paramref name="defaultValue"/> is returned.</returns>
		/// <remarks>Nullable types are parsed as the underlying value type.  Enum types are parsed using the Enum helper functions.</remarks>
		public static object SmartParseDefault(this string s, Type type, object defaultValue)
		{
			if (type == null)
				throw new ArgumentNullException("type");

			CheckDefault(type, defaultValue);

			object result = null;

			if (SmartTryParse(s, type, out result))
				return result;

			return defaultValue;
		}

		/// <summary>Converts the string representation of type <typeparamref name="T"/> to its equivalent of type <typeparamref name="T"/>.</summary>
		/// <typeparam name="T">The type of the value to convert.</typeparam>
		/// <param name="s">A string containing the value to convert.</param>
		/// <param name="defaultValue">The default value to use when conversion fails.</param>
		/// <returns>A value of type <typeparamref name="T"/> equivalent to the value in <paramref name="s"/>; if conversion fails, the value of <paramref name="defaultValue"/> is returned.</returns>
		/// <remarks>Nullable types are parsed as the underlying value type.  Enum types are parsed using the Enum helper functions.</remarks>
		public static T SmartParseDefault<T>(this string s, T defaultValue)
		{
			return (T)SmartParseDefault(s, typeof(T), defaultValue);
		}

		private static void UnwrapNullableType(ref Type type)
		{
			Type underlyingType = Nullable.GetUnderlyingType(type);
			if (underlyingType != null)
				type = underlyingType;
		}
	}
}
