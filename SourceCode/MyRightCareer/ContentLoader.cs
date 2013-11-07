using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MyRightCareer
{
    public class ContentLoader
    {
        //private static string contentFolderLocation = Directory.GetCurrentDirectory();
        //private static string contentFolderLocation = "http://" + Application.Current.Host.Source.Host + ":" + Application.Current.Host.Source.Port + "/ClientBin/Content";
        private static string contentFolderLocation = @"C:\Program Files";

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
