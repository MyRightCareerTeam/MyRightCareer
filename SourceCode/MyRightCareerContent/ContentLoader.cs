using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace MyRightCareerContent
{
    public class ContentLoader
    {
        private static string contentFolderLocation = @"C:\Program Files";
        private Dictionary<string, XDocument> contents;

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

        public XDocument ParseXMLtoNode(String docPath)
        {
            return XDocument.Parse(docPath);   
        }

        public XDocument getPage(string pageName)
        {
            return this.contents[pageName];
        }
    }
}
