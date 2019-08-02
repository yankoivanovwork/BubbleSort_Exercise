using System;
using System.Diagnostics;

namespace BubbleSort_Exercise
{
    class Program
    {
        private static long programMemory = 0;
        private static long defaultSortMemory = 0;
        private static long halfLoopsABSwapMemory = 0;
        private static long halfLoopsTempABSwapMemory = 0;
        private static long twoXHalfLoopsABSwap = 0;
        private static long twoXHalfLoopsTempABSwap = 0;

        static void Main(string[] args)
        {
            Random rng = new Random();
            int[] arrayElements = new int[50000];
            for (int i = 0; i < arrayElements.Length; i++)
            {
                arrayElements[i] = rng.Next(0, 50001);
            }

            programMemory = Process.GetCurrentProcess().WorkingSet64;

            Console.WriteLine("Fresh program memory: " + (programMemory / 1000) + " kilobytes");
            Console.Write(Environment.NewLine);

            Console.WriteLine("Before default sort program memory: " + (Process.GetCurrentProcess().WorkingSet64 / 1000) + " kilobytes");
            BubbleSortDefault(arrayElements);

            for (int i = 0; i < arrayElements.Length; i++)
            {
                arrayElements[i] = rng.Next(0, 50001);
            }

            Console.WriteLine("Before Half Loops A-B swap program memory: " + (Process.GetCurrentProcess().WorkingSet64 / 1000) + " kilobytes");
            BubbleSortHalfLoopsABSwap(arrayElements);

            for (int i = 0; i < arrayElements.Length; i++)
            {
                arrayElements[i] = rng.Next(0, 50001);
            }

            Console.WriteLine("Before Half Loops TempA-B swap program memory: " + (Process.GetCurrentProcess().WorkingSet64 / 1000) + " kilobytes");
            BubbleSortHalfLoopsTempABSwap(arrayElements);

            for (int i = 0; i < arrayElements.Length; i++)
            {
                arrayElements[i] = rng.Next(0, 50001);
            }

            Console.WriteLine("Before 2x Half loops A-B swap program memory: " + (Process.GetCurrentProcess().WorkingSet64 / 1000) + " kilobytes");
            BubbleSort2XHalfLoopsABSwap(arrayElements);

            for (int i = 0; i < arrayElements.Length; i++)
            {
                arrayElements[i] = rng.Next(0, 50001);
            }

            Console.WriteLine("Before 2x Half loops TempA-B swap program memory: " + (Process.GetCurrentProcess().WorkingSet64 / 1000) + " kilobytes");
            BubbleSort2XHalfLoopsTempABSwap(arrayElements);

            Console.WriteLine("END program memory: " + (Process.GetCurrentProcess().WorkingSet64 / 1000) + " kilobytes");

            Console.ReadKey();
        }

        private static void BubbleSortDefault(int[] arrayElements)
        {
            int tempNumber = 0;
            bool swapped = true;
            //int[] arrayElements = new int[] { 5, 22, 1, 10, 7, 33, 45, 6, 2 };
            Stopwatch counter = new Stopwatch();

            counter.Start();
            while (swapped)
            {
                swapped = false;

                for (int i = 0; i < arrayElements.Length; i++)
                {
                    if (i < arrayElements.Length - 1 && arrayElements[i] > arrayElements[i + 1])
                    {
                        tempNumber = arrayElements[i];
                        arrayElements[i] = arrayElements[i + 1];
                        arrayElements[i + 1] = tempNumber;
                        swapped = true;
                    }
                }
            }
            counter.Stop();

            defaultSortMemory = (Process.GetCurrentProcess().WorkingSet64 - programMemory) / 1000;

            Console.WriteLine("Bubble sort default, cycles: " + arrayElements.Length);
            //Console.WriteLine(string.Join(" ", arrayElements) + " - time for sorting: " + counter.Elapsed + " sec");
            Console.WriteLine("Time for sorting: " + counter.Elapsed + " sec");
            Console.WriteLine("Memory consumation: " + defaultSortMemory + " kilobytes");
            Console.Write(Environment.NewLine);
        }

