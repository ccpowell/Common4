using System;

namespace DRCOG.Common.Util
{
	/// <summary>Extension method class containing generic parsing functionality.</summary>
	public static partial class GenericParsing
	{
		static GenericParsing()
		{
			SetParseMethod<string>(ParseString);
			//SetParseMethod<Guid>(ParseGuid);
			SetTryParseMethod<string>(TryParseString);
			//SetTryParseMethod<Guid>(TryParseGuid);
		}
	}
}
