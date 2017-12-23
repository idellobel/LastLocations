using FreshMvvm;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;
using Plugin.Geolocator; // Geolocatie service
using Xamarin.Forms;
using B4.PE3.DellobelI.Domain.Models;
using B4.PE3.DellobelI.Domain.Services.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace B4.PE3.DellobelI.ViewModels
{
    public class MapViewModel : FreshBasePageModel
    {

        private LocationGroup currentLocationGroup;
        private ILocationGroupsService locationGroupsService;
        private IAppSettingsService settingsService;
        //private IEnumerable<Location> pinnedLocations;


        public MapViewModel(ILocationGroupsService locationGroupsService, IAppSettingsService appSettingsService)
        {
            this.locationGroupsService = locationGroupsService;
            settingsService = appSettingsService;
        }
        #region Properties

        private ObservableCollection<Pin> pinCollection = new ObservableCollection<Pin>();
        public ObservableCollection<Pin> PinCollection
        {
            get { return pinCollection; }
            set
            { pinCollection = value;
                RaisePropertyChanged();
            }
        }
        private string pinLabel;
        public string PinLabel
        {
            get { return pinLabel; }
            set
            {
                pinLabel = value;
                RaisePropertyChanged();
            }
        }


        private Position myPosition;
        

        public Position MyPosition
        {
            get { return myPosition; }
            set
            {
                myPosition = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        private async Task PinLocationsAsync() //plaats pins
        {
            await Task.Run(() =>
             {
                 foreach (Location pinnedLocation in currentLocationGroup.LocationItems)
                 {
                     MyPosition = new Position(pinnedLocation.Latitude, pinnedLocation.Longitude);
                     PinLabel = $"{pinnedLocation.LocationName} op {pinnedLocation.TimeLocation.ToString(" d MMMM yyyy HH:mm:ss uur", CultureInfo.CurrentCulture)}";
                     PinCollection.Add(new Pin() { Position = MyPosition, Type = PinType.Generic, Label = PinLabel });
                 }

             });
        }

        public override void Init(object initData)
        {
            LocationGroup locationGroup = initData as LocationGroup;
            currentLocationGroup = locationGroup;
            base.Init(initData);
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            await PinLocationsAsync();
        }

      }

    }



