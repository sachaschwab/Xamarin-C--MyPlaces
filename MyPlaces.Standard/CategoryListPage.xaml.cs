﻿using MyPlaces.Standard.Data;
using Xamarin.Forms;
using System.Linq;
using MyPlaces.Standard.ViewModels;

namespace MyPlaces.Standard
{
    public partial class CategoryListPage : ContentPage
    {
        public CategoryListPage()
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
                Padding = new Thickness(0, 20, 0, 0);

            MessagingCenter.Subscribe<object, Category>(this, MessageNames.EDIT_CATEGORY, (sender, cat) => EditControl.IsVisible = true);
        }

        void Save_Clicked(object sender, System.EventArgs e)
        {
            EditControl.IsVisible = false;
        }

        void Cancel_Clicked(object sender, System.EventArgs e)
        {
            EditControl.IsVisible = false;
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            Category category = ((CategoryViewModel)e.Item).Category;
            ((App)App.Current).SelectedCategory = category;// CurrentCategoryID = category.CategoryId;
            MainPage mainPage = (MainPage)App.Current.MainPage;
            PlacesListPage placesListPage = mainPage.Children.OfType<PlacesListPage>().Single();
            PlacesViewModel vm = (PlacesViewModel)placesListPage.BindingContext;
            vm.SelectedCategory = category;
            //TODO: Sacha: I'm commenting the line below out as we might want to keep the title "Places" defined in the PlacesListPage
            //placesListPage.Title = category.Name;
            placesListPage.Title = "Places"; 
            mainPage.CurrentPage = placesListPage;
        }
    }
}
