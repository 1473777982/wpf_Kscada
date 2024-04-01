﻿#pragma checksum "..\..\..\UControl\RealChart.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "9942FF04C832FF4AE333EBAD04D48F3E45AB6678A0EB8B5E3F2E2B261B806A96"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using DynamicDataDisplay;
using DynamicDataDisplay.Charts;
using DynamicDataDisplay.Charts.Axes;
using DynamicDataDisplay.Charts.Axes.Numeric;
using DynamicDataDisplay.Charts.Isolines;
using DynamicDataDisplay.Charts.Markers;
using DynamicDataDisplay.Charts.Navigation;
using DynamicDataDisplay.Charts.Shapes;
using DynamicDataDisplay.Common.Auxiliary;
using DynamicDataDisplay.Common.Palettes;
using DynamicDataDisplay.Converters;
using DynamicDataDisplay.DataSources;
using DynamicDataDisplay.MarkupExtensions;
using DynamicDataDisplay.Navigation;
using DynamicDataDisplay.PointMarkers;
using DynamicDataDisplay.ViewportRestrictions;
using Panuon.UI.Silver;
using R2R.UControl;
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


namespace R2R.UControl {
    
    
    /// <summary>
    /// RealChart
    /// </summary>
    public partial class RealChart : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\UControl\RealChart.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel dockPanel_RealChart;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\UControl\RealChart.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grid_chart_real;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\UControl\RealChart.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DynamicDataDisplay.ChartPlotter plotter_real;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\UControl\RealChart.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DynamicDataDisplay.Charts.HorizontalDateTimeAxis dateAxis_real;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\UControl\RealChart.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DynamicDataDisplay.Charts.VerticalLine verticalLine_real;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\UControl\RealChart.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tb_real;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\UControl\RealChart.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DgCustom_real;
        
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
            System.Uri resourceLocater = new System.Uri("/R2R;component/ucontrol/realchart.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UControl\RealChart.xaml"
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
            
            #line 10 "..\..\..\UControl\RealChart.xaml"
            ((R2R.UControl.RealChart)(target)).Unloaded += new System.Windows.RoutedEventHandler(this.UserControl_Unloaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.dockPanel_RealChart = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 3:
            
            #line 33 "..\..\..\UControl\RealChart.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.savePng_Click_realchart);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 38 "..\..\..\UControl\RealChart.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.removeAll_Click_realchart);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 43 "..\..\..\UControl\RealChart.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.startchart_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 48 "..\..\..\UControl\RealChart.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.stopchart_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.grid_chart_real = ((System.Windows.Controls.Grid)(target));
            return;
            case 8:
            this.plotter_real = ((DynamicDataDisplay.ChartPlotter)(target));
            
            #line 56 "..\..\..\UControl\RealChart.xaml"
            this.plotter_real.Loaded += new System.Windows.RoutedEventHandler(this.plotter_real_Loaded);
            
            #line default
            #line hidden
            
            #line 56 "..\..\..\UControl\RealChart.xaml"
            this.plotter_real.MouseMove += new System.Windows.Input.MouseEventHandler(this.plotter_MouseMove_realChart);
            
            #line default
            #line hidden
            return;
            case 9:
            this.dateAxis_real = ((DynamicDataDisplay.Charts.HorizontalDateTimeAxis)(target));
            return;
            case 10:
            this.verticalLine_real = ((DynamicDataDisplay.Charts.VerticalLine)(target));
            return;
            case 11:
            this.tb_real = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 12:
            this.DgCustom_real = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
