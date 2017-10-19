using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Utils
    {
        private static Stopwatch stopWatch;
        private static Random rnd = new Random();

        public static void Start()
        {
            stopWatch = new Stopwatch();
            stopWatch.Start();
        }

        public static void Finish()
        {
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }

        public static int GetNumber(int min , int max)
        {
            Thread.Sleep(1000);
            return rnd.Next(min, max);
        }
   
    }
}
