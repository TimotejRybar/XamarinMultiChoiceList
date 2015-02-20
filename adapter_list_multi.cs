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

namespace MultiChoiceList
{
    public class Adapter_List_Multi : BaseAdapter<string>
    {

        const string SELECT_ALL = "Select All"; // Edit to fit your target language

        protected List<string> Choices = new List<string> ();
        protected bool[] Statuses;

        bool CheckAllButton;

        Activity context;

        public Adapter_List_Multi(Activity context, List<string> Choices, bool CheckAllButton = false, bool AllChecked = false) : base()
        {
            this.context = context;
            this.Choices = Choices;

            this.CheckAllButton = CheckAllButton; // Will add Select All button to top of your list

            if (this.CheckAllButton)
            {
                this.Choices.Insert(0, SELECT_ALL);
                this.Statuses = new bool[this.Choices.Count - 1];

            } else this.Statuses = new bool[this.Choices.Count];


            for (int i = 0; i < this.Statuses.Length; i++)
            {
                if (AllChecked)
                    this.Statuses[i] = true;
                else
                    this.Statuses[i] = false;
            }


        }

        public string SelectedItemsToString(string spliter)
        {
            string s = "";

            if (CheckAllButton)
            {
                for (int i = 0; i < Statuses.Length; i++)
                {
                    if (Statuses[i])
                        s += Choices[i + 1] + spliter;
                }
            }
            else
            {
                for (int i = 0; i < Statuses.Length; i++)
                {
                    if (Statuses[i])
                        s += Choices[i] + spliter;
                }
            }

            if (s.EndsWith(spliter))
                s = s.Remove(s.Length - spliter.Length, spliter.Length);

            return s;
        }

        public int[] SelectedPositions
        {
            get
            {
                List<int> indexes = new List<int>();

                if (CheckAllButton == false)
                {
                    for (int i = 0; i < Statuses.Length; i++)
                    {
                        if (Statuses[i])
                            indexes.Add(i);
                    }
                }
                else
                {
                    for (int i = 0; i < Statuses.Length; i++)
                    {
                        if (Statuses[i])
                            indexes.Add(i);
                    }
                }
                    
                return indexes.ToArray();
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }
        public override string this[int position]
        {
            get { return Choices[position]; }
        }
        public override int Count
        {
            get { return Choices.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
                convertView = context.LayoutInflater.Inflate (Resource.Layout.adapter_list_multi, null);
                CheckBox Status = convertView.FindViewById<CheckBox> (Resource.Id.adapter_list_multi_status);

                convertView.Click += (sender, e) =>
                {

                    if (CheckAllButton == true)
                    {
                        if(position == 0)
                        {
                            for (int i = 0; i < Statuses.Length; i++)
                            {
                                Statuses[i] = true;
                            }
                        }
                        else if(position > 0)
                        {
                            if(Statuses[position - 1]) 
                            {
                                Status.Checked = false;
                                Statuses[position - 1] = false;
                            }
                            else
                            {
                                Status.Checked = true;
                                Statuses[position - 1] = true;
                            }
                        }
                    }
                    else 
                    {
                        if(Statuses[position] == true) 
                        {
                            Status.Checked = false;
                            Statuses[position] = false;
                        }
                        else
                        {
                            Status.Checked = true;
                            Statuses[position] = true;
                        }
                    }

                    this.NotifyDataSetChanged();
                };

                Status.Click += (sender, e) => 
                {
                    if(CheckAllButton == true)
                    {
                        if(position > 0)
                        {
                            if(Status.Checked) Statuses[position - 1] = true;
                            else Statuses[position - 1] = false;
                        }
                    }
                    else
                    {
                        if(Status.Checked) Statuses[position] = true;
                        else Statuses[position] = false;
                    }

                    this.NotifyDataSetChanged();
                };

             
                    

            convertView.FindViewById<TextView> (Resource.Id.adapter_list_multi_title).Text = Choices[position];

            if (CheckAllButton == true && position == 0)  Status.Visibility = ViewStates.Invisible;
            else Status.Visibility = ViewStates.Visible;

            if (CheckAllButton)
            {
                if (position > 0)
                {
                    if (Statuses[position - 1]) // select all workaround
                    {
                        Status.Checked = true;
                    }
                    else
                    {
                        Status.Checked = false;
                    }
                }
            }
            else
            {
                if (Statuses[position])
                    Status.Checked = true;
                else
                    Status.Checked = false;
            }


           

            return convertView;
        }
            
    }
}

