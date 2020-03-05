using System;

namespace MaxSum
{
    class Program
    {
        static void Main(string[] args)
        {
            MaxSum myClass = new MaxSum();

            // Test when N=10^9
            int[] bigData = new int[1000000000];
            Array.Fill(bigData, 1);
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            int sumBigData = myClass.FindSum(bigData);
            watch.Stop();
            Console.WriteLine("Array N=10^9 Sum: " + sumBigData+" $Time: " + watch.ElapsedMilliseconds +" ms");

            // Simple Test:
            int[] test1 = { 1, 2, 3, 1 };
            watch.Reset();
            watch.Start();
            int sumTest1 = myClass.FindSum(test1);
            watch.Stop();
            Console.WriteLine("{ 1, 2, 3, 1 } Sum: " + sumTest1+" $Time: " + watch.ElapsedMilliseconds + " ms");

            // Simple Test2:
            int[] test2 = { 2, 7, 9, 3, 1 };
            watch.Reset();
            watch.Start();
            int sumTest2 = myClass.FindSum(test2);
            watch.Stop();
            Console.WriteLine("{ 2, 7, 9, 3, 1 } Sum: " + sumTest2 + " $Time: " + watch.ElapsedMilliseconds + " ms");

            // Simple Test3:
            int[] test3 = { 5, 1, 2, 5 };
            watch.Reset();
            watch.Start();
            int sumTest3 = myClass.FindSum(test3);
            watch.Stop();
            Console.WriteLine("{ 5, 1, 2, 5 } Sum: " + sumTest3 + " $Time: " + watch.ElapsedMilliseconds + " ms");

            Console.ReadLine();
        }

        public class MaxSum
        {
            
            // spatial complexity that method is O(N) where N lenght of array
            public int FindSum(int[] arr)
            {
                int sum = 0;
                int step = 0;
                for (int i = 0; i < arr.Length; i++)
                {
                    // if the check went far ahead, take a free element for the sum
                    if (step == 2)
                    {
                        sum = sum + arr[i - 2];
                    }

                    //check can we take current element for calculating sum, or better check next element 
                    if ((arr.Length - 1) == i || arr[i] >= arr[i + 1])
                    {
                        sum = sum + arr[i];
                        step = 0;
                        
                        //if we took an item to calculate the amount, skip the next item
                        i++;
                    }
                    else
                    {
                        step++;
                    }

                }
                return sum;
            }
        }
    }
}
