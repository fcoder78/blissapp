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

namespace Blissapp
{
    [Activity(Label = "RetryScreen", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class RetryScreen : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RetryScreen);

            var btn = FindViewById<CircularPulsingButton>(Resource.Id.btnRetry);
            btn.Click += delegate
            {                
                var Main = new Intent(this, typeof(MainActivity));
                StartActivity(Main);
            };
        }
    }
}