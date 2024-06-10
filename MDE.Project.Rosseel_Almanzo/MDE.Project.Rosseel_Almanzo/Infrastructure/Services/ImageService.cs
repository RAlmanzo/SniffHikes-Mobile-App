using Firebase.Storage;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using MDE.Project.Rosseel_Almanzo.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MDE.Project.Rosseel_Almanzo.Domain.Services
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
            }
        }

        private async Task<string> LoadPhotoAsync(FileResult photo)
        {
            if (photo == null)
            {
                PhotoPath = null;
                return "error";
            }

            var result = _storageClient.Child("Events").Child(photo.FileName).PutAsync(await photo.OpenReadAsync());
            PhotoPath = await result;
            return PhotoPath;
        }

        public async Task<bool> DeleteImage(Image image)
        {
            var uri = new Uri(image.ImagePath);
            var path = uri.AbsolutePath;
            var decodedPath = Uri.UnescapeDataString(path);
            var fileName = decodedPath.Split('/').Last();

            await _storageClient.Child("Events").Child(fileName).DeleteAsync();
            return await Task.FromResult(true);
        }
    }
}
