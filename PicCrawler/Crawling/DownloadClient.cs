using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PicCrawler.Crawling
{
    class DownloadClient
    {
        public string ClientId { get; private set; }

        public string UriAddress { get; private set; }

        public string DownloadDir { get; private set; }

        protected string DownLoadDirSubFolderName { get; private set; }

        protected HtmlParser Parser { get; private set; }

        private int _validUriCount { get; set; } = 0;

        private int _downloadSuccessCount { get; set; } = 0;

        private int _downloadFailureCount { get; set; } = 0;

        public DownloadClient(string clientId, string uriAddress, string downloadDir, HtmlParser parser)
        {
            ClientId = clientId;

            DownloadDir = downloadDir;
            Directory.CreateDirectory(DownloadDir);

            Parser = parser;

            Sanity.Requires(Common.ValidateUri(uriAddress), string.Format(GlobalMessages.URI_IS_INVALID, uriAddress));

            UriAddress = uriAddress;

            Parser.LoadHtmlStream(UriAddress);
        }

        public void Run()
        {
            var fileUris = Parser.RunParser();
            DownLoadDirSubFolderName = Common.RemoveInvalidPathChars(fileUris.First());
            DownloadDir = Path.Combine(DownloadDir, DownLoadDirSubFolderName);
            Directory.CreateDirectory(DownloadDir);
            Logger.SafeWriteLine(GlobalMessages.CREATE_DOWNLOAD_TARGET_FOLDER, DownloadDir);
            fileUris = fileUris.Skip(1);
            DownloadFiles(fileUris);
        }

        private void DownloadFiles(IEnumerable<string> fileUris)
        {
            Logger.SafeWriteLine(GlobalMessages.START_DOWNLOADING_FROM, DownLoadDirSubFolderName);
            fileUris = fileUris.Distinct();
            int uriCount = fileUris.Count();
            var uriMap = fileUris.Zip(Enumerable.Range(0, uriCount), (uri, name) => new { uri, name });
            foreach (var map in uriMap)
            {
                if (Common.ValidateUri(map.uri))
                {
                    ++_validUriCount;
                    DownloadFile(map.uri, $"{map.name}{Path.GetExtension(map.uri)}");
                }
                else
                {
                    Logger.SafeWriteError(GlobalMessages.URI_IS_INVALID, map.uri);
                }
            }
            Logger.SafeWriteLine(GlobalMessages.DOWNLOAD_SUMMARY, uriCount.ToString(), 
                _validUriCount.ToString(), _downloadSuccessCount.ToString(), _downloadFailureCount.ToString());
            Logger.SafeWriteLine(GlobalMessages.FILES_DOWNLOADED, _downloadSuccessCount.ToString(), uriCount.ToString(), UriAddress);
        }

        private void DownloadFile(string fileUri, string fileName)
        {
            string filePath = Path.Combine(DownloadDir, fileName);
            try
            {
                Parser.WClient.DownloadFile(fileUri, filePath);
                Logger.WriteLine(GlobalMessages.ONE_FILE_DOWNLOADED, filePath);
                ++_downloadSuccessCount;
            }
            catch (Exception e)
            {
                ++_downloadFailureCount;
                Logger.SafeWriteError(e.ToString());
            }
        }
    }
}
