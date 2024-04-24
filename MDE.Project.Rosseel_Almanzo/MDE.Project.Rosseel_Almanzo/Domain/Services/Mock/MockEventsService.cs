using Firebase.Database;
using Firebase.Database.Query;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;

namespace MDE.Project.Rosseel_Almanzo.Domain.Services.Mock
{
    public class MockEventsService : IEventsService
    {
        private static List<Event> _events;
        private readonly FirebaseClient _client;

        public MockEventsService()
        {
            _client = new FirebaseClient("https://sniffhikes-8e9a6-default-rtdb.europe-west1.firebasedatabase.app/");
        }

        public async Task<bool> CreateEventAsync(Event newEvent)
        {
            try
            {
                //_events.Add(newEvent);
                await _client.Child("Events")
                    .PostAsync(newEvent);
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> AddCommentAsync(string id, Comment comment)
        {
            var existingEvent = await GetEventByIdAsync(id);
            existingEvent.Comments.Add(comment);
            await _client.Child("Events").Child(id).PutAsync(existingEvent);
            return false;
        }

        public async Task<List<BaseModel<Event>>> GetAllEventsAsync()
        {
            var eventsSnapshot = await _client.Child("Events").OnceAsync<Event>();

            var eventsList = new List<BaseModel<Event>>();

            foreach (var snapshot in eventsSnapshot)
            {
                var eventData = new BaseModel<Event>
                {
                    Key = snapshot.Key,
                    Value = snapshot.Object,
                };
                
                eventsList.Add(eventData);
            }

            return eventsList;
        }


        public async Task<List<Event>> GetAllEventsByUserId(int id)
        {
            return await Task.FromResult(_events.Where(e => e.OrginazerId == id).ToList());
        }

        public async Task<Event> GetEventByIdAsync(string id)
        {
            var eventSnapshot = await _client.Child("Events").Child(id).OnceSingleAsync<Event>();

            if (eventSnapshot != null)
            {
                return await Task.FromResult(eventSnapshot);
            }
            return null;
            //return await Task.FromResult(_events.FirstOrDefault(e => e.Id == id));
        }

        //public async Task<List<Image>> GetEventImagesAsync(int id)
        //{
        //    var selectedEvent = await GetEventByIdAsync(id);
        //    return (List<Image>)await Task.FromResult(selectedEvent.Images);
        //}
    }
}
