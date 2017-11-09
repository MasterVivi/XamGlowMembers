using Foundation;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="T:members.iOS.Tables.MembersTableViewSource"/> class.
        /// </summary>
        /// <param name="tableView">Table view.</param>
        public MembersTableViewSource (UITableView tableView) : base(tableView)
        {
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
    }
}