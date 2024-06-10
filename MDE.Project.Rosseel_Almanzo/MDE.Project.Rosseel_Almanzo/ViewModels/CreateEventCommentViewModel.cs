using FreshMvvm;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MDE.Project.Rosseel_Almanzo.ViewModels
{
    public class CreateEventCommentViewModel : FreshBasePageModel
    {
        private readonly IEventsService _eventsService;

        private string content;
        private string id;
        private string contentError;

        public string ContentError
        {
            get => contentError;
            set
            {
                contentError = value;
                RaisePropertyChanged(nameof(ContentError));
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

        public string Content
        {
            get => content;
            set
            {
                content = value;
                RaisePropertyChanged(nameof(Content));
            }
        }

        public CreateEventCommentViewModel(IEventsService eventsService)
        {
            _eventsService = eventsService;
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
                        Content = Content,
                    };

                    if (Validate(newComment))
                    {
                        if (!await _eventsService.AddCommentAsync(id, newComment))
                        {
                            await CoreMethods.DisplayAlert("Error", "Something went wrong and comment could not be added", "Ok");
                        };

                        await CoreMethods.PushPageModel<EventDetailViewModel>(id, false, true);
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
                    await CoreMethods.PushPageModel<EventDetailViewModel>(id, false, true);
                });
            }
        }

        private bool Validate(Comment currentComment)
        {

            var validator = new CommentsValidator();

            var result = validator.Validate(currentComment);

            foreach (var error in result.Errors)
            {
                if (error.PropertyName == nameof(Content))
                {
                    ContentError = error.ErrorMessage;
                }
            }
            return result.IsValid;
        }
    }
}
