using IndexBlock.Common.Enum;
using System.ComponentModel;
using System.Globalization;
using System.Numerics;

namespace IndexBlock.Common.Extensions
{
    public static class MethodExtension
    {
        public static string GetEnumDescription(object value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Any() ? attributes.First().Description : value.ToString();
        }

        public static string ToJsonMethodName(this RpcMethod value)
        {
            return GetEnumDescription(value);
        }
        public static string ToHexString(this int value)
        {
            return string.Format("0x{0}", value.ToString("X").ToLower());
        }
        public static string ToHexString(this BigInteger value)
        {
            return string.Format("0x{0}", value.ToString("X").ToLower());
        }

        public static int ToNumberInt(this string value)
        {
            return Convert.ToInt32(value, 16);
        }
        public static decimal ToNumberDecimal(this string value)
        {
            return Convert.ToDecimal(value);
        }
        public static ulong ToNumberLong(this string value)
        {
            return Convert.ToUInt64(value, 16);
        }
        public static BigInteger ToNumberBigInteger(this string value)
        {
            BigInteger tmpValue;
            var success = BigInteger.TryParse(value, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out tmpValue);

            if (!success)
                tmpValue = BigInteger.Parse(value.Substring(2), NumberStyles.HexNumber);

            if (tmpValue < 0)
                tmpValue = BigInteger.Parse("0" + value.Substring(2), NumberStyles.HexNumber);

            return tmpValue;
        }
    }
}
