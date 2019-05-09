using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PicCrawler;

namespace PicCrawler.Crawling
{
    class CrawlerController
    {
        // The max count for items that will be downloaded
        public int MaxDownloadedItemCount { get; private set; }

        // Sleep time after one thread finishes the current crawling job (1000 * N millisecond)
        public int CrawlerThreadSleepMillisecond { get; private set; }

        // Sleep time after one thread finishes the current crawling job (1000 * N millisecond)
        public int PageCrawlingSleepMilliSecond { get; private set; }

        public CrawlerController(int maxDownloadedItemCount, int crawlerThreadSleepMillisecond, int pageCrawlingSleepMillisecond)
        {
            MaxDownloadedItemCount = maxDownloadedItemCount;
            CrawlerThreadSleepMillisecond = crawlerThreadSleepMillisecond;
            PageCrawlingSleepMilliSecond = pageCrawlingSleepMillisecond;

            ConfigureCrawlerControllerDone();
        }

        private void ConfigureCrawlerControllerDone()
        {
            Logger.WriteLine($"Done: Configure crawler controller");
            Logger.WriteLine($"MaxDownloadedItemCount = {MaxDownloadedItemCount}");
            Logger.WriteLine($"CrawlerThreadSleepTime = {(double)CrawlerThreadSleepMillisecond / 1000} seconds");
            Logger.WriteLine($"PageCrawlingSleepTime = {(double)PageCrawlingSleepMilliSecond / 1000} seconds");
        }
    }
}
