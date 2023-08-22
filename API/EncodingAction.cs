using System;
using System.Security.Cryptography;
using System.Text;

namespace API
{
    public static class EncodingAction
    {
        public static string GetMd5Hash (string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // 将输入字符串转换为字节数组并计算 MD5 哈希值
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // 创建一个 StringBuilder 对象以便存储哈希后的数据
                StringBuilder sBuilder = new StringBuilder();

                // 遍历哈希数据的每个字节并将其格式化为十六进制字符串
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // 返回格式化后的 MD5 哈希值
                return sBuilder.ToString();
            }
        }

        /// <summary>
        /// 返回一个字符串的数字
        /// </summary>
        /// <param name="_num"></param>
        /// <returns></returns>
        public static int GetNum (string _num)
        {
            int _temp;
            int.TryParse(_num, out _temp);
            return _temp;
        }

        /// <summary>
        /// 返回字符串的sha256值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetShA256 (string input)
        {
            return GetMd5Hash(input);
            //这里先不使用sha256
            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}