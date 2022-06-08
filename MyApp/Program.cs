using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.Dynamic;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;



namespace MyApp
{
    internal class DynamicObjectTest : DynamicObject
    {
        private Dictionary<string, object> data = new Dictionary<string, object>();
        public override bool TrySetMember(SetMemberBinder binder, object? value)
        {
            if (value is not null)
            {
                data[binder.Name] = value;
                return true;
            }
            return false;
        }
        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            result = null;
            if (data.ContainsKey(binder.Name))
            {
                result = data[binder.Name];
                return true;
            }
            return false;
        }
        public override bool TryInvokeMember(InvokeMemberBinder binder, object?[]? args, out object? result)
        {
            result = null;
            if (args?[0] is Person number)
            {
                dynamic method = data[binder.Name];
                result = method(number);
            }
            return result != null;
        }
    }
    internal class Program
    {
        private static void Main()
        {
            Assembly assembly = Assembly.LoadFrom("E:/C#/BankSystem/BankSystem/bin/Debug/net6.0/BankSystem.dll");
            Type? type = assembly.GetType("BankSystem.Bank");
            //object ?obj = Activator.CreateInstance(type);
            //MethodInfo methodInfo = type.GetMethod("ManageMain");
            //methodInfo.Invoke(obj, null);
            dynamic obj = Activator.CreateInstance(type);
            obj.ManageMain();

            ScriptEngine scriptEngine = Python.CreateEngine();
            ScriptScope scriptScope = scriptEngine.CreateScope();

            scriptEngine.ExecuteFile("hello.py.txt", scriptScope);
            dynamic square = scriptScope.GetVariable("square");

            dynamic result = square(6);
            Console.WriteLine(result);

        }

        private static void ExportToExcel(List<Car> carInStock)
        {
            Excel.Application application = new Excel.Application();
            application.Visible = true;
            //application.Workbooks.Add();
            application.Workbooks.OpenText("E:/C#/LearnSolution/MyApp/bin/Debug/net6.0/Inventore.xlsx");

            Excel._Worksheet worksheet = application.ActiveSheet;

            worksheet.Cells[1, "A"] = "Name";
            worksheet.Cells[1, "B"] = "Model";
            worksheet.Cells[1, "C"] = "Color";

            int testRow = 5;
            int row = testRow;
            foreach (var car in carInStock)
            {
                row++;
                worksheet.Cells[row, "A"] = car.Name;
                worksheet.Cells[row, "B"] = car.Model;
                worksheet.Cells[row, "C"] = car.Color;
            }

            worksheet.Range["A1"].AutoFormat(Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic2);
            worksheet.SaveAs($@"E:/C#/LearnSolution/MyApp/bin/Debug/net6.0/Inventore.xlsx");
            application.Quit();
            Console.WriteLine("The Inventory.xslx file has been saved to your app folder");
        }
    }

    internal class Person
    {
        public Person(int age, string name)
        {
            Age = age;
            Name = name;
        }


        public void Display()
        {
            Console.WriteLine($"Age - {Age}/nName - {Name}");
        }

        public int Age { get; set; }
        public string Name { get; set; }
    }

    
}
