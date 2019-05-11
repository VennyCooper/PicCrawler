using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PicCrawler
{
    class Common
    {
        public static Stream GetPageHtml(string uri)
        {
            Stream htmlStream = null;
            WebClient webClient = new WebClient();
            try
            {
                htmlStream = webClient.OpenRead(uri);
            }
            catch (Exception e)
            {
                Logger.WriteLine(e.ToString());
                throw;
            }
            return htmlStream;
        }

        public static string RemoveInvalidPathChars(string inputPath)
        {
            char[] invalidPathChars = Path.GetInvalidPathChars()
                .Concat(new char[]{ '\\', '/', '?', '|', ':', '<', '>', '*', '\"' }).Distinct().ToArray();
            return string.Join(string.Empty, inputPath.Where(x => !invalidPathChars.Contains(x)));
        }

        public static bool ValidateUri(string uri)
        {
            return Uri.IsWellFormedUriString(uri, UriKind.Absolute) && Uri.TryCreate(uri, UriKind.Absolute, out _);
        }
    }
}
