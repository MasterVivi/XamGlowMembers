using System.Windows.Input;
using Foundation;
using members.Core.ViewModels;
using members.iOS.Tables;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using UIKit;

namespace members.iOS.Views
{
    public partial class MembersView : MvxViewController<MembersViewModel>, IUISearchBarDelegate
    {
        #region Properties

        private MembersTableViewSource _source;

        // Search variables
        public ICommand SearchCommand { get; set; }
        private UISearchBar _searchBar;
        private UIBarButtonItem _searchBtn;

        #endregion

        #region Instantiation

        public MembersView() : base("MembersView", null)
        {
            CreateSearchButton();
        }

        private void CreateSearchButton()
        {
            SearchCommand = new MvxCommand(AddUISearchItem);

            _searchBar = new UISearchBar();
            _searchBar.SizeToFit();
            _searchBar.ShowsCancelButton = true;
            _searchBar.Delegate = this;
            _searchBar.Placeholder = "Search Email";
            _searchBar.AutocapitalizationType = UITextAutocapitalizationType.None;
        }

        /// <summary>
        /// Process binding of the view to our ViewModel when the view loads
        /// </summary>
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Members";

            // Create source that handles the cells
            _source = new MembersTableViewSource(_table_members_list, ViewModel);

            // Being the binding
            var set = this.CreateBindingSet<MembersView, MembersViewModel>();
            set.Bind(_source).To(ViewModel => ViewModel.Members);
            set.Apply();

            // Attach the source to the list
            _table_members_list.Source = _source;

            // add search button
            _searchBtn = new UIBarButtonItem(
              UIImage.FromBundle("SearchIcon"), UIBarButtonItemStyle.Plain, (sender, e) =>
              {
                  SearchCommand?.Execute(null);
              });
            NavigationItem.SetRightBarButtonItem(_searchBtn, true);
        }

        /// <summary>
        /// Adds the UI Search item to the navigation bar
        /// </summary>
        private void AddUISearchItem()
        {
            NavigationItem.SetHidesBackButton(true, true);
            NavigationItem.SetRightBarButtonItem(null, true);
            NavigationItem.TitleView = _searchBar;
        }

        #endregion

        #region Search bar callbacks

        [Export("searchBar:textDidChange:")]
        public async void TextChanged(UISearchBar searchBar, string searchText)
        {
            await ViewModel.SearchMembers(searchText.ToLower());
        }

        [Export("searchBarCancelButtonClicked:")]
        public async void CancelButtonClicked(UISearchBar searchBar)
        {
            NavigationItem.SetHidesBackButton(false, true);
            NavigationItem.SetRightBarButtonItem(_searchBtn, true);
            NavigationItem.TitleView = null;

            // Reset values to original
            await ViewModel.EndSearch();
        }

        #endregion
    }
}