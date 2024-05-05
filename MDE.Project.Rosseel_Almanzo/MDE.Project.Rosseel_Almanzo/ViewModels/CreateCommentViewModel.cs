using FreshMvvm;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace MDE.Project.Rosseel_Almanzo.ViewModels
{
    public class CreateCommentViewModel : FreshBasePageModel
    {
        private readonly IRoutesService _routesService;

        private string comment;
        private string id;

        public string Id
        {
            get => id;
            set
            {
                id = value;
                RaisePropertyChanged(nameof(Comment));
            }
        }

        public string Comment 
        {
            get => comment;
            set
            {
                comment = value;
                RaisePropertyChanged(nameof(Comment));
            }
        }

        public CreateCommentViewModel(IRoutesService routesService)
        {
            _routesService = routesService;
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            Id = initData.ToString();
        }

        public ICommand AddCommentCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var newComment = new Comment
                    {
                        Id = Guid.NewGuid().ToString(),
                        CreatedOn = DateTime.Now,
                        Content = comment,
                    };

                    if (!await _routesService.AddCommentAsync(id, newComment))
                    {
                        await CoreMethods.DisplayAlert("Error", "Something went wrong and comment could not be added", "Ok");
                    };

                    await CoreMethods.PushPageModel<RouteDetailsViewModel>(id, false, true);
                });
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<RouteDetailsViewModel>(id, false, true);
                });
            }
        }
    }
}
