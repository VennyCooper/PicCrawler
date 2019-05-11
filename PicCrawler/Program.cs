using System;
using System.Collections.Generic;
using PicCrawler.Crawling;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PicCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            string workDir = @"";
            string runDir = Path.Combine(workDir, $"run_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}");
            string logDir = runDir;
            string uri = "";
            
            Logger.ConfigureLogger(logDir);
            try
            {
                DownloadClient client = new DownloadClient("1", uri, runDir, new PicWebParser());
                Crawler crawler = new Crawler("1", new CrawlingController(0, 0, 0), client);
                crawler.StartCrawling();
            }
            catch (Exception e)
            {
                Logger.SafeWriteError(e.ToString());
            }
            
        }
    }
}
