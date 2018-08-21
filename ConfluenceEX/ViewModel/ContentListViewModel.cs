using ConfluenceEX.Command;
using ConfluenceRestClient.Model;
using ConfluenceRestClient.Service;
using ConfluenceRestClient.Service.Implementation;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceEX.ViewModel
{
    class ContentListViewModel : ViewModelBase
    {
        private IContentService _contentService;

        private ObservableCollection<Content> _spaceContentList;

        public DelegateCommand SpaceContentSelectedCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public ContentListViewModel(string username, string password, Space space)
        {
            string spaceKey = space.Key;

            this._contentService = new ContentService(username, password);

            this.SpaceContentList = new ObservableCollection<Content>(this._contentService.GetContentBySpaceKey(spaceKey).Results);
            this.SpaceContentSelectedCommand = new DelegateCommand(OnItemSelected);

        }

        private void OnItemSelected(object sender)
        {
            Content content = sender as Content;

            IVsWindowFrame ppFrame;
            var service = Package.GetGlobalService(typeof(IVsWebBrowsingService)) as IVsWebBrowsingService;

            service.Navigate("https://lubomyl3.atlassian.net/wiki" + content.Links.Webui, 0, out ppFrame);
        }

        public ObservableCollection<Content> SpaceContentList
        {
            get { return this._spaceContentList; }
            set { this._spaceContentList = value; }
        }
    }
}
