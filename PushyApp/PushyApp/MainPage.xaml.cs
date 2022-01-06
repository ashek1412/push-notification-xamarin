using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PushyApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            if (!String.IsNullOrEmpty(App.apiMsg))
            {
                pageTitle.Text = "New File is Submitted";
                Pengingecufile.Text = App.apiMsg;
            }
            else if (!String.IsNullOrEmpty(App.stoken))
            {
                pageTitle.Text = "Device ID:";
                Pengingecufile.Text = App.stoken;

            }


            //pageTitle.Text=DependencyService.Get<IMyInterface>().GetPlatformName().ToString();
        }


        void startButtonClicked(object sender, EventArgs e)
        {

            DependencyService.Get<IMyInterface>().StartService();
        }


        void stopButtonClicked(object sender, EventArgs e)
        {

            DependencyService.Get<IMyInterface>().StopService();
        }

    }
}
