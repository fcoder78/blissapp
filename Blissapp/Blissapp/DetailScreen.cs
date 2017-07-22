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
using Android.Graphics;
using System.Net;
using IR.Sohreco.Circularpulsingbutton;

namespace Blissapp
{
    [Activity(Label = "DetailScreen", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class DetailScreen : AppCompatActivity
    {      
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DetailScreen);

            string QID = Intent.GetStringExtra("QID") ?? "Data not available";
            string QQ = Intent.GetStringExtra("QQ") ?? "Data not available";
            string QI = Intent.GetStringExtra("QI") ?? "Data not available";
            string QT = Intent.GetStringExtra("QT") ?? "Data not available";
            string QP= Intent.GetStringExtra("QP") ?? "Data not available";


            var btShare = FindViewById<CircularPulsingButton>(Resource.Id.btnShare);
            var txtID = FindViewById<TextView>(Resource.Id.txtID);
            var txtQuestion = FindViewById<TextView>(Resource.Id.txtQuestion);
            var imageView_I = FindViewById<ImageView>(Resource.Id.imageView_I);
            var imageView_T = FindViewById<ImageView>(Resource.Id.imageView_T);
            var imageBitmap_I = GetImageBitmapFromUrl(QI);
            var imageBitmap_T = GetImageBitmapFromUrl(QT);
            

            txtID.Text = "ID: "+QID;
            txtQuestion.Text = "Q: "+QQ;
            imageView_I.SetImageBitmap(imageBitmap_I);
            imageView_T.SetImageBitmap(imageBitmap_T);

            btShare.Click += delegate
            {
                var shareScreen = new Intent(this, typeof(ShareScreen));
                StartActivity(shareScreen);
                this.Finish();

            };
            

        }
        //Get images from URL
        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }
    }
}