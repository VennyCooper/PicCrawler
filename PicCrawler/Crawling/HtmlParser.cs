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
        protected Uri TargetUri { get; private set; }
        protected Stream HtmlStream { get; private set; }
        
        protected HtmlParser(Uri rootUri, Stream htmlStream)
        {
            TargetUri = rootUri;
            HtmlStream = htmlStream;

            ConfigureParserInfoDone();
        }

        protected abstract void ConfigureParserInfoDone();
    }
}
