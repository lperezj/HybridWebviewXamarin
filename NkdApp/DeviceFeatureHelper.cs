using System;
using System.IO;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Location = Xamarin.Essentials.Location;

namespace NkdApp
{
    public class DeviceFeatureHelper
    {
        public async Task<string> TakePhoto(ContentPage pageContext)
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await pageContext.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return null;
            }

            await CheckForCameraAndGalleryPermission();

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "TestPhotoFolder",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.Medium,
                MaxWidthHeight = 1000,
            });

            if (file == null)
                return null;

            // Convert bytes to base64 content
            var imageAsBase64String = Convert.ToBase64String(ConvertFileToByteArray(file));

            return imageAsBase64String;
        }

        public async Task<string> SelectPhoto(ContentPage pageContext)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await pageContext.DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return null;
            }

            await CheckForCameraAndGalleryPermission();

            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
            });

            if (file == null)
                return null;

            // Convert bytes to base64 content
            var imageAsBase64String = Convert.ToBase64String(ConvertFileToByteArray(file));

            return imageAsBase64String;
        }

        private byte[] ConvertFileToByteArray(MediaFile imageFile)
        {
            // Convert Image to bytes
            byte[] imageAsBytes;
            using (var memoryStream = new MemoryStream())
            {
                imageFile.GetStream().CopyTo(memoryStream);
                imageFile.Dispose();
                imageAsBytes = memoryStream.ToArray();
            }

            return imageAsBytes;
        }

        private async Task<bool> CheckForCameraAndGalleryPermission()
        {
            var statusPhotos = await Permissions.CheckStatusAsync<Permissions.Photos>();
            var statusCamera = await Permissions.CheckStatusAsync<Permissions.Camera>();
            var statusStorage = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

            if (statusCamera == PermissionStatus.Granted &&
                statusPhotos == PermissionStatus.Granted &&
                statusStorage == PermissionStatus.Granted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<string> GetDeviceData()
        {
            // Device Model (SMG-950U, iPhone10,6)
            var device = DeviceInfo.Model;

            // Manufacturer (Samsung)
            var manufacturer = DeviceInfo.Manufacturer;

            // Device Name (Motz's iPhone)
            var deviceName = DeviceInfo.Name;

            // Operating System Version Number (7.0)
            var version = DeviceInfo.VersionString;

            // Platform (Android)
            var platform = DeviceInfo.Platform;

            // Idiom (Phone)
            var idiom = DeviceInfo.Idiom;

            // Device Type (Physical)
            var deviceType = DeviceInfo.DeviceType;

            return $"{nameof(DeviceInfo.Model)}: {device}<br />" +
                $"{nameof(DeviceInfo.Manufacturer)}: {manufacturer}<br />" +
                $"{nameof(DeviceInfo.Name)}: {deviceName}<br />" +
                $"{nameof(DeviceInfo.VersionString)}: {version}<br />" +
                $"{nameof(DeviceInfo.Platform)}: {platform}<br />" +
                $"{nameof(DeviceInfo.Idiom)}: {idiom}<br />" +
                $"{nameof(DeviceInfo.DeviceType)}: {deviceType}";
        }

        public async Task<string> GetGpsLocation()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.High);
            var location = await Geolocation.GetLocationAsync(request);

            if (location != null)
            {
                return $"{nameof(Location.Latitude)}: {location.Latitude}<br />" +
                       $"{nameof(Location.Longitude)}: {location.Longitude}<br />" +
                       $"{nameof(Location.Altitude)}: { location.Altitude ?? 0.00 }";
            }

            return null;
        }
    }
}
