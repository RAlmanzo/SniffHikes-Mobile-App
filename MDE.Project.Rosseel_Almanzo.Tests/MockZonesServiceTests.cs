using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDE.Project.Rosseel_Almanzo.Tests
{
    public class MockZonesServiceTests
    {
        [Fact]
        public async Task CreateZoneAsync_WithValidZone_ReturnsCreated()
        {
            // Arrange
            var mockZones = new List<Zone>();
            var mockService = new Mock<IZonesService>();

            mockService.Setup(s => s.CreateZoneAsync(It.IsAny<Zone>()))
                       .ReturnsAsync("Created")
                       .Callback<Zone>(zone => mockZones.Add(zone));

            var newZone = new Zone
            {
                Id = "7",
                Title = "Test zone",
                Description = "This is a test zone.",
                Street = "Test Street",
                City = "Test City",
                Country = "Test Country",
            };

            // Act
            var result = await mockService.Object.CreateZoneAsync(newZone);

            // Assert
            Assert.Equal("Created", result);
            Assert.Single(mockZones);
        }

        [Fact]
        public async Task CreateZoneAsync_WithValidZone_AddsZoneToList()
        {
            // Arrange
            var mockZones = new List<Zone>();
            var mockService = new Mock<IZonesService>();

            mockService.Setup(s => s.CreateZoneAsync(It.IsAny<Zone>()))
                       .ReturnsAsync("Created")
                       .Callback<Zone>(zone => mockZones.Add(zone));

            var newZone = new Zone { Id = "1", Title = "zone 1" };

            // Act
            var result = await mockService.Object.CreateZoneAsync(newZone);

            // Assert
            Assert.Contains(newZone, mockZones);
        }

        [Fact]
        public async Task GetAllZonesAsync_WithValidZonesList_ReturnsListOfZones()
        {
            // Arrange
            var mockZones = new List<Zone>
            {
                new Zone
                {
                    Id = "1",
                    Title = "Test zone 1",
                    Description = "Test Description 1",
                },
                new Zone
                {
                    Id = "2",
                    Title = "Test zone 2",
                    Description = "Test Description 2",
                }
            };

            var mockService = new Mock<IZonesService>();

            var baseModels = mockZones.Select(zone => new BaseModel
            {
                Id = zone.Id,
                Title = zone.Title,
                Description = zone.Description,
            }).ToList();

            mockService.Setup(s => s.GetAllZonesAsync()).Returns(Task.FromResult(baseModels));

            // Act
            var result = await mockService.Object.GetAllZonesAsync();

            // Assert
            Assert.Equal(mockZones.Count, result.Count());
        }


        [Fact]
        public async Task GetAllZonesAsync_WithoutExistingZones_ReturnsEmptyList()
        {
            // Arrange
            var mockService = new Mock<IZonesService>();
            mockService.Setup(s => s.GetAllZonesAsync()).Returns(Task.FromResult(new List<BaseModel>()));

            // Act
            var result = await mockService.Object.GetAllZonesAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetZoneByIdAsync_WithValidId_ReturnsZone()
        {
            // Arrange
            var mockZones = new List<Zone>
            {
                new Zone { Id = "1", Title = "zone 1" },
                new Zone { Id = "2", Title = "zone 2" }
            };

            var mockService = new Mock<IZonesService>();
            mockService.Setup(s => s.GetZoneByIdAsync(It.IsAny<string>()))
                       .Returns((string id) => Task.FromResult(mockZones.FirstOrDefault(r => r.Id == id)));

            var id = "1";

            // Act
            var result = await mockService.Object.GetZoneByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task GetZoneByIdAsync_WithInvalidId_ReturnsNull()
        {
            // Arrange
            var mockZones = new List<Zone>
            {
                new Zone { Id = "1", Title = "zone 1" },
                new Zone { Id = "2", Title = "zone 2" }
            };

            var mockService = new Mock<IZonesService>();
            mockService.Setup(s => s.GetZoneByIdAsync(It.IsAny<string>()))
                       .Returns((string id) => Task.FromResult(mockZones.FirstOrDefault(r => r.Id == id)));

            var id = "33";

            // Act
            var result = await mockService.Object.GetZoneByIdAsync(id);

            // Assert
            Assert.Null(result);
        }
    }
}
