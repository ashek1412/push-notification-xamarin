using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ME.Pushy.Sdk;
using System.Threading.Tasks;
using Android.Util;
using XamarinApp.Droid.BAL;
using Android.Content;
using PushyApp.Droid.BAL;
using Xamarin.Forms;

namespace PushyApp.Droid
{
    [Activity(Label = "ECM Notifier", Icon = "@drawable/largicon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        WebApi wapi;
        public static Context AppContext;
       
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                TabLayoutResource = Resource.Layout.Tabbar;
                ToolbarResource = Resource.Layout.Toolbar;


                base.OnCreate(savedInstanceState);
                AppContext = this.ApplicationContext;
                Pushy.Listen(this);
                await RegisterForPushNotifications();

                Xamarin.Essentials.Platform.Init(this, savedInstanceState);
                global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

                LoadApplication(new App());

            }
            catch (Exception dex)
            {

                Device.BeginInvokeOnMainThread(async () => {
                    await App.Current.MainPage.DisplayAlert("Hello", dex.Message, "OK");
                });
            }


            //Intent service = new Intent(AppContext, typeof(NotificationsService));
            //AppContext.StartForegroundService(service);

        }
    public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public static void StartPushService()
        {
            AppContext.StartService(new Intent(AppContext, typeof(NotificationsService)));

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Kitkat)
            {
                PendingIntent pintent = PendingIntent.GetService(AppContext, 0, new Intent(AppContext, typeof(NotificationsService)), 0);
                AlarmManager alarm = (AlarmManager)AppContext.GetSystemService(Context.AlarmService);
                alarm.Cancel(pintent);
            }
        }



        private async Task RegisterForPushNotifications()
        {            // Execute Pushy.Register() in a background thread
            string deviceToken=null;                             
            await Task.Run(() =>
            {

                try
                {
                    deviceToken= Pushy.Register(this);
                    App.stoken = deviceToken;
                    wapi = new WebApi();
                    wapi.SaveToken(deviceToken);


                }
                catch (Exception exc)
                {
                    // Log error to console
                    System.Console.WriteLine("MyApp", exc.Message, exc);
                    return;
                }

                // Succeeded, optionally do something to alert the user
            });

            System.Console.WriteLine("Stoken:" + deviceToken);
        }


       







    }
}