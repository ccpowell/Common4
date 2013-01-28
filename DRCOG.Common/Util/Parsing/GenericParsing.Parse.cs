using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace DRCOG.Common.Util
{
	/// <summary>Delegate type defining the generic Parse method signature.</summary>
	/// <typeparam name="T">The type of the value to convert.</typeparam>
	/// <param name="s">A string containing the value to convert.</param>
	/// <returns>A value of the specified type equivalent to the value in <paramref name="s"/>.</returns>
	public delegate T ParseDelegate<T>(string s);

	partial class GenericParsing
	{
		private static readonly Dictionary<Type, MethodInfo> _parseMethods = new Dictionary<Type, MethodInfo>();


		/// <summary>Converts the string representation of the specified type to its equivalent of the specified type.</summary>
		/// <param name="s">A string containing the value to convert.</param>
		/// <param name="type">The type to which the string is to be converted.</param>
		/// <returns>A value of the specified type equivalent to the value in <paramref name="s"/>.</returns>
		public static object Parse(this string s, Type type)
		{
			if (type == null)
				throw new ArgumentNullException("type");

			MethodInfo method = GetParseMethod(type);
			if (method == null)
				throw new Exception(string.Format("No suitable Parse method found for type '{0}'.", type.FullName));



			return method.Invoke(null, new object[] { s });
		}

		/// <summary>Converts the string representation of type <typeparamref name="T"/> to its equivalent of type <typeparamref name="T"/>.</summary>
		/// <typeparam name="T">The type of the value to convert.</typeparam>
		/// <param name="s">A string containing the value to convert.</param>
		/// <returns>A value of the specified type equivalent to the value in <paramref name="s"/>.</returns>
		public static T Parse<T>(this string s)
		{
            
			return (T)Parse(s, typeof(T));
		}

		/// <summary>Supplies a method to parse type <typeparamref name="T"/>.</summary>
		/// <typeparam name="T">The type that the method is able to convert.</typeparam>
		/// <param name="method">A method that can parse type <typeparamref name="T"/>.</param>
		public static void SetParseMethod<T>(MethodInfo method)
		{
			if (method == null)
				throw new ArgumentNullException("method");

			Type type = typeof(T);

			if (GetParseMethod(type) != null)
				throw new Exception(string.Format("The type '{0}' has a TryParse method available. Either the type defines one or one has already been explicitly provided.", type.FullName));

			if (!HasParseSignature(method, type))
				throw new Exception("The provided method does not match the required signature.");

			_parseMethods[type] = method;
		}

		/// <summary>Supplies a method to parse type <typeparamref name="T"/>.</summary>
		/// <typeparam name="T">The type that the method is able to convert.</typeparam>
		/// <param name="parseDelegate">A method that can parse type <typeparamref name="T"/>.</param>
		public static void SetParseMethod<T>(ParseDelegate<T> parseDelegate)
		{
			if (parseDelegate == null)
				throw new ArgumentNullException("parseDelegate");

			SetParseMethod<T>(parseDelegate.Method);
		}

		private static MethodInfo GetParseMethod(Type type)
		{
			if (!_parseMethods.ContainsKey(type))
			{
				lock (((ICollection)_parseMethods).SyncRoot)
				{
					if (!_parseMethods.ContainsKey(type))
					{
						_parseMethods.Add(type, FindParseMethod(type));
					}
				}
			}

			return _parseMethods[type];
		}

		private static MethodInfo FindParseMethod(Type type)
		{
			BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Static;
			Type[] parameterTypes = new Type[] { typeof(string) };

			MethodInfo method = type.GetMethod("Parse", bindingFlags, null, parameterTypes, null);
			if (method == null)
				return null;

			if (!HasParseSignature(method, type))
				return null;

			return method;
		}

		private static bool HasParseSignature(MethodInfo method, Type type)
		{
			if (method == null)
				throw new ArgumentNullException("method");

			if (type == null)
				throw new ArgumentNullException("type");

			if (method.ContainsGenericParameters || !method.IsStatic || method.ReturnType != type)
				return false;

			ParameterInfo[] parameters = method.GetParameters();

			return parameters.Length == 1 && parameters[0].ParameterType == typeof(string);
		}

		private static string ParseString(string s)
		{
			return s;
		}

		private static Guid ParseGuid(string s)
		{
			return new Guid(s);
		}
	}
}
