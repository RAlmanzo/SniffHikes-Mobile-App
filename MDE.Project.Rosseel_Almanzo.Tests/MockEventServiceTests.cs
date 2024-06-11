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
    public class MockEventServiceTests
    {
        [Fact]
        public async Task CreateEventAsync_WithValidEvent_ReturnsCreated()
        {
            // Arrange
            var mockEvents = new List<Event>();
            var mockService = new Mock<IEventsService>();

            mockService.Setup(s => s.CreateEventAsync(It.IsAny<Event>()))
                       .ReturnsAsync("Created")
                       .Callback<Event>(e => mockEvents.Add(e));

            var newEvent = new Event
            {
                Id = "7",
                Title = "Test event",
                Description = "This is a test event.",
                Street = "Test Street",
                City = "Test City",
                Country = "Test Country",
                DateEvent = DateTime.Now,
                OrginazerId = "Test OrganizerId"
            };

            // Act
            var result = await mockService.Object.CreateEventAsync(newEvent);

            // Assert
            Assert.Equal("Created", result);
            Assert.Single(mockEvents);
        }

        [Fact]
        public async Task CreateEventAsync_WithValidEvent_AddsEventToList()
        {
            // Arrange
            var mockEvents = new List<Event>();
            var mockService = new Mock<IEventsService>();

            mockService.Setup(s => s.CreateEventAsync(It.IsAny<Event>()))
                       .ReturnsAsync("Created")
                       .Callback<Event>(e => mockEvents.Add(e));

            var newEvent = new Event { Id = "1", Title = "event 1" };

            // Act
            var result = await mockService.Object.CreateEventAsync(newEvent);

            // Assert
            Assert.Contains(newEvent, mockEvents);
        }


        [Fact]
        public async Task GetAllEventsAsync_WithValidEventsList_ReturnsListOfEvents()
        {
            // Arrange
            var mockEvents = new List<Event>
            {
                new Event
                {
                    Id = "1",
                    Title = "Test event 1",
                    Description = "Test Description 1",
                },
                new Event
                {
                    Id = "2",
                    Title = "Test event 2",
                    Description = "Test Description 2",
                }
            };

            var mockService = new Mock<IEventsService>();

            var baseModels = mockEvents.Select(route => new BaseModel
            {
                Id = route.Id,
                Title = route.Title,
                Description = route.Description,
            });

            mockService.Setup(s => s.GetAllEventsAsync()).Returns(Task.FromResult(baseModels));

            // Act
            var result = await mockService.Object.GetAllEventsAsync();

            // Assert
            Assert.Equal(mockEvents.Count, result.Count());
        }

        [Fact]
        public async Task GetAllEventsAsync_WithoutExistingEvents_ReturnsEmptyList()
        {
            // Arrange
            var mockService = new Mock<IEventsService>();
            mockService.Setup(s => s.GetAllEventsAsync()).Returns(Task.FromResult<IEnumerable<BaseModel>>(new List<BaseModel>()));

            // Act
            var result = await mockService.Object.GetAllEventsAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetEventByIdAsync_WithValidId_ReturnsEvent()
        {
            // Arrange
            var mockEvents = new List<Event>
            {
                new Event { Id = "1", Title = "event 1" },
                new Event { Id = "2", Title = "event 2" }
            };

            var mockService = new Mock<IEventsService>();
            mockService.Setup(s => s.GetEventByIdAsync(It.IsAny<string>()))
                       .Returns((string id) => Task.FromResult(mockEvents.FirstOrDefault(r => r.Id == id)));

            var id = "1";

            // Act
            var result = await mockService.Object.GetEventByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task GetEventByIdAsync_WithInvalidId_ReturnsNull()
        {
            // Arrange
            var mockEvents = new List<Event>
            {
                new Event { Id = "1", Title = "event 1" },
                new Event { Id = "2", Title = "event 2" }
            };

            var mockService = new Mock<IEventsService>();
            mockService.Setup(s => s.GetEventByIdAsync(It.IsAny<string>()))
                       .Returns((string id) => Task.FromResult(mockEvents.FirstOrDefault(r => r.Id == id)));

            var id = "3";

            // Act
            var result = await mockService.Object.GetEventByIdAsync(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllEventsByUserId_WithValidUserId_ReturnsEventsOfUser()
        {
            // Arrange
            var userId = "2";
            var mockEvents = new List<Event>
            {
                new Event { Id = "1", Title = "Event 1", OrginazerId = "1" },
                new Event { Id = "2", Title = "Event 2", OrginazerId = "2" },
                new Event { Id = "3", Title = "Event 3", OrginazerId = "2" },
                new Event { Id = "4", Title = "Event 4", OrginazerId = "3" }
            };

            var mockService = new Mock<IEventsService>();
            mockService.Setup(s => s.GetAllEventsByUserId(It.IsAny<string>()))
                       .Returns((string id) => Task.FromResult<IEnumerable<BaseModel>>(
                           mockEvents.Where(r => r.OrginazerId == id)
                                     .Select(e => new BaseModel
                                     {
                                         Id = e.Id,
                                         Title = e.Title,
                                         Description = e.Description,
                                         Image = e.Images?.FirstOrDefault(),
                                     }).ToList()));

            // Act
            var result = await mockService.Object.GetAllEventsByUserId(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAllEventsByUserId_WithInvalidUserId_ReturnsEmptyList()
        {
            // Arrange
            var userId = "999";
            var mockService = new Mock<IEventsService>();
            mockService.Setup(s => s.GetAllEventsByUserId(It.IsAny<string>()))
                       .Returns((string id) => Task.FromResult<IEnumerable<BaseModel>>(
                           new List<BaseModel>()));

            // Act
            var result = await mockService.Object.GetAllEventsByUserId(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
