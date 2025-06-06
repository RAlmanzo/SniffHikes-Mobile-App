﻿using MDE.Project.Rosseel_Almanzo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces
{
    public interface IRoutesService
    {
        Task<Route> GetRouteByIdAsync(string id);
        Task<IEnumerable<BaseModel>> GetAllRoutesAsync();
        Task<IEnumerable<BaseModel>> GetAllRoutesByUserId(string id);
        Task<string> CreateRouteAsync(Route newRoute);
        Task<string> DeleteRouteAsync(string id);
        Task<bool> AddCommentAsync(string id, Comment comment);
        Task<bool> DeleteCommentAsync(string id, string commentId);
        Task<bool> UpdateRouteAsync(Route toUpdate);
        Task<List<BaseModel>> SearchByCity(string cityName);
    }
}
