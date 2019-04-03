using Framework.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace Framework.Web.Helpers
{
    public static class ActionResultHelper
    {
        public static FileResult ExportCsv(byte[] bytes, string name)
        {
            var result = new FileContentResult(bytes, HttpWebNames.CsvContentType)
            {
                FileDownloadName = $"{name}.csv"
            };

            return result;
        }

        public static FileResult ExportTxt(byte[] bytes, string name)
        {
            var result = new FileContentResult(bytes, HttpWebNames.CsvContentType)
            {
                FileDownloadName = $"{name}.txt"
            };

            return result;
        }
    }
}
