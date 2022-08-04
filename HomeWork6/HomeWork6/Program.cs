using HomeWork6;

Console.WriteLine("Welcome to the program");

bool _continue = true;

while (_continue)
{
    Console.WriteLine("Please choose the option (input the number)\n\n");
    Console.WriteLine("1. Read file data");
    Console.WriteLine("2. Add data");
    Console.WriteLine("3. Exit\n\n");
    Console.Write("Your choice here:");
    string choice = Console.ReadLine();
    Console.WriteLine("\n\n");

    if (choice == "1")
    {
        await FileAdapter.StartingFileProcessing();
        await FileAdapter.ReadAndPrint();
    }
    else if (choice=="2")
    {
        await FileAdapter.StartingFileProcessing();
        Employee employee = new Employee();
        employee.FillFields();
        await FileAdapter.Write(employee);
    }
    else if (choice == "3")
        _continue = false;
    else
        Console.WriteLine("Input parameter is wrong. Try again.");
}




