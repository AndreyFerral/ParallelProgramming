Random random = new();
const int countThreads = 500;
int[] nums = new int[countThreads];
double result = 0;

FillArray();
StartThreads();

void FillArray()
{
    // заполняем массив
    for (int i = 0; i < countThreads; i++)
    {
        nums[i] = random.Next(-100, 100);
    }
}

void StartThreads() 
{
    // запускаем потоки
    for (int i = 0; i < countThreads; i++)
    { 
        Thread myThread = new Thread(Cosinus);
        myThread.Start(i);
        myThread.Join();
        Thread.Sleep(random.Next(1, 5));
    }
}

Console.WriteLine("Результат потоковой подпрограммы: " + result);
Console.WriteLine("Результат простой подпрограммы:   " + Simple());

void Cosinus(object obj)
{
    if (obj is int i)
    {
        result += Math.Cos(nums[i]);
        // Console.WriteLine(nums[i] + " " + result);
    }
}

double Simple() 
{
    double res = 0;
    for (int i = 0; i < countThreads; i++)
    {
        res += Math.Cos(nums[i]);
        // Console.WriteLine(nums[i] + " " + res);
    }
    return res;
}