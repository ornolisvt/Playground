using System;
using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Views.Attributes;
using Playground.Core.ViewModels;
using Android.Support.Design.Widget;

namespace Playground.Droid.Views
{
    [MvxTabLayoutPresentation(TabLayoutResourceId = Resource.Id.tabs, ViewPagerResourceId = Resource.Id.viewpager, Title = "Tab 1")]
    [Register(nameof(Tab1View))]
    public class Tab1View : MvxFragment<Tab1ViewModel>, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        private static string SELECTED_ITEM = "arg_selected_item";
        private int mSelectedItem;
        private BottomNavigationView _bottomNavigationView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.Tab1View, null);
            _bottomNavigationView = (BottomNavigationView)view.FindViewById(Resource.Id.bottom_navigation);
            _bottomNavigationView.SetOnNavigationItemSelectedListener(this);

            IMenuItem selectedItem;
            if (savedInstanceState != null)
            {
                mSelectedItem = savedInstanceState.GetInt(SELECTED_ITEM, 0);
                selectedItem = _bottomNavigationView.Menu.FindItem(mSelectedItem);
            }
            else
            {
                selectedItem = _bottomNavigationView.Menu.GetItem(0);
            }
            SelectFragment(selectedItem);
            return view;
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt(SELECTED_ITEM, mSelectedItem);
            base.OnSaveInstanceState(outState);
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            SelectFragment(item);
            return true;
        }

        private void SelectFragment(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_favorites:
                    ViewModel.OpenChildCommand.Execute(null);
                    break;
                case Resource.Id.action_schedules:
                    ViewModel.OpenSecondChildCommand.Execute(null);
                    break;
                case Resource.Id.action_music:
                    ViewModel.OpenNestedChildCommand.Execute(null);
                    break;
            }
            mSelectedItem = item.ItemId;
            for (var i = 0; i < _bottomNavigationView.Menu.Size(); i++)
            {
                IMenuItem menuItem = _bottomNavigationView.Menu.GetItem(i);
                menuItem.SetChecked(menuItem.ItemId == item.ItemId);
            }
        }
    }
}
