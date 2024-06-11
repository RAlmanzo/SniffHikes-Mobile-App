using Firebase.Database;
using Firebase.Database.Query;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Mock;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Validators;
using MDE.Project.Rosseel_Almanzo.Infrastructure.Services;
using MDE.Project.Rosseel_Almanzo.ViewModels;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MDE.Project.Rosseel_Almanzo.Tests
{
    public class EventServiceTests
    {
        [Fact]
        public async void CreateEventAsync_WithValidEvent_ReturnsCreated()
        {
            //Arrange
            var mockFirebaseClient = new Mock<FirebaseClient>("https://sniffhikes-8e9a6-default-rtdb.europe-west1.firebasedatabase.app/");
            var mockEvent = new Event { Title = "test" };

            var mockChildQuery = new Mock<ChildQuery>();
            mockChildQuery.Setup(cq => cq.PostAsync(It.IsAny<Event>(), It.IsAny<bool>()))
                          .ReturnsAsync((FirebaseObject<Event>)null);

            mockFirebaseClient.Setup(fc => fc.Child("Events")).Returns(mockChildQuery.Object);

            var eventService = new EventsService(mockFirebaseClient.Object);

            //Act
            var result = await eventService.CreateEventAsync(mockEvent);

            //Assert
            Assert.Equal("Created", result);
        }

        [Fact]
        public async Task CreateEventCommand_WithValidEvent_CallsEventsServiceAndNavigatesToEventsViewModel()
        {
            // Arrange
            var mockEventsService = new Mock<IEventsService>();
            mockEventsService.Setup(s => s.CreateEventAsync(It.IsAny<Event>())).ReturnsAsync("Created");

            var mockImageService = new Mock<IImageService>();
            var viewModel = new CreateEventViewModel(mockEventsService.Object, mockImageService.Object);

            var mockEvent = new Event() { Title = "test" };

            var mockEventValidator = new Mock<EventsValidator>();
            mockEventValidator.Setup(v => v.Validate(It.IsAny<Event>())).Returns(new FluentValidation.Results.ValidationResult());

            // Act
            await Task.Run(() => ((Command)viewModel.CreateEventCommand).Execute(null));

            // Assert
            mockEventsService.Verify(s => s.CreateEventAsync(It.IsAny<Event>()), Times.Once);
        }
    }
}
