using Firebase.Auth;
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
    public class AccountServiceTests
    {
        private readonly Mock<FirebaseAuthProvider> _authProviderMock;
        private readonly Mock<IUsersService> _usersServiceMock;
        private readonly Mock<IAccountService> _accountServiceMock;

        public AccountServiceTests()
        {
            _authProviderMock = new Mock<FirebaseAuthProvider>();
            _usersServiceMock = new Mock<IUsersService>();
            _accountServiceMock = new Mock<IAccountService>();
        }

        [Fact]
        public async Task Login_Success_ReturnsEmailConfirmed()
        {
            // Arrange
            var mockUser = new Domain.Models.User
            {
                Email = "test@test.com",
                Password = "password"
            };

            var mockToken = new Mock<FirebaseAuthLink>();
            mockToken.Setup(t => t.User.LocalId).Returns(It.IsAny<string>());
            mockToken.Setup(t => t.FirebaseToken).Returns(It.IsAny<string>());

            _authProviderMock.Setup(ap => ap.SignInWithEmailAndPasswordAsync(It.IsAny<string>(), It.IsAny<string>(), null))
                         .ReturnsAsync(mockToken.Object);


            _usersServiceMock.Setup(us => us.GetUserByIdAsync(It.IsAny<string>()))
                             .ReturnsAsync(mockUser);

            // Act
            var result = await _accountServiceMock.Object.Login("test@example.com", "password");

            // Assert
            Assert.Equal("Email confirmed", result);
        }
    }
}
