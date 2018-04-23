using ConfluenceRestClient.Service;
using ConfluenceRestClient.Service.Implementation;
using ConfluenceRestClient.Model;

using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;
using ConfluenceEX.Main;
using System;

namespace ConfluenceEX.ViewModel
{
    public class ContentListViewModel : ViewModelBase
    {

        private const int CONTENT_ID = 32959;

        private IContentService _contentService;

        private Content _content;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public ContentListViewModel()
        {
            _contentService = new ContentService();
            _content = _contentService.GetContentById(CONTENT_ID);

           

            OleMenuCommandService service = ConfluenceCommandPackage.Mcs;
            CommandID toolbarMenuTestCommandID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.TestCommand1Id);
            MenuCommand onToolbarMenuClickTest = new MenuCommand(TestOnPropertyChanged, toolbarMenuTestCommandID);

            MenuCommand command = service.FindCommand(new CommandID(Guids.guidConfluenceToolbarMenu, Guids.TestCommand1Id));

            service.AddCommand(onToolbarMenuClickTest);
        }

        private void TestOnPropertyChanged(object sender, EventArgs e)
        {
            Title = "Test OnPropertyChanged";
        }

        public Content Content
        {
            get { return _content; }
            set
            {
                _content = value;
                OnPropertyChanged("Content");
            }
        }

        public string Title
        {
            get { return _content.Title; }
            set {
                _content.Title = value;
                OnPropertyChanged("Title");
            }
        }
    }
}