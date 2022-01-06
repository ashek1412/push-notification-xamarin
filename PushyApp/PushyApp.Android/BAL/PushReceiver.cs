using Android.App;
using Android.Content;

using Android.Graphics;
using Android.Media;
using Android.Support.V4.App;
using ME.Pushy.Sdk;
using System;

namespace PushyApp.Droid.BAL
{
	[BroadcastReceiver(Enabled = true, Exported = false)]
	[IntentFilter(new[] { "pushy.me" })]
	public class PushReceiver : BroadcastReceiver
	{
		public override void OnReceive(Context context1, Intent intent)
		{
			string messageTitle = "ECM Notifier";
			string notificationText = "Test notification";
			String channelId = "default";
			



			// Attempt to extract the "message" property from the payload: {"message":"Hello World!"}
			if (intent.GetStringExtra("message") != null)
			{
				notificationText = intent.GetStringExtra("message");
			}

			if (intent.GetStringExtra("title") != null)
			{
				messageTitle = intent.GetStringExtra("title");
			}

			App.apiMsg = notificationText;

			// Prepare a notification with vibration, sound and lights
			var builder = new NotificationCompat.Builder(context1, channelId)
				  .SetAutoCancel(true)
				  .SetSmallIcon(Resource.Drawable.icon)
				  .SetPriority(NotificationManagerCompat.ImportanceHigh)
				  .SetContentTitle("New File by " + messageTitle + " .Tap for Details")
				  .SetContentText(notificationText)
				  //.SetLights(Color.Red, 1000, 1000)
				  //.SetVibrate(new long[] { 0, 400, 250, 400 })
				  .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
				  .SetContentIntent(PendingIntent.GetActivity(context1, 0, new Intent(context1, typeof(MainActivity)), PendingIntentFlags.UpdateCurrent));

			// Automatically configure a Notification Channel for devices running Android O+
			Pushy.SetNotificationChannel(builder, context1);

			// Get an instance of the NotificationManager service
			var notificationManager = (NotificationManager)context1.GetSystemService(Context.NotificationService);

			// Build the notification and display it
			notificationManager.Notify(new Random().Next(), builder.Build());
		}
	}
}