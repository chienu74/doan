

namespace doan.Utilities
{
    public class Functions
    {
        public static string TitleSlugGeneration(string type, string alias, long id)
        {
            string sTitle = type+"-"+SlugGenerator.SlugGenerator.GenerateSlug(alias)+"-"+id.ToString()+".html";
            return sTitle;
        }
        public static string getCurrentDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
