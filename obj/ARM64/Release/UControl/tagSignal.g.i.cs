﻿#pragma checksum "..\..\..\..\UControl\tagSignal.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4BCEBF06FBEF16775087D25790B832B88EA7A7B20EE74D8AD937212961ED6C2E"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using R2R.UControl;
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


namespace R2R.UControl {
    
    
    /// <summary>
    /// tagSignal
    /// </summary>
    public partial class tagSignal : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\..\..\UControl\tagSignal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal R2R.UControl.tagSignal tagSig;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\..\UControl\tagSignal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextBlockRecording;
        
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
            System.Uri resourceLocater = new System.Uri("/R2R;component/ucontrol/tagsignal.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UControl\tagSignal.xaml"
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
            this.tagSig = ((R2R.UControl.tagSignal)(target));
            
            #line 9 "..\..\..\..\UControl\tagSignal.xaml"
            this.tagSig.Loaded += new System.Windows.RoutedEventHandler(this.tagSig_Loaded);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\..\UControl\tagSignal.xaml"
            this.tagSig.Unloaded += new System.Windows.RoutedEventHandler(this.tagSig_Unloaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TextBlockRecording = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

