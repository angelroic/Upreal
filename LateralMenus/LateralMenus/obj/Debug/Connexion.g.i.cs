﻿#pragma checksum "C:\Users\roic\Desktop\LateralMenus\LateralMenus\Connexion.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D7BC4B9E1CE73B9B5407735F05712EFF"
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
    
    
    public partial class Connexion : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Canvas canvas;
        
        internal System.Windows.Media.Animation.Storyboard moveAnimation;
        
        internal System.Windows.Controls.Canvas LayoutRoot;
        
        internal System.Windows.Controls.Button ConnexionButton;
        
        internal System.Windows.Controls.TextBox RechercheBox;
        
        internal System.Windows.Controls.Button SendRequestButton;
        
        internal System.Windows.Controls.Grid grdCommands;
        
        internal System.Windows.Controls.TextBox usernameBox;
        
        internal System.Windows.Controls.Button SendConnexionButton;
        
        internal System.Windows.Controls.TextBox passBox;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/LateralMenus;component/Connexion.xaml", System.UriKind.Relative));
            this.canvas = ((System.Windows.Controls.Canvas)(this.FindName("canvas")));
            this.moveAnimation = ((System.Windows.Media.Animation.Storyboard)(this.FindName("moveAnimation")));
            this.LayoutRoot = ((System.Windows.Controls.Canvas)(this.FindName("LayoutRoot")));
            this.ConnexionButton = ((System.Windows.Controls.Button)(this.FindName("ConnexionButton")));
            this.RechercheBox = ((System.Windows.Controls.TextBox)(this.FindName("RechercheBox")));
            this.SendRequestButton = ((System.Windows.Controls.Button)(this.FindName("SendRequestButton")));
            this.grdCommands = ((System.Windows.Controls.Grid)(this.FindName("grdCommands")));
            this.usernameBox = ((System.Windows.Controls.TextBox)(this.FindName("usernameBox")));
            this.SendConnexionButton = ((System.Windows.Controls.Button)(this.FindName("SendConnexionButton")));
            this.passBox = ((System.Windows.Controls.TextBox)(this.FindName("passBox")));
        }
    }
}

