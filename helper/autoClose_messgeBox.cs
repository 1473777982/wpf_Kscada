using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

// 参考： https://www.cnblogs.com/hsiang/p/8878093.html

namespace R2R.helper
{
    /// <summary>
    /// 描述:通过WinAPI进行查找窗口，并对窗口进行操作
    /// </summary>
    public class WndHelper
    {
        /// <summary>
        /// 查找窗口
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="title">窗口标题</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr FindWindow(IntPtr hwnd, string title);

        /// <summary>
        /// 移动窗口
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="x">起始位置X</param>
        /// <param name="y">起始位置Y</param>
        /// <param name="nWidth">窗口宽度</param>
        /// <param name="nHeight">窗口高度</param>
        /// <param name="rePaint">是否重绘</param>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern void MoveWindow(IntPtr hwnd, int x, int y, int nWidth, int nHeight, bool rePaint);

        /// <summary>
        /// 获取窗口矩形
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="rect"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern bool GetWindowRect(IntPtr hwnd, out Rectangle rect);

        /// <summary>
        /// 向窗口发送信息
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="msg">信息</param>
        /// <param name="wParam">高字节</param>
        /// <param name="lParam">低字节</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int PostMessage(IntPtr hwnd, int msg, uint wParam, uint lParam);

        public const int WM_CLOSE = 0x10; //关闭命令

        public const int WM_KEYDOWN = 0x0100;//按下键

        public const int WM_KEYUP = 0x0101;//按键起来

        public const int VK_RETURN = 0x0D;//回车键

        /*public*/
        static bool IsWorking = false;

        /// <summary>
        /// 对话框标题
        /// </summary>
        /*public*/
        static string[] titles = new string[4] { "请选择", "提示", "错误", "警告" };

        /// <summary>
        /// 查找和移动窗口
        /// </summary>
        /// <param name="title">窗口标题</param>
        /// <param name="x">起始位置X</param>
        /// <param name="y">起始位置Y</param>
        public static void FindAndMoveWindow(string title, int x, int y)
        {
            Thread t = new Thread(() =>
            {
                IntPtr msgBox = IntPtr.Zero;
                while ((msgBox = FindWindow(IntPtr.Zero, title)) == IntPtr.Zero) ;
                Rectangle r = new Rectangle();
                GetWindowRect(msgBox, out r);
                MoveWindow(msgBox, x, y, r.Width - r.X, r.Height - r.Y, true);
            });
            t.Start();
        }

        /// <summary>
        /// 查找和关闭窗口
        /// </summary>
        /// <param name="title">标题</param>
        public static void FindAndKillWindow(string title)
        {
            IntPtr ptr = FindWindow(IntPtr.Zero, title);
            if (ptr != IntPtr.Zero)
            {
                int ret = PostMessage(ptr, WM_CLOSE, 0, 0);
                Thread.Sleep(1000);
                ptr = FindWindow(IntPtr.Zero, title);
                if (ptr != IntPtr.Zero)
                {
                    PostMessage(ptr, WM_KEYDOWN, VK_RETURN, 0);
                    PostMessage(ptr, WM_KEYUP, VK_RETURN, 0);
                }
            }
        }

        public async static void AutoCloseMsgBox(string titleTxt, int delay_ms)
        {
            await Task.Delay(delay_ms);
            await Task.Run(() => FindAndKillWindow(titleTxt));
        }

        /// <summary>
        /// 查找和关闭窗口
        /// </summary>
        /*public*/
        static void FindAndKillWindow()
        {
            Thread t = new Thread(() =>
            {
                while (IsWorking)
                {
                    //按标题查找
                    foreach (string title in titles)
                    {
                        FindAndKillWindow(title);
                    }
                    Thread.Sleep(3000);
                }
            });

            t.Start();
        }
    }
}
