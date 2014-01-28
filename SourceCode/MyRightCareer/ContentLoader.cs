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
        public const string Title = "title";

        private MainPage mainPage;

        private enum Nodes
        {
            content,

            assignment,

            exercise,

            image,

            video
        }

        public ContentLoader(MainPage mainPage)
        {
            this.mainPage = mainPage;
        }

        public void GetContent()
        {
            WebClient client = new WebClient();
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(this.client_OpenReadCompleted);
            client.OpenReadAsync(new Uri("content.xml", UriKind.Relative));
        }

        private void client_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            StreamReader myReader = new StreamReader(e.Result);

            String result = myReader.ReadToEnd();

            myReader.Close();

            this.ParseXML(result);
        }

        private void ParseXML(string xmlString)
        {
            List<Dictionary<string, string>> menuButtons = new List<Dictionary<string,string>>();
            XmlReader reader = XmlReader.Create(new StringReader(xmlString));
            int currentAssignment = -1;
            int currentExercise = -1;

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name.Equals(Nodes.assignment.ToString()))
                        {
                            currentAssignment++;

                            menuButtons.Add(new Dictionary<string, string>());

                            string assignmentName = reader.GetAttribute(0);
                            menuButtons[currentAssignment].Add(Title, assignmentName);
                        }
                        else if (reader.Name.Equals(Nodes.exercise.ToString()))
                        {
                            currentExercise++;

                            string exerciseName = reader.GetAttribute(0);
                            menuButtons[currentAssignment].Add(currentExercise.ToString(), exerciseName);
                        }
                        else if (reader.Name.Equals(Nodes.image.ToString()))
                        {
                            this.mainPage.SetPageContents_Image(currentAssignment, currentExercise, reader.GetAttribute(0));
                        }
                        else if (reader.Name.Equals(Nodes.video.ToString()))
                        {
                            this.mainPage.SetPageContents_Video(currentAssignment, currentExercise, reader.GetAttribute(0));
                        }

                        break;
                    case XmlNodeType.Text:
                        this.mainPage.SetPageContents_Text(currentAssignment, currentExercise, reader.Value);

                        break;
                    case XmlNodeType.EndElement:
                        if (reader.Name.Equals(Nodes.assignment.ToString()))
                        {
                            currentExercise = -1;
                        }
                        else if (reader.Name.Equals(Nodes.content.ToString()))
                        {
                            this.mainPage.SetMenuButtons(menuButtons);
                        }

                        break;
                }
            }
        }
    }
}
