using FreshMvvm;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Linq;

namespace MDE.Project.Rosseel_Almanzo.ViewModels
{
    public class ZoneDetailsViewModel : FreshBasePageModel
    {
        private readonly IZonesService _zonesService;

        private int id;
        private string title;
        private string description;
        private string street;
        private string city;
        private string country;
        private ObservableCollection<Domain.Models.Image> images;
        private ObservableCollection<Comment> comments;

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

        public int Id
        {
            get => id;
            set
            {
                id = value;
                RaisePropertyChanged(nameof(Id));
            }
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            Id = (int)initData;

            GetRouteDetails.Execute(null);
        }

        public ICommand GetRouteDetails
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
                    comments = item.Comments != null ? new ObservableCollection<Comment>(item.Comments) : new ObservableCollection<Comment>();
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
    }
}
