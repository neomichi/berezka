using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Berezka.Data.EnumType;
using Berezka.Data.Model;
using static System.Net.WebRequestMethods;

namespace Berezka.Data
{
    public static class Helper
    {       

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source != null && toCheck != null && source.IndexOf(toCheck, comp) >= 0;
        }

        public static UserClaims GetClaims(this ClaimsPrincipal user)
        {
            return new UserClaims(user);
        }

        public static string StrToHash(this string value)
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(value))
            {
                var sha1 = SHA512.Create();
                var inputBytes = Encoding.UTF8.GetBytes(value);
                var hash = sha1.ComputeHash(inputBytes);

                for (var i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
            }
            return sb.ToString();
        }
        public static DateTime GetTimeZone(TimeSpan? timeSpan=null)
        { var result = DateTime.UtcNow;
            if (timeSpan.HasValue) result += timeSpan.Value;
            return result;
        }
        /// <summary>
        /// сравнение строки ignore case
        /// </summary>
        /// <param name="value1">строка</param>
        /// <param name="value2">строка</param>
        /// <returns></returns>
        public static bool EQ(this string value1, string value2)
        {  
            return value1.Equals(value2, StringComparison.OrdinalIgnoreCase);
            //return value1.ToLower() == value2.ToLower() ? true : false;            
        }

        //public static string GenerateRefreshToken(Guid UserId, int minutes,  string securityKey)
        //{
        //    var date = DateTime.UtcNow.AddMinutes(minutes);
        //    var str = new StringBuilder(UserId.ToString("N"))
        //        .Append(',').Append(date.FullParce())
        //        .Append(',').Append(securityKey).ToString();
        //    return RsaHelper.Encryption(str);
        //}



        /// <summary>
        /// Unix timestamp to datetime
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static int ToInt(this string value)
        {
            int result = 0;
            var pattern = @"\d+";
            if (!string.IsNullOrWhiteSpace(value))
            {
                result = Regex.Match(value, pattern).Success ? int.Parse(Regex.Match(value, pattern).Value) : 0;

            }
            return result;
        }

        // public static object DateTimeFullParce(string v)
        // {
        //
        //     return new Onject();
        // }

        public static Guid ToGuid(this string val)
        {
            Guid result;
            Guid.TryParse(val, out result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string FullToString(this DateTime val)
        {
            return val.ToString("yyyyMMddHHmmssffff", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static DateTime ParceFullToDateTime(this string val)
        {
            return DateTime.ParseExact(val, "yyyyMMddHHmmssffff", CultureInfo.InvariantCulture);
        }

        public static string ObjIsString(this Object val)
        {
            var result = "";
            if (val is string) result = (string)val;

            return result;
        }

      

        public static Dictionary<int,string> EnumToDic<T>() where T : System.Enum
        {
            

            var result = new Dictionary<int, string>();
            var values = Enum.GetValues(typeof(T));
            foreach (int item in values)
                result.Add(item, Enum.GetName(typeof(T), item));
            return result;

            //return Enum.GetValues(typeof(val)).Cast<val>()
            //    .ToDictionary(x => (int)x, x => x.ToString());
        }

        public static ValueTask<int> AddAsync(int a, int b)
        {
            return new ValueTask<int>(a + b);
        }




    }
}
