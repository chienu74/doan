

using System.Security.Cryptography;
using System.Text;

namespace doan.Utilities
{
    public class Functions
    {
        public static int _AccountID = 0;
        public static string _UserName = String.Empty;
        public static string _Email = String.Empty;

        public static string _Message = string.Empty;
        public static string _MessageEmail = string.Empty;
        public static string _Avatar = string.Empty;
    
        public static string TitleSlugGeneration(string type, string alias, long id)
        {
            string sTitle = type+"-"+SlugGenerator.SlugGenerator.GenerateSlug(alias)+"-"+id.ToString()+".html";
            return sTitle;
        }
        public static string getTime()
        {
            return DateTime.Now.ToString("dd-MM-yyyy");
        }
        public static string getCurrentDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public static string getDay()
        {
            return DateTime.Now.ToString("dd");
        }
        public static string getHour()
        {
            return DateTime.Now.ToString("HH");
        }
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                stringBuilder.Append(result[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        public static string MD5Password(string text)
        {
            string str = MD5Hash(text);
            for (int i = 0; i <= 5; i++)
                str = MD5Hash(str + "_" + str);
            return str;
        }

        public static bool IsLogin()
        {
            if (string.IsNullOrEmpty(Functions._UserName) || string.IsNullOrEmpty(Functions._Email) || (Functions._AccountID <= 0))
                return false;
            return true;
        }

    }
}
