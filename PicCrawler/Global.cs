using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicCrawler
{
    public static class GlobalMessages
    {
        #region Initialization messages
        // {0}: log path
        public const string LOG_CREATED = "Done: Create log {0}";
        // {0}: error log path
        public const string ERROR_LOG_CREATED = "Done: Create error log {0}";
        #endregion

        #region Crawling
        // {0}: crawler id, {1}: uri to crawl
        public const string START_CRAWLING = "{0} starts crawling {1}";
        // {0}: target job name
        public const string SAVE_TO_TARGET = "Save files as {0}";
        // {0}: target folder path
        public const string CREATE_DOWNLOAD_TARGET_FOLDER = "Done: Create subfolder {0}";
        #endregion

        #region Download
        // {0}: download source name
        public const string START_DOWNLOADING_FROM = "Start downloading files from {0}";
        // {0}: local file path from downloading
        public const string ONE_FILE_DOWNLOADED = "File downloaded as {0}";
        // {0}: actually downloaded file count, {1}: total file count, {2}: page uri
        public const string FILES_DOWNLOADED = "Totally {0} out of {1} files downloaded from page {2}";
        // {0}: total file count, {1}: valid file uri count, {2}: actually downloaded file count, {3}: count of files failed to be downloaded
        public const string DOWNLOAD_SUMMARY = "Total = {0}, Valid = {1}, Downloaded = {2}, FailedToDownload = {3}";
        #endregion

        #region Error messages
        // {0}: file or directory name, {1}: file or directory path
        public const string FILE_OR_DIRECTORY_NOT_EXISTS = "Error: {0} {1} does not exist.";
        // {0}: uri
        public const string URI_IS_INVALID = "Invalid URI {0}";
        #endregion
    }

    public static class GlobalFormaters
    {
        public const string CONFIGURATION_INDENTION = "------";
    }
}
