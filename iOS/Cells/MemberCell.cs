using System;
using Foundation;
using members.Core.Models;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace members.iOS.Cells
{
    /// <summary>
    /// Cells should subclass MvxTableViewCell to allow for binding
    /// to the corresponding core data
    /// </summary>
    public partial class MemberCell : BaseTableViewCell
    {
        public static readonly NSString Key = new NSString("MemberCell");
        public static readonly UINib Nib;

        // Lazy image loader
        private MvxImageViewLoader _imageLoader;

        static MemberCell()
        {
            Nib = UINib.FromName(Key, NSBundle.MainBundle);
        }

        protected MemberCell(IntPtr handle) : base(handle)
        {
            _imageLoader = new MvxImageViewLoader(() => ImageView);
        }

        /// <summary>
        /// Bind outlet values to Member object passed in list
        /// </summary>
        protected override void BindToViewModel()
        {
            var set = this.CreateBindingSet<MemberCell, Member>();
            set.Bind(_txt_member_name).To(vm => vm.Name);
            set.Bind(_txt_member_email).To(vm => vm.Email);
            set.Bind(_imageLoader).To(vm => vm.ImageUrl);
            set.Apply();
        }
    }
}