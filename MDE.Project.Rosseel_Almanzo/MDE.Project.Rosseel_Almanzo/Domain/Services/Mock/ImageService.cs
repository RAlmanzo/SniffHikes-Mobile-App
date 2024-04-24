using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MDE.Project.Rosseel_Almanzo.Domain.Services.Mock
{
    public class ImageService : IImageService
    {
        private readonly FirebaseStorage _storageClient;
        public string PhotoPath { get; set; }

        public ImageService()
        {
            _storageClient = new FirebaseStorage("sniffhikes-8e9a6.appspot.com");
        }

        public async Task<string> TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                var result = await LoadPhotoAsync(photo);
                return result;
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                throw;
                return fnsEx.Message;
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                return pEx.Message;
                // Permissions not granted
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");µ
                throw;
                return ex.Message;
            }
        }

        public async Task<string> PickPhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                var result = await LoadPhotoAsync(photo);
                return result;
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                return fnsEx.Message;
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                return pEx.Message;
                // Permissions not granted
            }
            catch (Exception ex)
            {
                return ex.Message;
                //Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }
        private async Task<string> LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo == null)
            {
                PhotoPath = null;
                return "error";
            }
            ////save image in firebase storage
            //var task = new FirebaseStorage("sniffhikes-8e9a6.appspot.com").Child("Events").Child(photo.FileName).PutAsync(await photo.OpenReadAsync());
            //PhotoPath = await task;
            var result = _storageClient.Child("Events").Child(photo.FileName).PutAsync(await photo.OpenReadAsync());
            PhotoPath = await result;
            return PhotoPath;
            //// save the file into local storage
            //var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            //using (var stream = await photo.OpenReadAsync())
            //using (var newStream = File.OpenWrite(newFile))
            //    await stream.CopyToAsync(newStream);

            //PhotoPath = newFile;
        }
    }
}
