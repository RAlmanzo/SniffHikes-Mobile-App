using MDE.Project.Rosseel_Almanzo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDE.Project.Rosseel_Almanzo.Domain.Services
{
    public interface IEventsService
    {
        Task<Event> GetEventByIdAsync(string id);
        Task <List<BaseModel<Event>>> GetAllEventsAsync();
        //Task<List<Image>> GetEventImagesAsync(int id);
        Task<List<Event>> GetAllEventsByUserId(int id);
        Task<bool> CreateEventAsync(Event newEvent);
        Task<bool> AddCommentAsync(string id, Comment comment);

    }
}