        private static void BubbleSortHalfLoopsABSwap(int[] arrayElements)
        {
            bool swapped = true;
            //int[] arrayElements = new int[] { 5, 22, 1, 10, 7, 33, 45, 6, 2 };
            Stopwatch counter = new Stopwatch();

            counter.Start();
            while (swapped)
            {
                swapped = false;

                for (int i = 0; i < arrayElements.Length / 2; i++)
                {
                    if (arrayElements[i] > arrayElements[i + 1])
                    {
                        arrayElements[i] = arrayElements[i] + arrayElements[i + 1];
                        arrayElements[i + 1] = arrayElements[i] - arrayElements[i + 1];
                        arrayElements[i] = arrayElements[i] - arrayElements[i + 1];
                        swapped = true;
                    }

                    if (arrayElements[arrayElements.Length - 1 - i] < arrayElements[arrayElements.Length - 2 - i])
                    {
                        arrayElements[arrayElements.Length - 1 - i] = arrayElements[arrayElements.Length - 1 - i] + arrayElements[arrayElements.Length - 2 - i];
                        arrayElements[arrayElements.Length - 2 - i] = arrayElements[arrayElements.Length - 1 - i] - arrayElements[arrayElements.Length - 2 - i];
                        arrayElements[arrayElements.Length - 1 - i] = arrayElements[arrayElements.Length - 1 - i] - arrayElements[arrayElements.Length - 2 - i];
                        swapped = true;
                    }
                }
            }
            counter.Stop();
            
            halfLoopsABSwapMemory = (Process.GetCurrentProcess().WorkingSet64 - programMemory) / 1000;

            Console.WriteLine("Bubble sort, 1x cycles: " + (arrayElements.Length / 2) + ", a <-> b swap");
            //Console.WriteLine(string.Join(" ", arrayElements) + " - time for sorting: " + counter.Elapsed + " sec");
            Console.WriteLine("Time for sorting: " + counter.Elapsed + " sec");
            Console.WriteLine("Memory consumation: " + halfLoopsABSwapMemory + " kilobytes");
            Console.Write(Environment.NewLine);
        }

        private static void BubbleSortHalfLoopsTempABSwap(int[] arrayElements)
        {
            int tempNumber = 0;
            bool swapped = true;
            //int[] arrayElements = new int[] { 5, 22, 1, 10, 7, 33, 45, 6, 2 };
            Stopwatch counter = new Stopwatch();

            counter.Start();
            while (swapped)
            {
                swapped = false;

                for (int i = 0; i < arrayElements.Length / 2; i++)
                {
                    if (arrayElements[i] > arrayElements[i + 1])
                    {
                        tempNumber = arrayElements[i];
                        arrayElements[i] = arrayElements[i + 1];
                        arrayElements[i + 1] = tempNumber;
                        swapped = true;
                    }

                    if (arrayElements[arrayElements.Length - 1 - i] < arrayElements[arrayElements.Length - 2 - i])
                    {
                        tempNumber = arrayElements[arrayElements.Length - 1 - i];
                        arrayElements[arrayElements.Length - 1 - i] = arrayElements[arrayElements.Length - 2 - i];
                        arrayElements[arrayElements.Length - 2 - i] = tempNumber;
                        swapped = true;
                    }
                }
            }
            counter.Stop();
            
            halfLoopsTempABSwapMemory = (Process.GetCurrentProcess().WorkingSet64 - programMemory) / 1000;

            Console.WriteLine("Bubble sort, 1x cycles: " + (arrayElements.Length / 2) + ", temp a,b swap");
            //Console.WriteLine(string.Join(" ", arrayElements) + " - time for sorting: " + counter.Elapsed + " sec");
            Console.WriteLine("Time for sorting: " + counter.Elapsed + " sec");
            Console.WriteLine("Memory consumation: " + halfLoopsTempABSwapMemory + " kilobytes");
            Console.Write(Environment.NewLine);
        }

