using Firebase.Database;
using Firebase.Database.Query;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDE.Project.Rosseel_Almanzo.Infrastructure.Services
{
    public class UsersService : IUsersService
    {
        private readonly FirebaseClient _client;

        public UsersService()
        {
            _client = new FirebaseClient("https://sniffhikes-8e9a6-default-rtdb.europe-west1.firebasedatabase.app/");
        }

        public async Task<bool> CreateUserAsync(User newUser)
        {
            try
            {
                newUser.Password = null;
                await _client.Child("Users").Child($"{newUser.Id}")
                    .PutAsync(newUser);
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                var result = ex.Message;
                return await Task.FromResult(false);
            }
        }

        public Task<List<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
