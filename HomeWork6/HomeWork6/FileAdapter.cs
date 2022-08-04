using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Globalization;

namespace HomeWork6
{
    internal static class FileAdapter
    {
        static string fileBuffer; //File data will be loaded here
        static int ID;
        public static async Task StartingFileProcessing()
        {
            if (File.Exists(@"Employees.txt"))
            {
                using (FileStream fstream = File.OpenRead(@"Employees.txt"))
                {
                    byte[] buffer = new byte[fstream.Length];
                    await fstream.ReadAsync(buffer, 0, buffer.Length);
                    fileBuffer = Encoding.Default.GetString(buffer);
                }

                int countOfLines = 0;

                foreach (char c in fileBuffer)
                    if (c == '\n')
                        countOfLines++;
                ID = countOfLines; // находим пследний ID
            }
            else
                File.Create(@"Employees.txt");
        }

        public static async Task Write(Employee employee)
        {
            string textToWrite = SerializeData(employee);

            using (FileStream fstream = new FileStream(@"Employees.txt", FileMode.OpenOrCreate))
            {
                fstream.Seek(0, SeekOrigin.End);
                byte[] buffer = Encoding.Default.GetBytes(textToWrite);
                await fstream.WriteAsync(buffer, 0, buffer.Length);
            }
        }

        public static async Task ReadAndPrint()//I'd like to separate them for single responsibility. But it would make chane of methods
        {
            if (fileBuffer == null)
                Console.WriteLine("File is empty. Insert data first");
            else
            {
                using (FileStream fstream = File.OpenRead(@"Employees.txt"))
                {
                    byte[] buffer = new byte[fstream.Length];
                    await fstream.ReadAsync(buffer, 0, buffer.Length);
                    fileBuffer = Encoding.Default.GetString(buffer);
                }

                Employee[] employees = DeserializeData(fileBuffer);
                foreach (Employee employee in employees)
                {
                    employee.PrintData();
                }
            }
        }

        public static string SerializeData(Employee employee) //Text encode
        {
            StringBuilder outputText = new StringBuilder();// для скоросости и экономии памяти при конкатизации

            foreach (FieldInfo field in typeof(Employee).GetFields())
            {
                if (field.Name == "ID")
                    outputText.Append(++ID);
                else
                    outputText.Append($"#{field.GetValue(employee)}");
            }
            outputText.Append("\n");

            return outputText.ToString();
        }

        public static Employee[] DeserializeData(string rawText) // Text decode
        {
            string[] employeesArr = rawText.Split('\n');
            Employee[] employeesList = new Employee[employeesArr.Length - 1];

            for (int i = 0; i < employeesArr.Length - 1; i++)
            {
                Employee employee = new Employee();
                string[] employeeFields = employeesArr[i].Split('#');
                //IFormatProvider culture = new CultureInfo("en-US", true);
                //employee.InsertDate = DateTime.ParseExact(employeeFields[1], "dd.MM.yyyy HH:mm", culture);

                employee.ID = int.Parse(employeeFields[0]);
                employee.InsertDate = DateTime.Parse(employeeFields[1]);
                employee.FullName = employeeFields[2];
                employee.Age = int.Parse(employeeFields[3]);
                employee.Height = int.Parse(employeeFields[4]);
                employee.BirthDate = DateOnly.Parse(employeeFields[5]);
                employee.BirthAddress = employeeFields[6];

                employeesList[i] = employee;
            }

            return employeesList;
        }


    }
}
