using FreshMvvm;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MDE.Project.Rosseel_Almanzo.ViewModels
{
    public class UpdateRouteViewModel : FreshBasePageModel
    {
        private readonly IRoutesService _routesService;

        private string id;
        private string title;
        private string description;
        private string street;
        private string city;
        private string country;
        private DateTime dateEvent;
        private ObservableCollection<Domain.Models.Image> images;
        private ObservableCollection<Comment> comments;
        private Comment selectedComment;

        public Comment SelectedComment
        {
            get => selectedComment;
            set
            {
                selectedComment = value;
                RaisePropertyChanged(nameof(SelectedComment));
                if (selectedComment != null)
                {
                    DeleteCommentCommand.Execute(null);
                }
            }
        }

        public ObservableCollection<Comment> Comments
        {
            get => comments;
            set
            {
                comments = value;
                RaisePropertyChanged(nameof(Comments));
            }
        }

        public ObservableCollection<Domain.Models.Image> Images
        {
            get => images;
            set
            {
                images = value;
                RaisePropertyChanged(nameof(Images));
            }
        }

        public DateTime DateEvent
        {
            get => dateEvent;
            set
            {
                dateEvent = value;
                RaisePropertyChanged(nameof(DateEvent));
            }
        }

        public string Country
        {
            get => country;
            set
            {
                country = value;
                RaisePropertyChanged(nameof(Country));
            }
        }

        public string City
        {
            get => city;
            set
            {
                city = value;
                RaisePropertyChanged(nameof(City));
            }
        }

        public string Street
        {
            get => street;
            set
            {
                street = value;
                RaisePropertyChanged(nameof(Street));
            }
        }

        public string Description
        {
            get => description;
            set
            {
                description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

        public string Title
        {
            get => title;
            set
            {
                title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        public string Id
        {
            get => id;
            set
            {
                id = value;
                RaisePropertyChanged(nameof(Id));
            }
        }

        public UpdateRouteViewModel(IRoutesService routesService)
        {
            Images = new ObservableCollection<Domain.Models.Image>();
            Comments = new ObservableCollection<Comment>();
            _routesService = routesService;
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            Id = initData.ToString();
            RefreshData.Execute(null);
        }

        public ICommand RefreshData
        {
            get
            {
                return new Command(async () =>
                {
                    var selectedRoute = await _routesService.GetRouteByIdAsync(id);
                    Title = selectedRoute.Title;
                    Description = selectedRoute.Description;
                    Street = selectedRoute.Street;
                    City = selectedRoute.City;
                    Country = selectedRoute.Country;
                    DateEvent = selectedRoute.DateEvent;
                    Images = selectedRoute.Images != null ? new ObservableCollection<Domain.Models.Image>(selectedRoute.Images) : new ObservableCollection<Domain.Models.Image>();
                    Comments = selectedRoute.Comments != null ? new ObservableCollection<Comment>(selectedRoute.Comments) : new ObservableCollection<Comment>();
                });
            }
        }

        public ICommand DeleteCommentCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var result = await CoreMethods.DisplayAlert("Delete Comment", "Are u sure u want to delete comment?", "Yes", "Cancel");
                    if (result)
                    {
                        var deleteResult = await _routesService.DeleteCommentAsync(Id, selectedComment.Id);
                        if (deleteResult)
                        {
                            Comments.Remove(selectedComment);
                            await CoreMethods.DisplayAlert("Delete Comment", "Comment succesfull deleted", "Ok");
                        }
                        else
                        {
                            await CoreMethods.DisplayAlert("Delete Comment", "Delete comment failed!", "Ok");
                        }
                    }
                });
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<RoutesViewModel>();
                });
            }
        }

        public ICommand DeleteRouteCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var result = await _routesService.DeleteRouteAsync(Id);
                    if(result == "Deleted")
                    {
                        await CoreMethods.DisplayAlert("Deleted", "Route succesfull deleted!", "Ok");
                    }
                    else
                    {
                        await CoreMethods.DisplayAlert("Failed", result, "Ok");
                    }

                    await CoreMethods.PushPageModel<RoutesViewModel>();
                });
            }
        }
    }
}
