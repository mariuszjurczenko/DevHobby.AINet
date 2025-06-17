namespace DevHobby.GPTizza.Model;

public class OrderDetail
{
    public int OrderDetailId { get; set; }
    public int OrderId { get; set; }
    public Pizza Pizza { get; set; }
    public int PizzaId { get; set; }
    public int Amount { get; set; }
}