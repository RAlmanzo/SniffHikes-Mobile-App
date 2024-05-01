using Firebase.Database;
using Firebase.Database.Query;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MDE.Project.Rosseel_Almanzo.Domain.Services.Mock
{
    public class EventsService : IEventsService
    {
        private static List<Event> _events;
        private readonly FirebaseClient _client;

        public EventsService()
        {
            _client = new FirebaseClient("https://sniffhikes-8e9a6-default-rtdb.europe-west1.firebasedatabase.app/");
        }

        public async Task<string> CreateEventAsync(Event newEvent)
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                newEvent.OrginazerId = token;
                await _client.Child("Events")
                    .PostAsync(newEvent);
                return await Task.FromResult("Created");
            }
            catch(Exception ex)
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
            var eventsSnapshot = await _client.Child("Events").OnceAsync<Event>();

            //map data to event collection
            var events = eventsSnapshot.Select(e => new BaseModel
            {
                Id = e.Key,
                Title = e.Object.Title,
                Description = e.Object.Description,
                Image = e.Object.Images?.FirstOrDefault(),
            }).ToList();

            return await Task.FromResult(events);
        }


        public async Task<IEnumerable<BaseModel>> GetAllEventsByUserId(string id)
        {
            //get data
            var myEventsSnapshot = await _client.Child("Events").OnceAsync<Event>();            
            var eventsList = myEventsSnapshot.Where(e => e.Object.OrginazerId == id).ToList();

            //map data to events collection
            var events = eventsList.Select(e => new BaseModel
            {
                Id = e.Key,
                Title = e.Object.Title,
                Description = e.Object.Description,
                Image = e.Object.Images?.FirstOrDefault(),
            }).ToList();

            return await Task.FromResult(events);
        }

        public async Task<Event> GetEventByIdAsync(string id)
        {
            var eventSnapshot = await _client.Child("Events").Child(id).OnceSingleAsync<Event>();

            if (eventSnapshot != null)
            {
                return await Task.FromResult(eventSnapshot);
            }
            return null;
        }
    }
}
