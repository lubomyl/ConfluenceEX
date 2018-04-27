using ConfluenceRestClient.Service;
using ConfluenceRestClient.Service.Implementation;
using ConfluenceRestClient.Model;

using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;
using ConfluenceEX.Main;
using System;
using System.Collections.ObjectModel;

namespace ConfluenceEX.ViewModel
{
    public class ContentListViewModel : ViewModelBase
    {

        private const int CONTENT_ID = 32959;

        private IContentService _contentService;

        private Content _content;

        public ObservableCollection<Content> ContentList { get; private set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public ContentListViewModel()
        {
            _contentService = new ContentService();
            _content = _contentService.GetContentById(CONTENT_ID);

            this.ContentList = new ObservableCollection<Content>();

            this.ContentList.Add(_content);

            OleMenuCommandService service = ConfluencePackage.Mcs;

            if (service.FindCommand(new CommandID(Guids.guidConfluenceToolbarMenu, Guids.TestCommand1Id)) == null)
            {
                InitializeCommands(service);
            }
        }

        private void InitializeCommands(OleMenuCommandService service)
        {
            if (service != null)
            {
                CommandID toolbarMenuCommand1ID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.TestCommand1Id);
                CommandID toolbarMenuCommand2ID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.TestCommand2Id);

                MenuCommand onToolbarMenuCommand1Click = new MenuCommand(TestOnPropertyChanged, toolbarMenuCommand1ID);
                MenuCommand onToolbarMenuCommand2Click = new MenuCommand(TestOnCollectionAdd, toolbarMenuCommand2ID);

                service.AddCommand(onToolbarMenuCommand1Click);
                service.AddCommand(onToolbarMenuCommand2Click);
            }
        }

        private void TestOnPropertyChanged(object sender, EventArgs e)
        {
            Random random = new Random();

            Title = "Nový titul" + random.Next();
        }

        private void TestOnCollectionAdd(object sender, EventArgs e)
        {
            Content cnt = new Content();
            Random random = new Random();

            cnt.Title = "Nový titul" + random.Next();

            ContentList.Add(cnt);
        }

        #region ContentListViewModel Members

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

        #endregion
    }
}