using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicCrawler.Crawling
{
    class Crawler
    {
        public string ThreadId { get; private set; }
        public string PageSourceCode { get; private set; }
        public CrawlerController Controller { get; private set; }

        public Crawler(string threadId, string pageSourceCode, CrawlerController controller)
        {
            ThreadId = threadId;
            PageSourceCode = pageSourceCode;
            Controller = controller;
        }

        private void CreateCrawlerInfoDone()
        {
            Logger.SafeWriteLine($"Done: Create Crawler-{ThreadId}");
        }
    }
}
