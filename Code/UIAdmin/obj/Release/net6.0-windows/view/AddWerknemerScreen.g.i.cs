﻿#pragma checksum "..\..\..\..\view\AddWerknemerScreen.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "AC79959132A06B158A8E851799C972645B09140B"
//------------------------------------------------------------------------------
// <auto-generated>
//     Deze code is gegenereerd met een hulpprogramma.
//     Runtime-versie:4.0.30319.42000
//
//     Als u wijzigingen aanbrengt in dit bestand, kan dit onjuist gedrag veroorzaken wanneer
//     de code wordt gegenereerd.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
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
using UIAdmin.view;


namespace UIAdmin.view {
    
    
    /// <summary>
    /// AddWerknemerScreen
    /// </summary>
    public partial class AddWerknemerScreen : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 34 "..\..\..\..\view\AddWerknemerScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button HomeBtn;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\view\AddWerknemerScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel BedrijfBorder;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\..\view\AddWerknemerScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TBNaam;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\..\view\AddWerknemerScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TBVoornaam;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\view\AddWerknemerScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border BorderNaam;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\..\view\AddWerknemerScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxNaam;
        
        #line default
        #line hidden
        
        
        #line 108 "..\..\..\..\view\AddWerknemerScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border BorderVoornaam;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\..\view\AddWerknemerScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxVoorNaam;
        
        #line default
        #line hidden
        
        
        #line 143 "..\..\..\..\view\AddWerknemerScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CancelBtn;
        
        #line default
        #line hidden
        
        
        #line 144 "..\..\..\..\view\AddWerknemerScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveBtn;
        
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
            System.Uri resourceLocater = new System.Uri("/UIAdmin;V1.0.0.0;component/view/addwerknemerscreen.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\view\AddWerknemerScreen.xaml"
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
            
            #line 9 "..\..\..\..\view\AddWerknemerScreen.xaml"
            ((System.Windows.Controls.Border)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Border_MouseDown);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\..\view\AddWerknemerScreen.xaml"
            ((System.Windows.Controls.Border)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Border_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.HomeBtn = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\..\view\AddWerknemerScreen.xaml"
            this.HomeBtn.Click += new System.Windows.RoutedEventHandler(this.HomeBtn_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.BedrijfBorder = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 4:
            this.TBNaam = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.TBVoornaam = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.BorderNaam = ((System.Windows.Controls.Border)(target));
            return;
            case 7:
            this.TextBoxNaam = ((System.Windows.Controls.TextBox)(target));
            
            #line 75 "..\..\..\..\view\AddWerknemerScreen.xaml"
            this.TextBoxNaam.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.BorderVoornaam = ((System.Windows.Controls.Border)(target));
            return;
            case 9:
            this.TextBoxVoorNaam = ((System.Windows.Controls.TextBox)(target));
            
            #line 111 "..\..\..\..\view\AddWerknemerScreen.xaml"
            this.TextBoxVoorNaam.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.CancelBtn = ((System.Windows.Controls.Button)(target));
            
            #line 143 "..\..\..\..\view\AddWerknemerScreen.xaml"
            this.CancelBtn.Click += new System.Windows.RoutedEventHandler(this.CancelBtn_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.SaveBtn = ((System.Windows.Controls.Button)(target));
            
            #line 144 "..\..\..\..\view\AddWerknemerScreen.xaml"
            this.SaveBtn.Click += new System.Windows.RoutedEventHandler(this.SaveBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

