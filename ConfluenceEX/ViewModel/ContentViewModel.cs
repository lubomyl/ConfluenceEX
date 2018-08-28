using ConfluenceRestClient.Model;
using ConfluenceRestClient.Service;
using ConfluenceRestClient.Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceEX.ViewModel
{
    public class ContentViewModel : ViewModelBase
    {

        private Content _content;

        private IContentService _contentService;

        public ContentViewModel(int contentId)
        {
            this._contentService = new ContentService();

            this.GetContentAsync(contentId);
        }

        private async void GetContentAsync(int contentId)
        {
            Task<Content> contentTask = this._contentService.GetContentByIdAsync(contentId);

            this.Content = await contentTask as Content;
        }

        public Content Content
        {
            get
            {
                return this._content;
            }
            set
            {
                this._content = value;
                OnPropertyChanged("Content");
            }
        }

    }
}
