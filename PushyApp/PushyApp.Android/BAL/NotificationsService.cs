using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PushyApp.Droid.BAL
{
    [Service]
    public class NotificationsService : Service
    {

        public const int SERVICE_RUNNING_NOTIFICATION_ID = 10000;
        internal static readonly string CHANNEL_ID = "my_notification_channel";
        internal static readonly string CHANNEL_NAME = "my_notification_channel_name";
        

        public override void OnCreate()
        {
            base.OnCreate();
            Context context = ApplicationContext;
            Intent service = new Intent(context, typeof(PushReceiver));
            //CreateNotificationChannel();

            //Toast.MakeText(this, "Notifications Service - Created", ToastLength.Long);
            //PushReceiver ps = new PushReceiver();
            //ps.OnReceive(context, service);
        }

        /* public override StartCommandResult OnStartCommand(Android.Content.Intent intent, StartCommandFlags flags, int startId)
         {
             //Toast.MakeText(this, "Notifications Service - Started", ToastLength.Long);
             return StartCommandResult.Sticky;
         }*/


        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {

            

            CreateNotificationChannel();
            string messageBody = "";
            // / Create an Intent for the activity you want to start
            Intent resultIntent = new Intent(this, typeof(MainActivity));
            // Create the TaskStackBuilder and add the intent, which inflates the back stack
            TaskStackBuilder stackBuilder = TaskStackBuilder.Create(this);
            stackBuilder.AddNextIntentWithParentStack(resultIntent);
            // Get the PendingIntent containing the entire back stack
            PendingIntent resultPendingIntent = stackBuilder.GetPendingIntent(0, PendingIntentFlags.UpdateCurrent);
            var notification = new Notification.Builder(this, CHANNEL_ID)
             .SetContentIntent(resultPendingIntent)
             .SetContentTitle("ECM Notifier is ON")
             .SetContentText(messageBody)
             .SetSmallIcon(Resource.Drawable.icon)
             .SetOngoing(true)            
             .Build();
            StartForeground(SERVICE_RUNNING_NOTIFICATION_ID, notification);
            //do you work
            return StartCommandResult.Sticky;


        }

        public override void OnDestroy()
        {
            //base.OnDestroy();
            //StopForeground(true);
        }
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var channel = new NotificationChannel(CHANNEL_ID, CHANNEL_NAME, NotificationImportance.Default)
            {
                Description = "Firebase Cloud Messages appear in this channel"
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

    }
}