using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;

namespace SportsStore.Controllers
{
  public class ProductController : Controller
  {
    IProductRepository repository;
    public int PageSize = 4;

    public ProductController(IProductRepository repo)
    {
      repository = repo;
    }

    public IActionResult List(int productPage = 1) =>
      View(repository.Products
           .OrderBy(p => p.ProductID)
           .Skip((productPage - 1) * PageSize)
           .Take(PageSize));
  }
}
