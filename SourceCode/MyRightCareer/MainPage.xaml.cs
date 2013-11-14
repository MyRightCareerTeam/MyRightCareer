using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MyRightCareer
{
    public partial class MainPage : UserControl
    {
        private ContentLoader loader;
        private string[][] content;

        public MainPage()
        {
            InitializeComponent();

            this.loader = new ContentLoader(this);
            this.loader.GetContent();
        }

        public void SetMenuButtons(List<Dictionary<string, string>> menuButtons)
        {
            this.topBanner.SetMenuButtons(menuButtons);
        }

        public void SetPageContents(int exercise, int step, string content)
        {
            this.content[exercise][step] = content;
        }
    }
}
