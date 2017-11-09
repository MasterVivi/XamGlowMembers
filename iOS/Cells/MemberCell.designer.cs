// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace members.iOS.Cells
{
    [Register ("MemberCell")]
    partial class MemberCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel _txt_member_email { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel _txt_member_name { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (_txt_member_email != null) {
                _txt_member_email.Dispose ();
                _txt_member_email = null;
            }

            if (_txt_member_name != null) {
                _txt_member_name.Dispose ();
                _txt_member_name = null;
            }
        }
    }
}