using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicCrawler.Crawling
{
    class Crawler
    {
        public string CrawlerId { get; private set; }

        public CrawlingController Controller { get; private set; }

        public DownloadClient CrawlerClient { get; private set; }

        public Crawler(string crawlerId, CrawlingController controller, DownloadClient crawlerClient)
        {
            CrawlerId = $"Crawler-{crawlerId}";
            Controller = controller;
            CrawlerClient = crawlerClient;

            CreateCrawlerInfoDone();
        }

        private void CreateCrawlerInfoDone()
        {
            Logger.SafeWriteLine($"Done: Create {CrawlerId}, Client-{CrawlerClient.ClientId}");
            Logger.SafeWriteLine($"{GlobalFormaters.CONFIGURATION_INDENTION} CrawlingFrom = {CrawlerClient.UriAddress}");
            Logger.SafeWriteLine($"{GlobalFormaters.CONFIGURATION_INDENTION} SaveTo = {CrawlerClient.DownloadDir}");
        }

        public void StartCrawling()
        {
            Logger.SafeWriteLine(GlobalMessages.START_CRAWLING, CrawlerId, CrawlerClient.UriAddress);
            CrawlerClient.Run();
        }
    }
}
