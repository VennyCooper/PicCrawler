using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PicCrawler.Crawling
{
    class Client
    {
        public string DownloadDir { get; private set; }

        public WebClient WClient { get; private set; }

        public Client(string downloadDir)
        {
            DownloadDir = downloadDir;
            if (!Directory.Exists(DownloadDir))
            {
                Directory.CreateDirectory(DownloadDir);
            }
            
            WClient = new WebClient();
        }

        public void DownloadFile(string fileUri, string fileName)
        {
            try
            {
                string filePath = Path.Combine(DownloadDir, fileName);
                WClient.DownloadFile(fileUri, filePath);
                Logger.WriteLine(string.Format(GlobalMessages.ONE_FILE_DOWNLOADED, filePath));
            }
            catch (Exception e)
            {
                Logger.SafeWriteError(e.ToString());
            }
        }

        public void DownloadFiles(IEnumerable<string> fileUris, string pageUri)
        {
            fileUris = fileUris.Distinct();

            int uriCount = fileUris.Count();

            int validUriCount = 0;

            var uriMap = fileUris.Zip(Enumerable.Range(0, uriCount), (uri, name) => new { uri, name });

            foreach (var map in uriMap)
            {
                if (ValidateUri(map.uri))
                {
                    DownloadFile(map.uri, $"{map.name}{Path.GetExtension(map.uri)}");
                    ++validUriCount;
                }
                else
                {
                    Logger.SafeWriteError(string.Format(GlobalMessages.URI_IS_INVALID, map.uri));
                }
            }
            Logger.SafeWriteLine(string.Format(GlobalMessages.FILES_DOWNLOADED, validUriCount, uriCount, pageUri));
        }

        private bool ValidateUri(string uri)
        {
            return Uri.IsWellFormedUriString(uri, UriKind.Absolute) && Uri.TryCreate(uri, UriKind.Absolute, out _);
        }
    }
}
