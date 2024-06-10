using MDE.Project.Rosseel_Almanzo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces
{
    public interface IEventsService
    {
        Task<Event> GetEventByIdAsync(string id);
        Task<IEnumerable<BaseModel>> GetAllEventsAsync();
        Task<IEnumerable<BaseModel>> GetAllEventsByUserId(string id);
        Task<string> CreateEventAsync(Event newEvent);
        Task<string> DeleteEventAsync(string id);
        Task<bool> AddCommentAsync(string id, Comment comment);
        Task<bool> DeleteCommentAsync(string id, string commentId);
        Task<bool> UpdateEventAsync(Event toUpdate);
        Task<List<BaseModel>> SearchByCity(string cityName);
    }
}
