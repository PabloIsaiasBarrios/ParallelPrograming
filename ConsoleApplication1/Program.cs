using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            SolucionParalela4();
            Console.ReadLine();
        }

        #region SolucionParalela1
        static void MethodA1()
        {
            Console.WriteLine("Metodo A");
            Thread.Sleep(1000);
        }

        static void MethodB1()
        {
            Console.WriteLine("Metodo B");
            Thread.Sleep(3000);
        }
        #endregion


        /// <summary>
        /// Este metodo crea 2 tareas en paralelo
        /// </summary>
        public static void SolucionParalela1()
        {
            Utils.Start();
            var TaskA = Task.Factory.StartNew(MethodA1);
            var TaskB = Task.Factory.StartNew(MethodB1);
            Task.WaitAll(TaskA, TaskB);
            Utils.Finish();
        }
        /// <summary>
        /// Ejecuta la tarea de manera secuencial con respecto al ejercicio SolucionParalela1
        /// </summary>
        public static void SolucionSecuencial1()
        {
            Utils.Start();
            MethodA1();
            MethodB1();
            Utils.Finish();
        }

        #region SolucionParalela2
        static void MethodA2()
        {
            Thread.SpinWait(4000);
            Console.WriteLine("Metodo A");
        }

        static void MethodB2()
        {
            Thread.SpinWait(1000);
            Console.WriteLine("Metodo B");
        }
        #endregion

        public static void SolucionParalela2()
        {
            //CalcularTiempo.Start();
            var TaskA = Task.Factory.StartNew(MethodA2);
            var TaskB = Task.Factory.StartNew(MethodB2);

            Console.WriteLine($"MethodA2 id = {TaskA.Id}");
            Console.WriteLine($"MethodB2 id = {TaskB.Id}");

            var tasks = new[] { TaskA, TaskB };
            var wichTask = Task.WaitAny(tasks);
            Console.WriteLine($"La tarea {tasks[wichTask].Id} es la tarea de oro");

            Console.WriteLine("Presione enter para salir");
        }


        public static void SolucionParalela3()
        {
            var TaskA = Task<int>.Factory.StartNew(val => ((string)val).Length, "Parallel Programing in Visual Studio");
            TaskA.Wait();
            Console.WriteLine(TaskA.Result);

            Console.WriteLine("Presione enter para salir");
        }

        static int MethodA3()
        {
            return ("Parallel Programing in Visual Studio").Length;
        }

        static int MethodB3(string message)
        {
            return message.Length;
        }

        public static void _SolucionParalela3()
        {
            var message = Console.ReadLine();
            var TaskA = Task<int>.Factory.StartNew(() => MethodB3(message));
            TaskA.Wait();
            Console.WriteLine(TaskA.Result);

            Console.WriteLine("Presione enter para salir");
        }

        public static void SolucionParalela4()
        {
            Console.WriteLine("Metodo Secuencial");
            Utils.Start();
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine(Utils.GetNumber(0, 45));
            }
            Utils.Finish();


            Console.WriteLine("Metodo Paralelo");
            Utils.Start();
            //var lista = new List<Task>();

            Parallel.For(0, 6, i =>
                {
                    var task = Task<int>.Factory.StartNew(() => Utils.GetNumber(0, 45));
                    Console.WriteLine($"Task: {task.Id}  value: {task.Result}");
                    Task.WaitAny(task);
                }
                );
            //for (int i = 0; i < 6; i++)
            //{
            //    var task = Task<int>.Factory.StartNew(() => Utils.GetNumber(0, 45));

            //    //Task.WaitAny(task);
            //    //Console.WriteLine($"Task: {task.Id}  value: {task.Result}");
            //    lista.Add(task);
            //}

            //Parallel.Invoke(() =>
            //{
            //    for (int i = 0; i < 6; i++)
            //    {
            //        Utils.GetNumber(0, 45);
            //    }
            //});
            //var task1 = Task<int>.Factory.StartNew( Utils.GetNumber1);

            //var task2 = Task<int>.Factory.StartNew(Utils.GetNumber2);
            //Task.WaitAll(task1, task2);
            //Console.WriteLine($"Task: {task1.Id}  value: {task1.Result}");

            //Console.WriteLine($"Task: {task2.Id}  value: {task2.Result}");

            Utils.Finish();
        }
    }
}
