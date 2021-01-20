using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Firebase.Messaging;
using Xamarin.Essentials;

namespace NkdApp.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class FirabaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "FirabaseMessagingService";

        public override void OnMessageReceived(RemoteMessage message)
        {
            //base.OnMessageReceived(message);
            //Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            var body = message.GetNotification().Body;
            SendNotification(body, message.Data);
        }

        private void SendNotification(string messageBody, IDictionary<string, string> data)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            foreach (var key in data.Keys)
            {
                intent.PutExtra(key, data[key]);
            }

            var pendingIntent = PendingIntent.GetActivity(this,
                                                          0,
                                                          intent,
                                                          PendingIntentFlags.OneShot);

            var notificationBuilder = new NotificationCompat.Builder(this, MainActivity.CHANNEL_ID)
                                      .SetSmallIcon(Resource.Drawable.notification_template_icon_bg)
                                      .SetContentTitle("NDK Notification")
                                      .SetContentText(messageBody)
                                      .SetAutoCancel(true)
                                      .SetContentIntent(pendingIntent);

            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(Guid.NewGuid().ToString().GetHashCode(), notificationBuilder.Build());
        }

        public override void OnNewToken(string token)
        {
            Console.WriteLine(TAG, "FCM token: " + token);
            Preferences.Set("NotifToken", token);
        }
    }
}
