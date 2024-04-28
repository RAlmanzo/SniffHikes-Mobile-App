using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDE.Project.Rosseel_Almanzo.Domain.Services
{
    public interface IImageService
    {
        Task<string> TakePhotoAsync();
        Task<string> PickPhotoAsync();
    }
}
