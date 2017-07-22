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
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace Blissapp
{
    public class BlissAPI
    {
        public class Choice
        {
            public string choice { get; set; }
            public int votes { get; set; }
        }

        public class Question
        {
            public int id { get; set; }
            public string question { get; set; }
            public string image_url { get; set; }
            public string thumb_url { get; set; }
            public string published_at { get; set; }
            public List<Choice> choices { get; set; }
        }

    }
}