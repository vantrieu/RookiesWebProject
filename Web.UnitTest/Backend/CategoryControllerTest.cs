using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Backend.Controllers;
using Web.Services;
using Web.ShareModels;
using Xunit;

namespace Web.XUnitTest.Backend
{
    public class CategoryControllerTest
    {
        [Fact]
        public async Task Get_return_list_categoriesAsync()
        {
            var mockCategoryRepo = new Mock<ICategoryRepository>();

            List<Category> temp = new List<Category>
            {
                new Category { Id = 1, Name = "QUẦN", Description = "Dành cho nam và nữ" },
                new Category { Id = 2, Name = "ÁO", Description = "Chỉ dành cho nữ" },
                new Category { Id = 3, Name = "VÁY", Description = "Dành cho nam và nữ" },
                new Category { Id = 4, Name = "TÚI", Description = "Chỉ dành cho nữ hoặc giống nữ" },
                new Category { Id = 5, Name = "MŨ", Description = "Dành cho nam và nữ" },
            };

            mockCategoryRepo.Setup(mcp => mcp.GetAllAsync()).ReturnsAsync(temp.AsEnumerable());

            var controller = new CategoryController(mockCategoryRepo.Object);

            var results = await controller.Get();

            Assert.IsType<ActionResult<Category>>(results);
            Assert.NotNull(results);
            var actionResult = Assert.IsType<OkObjectResult>(results.Result);
            Assert.NotNull(actionResult);
            var listCategories = actionResult.Value as List<Category>;
            Assert.Equal(temp.Count, listCategories.Count);
            Assert.Equal(temp, listCategories);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task GetById_return_one_categoryAsync(int value)
        {
            var mockCategoryRepo = new Mock<ICategoryRepository>();

            List<Category> temp = new List<Category>
            {
                new Category { Id = 1, Name = "QUẦN", Description = "Dành cho nam và nữ" },
                new Category { Id = 2, Name = "ÁO", Description = "Chỉ dành cho nữ" },
                new Category { Id = 3, Name = "VÁY", Description = "Dành cho nam và nữ" },
                new Category { Id = 4, Name = "TÚI", Description = "Chỉ dành cho nữ hoặc giống nữ" },
                new Category { Id = 5, Name = "MŨ", Description = "Dành cho nam và nữ" },
            };

            mockCategoryRepo.Setup(mcp => mcp.GetByIdAsync(value)).ReturnsAsync(temp[value]);

            var controller = new CategoryController(mockCategoryRepo.Object);

            var result = await controller.GetById(value);

            Assert.IsType<ActionResult<Category>>(result);
            Assert.NotNull(result);
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(actionResult);
            var categoriy = actionResult.Value as Category;
            Assert.Equal(value + 1, categoriy.Id);
        }

        [Theory]
        [InlineData("QUẦN")]
        [InlineData("TEST")]
        public async Task GetByName_return_one_categoryAsync(string value)
        {
            var mockCategoryRepo = new Mock<ICategoryRepository>();

            List<Category> temp = new List<Category>
            {
                new Category { Id = 1, Name = "QUẦN", Description = "Dành cho nam và nữ" }
            };

            mockCategoryRepo.Setup(mcp => mcp.GetByNameAsync(value)).ReturnsAsync(temp.AsEnumerable());

            var controller = new CategoryController(mockCategoryRepo.Object);

            var results = await controller.GetByName(value);

            Assert.IsType<ActionResult<Category>>(results);
            Assert.NotNull(results);
            var actionResult = Assert.IsType<OkObjectResult>(results.Result);
            Assert.NotNull(actionResult);
            var listCategories = actionResult.Value as List<Category>;
            Assert.Equal(temp.Count, listCategories.Count);
            Assert.Equal(value, listCategories[0].Name);
        }

        [Fact]
        public async Task Add_return_one_categoriesAsync()
        {
            var mockCategoryRepo = new Mock<ICategoryRepository>();

            Category category = new Category { Id = 6, Name = "MŨ", Description = "Dành cho nam và nữ" };

            mockCategoryRepo.Setup(mcp => mcp.CreateAsync(category)).ReturnsAsync(category);

            var controller = new CategoryController(mockCategoryRepo.Object);

            var result = await controller.Add(category);

            Assert.IsType<ActionResult<Category>>(result);
            Assert.NotNull(result);
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(actionResult);
            var value = actionResult.Value as Category;
            Assert.Equal(category, value);
        }

        [Fact]
        public async Task Update_return_one_categoriesAsync()
        {
            var mockCategoryRepo = new Mock<ICategoryRepository>();

            Category category = new Category { Id = 5, Name = "MŨ", Description = "Chỉ dành cho nữ" };

            mockCategoryRepo.Setup(mcp => mcp.UpdateAsync(5, category)).ReturnsAsync(category);

            var controller = new CategoryController(mockCategoryRepo.Object);

            var result = await controller.Edit(5, category);

            Assert.NotNull(result);
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(actionResult);
            var value = actionResult.Value as Category;
            Assert.True(category.Id == value.Id);
        }

        [Fact]
        public async Task Delete_return_one_categoriesAsync()
        {
            var mockCategoryRepo = new Mock<ICategoryRepository>();

            Category category = new Category { Id = 5, Name = "MŨ", Description = "Chỉ dành cho nữ" };

            mockCategoryRepo.Setup(mcp => mcp.DeleteAsync(5)).ReturnsAsync(category);

            var controller = new CategoryController(mockCategoryRepo.Object);

            var result = await controller.Delete(5);

            Assert.NotNull(result);
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(actionResult);
            var value = actionResult.Value as Category;
            Assert.True(category == value);
        }
    }
}
