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
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Threading;
using static Android.Widget.AdapterView;
using Java.Util;
using Plugin.Connectivity;

namespace Blissapp
{
    [Activity(Label = "ListScreen", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class ListScreen : AppCompatActivity
    {
        private ArrayAdapter<BlissAPI.Question> adapter;
        
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ListScreen);
            var listview = FindViewById<ListView>(Resource.Id.listView);
            var searchView = FindViewById<SearchView>(Resource.Id.searchView);
            var btLoadMore = FindViewById<Button>(Resource.Id.btnLoadMore);

            searchView.QueryTextChange += sv_QueryTextChange;

            

            //GET List all Questions from API
            List<BlissAPI.Question> listQ = await listQuestions();

            //ADAPTER
            adapter = new ArrayAdapter<BlissAPI.Question>(this, Android.Resource.Layout.SimpleDropDownItem1Line);            
            adapter.Clear();

            //add ID from Questions to Adpter
            foreach(var q in listQ)
            {
                adapter.Add(q.id);
                adapter.NotifyDataSetChanged();
            }
                   
            listview.Adapter = adapter;

            if(listview.Adapter != null)
            {
                btLoadMore.Enabled = true;
            }

            btLoadMore.Click += async delegate
            {
                //GET List all Questions from API
                List<BlissAPI.Question> listQ2 = await listQuestions();
                //add more questions to listQ
                foreach (var q in listQ2)
                {
                    listQ.Add(q);                                        
                    
                }

                adapter.Clear();//clear

                //add content to adapter again
                foreach (var qid in listQ)
                {
                    adapter.Add(qid.id);
                    adapter.NotifyDataSetChanged();
                }

                listview.Adapter = adapter;
            };            

            listview.ItemClick += async (object sender, ItemClickEventArgs e) =>
            {
                //GET Retieve a Question (get question from specific ID)
                var qID = listview.GetItemAtPosition(e.Position);
                
                string qID_result = qID.ToString();
                qID_result = qID_result.Replace("{}", "");

                var baseAddress = new Uri("https://private-anon-b578fa752f-blissrecruitmentapi.apiary-mock.com/");

                using (var httpClient = new HttpClient { BaseAddress = baseAddress })
                {
                    using (var response = await httpClient.GetAsync("questions/"+qID_result))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var responseData = await response.Content.ReadAsStringAsync();

                            var question = JsonConvert.DeserializeObject<BlissAPI.Question>(responseData);

                            var detailsView = new Intent(this, typeof(DetailScreen));
                            detailsView.PutExtra("QID", question.id.ToString());
                            detailsView.PutExtra("QQ", question.question);
                            detailsView.PutExtra("QI", question.image_url);
                            detailsView.PutExtra("QT", question.thumb_url);
                            detailsView.PutExtra("QP", question.published_at);
                           
                            //send choices? 
                           
                            StartActivity(detailsView);
                            
                        }
                        
                    }
                }

            };

            

        }

        private void sv_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            //filter
            adapter.Filter.InvokeFilter(e.NewText);
        }

        //GET List all Questions from API
        public async Task<List<BlissAPI.Question>> listQuestions()
        {
            try
            {
                var baseAddress = new Uri("https://private-anon-b578fa752f-blissrecruitmentapi.apiary-mock.com/");

                using (var httpClient = new HttpClient { BaseAddress = baseAddress })
                {

                    using (var response = await httpClient.GetAsync("questions?10&10&"))
                    {
                        response.EnsureSuccessStatusCode();

                        if (response.IsSuccessStatusCode)
                        {
                            var responseData = await response.Content.ReadAsStringAsync();

                            var questions = JsonConvert.DeserializeObject<List<BlissAPI.Question>>(responseData);

                            return questions;
                        }


                        return null;

                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }


    }
}