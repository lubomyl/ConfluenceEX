using ConfluenceEX.Command;
using ConfluenceRestClient.Model;
using ConfluenceRestClient.Service;
using ConfluenceRestClient.Service.Implementation;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
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

        public DelegateCommand OpenContentInBuildInTabCommand { get; private set; }
        public DelegateCommand OpenContentInExternalTabCommand { get; private set; }

        public ContentViewModel(int contentId)
        {
            this._contentService = new ContentService();

            this.GetContentAsync(contentId);

            this.OpenContentInBuildInTabCommand = new DelegateCommand(OpenContentInBuildInTab);
            this.OpenContentInExternalTabCommand = new DelegateCommand(OpenContentInExternalTab);
        }

        private async void GetContentAsync(int contentId)
        {
            System.Threading.Tasks.Task<Content> contentTask = this._contentService.GetContentByIdAsync(contentId);

            this.Content = await contentTask as Content;
        }

        private void OpenContentInBuildInTab(object sender)
        {
            string contentUrl = sender as string;

            IVsWindowFrame ppFrame;
            var service = Package.GetGlobalService(typeof(IVsWebBrowsingService)) as IVsWebBrowsingService;

            service.Navigate("https://lubomyl3.atlassian.net/wiki" + contentUrl, 0, out ppFrame);
        }

        private void OpenContentInExternalTab(object sender)
        {
            string contentUrl = sender as string;

            System.Diagnostics.Process.Start("https://lubomyl3.atlassian.net/wiki" + contentUrl);
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
