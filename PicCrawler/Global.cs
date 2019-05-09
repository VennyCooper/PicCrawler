using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicCrawler
{
    public static class GlobalMessages
    {
        #region Download
        // {0}: local file path from downloading
        public static string ONE_FILE_DOWNLOADED = "File downloaded as {0}";
        // {0}: actually downloaded file count, {1}: total file count, {2}: page uri
        public static string FILES_DOWNLOADED = "Totally {0} out of {1} files downloaded from page {2}";
        #endregion

        #region Error messages
        // {0}: file or directory name, {1}: file or directory path
        public static string FILE_OR_DIRECTORY_NOT_EXISTS = "Error: {0} {1} does not exist.";
        // {0}: uri
        public static string URI_IS_INVALID = "Invalid URI {0}";
        #endregion
    }
}
