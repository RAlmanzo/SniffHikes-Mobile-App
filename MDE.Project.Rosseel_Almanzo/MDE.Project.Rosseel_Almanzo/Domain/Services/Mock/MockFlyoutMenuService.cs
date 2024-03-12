using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MDE.Project.Rosseel_Almanzo.Domain.Services.Mock
{
    public class MockFlyoutMenuService : IFlyoutMenuService
    {
        private static List<FlyoutPageItem> _items;

        public MockFlyoutMenuService()
        {
            _items = new List<FlyoutPageItem>
            {
                new FlyoutPageItem
                {
                    Title = "Home",
                    IconSource = "https://source.unsplash.com/user/c_v_r/1900x800",
                },
                new FlyoutPageItem
                {
                    Title = "Events",
                    IconSource = "https://source.unsplash.com/user/c_v_r/1900x800",
                },
                new FlyoutPageItem
                {
                    Title = "Routes",
                    IconSource = "https://source.unsplash.com/user/c_v_r/1900x800",
                },
            };
        }
        public async Task<List<FlyoutPageItem>> GetItems()
        {
            return await Task.FromResult(_items);
        }
    }
}