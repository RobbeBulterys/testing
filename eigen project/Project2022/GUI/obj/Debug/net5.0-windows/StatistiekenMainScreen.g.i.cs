﻿#pragma checksum "..\..\..\StatistiekenMainScreen.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "CB0C9E82348C348070F821394958AA955AB70D12"
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
    /// StatistiekenMainScreen
    /// </summary>
    public partial class StatistiekenMainScreen : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\StatistiekenMainScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnHomeButton;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\StatistiekenMainScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMinimizeButton;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\StatistiekenMainScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMaximizeButton;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\StatistiekenMainScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCloseButton;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\StatistiekenMainScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbStatistieken;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\StatistiekenMainScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblUsername;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\StatistiekenMainScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lstvDartsScoreTraining;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.10.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/GUI;V1.0.0.0;component/statistiekenmainscreen.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\StatistiekenMainScreen.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.btnHomeButton = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\StatistiekenMainScreen.xaml"
            this.btnHomeButton.Click += new System.Windows.RoutedEventHandler(this.btnHomeButton_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnMinimizeButton = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\StatistiekenMainScreen.xaml"
            this.btnMinimizeButton.Click += new System.Windows.RoutedEventHandler(this.btnMinimizeButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnMaximizeButton = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\StatistiekenMainScreen.xaml"
            this.btnMaximizeButton.Click += new System.Windows.RoutedEventHandler(this.btnMaximizeButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnCloseButton = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\StatistiekenMainScreen.xaml"
            this.btnCloseButton.Click += new System.Windows.RoutedEventHandler(this.btnCloseButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.cmbStatistieken = ((System.Windows.Controls.ComboBox)(target));
            
            #line 52 "..\..\..\StatistiekenMainScreen.xaml"
            this.cmbStatistieken.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cmbStatistieken_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.lblUsername = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.lstvDartsScoreTraining = ((System.Windows.Controls.ListView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

