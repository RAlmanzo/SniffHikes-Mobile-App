using MDE.Project.Rosseel_Almanzo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDE.Project.Rosseel_Almanzo.Domain.Services
{
    public interface IEventsService
    {
        Task<Event> GetEventByIdAsync(int id);
        Task<List<Event>> GetAllEvents();
    }
}
