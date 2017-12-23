using System;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using B4.PE3.DellobelI.ViewModels;
using B4.PE3.DellobelI.Domain.Models;

namespace B4.PE3.DellobelI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
         
         
        }


        protected override void OnAppearing()
        {
            MessagingCenter.Subscribe<MainViewModel, Location>(this, Constants.MessageNames.LocationSaveFail,
               async (MainViewModel sender, Location currentlocation) => {
                   await DisplayAlert("KAN NIET OPSLAAN", $"Coördinaten dienen eerst opgehaald", "Ok");
               });

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<MainViewModel, Location>(this, Constants.MessageNames.LocationSaveFail);

            base.OnDisappearing();
        }

    }
}