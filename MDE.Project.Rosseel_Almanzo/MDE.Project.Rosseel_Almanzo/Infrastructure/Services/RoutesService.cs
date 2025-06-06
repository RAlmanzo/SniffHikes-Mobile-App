﻿using Firebase.Database;
using Firebase.Database.Query;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using MDE.Project.Rosseel_Almanzo.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MDE.Project.Rosseel_Almanzo.Infrastructure.Services
{
    public class RoutesService : IRoutesService
    {
        private readonly FirebaseClient _client;

        public RoutesService(FirebaseClient client)
        {
            _client = client;
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

        public async Task<IEnumerable<BaseModel>> GetAllRoutesAsync()
        {
            //get de data
            var routesSnapshot = await _client.Child("Routes").OnceAsync<RouteDto>();
            var sortedRoutes = routesSnapshot.OrderByDescending(r => r.Object.DateEvent);
            //map data to event collection
            var routes = sortedRoutes.Select(e => new BaseModel
            {
                Id = e.Key,
                Title = e.Object.Title,
                Description = e.Object.Description,
                Image = e.Object.Images?.FirstOrDefault(),
                OrginazerId = e.Object.OrganizerId,
            }).ToList();

            return await Task.FromResult(routes);
        }

        public async Task<Route> GetRouteByIdAsync(string id)
        {
            var routeSnapshot = await _client.Child("Routes").Child(id).OnceSingleAsync<RouteDto>();

            if (routeSnapshot != null)
            {
                var selectedRoute = new Route
                {
                    Id = routeSnapshot.Id,
                    Title = routeSnapshot.Title,
                    Description = routeSnapshot.Description,
                    Street = routeSnapshot.Street,
                    City = routeSnapshot.City,
                    Country = routeSnapshot.Country,
                    DateEvent = routeSnapshot.DateEvent,
                    OrganizerId = routeSnapshot.OrganizerId,
                    Images = routeSnapshot.Images,
                    Comments = routeSnapshot.Comments != null ? routeSnapshot.Comments.OrderByDescending(c => c.CreatedOn).ToList() : new List<Comment>(),
                };
                return await Task.FromResult(selectedRoute);
            };
            return null;
        }

        public async Task<bool> AddCommentAsync(string id, Comment comment)
        {
            try
            {
                var existingRoute = await GetRouteByIdAsync(id);
                if (existingRoute.Comments == null)
                {
                    existingRoute.Comments = new ObservableCollection<Comment>();
                }
                var token = await SecureStorage.GetAsync("token");
                comment.UserId = token;
                var name = await SecureStorage.GetAsync("name");
                comment.UserName = name;

                existingRoute.Comments.Add(comment);

                await _client.Child("Routes").Child(id).PutAsync(existingRoute);
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
                var route = await GetRouteByIdAsync(id);
                //get the comment
                var selectedComment = route.Comments.Where(c => c.Id == commentId).FirstOrDefault();
                //delete comment and update db
                route.Comments.Remove(selectedComment);

                await _client.Child("Routes").Child(id).PutAsync(route);
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<IEnumerable<BaseModel>> GetAllRoutesByUserId(string id)
        {
            //get data
            var myRoutesSnapshot = await _client.Child("Routes").OnceAsync<RouteDto>();
            var routesList = myRoutesSnapshot.Where(e => e.Object.OrganizerId == id).ToList();

            //map data to routes collection
            var routes = routesList.Select(e => new BaseModel
            {
                Id = e.Key,
                Title = e.Object.Title,
                Description = e.Object.Description,
                Image = e.Object.Images?.FirstOrDefault(),
                OrginazerId = e.Object.OrganizerId,
            }).ToList();

            return await Task.FromResult(routes);
        }

        public async Task<string> DeleteRouteAsync(string id)
        {
            try
            {
                //TODO delete images from db!!!!!!!!!!!!!!!!!!!!!!!

                await _client.Child("Routes").Child(id).DeleteAsync();
                return await Task.FromResult("Deleted");
            }
            catch (Exception ex)
            {
                return await Task.FromResult(ex.Message);
            }

        }

        public async Task<bool> UpdateRouteAsync(Route toUpdate)
        {
            try
            {
                await _client.Child("Routes").Child(toUpdate.Id)
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
            var routesSnapshot = await _client.Child("Routes").OnceAsync<RouteDto>();
            //sort data
            var cityRoutes = routesSnapshot.Where(z => z.Object.City.ToLower().Contains(cityName.ToLower()));

            var zones = cityRoutes.Select(e => new BaseModel
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
