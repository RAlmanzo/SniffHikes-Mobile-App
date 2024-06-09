using Firebase.Database;
using Firebase.Database.Query;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using MDE.Project.Rosseel_Almanzo.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
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

        public Task<bool> AddCommentAsync(string id, Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCommentAsync(string id, string commentId)
        {
            throw new NotImplementedException();
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
    }
}
