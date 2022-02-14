using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace lab1
{
    public partial class Form1 : Form
    {
        Timer myTimer;
        Process process1;
        Process process2;

        public Form1()
        {
            InitializeComponent();
            myTimer = new Timer();
            process1 = Process.Start(@"..\..\..\lab1.1\bin\Debug\lab1.1.exe");
            process2 = Process.Start(@"..\..\..\lab1.2\bin\Debug\lab1.2.exe");
            ActivateTimer();
        }

        public void ActivateTimer()
        {
            myTimer.Interval = 200;
            myTimer.Enabled = true;
            myTimer.Tick += new EventHandler(TimerEventProcessor);
            myTimer.Start();
        }

        private void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            if (process1.HasExited && process2.HasExited) this.Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!process1.HasExited) process1.Kill();
            if (!process2.HasExited) process2.Kill();
        }
    }
}
