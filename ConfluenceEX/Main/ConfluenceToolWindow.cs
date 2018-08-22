using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using ConfluenceEX.Main;
using ConfluenceEX.View;
using System.ComponentModel.Design;
using ConfluenceEX.ViewModel;
using ConfluenceRESTClient.Service.Implementation;
using ConfluenceRESTClient.Service;
using ConfluenceRESTClient.Model;
using ConfluenceEX.Common;
using Microsoft.VisualStudio.Shell.Interop;
using System.Collections.ObjectModel;
using ConfluenceRestClient.Model;
using Microsoft.VisualStudio;
using System.Collections.Generic;

namespace ConfluenceEX
{

    [Guid(Guids.GUID_CONFLUENCE_TOOL_WINDOW_STRING)]
    public class ConfluenceToolWindow : ToolWindowPane
    {
        private readonly object _view;
        private ConfluenceToolWindowNavigatorViewModel _navigator;
        private bool _isAuthenticated;

        private static AuthenticatedUser _authenticatedUser;

        public ObservableCollection<Space> spaceList;
        public ObservableCollection<Space> spaceListCopy;

        public bool repeatedSearch = false;

        public IAuthenticationService _authenticationService;

        /// <summary>
        /// Standard constructor for the tool window.
        /// </summary>
        public ConfluenceToolWindow() : base(null)
        {
            this.Caption = Resources.ConflueceToolWindowTitle;
            this._authenticationService = new AuthenticationService(SignedInUser.Username, SignedInUser.Password);
            this._navigator = new ConfluenceToolWindowNavigatorViewModel(this);

            _authenticatedUser = _authenticationService.Authenticate();

            if (_authenticationService.IsAuthenticated(_authenticatedUser))
            {
                this._navigator.ShowSpaces(null, null);
            } 
            else
            {
                this._navigator.ShowBeforeSignIn();
            }

            this._view = new ConfluenceToolWindowNavigator(this._navigator);
            base.Content = _view;

            this.ToolBar = new CommandID(Guids.guidConfluencePackage, Guids.CONFLUENCE_TOOLBAR_ID);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public override bool SearchEnabled
        {
            get { return true; }
        }

        public override IVsSearchTask CreateSearch(uint dwCookie, IVsSearchQuery pSearchQuery, IVsSearchCallback pSearchCallback)
        {
            if (pSearchQuery == null || pSearchCallback == null)
                return null;
            return new SearchTask(dwCookie, pSearchQuery, pSearchCallback, this);
        }

        public override void ClearSearch()
        {
            repeatedSearch = false;

            ConfluenceToolWindowNavigator control = (ConfluenceToolWindowNavigator) this.Content;

            spaceList.Clear();

            foreach(Space space in spaceListCopy)
            {
                spaceList.Add(space);
            }
        }

        internal class SearchTask : VsSearchTask
        {
            private ConfluenceToolWindow m_toolWindow;
            

            public SearchTask(uint dwCookie, IVsSearchQuery pSearchQuery, IVsSearchCallback pSearchCallback, ConfluenceToolWindow toolwindow)
                : base(dwCookie, pSearchQuery, pSearchCallback)
            {
                m_toolWindow = toolwindow;
            }

            protected override void OnStartSearch()
            {
                ConfluenceToolWindowNavigator control = (ConfluenceToolWindowNavigator) m_toolWindow.Content;

                control.Dispatcher.Invoke(() =>
                {
                    ConfluenceToolWindowNavigatorViewModel navigator = (ConfluenceToolWindowNavigatorViewModel) control.DataContext;
                    
                    m_toolWindow.spaceList = ((SpaceListViewModel)((SpaceListView)navigator.SelectedView).DataContext).SpaceList;

                    if (!m_toolWindow.repeatedSearch)
                    {
                        m_toolWindow.spaceListCopy = new ObservableCollection<Space>();

                        foreach (Space space in m_toolWindow.spaceList)
                        {
                            m_toolWindow.spaceListCopy.Add(space);
                        }
                    }
                });

                List<Space> resultList = new List<Space>();

                bool matchCase = false;

                uint resultCount = 0;
                this.ErrorCode = VSConstants.S_OK;

                try
                {
                    string searchString = this.SearchQuery.SearchString;

                    // Determine the results.   
                    uint progress = 0;
                    foreach (Space space in m_toolWindow.spaceList)
                    {
                        if (matchCase == true)
                        {
                            if (space.Name.Contains(searchString))
                            {
                                resultList.Add(space);
                                resultCount++;
                            }
                        }
                        else
                        {
                            if (space.Name.ToLower().Contains(searchString.ToLower()))
                            {
                                resultList.Add(space);
                                resultCount++;
                            }
                        }

                        // SearchCallback.ReportProgress(this, progress++, (uint)contentArr.GetLength(0));   

                        // Uncomment the following line to demonstrate the progress bar.   
                        // System.Threading.Thread.Sleep(100);  
                    }
                }
                catch (Exception e)
                {
                    this.ErrorCode = VSConstants.E_FAIL;
                }
                finally
                {
                    m_toolWindow.repeatedSearch = true;

                    ThreadHelper.Generic.Invoke(() =>
                    {
                        m_toolWindow.spaceList.Clear();

                        foreach(Space space in resultList)
                        {
                            m_toolWindow.spaceList.Add(space);
                        }
                    });

                    this.SearchResults = resultCount;
                }

                // Call the implementation of this method in the base class.   
                // This sets the task status to complete and reports task completion.   
                base.OnStartSearch();
            }

            protected override void OnStopSearch()
            {
                this.SearchResults = 0;

                m_toolWindow.spaceListCopy = new ObservableCollection<Space>();

                foreach (Space space in m_toolWindow.spaceList)
                {
                    m_toolWindow.spaceListCopy.Add(space);
                }
            }
        }

        public ConfluenceToolWindowNavigatorViewModel Navigator
        {
            get { return this._navigator; }
            set { this._navigator = value; }
        }

        public bool IsAuthenticated
        {
            get { return this._isAuthenticated; }
            private set { this._isAuthenticated = value; }
        }

        public static AuthenticatedUser AuthenticatedUser
        {
            get { return _authenticatedUser; }
            set { _authenticatedUser = value; }
        }
    }
}
