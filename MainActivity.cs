using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using System.Collections.Generic;

namespace MultiChoiceList
{
    [Activity(Label = "MultiChoiceList", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Adapter_List_Multi MultiAdapter, DialogAdapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            ListView list = FindViewById<ListView>(Resource.Id.main_list); // Find our ListView

            List<string> items = new List<string>(); // create List and fill it with ingredients

            items.Add("Potato");
            items.Add("Apple");
            items.Add("Lemon");
            items.Add("Banana");
            items.Add("Butter");
            items.Add("Peanuts");
            items.Add("Ham");
            items.Add("Mustard");
            items.Add("Beer");

            List<string> items2 = items.GetRange(0, items.Count); // copy first list



            MultiAdapter = new Adapter_List_Multi(this, items, true, false); // create multi choice list adapter with "Select All" button
            DialogAdapter = new Adapter_List_Multi(this, items2, false, false); // create checked multi choice list adapter


            list.Adapter = MultiAdapter; // set adapter

            FindViewById<Button>(Resource.Id.main_update).Click += delegate
            {
                string SelectedItems = MultiAdapter.SelectedItemsToString(", "); // build string from selected items

                FindViewById<TextView>(Resource.Id.main_items).Text = "Selected items: " + SelectedItems; // set our textview's text to our selected items string
            };

            FindViewById<Button>(Resource.Id.main_button).Click += delegate
            {
                Dialog d = new Dialog(this); // initialize dialog creation
                d.RequestWindowFeature((int)WindowFeatures.NoTitle); // remove ugly action bar from dialog

                d.SetCancelable(true);
                d.SetContentView(Resource.Layout.dialog);

                d.FindViewById<ListView>(Resource.Id.dialog_list).Adapter = DialogAdapter; // set adapter

                d.DismissEvent += delegate // call when dialog is dismissed
                {
                    Toast.MakeText(this, "Selected items: " + DialogAdapter.SelectedItemsToString(", "), ToastLength.Short).Show(); // display selected items in toast
                    Toast.MakeText(this, "Selected ids: " + string.Join(", ", DialogAdapter.SelectedPositions), ToastLength.Short).Show(); // show selected items indexes in toast

                };

                d.Show();
            };

        }
    }
}


