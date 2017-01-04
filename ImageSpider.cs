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
                using (var writer = File.OpenWrite($"ImageSpider-{FileName}.{response.ContentType.Substring(response.ContentType.IndexOf("/") >= 0 ? response.ContentType.IndexOf("/") + 1 : 0, response.ContentType.Length - (response.ContentType.IndexOf("/") >= 0 ? response.ContentType.IndexOf("/") + 1 : 0))}")
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine(FileName);
        }
    }
}