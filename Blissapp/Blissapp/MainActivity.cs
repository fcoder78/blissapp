using Android.App;
using Android.OS;
using Android.Support.V7.App;
using System;
using System.Net.Http;
using Android.Content;


namespace Blissapp
{
    [Activity(Label = "Blissapp", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class MainActivity : AppCompatActivity
    {
        Android.App.ProgressDialog progress;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //get health status from API           
            checkHealth();           
        }
        
        private async void checkHealth()
        {
            try
            {
                var baseAddress = new Uri("https://private-anon-b578fa752f-blissrecruitmentapi.apiary-mock.com/health");

                using (var httpClient = new HttpClient { BaseAddress = baseAddress })
                {
                    progress = new Android.App.ProgressDialog(this);
                    progress.Indeterminate = true;
                    progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
                    progress.SetMessage("Loading...");
                    progress.SetCancelable(false);
                    progress.Show();
                    HttpResponseMessage response = await httpClient.GetAsync(httpClient.BaseAddress);
                   
                    if (response.IsSuccessStatusCode)
                    {
                        progress.Dismiss();
                        var listScreen = new Intent(this, typeof(ListScreen));
                        StartActivity(listScreen);
                    }
                    else
                    {
                        progress.Dismiss();                        
                        var Retry = new Intent(this, typeof(RetryScreen));
                        StartActivity(Retry);
                    }

                }
            }
            catch(Exception ex)
            {
                return;
            }

           


        }
        
    }
}
    