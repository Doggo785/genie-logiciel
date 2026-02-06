using System;
using System.Diagnostics;
namespace GestionMemoire
{
    class HeapStack
    {
        public static void Run()
        {
            const int largeSize = 300000;
            AllocateLargeArrayOnHeap(largeSize);
            AllocateLargeArrayOnStack(largeSize);
        }

        static void AllocateLargeArrayOnStack(int size)
        {
            try // Stack
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                Span<int> stackArray = stackalloc int[size];
                for (int j = 0; j < 2000; j++)
                {
                    stackArray[0] = 0;
                    for (int i = 1; i < stackArray.Length; i++)
                    {
                        stackArray[i] = stackArray[i - 1];

                    }
                }
                stopwatch.Stop();
                Console.WriteLine($"stack : {stopwatch.ElapsedMilliseconds} ms");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"stack allocation not possible: {ex.Message}");
            }

        }

        static void AllocateLargeArrayOnHeap(int size)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                int[] heapArray = new int[size];
                for (int j = 0; j < 2000; j++)
                {
                    heapArray[0] = 0;
                    for (int i = 1; i < heapArray.Length; i++)
                    {
                        heapArray[i] = heapArray[i - 1];

                    }
                }
                stopwatch.Stop();
                Console.WriteLine($"Heap : {stopwatch.ElapsedMilliseconds} ms");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"stack allocation not possible: {ex.Message}");
            }

        }
    }
}