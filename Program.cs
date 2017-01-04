using System;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("==============");
            Console.WriteLine("开始。。。");
            Console.WriteLine("==============");
            var start = 1;
            for (; start <= 23; start++)
            {
                var imageSpider = new ImageSpider("http://img1.mm131.com/pic/2783/13.jpg", $"{start}");
                imageSpider.Spide();
            }
            Console.WriteLine("==============");
            Console.WriteLine("结束。。。");
            Console.WriteLine("==============");
            Console.ReadLine();
        }
    }
}
