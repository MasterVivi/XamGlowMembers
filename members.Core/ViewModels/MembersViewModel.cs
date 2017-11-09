using System;
using System.Collections.Generic;
using members.Core.Models;
using MvvmCross.Core.ViewModels;

namespace members.Core.ViewModels
{
    /// <summary>
    /// All view models should inherit from MvxViewModel in MVVMCross
    /// </summary>
    public class MembersViewModel : MvxViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:members.Core.ViewModels.MembersViewModel"/> class.
        /// Test creation of data currently
        /// </summary>
        public MembersViewModel() : base()
        {
            Members = new List<Member>();

            for (int i = 0; i < 20; ++i)
            {
                Members.Add(new Member());
            }
        }

        /// <summary>
        /// Gets or sets the members.
        /// List that is binded to with UI elements
        /// </summary>
        /// <value>The members.</value>
        public List<Member> Members
        {
            get; set;
        }
    }
}