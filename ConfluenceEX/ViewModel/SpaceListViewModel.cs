using ConfluenceRestClient.Model;

using System.ComponentModel.Design;
using ConfluenceEX.Main;
using System;
using System.Collections.ObjectModel;
using ConfluenceRESTClient.Service;
using ConfluenceRESTClient.Service.Implementation;
using ConfluenceEX.Command;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;

namespace ConfluenceEX.ViewModel
{
    public class SpaceListViewModel : ViewModelBase
    {
        private ISpaceService _spaceService;

        private ConfluenceToolWindowNavigatorViewModel _parent;

        private ObservableCollection<Space> _spaceList;

        public DelegateCommand SpaceSelectedCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public SpaceListViewModel(string username, string password, ConfluenceToolWindowNavigatorViewModel parent)
        {
            this._spaceService = new SpaceService();

            this._parent = parent;
            this.SpaceList = new ObservableCollection<Space>();

            this.SpaceSelectedCommand = new DelegateCommand(OnItemSelected);
            OleMenuCommandService service = ConfluencePackage.Mcs;
            InitializeCommands(service);

            GetSpacesAsync();

            this.SpaceList.CollectionChanged += this.OnCollectionChanged;    
        }

        private async void GetSpacesAsync()
        {
            System.Threading.Tasks.Task<SpaceList> spaceTask = this._spaceService.GetAllSpacesAsync();

            var spaceList = await spaceTask as SpaceList;
            
            foreach(Space s in spaceList.Results)
            {
                this.SpaceList.Add(s);
            }  
        }

        private async void RefreshSpacesAsync(object sender, EventArgs e)
        {
            System.Threading.Tasks.Task<SpaceList> spaceTask = this._spaceService.GetAllSpacesAsync();

            var spaceList = await spaceTask as SpaceList;

            this.SpaceList.Clear();

            foreach (Space s in spaceList.Results)
            {
                this.SpaceList.Add(s);
            }
        }

        private void OnItemSelected(object sender)
        {
            Space space = sender as Space;

            this._parent.ShowSpaceContent(space);
        }

        private void InitializeCommands(OleMenuCommandService service)
        {
            if (service != null)
            {
                CommandID toolbarMenuCommandRefreshID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.COMMAND_REFRESH_ID);

                MenuCommand onToolbarMenuCommandRefreshClick = new MenuCommand(RefreshSpacesAsync, toolbarMenuCommandRefreshID);

                service.RemoveCommand(service.FindCommand(toolbarMenuCommandRefreshID));
                service.AddCommand(onToolbarMenuCommandRefreshClick);
            }
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }

        public ObservableCollection<Space> SpaceList {
            get { return this._spaceList; }
            set { this._spaceList = value; }
        }

    }
}