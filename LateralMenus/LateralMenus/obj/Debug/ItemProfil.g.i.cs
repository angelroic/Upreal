﻿#pragma checksum "C:\Users\Roic\Documents\GitHub\Upreal\LateralMenus\LateralMenus\ItemProfil.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "646F70288D5C0B12AB26DF191505FF40"
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
    
    
    public partial class ItemProfil : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Canvas canvas;
        
        internal System.Windows.Media.Animation.Storyboard moveAnimation;
        
        internal System.Windows.Controls.Canvas LayoutRoot;
        
        internal System.Windows.Controls.Button ConnexionButton;
        
        internal System.Windows.Controls.Grid grdCommands;
        
        internal System.Windows.Controls.TextBox RechercheBox;
        
        internal System.Windows.Controls.Button SendRequestButton;
        
        internal System.Windows.Controls.Image ImageProduit;
        
        internal System.Windows.Controls.TextBlock NomProduit;
        
        internal System.Windows.Controls.TextBlock CateText;
        
        internal System.Windows.Controls.TextBlock desc;
        
        internal System.Windows.Controls.ListBox CommentList;
        
        internal System.Windows.Controls.Button AimeButton;
        
        internal System.Windows.Controls.Button CommentButton;
        
        internal System.Windows.Controls.Button AjoutListButton;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/LateralMenus;component/ItemProfil.xaml", System.UriKind.Relative));
            this.canvas = ((System.Windows.Controls.Canvas)(this.FindName("canvas")));
            this.moveAnimation = ((System.Windows.Media.Animation.Storyboard)(this.FindName("moveAnimation")));
            this.LayoutRoot = ((System.Windows.Controls.Canvas)(this.FindName("LayoutRoot")));
            this.ConnexionButton = ((System.Windows.Controls.Button)(this.FindName("ConnexionButton")));
            this.grdCommands = ((System.Windows.Controls.Grid)(this.FindName("grdCommands")));
            this.RechercheBox = ((System.Windows.Controls.TextBox)(this.FindName("RechercheBox")));
            this.SendRequestButton = ((System.Windows.Controls.Button)(this.FindName("SendRequestButton")));
            this.ImageProduit = ((System.Windows.Controls.Image)(this.FindName("ImageProduit")));
            this.NomProduit = ((System.Windows.Controls.TextBlock)(this.FindName("NomProduit")));
            this.CateText = ((System.Windows.Controls.TextBlock)(this.FindName("CateText")));
            this.desc = ((System.Windows.Controls.TextBlock)(this.FindName("desc")));
            this.CommentList = ((System.Windows.Controls.ListBox)(this.FindName("CommentList")));
            this.AimeButton = ((System.Windows.Controls.Button)(this.FindName("AimeButton")));
            this.CommentButton = ((System.Windows.Controls.Button)(this.FindName("CommentButton")));
            this.AjoutListButton = ((System.Windows.Controls.Button)(this.FindName("AjoutListButton")));
        }
    }
}

