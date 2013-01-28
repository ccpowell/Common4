using System;
using System.Text;
using DRCOG.Common.DesignByContract;

namespace DRCOG.Common.Util
{
    /// <summary>
    /// Utility class that helps with manipulating <see cref="Byte"/> arrays.
    /// </summary>
    public class ByteArrayHelper
    {                                   //
        private const char SEPARATOR = '_';

        /// <summary>
        /// Works with <see cref="ByteArrayHelper.ToString"/> to encode and decode <see cref="Byte"/> arrays
        /// </summary>
        /// <param name="str">The string to convert to a <see cref="Byte"/> array.</param>
        /// <returns>The <see cref="Byte"/> array stored in <paramref name="str"/></returns>
        public static Byte[] ToByteArray(String str)
        {
            Check.Require(!String.IsNullOrEmpty(str), "Cannot convert a null or empty string to a " + typeof(Byte[]).FullName);
            String[] stringBytes = str.Split(SEPARATOR);
            Byte[] bytes = new Byte[stringBytes.Length];

            for (int i = 0; i < stringBytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(stringBytes[i]);
            }
            return bytes;
        }

        /// <summary>
        /// Works with <see cref="ByteArrayHelper.ToByteArray"/> to encode and decode <see cref="Byte"/> arrays
        /// </summary>
        /// <param name="array">The array to convert to a string.</param>
        /// <returns>A string representation of the <paramref name="array"/></returns>
        public static String ToString(Byte[] array)
        {
            Check.Require(array != null && array.Length > 0, "Cannot convert a null or 0 length " + typeof(Byte[]).FullName + " to string");
            StringBuilder sb = new StringBuilder();
            foreach (Byte b in array)
            {
                sb.Append(b.ToString() + SEPARATOR);
            }
            //remove the last SEPARATOR
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        /// <summary>
        /// A byte by byte equals comparison for a <see cref="Byte"/> array
        /// </summary>
        /// <param name="array1">First byte array</param>
        /// <param name="array2">Second byte array</param>
        /// <returns>True if the bytes in each array are equal.</returns>
        public static bool Equals(Byte[] array1, Byte[] array2)
        {
            if (array1 == null || array2 == null)
            {
                return false;
            }

            if (array1.Length != array2.Length)
            {
                return false;
            }

            //Look at each byte
            for (int i = 0; i < array1.Length; i++)
            {
                if (!array1[i].Equals(array2[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
