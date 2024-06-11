using Firebase.Database;
using Firebase.Database.Query;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services;
using MDE.Project.Rosseel_Almanzo.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MDE.Project.Rosseel_Almanzo.Infrastructure.Services
{
    public class UsersService : IUsersService
    {
        private readonly FirebaseClient _client;

        public UsersService(FirebaseClient client)
        {
            _client = client;
        }

        public async Task<bool> AddDogAsync(string userId, Dog dog)
        {
            try
            {
                var user = await GetUserByIdAsync(userId);
                if (user.Dogs == null)
                {
                    user.Dogs = new ObservableCollection<Dog>();
                }
                user.Dogs.Add(dog);

                await _client.Child("Users").Child(userId).PutAsync(user);
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
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

        public async Task<string> DeleteUserAsync(string id)
        {
            try
            {
                //TODO delete images from db!!!!!!!!!!!!!!!!!!!!!!!

                await _client.Child("Users").Child(id).DeleteAsync();
                return await Task.FromResult("Deleted");
            }
            catch (Exception ex)
            {
                return await Task.FromResult(ex.Message);
            }
        }

        public Task<List<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            var userSnapshot = await _client.Child("Users").Child(id).OnceSingleAsync<UserDto>();

            if (userSnapshot != null)
            {
                var user = new User
                {
                    Id = id,
                    FirstName = userSnapshot.FirstName,
                    LastName = userSnapshot.LastName,
                    Email = userSnapshot.Email,
                    City = userSnapshot.City,
                    Country = userSnapshot.Country,
                    Gender = userSnapshot.Gender,
                    DateOfBirth = userSnapshot.DateOfBirth,
                    Password = userSnapshot.Password,
                    Dogs = userSnapshot.Dogs != null ? new ObservableCollection<Dog>(userSnapshot.Dogs) : new ObservableCollection<Dog>(),
                    Image = userSnapshot.Image,
                    IsAdmin = userSnapshot.IsAdmin,
                };
                
                return await Task.FromResult(user);
            };
            return null;
        }

        public async Task<bool> UpdateUserAsync(User toUpdate)
        {
            try
            {
                await _client.Child("Users").Child(toUpdate.Id)
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
