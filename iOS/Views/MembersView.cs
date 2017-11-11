using Foundation;
using members.Core.ViewModels;
using members.iOS.Tables;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using UIKit;

namespace members.iOS.Views
{
    public partial class MembersView : MvxViewController<MembersViewModel>, IUISearchBarDelegate, IUITableViewDelegate
    {
        private MembersTableViewSource _source;

        public MembersView() : base("MembersView", null)
        {
        }

        /// <summary>
        /// Process binding of the view to our ViewModel when the view loads
        /// </summary>
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Members";

            // Create source that handles the cells
            _source = new MembersTableViewSource(_table_members_list);

            // Being the binding
            var set = this.CreateBindingSet<MembersView, MembersViewModel>();
            set.Bind(_source).To(ViewModel => ViewModel.Members);
            set.Apply();

            // Attach the source to the list
            _table_members_list.Source = _source;
        }

        [Export("scrollViewDidScroll:")]
        public void Scrolled(UIScrollView scrollView)
        {
            var curScrollHeight = scrollView.ContentOffset.Y + scrollView.Frame.Size.Height;
            var scrollLimit = scrollView.ContentSize.Height;

            if (curScrollHeight >= scrollLimit)
            {
              //_tableView_mySchedule_table.TableFooterView = _indicator;
              //_indicator.StartAnimating();

              //_tableView_mySchedule_table.Bounces = false;

                ViewModel.NextPage();
            }
        }
    }
}