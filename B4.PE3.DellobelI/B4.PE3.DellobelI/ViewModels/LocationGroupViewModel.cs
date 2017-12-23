using FreshMvvm;
using System;
using B4.PE3.DellobelI.Domain.Models;
using B4.PE3.DellobelI.Domain.Services.Mock;
using B4.PE3.DellobelI.Pages;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using B4.PE3.DellobelI.Domain.Services.Abstract;
using System.Threading.Tasks;
using System.Diagnostics;

namespace B4.PE3.DellobelI.ViewModels
{
    public class LocationGroupViewModel : FreshBasePageModel
    {

        //private Location currentLocation;
        //ILocationsService locationsService;
        private IAppSettingsService settingsService;
        private ILocationGroupsService locationGroupsService;
        

        public LocationGroupViewModel(/*ILocationsService locationsService,*/ IAppSettingsService settingsService, ILocationGroupsService locationGroupsService)
        {
            
            //this.locationsService = locationsService;
            this.settingsService = settingsService;
            this.locationGroupsService = locationGroupsService;

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


        private ObservableCollection<LocationGroup> locationGrouplist;
        public ObservableCollection<LocationGroup> LocationGrouplist
        {
            get { return locationGrouplist; }
            set
            {
                locationGrouplist = value;
                RaisePropertyChanged(nameof(LocationGrouplist));
            }
        }

        public ICommand OpenLocationGroupItemPageCommand => new Command<LocationGroup>(
          async (LocationGroup locationGroup) =>
            {
               await CoreMethods.PushPageModel<LocationGroupItemViewModel>(locationGroup, false, true);
            }
        );



        public ICommand OpenSettingsPageCommand => new Command(
             async() =>
             {
                 await CoreMethods.PushPageModel<SettingsViewModel>(true);
            }
        );

        public ICommand BackToMainPageCommand => new Command(
             async () =>
             {
                 await CoreMethods.PushPageModel<MainViewModel>(true);
             }
        );

        public ICommand OpenMapCommand => new Command<LocationGroup>(
             async (LocationGroup locatiegroep) =>
             {
                 await CoreMethods.PushPageModel<MapViewModel>(locatiegroep,false,true);
             }
        );


        public ICommand DeleteLocationGroupCommand => new Command<LocationGroup>(
          async (LocationGroup locatiegroep) => {
              await locationGroupsService.DeleteLocationGroupList(locatiegroep.Id);
              await RefreshLocationGroupList();
          }
      );

       

        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            await RefreshLocationGroupList();
        }

        private async Task RefreshLocationGroupList()
        {
            IsBusy = true;
            //get settings, because we need current user Id
            var settings = await settingsService.GetSettings();
            //get all locationGrouplist for this user
            var locationGrouplist = await locationGroupsService.GetLocationGrouplistForUser(settings.CurrentUserId);
            //bind IEnumerable<LocationGroup> to the ListView's ItemSource
            LocationGrouplist = null;    //Important! ensure the list is empty first to force refresh!
            LocationGrouplist = new ObservableCollection<LocationGroup>(locationGrouplist);
            IsBusy = false;
        }
       

    }

  
}
