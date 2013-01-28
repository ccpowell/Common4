using System;

namespace DRCOG.Common.Util
{
	partial class GenericParsing
	{
		/// <summary>Converts the string representation of the specified type to its equivalent of the specified type, returning a default value if conversion fails.</summary>
		/// <param name="s">A string containing the value to convert.</param>
		/// <param name="type">The type to which the string is to be converted.</param>
		/// <param name="defaultValue">The default value to use when conversion fails.</param>
		/// <returns>A value of the specified type equivalent to the value in <paramref name="s"/>; if conversion fails, the value of <paramref name="defaultValue"/> is returned.</returns>
		public static object ParseDefault(this string s, Type type, object defaultValue)
		{
			if (type == null)
				throw new ArgumentNullException("type");

			CheckDefault(type, defaultValue);

			object result = null;

			if (!TryParse(s, type, out result))
				return defaultValue;

			return result;
		}

		/// <summary>Converts the string representation of type <typeparamref name="T"/> to its equivalent of type <typeparamref name="T"/>.</summary>
		/// <typeparam name="T">The type of the value to convert.</typeparam>
		/// <param name="s">A string containing the value to convert.</param>
		/// <param name="defaultValue">The default value to use when conversion fails.</param>
		/// <returns>A value of type <typeparamref name="T"/> equivalent to the value in <paramref name="s"/>; if conversion fails, the value of <paramref name="defaultValue"/> is returned.</returns>
		public static T ParseDefault<T>(this string s, T defaultValue)
		{
			return (T)ParseDefault(s, typeof(T), defaultValue);
		}

		private static void CheckDefault(Type type, object defaultValue)
		{
			bool isValid = false;

			if (type.IsValueType)
			{
				Type underlyingType = Nullable.GetUnderlyingType(type);
				if (underlyingType != null)
				{
					if (defaultValue == null)
					{
						isValid = true;
					}
					else
					{
						Type defaultValueType = defaultValue.GetType();
						if (underlyingType == (Nullable.GetUnderlyingType(defaultValueType) ?? defaultValueType))
							isValid = true;
					}
				}
				else
				{
					if (type == defaultValue.GetType())
						isValid = true;
				}
			}
			else
			{
				if (defaultValue == null || type.IsAssignableFrom(defaultValue.GetType()))
					isValid = true;
			}

			if (!isValid)
				throw new ArgumentException("The default value cannot be assigned to a variable of the specified type.", "defaultValue");
		}
	}
}
