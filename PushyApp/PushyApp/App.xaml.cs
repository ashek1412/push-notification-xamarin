using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PushyApp
{
    public partial class App : Application
    {
        public static string apiMsg;
        public static string stoken;
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
