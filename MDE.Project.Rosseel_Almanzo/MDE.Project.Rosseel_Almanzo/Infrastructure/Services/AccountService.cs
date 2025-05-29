using Firebase.Auth;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using User = MDE.Project.Rosseel_Almanzo.Domain.Models.User;

namespace MDE.Project.Rosseel_Almanzo.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly FirebaseAuthProvider _authProvider;
        private readonly IUsersService _usersService;

        public AccountService(IUsersService usersService, FirebaseAuthProvider authProvider)
        {
            _usersService = usersService;
            _authProvider = authProvider;
        }

        public async Task<string> Login(string username, string password)
        {
            try
            {
                var token = await _authProvider.SignInWithEmailAndPasswordAsync(username, password);
                var userIdToken = await SecureStorage.GetAsync("token");
                var accountToken = await SecureStorage.GetAsync("account");

                if (!string.IsNullOrWhiteSpace(userIdToken) || !string.IsNullOrWhiteSpace(accountToken))
                {
                    SecureStorage.RemoveAll();
                }

                await SecureStorage.SetAsync("token", token.User.LocalId);
                await SecureStorage.SetAsync("account", token.FirebaseToken);

                var user = await _usersService.GetUserByIdAsync(token.User.LocalId);
                await SecureStorage.SetAsync("admin", user.IsAdmin.ToString());
                await SecureStorage.SetAsync("name", user.FirstName + " " + user.LastName);

                return await Task.FromResult("Email confirmed");
            }
            catch (Exception ex)
            {
                return await Task.FromResult(ex.Message);
            }
        }

        public async Task<bool> Register(User newUser)
        {
            try
            {
                var token = await _authProvider.CreateUserWithEmailAndPasswordAsync(newUser.Email, newUser.Password);
                newUser.Id = token.User.LocalId;
                newUser.IsAdmin = false;
                await _usersService.CreateUserAsync(newUser);
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> DeleteAccountAsync(string id)
        {
            try
            {
                var token = await SecureStorage.GetAsync("account");
                await _authProvider.DeleteUserAsync(token);
                SecureStorage.RemoveAll();

                if (await _usersService.DeleteUserAsync(id) != "Deleted")
                {
                    return await Task.FromResult(false);
                }
                ;
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }
    }
}
