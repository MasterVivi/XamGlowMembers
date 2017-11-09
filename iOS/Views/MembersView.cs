using members.Core.ViewModels;
using members.iOS.Tables;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;

namespace members.iOS.Views
{
    public partial class MembersView : MvxViewController<MembersViewModel>
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

            // Create source that handles the cells
            _source = new MembersTableViewSource(_table_members_list);

            // Being the binding
            var set = this.CreateBindingSet<MembersView, MembersViewModel>();
            set.Bind(_source).To(ViewModel => ViewModel.Members);
            set.Apply();

            // Attach the source to the list
            _table_members_list.Source = _source;
        }
    }
}