using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace API
{
    public static class AccountAPI
    {
        public static List<char> VerificationCodeList = new List<char> { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        /// <summary>
        /// 检查邮箱地址的合法性，并将大写字母转换为小写字母
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static string CheckEmail (ref string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return "邮箱地址不能为空";
            }

            // 将email参数中的大写字母转换为小写字母
            email = email.ToLower();

            // 检查email参数是否符合邮箱格式
            Regex emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(email))
            {
                return "邮箱地址格式不正确";
            }
            return "邮箱地址合法";
        }

        public static string SplicingStatements (params string[] values)
        {
            return string.Join("&", values);
        }

        public static string CreatNew_VerificationCode ()
        {
            string _VerificationCode = "";
            for (int i = 1; i <= 4; i++)
            {
                _VerificationCode += VerificationCodeList[NewRandom.GetRandomInAB(1, 61)];
            }
            return _VerificationCode;
        }
    }
}