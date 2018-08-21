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
        private const int CONTENT_ID = 196609;

        private ISpaceService _spaceService;
        private ConfluenceToolWindowNavigatorViewModel _parent;

        public ObservableCollection<Space> SpaceList { get; set; }

        public DelegateCommand SpaceSelectedCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public SpaceListViewModel(string username, string password, ConfluenceToolWindowNavigatorViewModel parent)
        {
            this._spaceService = new SpaceService(username, password);
            this._parent = parent;
            this.SpaceList = new ObservableCollection<Space>();

            this.SpaceSelectedCommand = new DelegateCommand(OnItemSelected);
            OleMenuCommandService service = ConfluencePackage.Mcs;
            InitializeCommands(service);

            getSpacesAsync();

            this.SpaceList.CollectionChanged += this.OnCollectionChanged;    
        }

        private async void getSpacesAsync()
        {
            System.Threading.Tasks.Task<SpaceList> spaceTask = this._spaceService.GetAllSpacesAsync();

            var spaceList = await spaceTask as SpaceList;

            foreach(Space s in spaceList.Results)
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
                CommandID toolbarMenuCommandRefreshID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.TestCommandRefreshId);

                MenuCommand onToolbarMenuCommandRefreshClick = new MenuCommand(RefreshSpaces, toolbarMenuCommandRefreshID);

                service.RemoveCommand(service.FindCommand(toolbarMenuCommandRefreshID));
                service.AddCommand(onToolbarMenuCommandRefreshClick);
            }
        }

        private void RefreshSpaces(object sender, EventArgs e)
        {
            List<Space> temporarySpaceList;

            this.SpaceList.Clear();
            temporarySpaceList = new List<Space>(this._spaceService.GetAllSpaces().Results);

            foreach(Space space in temporarySpaceList)
            {
                this.SpaceList.Add(space);
            }
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }

    }
}