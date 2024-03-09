using MDE.Project.Rosseel_Almanzo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    Id = 1,
                    Title = "Ardennen Boswandeling",
                    Description = "Verken de prachtige bossen van de Ardennen.",
                    Street = "Ardennenstraat 123",
                    City = "La Roche-en-Ardenne",
                    Country = "België",
                    DateEvent = DateTime.Now.AddDays(7),
                },
                new Event
                {
                    Id = 2,
                    Title = "Kustpad Ontdekkingstocht",
                    Description = "Wandel langs de schilderachtige kustpaden en geniet van de zeebries.",
                    Street = "Zeepromenade 456",
                    City = "Oostende",
                    Country = "België",
                    DateEvent = DateTime.Now.AddDays(14),
                },
                new Event
                {
                    Id = 3,
                    Title = "Hoge Venen Natuurwandeling",
                    Description = "Ontdek de unieke flora en fauna van de Hoge Venen.",
                    Street = "Venengebied 789",
                    City = "Eupen",
                    Country = "België",
                    DateEvent = DateTime.Now.AddDays(21),
                },
                new Event
                {
                    Id = 4,
                    Title = "Groene Kempen Tocht",
                    Description = "Wandel door de groene landschappen van de Kempen-regio.",
                    Street = "Kempenweg 101",
                    City = "Turnhout",
                    Country = "België",
                    DateEvent = DateTime.Now.AddDays(28),
                },
            };
        }

        public async Task<List<Event>> GetAllEvents()
        {
            return await Task.FromResult(_events);
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await Task.FromResult(_events.FirstOrDefault(e => e.Id == id));
        }
    }
}
