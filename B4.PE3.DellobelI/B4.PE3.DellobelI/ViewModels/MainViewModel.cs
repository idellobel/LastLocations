
using FreshMvvm;
using B4.PE3.DellobelI.Domain.Models;
using B4.PE3.DellobelI.Domain.Validators;
using Plugin.Geolocator; // Geolocatie service
using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using B4.PE3.DellobelI.Domain.Services.Abstract;

namespace B4.PE3.DellobelI.ViewModels
{
    public class MainViewModel : FreshBasePageModel
    {
        private Location currentLocation;
        private LocationItemValidator locationValidator;
        IAppSettingsService settingsService;
        ILocationsService locationsService;

        public MainViewModel(ILocationsService locationsService,IAppSettingsService settingsService)
        {
            
            this.locationsService = locationsService;
            this.settingsService = settingsService;
            locationValidator = new LocationItemValidator();
            Inactive = false;


        }

        #region Properties


        private string pageTitle;
        public string PageTitle
        {
            get { return pageTitle; }
            set
            {
                pageTitle = value;
                RaisePropertyChanged(nameof(pageTitle));
            }
        }

        private bool isEnabled ;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                RaisePropertyChanged(nameof(IsEnabled));
            }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                RaisePropertyChanged(nameof(IsBusy));
            }
        }

        private bool inactive;
        public bool Inactive
        {
            get
            {
                return inactive;
            }
            set
            {
                //if (inactive != value)
                //{
                //    inactive = value;
                //    ((Command)SaveCurrentLocationCommand).ChangeCanExecute();
                inactive = value;
                   RaisePropertyChanged(nameof(Inactive));
                //}
            }
        }

        // latitude en longitude in string == gemakkelijker geen waarde meegeven bij opstart.
        private string latitude = string.Empty ;
        public string Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                RaisePropertyChanged(nameof(Latitude));
            }
        }

        private string longitude = string.Empty;
        public string Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                RaisePropertyChanged(nameof(Longitude));
            }
        }

        //universele tijd aangepast naar locale tijd in geconverteerd formaat== dag(cijfer) maand(letters) jaar(cijf.voll.) uur(24) min en sec  
        private static string initTime = DateTime.UtcNow.ToLocalTime().ToString(" d MMMM yyyy HH: mm uur", CultureInfo.CurrentCulture);


        private string timeLocation = initTime;
        public string TimeLocation
        {
            get { return timeLocation; }
            set
            {
                timeLocation = value;
                RaisePropertyChanged(nameof(TimeLocation));
            }
        }

        private string locationName;
        public string LocationName
        {
            get { return locationName; }
            set
            {
                locationName = value;
                RaisePropertyChanged(nameof(LocationName));
            }
        }

        private string locationNameError;
        public string LocationNameError
        {
            get { return locationNameError; }
            set
            {
                locationNameError = value;
                RaisePropertyChanged(nameof(LocationNameError));
                RaisePropertyChanged(nameof(LocationNameErrorVisible));
            }
        }

        public bool LocationNameErrorVisible
        {
            get { return !string.IsNullOrWhiteSpace(LocationNameError); }
        }

        #endregion Prperties

        private async Task RetreiveLocationAsync() // async taak tot ophalen van de locatie
        {
            
            IsBusy = true;

            //Voor manuele invoer UWP schakel locator uit:
            var locator = CrossGeolocator.Current; // implementatie huidige locatie met Plugin.Geolocator
            locator.DesiredAccuracy = 20; // Gewenste nauwkeurigheid op 20 meter

            try
            {

                var position = await locator.GetPositionAsync(); // Task bepaling positie


                Latitude = position.Latitude.ToString();
                Longitude = position.Longitude.ToString();

                //Manueel positie invoeren op UWP

                //Latitude = "50.895170";
                //Longitude = "4.341302";


                TimeLocation = DateTime.UtcNow.ToLocalTime().ToString(" d MMMM yyyy HH:mm:ss",CultureInfo.CurrentCulture );

                Map MyMap = new Map();

                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Convert.ToDouble(Latitude), Convert.ToDouble(Longitude)),
                 Distance.FromKilometers(1)));

                //Werking met Android-emulator:
                //Kolom naast emulator, druk '...(more), linksboven Location => scherm Longitude/Latitude, druk SEND. 
               

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
            Inactive = true;
            IsBusy = false;
            

        }


        public ICommand GetCurrentLocationCommand => new Command(
            async () => {
                            await RetreiveLocationAsync();
                            IsEnabled = false;
                         }
            );

               

        private void EmptyLocationState()
        {
            IsEnabled = true;
            Latitude = string.Empty;
            Longitude = string.Empty;
            LocationName = string.Empty;
            LocationNameError = string.Empty;
            TimeLocation = initTime;
         
        }


        private  void SaveLocationState()
        {
            Inactive = false;
            currentLocation = new Location();
            currentLocation.LocationId = Guid.NewGuid();
            currentLocation.Latitude = Convert.ToDouble(Latitude);
            currentLocation.Longitude = Convert.ToDouble(Longitude);
            currentLocation.TimeLocation = Convert.ToDateTime(TimeLocation);
            currentLocation.LocationName = LocationName;
            IsEnabled = false;
            Inactive = true;
        }

      
        public ICommand SaveCurrentLocationCommand =>
         new Command(
            async  () =>
             {
                 try

                 {
                     SaveLocationState();

                     if (Validate(currentLocation))
                     {
                         await locationsService.Save(currentLocation);
                         await CoreMethods.PushPageModel<LocationGroupViewModel>(currentLocation, false, true);
                        
                         EmptyLocationState();
                     }
                     
                 }
                 catch (Exception ex)
                 {
                     Debug.WriteLine(ex.Message);
                     
                     MessagingCenter.Send(this,
                        Constants.MessageNames.LocationSaveFail, currentLocation);

                     await CoreMethods.PopPageModel(false, true);

                 }



             }
             );

        private bool Validate(Location location)
        {
            var validationResult = locationValidator.Validate(location);
            //loop through error to identify properties
            foreach (var error in validationResult.Errors)
            {
                if (error.PropertyName == nameof(location.LocationName))
                {
                   LocationNameError = error.ErrorMessage;
                }
            }
            return validationResult.IsValid;
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            IsEnabled = true;
            EmptyLocationState();
            currentLocation = null;
        }

       



    }
            
   
}
