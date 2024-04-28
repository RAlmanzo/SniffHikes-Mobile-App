using MDE.Project.Rosseel_Almanzo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces
{
    public interface IZonesService
    {
        Task<Zone> GetZoneByIdAsync(int id);
        Task<List<Zone>> GetAllZonesAsync();
    }
}
