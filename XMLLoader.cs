using System.Xml;
using System;

public class XMLLoader
{
    private XmlDocument Document;
    public void Load()
    {
        Document = new XmlDocument();
        Document.Load(new System.IO.FileStream("data.xml",System.IO.FileMode.Open));
    }

    public void Out()
    {
        Console.WriteLine("XML:"+Document.DocumentElement.InnerText);
    }
}