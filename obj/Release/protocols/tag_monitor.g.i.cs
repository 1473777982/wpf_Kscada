﻿#pragma checksum "..\..\..\protocols\tag_monitor.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "25CE9DADF373D721108CB71494E99597F8B8C3CAF9A1DADB03E3BCB5484BA8F7"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Panuon.UI.Silver;
using R2R.protocols;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace R2R.protocols {
    
    
    /// <summary>
    /// tag_monitor
    /// </summary>
    public partial class tag_monitor : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 249 "..\..\..\protocols\tag_monitor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TreeView tree1;
        
        #line default
        #line hidden
        
        
        #line 273 "..\..\..\protocols\tag_monitor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid varDataGrid;
        
        #line default
        #line hidden
        
        
        #line 287 "..\..\..\protocols\tag_monitor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContextMenu contextMenu;
        
        #line default
        #line hidden
        
        
        #line 288 "..\..\..\protocols\tag_monitor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem find;
        
        #line default
        #line hidden
        
        
        #line 289 "..\..\..\protocols\tag_monitor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem cancel;
        
        #line default
        #line hidden
        
        
        #line 296 "..\..\..\protocols\tag_monitor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock total;
        
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
            System.Uri resourceLocater = new System.Uri("/R2R;component/protocols/tag_monitor.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\protocols\tag_monitor.xaml"
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
            
            #line 9 "..\..\..\protocols\tag_monitor.xaml"
            ((R2R.protocols.tag_monitor)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.tree1 = ((System.Windows.Controls.TreeView)(target));
            
            #line 250 "..\..\..\protocols\tag_monitor.xaml"
            this.tree1.SelectedItemChanged += new System.Windows.RoutedPropertyChangedEventHandler<object>(this.tree1_SelectedItemChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.varDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 4:
            this.contextMenu = ((System.Windows.Controls.ContextMenu)(target));
            return;
            case 5:
            this.find = ((System.Windows.Controls.MenuItem)(target));
            
            #line 288 "..\..\..\protocols\tag_monitor.xaml"
            this.find.Click += new System.Windows.RoutedEventHandler(this.find_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.cancel = ((System.Windows.Controls.MenuItem)(target));
            
            #line 289 "..\..\..\protocols\tag_monitor.xaml"
            this.cancel.Click += new System.Windows.RoutedEventHandler(this.cancel_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.total = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

