new Lab4().start();

class Lab4
{
    Random random = new();
    AutoResetEvent[] freeX, readyX, freeY, readyY;

    int n = 0;
    int joutX = 0;
    int joutY = 0;
    double x, y;

    const int countNumbers = 10;
    double[] a = new double[countNumbers];
    double[] b = new double[countNumbers];
    int[] numsU = new int[countNumbers];
    int[] numsV = new int[countNumbers];

    void xFirstFunc(object sy)
    {
        double w;
        for (; ; )
        {
            readyX[0].WaitOne();
            w = numsU[joutX] - numsV[joutX];
            freeX[0].Set();
            freeX[1].WaitOne();
            x = w;
            readyX[1].Set();
        }
    }

    void xSecondFunc(object sy)
    {
        double w;
        for (; ; )
        {
            readyX[1].WaitOne();
            a[joutX++] = Math.Cos(x);
            freeX[1].Set();
        }
    }

    void yFirstFunc(object sy)
    {
        double w;
        for (; ; )
        {
            readyY[0].WaitOne();
            w = numsV[joutY] + numsU[joutY];
            freeY[0].Set();
            freeY[1].WaitOne();
            y = w;
            readyY[1].Set();

        }
    }

    void ySecondFunc(object sy)
    {
        double w;
        for (; ; )
        {
            readyY[1].WaitOne();
            w = Math.Cos(y);
            freeY[1].Set();
            freeY[2].WaitOne();
            y = w;
            readyY[2].Set();
        }
    }

    void yThirdFunc(object sy)
    {
        for (; ; )
        {
            readyY[2].WaitOne();
            b[joutY++] = Math.Exp(y);
            freeY[2].Set();
        }

    }

    public void start()
    {
        readyX = new AutoResetEvent[2];
        freeX = new AutoResetEvent[2];
        readyY = new AutoResetEvent[3];
        freeY = new AutoResetEvent[3];

        for (int i = 0; i < countNumbers; i++)
        {
            numsU[i] = random.Next(-100, 100);
            numsV[i] = random.Next(-100, 100);
        }

        for (int i = 0; i < readyX.Length; i++)
        {
            readyX[i] = new AutoResetEvent(false);
            freeX[i] = new AutoResetEvent(true);
        }

        for (int i = 0; i < readyY.Length; i++)
        {
            readyY[i] = new AutoResetEvent(false);
            freeY[i] = new AutoResetEvent(true);
        }

        new Thread(xFirstFunc).Start();
        new Thread(xSecondFunc).Start();
        new Thread(yFirstFunc).Start();
        new Thread(ySecondFunc).Start();
        new Thread(yThirdFunc).Start();

        for (n = 0; n < countNumbers; n++)
        {
            freeX[0].WaitOne();
            freeY[0].WaitOne();

            readyX[0].Set();
            readyY[0].Set();

            Thread.Sleep(1);
        }

        for (int i = 0; i < countNumbers; i++)
        {
            Console.WriteLine("Проверка x = " + Math.Cos(numsU[i] - numsV[i]) + " y = " + Math.Exp(Math.Cos(numsU[i] + numsV[i])));
            Console.WriteLine("Вывод    x = " + a[i] + " y = " + b[i]);
            Console.WriteLine();
        }
    }
}