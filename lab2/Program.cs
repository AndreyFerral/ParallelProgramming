int N = 20; // степень многочлена
double x;   // значение переменной
Random random = new();

int i, j;
double v, z;

arg t = new arg(); // объявление структуры параметров
for (x = 0; x < 10; x += 1) // перебор значений х
{
    // границы массива матриц
    t.l = 0; 
    t.r = N;

    prod(t); // вызов подпрограммы
    Console.Write("\nPolynom value = " + t.q); // вывод

    // Здесь происходит проверочный расчет
    z = 0;
    for (i = 0; i <= N; i++) // цикл для проверки
    {
        v = 1;
        for (j = 0; j < i; j++) v = v * x; // вычисление xi   
        z += v * Math.Pow((N+1),2); // вычисление значения многочлена
    }
    Console.Write(" == " + z); // вывод значения для проверки
}

void prod(object s)
{
    int l = ((arg)s).l, r = ((arg)s).r;
    if (l == r)
    {
        // установка коэффициентов
        ((arg)s).p = x;
        ((arg)s).q = Math.Pow((N+1),2);
    }
    else
    {
        arg r1 = new arg();
        arg r2 = new arg();

        // установка границ
        r1.l = l; 
        r1.r = (l + r + 1) / 2 - 1;
        r2.l = (l + r + 1) / 2; 
        r2.r = r;

        Thread thread1 = new Thread(prod);
        Thread thread2 = new Thread(prod);

        thread1.Start(r1);
        thread2.Start(r2);

        thread1.Join();
        thread2.Join();

        ((arg)s).p = (r1.p) * (r2.p); // p1 * p2
        ((arg)s).q = (r1.p) * (r2.q) + (r1.q); // p1q2 + q1
    }
}

class arg // структура для передачи параметров
{
    public int l, r; // нижний и верхний номера 2х2=матрицы
    public double p, q; // коэффициенты первой строки матрицы
};