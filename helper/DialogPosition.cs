using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace R2R.helper
{
    public class DialogPosition
    {
        public static void setDialogPosition(Window dialog, Point mousePosition)
        {
            dialog.WindowStartupLocation = WindowStartupLocation.Manual;
            double left = mousePosition.X;
            double top = mousePosition.Y + 80;

            var dpiScaleX = (double)System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / System.Windows.SystemParameters.PrimaryScreenWidth;
            var dpiScaleY = (double)System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / System.Windows.SystemParameters.PrimaryScreenHeight;
            dialog.Left = left / dpiScaleX;
            dialog.Top = top / dpiScaleY;
        }
    }
}
