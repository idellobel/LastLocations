using FreshMvvm;
using B4.PE3.DellobelI.Domain.Models;
using B4.PE3.DellobelI.Domain.Services.Abstract;
using B4.PE3.DellobelI.Domain.Validators;
using System;
using FluentValidation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace B4.PE3.DellobelI.ViewModels
{
    public class LocationGroupItemViewModel : FreshBasePageModel
    {
        private ILocationsService locationsService;
        private Location currentLocation;
        private IAppSettingsService settingsService;
        private ILocationGroupsService locationGroupsService;
        private IUsersService usersService;
        private LocationGroup currentLocationGroup;
        private User currentUser;
        private IValidator locationGroupValidator;

        public LocationGroupItemViewModel(
            ILocationsService locationsService,
            IAppSettingsService settingsService,
            ILocationGroupsService locationGroupsService,
            IUsersService usersService)
        {
            this.locationsService = locationsService;
            this.settingsService = settingsService;
            this.locationGroupsService = locationGroupsService;
            this.usersService = usersService;
            locationGroupValidator = new LocationGroupValidator();
        }

        //public Guid Id { get; set; }
        //public string ItemDescription { get; set; }
        //public int Order { get; set; }
        //public DateTime? CompletionDate { get; set; }
        //public Guid BucketId { get; set; }
        //public Bucket Bucket { get; set; }

        //public Guid LocationId { get; set; }
        //public double Latitude { get; set; }
        //public double Longitude { get; set; }
        //public double Altitude { get; set; }
        //public DateTime TimeLocation { get; set; }
        //public string LocationName { get; set; }
        //public int Position { get; set; }
        //public Guid LocationGroupId { get; set; }
        //public LocationGroup LocationGroup { get; set; }
        //public Guid Id { get; internal set; }


        #region Properties 

        private string pageTitle;
        public string PageTitle
        {
            get { return pageTitle; }
            set
            {
                pageTitle = value;
                RaisePropertyChanged(nameof(PageTitle));
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

        private string locationGroupTitle;
        public string LocationGroupTitle
        {
            get { return locationGroupTitle; }
            set
            {
                locationGroupTitle = value;
                RaisePropertyChanged(nameof(LocationGroupTitle));
            }
        }

        private string locationGroupTitleError;
        public string LocationGroupTitleError
        {
            get { return locationGroupTitleError; }
            set
            {
                locationGroupTitleError = value;
                RaisePropertyChanged(nameof(LocationGroupTitleError));
                RaisePropertyChanged(nameof(LocationGroupTitleErrorVisible));
            }
        }

        
        public bool LocationGroupTitleErrorVisible
        {
            get { return !string.IsNullOrWhiteSpace(LocationGroupTitleError); }
          
        }


        private string locationGroupDescription;
        public string LocationGroupDescription
        {
            get { return locationGroupDescription; }
            set
            {
                locationGroupDescription = value;
                RaisePropertyChanged(nameof(LocationGroupDescription));
            }
        }

        private string locationGroupDescriptionError;
        public string LocationGroupDescriptionError
        {
            get { return locationGroupDescriptionError; }
            set
            {
                locationGroupDescriptionError = value;
                RaisePropertyChanged(nameof(LocationGroupDescriptionError));
                RaisePropertyChanged(nameof(LocationGroupDescriptionErrorVisible));
            }
        }

        public bool LocationGroupDescriptionErrorVisible
        {
            get { return !string.IsNullOrWhiteSpace(LocationGroupDescriptionError); }
        }

        private ObservableCollection<Location> locations;
        public ObservableCollection<Location> Locations
        {
            get { return locations; }
            set
            {
                locations = value;
                RaisePropertyChanged(nameof(Locations));
            }
        }


        #endregion

        /// <summary>
        /// Callled whenever the page is navigated to.
        /// </summary>
        /// <param name="initData"></param>

        public async override void Init(object initData)
        {
           currentLocationGroup = initData as LocationGroup;
            var settings = await settingsService.GetSettings();
            currentUser = await usersService.GetUserById(settings.CurrentUserId);

            await RefreshLocationGroup();
        }

        /// <summary>
        /// Executed when returning to this Model from a previous model
        /// </summary>
        /// <param name="returnedData"></param>
        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
            if (returnedData is Location)
            {
                //refresh list, to update this item visually
                LoadLocationGroupState();
            }
        }

        /// <summary>
        /// Refreshes the currentLocationGroup (to edit) or initializes a new one (to add)
        /// </summary>
        /// <returns></returns>
        private async Task RefreshLocationGroup()
        {
            if (currentLocationGroup != null)
            {
                PageTitle = currentLocationGroup.Title;
                currentLocationGroup = await locationGroupsService.GetLocationGroupList(currentLocationGroup.Id);
            }
            else
            {
                PageTitle = "Nieuw Locatielijst"; 
                currentLocationGroup = new LocationGroup();
                currentLocationGroup.Id = Guid.NewGuid();
                currentLocationGroup.Owner = currentUser;
                currentLocationGroup.LocationItems= new List<Location>();
            }
            LoadLocationGroupState();
        }
       

        /// <summary>
        /// Loads the currentLocationGroup list properties into the VM properties for display in UI
        /// </summary>
        private void LoadLocationGroupState()
        {
            LocationGroupTitle = currentLocationGroup.Title;
            LocationGroupDescription = currentLocationGroup.Description;

            Locations = new ObservableCollection<Location>(currentLocationGroup.LocationItems);
        }

        /// <summary>
        /// Saves the VM properties back to the current locationGroup
        /// </summary>
        private void SaveLocationGroupState()
        {
            currentLocationGroup.Title = LocationGroupTitle;
            currentLocationGroup.Description = LocationGroupDescription;
        }
           

        /// <summary>
        /// Validatie van LocationGroup door validator
        /// </summary>
        /// <param name="locationGroup">locationGroup te valideren</param>
        /// <returns></returns>
        private bool Validate(LocationGroup locationGroup)
        {
            var validationResult = locationGroupValidator.Validate(locationGroup);
            //loop through error to identify properties
            foreach (var error in validationResult.Errors)
            {
                if (error.PropertyName == nameof(locationGroup.Title))
                {
                    LocationGroupTitleError = error.ErrorMessage;
                }
                if (error.PropertyName == nameof(locationGroup.Description))
                {
                    LocationGroupDescriptionError = error.ErrorMessage;
                }
            }
            return validationResult.IsValid;
        }

        public ICommand SaveLocationGroupCommand => new Command(
            async () => {
                SaveLocationGroupState();
                if (Validate(currentLocationGroup))
                {
                    IsBusy = true;
                    await locationGroupsService.SaveLocationGroupList(currentLocationGroup);
                    IsBusy = false;

                    MessagingCenter.Send(this,
                        Constants.MessageNames.LocationGroupSaved, currentLocationGroup);

                    await CoreMethods.PopPageModel(false, true);
                }
            }
        );

        public ICommand OpenLocationPageCommand => new Command<Location>(
            async (Location location) => {

                SaveLocationGroupState();
                await GetLocation();
                if (location == null)
                {
                    
                    //registratie nieuwe locatie
                    location = new Location
                    {
                        LocationName = currentLocation.LocationName ,
                        Latitude = currentLocation.Latitude,
                        Longitude = currentLocation.Longitude,
                        TimeLocation = currentLocation.TimeLocation,
                        LocationGroup = currentLocationGroup,
                        LocationGroupId = currentLocationGroup.Id
                    };
                }
                await CoreMethods.PushPageModel<LocationViewModel>(location, false, true);
            }
        );

        public ICommand DeleteLocationCommand => new Command<Location>(
            async (Location location) => {
                location.LocationGroup.LocationItems.Remove(location);
                await locationGroupsService.SaveLocationGroupList(location.LocationGroup);
                await RefreshLocationGroup();
            }
        );

        private async Task GetLocation()
        {
           
                //PageTitle = currentLocation.LocationName;
                var imLoc = await locationsService.GetFirst();
            currentLocation = new Location();
                currentLocation.LocationName = imLoc.LocationName;
                currentLocation.Latitude = imLoc.Latitude;
                currentLocation.Longitude = imLoc.Longitude;
                currentLocation.LocationId = imLoc.LocationId;
                currentLocation.TimeLocation = imLoc.TimeLocation;


        }
    }
}


