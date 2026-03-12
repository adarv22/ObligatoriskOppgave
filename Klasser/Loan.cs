namespace ObligatoriskOppgave1.Models;

public class Loan
{
    public User Borrower { get; set; } = null!;
    public Book Book { get; set; } = null!;
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public bool Returned { get; set; }
}
