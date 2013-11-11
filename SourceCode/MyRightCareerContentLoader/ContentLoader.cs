using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Xml;

namespace MyRightCareerContentLoader
{
    public class ContentLoader
    {
        private static string contentFolderLocation = @"C:\Program Files";
        private Dictionary<string, XmlNode> contents;

        public ContentLoader()
        {

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

        public XmlNode ParseXMLtoNode(String docPath)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(docPath);
            XmlNodeReader nodereader = new XmlNodeReader(doc);
            XmlNode page = doc.SelectSingleNode("/page");
            return page;
        }

        public XmlNode getPage(string pageName)
        {
            return this.contents[pageName];
        }
    }
}
