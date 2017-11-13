using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using members.Core.Models;
using members.Core.Services.Network;
using MvvmCross.Core.ViewModels;

namespace members.Core.ViewModels
{
    /// <summary>
    /// All view models should inherit from MvxViewModel in MVVMCross
    /// </summary>
    public class MembersViewModel : MvxViewModel
    {
        #region Properties

        private IAPIService _apiService;
        private IUserDialogs _userDialogs;

        private bool _hasMoreContent;
        private int _currentPage = 1;
        private string _currentSearchQuery = null;

        /// <summary>
        /// Gets or sets the members.
        /// List that is binded to with UI elements
        /// </summary>
        /// <value>The members.</value>
        private List<Member> _members;
        public List<Member> Members
        {
            get
            {
                return _members;
            }
            set
            {
                _members = value;
                RaisePropertyChanged(() => Members);
            }
        }

        #endregion

        #region Instantiation

        /// <summary>
        /// Initializes a new instance of the <see cref="T:members.Core.ViewModels.MembersViewModel"/> class.
        /// Test creation of data currently
        /// </summary>
        public MembersViewModel(IAPIService apiService, IUserDialogs userDialogs) : base()
        {
            _apiService = apiService;
            _userDialogs = userDialogs;

            Members = new List<Member>();
        }

        /// <summary>
        /// Callback for the view being created
        /// </summary>
        public async override void ViewCreated()
        {
            base.ViewCreated();

            await NewSearch(_currentPage);
        }

        #endregion

        #region Methods
           
        /// <summary>
        /// Handles paging through content
        /// </summary>
        /// <returns>The page.</returns>
        public Task NextPage()
        {
            if (_hasMoreContent) {
                _hasMoreContent = false;
                _currentPage += 1;
                return LoadMembers(_currentPage, _currentSearchQuery);
            }

            return Task.FromResult(0);
        }

        /// <summary>
        /// Passes a search to string to filter uses by email
        /// </summary>
        /// <returns>The members.</returns>
        /// <param name="email">Email.</param>
        public async Task SearchMembers (string email)
        {
            // Set the current search query
            _currentSearchQuery = email;
            ResetList();

            await NewSearch(_currentPage, _currentSearchQuery);
        }

        /// <summary>
        /// New search that requires a loading bar
        /// </summary>
        /// <returns>The search.</returns>
        public async Task NewSearch (int page, string searchQuery = null)
        {
            _userDialogs.ShowLoading("fetching");

            await LoadMembers(page, searchQuery);

            _userDialogs.HideLoading();
        }

        /// <summary>
        /// Put the list state back to not being searched
        /// </summary>
        /// <returns>The search.</returns>
        public async Task EndSearch ()
        {
            _currentSearchQuery = null;
            ResetList();

            await NewSearch(_currentPage);
        }

        /// <summary>
        /// Handles the actual query to retrieve the members
        /// </summary>
        /// <returns>The members.</returns>
        /// <param name="page">Page.</param>
        /// <param name="searchQuery">Search query.</param>
        private async Task LoadMembers(int page, string searchQuery = null)
        {
            // Add data to current data
            var result = await _apiService.GetMembers(page, searchQuery);
            _hasMoreContent = result.HasPartialContent;
            Members.AddRange(result.Response);
            Members = new List<Member>(Members);
        }

        /// <summary>
        /// Resets the list.
        /// </summary>
        private void ResetList ()
        {
            _currentPage = 1;
            Members = new List<Member>();
        }

        #endregion
    }
}