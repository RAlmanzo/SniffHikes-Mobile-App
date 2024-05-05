using FreshMvvm;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Mock;
using MDE.Project.Rosseel_Almanzo.Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Validators;
using Xamarin.Essentials;

namespace MDE.Project.Rosseel_Almanzo.ViewModels
{
    public class AddDogViewModel : FreshBasePageModel
    {
        private readonly IUsersService _usersService;

        private string name;
        private DateTime dateOfBirth;
        private string gender;
        private string race;
        private List<Dog> dogs;
        private string nameError;
        private string dateOfBirthError;

        public string Id { get; private set; }

        public string NameError
        {
            get => nameError;
            set
            {
                nameError = value;
                RaisePropertyChanged(nameof(NameError));
            }
        }

        public string DateOfBirthError
        {
            get => dateOfBirthError;
            set
            {
                dateOfBirthError = value;
                RaisePropertyChanged(nameof(DateOfBirthError));
            }
        }

        public List<Dog> Dogs
        {
            get => dogs;
            set
            {
                dogs = value;
                RaisePropertyChanged(nameof(Dogs));
            }
        }

        public string Gender
        {
            get => gender;
            set
            {
                gender = value;
                RaisePropertyChanged(nameof(Gender));
            }
        }

        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set
            {
                dateOfBirth = value;
                RaisePropertyChanged(nameof(DateOfBirth));
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public string Race
        {
            get => race;
            set
            {
                race = value;
                RaisePropertyChanged(nameof(Race));
            }
        }

        public AddDogViewModel(IUsersService usersService)
        {
            _usersService = usersService;
            dogs = new List<Dog>();
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            Id = initData.ToString();
        }

        public ICommand AddDogCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var dog = new Dog
                    {
                        Name = Name,
                        Race = Race,
                        Gender = Gender,
                        DateOfBirth = DateOfBirth,
                    };
                    
                    if (Validate(dog))
                    {
                        var result = await _usersService.AddDogAsync(Id, dog);
                        if (!result)
                        {
                            await CoreMethods.DisplayAlert("Failed", "Could not add dog!", "Ok");
                        }

                        await CoreMethods.PushPageModel<ProfileViewModel>();
                    }                   
                });
            }
        }

        private bool Validate(Dog dog)
        {

            var validator = new DogsValidator();

            var result = validator.Validate(dog);

            foreach (var error in result.Errors)
            {
                if (error.PropertyName == nameof(Name))
                {
                    NameError = error.ErrorMessage;
                }

                if (error.PropertyName == nameof(DateOfBirth))
                {
                    DateOfBirthError = error.ErrorMessage;
                }
            }
            return result.IsValid;
        }
    }
}
