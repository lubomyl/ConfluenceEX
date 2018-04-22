using ConfluenceRestClient.Service;
using ConfluenceRestClient.Service.Implementation;
using ConfluenceRestClient.Model;

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
    }
}