using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data.Entities
{
  public class Order
  {
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string OrderNumber { get; set; }

    //The code written below is used to create a relationship between two entity classes
    //The "ICollection" simply means that this is a one to many relationship; one order can have many order items
    //The fact that you're calling the "OrderItem" class here indicates that the "Order" class is the parent class
    public ICollection<OrderItem> Items { get; set; }
  }
}
