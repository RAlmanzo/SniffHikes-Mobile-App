using MDE.Project.Rosseel_Almanzo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces
{
    public interface IZonesService
    {
        Task<Zone> GetZoneByIdAsync(string id);
        Task<List<BaseModel>> GetAllZonesAsync();
        Task<string> CreateZoneAsync(Zone newZone);
        Task<string> DeleteZoneAsync(string id);
        Task<bool> AddCommentAsync(string id, Comment comment);
        Task<bool> DeleteCommentAsync(string id, string commentId);
        Task<bool> UpdateZoneAsync(Zone toUpdate);
        Task<List<BaseModel>> SearchByCity(string cityName);
    }
}
