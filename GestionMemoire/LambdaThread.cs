using System;
using System.Threading;

namespace GestionMemoire
{
    delegate int DELG2(int v);
    class LambdaThread
    {
        static void Run() {
            {
                DELG2 d = x =>
                {
                    int res = x * x;
                    Console.WriteLine($"Le carré de {x} est {res}");
                    return res;
                };

                Thread[] Threads = new Thread[10];


                for (int i = 0; i < Threads.Length; i++)
                {
                    int j = i;
                    Threads[i] = new Thread(() => d(j));
                }

                foreach (Thread t in Threads)
                {
                    t.Start();
                    t.Join();
                }
            }
        }
    }
}