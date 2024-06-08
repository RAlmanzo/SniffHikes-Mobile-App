using Firebase.Database;
using Firebase.Database.Query;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
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

        public Task<string> DeleteZoneAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Zone>> GetAllZonesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Zone> GetZoneByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateZoneAsync(Zone toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
