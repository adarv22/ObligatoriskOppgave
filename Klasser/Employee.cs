namespace ObligatoriskOppgave1.Models;

public class Employee : User
{
    public string EmployeeId { get; set; } = "";
    public string Position { get; set; } = "";
    public string Department { get; set; } = "";
    
}