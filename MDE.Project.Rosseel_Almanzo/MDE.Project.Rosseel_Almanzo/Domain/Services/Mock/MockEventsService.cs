using MDE.Project.Rosseel_Almanzo.Domain.Models;
using System;
using System.Collections.Generic;
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
                    Id = 1,
                    OrginazerId = 1,
                    Title = "Ardennen Boswandeling",
                    Description = "Verken de prachtige bossen van de Ik vond het honden event  bossen van de Ik vond het honden event  bossen van de Ik vond het honden event echt geweldig! Mijn hond en ik hebbe Ik vond het honden event echt geweldig! Mijn hond en ik hebbe Ik vond het honden event echt geweldig! Mijn hond en ik hebbe Ardennen.Verken de prachtige bossen van de Ardennen.Verken de prachtige bossen van de Ardennen.Verken de prachtige bossen van de Ardennen." ,
                    Street = "Ardennenstraat 123",
                    City = "La Roche-en-Ardenne",
                    Country = "België",
                    DateEvent = DateTime.Now.AddDays(7),
                    Images = new List<Image>
                    {
                        new Image{Title="first image", ImagePath ="login.jpg"},
                        new Image{Title="first image", ImagePath ="loooogin.jpg"},
                        new Image{Title="first image", ImagePath ="dog.jpg"},
                        new Image{Title="Second image", ImagePath="https://source.unsplash.com/user/c_v_r/1900x800"}
                    },
                    Comments = new List<Comment>
                    {
                        new Comment { Content = "Ik vond het honden event echt geweldig! Mijn hond en ik hebben genoten van de leuke activiteiten en de gezellige sfeer. Hopelijk volgend jaar weer!" },
                        new Comment { Content = "Het honden event was zo leuk! Mijn harige vriend heeft nieuwe vrienden gemaakt en we hebben samen veel plezier gehad. Bedankt aan iedereen die het heeft georganiseerd!" },
                        new Comment { Content = "Dit honden event was precies wat mijn viervoeter nodig had. Hij heeft zoveel plezier gehad en we hebben allebei genoten van de dag vol activiteiten en leuke ontmoetingen!" },
                        new Comment { Content = "Het honden event was een geweldige ervaring! Mijn hond en ik hebben genoten van de gezelligheid en de leuke dingen om te doen. Een fantastische dag voor ons beiden!" },
                        new Comment { Content = "Wat een fantastisch honden event! Ik heb zoveel leuke mensen ontmoet en mijn hond heeft zich geen moment verveeld. Bedankt aan iedereen die heeft geholpen om dit mogelijk te maken!" },
                    }
                },
                new Event
                {
                    Id = 2,
                    OrginazerId = 1,
                    Title = "Kustpad Ontdekkingstocht",
                    Description = "Wandel langs de schilderachtige kustpaden en geniet van de zeebries.",
                    Street = "Zeepromenade 456",
                    City = "Oostende",
                    Country = "België",
                    DateEvent = DateTime.Now.AddDays(14),
                    Images = new List<Image>
                    {
                        new Image{Title="first image", ImagePath ="login.jpg"},
                        new Image{Title="first image", ImagePath ="loooogin.jpg"},
                        new Image{Title="first image", ImagePath ="dog.jpg"},
                    },
                    Comments = new List<Comment>
                    {
                        new Comment { Content = "Ik vond het honden event echt geweldig! Mijn hond en ik hebben genoten van de leuke activiteiten en de gezellige sfeer. Hopelijk volgend jaar weer!" },
                        new Comment { Content = "Het honden event was zo leuk! Mijn harige vriend heeft nieuwe vrienden gemaakt en we hebben samen veel plezier gehad. Bedankt aan iedereen die het heeft georganiseerd!" },
                        new Comment { Content = "Dit honden event was precies wat mijn viervoeter nodig had. Hij heeft zoveel plezier gehad en we hebben allebei genoten van de dag vol activiteiten en leuke ontmoetingen!" },
                        new Comment { Content = "Het honden event was een geweldige ervaring! Mijn hond en ik hebben genoten van de gezelligheid en de leuke dingen om te doen. Een fantastische dag voor ons beiden!" },
                        new Comment { Content = "Wat een fantastisch honden event! Ik heb zoveel leuke mensen ontmoet en mijn hond heeft zich geen moment verveeld. Bedankt aan iedereen die heeft geholpen om dit mogelijk te maken!" },
                    }
                },
                new Event
                {
                    Id = 3,
                    OrginazerId = 2,
                    Title = "Hoge Venen Natuurwandeling",
                    Description = "Ontdek de unieke flora en fauna van de Hoge Venen.",
                    Street = "Venengebied 789",
                    City = "Eupen",
                    Country = "België",
                    DateEvent = DateTime.Now.AddDays(21),
                    Images = new List<Image>
                    {
                        new Image{Title="first image", ImagePath ="login.jpg"},
                        new Image{Title="first image", ImagePath ="loooogin.jpg"},
                        new Image{Title="first image", ImagePath ="dog.jpg"},
                    },
                    Comments = new List<Comment>
                    {
                        new Comment { Content = "Ik vond het honden event echt geweldig! Mijn hond en ik hebben genoten van de leuke activiteiten en de gezellige sfeer. Hopelijk volgend jaar weer!" },
                        new Comment { Content = "Het honden event was zo leuk! Mijn harige vriend heeft nieuwe vrienden gemaakt en we hebben samen veel plezier gehad. Bedankt aan iedereen die het heeft georganiseerd!" },
                        new Comment { Content = "Dit honden event was precies wat mijn viervoeter nodig had. Hij heeft zoveel plezier gehad en we hebben allebei genoten van de dag vol activiteiten en leuke ontmoetingen!" },
                        new Comment { Content = "Het honden event was een geweldige ervaring! Mijn hond en ik hebben genoten van de gezelligheid en de leuke dingen om te doen. Een fantastische dag voor ons beiden!" },
                        new Comment { Content = "Wat een fantastisch honden event! Ik heb zoveel leuke mensen ontmoet en mijn hond heeft zich geen moment verveeld. Bedankt aan iedereen die heeft geholpen om dit mogelijk te maken!" },
                    }
                },
                new Event
                {
                    Id = 4,
                    OrginazerId = 3,
                    Title = "Groene Kempen Tocht",
                    Description = "Wandel door de groene landschappen van de Kempen-regio.",
                    Street = "Kempenweg 101",
                    City = "Turnhout",
                    Country = "België",
                    DateEvent = DateTime.Now.AddDays(28),
                    Images = new List<Image>
                    {
                        new Image{Title="first image", ImagePath ="login.jpg"},
                        new Image{Title="first image", ImagePath ="loooogin.jpg"},
                        new Image{Title="first image", ImagePath ="dog.jpg"},
                    },
                    Comments = new List<Comment>
                    {
                        new Comment { Content = "Ik vond het honden event echt geweldig! Mijn hond en ik hebben genoten van de leuke activiteiten en de gezellige sfeer. Hopelijk volgend jaar weer!" },
                        new Comment { Content = "Het honden event was zo leuk! Mijn harige vriend heeft nieuwe vrienden gemaakt en we hebben samen veel plezier gehad. Bedankt aan iedereen die het heeft georganiseerd!" },
                        new Comment { Content = "Dit honden event was precies wat mijn viervoeter nodig had. Hij heeft zoveel plezier gehad en we hebben allebei genoten van de dag vol activiteiten en leuke ontmoetingen!" },
                        new Comment { Content = "Het honden event was een geweldige ervaring! Mijn hond en ik hebben genoten van de gezelligheid en de leuke dingen om te doen. Een fantastische dag voor ons beiden!" },
                        new Comment { Content = "Wat een fantastisch honden event! Ik heb zoveel leuke mensen ontmoet en mijn hond heeft zich geen moment verveeld. Bedankt aan iedereen die heeft geholpen om dit mogelijk te maken!" },
                    }
                },
            };
        }

        public Task<bool> CreateEventAsync(Event newEvent)
        {
            try
            {
                _events.Add(newEvent);
                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            return await Task.FromResult(_events);
        }

        public async Task<List<Event>> GetAllEventsByUserId(int id)
        {
            return await Task.FromResult(_events.Where(e => e.OrginazerId == id).ToList());
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await Task.FromResult(_events.FirstOrDefault(e => e.Id == id));
        }

        //public async Task<List<Image>> GetEventImagesAsync(int id)
        //{
        //    var selectedEvent = await GetEventByIdAsync(id);
        //    return (List<Image>)await Task.FromResult(selectedEvent.Images);
        //}
    }
}
