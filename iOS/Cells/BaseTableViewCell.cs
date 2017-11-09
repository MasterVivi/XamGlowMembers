using System;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Platform.Platform;

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

            try
            {
                BindToViewModel();
                BindToLanguage();
                ApplyTheme();
                AddHandlers();
            }
            catch (Exception ex)
            {
                MvxTrace.TaggedError("Binding", ex.Message);
            }
        }

        /// <summary>
        /// Cleanup of remaining handlers
        /// </summary>
        /// <returns>The dispose.</returns>
        /// <param name="disposing">If set to <c>true</c> disposing.</param>
        protected override void Dispose(bool disposing)
        {
            RemoveHandlers();
            base.Dispose(disposing);
        }

        /// <summary>
        /// Binds view components to view model.
        /// </summary>
        /// <example>set.Bind([Component]).To(vm => vm.[Property]);</example>
        protected abstract void BindToViewModel();

        /// <summary>
        /// Binds component text to language translations
        /// </summary>
        /// <example>this.BindLanguage([Component], "[Property]", "[Resource]");</example>
        protected abstract void BindToLanguage();

        /// <summary>
        /// Applies the theme.
        /// </summary>
        /// <returns>The theme.</returns>
        protected abstract void ApplyTheme();

        /// <summary>
        /// Adds the handlers.
        /// </summary>
        /// <returns>The handlers.</returns>
        protected abstract void AddHandlers();

        /// <summary>
        /// Removes the handlers.
        /// </summary>
        /// <returns>The handlers.</returns>
        protected abstract void RemoveHandlers();
    }
}