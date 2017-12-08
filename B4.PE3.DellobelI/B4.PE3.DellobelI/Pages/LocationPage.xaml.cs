
using B4.PE3.DellobelI.Domain.Models;
using B4.PE3.DellobelI.ViewModels;
using Xamarin.Forms;

namespace B4.PE3.DellobelI.Pages
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationPage : ContentPage
	{
		public LocationPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            MessagingCenter.Subscribe<LocationViewModel, Location>(this, Constants.MessageNames.LocationSaved,
               async (LocationViewModel sender, Location savedLocation) => {
                   await DisplayAlert("Bewaard", $" {savedLocation.LocationName} is toegevoegd aan lijst", "Ok");
               });

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<LocationViewModel, Location>(this, Constants.MessageNames.LocationSaved);

            base.OnDisappearing();
        }
    }
}