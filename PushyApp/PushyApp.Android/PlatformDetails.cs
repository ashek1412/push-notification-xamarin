using Android.Content;
using PushyApp.Droid;
using PushyApp.Droid.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(PlatformDetails))]
namespace PushyApp.Droid
{

    public class PlatformDetails : IMyInterface
    {
        Intent intent;
        public PlatformDetails()
        {


        }
        public string GetPlatformName()
        {
            return "I am Android!";
        }


        public int StartService()
        {

            intent = new Intent(Android.App.Application.Context, typeof(NotificationsService));

            //Intent service = new Intent(AppContext, typeof(NotificationsService));
            //AppContext.StartForegroundService(service);


            // start foreground service.
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                Android.App.Application.Context.StartForegroundService(intent);
                return 1;
                // return "I am Android!";
            }
            else

                return 0;
           
        }


        public int StopService()
        {
            try
            {
                Android.App.Application.Context.StopService(intent);
                return 1;

            }
            catch (Exception ex)
            {

                return - 1;

            }
        }
    }
}