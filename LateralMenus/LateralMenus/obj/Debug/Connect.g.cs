﻿#pragma checksum "C:\Users\Roic\Documents\GitHub\Upreal\LateralMenus\LateralMenus\Connect.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F44176EBEB2D64DCEF7E5853F727BB5E"
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
    
    
    public partial class Connect : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Canvas canvas;
        
        internal System.Windows.Media.Animation.Storyboard moveAnimation;
        
        internal System.Windows.Controls.Canvas LayoutRoot;
        
        internal System.Windows.Controls.Button ConnexionButton;
        
        internal System.Windows.Controls.Grid grdCommands;
        
        internal System.Windows.Controls.TextBox RechercheBox;
        
        internal System.Windows.Controls.Button SendRequestButton;
        
        internal System.Windows.Controls.TextBox NameBox;
        
        internal System.Windows.Controls.PasswordBox PasswordBox;
        
        internal System.Windows.Controls.TextBox EmailBox;
        
        internal System.Windows.Controls.CheckBox CguBox;
        
        internal System.Windows.Controls.Button SendInscriptionButton;
        
        internal System.Windows.Controls.TextBlock textBlock;
        
        internal System.Windows.Controls.TextBlock textBlock1;
        
        internal System.Windows.Controls.TextBlock textBlock2;
        
        internal System.Windows.Controls.TextBlock textBlock3;
        
        internal System.Windows.Controls.PasswordBox RePasswordBox;
        
        internal System.Windows.Controls.TextBox usernameBox;
        
        internal System.Windows.Controls.Button SendConnexionButton;
        
        internal System.Windows.Controls.PasswordBox passBox;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/LateralMenus;component/Connect.xaml", System.UriKind.Relative));
            this.canvas = ((System.Windows.Controls.Canvas)(this.FindName("canvas")));
            this.moveAnimation = ((System.Windows.Media.Animation.Storyboard)(this.FindName("moveAnimation")));
            this.LayoutRoot = ((System.Windows.Controls.Canvas)(this.FindName("LayoutRoot")));
            this.ConnexionButton = ((System.Windows.Controls.Button)(this.FindName("ConnexionButton")));
            this.grdCommands = ((System.Windows.Controls.Grid)(this.FindName("grdCommands")));
            this.RechercheBox = ((System.Windows.Controls.TextBox)(this.FindName("RechercheBox")));
            this.SendRequestButton = ((System.Windows.Controls.Button)(this.FindName("SendRequestButton")));
            this.NameBox = ((System.Windows.Controls.TextBox)(this.FindName("NameBox")));
            this.PasswordBox = ((System.Windows.Controls.PasswordBox)(this.FindName("PasswordBox")));
            this.EmailBox = ((System.Windows.Controls.TextBox)(this.FindName("EmailBox")));
            this.CguBox = ((System.Windows.Controls.CheckBox)(this.FindName("CguBox")));
            this.SendInscriptionButton = ((System.Windows.Controls.Button)(this.FindName("SendInscriptionButton")));
            this.textBlock = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock")));
            this.textBlock1 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock1")));
            this.textBlock2 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock2")));
            this.textBlock3 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock3")));
            this.RePasswordBox = ((System.Windows.Controls.PasswordBox)(this.FindName("RePasswordBox")));
            this.usernameBox = ((System.Windows.Controls.TextBox)(this.FindName("usernameBox")));
            this.SendConnexionButton = ((System.Windows.Controls.Button)(this.FindName("SendConnexionButton")));
            this.passBox = ((System.Windows.Controls.PasswordBox)(this.FindName("passBox")));
        }
    }
}

