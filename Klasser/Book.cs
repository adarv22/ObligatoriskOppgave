namespace ObligatoriskOppgave1.Models;

public class Book
{
    public string BookId { get; set; } = "";
    public string Title { get; set; } = "";
    public string Author { get; set; } = "";
    public int Year { get; set; } = 0;
    public int Copies { get; set; } = 0;
}