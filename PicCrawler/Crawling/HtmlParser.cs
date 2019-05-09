using PicCrawler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PicCrawler.Crawling
{
    abstract class HtmlParser
    {
        protected Uri RootUri { get; private set; }
        protected Stream HtmlStream { get; private set; }
        
        protected HtmlParser(Uri rootUri, Stream htmlStream)
        {
            RootUri = rootUri;
            HtmlStream = htmlStream;

            ConfigureParserInfoDone();
        }

        protected abstract void ConfigureParserInfoDone();
    }
}
