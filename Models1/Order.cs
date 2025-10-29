namespace App1.Models1;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string CustomerName { get; set; } = "";
    public List<OrderProduct> OrderProducts { get; set; } = new();
}
