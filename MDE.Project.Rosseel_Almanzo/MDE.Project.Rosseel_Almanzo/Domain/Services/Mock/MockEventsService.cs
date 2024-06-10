using Firebase.Database;
using Firebase.Database.Query;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;

namespace MDE.Project.Rosseel_Almanzo.Domain.Services.Mock
{
    public class MockEventsService : IEventsService
    {
        private static List<Event> _events;

        public MockEventsService()
        {
            _events = new List<Event>
            {
                new Event
                {
                    Id = "-NvvRn23lsyHXXMXKcZw",
                    Title = "Ardennen Boswandeling",
                    Description = "Verken de prachtige bossen van de Ardennen.",
                    Street = "Ardennenstraat 123",
                    City = "La Roche-en-Ardenne",
                    Country = "België",
                    DateEvent = DateTime.Now.AddDays(7),
                    OrginazerId = "1",
                    Comments = new List<Comment>
                    {
                        new Comment{Content = "nice and verry good"},
                    },
                    Images = new List<Image>
                    {
                        new Image{ImagePath = "login.jpg"},
                    },
                },
                new Event
                {
                    Id="2",
                    Title = "Kustpad Ontdekkingstocht",
                    Description = "Wandel langs de schilderachtige kustpaden en geniet van de zeebries.",
                    Street = "Zeepromenade 456",
                    City = "Oostende",
                    Country = "België",
                    DateEvent = DateTime.Now.AddDays(14),
                },
                new Event
                {
                    Id ="3",
                    Title = "Hoge Venen Natuurwandeling",
                    Description = "Ontdek de unieke flora en fauna van de Hoge Venen.",
                    Street = "Venengebied 789",
                    City = "Eupen",
                    Country = "België",
                    DateEvent = DateTime.Now.AddDays(21),
                },
                new Event
                {
                    Id="4",
                    Title = "Groene Kempen Tocht",
                    Description = "Wandel door de groene landschappen van de Kempen-regio.",
                    Street = "Kempenweg 101",
                    City = "Turnhout",
                    Country = "België",
                    DateEvent = DateTime.Now.AddDays(28),
                },
            };
        }

        public async Task<string> CreateEventAsync(Event newEvent)
        {
            _events.Add(newEvent);
            return await Task.FromResult("Created");
        }

        public async Task<bool> AddCommentAsync(string id, Comment comment)
        {
            return await Task.FromResult(true);       
        }

        public async Task<IEnumerable<BaseModel>> GetAllEventsAsync()
        {
            //map data to event collection
            var events = _events.Select(e => new BaseModel
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                Image = e.Images?.FirstOrDefault(),
            }).ToList();

            return await Task.FromResult(events);
        }

        public async Task<IEnumerable<BaseModel>> GetAllEventsByUserId(string id)
        {
            //get data
            var events = _events.Where(e => e.OrginazerId == id).ToList();
            //map data to event collection
            var eventsList = events.Select(e => new BaseModel
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                Image = e.Images?.FirstOrDefault(),
            }).ToList();

            return await Task.FromResult(eventsList);
        }

        public async Task<Event> GetEventByIdAsync(string id)
        {
            return await Task.FromResult(_events.FirstOrDefault(e => e.Id == id));
        }

        public Task<string> DeleteEventAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCommentAsync(string id, string commentId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEventAsync(Event toUpdate)
        {
            throw new NotImplementedException();
        }

        public Task<List<BaseModel>> SearchByCity(string cityName)
        {
            throw new NotImplementedException();
        }
    }
}
