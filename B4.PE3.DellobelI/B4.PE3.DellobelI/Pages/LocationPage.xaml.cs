
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
                   await DisplayAlert($"{savedLocation.LocationName} Bewaard", $"{savedLocation.LocationName} is toegevoegd aan lijst.\n" +
                       $"Terug Locatielijst = Backtoets of Oplag-icoon.\n\n" +
                       $"Bewaar ook in de Locatielijst, in dit geval:\n\n" +
                       $"{savedLocation.LocationGroup.Title}!", "Ok");
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