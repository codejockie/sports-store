using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models
{
  public class EFProductRepository : IProductRepository
  {
    ApplicationDbContext context;

    public EFProductRepository(ApplicationDbContext ctx)
    {
      context = ctx;
    }

    public IQueryable<Product> Products => context.Products;
  }
}
