using MDE.Project.Rosseel_Almanzo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDE.Project.Rosseel_Almanzo.Domain.Services.Mock
{
    public class MockRoutesService : IRoutesService
    {
        public static List<Route> _routes;
        public MockRoutesService()
        {
            _routes = new List<Route>
            {
                new Route
                {
                    Id = 1,
                    Title = "Ardennen Avontuur",
                    Description = "Verken de prachtige natuur van de Ardennen tijdens deze avontuurlijke wandelroute.Verken de prachtige natuur van de Ardennen tijdens deze avontuurlijke wandelroute.Verken de prachtige natuur van de Ardennen tijdens deze avontuurlijke wandelroute.Verken de prachtige natuur van de Ardennen tijdens deze avontuurlijke wandelroute.",
                    Street = "Ardennenweg 1",
                    City = "La Roche-en-Ardenne",
                    Country = "België",
                    DateEvent = new DateTime(2024, 4, 1),
                    OrganizerId = 2,
                    Images = new List<Image>
                    {
                        new Image{Title="first image", ImagePath ="login.jpg"},
                        new Image{Title="first image", ImagePath ="loooogin.jpg"},
                        new Image{Title="first image", ImagePath ="bluesky.jpg"},
                        new Image{Title="Second image", ImagePath="https://source.unsplash.com/user/c_v_r/1900x800"}
                    },
                    Comments = new List<Comment>
                    {
                        new Comment { Content = "Mijn hond en ik hebben genoten van de leuke activiteitenIk vond het honden event echt geweldig! Mijn hond en ik hebben genoten van de leuke activiteiten en de gezellige sfeer. Hopelijk volgend jaar weer!\"Ik vond het honden event echt geweldig! Mijn hond en ik hebben genoten van de leuke activiteiten en de gezellige sfeer. Hopelijk volgend jaar weer!" },
                        new Comment { Content = "Het honden event was zo leuk! Mijn harige vriend heeft nieuwe vrienden gemaakt en we hebben samen veel plezier gehad. Bedankt aan iedereen die het heeft georganiseerd!" },
                        new Comment { Content = "Dit honden event was precies wat mijn viervoeter nodig had. Hij heeft zoveel plezier gehad en we hebben allebei genoten van de dag vol activiteiten en leuke ontmoetingen!" },
                        new Comment { Content = "Het honden event was een geweldige ervaring! Mijn hond en ik hebben genoten van de gezelligheid en de leuke dingen om te doen. Een fantastische dag voor ons beiden!" },
                        new Comment { Content = "Wat een fantastisch honden event! Ik heb zoveel leuke mensen ontmoet en mijn hond heeft zich geen moment verveeld. Bedankt aan iedereen die heeft geholpen om dit mogelijk te maken!" },
                    }
                },
                new Route
                {
                    Id = 2,
                    Title = "Brugse Binnenstad",
                    Description = "Ontdek de historische binnenstad van Brugge tijdens deze sfeervolle wandeling langs grachten en middeleeuwse gebouwen.",
                    Street = "Markt 1",
                    City = "Brugge",
                    Country = "België",
                    DateEvent = new DateTime(2024, 4, 5),
                    OrganizerId = 3,
                    Images = new List<Image>
                    {
                        new Image{Title="first image", ImagePath ="login.jpg"},
                        new Image{Title="first image", ImagePath ="loooogin.jpg"},
                        new Image{Title="first image", ImagePath ="bluesky.jpg"},
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
                new Route
                {
                    Id = 3,
                    Title = "Ardennen Uitzicht",
                    Description = "Geniet van adembenemende uitzichten tijdens deze panoramische wandelroute door de Ardennen.",
                    Street = "Uitzichtweg 5",
                    City = "Durbuy",
                    Country = "België",
                    DateEvent = new DateTime(2024, 4, 10),
                    OrganizerId = 4,
                    Images = new List<Image>
                    {
                        new Image{Title="first image", ImagePath ="login.jpg"},
                        new Image{Title="first image", ImagePath ="loooogin.jpg"},
                        new Image{Title="first image", ImagePath ="bluesky.jpg"},
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
                new Route
                {
                    Id = 4,
                    Title = "Kustwandeling Oostende",
                    Description = "Maak een ontspannende wandeling langs de kustlijn van Oostende en geniet van de frisse zeelucht.",
                    Street = "Zeedijk 1",
                    City = "Oostende",
                    Country = "België",
                    DateEvent = new DateTime(2024, 4, 15),
                    OrganizerId = 5,
                    Images = new List<Image>
                    {
                        new Image{Title="first image", ImagePath ="login.jpg"},
                        new Image{Title="first image", ImagePath ="loooogin.jpg"},
                        new Image{Title="first image", ImagePath ="bluesky.jpg"},
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
                new Route
                {
                    Id = 5,
                    Title = "Maasvallei Verkenning",
                    Description = "Verken de prachtige natuur langs de Maasvallei tijdens deze gevarieerde wandelroute.",
                    Street = "Maasdijk 10",
                    City = "Dinant",
                    Country = "België",
                    DateEvent = new DateTime(2024, 4, 20),
                    OrganizerId = 5,
                    Images = new List<Image>
                    {
                        new Image{Title="first image", ImagePath ="login.jpg"},
                        new Image{Title="first image", ImagePath ="loooogin.jpg"},
                        new Image{Title="first image", ImagePath ="bluesky.jpg"},
                        new Image{Title="Second image", ImagePath="https://source.unsplash.com/user/c_v_r/1900x800"}
                    },
                    Comments = new List<Comment>
                    {
                        new Comment { Content = "Ik vond het honden event echt geweldig! Mijn hond en ik hebben genoten van de leuke activiteiten en de gezellige sfeer. Hopelijk volgend jaar weer!" },
                        new Comment { Content = "Het honden event was zo leuk! Mijn harige vriend heeft nieuwe vrienden gemaakt en we hebben samen veel plezier gehad. Bedankt aan iedereen die het heeft georganiseerd!" },
                        new Comment { Content = "Dit honden event was precies wat mijn viervoeter nodig had. Hij heeft zoveel plezierDit honden event was precies wat mijn viervoeter nodig had. Hij heeft zoveel plezierDit honden event was precies wat mijn viervoeter nodig had. Hij heeft zoveel plezier gehad en we hebben allebei genoten van de dag vol activiteiten en leuke ontmoetingen!" },
                        new Comment { Content = "Het honden event was een geweldige ervaring! Mijn hond en ik hebben genoten van de gezelligheid en de leuke dingen om te doen. Een fantastische dag voor ons beiden!" },
                        new Comment { Content = "Wat een fantastisch honden event! Ik heb zoveel leuke mensen ontmoet en mijn hond heeft zich geen moment verveeld. Bedankt aan iedereen die heeft geholpen om dit mogelijk te maken!" },
                    }
                }
            };
        }

        public Task<bool> CreateRouteAsync(Route newRoute)
        {
            try
            {
                _routes.Add(newRoute);
                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        public async Task<List<Route>> GetAllRoutesAsync()
        {
            return await Task.FromResult(_routes);
        }

        public async Task<List<Route>> GetAllRoutesByUserId(int id)
        {
            return await Task.FromResult(_routes.Where(r => r.OrganizerId == id).ToList());
        }

        public async Task<Route> GetRouteByIdAsync(int id)
        {
            return await Task.FromResult(_routes.FirstOrDefault(r => r.Id == id));
        }
    }
}
