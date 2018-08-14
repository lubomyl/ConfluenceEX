using ConfluenceEX.Command;
using ConfluenceRestClient.Model;
using ConfluenceRestClient.Service;
using ConfluenceRestClient.Service.Implementation;
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

        private const int CONTENT_ID = 196609;

        private IContentService _contentService;

        public ObservableCollection<Content> SpaceContentList { get; set; }

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
            //TODO on content selected open new tab with html page
        }
    }
}
