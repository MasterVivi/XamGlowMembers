using System;
using MvvmCross.Binding.iOS.Views;

namespace members.iOS.Cells
{
    /// <summary>
    /// Cells should sublclass MvxTableViewCell to allow for binding
    /// to the corresponding core data
    /// </summary>
    public abstract class BaseTableViewCell : MvxTableViewCell
    {
        public BaseTableViewCell()
        {}

        protected BaseTableViewCell(IntPtr handle) : base(handle)
        {}

        /// <summary>
        /// Perform a selection of functions on the cell after load
        /// </summary>
        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            BindToViewModel();
        }

        /// <summary>
        /// Binds view components to view model.
        /// </summary>
        /// <example>set.Bind([Component]).To(vm => vm.[Property]);</example>
        protected abstract void BindToViewModel();
    }
}