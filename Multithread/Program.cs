class Program
{
    private static int _nb_thread_in_progress = 0;
    static int _CountExclusive_access = 0;
    static object key = new object();
    static object _lockObject = new object();
    static Mutex mut = new Mutex(false, "Jean");
    private static Semaphore sem = new Semaphore(3, 3);
    static CountdownEvent countdown = new CountdownEvent(300);
    static void Main(string[] args)
    {
        for(int i = 0; i < 300; i++)
        {
            int idx = i;
            new Thread(() => FcTA("Thread_" + idx)).Start();
        }
        countdown.Wait();
    }
    static void FcTA(string name)
    {
        lock (key) { Console.WriteLine("Thread {0} is at the start of FcTA : {1}", name, ++_nb_thread_in_progress); }
        if (sem.WaitOne(200))
        {
            Fct_Exclusive_access(name);
            sem.Release();
        }
        else
        {
            Console.WriteLine("Thread {0} a abandonné : trop d'attente !", name);
        }
        lock (key) { Console.WriteLine("Thread {0} is at the end of FcTA : {1}", name, --_nb_thread_in_progress); }
        countdown.Signal();
    }
    static void Fct_Exclusive_access(string name)
    {
        //sem.WaitOne();
        Console.WriteLine("Thread {0} is entering the exclusive access zone ", name);
        Interlocked.Increment(ref _CountExclusive_access);
        Thread.Sleep(50);
        if(_CountExclusive_access> 1)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Thread {0} is in the exclusive access zone ", name);
            Console.ResetColor();
        }
        Thread.Sleep(50);
        Interlocked.Decrement(ref _CountExclusive_access);
        Console.WriteLine("Thread {0} is leaving the exclusive access zone ", name);   
        //sem.Release();
    }
}