using ObligatoriskOppgave1.Models;

public static class UserMethods
{  
    // Opprette student (skult valg 99)
    static void CreateStudent(List<Student> students)
    {
        Console.WriteLine("Skriv inn student Navn;");
        string name = Console.ReadLine() ?? "Ukjent navn";
        Console.WriteLine("Skriv inn student Epost;");
        string email = Console.ReadLine() ?? "Ukjent epost";
        Console.WriteLine("Skriv inn student ID;");
        string studentId = Console.ReadLine() ?? "Ukjent ID";
        Student newStudent = new Student
        {
            Name = name,
            Email = email,
            StudentId = studentId
        };
        students.Add(newStudent);
        Console.WriteLine($"Student {name} opprettet.");
    }

    // Opprette ansatt (skult valg 98)
    static void CreateEmployee(List<Employee> employees)
    {
        Console.WriteLine("Skriv inn ansatt Navn;");
        string name = Console.ReadLine() ?? "Ukjent navn";
        Console.WriteLine("Skriv inn ansatt Epost;");
        string email = Console.ReadLine() ?? "Ukjent epost";
        Console.WriteLine("Skriv inn ansatt ID;");
        string employeeId = Console.ReadLine() ?? "Ukjent ID";
        Console.WriteLine("Skriv inn ansatt Stilling;");
        string position = Console.ReadLine() ?? "Ukjent stilling";
        Console.WriteLine("Skriv inn ansatt Avdeling;");
        string department = Console.ReadLine() ?? "Ukjent avdeling";
        Employee newEmployee = new Employee
        {
            Name = name,
            Email = email,
            EmployeeId = employeeId,
            Position = position,
            Department = department
        };
        employees.Add(newEmployee);
        Console.WriteLine($"Ansatt {name} opprettet.");
    }

    /// Opprette utvekslingsstudent (skult valg 97)
    static void CreateExchangeStudent(List<Student> students)
    {
        Console.WriteLine("Skriv inn utvekslingsstudent navn:");
        string name = Console.ReadLine()    ?? "Ukjent navn";

        Console.WriteLine("Skriv inn utvekslingsstudent epost:");
        string email = Console.ReadLine()   ?? "Ukjent epost";

        Console.WriteLine("Skriv inn utvekslingsstudent ID:");
        string studentId = Console.ReadLine()   ?? "Ukjent ID";

        Console.WriteLine("Skriv inn hjemuniversitet:");
        string homeUniversity = Console.ReadLine() ?? "Ukjent universitet";

        Console.WriteLine("Skriv inn land:");
        string country = Console.ReadLine() ?? "Ukjent land";

        Console.WriteLine("Skriv inn startdato (yyyy-mm-dd):");
        DateTime fromDate = DateTime.Parse(Console.ReadLine() ?? "Ukjent dato");

        Console.WriteLine("Skriv inn sluttdato (yyyy-mm-dd):");
        DateTime toDate = DateTime.Parse(Console.ReadLine() ?? "Ukjent dato");

        ExchangeStudent newStudent = new ExchangeStudent
        {
            Name = name,
            Email = email,
            StudentId = studentId,
            HomeUniversity = homeUniversity,
            Country = country,
            FromDate = fromDate,
            ToDate = toDate
        };

        students.Add(newStudent);

        Console.WriteLine($"Utvekslingsstudent {name} opprettet.");
    }
}
