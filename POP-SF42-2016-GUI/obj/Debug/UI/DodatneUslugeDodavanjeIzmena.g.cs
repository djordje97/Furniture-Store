﻿#pragma checksum "..\..\..\UI\DodatneUslugeDodavanjeIzmena.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F684931A76AD426EAFAD86F5A3BCF0238735A1C4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using POP_SF42_2016_GUI.UI;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace POP_SF42_2016_GUI.UI {
    
    
    /// <summary>
    /// DodatneUslugeDodavanjeIzmena
    /// </summary>
    public partial class DodatneUslugeDodavanjeIzmena : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\UI\DodatneUslugeDodavanjeIzmena.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbNazivUsluge;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\UI\DodatneUslugeDodavanjeIzmena.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbCenaUsluge;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\UI\DodatneUslugeDodavanjeIzmena.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPotvrdi;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\UI\DodatneUslugeDodavanjeIzmena.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnOdustani;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\UI\DodatneUslugeDodavanjeIzmena.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbIspisGreske;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/POP-SF42-2016-GUI;component/ui/dodatneuslugedodavanjeizmena.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UI\DodatneUslugeDodavanjeIzmena.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.tbNazivUsluge = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.tbCenaUsluge = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.btnPotvrdi = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\..\UI\DodatneUslugeDodavanjeIzmena.xaml"
            this.btnPotvrdi.Click += new System.Windows.RoutedEventHandler(this.Potvrdi);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnOdustani = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.tbIspisGreske = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

