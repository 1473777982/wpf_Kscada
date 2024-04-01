using DynamicDataDisplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace R2R.Pages
{
    public class Page0_ViewModle : helper.NotifyChanged 
    {
        SolidColorBrush _RP1 = Brushes.White;
        SolidColorBrush _RP2 = Brushes.White;
        SolidColorBrush _SP1 = Brushes.White;
        SolidColorBrush _SP2 = Brushes.White;
        SolidColorBrush _TP1 = Brushes.White;
        SolidColorBrush _TP2 = Brushes.White;
        SolidColorBrush _TP3 = Brushes.White;
        SolidColorBrush _TP4 = Brushes.White;
        SolidColorBrush _TP5 = Brushes.White;
        SolidColorBrush _TP6 = Brushes.White;
        SolidColorBrush _SV0 = Brushes.White;
        SolidColorBrush _SV1 = Brushes.White;
        SolidColorBrush _SV2 = Brushes.White;
        SolidColorBrush _SV3 = Brushes.White;
        SolidColorBrush _SV4 = Brushes.White;
        SolidColorBrush _SV5 = Brushes.White;
        SolidColorBrush _MPG1 = Brushes.White;
        SolidColorBrush _MPG2 = Brushes.White;
        SolidColorBrush _MPG3 = Brushes.White;
        SolidColorBrush _MPG4 = Brushes.White;
        SolidColorBrush _MPG5 = Brushes.White;
        SolidColorBrush _Ellrobot = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD3D5DE"));
        SolidColorBrush _Pol_robot = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD3D5DE"));
        double _Angle_robot = 90;
        double _hight_robot = 37.5; //150， 4倍关系
        double _width_robot = 300; //75

        //List<SolidColorBrush> _TP = new List<SolidColorBrush>(6);
        //SolidColorBrush[] _TP = new SolidColorBrush[6] {Brushes.White, Brushes.White, Brushes.White, Brushes.White, Brushes.White, Brushes.White, };
        public SolidColorBrush RP1 { get => _RP1; set => SetProperty(ref _RP1, value); }
        public SolidColorBrush RP2 { get => _RP2; set => SetProperty(ref _RP2, value); }
        public SolidColorBrush SP1 { get => _SP1; set => SetProperty(ref _SP1, value); }
        public SolidColorBrush SP2 { get => _SP2; set => SetProperty(ref _SP2, value); }
        public SolidColorBrush SV0 { get => _SV0; set => SetProperty(ref _SV0, value); }
        public SolidColorBrush SV1 { get => _SV1; set => SetProperty(ref _SV1, value); }
        public SolidColorBrush SV2 { get => _SV2; set => SetProperty(ref _SV2, value); }
        public SolidColorBrush SV3 { get => _SV3; set => SetProperty(ref _SV3, value); }
        public SolidColorBrush SV4 { get => _SV4; set => SetProperty(ref _SV4, value); }
        public SolidColorBrush SV5 { get => _SV5; set => SetProperty(ref _SV5, value); }
        public SolidColorBrush MPG1 { get => _MPG1; set => SetProperty(ref _MPG1, value); }
        public SolidColorBrush MPG2 { get => _MPG2; set => SetProperty(ref _MPG2, value); }
        public SolidColorBrush MPG3 { get => _MPG3; set => SetProperty(ref _MPG3, value); }
        public SolidColorBrush MPG4 { get => _MPG4; set => SetProperty(ref _MPG4, value); }
        public SolidColorBrush MPG5 { get => _MPG5; set => SetProperty(ref _MPG5, value); }

        // public SolidColorBrush[] TP { get => _TP; set => SetProperty(ref _TP, value); }  

        public SolidColorBrush Ellrobot { get => _Ellrobot; set => SetProperty(ref _Ellrobot, value); }
        public SolidColorBrush Pol_robot { get => _Pol_robot; set => SetProperty(ref _Pol_robot, value); }
        public double Angle_robot { get => _Angle_robot; set => SetProperty(ref _Angle_robot, value); }
        public double hight_robot { get => _hight_robot; set => SetProperty(ref _hight_robot, value); }
        public double width_robot { get => _width_robot; set => SetProperty(ref _width_robot, value); }


    }
}
