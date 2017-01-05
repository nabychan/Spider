using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
namespace ConsoleApplication
{
    public class ImageSpider
    {
        public string Url { get; private set; }
        public string FileName { get; private set; }
        public ImageSpider(string url, string fileName)
        {
            Url = url;
            FileName = fileName;
        }

        public async void Spide()
        {
            try
            {
                var request = WebRequest.Create(Url);
                var response = await request.GetResponseAsync();
                var fileName = GetFileNameFromResponseOrUrl(Url, response);
                if (String.IsNullOrWhiteSpace(fileName)) fileName = FileName;
                using (var writer = File.OpenWrite($"ImageSpider-{fileName}.{response.ContentType.Substring(response.ContentType.IndexOf("/") >= 0 ? response.ContentType.IndexOf("/") + 1 : 0, response.ContentType.Length - (response.ContentType.IndexOf("/") >= 0 ? response.ContentType.IndexOf("/") + 1 : 0))}")
                )
                {
                    var bytes = new byte[1024];
                    var responseStream = response.GetResponseStream();
                    var size = responseStream.Read(bytes, 0, bytes.Length);
                    while (size > 0)
                    {
                        writer.Write(bytes, 0, size);
                        size = responseStream.Read(bytes, 0, bytes.Length);
                    }
                    writer.Flush();
                }
                Console.WriteLine(String.Join(",",((HttpWebResponse)response).Headers));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine(FileName);
        }

        private string GetFileNameFromResponseOrUrl(string url, WebResponse response)
        {
            var filename = GetFileNameFromResponse(response);
            if (String.IsNullOrWhiteSpace(filename))
            {
                filename = GetFileNameFromUrl(url);
            }
            return filename;
        }

        private string GetFileNameFromUrl(string url)
        {
            if (url == null) return "";
            var filename =url.LastIndexOf("/")>=0? url.Substring(url.LastIndexOf("/") + 1):"";
            filename = filename.IndexOf("?") >= 0 ? filename.Substring(0, filename.IndexOf("?") + 1) : filename;
            return filename;
        }

        private string GetFileNameFromResponse(WebResponse response)
        {
            if (response == null) return "";
            var contentDisposition = response.Headers["Content-Disposition"] ?? "";
            var fileNameFlag = "filename=";
            var fileName = contentDisposition.LastIndexOf(fileNameFlag)>=0?contentDisposition.Substring(contentDisposition.LastIndexOf(fileNameFlag) + fileNameFlag.Length):"";
            return fileName;
        }
    }
}