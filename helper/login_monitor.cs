using System;
using System.Runtime.InteropServices;


namespace R2R.helper
{
    class ActivityMonitor : System.ComponentModel.Component
    {

        [DllImport("User32")] private static extern bool GetLastInputInfo(ref LASTINPUTINFO lii);
        private System.Timers.Timer timer = new System.Timers.Timer();
        private bool isIdle = false;
        private int idleTimeOut = 5000;
        private int resolution = 100;
        private int lastActivity = Environment.TickCount;
        public event EventHandler OnIdle;
        public event EventHandler OnActive;
        [StructLayout(LayoutKind.Sequential)]
        private struct LASTINPUTINFO
        {
            public int size;
            public int lastTick;
        }
        public int IdleTimeOut
        {
            get { return idleTimeOut; }
            set { idleTimeOut = Math.Max(50, value); }
        }
        public int Resolution
        {
            get { return resolution; }
            set { resolution = Math.Max(50, value); }
        }
        public bool IsIdle
        {
            get { return this.isIdle; }
            private set
            {
                if (this.isIdle != value)
                {
                    this.isIdle = value;
                    if (isIdle && OnIdle != null)
                    {
                        OnIdle(this, EventArgs.Empty);
                    }
                    if (!isIdle && OnActive != null)
                    {
                        OnActive(this, EventArgs.Empty);
                    }
                }
            }
        }

        public void Start()
        {
            this.isIdle = false;
            timer.Elapsed -= Timer_Elapsed;
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = this.resolution;
            timer.Start();
        }
        public void Stop()
        {
            timer.Stop();
        }

        private void Timer_Elapsed(object sender, EventArgs e)
        {
            LASTINPUTINFO lastInputInfo = new LASTINPUTINFO();
            lastInputInfo.size = 8;
            if (GetLastInputInfo(ref lastInputInfo))
            {
                lastActivity = Math.Max(lastActivity, lastInputInfo.lastTick);
                int diff = Environment.TickCount - lastActivity;
                this.IsIdle = diff > this.IdleTimeOut;
            }
        }

    }
    class Monitor_users
    {
        public void monitor()
        {
            ActivityMonitor monitor = new ActivityMonitor();
            monitor.IdleTimeOut = 10000;
            //2 seconds
            //monitor.OnIdle += delegate { Console.WriteLine("On idle"); };
            monitor.OnIdle += (a, b) => Console.WriteLine("On idle");
            //monitor.OnActive += delegate { Console.WriteLine("On active"); };
            monitor.Start();
            //Console.ReadLine();
        }
    }
}
