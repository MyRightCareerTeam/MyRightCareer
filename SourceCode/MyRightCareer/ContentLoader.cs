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

            exercise,

            step
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
            int currentExercise = -1;
            int currentStep = -1;

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name.Equals(Nodes.exercise.ToString()))
                        {
                            currentExercise++;

                            menuButtons.Add(new Dictionary<string, string>());

                            string exerciseName = reader.GetAttribute(0);
                            menuButtons[currentExercise].Add(Title, exerciseName);
                        }
                        else if (reader.Name.Equals(Nodes.step.ToString()))
                        {
                            currentStep++;

                            string stepName = reader.GetAttribute(0);
                            menuButtons[currentExercise].Add(currentStep.ToString(), stepName);
                        }
                        else if (reader.Name.Equals("test"))
                        {
                            this.mainPage.SetPageContents(currentExercise, currentStep, reader.GetAttribute(0));
                        }

                        break;
                    case XmlNodeType.Text:
                        this.mainPage.SetPageContents(currentExercise, currentStep, reader.Value);

                        break;
                    case XmlNodeType.EndElement:
                        if (reader.Name.Equals(Nodes.exercise.ToString()))
                        {
                            currentStep = -1;
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
