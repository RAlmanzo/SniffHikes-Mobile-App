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
                    Title = "Ardennen Boswandeling",
                    Description = "Verken de prachtige bossen van de Ardennen.",
                    Street = "Ardennenstraat 123",
                    City = "La Roche-en-Ardenne",
                    Country = "België",
                    DateEvent = DateTime.Now.AddDays(7),
                },
                new Event
                {
                    Title = "Kustpad Ontdekkingstocht",
                    Description = "Wandel langs de schilderachtige kustpaden en geniet van de zeebries.",
                    Street = "Zeepromenade 456",
                    City = "Oostende",
                    Country = "België",
                    DateEvent = DateTime.Now.AddDays(14),
                },
                new Event
                {
                    Title = "Hoge Venen Natuurwandeling",
                    Description = "Ontdek de unieke flora en fauna van de Hoge Venen.",
                    Street = "Venengebied 789",
                    City = "Eupen",
                    Country = "België",
                    DateEvent = DateTime.Now.AddDays(21),
                },
                new Event
                {
                    Title = "Groene Kempen Tocht",
                    Description = "Wandel door de groene landschappen van de Kempen-regio.",
                    Street = "Kempenweg 101",
                    City = "Turnhout",
                    Country = "België",
                    DateEvent = DateTime.Now.AddDays(28),
                },
            };
        }

        public async Task<bool> CreateEventAsync(Event newEvent)
        {
            _events.Add(newEvent);
            return await Task.FromResult(true);
        }

        public async Task<bool> AddCommentAsync(string id, Comment comment)
        {
            return await Task.FromResult(true);       
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            return await Task.FromResult(_events);
        }


        public async Task<List<Event>> GetAllEventsByUserId(int id)
        {
            return await Task.FromResult(_events.Where(e => e.OrginazerId == id).ToList());
        }

        public async Task<Event> GetEventByIdAsync(string id)
        {
            //return await Task.FromResult(_events.FirstOrDefault(e => e.Id == id));
            return await Task.FromResult(_events.FirstOrDefault());
        }

        Task<List<BaseModel<Event>>> IEventsService.GetAllEventsAsync()
        {
            throw new NotImplementedException();
        }

        //public async Task<List<Image>> GetEventImagesAsync(int id)
        //{
        //    var selectedEvent = await GetEventByIdAsync(id);
        //    return (List<Image>)await Task.FromResult(selectedEvent.Images);
        //}
    }
}
