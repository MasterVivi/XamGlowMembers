// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace members.iOS.Views
{
    [Register ("MembersView")]
    partial class MembersView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView _table_members_list { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (_table_members_list != null) {
                _table_members_list.Dispose ();
                _table_members_list = null;
            }
        }
    }
}