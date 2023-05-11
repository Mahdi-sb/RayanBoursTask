namespace RayanBours.Domain.Model;

public class Company
{
    private Company()
    {

    }

    public Company(int id, string symbol)
    {
        Id = id;
        Symbol = symbol;
    }
    public int Id { get; set; }
    public string Symbol { get; set; }
}