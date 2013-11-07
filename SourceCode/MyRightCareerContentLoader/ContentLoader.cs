using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace MyRightCareerContentLoader
{
    public class ContentLoader
    {
        private static string contentFolderLocation = @"C:\Program Files";

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
    }
}
