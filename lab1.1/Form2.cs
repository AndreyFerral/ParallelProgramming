﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace lab1._1
{
    public partial class Form2 : Form
    {
        Timer myTimer;
        Bitmap bitmap;
        Graphics graph;
        Random random;

        public Form2()
        {
            InitializeComponent();
            myTimer = new Timer(); 
            random = new Random();
            bitmap = new Bitmap(pbRectangle.Width, pbRectangle.Height);
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
            int height = randomNumber(random, 50, 100);

            int x = randomNumber(random, 0, pbRectangle.Width - width);
            int y = randomNumber(random, 0, pbRectangle.Height - height);

            int red = randomNumber(random, 0, 255);
            int green = randomNumber(random, 0, 255);
            int blue = randomNumber(random, 0, 255);

            Brush brush = new SolidBrush(Color.FromArgb(red, green, blue));
            graph.FillRectangle(brush, x, y, width, height);
            pbRectangle.Image = bitmap;
        }

        int randomNumber(Random random, int min, int max)
        {
            int randomNumber = random.Next(min, max);
            Console.WriteLine(randomNumber);
            return randomNumber;
        }
    }
}
