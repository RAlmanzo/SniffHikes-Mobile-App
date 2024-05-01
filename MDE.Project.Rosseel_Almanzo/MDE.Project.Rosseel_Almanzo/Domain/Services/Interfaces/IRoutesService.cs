using MDE.Project.Rosseel_Almanzo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces
{
    public interface IRoutesService
    {
        Task<Route> GetRouteByIdAsync(int id);
        Task<IEnumerable<BaseModel>> GetAllRoutesAsync();
        Task<IEnumerable<BaseModel>> GetAllRoutesByUserId(int id);
        Task<string> CreateRouteAsync(Route newRoute);
        Task<bool> AddCommentAsync(string id, Comment comment);
    }
}
