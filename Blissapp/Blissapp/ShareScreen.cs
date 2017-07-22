using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using IR.Sohreco.Circularpulsingbutton;
using System.Net.Http;

namespace Blissapp
{
    [Activity(Label = "ShareScreen", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class ShareScreen : AppCompatActivity
    {
        string mail;
        string content_url;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ShareScreen);

            var txtEmail = FindViewById<EditText>(Resource.Id.txtEmail);
            var lblink = FindViewById<TextView>(Resource.Id.tvContent);
            var btSend = FindViewById<CircularPulsingButton>(Resource.Id.btnSend);
            
            btSend.Click += async delegate {

                mail = txtEmail.Text.Trim();
                content_url = lblink.Text;
                //check if email its null or valid then send POST
                if (mail!=null && Android.Util.Patterns.EmailAddress.Matcher(mail).Matches())
                {
                    //Send POST (SHARE) to API
                    var baseAddress = new Uri("https://private-anon-b578fa752f-blissrecruitmentapi.apiary-mock.com/");

                    using (var httpClient = new HttpClient { BaseAddress = baseAddress })
                    {

                        using (var content = new StringContent(""))
                        {
                            using (var response = await httpClient.PostAsync("share?" + mail + "&" + content_url, content))
                            {
                                string responseData = await response.Content.ReadAsStringAsync();

                                if (response.IsSuccessStatusCode)
                                {
                                    Toast.MakeText(this, "Your email has been sent successfully.", ToastLength.Long).Show();
                                    this.Finish();
                                }
                                else
                                {
                                    Toast.MakeText(this, "Oops! Unable to send your email", ToastLength.Long).Show();
                                }
                            }
                        }
                    };
                }else { Toast.MakeText(this, "Invalid email address", ToastLength.Short).Show(); }
                

            };      
        }
    }
}