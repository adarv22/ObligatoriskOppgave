namespace ObligatoriskOppgave1.Models;
public class ExchangeStudent : Student
{
    public string HomeUniversity { get; set; } = "";
    public string Country { get; set; } = "";
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}