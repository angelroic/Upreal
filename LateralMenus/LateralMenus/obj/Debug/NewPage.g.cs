﻿#pragma checksum "C:\Users\Roic\Documents\GitHub\Upreal\LateralMenus\LateralMenus\NewPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C2EBD91BA0C11D967446B4C45BE12D66"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
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


namespace LateralMenus {
    
    
    public partial class NewPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Canvas canvas;
        
        internal System.Windows.Media.Animation.Storyboard moveAnimation;
        
        internal System.Windows.Controls.Canvas LayoutRoot;
        
        internal System.Windows.Controls.Button ConnexionButton;
        
        internal System.Windows.Controls.Grid grdCommands;
        
        internal System.Windows.Controls.TextBox RechercheBox;
        
        internal System.Windows.Controls.Button SendRequestButton;
        
        internal System.Windows.Controls.Image ImgNews;
        
        internal System.Windows.Controls.TextBlock TextNews;
        
        internal System.Windows.Controls.TextBlock TitleNews;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/LateralMenus;component/NewPage.xaml", System.UriKind.Relative));
            this.canvas = ((System.Windows.Controls.Canvas)(this.FindName("canvas")));
            this.moveAnimation = ((System.Windows.Media.Animation.Storyboard)(this.FindName("moveAnimation")));
            this.LayoutRoot = ((System.Windows.Controls.Canvas)(this.FindName("LayoutRoot")));
            this.ConnexionButton = ((System.Windows.Controls.Button)(this.FindName("ConnexionButton")));
            this.grdCommands = ((System.Windows.Controls.Grid)(this.FindName("grdCommands")));
            this.RechercheBox = ((System.Windows.Controls.TextBox)(this.FindName("RechercheBox")));
            this.SendRequestButton = ((System.Windows.Controls.Button)(this.FindName("SendRequestButton")));
            this.ImgNews = ((System.Windows.Controls.Image)(this.FindName("ImgNews")));
            this.TextNews = ((System.Windows.Controls.TextBlock)(this.FindName("TextNews")));
            this.TitleNews = ((System.Windows.Controls.TextBlock)(this.FindName("TitleNews")));
        }
    }
}

