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
        Task<bool> AddCommentAsync(string id, Comment comment);

    }
}
