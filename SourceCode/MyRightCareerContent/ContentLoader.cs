using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Xml;
//using System.Xml.Linq;

namespace MyRightCareer
{
    public class ContentLoader
    {
        private static string contentFolderLocation = @"C:\Program Files";
        //private Dictionary<string, XDocument> contents;

        public static void GetXMLContent()
        {
            WebClient client = new WebClient();
            //client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(client_OpenReadCompleted);
            client.OpenReadAsync(new Uri("content.xml", UriKind.Relative));
            //client.DownloadStringAsync(new Uri("content.xml", UriKind.Relative));
        }

        private static void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Result);
        }

        static void client_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            StreamReader myReader = new StreamReader(e.Result);
            String result = myReader.ReadToEnd();
            System.Diagnostics.Debug.WriteLine(result);
            myReader.Close();

            ParseXML(result);
        }

        static void ParseXML(string xmlString)
        {
            System.Diagnostics.Debug.WriteLine("Parsing");

            XmlReader reader = XmlReader.Create(new StringReader(xmlString));

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        System.Diagnostics.Debug.WriteLine("ELEMENT");
                        System.Diagnostics.Debug.WriteLine(reader.Name);
                        break;
                    case XmlNodeType.Text:
                        System.Diagnostics.Debug.WriteLine("TEXT");
                        System.Diagnostics.Debug.WriteLine(reader.Value);
                        break;
                    case XmlNodeType.XmlDeclaration:
                    case XmlNodeType.ProcessingInstruction:
                        System.Diagnostics.Debug.WriteLine("PROCESSING INSTRUCTION");
                        System.Diagnostics.Debug.WriteLine(reader.Name, reader.Value);
                        break;
                    case XmlNodeType.Comment:
                        System.Diagnostics.Debug.WriteLine("COMMENT");
                        System.Diagnostics.Debug.WriteLine(reader.Value);
                        break;
                    case XmlNodeType.EndElement:
                        System.Diagnostics.Debug.WriteLine("END");
                        break;
                }
            }
        }

        public static List<Dictionary<string, string>> GetMenuButtons()
        {
            List<Dictionary<string, string>> menuButtons = new List<Dictionary<string, string>>();
            
            DirectoryInfo contentDirectory = new DirectoryInfo(contentFolderLocation);
            List<string> subDirectories = new List<string>();
            
            foreach (DirectoryInfo subDirectory in contentDirectory.EnumerateDirectories())
            {
                subDirectories.Add(subDirectory.Name);
                Console.WriteLine(subDirectory.Name);
            }
            
            subDirectories.Sort();

            return menuButtons;
        }
        /*
        public XDocument ParseXMLtoNode(String docPath)
        {
            return XDocument.Parse(docPath);   
        }

        public XDocument getPage(string pageName)
        {
            return this.contents[pageName];
        }*/
    }
}
