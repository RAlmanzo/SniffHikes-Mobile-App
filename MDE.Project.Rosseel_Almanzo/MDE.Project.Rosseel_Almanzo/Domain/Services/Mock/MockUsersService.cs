using MDE.Project.Rosseel_Almanzo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MDE.Project.Rosseel_Almanzo.Domain.Services.Mock
{
    public class MockUsersService : IUsersService
    {
        public List<User> _users;

        public MockUsersService()
        {
            _users = new List<User>
        {
            new User
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                City = "New York",
                Country = "USA",
                Gender = "Male",
                DateOfBirth = new DateTime(1990, 5, 15),
                Password = "password123"
            },
            new User
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                City = "Los Angeles",
                Country = "USA",
                Gender = "Female",
                DateOfBirth = new DateTime(1985, 8, 22),
                Password = "securepassword"
            },
            new User
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Johnson",
                Email = "michael.johnson@example.com",
                City = "London",
                Country = "UK",
                Gender = "Male",
                DateOfBirth = new DateTime(1982, 3, 10),
                Password = "pass1234"
            },
            new User
            {
                Id = 4,
                FirstName = "Emily",
                LastName = "Brown",
                Email = "emily.brown@example.com",
                City = "Toronto",
                Country = "Canada",
                Gender = "Female",
                DateOfBirth = new DateTime(1995, 11, 5),
                Password = "password456"
            },
            new User
            {
                Id = 5,
                FirstName = "William",
                LastName = "Taylor",
                Email = "william.taylor@example.com",
                City = "Sydney",
                Country = "Australia",
                Gender = "Male",
                DateOfBirth = new DateTime(1988, 7, 18),
                Password = "securepass"
            }
        };
        }

        public Task<bool> CreateUserAsync(User newUser)
        {
            try
            {
                _users.Add(newUser);
                return Task.FromResult(true);
            }
            catch 
            {
                return Task.FromResult(false);
            }            
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await Task.FromResult(_users);
        }
    }
}
