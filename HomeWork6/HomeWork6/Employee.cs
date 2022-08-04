using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Globalization;



namespace HomeWork6
{
    internal class Employee
    {
        public int ID; // при записи значение присваивать будет FileAdapter. В этом классе данное поле для чтения. Надо было изначально писать свойствами
        public DateTime InsertDate;
        public string FullName;
        public int Age;
        public int Height;
        public DateOnly BirthDate;
        public string BirthAddress;

        public void FillFields()
        {
            Console.WriteLine("Please input full name");
            FullName = Console.ReadLine();

            Console.WriteLine("Please input age");
            Age = int.Parse(Console.ReadLine());

            Console.WriteLine("Please input height");
            Height = int.Parse(Console.ReadLine());

            IFormatProvider culture = new CultureInfo("en-US", true);
            Console.WriteLine("Please input date of bith (mm/dd/yyyy)");
            BirthDate = DateOnly.Parse(Console.ReadLine());

            Console.WriteLine("Please input address of bith");
            BirthAddress = Console.ReadLine();

            InsertDate = DateTime.Now;
        }


    }

    internal static class EmployeeExt
    {
        public static void PrintData(this Employee employee)
        {
            foreach (FieldInfo filed in typeof(Employee).GetFields())
                Console.WriteLine("{0} = {1}", filed.Name, filed.GetValue(employee));
            Console.WriteLine();
        }
    }
}
