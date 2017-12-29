using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SportsStore.Tests
{
  public class AdminControllerTests
  {
    [Fact]
    public void Index_Contains_All_Products()
    {
      // Arrange - create the mock repository
      Mock<IProductRepository> mock = new Mock<IProductRepository>();
      mock.Setup(m => m.Products).Returns(new Product[] {
        new Product {ProductID = 1, Name = "P1"},
        new Product {ProductID = 2, Name = "P2"},
        new Product {ProductID = 3, Name = "P3"},
      }.AsQueryable<Product>());

      // Arrange - create a controller
      AdminController target = new AdminController(mock.Object);

      // Action
      Product[] result
        = GetViewModel<IEnumerable<Product>>(target.Index())?.ToArray();

      // Assert
      Assert.Equal(3, result.Length);
      Assert.Equal("P1", result[0].Name);
      Assert.Equal("P2", result[1].Name);
      Assert.Equal("P3", result[2].Name);
    }

    T GetViewModel<T>(IActionResult result) where T : class
    {
      return (result as ViewResult)?.ViewData.Model as T;
    }

    [Fact]
    public void Can_Edit_Product()
    {
      // Arrange - create the mock repository
      var mock = new Mock<IProductRepository>();
      mock.Setup(m => m.Products).Returns(new Product[] {
        new Product {ProductID = 1, Name = "P1"},
        new Product {ProductID = 2, Name = "P2"},
        new Product {ProductID = 3, Name = "P3"},
      }.AsQueryable<Product>());

      // Arrange - create the controller
      var target = new AdminController(mock.Object);

      // Act
      var p1 = GetViewModel<Product>(target.Edit(1));
      var p2 = GetViewModel<Product>(target.Edit(2));
      var p3 = GetViewModel<Product>(target.Edit(3));

      // Assert
      Assert.Equal(1, p1.ProductID);
      Assert.Equal(2, p2.ProductID);
      Assert.Equal(3, p3.ProductID);
    }

    [Fact]
    public void Cannot_Edit_Nonexistent_Product()
    {
      // Arrange - create the mock repository
      var mock = new Mock<IProductRepository>();
      mock.Setup(m => m.Products).Returns(new Product[] {
        new Product {ProductID = 1, Name = "P1"},
        new Product {ProductID = 2, Name = "P2"},
        new Product {ProductID = 3, Name = "P3"},
      }.AsQueryable<Product>());

      // Arrange - create the controller
      var target = new AdminController(mock.Object);

      // Act
      var result = GetViewModel<Product>(target.Edit(4));

      // Assert
      Assert.Null(result);
    }
  }
}