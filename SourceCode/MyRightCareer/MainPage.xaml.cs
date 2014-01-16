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
        private const string BOLD_CHAR = "**";
        private const string ITALICIZED_CHAR = "##";
        private const string UNDERLINED_CHAR = "__";
        
        private ContentLoader loader;
        private List<List<List<UIElement>>> content = new List<List<List<UIElement>>>();

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

        public void SetPageContents_Text(int exercise, int step, string content)
        {
            this.CheckContentSize(exercise, step);

            TextBlock t = this.ParseText(content);

            this.content[exercise][step].Add(t);
        }

        public void SetPageContents_Image(int exercise, int step, string source)
        {
            this.CheckContentSize(exercise, step);

            Image i = new Image();
            
            i.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/images/" + source, UriKind.Relative));
            i.Stretch = Stretch.None;

            this.content[exercise][step].Add(i);
        }

        public void SetPageContents_Video(int exercise, int step, string source)
        {
            this.CheckContentSize(exercise, step);

            MediaElement v = new MediaElement();
            
            v.Source = new Uri("/videos/" + source, UriKind.Relative);
            v.Stretch = Stretch.None;
            
            this.content[exercise][step].Add(v);
        }

        public void LoadPageContents(int exercise, int step)
        {
            this.contentPanel.Children.Clear();

            foreach(UIElement o in this.content[exercise][step])
            {
                this.contentPanel.Children.Add(o);
            }
            // create a new image
            Image image = new Image();
            image.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/Koala.jpg", UriKind.Relative));
            this.contentPanel.Children.Add(image);
        }

        private void CheckContentSize(int exercise, int step)
        {
            if (this.content.Count <= exercise)
            {
                this.content.Add(new List<List<UIElement>>());
            }

            if (this.content[exercise].Count <= step)
            {
                this.content[exercise].Add(new List<UIElement>());
            }
        }

        private TextBlock ParseText(string text)
        {
            TextBlock block = new TextBlock();

            string[] boldText = text.Split(new string[] { BOLD_CHAR }, StringSplitOptions.None);

            for (int i = 0; i < boldText.Length; i++)
            {
                if (i % 2 == 0)
                {
                    string[] italicizedText = boldText[i].Split(new string[] { ITALICIZED_CHAR }, StringSplitOptions.None);

                    for (int j = 0; j < italicizedText.Length; j++)
                    {
                        if (j % 2 == 0)
                        {
                            string[] underlinedText = italicizedText[j].Split(new string[] { UNDERLINED_CHAR }, StringSplitOptions.None);

                            for (int k = 0; k < underlinedText.Length; k++)
                            {
                                if (k % 2 == 0)
                                {
                                    block.Inlines.Add(new Run() { Text = underlinedText[k] });
                                }
                                else
                                {
                                    block.Inlines.Add(new Run() { Text = underlinedText[k], TextDecorations = TextDecorations.Underline });
                                }
                            }
                        }
                        else
                        {
                            block.Inlines.Add(new Run() { Text = italicizedText[j], FontStyle = FontStyles.Italic });
                        }
                    }
                }
                else
                {
                    block.Inlines.Add(new Run() { Text = boldText[i], FontWeight = FontWeights.Bold });
                }
            }

            return block;
        }
    }
}
