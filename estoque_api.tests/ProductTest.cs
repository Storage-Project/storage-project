using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.AspNetCore.Mvc;

using storage.Repository;
using storage.Controllers;
using storage.Models;

namespace estoque_api.tests
{
    public class ProductTest
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private List<Product> _products;

        public ProductTest()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _products = GetList();
            _productRepositoryMock.Setup(r => r.GetProductByID(2).Result).Returns(_products.Where(i => i.Id == 2).FirstOrDefault());
        }

        public List<Product> GetList()
        {
            Category category = new Category() { Description = "book" };
            var products = new List<Product>()
            {
                new Product(){ Id=1, Description="A Song of Ice and Fire", Category = category, Price=10.50m, Quantity=10},
                new Product(){ Id=2, Description="The Chronicles of Narnia - The Lion, the Witch and the Wardrobe", Category = category, Price=30.50m, Quantity=15},
                new Product(){ Id=3, Description="Alice in Wonderland", Category = category, Price=20.50m, Quantity=50}
            };
            return products;

        }
        [Fact]
        public async void List_GetProducts_AllProducts()
        {   
            var controller = new ProductController(_productRepositoryMock.Object);

            var result = controller.GetAsync().Result;
            var okResult = result as OkObjectResult;
            var actualResult = okResult.Value as List<Product>;

            Assert.Equal(_products, actualResult);
        }


    }
}
