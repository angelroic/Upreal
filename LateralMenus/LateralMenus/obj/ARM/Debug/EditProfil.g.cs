﻿#pragma checksum "C:\Users\roic\Documents\GitHub\Upreal\LateralMenus\LateralMenus\EditProfil.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A1EF10FAB4A54EB3D394F1A4977561FB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
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
    
    
    public partial class EditProfil : Microsoft.Phone.Controls.PhoneApplicationPage {
        
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
        
        internal System.Windows.Controls.TextBox PrenomBox;
        
        internal System.Windows.Controls.TextBox Prenom2Box;
        
        internal System.Windows.Controls.TextBox NomBox;
        
        internal System.Windows.Controls.TextBox TelBox;
        
        internal System.Windows.Controls.TextBox VilleBox;
        
        internal System.Windows.Controls.CheckBox NumeroCheck;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/LateralMenus;component/EditProfil.xaml", System.UriKind.Relative));
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
            this.PrenomBox = ((System.Windows.Controls.TextBox)(this.FindName("PrenomBox")));
            this.Prenom2Box = ((System.Windows.Controls.TextBox)(this.FindName("Prenom2Box")));
            this.NomBox = ((System.Windows.Controls.TextBox)(this.FindName("NomBox")));
            this.TelBox = ((System.Windows.Controls.TextBox)(this.FindName("TelBox")));
            this.VilleBox = ((System.Windows.Controls.TextBox)(this.FindName("VilleBox")));
            this.NumeroCheck = ((System.Windows.Controls.CheckBox)(this.FindName("NumeroCheck")));
        }
    }
}
