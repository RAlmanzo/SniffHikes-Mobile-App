﻿using FreshMvvm;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Linq;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using MDE.Project.Rosseel_Almanzo.Infrastructure.Services;

namespace MDE.Project.Rosseel_Almanzo.ViewModels
{
    public class ZoneDetailsViewModel : FreshBasePageModel
    {
        private readonly IZonesService _zonesService;

        private string id;
        private string title;
        private string description;
        private string street;
        private string city;
        private string country;
        private ObservableCollection<Domain.Models.Image> images;
        private ObservableCollection<Comment> comments;
        private Comment selectedComment;
        private string commentCreator;

        public string CommentCreator
        {
            get => commentCreator;
            set
            {
                commentCreator = value;
            }
        }

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

        public ZoneDetailsViewModel(IZonesService zonesService)
        {
            Images = new ObservableCollection<Domain.Models.Image>();
            Comments = new ObservableCollection<Comment>();
            _zonesService = zonesService;
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

        public async override void Init(object initData)
        {
            base.Init(initData);

            Id = initData.ToString();

            CommentCreator = await SecureStorage.GetAsync("token");
            GetZoneDetails.Execute(null);
        }

        public ICommand GetZoneDetails
        {
            get
            {
                return new Command(async () =>
                {
                    var item = await _zonesService.GetZoneByIdAsync(Id);
                    Title = item.Title;
                    Description = item.Description;
                    Street = item.Street;
                    City = item.City;
                    Country = item.Country;
                    Images = item.Images != null ? new ObservableCollection<Domain.Models.Image>(item.Images) : new ObservableCollection<Domain.Models.Image>();
                    Comments = item.Comments != null ? new ObservableCollection<Comment>(item.Comments) : new ObservableCollection<Comment>();
                });
            }
        }

        public ICommand GetZoneDirections
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        var item = await _zonesService.GetZoneByIdAsync(Id);
                        var address = $"{item.Street} {item.City} {item.Country}";
                        var locations = await Geocoding.GetLocationsAsync(address);

                        var location = locations?.FirstOrDefault();
                        if (location != null)
                        {
                            var locationCoordinates = new Location(location.Latitude, location.Longitude);

                            try
                            {
                                await Map.OpenAsync(locationCoordinates, new MapLaunchOptions
                                {
                                    Name = item.Title,
                                    NavigationMode = NavigationMode.None,
                                });
                            }
                            catch (Exception ex)
                            {
                                // No map application available to open
                            }
                        }
                    }
                    catch (FeatureNotSupportedException fnsEx)
                    {
                        // Feature not supported on device
                    }
                    catch (Exception ex)
                    {
                        // Handle exception that may have occurred in geocoding
                    }
                });
            }
        }

        public ICommand BackCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<ZonesViewModel>();
                });
            }
        }

        public ICommand AddCommentCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<CreateZoneCommentViewModel>(id);
                });
            }
        }

        public ICommand DeleteCommentCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (selectedComment.UserId == commentCreator)
                    {
                        var result = await CoreMethods.DisplayAlert("Delete Comment", "Are u sure u want to delete comment?", "Yes", "Cancel");
                        if (result)
                        {
                            var deleteResult = await _zonesService.DeleteCommentAsync(Id, selectedComment.Id);
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
                    }              
                });
            }
        }
    }
}
