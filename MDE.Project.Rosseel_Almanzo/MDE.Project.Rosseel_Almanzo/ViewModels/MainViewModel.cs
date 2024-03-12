using FreshMvvm;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Mock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MDE.Project.Rosseel_Almanzo.ViewModels
{
    public class MainViewModel : FreshBasePageModel
    {
        private readonly IFlyoutMenuService _flyoutMenuService;
        private List<FlyoutPageItem> items;

        public List<FlyoutPageItem> Items
        {
            get => items;
            set => items = value;
        }

        public MainViewModel()
        {
            _flyoutMenuService = new MockFlyoutMenuService();
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            RefreshData.Execute(null);
        }

        public ICommand RefreshData
        {
            get
            {
                return new Command(async () =>
                {
                    List<FlyoutPageItem> fetchedItems = await _flyoutMenuService.GetItems();
                    Items = new List<FlyoutPageItem>(fetchedItems);
                });
            }
        }

        public ICommand HomeCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<MainViewModel>();
                });
            }
        }
        public ICommand EventsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<EventsViewModel>();
                });
            }
        }
    }
}
