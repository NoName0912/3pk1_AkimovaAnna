using System;
using System.Collections;
using System.Diagnostics;

//Простой поиск для массива
static int SimpleSearch(int[] a, int x)
{
    int i = 0;
    while (i < a.Length && a[i] != x)
        i++;
    if (i < a.Length)
        return i;
    else
        return -1;
}
//Простой поиск для списка
static int SimpleSearch2(List<int> a, int x)
{
    int i = 0;
    while (i < a.Capacity && a[i] != x)
        i++;
    if (i < a.Capacity)
        return i;
    else
        return -1;
}
//Бинарный поиск для массива
static int BinarySearch(int[] a, int x)
{
    int middle, left = 0, right = a.Length - 1;
    do
    {
        middle = (left + right) / 2;
        if (x > a[middle])
            left = middle + 1;
        else
            right = middle - 1;
    }
    while ((a[middle] != x) && (left <= right));
    if (a[middle] == x)
        return middle;
    else
        return -1;
}
//Бинарный поиск для списка
static int BinarySearch2(List<int> a, int x)
{
    int middle, left = 0, right = a.Capacity - 1;
    do
    {
        middle = (left + right) / 2;
        if (x > a[middle])
            left = middle + 1;
        else
            right = middle - 1;
    }
    while ((a[middle] != x) && (left <= right));
    if (a[middle] == x)
        return middle;
    else
        return -1;
}

static void SortBinInsert(int[] a)
{
    int N = a.Length;
    for (int i = 1; i <= N - 1; i++)
    {
        int tmp = a[i], left = 1, right = i - 1;
        while (left <= right)
        {
            int m = (left + right) / 2;
            if (tmp < a[m])
                right = m - 1;
            else left = m + 1;
        }
        for (int j = i - 1; j >= left; j--)
            a[j + 1] = a[j];

        a[left] = tmp;
    }
}

static void SortBinInsert2(List<int> a)
{
    int N = a.Capacity;
    for (int i = 1; i <= N - 1; i++)
    {
        int tmp = a[i], left = 1, right = i - 1;
        while (left <= right)
        {
            int m = (left + right) / 2;
            if (tmp < a[m])
                right = m - 1;
            else left = m + 1;
        }
        for (int j = i - 1; j >= left; j--)
            a[j + 1] = a[j];

        a[left] = tmp;
    }
}


Console.WriteLine("1 задание:");
//массив
const int n = 100000;
int[] a = new int[n];
//заполнение массива
Random rnd = new Random();
for (int i = 0; i < n; i++)
    a[i] = rnd.Next(0, 1000000);
//измерение времени для обычного поиска в массиве
Timing objT = new Timing();
Stopwatch stpWatch = new Stopwatch();
stpWatch.Start();
objT.StartTime();
SimpleSearch(a, n);
stpWatch.Stop();
objT.StopTime();
Console.WriteLine("Обычный поиск:");
Console.WriteLine($"StopWatch:{stpWatch.ElapsedMilliseconds.ToString()}");
Console.WriteLine($"StopTicks:{stpWatch.ElapsedTicks.ToString()}");
Console.WriteLine("Timing: " + objT.Result().ToString());

//измерение времени для бинарного поиска в массиве
//сортировка
SortBinInsert(a);
//измерение времени для бинарного поиска в массиве
Timing objT2 = new Timing();
Stopwatch stpWatch2 = new Stopwatch();
stpWatch2.Start();
objT2.StartTime();
BinarySearch(a, n);
stpWatch2.Stop();
objT2.StopTime();
Console.WriteLine("Бинарный поиск:");
Console.WriteLine($"StopWatch:{stpWatch2.ElapsedMilliseconds.ToString()}");
Console.WriteLine($"StopTicks:{stpWatch2.ElapsedTicks.ToString()}");
Console.WriteLine("Timing: " + objT2.Result().ToString());

Console.WriteLine("2 задание:");
//список
List<int> numbers = new List<int>(n);
//заполнение списка
for (int i = 0; i < n; i++)
    numbers.Add(rnd.Next(0, 1000000));
//измерение времени для обычного поиска в списке
Timing objT3 = new Timing();
Stopwatch stpWatch3 = new Stopwatch();
stpWatch3.Start();
objT3.StartTime();
SimpleSearch2(numbers, n);
stpWatch3.Stop();
objT3.StopTime();
Console.WriteLine("Обычный поиск:");
Console.WriteLine($"StopWatch:{stpWatch3.ElapsedMilliseconds.ToString()}");
Console.WriteLine($"StopTicks:{stpWatch3.ElapsedTicks.ToString()}");
Console.WriteLine("Timing: " + objT3.Result().ToString());

//измерение времени для бинарного поиска в списке
//сортировка
SortBinInsert2(numbers);
//измерение времени для бинарного поиска в списке
Timing objT4 = new Timing();
Stopwatch stpWatch4 = new Stopwatch();
stpWatch4.Start();
objT4.StartTime();
BinarySearch2(numbers, n);
stpWatch4.Stop();
objT4.StopTime();
Console.WriteLine("Бинарный поиск:");
Console.WriteLine($"StopWatch:{stpWatch4.ElapsedMilliseconds.ToString()}");
Console.WriteLine($"StopTicks:{stpWatch4.ElapsedTicks.ToString()}");
Console.WriteLine("Timing: " + objT4.Result().ToString());

//В обоих заданиях по результатам видно, что бинарный поиск идёт намного быстрее.
//С хэш-таблицей у меня не особо получилось.

internal class Timing
{
    TimeSpan duration;
    TimeSpan[] threads;
    public Timing()
    {
        duration = new TimeSpan(0);
        threads = new TimeSpan[Process.GetCurrentProcess().Threads.Count];
    }
    public void StartTime()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        for (int i = 0; i < threads.Length; i++)
            threads[i] = Process.GetCurrentProcess().Threads[i].UserProcessorTime;
    }
    public void StopTime()
    {
        TimeSpan tmp;
        for (int i = 0; i < threads.Length; i++)
        {
            tmp = Process.GetCurrentProcess().Threads[i].
            UserProcessorTime.Subtract(threads[i]);
            if (tmp > TimeSpan.Zero)
                duration = tmp;
        }
    }
    public TimeSpan Result()
    {
        return duration;
    }

}

