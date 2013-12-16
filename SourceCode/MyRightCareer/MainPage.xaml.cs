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
        private string[,] content = new string[10,10];
        private List<List<List<UIElement>>> c = new List<List<List<UIElement>>>();

        public MainPage()
        {
            InitializeComponent();

            this.topBanner.SetMainPage(this);
            
            this.loader = new ContentLoader(this);
            this.loader.GetContent();
        }

        public void SetMenuButtons(List<Dictionary<string, string>> menuButtons)
        {
            this.topBanner.SetMenuButtons(menuButtons);
        }

        public void SetPageContents(int exercise, int step, string content)
        {
            //this.content[exercise, step] = content;
            /*if (c[exercise] == null)
            {
                c[exercise] = new List<List<object>>();
            }

            if (c[exercise][step] == null)
            {
                c[exercise][step] = new List<object>();
            }*/
            if (c.Count <= exercise)
            {
                c.Add(new List<List<UIElement>>());
            }

            if (c[exercise].Count <= step)
            {
                c[exercise].Add(new List<UIElement>());
            }

            TextBlock t = new TextBlock();
            t.Text = content;
            c[exercise][step].Add(t);
        }

        public void LoadPageContents(int exercise, int step)
        {
            this.contentPanel.Children.Clear();
            foreach(UIElement o in this.c[exercise][step])
            {
                //TextBlock t = new TextBlock();
                //t.Text = "Element";
                this.contentPanel.Children.Add(o);
            }
            
            /*string text = this.content[exercise, step];
            string[] boldText = text.Split(new string[] {"**"},StringSplitOptions.None);

            this.contentBlock.Inlines.Clear();

            for (int i = 0; i < boldText.Length; i++)
            {
                if (i % 2 == 0)
                {
                    string[] italicizedText = boldText[i].Split(new string[] { "#" }, StringSplitOptions.None);

                    for (int j = 0; j < italicizedText.Length; j++)
                    {
                        if (j % 2 == 0)
                        {
                            string[] underlinedText = italicizedText[j].Split(new string[] { "__" }, StringSplitOptions.None);

                            for (int k = 0; k < underlinedText.Length; k++)
                            {
                                if (k % 2 == 0)
                                {
                                    this.contentBlock.Inlines.Add(new Run() { Text = underlinedText[k]});
                                }
                                else
                                {
                                    this.contentBlock.Inlines.Add(new Run() { Text = underlinedText[k], TextDecorations = TextDecorations.Underline});
                                }
                            }
                        }
                        else
                        {
                            this.contentBlock.Inlines.Add(new Run() { Text = italicizedText[j], FontStyle = FontStyles.Italic });
                        }
                    }
                }
                else
                {
                    this.contentBlock.Inlines.Add(new Run() { Text = boldText[i], FontWeight = FontWeights.Bold });
                } 
            }
 //           this.contentBlock.Text = text;*/
        }
    }
}
