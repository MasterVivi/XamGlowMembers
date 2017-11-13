using CoreGraphics;
using Foundation;
using members.Core.ViewModels;
using members.iOS.Cells;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace members.iOS.Tables
{
    /// <summary>
    /// Subclass MvxTableViewSource for better support for ViewModel binding 
    /// and property changes.
    /// </summary>
    public class MembersTableViewSource : MvxTableViewSource
    {
        private static readonly string cellIdentifier = "MemberCell";

        private bool _isPaging = false;
        private UITableView _tableView;
        private UIActivityIndicatorView _indicator;

        // Reference to view model for data retrieval
        private MembersViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:members.iOS.Tables.MembersTableViewSource"/> class.
        /// </summary>
        /// <param name="tableView">Table view.</param>
        public MembersTableViewSource(UITableView tableView, MembersViewModel viewModel) : base(tableView)
        {
            _tableView = tableView;
            _viewModel = viewModel;

            InitializeRefreshControl(tableView, UIColor.Black);

            tableView.RegisterNibForCellReuse(UINib.FromName(MemberCell.Key, NSBundle.MainBundle), cellIdentifier);
        }

        /// <summary>
        /// Specify the cell that should be returned for the given object data passed
        /// </summary>
        /// <returns>The or create cell for.</returns>
        /// <param name="tableView">Table view.</param>
        /// <param name="indexPath">Index path.</param>
        /// <param name="item">Item.</param>
        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            // request a recycled cell to save memory
            return tableView.DequeueReusableCell(cellIdentifier, indexPath);
        }

        #region TableView Callbacks

        /// <summary>
        /// Scrolled the specified scrollView.
        /// </summary>
        /// <returns>The scrolled.</returns>
        /// <param name="scrollView">Scroll view.</param>
        public async override void Scrolled(UIScrollView scrollView)
        {
            var curScrollHeight = scrollView.ContentOffset.Y + scrollView.Frame.Size.Height;
            var scrollLimit = scrollView.ContentSize.Height;

            if (!_isPaging && curScrollHeight >= scrollLimit)
            {
                _isPaging = true;

                _tableView.TableFooterView = _indicator;
                _indicator.StartAnimating();

                await _viewModel.NextPage();

                _indicator.StopAnimating();
                _tableView.TableFooterView = null;

                _isPaging = false;
            }
        }

        #endregion

        #region Indicator handlers

        private void InitializeRefreshControl(UITableView tableView, UIColor color)
        {
            _indicator = new UIActivityIndicatorView(new CGRect(0, 0, tableView.Frame.Width, 50));
            _indicator.Color = color;
            _indicator.HidesWhenStopped = true;
        }

        #endregion
    }
}