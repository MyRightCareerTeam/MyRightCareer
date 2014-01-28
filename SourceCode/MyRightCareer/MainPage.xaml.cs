using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Browser;
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
        private const string LINK_CHAR = "@@";
        
        private ContentLoader loader;
        private List<List<List<UIElement>>> content = new List<List<List<UIElement>>>();

        private bool isDragging = false;

        public MainPage()
        {
            InitializeComponent();

            this.topBanner.SetMainPage(this);
            
            this.loader = new ContentLoader(this);
            this.loader.GetContent();

            topContentBorder.MouseLeftButtonDown += topContentBorder_MouseLeftButtonDown;
            topContentBorder.MouseMove += topContentBorder_MouseMove;
            topContentBorder.MouseLeftButtonUp += topContentBorder_MouseLeftButtonUp;
        }

        void topContentBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.isDragging = true;

            this.topContentBorder.CaptureMouse();
        }

        void topContentBorder_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isDragging)
            {
                Point mousePosition = e.GetPosition(this.contentGrid);

                //this.topContentBorder.Height = mousePosition.Y;
                this.topContentBorder.SetValue(Border.HeightProperty, mousePosition.Y);
                GridLength top = new GridLength(mousePosition.Y / this.contentGrid.Height, GridUnitType.Star);
                GridLength bottom = new GridLength(1 - (mousePosition.Y / this.contentGrid.Height), GridUnitType.Star);
                this.contentGrid.RowDefinitions[0].Height = top;
                this.contentGrid.RowDefinitions[1].Height = bottom;
                this.contentGrid.RowDefinitions[0].SetValue(RowDefinition.HeightProperty, top);
                this.contentGrid.RowDefinitions[1].SetValue(RowDefinition.HeightProperty, bottom);
            }
        }

        void topContentBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.isDragging)
            {
                this.isDragging = false;

                this.topContentBorder.ReleaseMouseCapture();
            }
        }

        public void SetMenuButtons(List<Dictionary<string, string>> menuButtons)
        {
            this.topBanner.SetMenuButtons(menuButtons);
        }

        public void SetPageContents_Text(int assignment, int exercise, string content)
        {
            this.CheckContentSize(assignment, exercise);

            RichTextBlock t = this.ParseText(content);

            this.content[assignment][exercise].Add(t);
        }

        public void SetPageContents_Image(int assignment, int exercise, string source)
        {
            this.CheckContentSize(assignment, exercise);

            Image i = new Image();
            
            i.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("/images/" + source, UriKind.Relative));
            i.Stretch = Stretch.None;

            this.content[assignment][exercise].Add(i);
        }

        public void SetPageContents_Video(int assignment, int exercise, string source)
        {
            this.CheckContentSize(assignment, exercise);

            MediaElement v = new MediaElement();
            
            v.Source = new Uri("/videos/" + source, UriKind.Relative);
            v.Stretch = Stretch.None;

            VideoPlayer p = new VideoPlayer(new Uri("/videos/" + source, UriKind.Relative));

            this.content[assignment][exercise].Add(p);
        }

        public void LoadWebPage(string url)
        {
            HtmlElement frame = HtmlPage.Document.CreateElement("IFrame");
            frame.Id = "myIFrame";
            frame.SetAttribute("frameBorder", "0");
            frame.SetStyleAttribute("position", "absolute");
            frame.SetStyleAttribute("left", 0 + "px");
            frame.SetStyleAttribute("top", 20 + "%");
            frame.SetStyleAttribute("width", 90 + "%");
            frame.SetStyleAttribute("height", 42.5 + "%");

            frame.SetAttribute("src", url);  // Set src
            HtmlPage.Document.Body.AppendChild(frame);
        }

        public void LoadPageContents(int assignment, int exercise)
        {
            this.contentPanel.Children.Clear();

            foreach(UIElement o in this.content[assignment][exercise])
            {
                this.contentPanel.Children.Add(o);
            }
        }

        private void CheckContentSize(int assignment, int exercise)
        {
            if (this.content.Count <= assignment)
            {
                this.content.Add(new List<List<UIElement>>());
            }

            if (this.content[assignment].Count <= exercise)
            {
                this.content[assignment].Add(new List<UIElement>());
            }
        }

        private RichTextBlock ParseText(string text)
        {
            RichTextBlock block = new RichTextBlock();
            block.TextWrapping = TextWrapping.Wrap;
            Paragraph p = new Paragraph();

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
                                    //block.Inlines.Add(new Run() { Text = underlinedText[k] });
                                    //p.Inlines.Add(new Run() { Text = underlinedText[k] });

                                    string[] linkText = underlinedText[k].Split(new string[] { LINK_CHAR }, StringSplitOptions.None);

                                    for (int l = 0; l < linkText.Length; l++)
                                    {
                                        if (l % 2 == 0)
                                        {
                                            //block.Inlines.Add(new Run() { Text = underlinedText[k] });
                                            p.Inlines.Add(new Run() { Text = linkText[l] });
                                        }
                                        else
                                        {
                                            string[] link = linkText[l].Split(new string[] { "@" }, StringSplitOptions.None);

                                            if (link.Length == 2)
                                            {
                                                string displayText = link[0];
                                                string linkLocation = link[1];
                                            
                                                Hyperlink h = new Hyperlink();
                                                h.Inlines.Add(new Run() { Text = displayText });
                                                h.Click += (o, a) => { this.LoadWebPage(linkLocation); };
                                                //"http://www.onetonline.org/skills/"
                                                p.Inlines.Add(h);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    //block.Inlines.Add(new Run() { Text = underlinedText[k], TextDecorations = TextDecorations.Underline });
                                    p.Inlines.Add(new Run() { Text = underlinedText[k], TextDecorations = TextDecorations.Underline });
                                }
                            }
                        }
                        else
                        {
                            //block.Inlines.Add(new Run() { Text = italicizedText[j], FontStyle = FontStyles.Italic });
                            p.Inlines.Add(new Run() { Text = italicizedText[j], FontStyle = FontStyles.Italic });
                        }
                    }
                }
                else
                {
                    p.Inlines.Add(new Run() { Text = boldText[i], FontWeight = FontWeights.Bold });
                    //block.Inlines.Add(new Run() { Text = boldText[i], FontWeight = FontWeights.Bold });
                }
            }
            block.Blocks.Add(p);
            return block;
        }
    }
}
