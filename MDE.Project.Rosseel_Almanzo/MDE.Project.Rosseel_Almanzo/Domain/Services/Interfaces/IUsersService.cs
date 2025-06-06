﻿using MDE.Project.Rosseel_Almanzo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDE.Project.Rosseel_Almanzo.Domain.Services
{
    public interface IUsersService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<bool> CreateUserAsync(User newUser);
        Task<bool> UpdateUserAsync(User toUpdate);
        Task<string> DeleteUserAsync(string id);
        Task<User> GetUserByIdAsync(string id);
        Task<bool> AddDogAsync(string userId, Dog dog);
    }
}
