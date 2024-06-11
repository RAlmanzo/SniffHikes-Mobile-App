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
    public class EventsService : IEventsService
    {
        private readonly FirebaseClient _client;

        public EventsService(FirebaseClient client)
        {
            _client = client;
        }

        public async Task<string> CreateEventAsync(Event newEvent)
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                newEvent.OrginazerId = token;
                var result = await _client.Child("Events")
                    .PostAsync(newEvent);
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
                var existingEvent = await GetEventByIdAsync(id);
                if (existingEvent.Comments == null)
                {
                    existingEvent.Comments = new ObservableCollection<Comment>();
                }
                var token = await SecureStorage.GetAsync("token");
                comment.UserId = token;
                var name = await SecureStorage.GetAsync("name");
                comment.UserName = name;

                existingEvent.Comments.Add(comment);

                await _client.Child("Events").Child(id).PutAsync(existingEvent);
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<IEnumerable<BaseModel>> GetAllEventsAsync()
        {
            //get de data
            var eventsSnapshot = await _client.Child("Events").OnceAsync<EventDto>();
            var sortedEvents = eventsSnapshot.OrderByDescending(r => r.Object.DateEvent);

            //map data to event collection
            var events = sortedEvents.Select(e => new BaseModel
            {
                Id = e.Key,
                Title = e.Object.Title,
                Description = e.Object.Description,
                Image = e.Object.Images?.FirstOrDefault(),
                OrginazerId = e.Object.OrginazerId,
            }).ToList();

            return await Task.FromResult(events);
        }


        public async Task<IEnumerable<BaseModel>> GetAllEventsByUserId(string id)
        {
            //get data
            var myEventsSnapshot = await _client.Child("Events").OnceAsync<EventDto>();
            var eventsList = myEventsSnapshot.Where(e => e.Object.OrginazerId == id).OrderByDescending(e => e.Object.DateEvent).ToList();

            //map data to events collection
            var events = eventsList.Select(e => new BaseModel
            {
                Id = e.Key,
                Title = e.Object.Title,
                Description = e.Object.Description,
                Image = e.Object.Images?.FirstOrDefault(),
                OrginazerId = e.Object.OrginazerId,
            }).ToList();

            return await Task.FromResult(events);
        }

        public async Task<Event> GetEventByIdAsync(string id)
        {
            var eventSnapshot = await _client.Child("Events").Child(id).OnceSingleAsync<EventDto>();

            if (eventSnapshot != null)
            {
                var selectedEvent = new Event
                {
                    Id = eventSnapshot.Id,
                    Title = eventSnapshot.Title,
                    Description = eventSnapshot.Description,
                    Street = eventSnapshot.Street,
                    City = eventSnapshot.City,
                    Country = eventSnapshot.Country,
                    DateEvent = eventSnapshot.DateEvent,
                    OrginazerId = eventSnapshot.OrginazerId,
                    Images = eventSnapshot.Images,
                    Comments = eventSnapshot.Comments != null ? eventSnapshot.Comments.OrderByDescending(c => c.CreatedOn).ToList() : new List<Comment>(),
                    AttendingUserNames = eventSnapshot.AttendingUserNames ?? new List<string>(),
                };
                return await Task.FromResult(selectedEvent);
            };
            return null;
        }

        public async Task<string> DeleteEventAsync(string id)
        {
            try
            {
                //TODO delete images from db!!!!!!!!!!!!!!!!!!!!!!!

                await _client.Child("Events").Child(id).DeleteAsync();
                return await Task.FromResult("Deleted");
            }
            catch (Exception ex)
            {
                return await Task.FromResult(ex.Message);
            }

        }

        public async Task<bool> DeleteCommentAsync(string id, string commentId)
        {
            try
            {
                //get the route
                var selectedEvent = await GetEventByIdAsync(id);
                //get the comment
                var selectedComment = selectedEvent.Comments.Where(c => c.Id == commentId).FirstOrDefault();
                //delete comment and update db
                selectedEvent.Comments.Remove(selectedComment);

                await _client.Child("Events").Child(id).PutAsync(selectedEvent);
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> UpdateEventAsync(Event toUpdate)
        {
            try
            {
                await _client.Child("Events").Child(toUpdate.Id)
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
            var eventsSnapshot = await _client.Child("Events").OnceAsync<RouteDto>();
            //sort data
            var cityEvents = eventsSnapshot.Where(z => z.Object.City.ToLower().Contains(cityName.ToLower()));

            var zones = cityEvents.Select(e => new BaseModel
            {
                Id = e.Key,
                Title = e.Object.Title,
                Description = e.Object.Description,
                Image = e.Object.Images?.FirstOrDefault(),
                OrginazerId = e.Object.OrganizerId,
            }).ToList();

            return await Task.FromResult(zones);
        }

        public async Task<bool> SignUpToEvent(string id, string userId)
        {
            //get event
            var currentEvent = await GetEventByIdAsync(id);
            if (currentEvent != null)
            {              
                if (currentEvent.AttendingUserNames == null)
                {
                    currentEvent.AttendingUserNames = new ObservableCollection<string>();
                }
                currentEvent.AttendingUserNames.Add(userId);

                await _client.Child("Events").Child(id).PutAsync(currentEvent);
                return await Task.FromResult(true);
            }
            else
                return await Task.FromResult(false);
        }

        public async Task<IEnumerable<BaseModel>> GetRegisteredEventsByUserId(string userId)
        {
            //get data
            var eventsSnapshot = await _client.Child("Events").OnceAsync<EventDto>();
            var registeredEvents = eventsSnapshot.Where(e => e.Object.AttendingUserNames != null).Where(e => e.Object.AttendingUserNames.Any(u => u.Equals(userId))).ToList();

            var events = registeredEvents.Select(e => new BaseModel
            {
                Id = e.Key,
                Title = e.Object.Title,
                Description = e.Object.Description,
                Image = e.Object.Images?.FirstOrDefault(),
                OrginazerId = e.Object.OrginazerId,
            }).ToList();

            return await Task.FromResult(events);
        }
    }
}
