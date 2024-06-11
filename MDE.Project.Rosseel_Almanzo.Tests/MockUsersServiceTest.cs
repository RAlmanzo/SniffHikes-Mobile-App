using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDE.Project.Rosseel_Almanzo.Tests
{
    public class MockUsersServiceTest
    {
        [Fact]
        public async Task CreateUserAsync_WithValidInputUser_ReturnsTrue()
        {
            // Arrange
            var mockUsers = new List<User>();
            var mockService = new Mock<IUsersService>();

            var newUser = new User
            {
                Id = "1",
                FirstName = "TestUser",
                Email = "test@example.com"
            };

            mockService.Setup(s => s.CreateUserAsync(It.IsAny<User>()))
                       .Callback<User>(user => mockUsers.Add(user))
                       .Returns(Task.FromResult(true));

            // Act
            var result = await mockService.Object.CreateUserAsync(newUser);

            // Assert
            Assert.Contains(newUser, mockUsers);
            Assert.True(result);          
        }

        [Fact]
        public async Task CreateUserAsync_WithException_ReturnsFalse()
        {
            // Arrange
            var mockUsers = new List<User>();
            var mockService = new Mock<IUsersService>();

            var newUser = new User
            {
                Id = "1",
                FirstName = "TestUser",
                Email = "test@example.com"
            };

            mockService.Setup(s => s.CreateUserAsync(It.IsAny<User>()))
                       .ThrowsAsync(new Exception("test exception"));

            // Act
            bool result = false;
            try
            {
                result = await mockService.Object.CreateUserAsync(newUser);
            }
            catch{}

            // Assert
            Assert.False(result);
            Assert.DoesNotContain(newUser, mockUsers);
        }

        [Fact]
        public async Task GetAllUsersAsync_WithUsers_ReturnsAllUsers()
        {
            // Arrange
            var mockUsers = new List<User>
            {
                new User { Id = "1", FirstName = "User 1", Email = "user1@example.com" },
                new User { Id = "2", FirstName = "User 2", Email = "user2@example.com" }
            };

            var mockService = new Mock<IUsersService>();
            mockService.Setup(s => s.GetAllUsersAsync()).ReturnsAsync(mockUsers);

            // Act
            var result = await mockService.Object.GetAllUsersAsync();

            // Assert
            Assert.Equal(mockUsers.Count, result.Count);
            Assert.Equal(mockUsers, result);
        }

        [Fact]
        public async Task GetAllUsersAsync_WithNoUsers_ReturnsEmptyList()
        {
            // Arrange
            var mockUsers = new List<User>();
            var mockService = new Mock<IUsersService>();
            mockService.Setup(s => s.GetAllUsersAsync()).ReturnsAsync(mockUsers);

            // Act
            var result = await mockService.Object.GetAllUsersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetUserByIdAsync_WithValidId_ReturnsUser()
        {
            // Arrange
            var userId = "1";
            var mockUsers = new List<User>
            {
                new User { Id = "1", FirstName = "User 1", Email = "user1@example.com" },
                new User { Id = "2", FirstName = "User 2", Email = "user2@example.com" }
            };
            var mockService = new Mock<IUsersService>();
            mockService.Setup(s => s.GetUserByIdAsync(It.IsAny<string>()))
                       .Returns((string id) => Task.FromResult(mockUsers.FirstOrDefault(u => u.Id == id)));

            // Act
            var result = await mockService.Object.GetUserByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.Id);
        }

        [Theory]
        [InlineData("999")]
        [InlineData(null)]
        [InlineData("")]
        public async Task GetUserByIdAsync_WithNullId_ReturnsNull(string userId)
        {
            // Arrange
            var mockUsers = new List<User>
            {
                new User { Id = "1", FirstName = "User 1", Email = "user1@example.com" },
                new User { Id = "2", FirstName = "User 2", Email = "user2@example.com" }
            };
            var mockService = new Mock<IUsersService>();
            mockService.Setup(s => s.GetUserByIdAsync(It.IsAny<string>()))
                       .Returns((string id) => Task.FromResult(mockUsers.FirstOrDefault(u => u.Id == id)));

            // Act
            var result = await mockService.Object.GetUserByIdAsync(userId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteUserAsync_WithValidId_ReturnsDeleted()
        {
            // Arrange
            var userId = "1";
            var mockUsers = new List<User>
            {
                new User { Id = "1", FirstName = "User 1", Email = "user1@example.com" },
                new User { Id = "2", FirstName = "User 2", Email = "user2@example.com" }
            };
            var mockService = new Mock<IUsersService>();
            mockService.Setup(s => s.GetUserByIdAsync(It.IsAny<string>()))
                       .Returns((string id) => Task.FromResult(mockUsers.FirstOrDefault(u => u.Id == id)));
            mockService.Setup(s => s.DeleteUserAsync(It.IsAny<string>()))
                       .Returns((string id) =>
                       {
                           var user = mockUsers.FirstOrDefault(u => u.Id == id);
                           if (user != null)
                           {
                               mockUsers.Remove(user);
                               return Task.FromResult("Deleted");
                           }
                           return Task.FromResult("Not Found");
                       });

            // Act
            var result = await mockService.Object.DeleteUserAsync(userId);

            // Assert
            Assert.Equal("Deleted", result);
            Assert.DoesNotContain(mockUsers, u => u.Id == userId);
        }

        [Fact]
        public async Task DeleteUserAsync_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var userId = "999";
            var mockUsers = new List<User>
            {
                new User { Id = "1", FirstName = "User 1", Email = "user1@example.com" },
                new User { Id = "2", FirstName = "User 2", Email = "user2@example.com" }
            };
            var mockService = new Mock<IUsersService>();
            mockService.Setup(s => s.GetUserByIdAsync(It.IsAny<string>()))
                       .Returns((string id) => Task.FromResult(mockUsers.FirstOrDefault(u => u.Id == id)));
            mockService.Setup(s => s.DeleteUserAsync(It.IsAny<string>()))
                       .Returns((string id) =>
                       {
                           var user = mockUsers.FirstOrDefault(u => u.Id == id);
                           if (user != null)
                           {
                               mockUsers.Remove(user);
                               return Task.FromResult("Deleted");
                           }
                           return Task.FromResult("Not Found");
                       });

            // Act
            var result = await mockService.Object.DeleteUserAsync(userId);

            // Assert
            Assert.Equal("Not Found", result);
            Assert.Equal(2, mockUsers.Count);
        }
    }
}
