﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    public partial class TopBanner : UserControl
    {
        List<Dictionary<string, string>> menuButtons;
        Dictionary<string, Grid> subMenus;
        Grid mainMenu;
        const string title = "title";
        const double animationTime = 0.3;

        public TopBanner()
        {
            InitializeComponent();

            this.SetDictionary();
            this.AddButtons();
        }

        private void MainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.mainMenu.SetValue(Canvas.ZIndexProperty, 3);

            String buttonName = (string)((Button)sender).Content;
            Grid subMenuGrid = this.subMenus[buttonName];

            foreach (Grid grid in this.subMenus.Values)
            {
                if (!grid.Equals(subMenuGrid))
                {
                    grid.SetValue(Canvas.ZIndexProperty, 1);

                    if (((TranslateTransform)grid.RenderTransform).Y != 0)
                    {
                        this.RunAnimation(grid);
                    }
                }
            }
            
            subMenuGrid.SetValue(Canvas.ZIndexProperty, 2);
            this.RunAnimation(subMenuGrid);
        }

        private void SetDictionary()
        {
            this.menuButtons = new List<Dictionary<string, string>>();

            Dictionary<string, string> end = new Dictionary<string, string>();
            end.Add(title, "End");
            end.Add("End1", "End 1");
            end.Add("End2", "End 2");
            end.Add("End3", "End 3");
            this.menuButtons.Add(end);

            Dictionary<string, string> self = new Dictionary<string, string>();
            self.Add(title, "Self");
            self.Add("Self1", "Self 1");
            self.Add("Self2", "Self 2");
            self.Add("Self3", "Self 3");
            this.menuButtons.Add(self);

            Dictionary<string, string> past = new Dictionary<string, string>();
            past.Add(title, "Past");
            past.Add("Past1", "Past 1");
            past.Add("Past2", "Past 2");
            past.Add("Past3", "Past 3");
            this.menuButtons.Add(past);

            Dictionary<string, string> strength = new Dictionary<string, string>();
            strength.Add(title, "Strength");
            strength.Add("Strength1", "Strength 1");
            strength.Add("Strength2", "Strength 2");
            strength.Add("Strength3", "Strength 3");
            strength.Add("Strength4", "Strength 4");
            this.menuButtons.Add(strength);

            Dictionary<string, string> meaning = new Dictionary<string, string>();
            meaning.Add(title, "Meaning");
            meaning.Add("Meaning1", "Meaning 1");
            meaning.Add("Meaning2", "Meaning 2");
            this.menuButtons.Add(meaning);
        }

        private void AddButtons()
        {
            int mainMenuColumn = 0;
            int subMenuColumn;

            this.mainMenu = new Grid();
            this.subMenus = new Dictionary<string, Grid>();

            this.LayoutRoot.Children.Add(this.mainMenu);
            Grid.SetRow(this.mainMenu, 1);
            this.mainMenu.SetValue(Canvas.ZIndexProperty, 1);

            foreach (Dictionary<string, string> d in this.menuButtons)
            {
                subMenuColumn = 0;

                List<string> keys = d.Keys.ToList();
                keys.Sort();

                Grid subMenuGrid = new Grid();
                subMenuGrid.RenderTransform = new TranslateTransform();
                this.subMenus.Add(d[title], subMenuGrid);
                this.LayoutRoot.Children.Add(subMenuGrid);
                Grid.SetRow(subMenuGrid, 3);
                
                this.AddButtonToGrid(d[title], this.mainMenu, mainMenuColumn, this.MainMenuButton_Click);
                mainMenuColumn++;
                
                foreach (string s in keys)
                {
                    if (!s.Equals(title))
                    {
                        this.AddButtonToGrid(d[s], subMenuGrid, subMenuColumn, null);
                        subMenuColumn++;
                    }
                }
            }
            
        }

        private void AddButtonToGrid(string title, Grid grid, int column, RoutedEventHandler clickHandler)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            
            Button button = new Button();
            button.Content = title;
            button.Style = (Style)this.Resources["MenuBarButtonStyle"];
            button.Click += clickHandler;
            
            grid.Children.Add(button);
            Grid.SetColumn(button, column);
        }

        private void RunAnimation(UIElement e)
        {
            Storyboard storyboard = new Storyboard();

            DoubleAnimation animation = new DoubleAnimation();
            animation.By = this.LayoutRoot.RowDefinitions[1].ActualHeight;
            if (((TranslateTransform)e.RenderTransform).Y != 0) animation.By *= -1;
            animation.Duration = new Duration(TimeSpan.FromSeconds(animationTime));

            Storyboard.SetTarget(animation, e);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.Y)"));
            storyboard.Children.Add(animation);
            storyboard.Begin();
        }
    }
}
