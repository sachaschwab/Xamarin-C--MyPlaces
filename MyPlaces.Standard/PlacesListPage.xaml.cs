﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPlaces.Standard.Data;
using Xamarin.Forms;
using System.Linq;
using MyPlaces.Standard.ViewModels;

namespace MyPlaces.Standard
{
    public partial class PlacesListPage : ContentPage
    {
        MapPage mapPage = new MapPage();
        private Data.DataAccessLayer AccessLayer = new Data.DataAccessLayer();

        public PlacesListPage()
        {
            InitializeComponent();
            // TODO: Dummy => get Category Name from Transition from Category List Page
            Title = "Places";
            // Put in padding if iOS
            if (Device.RuntimePlatform == Device.iOS)
                Padding = new Thickness(0, 20, 0, 0);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // TODO: Decide whehter to keep the Button bar & picker feature. otherwise, erase these two lines
            CategoryButton.IsVisible = true;
            CategoryPicker.IsVisible = false;
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {

            var place = args.SelectedItem as Data.Place;
            if (place == null)
                return;

            // Provide selected place ID to App "dispatch".
            ((App)App.Current).SelectedPlaceId = place.PlaceId;

            var mainPage = this.Parent as TabbedPage;
            var newPhotoPage = mainPage.Children.OfType<NewPhotoPage>().First();
            NewPhotoViewModel vm = (NewPhotoViewModel)newPhotoPage.BindingContext;
            await vm.SetPlace(place.PlaceId.Value);
            mainPage.CurrentPage = newPhotoPage;
        }

        void OnButtonClicked(object sender, EventArgs args)
        {
            CategoryPicker.IsVisible = false;
            CategoryButton.IsVisible = true;
            CategoryPicker.Focus();
        }
    }
}
