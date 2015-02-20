# XamarinMultiChoiceList
Multi choice list with advanced features and working sample for Xamarin.Android

# How to use
Copy adapter_list_multi.cs from main folder and adapter_list_multi.axml from Resources/layout folder
and access it with namespace using these functions

MultiAdapter = new Adapter_List_Multi(context, items, SelectAllButton, SelectAll);
list.Adapter = MultiAdapter; 

to access selected items use

string SelectedItems = MultiAdapter.SelectedItemsToString(", "); // will output something like "potatoes, onions..."
int[] Positions = MultiAdapter.SelectedPositions; // will put selected items positions in array of integer




Check sample for more details and enjoy!

# Screenshots

![alt tag](https://github.com/TimotejRybar/XamarinMultiChoiceList/blob/master/Screenshots/Screenshot_2015-02-20-21-40-27.png)
