using Firebase.Database;
using Firebase.Database.Query;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using MDE.Project.Rosseel_Almanzo.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MDE.Project.Rosseel_Almanzo.Infrastructure.Services
{
    public class ZoneService : IZonesService
    {
        private readonly FirebaseClient _client;

        public ZoneService()
        {
            _client = new FirebaseClient("https://sniffhikes-8e9a6-default-rtdb.europe-west1.firebasedatabase.app/");
        }

        public async Task<string> CreateZoneAsync(Zone newZone)
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                newZone.OrganizerId = token;
                await _client.Child("Zones")
                    .PostAsync(newZone);
                return await Task.FromResult("Created");
            }
            catch (Exception ex)
            {
                return await Task.FromResult(ex.Message);
            }
        }

        public async Task<bool> AddCommentAsync(string id, Comment comment)
        {
            try
            {
                var zone = await GetZoneByIdAsync(id);
                if (zone.Comments == null)
                {
                    zone.Comments = new ObservableCollection<Comment>();
                }
                var token = await SecureStorage.GetAsync("token");
                comment.UserId = token;
                var name = await SecureStorage.GetAsync("name");
                comment.UserName = name;

                zone.Comments.Add(comment);

                await _client.Child("Zones").Child(id).PutAsync(zone);
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> DeleteCommentAsync(string id, string commentId)
        {
            try
            {
                //get the route
                var selectedEvent = await GetZoneByIdAsync(id);
                //get the comment
                var selectedComment = selectedEvent.Comments.Where(c => c.Id == commentId).FirstOrDefault();
                //delete comment and update db
                selectedEvent.Comments.Remove(selectedComment);

                await _client.Child("Zones").Child(id).PutAsync(selectedEvent);
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<string> DeleteZoneAsync(string id)
        {
            try
            {
                //TODO delete images from db!!!!!!!!!!!!!!!!!!!!!!!

                await _client.Child("Zones").Child(id).DeleteAsync();
                return await Task.FromResult("Deleted");
            }
            catch (Exception ex)
            {
                return await Task.FromResult(ex.Message);
            }
        }

        public async Task<List<BaseModel>> GetAllZonesAsync()
        {
            //get de data
            var zonesSnapshot = await _client.Child("Zones").OnceAsync<ZoneDto>();

            //map data to zones collection
            var zones = zonesSnapshot.Select(e => new BaseModel
            {
                Id = e.Key,
                Title = e.Object.Title,
                Description = e.Object.Description,
                Image = e.Object.Images?.FirstOrDefault(),
                OrginazerId = e.Object.OrganizerId,
            }).ToList();

            return await Task.FromResult(zones);
        }

        public async Task<Zone> GetZoneByIdAsync(string id)
        {
            var zoneSnapshot = await _client.Child("Zones").Child(id).OnceSingleAsync<ZoneDto>();

            if (zoneSnapshot != null)
            {
                var selectedZone = new Zone
                {
                    Id = zoneSnapshot.Id,
                    Title = zoneSnapshot.Title,
                    Description = zoneSnapshot.Description,
                    Street = zoneSnapshot.Street,
                    City = zoneSnapshot.City,
                    Country = zoneSnapshot.Country,
                    OrganizerId = zoneSnapshot.OrganizerId,
                    Images = zoneSnapshot.Images,
                    Comments = zoneSnapshot.Comments,
                };
                return await Task.FromResult(selectedZone);
            };
            return null;
        }

        public async Task<bool> UpdateZoneAsync(Zone toUpdate)
        {
            try
            {
                await _client.Child("Zones").Child(toUpdate.Id)
                    .PutAsync(toUpdate);
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<List<BaseModel>> SearchByCity(string cityName)
        {
            //get de data
            var zonesSnapshot = await _client.Child("Zones").OnceAsync<ZoneDto>();
            //sort data
            var cityZones = zonesSnapshot.Where(z => z.Object.City.ToLower().Contains(cityName.ToLower()));

            //map data to zones collection
            var zones = cityZones.Select(e => new BaseModel
            {
                Id = e.Key,
                Title = e.Object.Title,
                Description = e.Object.Description,
                Image = e.Object.Images?.FirstOrDefault(),
                OrginazerId = e.Object.OrganizerId,
            }).ToList();

            return await Task.FromResult(zones);
        }
    }
}
