﻿#pragma checksum "C:\Users\sheltotj\Documents\Courses\CSSE\Senior Project\MyRightCareer\SourceCode\MyRightCareer\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A80B9ECA92802996632B4719C912302D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18052
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MyRightCareer;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace MyRightCareer {
    
    
    public partial class MainPage : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal MyRightCareer.HelpPanel helpPanel;
        
        internal MyRightCareer.TopBanner topBanner;
        
        internal System.Windows.Controls.TextBlock contentBlock;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/MyRightCareer;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.helpPanel = ((MyRightCareer.HelpPanel)(this.FindName("helpPanel")));
            this.topBanner = ((MyRightCareer.TopBanner)(this.FindName("topBanner")));
            this.contentBlock = ((System.Windows.Controls.TextBlock)(this.FindName("contentBlock")));
        }
    }
}

