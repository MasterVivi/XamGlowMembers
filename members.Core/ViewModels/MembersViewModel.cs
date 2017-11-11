using System.Collections.Generic;
using System.Threading.Tasks;
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
        private bool _hasMoreContent;
        private int _currentPage = 1;

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
        public MembersViewModel(IAPIService apiService) : base()
        {
            _apiService = apiService;

            Members = new List<Member>();
        }

        public async override void ViewCreated()
        {
            base.ViewCreated();

            await LoadMembers(_currentPage);
        }

        #endregion

        #region Methods
           
        public Task NextPage()
        {
            if (_hasMoreContent) {
                _currentPage += 1;
                return LoadMembers(_currentPage);
            }

            return Task.FromResult(0);
        }

        public async Task LoadMembers (int page)
        {
            // Add data to current data
            var result = await _apiService.GetMembers(page);
            _hasMoreContent = result.HasPartialContent;
            Members.AddRange(result.Response);
            RaisePropertyChanged(() => Members);
        }

        #endregion
    }
}