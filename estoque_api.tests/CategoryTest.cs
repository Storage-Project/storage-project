using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Moq;
using Microsoft.AspNetCore.Mvc;

using storage.Repository;
using storage.Controllers;
using storage.Models;

namespace estoque_api.tests
{
    public class CategoryTest
    {
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly CategoryController _categoryController;
        private List<Category> _categories;

        public CategoryTest()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _categories = GetList();
            _categoryRepositoryMock.Setup(r => r.GetCategories().Result).Returns(_categories);
            _categoryController = new CategoryController(_categoryRepositoryMock.Object);
        }

        public List<Category> GetList()
        {
            var categories = new List<Category>()
            {
                new Category(){ Id=1, Description="Book"},
                new Category(){ Id=2, Description="Game"},
                new Category(){ Id=3, Description="Electronic"}
            };
            return categories;

        }
        [Fact]
        public void List_GetProducts_AllProducts()
        {
            var result = _categoryController.GetAsync().Result;
            var okResult = result as OkObjectResult;
            var actualResult = okResult.Value as List<Category>;

            Assert.Equal(_categories, actualResult);
        }
    }
}
