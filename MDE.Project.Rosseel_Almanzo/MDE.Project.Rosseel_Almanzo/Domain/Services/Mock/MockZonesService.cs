using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDE.Project.Rosseel_Almanzo.Domain.Services.Mock
{
    public class MockZonesService : IZonesService
    {
        public List<Zone> _zones;

        public MockZonesService()
        {
            _zones = new List<Zone>
            {
                new Zone
                {
                    Id = "1",
                    Title = "Strandzone Oostende",
                    Description = "Een ruime zone op het strand van Oostende waar honden vrij kunnen rondlopen en spelen.",
                    Street = "Zeedijk",
                    City = "Oostende",
                    Country = "België",
                    Images = new List<Image>
                    {
                        new Image{Title="first image", ImagePath ="login.jpg"},
                        new Image{Title="Second image", ImagePath="https://source.unsplash.com/user/c_v_r/1900x800"},
                        new Image{Title="first image", ImagePath ="loooogin.jpg"},
                        new Image{Title="first image", ImagePath ="bluesky.jpg"},
                        new Image{Title="Second image", ImagePath="https://source.unsplash.com/user/c_v_r/1900x800"}
                    },
                    Comments = new List<Comment>
                    {
                        new Comment { Content = "Een geweldige plek voor honden om te ravotten!Een geweldige plek voor honden om te ravotten!Een geweldige plek voor honden om te ravotten!" },
                        new Comment { Content = "Mijn hond houdt van deze losloopzone! Een geweldige plek voor honden om te ravotten!" }
                    }
                },
                new Zone
                {
                    Id = "2",
                    Title = "Hondenweide Park Spoor Noord",
                    Description = "Een omheinde hondenweide in Park Spoor Noord waar honden vrij kunnen rennen en spelen.",
                    Street = "Noorderlaan",
                    City = "Antwerpen",
                    Country = "België",
                    Images = new List<Image>
                    {
                        new Image{Title="first image", ImagePath ="login.jpg"},
                        new Image{Title="first image", ImagePath ="loooogin.jpg"},
                        new Image{Title="first image", ImagePath ="bluesky.jpg"},
                        new Image{Title="Second image", ImagePath="https://source.unsplash.com/user/c_v_r/1900x800"}
                    },
                    Comments = new List<Comment>
                    {
                        new Comment { Content = "Leuke plek voor honden en hun baasjes!" },
                        new Comment { Content = "Ik kom hier graag met mijn hond. Veel ruimte om te rennen!" }
                    }
                },
                new Zone
                {
                    Id = "3",
                    Title = "Losloopzone Bois de la Cambre",
                    Description = "Een afgebakende losloopzone in het Bois de la Cambre park waar honden vrij kunnen rondrennen en spelen.",
                    Street = "Chaussée de la Hulpe",
                    City = "Brussel",
                    Country = "België",
                    Images = new List<Image>
                    {
                        new Image{Title="first image", ImagePath ="login.jpg"},
                        new Image{Title="first image", ImagePath ="loooogin.jpg"},
                        new Image{Title="first image", ImagePath ="bluesky.jpg"},
                        new Image{Title="Second image", ImagePath="https://source.unsplash.com/user/c_v_r/1900x800"}
                    },
                    Comments = new List<Comment>
                    {
                        new Comment { Content = "Prachtige plek voor honden om te genieten van de natuur!" },
                        new Comment { Content = "Mijn hond en ik komen hier graag wandelen. Geweldige sfeer!" }
                    }
                },
                new Zone
                {
                    Id = "4",
                    Title = "Losloopzone Rivierenhof",
                    Description = "Een groot gebied in het Rivierenhof park waar honden los kunnen lopen en samen kunnen spelen.",
                    Street = "Turnhoutsebaan",
                    City = "Deurne",
                    Country = "België",
                    Images = new List<Image>
                    {
                        new Image{Title="first image", ImagePath ="login.jpg"},
                        new Image{Title="first image", ImagePath ="loooogin.jpg"},
                        new Image{Title="first image", ImagePath ="bluesky.jpg"},
                        new Image{Title="Second image", ImagePath="https://source.unsplash.com/user/c_v_r/1900x800"}
                    },
                    Comments = new List<Comment>
                    {
                        new Comment { Content = "Fantastische locatie voor honden en hun eigenaren!" },
                        new Comment { Content = "Mijn hond is dol op deze losloopzone. Veel ruimte om te verkennen!" }
                    }
                },
                new Zone
                {
                    Id = "5",
                    Title = "Hondenzone Kapermolenpark",
                    Description = "Een speciale hondenzone in het Kapermolenpark waar honden vrij kunnen spelen en sociaal kunnen zijn met andere honden.",
                    Street = "Elfde-Liniestraat",
                    City = "Hasselt",
                    Country = "België",
                    Images = new List<Image>
                    {
                        new Image{Title="first image", ImagePath ="login.jpg"},
                        new Image{Title="first image", ImagePath ="loooogin.jpg"},
                        new Image{Title="first image", ImagePath ="bluesky.jpg"},
                        new Image{Title="Second image", ImagePath="https://source.unsplash.com/user/c_v_r/1900x800"}
                    },
                    Comments = new List<Comment>
                    {
                        new Comment { Content = "Een geweldige toevoeging aan het park voor hondenbezitters!" },
                        new Comment { Content = "Ik ben blij dat mijn hond hier vrij kan rondrennen en spelen. Bedankt!" }
                    }
                },
                new Zone
                {
                    Id = "6",
                    Title = "Losloopweide Brielmeersen",
                    Description = "Een omheinde losloopweide in het recreatiepark De Brielmeersen waar honden veilig kunnen ravotten en plezier maken.",
                    Street = "Lucien Matthyslaan",
                    City = "Deinze",
                    Country = "België",
                    Images = new List<Image>
                    {
                        new Image{Title="first image", ImagePath ="login.jpg"},
                        new Image{Title="first image", ImagePath ="loooogin.jpg"},
                        new Image{Title="first image", ImagePath ="bluesky.jpg"},
                        new Image{Title="Second image", ImagePath="https://source.unsplash.com/user/c_v_r/1900x800"}
                    },
                    Comments = new List<Comment>
                    {
                        new Comment { Content = "Perfecte locatie voor honden om vrij te spelen en te rennen!" },
                        new Comment { Content = "Mijn hond houdt van deze losloopweide. Goede sfeer en vriendelijke mensen!" }
                    }
                },
                new Zone
                {
                    Id = "7",
                    Title = "Losloopzone Ter Motten",
                    Description = "Een aangename losloopzone in het groene Ter Motten park waar honden zich kunnen uitleven en nieuwe vrienden kunnen maken.",
                    Street = "Beernemstraat",
                    City = "Torhout",
                    Country = "België",
                    Images = new List<Image>
                    {
                        new Image{Title="first image", ImagePath ="login.jpg"},
                        new Image{Title="first image", ImagePath ="loooogin.jpg"},
                        new Image{Title="first image", ImagePath ="bluesky.jpg"},
                        new Image{Title="Second image", ImagePath="https://source.unsplash.com/user/c_v_r/1900x800"}
                    },
                    Comments = new List<Comment>
                    {
                        new Comment { Content = "Mooie plek om te wandelen met mijn hond. Geweldige sfeer en veel ruimte!" },
                        new Comment { Content = "Ik kom hier graag met mijn hond. Leuke ontmoetingen en veel speelplezier!" }
                    }
                },
            };
        }

        public Task<bool> AddCommentAsync(string id, Comment comment)
        {
            throw new NotImplementedException();
        }

        public async Task<string> CreateZoneAsync(Zone newZone)
        {
            _zones.Add(newZone);
            return await Task.FromResult("Created");
        }

        public Task<bool> DeleteCommentAsync(string id, string commentId)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteZoneAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BaseModel>> GetAllZonesAsync()
        {
            var zones = _zones.Select(e => new BaseModel
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                Image = e.Images?.FirstOrDefault(),
            }).ToList();

            return await Task.FromResult(zones);
        }

        public async Task<Zone> GetZoneByIdAsync(string id)
        {
            return await Task.FromResult(_zones.FirstOrDefault(z => z.Id == id));
        }

        public Task<List<BaseModel>> SearchByCity(string cityName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateZoneAsync(Zone toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
