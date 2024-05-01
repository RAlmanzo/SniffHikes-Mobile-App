using Firebase.Database;
using Firebase.Database.Query;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MDE.Project.Rosseel_Almanzo.Domain.Services
{
    public class RoutesService : IRoutesService
    {
        private readonly FirebaseClient _client;

        public RoutesService()
        {
            _client = new FirebaseClient("https://sniffhikes-8e9a6-default-rtdb.europe-west1.firebasedatabase.app/");
        }

        public async Task<string> CreateRouteAsync(Route newRoute)
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                newRoute.OrganizerId = token;
                await _client.Child("Routes")
                    .PostAsync(newRoute);
                return await Task.FromResult("Created");
            }
            catch (Exception ex)
            {
                return await Task.FromResult(ex.Message);
            }
        }

        public Task<List<Route>> GetAllRoutesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Route>> GetAllRoutesByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Route> GetRouteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<BaseModel>> IRoutesService.GetAllRoutesAsync()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<BaseModel>> IRoutesService.GetAllRoutesByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddCommentAsync(string id, Comment comment)
        {
            throw new NotImplementedException();
        }
    }
}
