using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;

namespace SportsStore.Controllers
{
  public class AdminController : Controller
  {
    readonly IProductRepository repository;

    public AdminController(IProductRepository repo)
    {
      repository = repo;
    }

    public ViewResult Index() => View(repository.Products);

    public ViewResult Edit(int productId) =>
      View(repository.Products
          .FirstOrDefault(p => p.ProductID == productId));
  }
}
