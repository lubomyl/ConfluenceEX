using ConfluenceEX.View;
using ConfluenceEX.ViewModel;
using ConfluenceRestClient.Model;
using Microsoft.Internal.VisualStudio.PlatformUI;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ConfluenceEX.Main
{
    public partial class ConfluenceToolWindow : ToolWindowPane
    {
        private ObservableCollection<Space> _spaceList;
        private ObservableCollection<Space> _spaceListCopy;

        private bool _repeatedSearch = false;

        public override bool SearchEnabled
        {
            get { return true; }
        }

        public override void ProvideSearchSettings(IVsUIDataSource pSearchSettings)
        {
            Utilities.SetValue(pSearchSettings,
                SearchSettingsDataSource.SearchWatermarkProperty.Name, "Search Spaces list");
        }

        public override IVsSearchTask CreateSearch(uint dwCookie, IVsSearchQuery pSearchQuery, IVsSearchCallback pSearchCallback)
        {
            if (pSearchQuery == null || pSearchCallback == null)
                return null;
            return new SearchTask(dwCookie, pSearchQuery, pSearchCallback, this);
        }

        public override void ClearSearch()
        {
            _repeatedSearch = false;

            ConfluenceToolWindowNavigator control = (ConfluenceToolWindowNavigator) this.Content;

            _spaceList.Clear();

            foreach (Space space in _spaceListCopy)
            {
                _spaceList.Add(space);
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
                ConfluenceToolWindowNavigator control = (ConfluenceToolWindowNavigator)m_toolWindow.Content;

                control.Dispatcher.Invoke(() =>
                {
                    ConfluenceToolWindowNavigatorViewModel navigator = (ConfluenceToolWindowNavigatorViewModel)control.DataContext;

                    InitializeListsToSearch(navigator);
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
                    resultCount = SearchForSpaceName(resultList, matchCase, resultCount, searchString);
                }
                catch (Exception e)
                {
                    this.ErrorCode = VSConstants.E_FAIL;
                }
                finally
                {
                    m_toolWindow._repeatedSearch = true;

                    ThreadHelper.Generic.Invoke(() =>
                    {
                        m_toolWindow._spaceList.Clear();

                        foreach (Space space in resultList)
                        {
                            m_toolWindow._spaceList.Add(space);
                        }
                    });

                    this.SearchResults = resultCount;
                }

                // Call the implementation of this method in the base class.   
                // This sets the task status to complete and reports task completion.   
                base.OnStartSearch();
            }

            private uint SearchForSpaceName(List<Space> resultList, bool matchCase, uint resultCount, string searchString)
            {
                foreach (Space space in m_toolWindow._spaceList)
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

                return resultCount;
            }

            private void InitializeListsToSearch(ConfluenceToolWindowNavigatorViewModel navigator)
            {
                m_toolWindow._spaceList = ((SpaceListViewModel)((SpaceListView)navigator.SelectedView).DataContext).SpaceList;

                if (!m_toolWindow._repeatedSearch)
                {
                    m_toolWindow._spaceListCopy = new ObservableCollection<Space>();

                    foreach (Space space in m_toolWindow._spaceList)
                    {
                        m_toolWindow._spaceListCopy.Add(space);
                    }
                }
            }

            protected override void OnStopSearch()
            {
                this.SearchResults = 0;

                m_toolWindow._spaceListCopy = new ObservableCollection<Space>();

                foreach (Space space in m_toolWindow._spaceList)
                {
                    m_toolWindow._spaceListCopy.Add(space);
                }
            }
        }
    }
}
