using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicCrawler
{
    class Program
    {
        public static bool ValidateUriAccess(string uri)
        {
            return Uri.IsWellFormedUriString(uri, UriKind.Absolute) && Uri.TryCreate(uri, UriKind.Absolute, out _);
        }

        static void Main(string[] args)
        {
            var a = ValidateUriAccess("https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1494677827304&di=8e8aaf1a717ae37b73b772ee4728c7ea&imgtype=0&src=http%3A%2F%2Fscimg.jb51.net%2Fallimg%2F141123%2F10-1411231F92W16.jpg");
        }
    }
}
