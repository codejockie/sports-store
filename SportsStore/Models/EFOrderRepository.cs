using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SportsStore.Models
{
  public class EFOrderRepository : IOrderRepository
  {
    readonly ApplicationDbContext context;

    public EFOrderRepository(ApplicationDbContext ctx)
    {
      context = ctx;
    }

    public IQueryable<Order> Orders => context.Orders
                                              .Include(o => o.Lines)
                                              .ThenInclude(l => l.Product);

    public void SaveOrder(Order order)
    {
      // prevents Product object from been written if it already exists
      // prevents EF Core from writing the deserialised Product objects that
      // are associated with the Order object.
      context.AttachRange(order.Lines.Select(l => l.Product));
      if (order.OrderID == 0)
      {
        context.Orders.Add(order);
      }
      context.SaveChanges();
    }
  }
}
