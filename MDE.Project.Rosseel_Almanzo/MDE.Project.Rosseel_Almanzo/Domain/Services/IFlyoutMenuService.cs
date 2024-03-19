using MDE.Project.Rosseel_Almanzo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MDE.Project.Rosseel_Almanzo.Domain.Services
{
    public interface IFlyoutMenuService
    {
        Task<List<FlyoutPageItem>> GetItems();
    }
}
