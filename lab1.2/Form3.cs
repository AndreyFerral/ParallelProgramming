using System;
using System.Drawing;
using System.Windows.Forms;

namespace lab1._2
{
    public partial class Form3 : Form
    {
        Timer myTimer;
        Bitmap bitmap;
        Graphics graph;
        Random random;

        public Form3()
        {
            InitializeComponent();
            myTimer = new Timer();
            random = new Random();
            bitmap = new Bitmap(pbRound.Width, pbRound.Height);
            graph = Graphics.FromImage(bitmap);
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
            int width = randomNumber(random, 50, 100);
            int height = width;

            int x = randomNumber(random, 0, pbRound.Width - width);
            int y = randomNumber(random, 0, pbRound.Height - height);

            int red = randomNumber(random, 0, 255);
            int green = randomNumber(random, 0, 255);
            int blue = randomNumber(random, 0, 255);

            Pen pen = new Pen(Color.FromArgb(red, blue, red));
            graph.DrawEllipse(pen, x, y, width, height);
            pbRound.Image = bitmap;
        }

        int randomNumber(Random random, int min, int max)
        {
            int randomNumber = random.Next(min, max);
            Console.WriteLine(randomNumber);
            return randomNumber;
        }
    }
}
