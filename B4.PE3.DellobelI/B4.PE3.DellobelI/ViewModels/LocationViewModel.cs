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
using B4.PE3.DellobelI.Domain.Services.Mock;

namespace B4.PE3.DellobelI.ViewModels
{
    public class LocationViewModel : FreshBasePageModel
    {
        private Location location;
        private Location currentLocation;
        private ILocationsService locationsService;
        private LocationItemValidator locationValidator;
        private ILocationGroupsService locationGroupsService;
        //private ObservableCollection<Location> imLocation;

        public LocationViewModel(ILocationsService locationsService, ILocationGroupsService locationGroupService )
        {
            this.locationsService = locationsService;
            this.locationGroupsService = locationGroupService;
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

        private bool isEnabled;
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

        private async Task GetLocation()
        {

            //var imGroup = await locationGroupsService.GetLocationGroupList(currentLocation.LocationGroupId);
            
            var imLoc = await locationsService.GetRecentstLocation();
            location = new Location();
            location.LocationName = imLoc.LocationName;
            location.Latitude = imLoc.Latitude;
            location.Longitude = imLoc.Longitude;
            location.TimeLocation = imLoc.TimeLocation;
        }


        public override void Init(object initData)
        {
            Location initLocation = initData as Location;
            currentLocation = initLocation;
           
            if (initLocation.LocationId == Guid.Empty)
            {
                PageTitle ="Nieuwe Locatie";
            }
            else
            {
                PageTitle = $"{currentLocation.LocationName}";
            }
            LoadLocationState();
            base.Init(initData);
        }

        private void LoadLocationState()
        {
            GetLocation().Wait();
            LocationName = location.LocationName;
            Latitude = location.Latitude.ToString();
            Longitude = location.Longitude.ToString();
            TimeLocation = location.TimeLocation.ToString(/*" d MMMM yyyy HH: mm uur", CultureInfo.CurrentCulture*/);
            
        }

        private void SaveItemState()
        {
            currentLocation.LocationName = LocationName;
            currentLocation.Latitude = Convert.ToDouble(Latitude);
            currentLocation.Longitude = Convert.ToDouble(Longitude);
            currentLocation.TimeLocation = Convert.ToDateTime(TimeLocation);
          
        }
        /// <summary>
        /// Bewaar Locatie aan locatielijst.
        /// </summary>

        public ICommand SaveLocationCommand => new Command(
            async () => {
                try
                {
                    SaveItemState();

                    
                        if (currentLocation.LocationId == Guid.Empty)
                        {
                            currentLocation.LocationGroup.LocationItems.Add(currentLocation);
                            currentLocation.LocationId = Guid.NewGuid();
                        }
                    MessagingCenter.Send(this,
                    Constants.MessageNames.LocationSaved, currentLocation);

                    //use coremethodes to Pop pages in FreshMvvm!
                    await CoreMethods.PopPageModel(currentLocation, false, true);
                   
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            }
        );
    }
}