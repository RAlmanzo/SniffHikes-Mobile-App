using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDE.Project.Rosseel_Almanzo.Domain.Services
{
    public class RoutesService : IRoutesService
    {
        public Task<bool> AddCommentAsync(string id, Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateRouteAsync(Route newRoute)
        {
            throw new NotImplementedException();
        }

        public Task<List<Route>> GetAllRoutesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Route>> GetAllRoutesByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Route> GetRouteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<BaseModel>> IRoutesService.GetAllRoutesAsync()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<BaseModel>> IRoutesService.GetAllRoutesByUserId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
