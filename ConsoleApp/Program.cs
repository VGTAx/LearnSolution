using System.Reflection;
using System.Diagnostics;
using System.Linq;
using System;
using System.Reflection;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //RunningProces();  
            InitDAD();
            DisplayDADStats();
            InitDAD();

        }
        static void RunningProces()
        {
            var runningProc = from p in Process.GetProcesses(".") orderby p.Id select p;

            foreach (var item in runningProc)
            {
                System.Console.WriteLine($"PID: {item.Id}\tName: {item.ProcessName}");
            }
           
        }
        static void GetSpecifyProcess()
        {
            Process process = null;
            int numberProc = int.Parse(Console.ReadLine());
            process = Process.GetProcessById(numberProc);
            System.Console.WriteLine($"{process.ProcessName}, {process.Id}");

            ProcessThreadCollection processThread = process.Threads;
            foreach (ProcessThread pt in processThread)
            {
                Console.WriteLine($"ID: {pt.Id}\tStart Time: {pt.StartTime.ToLongTimeString()}\tPriority lvl: {pt.PriorityLevel}\t" +
                    $"Adress:{pt.StartAddress}\t {pt.ThreadState}");
            }
        }
        static void EnumModulePID()
        {
            Process process = null;
            int pid = int.Parse(Console.ReadLine());
            process = Process.GetProcessById(pid);

            ProcessModuleCollection processModule = process.Modules;
            foreach (ProcessModule pm in processModule)
            {
                Console.WriteLine($" {pm.ModuleName} {pm.FileName} {pm.Container}");
            }
        }

        static void StartAndKillProc()
        {
            Process ffproc = null;
            ProcessStartInfo startInfo = new ProcessStartInfo();         
            startInfo.WindowStyle = ProcessWindowStyle.Maximized;
            startInfo.FileName = "E:/Games/Steam/steamapps/common/The Witcher 3/bin/x64/witcher3.exe";
            ffproc = Process.Start(startInfo);
            Console.ReadLine();
          
        }

        static void DisplayDADStats()
        {
            InitDAD();
            AppDomain appDomain = AppDomain.CurrentDomain;
            InitDAD();
            Console.WriteLine($"Friednly name: {appDomain.FriendlyName}\n" +
                $"is default?: {appDomain.IsDefaultAppDomain()}\n" +
                $"ID: {appDomain.Id}\n" +
                $"Base directory: {appDomain.BaseDirectory}\n");
            var assemblies = from p in appDomain.GetAssemblies() orderby p.GetName().Name select p;
            InitDAD();
            foreach (var item in assemblies)
            {
                Console.WriteLine($"Name: {item.GetName()}\n" +
                    $"Location: {item.Location}\n");
            }
        }

        private static void InitDAD()
        {
            AppDomain appDomain = AppDomain.CurrentDomain;
            appDomain.AssemblyLoad += (o, s) => Console.WriteLine($"{o} has loaded, {s.LoadedAssembly.GetName().Name}");
        }
    }
}