        private static void BubbleSort2XHalfLoopsABSwap(int[] arrayElements)
        {
            bool swapped = true;
            //int[] arrayElements = new int[] { 5, 22, 1, 10, 7, 33, 45, 6, 2 };
            Stopwatch counter = new Stopwatch();

            counter.Start();
            while (swapped)
            {
                swapped = false;

                for (int i = 0; i < arrayElements.Length / 2; i++)
                {
                    if (arrayElements[i] > arrayElements[i + 1])
                    {
                        arrayElements[i] = arrayElements[i] + arrayElements[i + 1];
                        arrayElements[i + 1] = arrayElements[i] - arrayElements[i + 1];
                        arrayElements[i] = arrayElements[i] - arrayElements[i + 1];
                        swapped = true;
                    }
                }

                for (int i = 0; i < arrayElements.Length / 2; i++)
                {
                    if (arrayElements[arrayElements.Length - 1 - i] < arrayElements[arrayElements.Length - 2 - i])
                    {
                        arrayElements[arrayElements.Length - 1 - i] = arrayElements[arrayElements.Length - 1 - i] + arrayElements[arrayElements.Length - 2 - i];
                        arrayElements[arrayElements.Length - 2 - i] = arrayElements[arrayElements.Length - 1 - i] - arrayElements[arrayElements.Length - 2 - i];
                        arrayElements[arrayElements.Length - 1 - i] = arrayElements[arrayElements.Length - 1 - i] - arrayElements[arrayElements.Length - 2 - i];
                        swapped = true;
                    }
                }
            }
            counter.Stop();
            
            twoXHalfLoopsABSwap = (Process.GetCurrentProcess().WorkingSet64 - programMemory) / 1000;

            Console.WriteLine("Bubble sort, 2x 0.5 cycles: " + (2 * (arrayElements.Length / 2)) + ", a <-> b swap");
            //Console.WriteLine(string.Join(" ", arrayElements) + " - time for sorting: " + counter.Elapsed + " sec");
            Console.WriteLine("Time for sorting: " + counter.Elapsed + " sec");
            Console.WriteLine("Memory consumation: " + twoXHalfLoopsABSwap + " kilobytes");
            Console.Write(Environment.NewLine);
        }

        private static void BubbleSort2XHalfLoopsTempABSwap(int[] arrayElements)
        {
            int tempNumber = 0;
            bool swapped = true;
            //int[] arrayElements = new int[] { 5, 22, 1, 10, 7, 33, 45, 6, 2 };
            Stopwatch counter = new Stopwatch();

            counter.Start();
            while (swapped)
            {
                swapped = false;

                for (int i = 0; i < arrayElements.Length / 2; i++)
                {
                    if (arrayElements[i] > arrayElements[i + 1])
                    {
                        tempNumber = arrayElements[i];
                        arrayElements[i] = arrayElements[i + 1];
                        arrayElements[i + 1] = tempNumber;
                        swapped = true;
                    }
                }

                for (int i = 0; i < arrayElements.Length / 2; i++)
                {
                    if (arrayElements[arrayElements.Length - 1 - i] < arrayElements[arrayElements.Length - 2 - i])
                    {
                        tempNumber = arrayElements[arrayElements.Length - 1 - i];
                        arrayElements[arrayElements.Length - 1 - i] = arrayElements[arrayElements.Length - 2 - i];
                        arrayElements[arrayElements.Length - 2 - i] = tempNumber;
                        swapped = true;
                    }
                }
            }
            counter.Stop();
            
            twoXHalfLoopsTempABSwap = (Process.GetCurrentProcess().WorkingSet64 - programMemory) / 1000;

            Console.WriteLine("Bubble sort, 2x 0.5 cycles: " + (2 * (arrayElements.Length / 2)) + ", temp a,b swap");
            //Console.WriteLine(string.Join(" ", arrayElements) + " - time for sorting: " + counter.Elapsed + " sec");
            Console.WriteLine("Time for sorting: " + counter.Elapsed + " sec");
            Console.WriteLine("Memory consumation: " + halfLoopsTempABSwapMemory + " kilobytes");
            Console.Write(Environment.NewLine);
        }
    }
}
