﻿#pragma checksum "..\..\..\MainScreen.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4B4E85394C70A8A961346BF50767675DD399D2F1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using GUI;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace GUI {
    
    
    /// <summary>
    /// MainScreen
    /// </summary>
    public partial class MainScreen : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\MainScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnHomeButton;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\MainScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMinimizeButton;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\MainScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMaximizeButton;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\MainScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCloseButton;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\MainScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LblUsernameDisplay;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\MainScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnDartsSpel;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\MainScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnDartsStatistieken;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.8.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/GUI;component/mainscreen.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MainScreen.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.btnHomeButton = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\MainScreen.xaml"
            this.btnHomeButton.Click += new System.Windows.RoutedEventHandler(this.btnHomeButton_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnMinimizeButton = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\MainScreen.xaml"
            this.btnMinimizeButton.Click += new System.Windows.RoutedEventHandler(this.btnMinimizeButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnMaximizeButton = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\MainScreen.xaml"
            this.btnMaximizeButton.Click += new System.Windows.RoutedEventHandler(this.btnMaximizeButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnCloseButton = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\MainScreen.xaml"
            this.btnCloseButton.Click += new System.Windows.RoutedEventHandler(this.btnCloseButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.LblUsernameDisplay = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.BtnDartsSpel = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\MainScreen.xaml"
            this.BtnDartsSpel.Click += new System.Windows.RoutedEventHandler(this.BtnDartsSpel_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.BtnDartsStatistieken = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\MainScreen.xaml"
            this.BtnDartsStatistieken.Click += new System.Windows.RoutedEventHandler(this.BtnDartsStatistieken_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

