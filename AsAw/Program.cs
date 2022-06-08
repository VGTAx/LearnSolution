using System;
using System.Threading;
using System.Runtime.Remoting.Messaging;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace AsAw
{
    public delegate int MyDelegate(int x);
    internal class Program
    {
        private static AutoResetEvent m_event = new AutoResetEvent(false);
        static int i = 0;
        private static bool working = false;
        static void Main(string[] args)
        {
            MyDelegate myDelegate = new MyDelegate(Display);
            Console.WriteLine("Main method work");
            Console.WriteLine($"Id Thread: {Thread.CurrentThread.ManagedThreadId}");
            IAsyncResult async = myDelegate.BeginInvoke(10, new AsyncCallback(AsyncCompleted), null);
            Process process = null;
            //process = Process.GetCurrentProcess();
            //ProcessThreadCollection processThread = process.Threads;
            //foreach (ProcessThread item in processThread)
            //{
            //    Console.WriteLine($"Name: {item.StartTime.ToLongTimeString()}, ID: {item.Id}");
            //}
            Console.WriteLine("Main finished work");


            Person person = new Person("Valera", 15);
            Person person1 = new Person("Oleg", 18);

            //Thread thread4 = new Thread(new ParameterizedThreadStart(Add));
            //thread4.Start(person);

            //Thread thread5 = new Thread(new ParameterizedThreadStart(Add));
            //thread5.Start(person1);

            Thread thread1 = new Thread(new ThreadStart(MethodThread));
            thread1.Name = "First";
            thread1.IsBackground = true;
            thread1.Start();
            Console.WriteLine($"Thread {thread1.Name} is alive?:{thread1.IsAlive}");
            m_event.WaitOne();
            Console.WriteLine($"Thread {thread1.Name} is alive?:{thread1.IsAlive}");

            //Thread thread2 = new Thread(new ThreadStart(MethodThread));
            //thread2.Name = "Second";
            //thread2.Start();
            //Console.WriteLine($"Thread {thread2.Name} is alive?:{thread2.IsAlive}");
            //m_event.WaitOne();
            //thread2.Abort();
            //Console.WriteLine($"Thread {thread2.Name} is alive?:{thread2.IsAlive}");


            //Thread thread3 = new Thread(new ThreadStart(MethodThread));
            //thread3.Name = "Thirth";
            //thread3.Start();
            //Console.WriteLine($"Thread {thread3.Name} is alive?:{thread3.IsAlive}");
            //m_event.WaitOne();
            //Console.WriteLine($"Thread {thread3.Name} is alive?:{thread3.IsAlive}");

            Console.ReadLine();

        }
        static int Display(int x)
        {
            Console.WriteLine();
            Console.WriteLine($"Id Thread: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("Start Display method");
            Thread.Sleep(2000);
            return x * x;
        }
        static void AsyncCompleted(IAsyncResult resObj)
        {
            string str = (string)resObj.AsyncState;
            AsyncResult result = (AsyncResult)resObj;
            MyDelegate myDelegate = (MyDelegate)result.AsyncDelegate;
            int x = myDelegate.EndInvoke(resObj);
            Console.WriteLine($"\nResult of {myDelegate.Method.Name} method = {x}");
            Console.WriteLine($"Async method {myDelegate.Method.Name} finished work");
            Console.WriteLine(str);
            working = true;
        }

        static int Show(int x)
        {
            Console.WriteLine();
            Console.WriteLine($"Id Thread: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("Start Show method");
            Thread.Sleep(2000);
            return x;
        }


        static void MethodThread()
        {            
            while (i < 10)
            {
                Console.WriteLine(i++);
                Thread.Sleep(200);
            }
            while(i>10 && i<20)
            {
                Console.WriteLine(i++);
                Thread.Sleep(200);
            }
            
            while (i > 20 && i < 30)
            {
                Console.WriteLine(i++);
                Thread.Sleep(200);
            }
            i++;

            m_event.Set();
        }

        static void Add(object obj)
        {
            if(obj is int n)
            {
                Console.WriteLine(n*n);
            }
            //int y = int.Parse(Console.ReadLine());
            if (obj is Person prs)
            {
                prs.Display();
            }
        }
    }
    class Person
    {
        public Person()  { }
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public void Display()
        {
            Console.WriteLine($"Name - {Name}\nAge - {Age}");
        }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
