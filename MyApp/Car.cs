using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;


namespace MyApp
{
    public sealed class Car
    {
        public Car(string name, string model, string color)
        {
            Name = name;
            Model = model;
            Color = color;
        }
        public Car() { }


        public void Display()
        {
            Console.WriteLine($"Name - {Name}\nModel - {Model}\nColor - {Color}");
        }

        public string Name { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
    }
}
