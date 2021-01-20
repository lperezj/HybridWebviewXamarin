using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Common;
using Android.Content;
using Firebase.Iid;
using Android.Gms.Extensions;
using Xamarin.Essentials;

namespace NkdApp.Droid
{
    [Activity(
        Label = "Nieuw Kijkduin",
        Icon = "@mipmap/ic_launcher",
        LaunchMode = LaunchMode.SingleTop,
        Theme = "@style/Theme.Splash",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public const string TAG = "MainActivity";
        internal static readonly string CHANNEL_ID = "ndk_notification_channel";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            //CrossCurrentActivity.Current.Init(this, savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            ZXing.Net.Mobile.Forms.Android.Platform.Init(); // Initiallize QR Reader
            ZXing.Mobile.MobileBarcodeScanner.Initialize(Application);

            LoadApplication(new App());

            ConfigureNotification();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void ConfigureNotification()
        {
            if (!IsPlayServicesAvailable())
            {
                throw new Exception("This device does not have Google Play Services and cannot receive push notifications.");
            }

            if (Intent.Extras != null)
            {
                foreach (var key in Intent.Extras.KeySet())
                {
                    var value = Intent.Extras.GetString(key);
                }
            }

            RegisterServiceWithActiveFireBaseToken();

            CreateNotificationChannel();
        }

        protected override void OnNewIntent(Intent intent)
        {
            if (intent != null)
                VerifyIntent(intent);

            base.OnNewIntent(intent);
        }

        private void VerifyIntent(Intent intent)
        {
           
        }

        private async void RegisterServiceWithActiveFireBaseToken()
        {
            try
            {
                var instanceIdResult = await FirebaseInstanceId.Instance.GetInstanceId().AsAsync<IInstanceIdResult>();
                var token = instanceIdResult.Token;
                Preferences.Set("NotifToken", token);
                Console.WriteLine($"FIREBASE TOKEN: {token}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FIREBASE ERROR: {ex.Message}");
            }
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    Console.WriteLine(TAG, GoogleApiAvailability.Instance.GetErrorString(resultCode));
                else
                {
                    Console.WriteLine(TAG, "This device is not supported");
                    Finish();
                }
                return false;
            }

            Console.WriteLine(TAG, "Google Play Services is available.");
            return true;
        }

        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var channelName = CHANNEL_ID;
            var channelDescription = string.Empty;
            var channel = new NotificationChannel(CHANNEL_ID, channelName, NotificationImportance.Default)
            {
                Description = channelDescription
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }
    }
}