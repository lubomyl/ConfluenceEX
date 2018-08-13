using ConfluenceRestClient.Model;

using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;
using ConfluenceEX.Main;
using System;
using System.Collections.ObjectModel;
using ConfluenceRESTClient.Service;
using ConfluenceRESTClient.Service.Implementation;

namespace ConfluenceEX.ViewModel
{
    public class SpaceListViewModel : ViewModelBase
    {

        private const int CONTENT_ID = 196609;

        private ISpaceService _spaceService;

        public ObservableCollection<Space> SpaceList { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public SpaceListViewModel(string username, string password)
        {
            this._spaceService = new SpaceService(username, password);

            this.SpaceList = new ObservableCollection<Space>(this._spaceService.GetAllSpaces().Results);

            OleMenuCommandService service = ConfluencePackage.Mcs;

            InitializeCommands(service);
        }

        private void InitializeCommands(OleMenuCommandService service)
        {
            if (service != null)
            {
                /*CommandID toolbarMenuCommand1ID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.TestCommand1Id);
                CommandID toolbarMenuCommand2ID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.TestCommand2Id);

                MenuCommand onToolbarMenuCommand1Click = new MenuCommand(TestOnPropertyChanged, toolbarMenuCommand1ID);
                MenuCommand onToolbarMenuCommand2Click = new MenuCommand(TestOnCollectionAdd, toolbarMenuCommand2ID);

                service.RemoveCommand(service.FindCommand(toolbarMenuCommand1ID));
                service.AddCommand(onToolbarMenuCommand1Click);

                service.RemoveCommand(service.FindCommand(toolbarMenuCommand2ID));
                service.AddCommand(onToolbarMenuCommand2Click);*/
            }
        }

    }
}