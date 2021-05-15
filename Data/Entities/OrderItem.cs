namespace DutchTreat.Data.Entities
{
  public class OrderItem
  {
    public int Id { get; set; }

    //The property below is used to create a direct relationship between the "OrderItem" and the "Product" class
    //It stems from the fact that an OrderItem is a product being ordered
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    //The property below is also used to create a relationship to the "Order" class
    public Order Order { get; set; }
  }
}