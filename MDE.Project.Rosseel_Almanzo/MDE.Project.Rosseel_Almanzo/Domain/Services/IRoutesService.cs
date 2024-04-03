using MDE.Project.Rosseel_Almanzo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDE.Project.Rosseel_Almanzo.Domain.Services
{
    public interface IRoutesService
    {
        Task<Route> GetRouteByIdAsync(int id);
        Task<List<Route>> GetAllRoutesAsync();
        //Task<List<Image>> GetRouteImagesAsync(int id);
        Task<List<Route>> GetAllRoutesByUserId(int id);
        Task<bool> CreateRouteAsync(Route newRoute);
    }
}
