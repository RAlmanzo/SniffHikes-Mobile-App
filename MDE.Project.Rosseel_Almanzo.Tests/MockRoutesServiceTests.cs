using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Mock;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDE.Project.Rosseel_Almanzo.Tests
{
    public class MockRoutesServiceTests
    {
        [Fact]
        public async Task CreateRouteAsync_WithValidRoute_ReturnsCreated()
        {
            // Arrange
            var mockRoutes = new List<Route>();
            var mockService = new Mock<IRoutesService>();

            mockService.Setup(s => s.CreateRouteAsync(It.IsAny<Route>()))
                       .ReturnsAsync("Created")
                       .Callback<Route>(route => mockRoutes.Add(route));

            var newRoute = new Route
            {
                Id = "7",
                Title = "Test Route",
                Description = "This is a test route.",
                Street = "Test Street",
                City = "Test City",
                Country = "Test Country",
                DateEvent = DateTime.Now,
                OrganizerId = "Test OrganizerId"
            };

            // Act
            var result = await mockService.Object.CreateRouteAsync(newRoute);

            // Assert
            Assert.Equal("Created", result);
            Assert.Single(mockRoutes);
        }

        [Fact]
        public async Task CreateRouteAsync_WithValidRoute_AddsRouteToList()
        {
            // Arrange
            var mockRoutes = new List<Route>();
            var mockService = new Mock<IRoutesService>();
            mockService.Setup(s => s.CreateRouteAsync(It.IsAny<Route>()))
                       .ReturnsAsync("Created")
                       .Callback<Route>(route => mockRoutes.Add(route));

            var newRoute = new Route { Id = "1", Title = "Route 1" };

            // Act
            var result = await mockService.Object.CreateRouteAsync(newRoute);

            // Assert
            Assert.Contains(newRoute, mockRoutes);
        }


        [Fact]
        public async Task GetAllRoutesAsync_WithValidRoutesList_ReturnsListOfRoutes()
        {
            // Arrange
            var mockRoutes = new List<Route>
            {
                new Route
                {
                    Id = "1",
                    Title = "Test Route 1",
                    Description = "Test Description 1",
                    Images = new List<Image>
                    {
                        new Image { Title = "Image 1", ImagePath = "image1.jpg" }
                    }
                },
                new Route
                {
                    Id = "2",
                    Title = "Test Route 2",
                    Description = "Test Description 2",
                    Images = new List<Image>
                    {
                        new Image { Title = "Image 2", ImagePath = "image2.jpg" }
                    }
                }
            };

            var mockService = new Mock<IRoutesService>();

            var baseModels = mockRoutes.Select(route => new BaseModel
            {
                Id = route.Id,
                Title = route.Title,
                Description = route.Description,
                Image = route.Images?.FirstOrDefault()
            });

            mockService.Setup(s => s.GetAllRoutesAsync()).Returns(Task.FromResult(baseModels));

            // Act
            var result = await mockService.Object.GetAllRoutesAsync();

            // Assert
            Assert.Equal(mockRoutes.Count, result.Count());
        }

        [Fact]
        public async Task GetAllRoutesAsync_WithEmptyRoutes_ReturnsEmptyList()
        {
            // Arrange
            var mockService = new Mock<IRoutesService>();
            mockService.Setup(s => s.GetAllRoutesAsync()).Returns(Task.FromResult<IEnumerable<BaseModel>>(new List<BaseModel>()));

            // Act
            var result = await mockService.Object.GetAllRoutesAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetRouteByIdAsync_WithValidId_ReturnsRoute()
        {
            // Arrange
            var mockRoutes = new List<Route>
            {
                new Route { Id = "1", Title = "Route 1" },
                new Route { Id = "2", Title = "Route 2" }
            };

            var mockService = new Mock<IRoutesService>();
            mockService.Setup(s => s.GetRouteByIdAsync(It.IsAny<string>()))
                       .Returns((string id) => Task.FromResult(mockRoutes.FirstOrDefault(r => r.Id == id)));

            var idToFind = "1";

            // Act
            var result = await mockService.Object.GetRouteByIdAsync(idToFind);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(idToFind, result.Id);
        }

        [Fact]
        public async Task GetRouteByIdAsync_WithInvalidId_ReturnsNull()
        {
            // Arrange
            var mockRoutes = new List<Route>
            {
                new Route { Id = "1", Title = "Route 1" },
                new Route { Id = "2", Title = "Route 2" }
            };

            var mockService = new Mock<IRoutesService>();
            mockService.Setup(s => s.GetRouteByIdAsync(It.IsAny<string>()))
                       .Returns((string id) => Task.FromResult(mockRoutes.FirstOrDefault(r => r.Id == id)));

            var idToFind = "3";

            // Act
            var result = await mockService.Object.GetRouteByIdAsync(idToFind);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllRoutesByUserId_WithValidUserId_ReturnsRoutesForUser()
        {
            // Arrange
            var userId = "2";
            var mockRoutes = new List<Route>
            {
                new Route { Id = "1", Title = "Route 1", OrganizerId = "1" },
                new Route { Id = "2", Title = "Route 2", OrganizerId = "2" },
                new Route { Id = "3", Title = "Route 3", OrganizerId = "2" },
                new Route { Id = "4", Title = "Route 4", OrganizerId = "3" }
            };

            var mockService = new Mock<IRoutesService>();
            mockService.Setup(s => s.GetAllRoutesByUserId(It.IsAny<string>()))
                       .Returns((string id) => Task.FromResult<IEnumerable<BaseModel>>(
                           mockRoutes.Where(r => r.OrganizerId == id)
                                     .Select(e => new BaseModel
                                     {
                                         Id = e.Id,
                                         Title = e.Title,
                                         Description = e.Description,
                                         Image = e.Images?.FirstOrDefault(),
                                     }).ToList()));

            // Act
            var result = await mockService.Object.GetAllRoutesByUserId(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAllRoutesByUserId_WithInvalidUserId_ReturnsEmptyList()
        {
            // Arrange
            var userId = "999";
            var mockService = new Mock<IRoutesService>();
            mockService.Setup(s => s.GetAllRoutesByUserId(It.IsAny<string>()))
                       .Returns((string id) => Task.FromResult<IEnumerable<BaseModel>>(
                           new List<BaseModel>()));

            // Act
            var result = await mockService.Object.GetAllRoutesByUserId(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
