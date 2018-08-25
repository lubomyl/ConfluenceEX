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

        private bool _openInExternalBrowser;

        public DelegateCommand SpaceContentSelectedCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public ContentListViewModel(string username, string password, Space space)
        {
            string spaceKey = space.Key;

            this._contentService = new ContentService();
            this.SpaceContentList = new ObservableCollection<Content>();

            //this.GetContentBySpaceKeyAsync(spaceKey);

            this.SpaceContentSelectedCommand = new DelegateCommand(OnItemSelected);
        }

        /*private async void GetContentBySpaceKeyAsync(string spaceKey)
        {
            System.Threading.Tasks.Task<ContentList> contentTask = this._contentService.GetContentBySpaceKeyAsync(spaceKey);

            var contentList = await contentTask as ContentList;

            foreach (Content c in contentList.Results)
            {
                this.SpaceContentList.Add(c);
            }
        }*/

        private void OnItemSelected(object sender)
        {
            Content content = sender as Content;

            if (this._openInExternalBrowser)
            {
                System.Diagnostics.Process.Start("https://lubomyl3.atlassian.net/wiki" + content.Links.Webui);
            }
            else
            {
                IVsWindowFrame ppFrame;
                var service = Package.GetGlobalService(typeof(IVsWebBrowsingService)) as IVsWebBrowsingService;

                service.Navigate("https://lubomyl3.atlassian.net/wiki" + content.Links.Webui, 0, out ppFrame);
            }
        }

        public ObservableCollection<Content> SpaceContentList
        {
            get { return this._spaceContentList; }
            set { this._spaceContentList = value; }
        }

        public bool OpenInExternalBrowser
        {
            get { return this._openInExternalBrowser; }
            set { this._openInExternalBrowser = value; }
        }
    }
}
