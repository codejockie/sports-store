using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
  public class ProductController : Controller
  {
    IProductRepository repository;

    public ProductController(IProductRepository repo)
    {
      repository = repo;
    }

    public IActionResult List() => View(repository.Products);
  }
}
