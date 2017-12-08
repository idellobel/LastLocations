using B4.PE3.DellobelI.Domain.Models;
using B4.PE3.DellobelI.ViewModels;
using Xamarin.Forms;

namespace B4.PE3.DellobelI.Pages
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationGroupItemPage: ContentPage
    {
       
        public LocationGroupItemPage()
        {
            InitializeComponent();
          
        }
        protected override void OnAppearing()
        {
            MessagingCenter.Subscribe<LocationGroupItemViewModel, LocationGroup>(this, Constants.MessageNames.LocationGroupSaved,
               async (LocationGroupItemViewModel sender, LocationGroup savedLocationGroup) =>
               {
                   await DisplayAlert("Bewaard", $"{savedLocationGroup.Title} is bewaard", "Ok");
               });

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<LocationGroupItemViewModel, LocationGroup>(this, Constants.MessageNames.LocationGroupSaved);

            base.OnDisappearing();
        }
    }
}