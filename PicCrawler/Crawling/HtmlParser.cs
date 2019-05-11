using PicCrawler;
using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PicCrawler.Crawling
{
    abstract class HtmlParser
    {
        protected string UriToParse { get; private set; }

        protected HtmlDocument RootHtmlDoc { get; private set; }

        protected HtmlNode DocNode { get; private set; }

        public WebClient WClient { get; private set; }

        public HtmlParser()
        {
            WClient = new WebClient();
        }

        public void LoadHtmlStream(string uriToParse)
        {
            UriToParse = uriToParse;
            RootHtmlDoc = new HtmlDocument();
            RootHtmlDoc.Load(Common.GetPageHtml(uriToParse));
            DocNode = RootHtmlDoc.DocumentNode;
        }

        public abstract IEnumerable<string> RunParser();

        protected abstract IEnumerable<string> GetFileUris();
    }
}
