using Android.OS;
using RestSharp;
using System;
using System.Net;



namespace XamarinApp.Droid.BAL
{
    class WebApi
    {


        public void SaveToken(string deviceToken)
        {
            try
            {
              
                // Device Model (SMG-950U, iPhone10,6)
                 var deviceModel = Build.Model;

                // Manufacturer (Samsung)
                var manufacturer = Build.Manufacturer;

                // Device Name (Motz's iPhone)
                var deviceName = Build.Device;

                // Operating System Version Number (7.0)
                var version = Build.VERSION.Release;

                // Platform (Android)
                var deviceType = Build.Type;

                
                


                var client = new RestClient("https://www.ecu-manager.com/magic/api/update_device_token");
                var request = new RestRequest(Method.POST);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("device_name", deviceName);
                request.AddParameter("device_model", deviceModel);
                request.AddParameter("device_type", deviceType);
                request.AddParameter("device_version", version);               
                request.AddParameter("device_token", deviceToken);
                request.AddParameter("device_brand", manufacturer);

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                IRestResponse response = client.Execute(request);

                // T serializer = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
                //String dynObj = serializer.Deserialize<String>(response.Content);\
                //var dynObj = response.Content;
                //if (dynObj == "1")
                //{

                //  return 1;
                //}
                //else
                //  return 0;
            }
            catch (Exception ex)
            {

                //return 0;
            }


        }
    }
}