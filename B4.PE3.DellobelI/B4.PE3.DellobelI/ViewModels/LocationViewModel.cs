using FluentValidation;
using System;
using FreshMvvm;
using B4.PE3.DellobelI.Domain.Models;
using B4.PE3.DellobelI.Domain.Validators;
using B4.PE3.DellobelI.Domain.Services.Abstract;
using System.Globalization;
using System.Windows.Input;
using Xamarin.Forms;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace B4.PE3.DellobelI.ViewModels
{
    public class LocationViewModel : FreshBasePageModel
    {
        private Location currentLocation;
        private ILocationsService locationsService;
        private LocationItemValidator locationValidator;
        private ILocationGroupsService locationGroupsService;

        public LocationViewModel(ILocationsService locationsService,ILocationGroupsService locationGroupsService )
        {
            this.locationsService = locationsService;
            this.locationGroupsService = locationGroupsService;
            locationValidator = new LocationItemValidator();
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

        private ObservableCollection<Location> imLocation;
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
            get { return !string.IsNullOrWhiteSpace(locationNameError); }
        }

        private string latitude = string.Empty;
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


        private string timeLocation;
        public string TimeLocation
        {
            get { return timeLocation; }
            set
            {
                timeLocation = value;
                RaisePropertyChanged(nameof(TimeLocation));
            }
        }

        #endregion

        public override void Init(object initData)
        {
            Location location = initData as Location;
            currentLocation = location;
           
                PageTitle = $"{currentLocation.LocationName} ";
          

            LoadLocationState();
            base.Init(initData);
        }

        private void LoadLocationState()
        {
           
            LocationName = currentLocation.LocationName;
            Latitude = currentLocation.Latitude.ToString();
            Longitude = currentLocation.Longitude.ToString();
            TimeLocation = currentLocation.TimeLocation.ToString(" d MMMM yyyy HH: mm uur", CultureInfo.CurrentCulture);
        }

        private void SaveItemState()
        {
            currentLocation.LocationName = LocationName;
            currentLocation.Latitude = Convert.ToDouble(Latitude);
            currentLocation.Longitude = Convert.ToDouble(Longitude);
            //currentLocation.TimeLocation = Convert.ToDateTime(TimeLocation);
            
        }

        public ICommand SaveLocationCommand => new Command(
            async () => {
                try
                {
                    SaveItemState();

                    //if (Validate(currentLocation))
                    //{
                        //if (currentLocation.Id == Guid.Empty)
                        //{
                            currentLocation.LocationGroup.LocationItems.Add(currentLocation);
                            currentLocation.LocationId = Guid.NewGuid();
                        //}

                        MessagingCenter.Send(this,
                       Constants.MessageNames.LocationSaved, currentLocation);
                        //use coremethodes to Pop pages in FreshMvvm!
                        await CoreMethods.PopPageModel(currentLocation, false, true);
                    //}

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
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
     


    }
}