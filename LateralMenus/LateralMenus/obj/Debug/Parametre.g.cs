﻿#pragma checksum "C:\Users\roic\Desktop\LateralMenus\LateralMenus\Parametre.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C361C87CEC510F22E1C76C11917CC4F6"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
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
    
    
    public partial class Parametre : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Canvas canvas;
        
        internal System.Windows.Media.Animation.Storyboard moveAnimation;
        
        internal System.Windows.Controls.Canvas LayoutRoot;
        
        internal System.Windows.Controls.Button ConnexionButton;
        
        internal System.Windows.Controls.TextBox RechercheBox;
        
        internal System.Windows.Controls.Button SendRequestButton;
        
        internal System.Windows.Controls.ListBox CategorieList;
        
        internal System.Windows.Controls.RadioButton RankButton5;
        
        internal System.Windows.Controls.RadioButton RankButton4;
        
        internal System.Windows.Controls.RadioButton RankButton3;
        
        internal System.Windows.Controls.RadioButton RankButton2;
        
        internal System.Windows.Controls.RadioButton RankButton1;
        
        internal System.Windows.Controls.Grid grdCommands;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/LateralMenus;component/Parametre.xaml", System.UriKind.Relative));
            this.canvas = ((System.Windows.Controls.Canvas)(this.FindName("canvas")));
            this.moveAnimation = ((System.Windows.Media.Animation.Storyboard)(this.FindName("moveAnimation")));
            this.LayoutRoot = ((System.Windows.Controls.Canvas)(this.FindName("LayoutRoot")));
            this.ConnexionButton = ((System.Windows.Controls.Button)(this.FindName("ConnexionButton")));
            this.RechercheBox = ((System.Windows.Controls.TextBox)(this.FindName("RechercheBox")));
            this.SendRequestButton = ((System.Windows.Controls.Button)(this.FindName("SendRequestButton")));
            this.CategorieList = ((System.Windows.Controls.ListBox)(this.FindName("CategorieList")));
            this.RankButton5 = ((System.Windows.Controls.RadioButton)(this.FindName("RankButton5")));
            this.RankButton4 = ((System.Windows.Controls.RadioButton)(this.FindName("RankButton4")));
            this.RankButton3 = ((System.Windows.Controls.RadioButton)(this.FindName("RankButton3")));
            this.RankButton2 = ((System.Windows.Controls.RadioButton)(this.FindName("RankButton2")));
            this.RankButton1 = ((System.Windows.Controls.RadioButton)(this.FindName("RankButton1")));
            this.grdCommands = ((System.Windows.Controls.Grid)(this.FindName("grdCommands")));
        }
    }
}
