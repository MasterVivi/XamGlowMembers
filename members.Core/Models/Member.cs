using System;
using members.Core.Models.Interfaces;

namespace members.Core.Models
{
    public class Member : IMember
    {
        public string Id { get; set; }

        // Used properties
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }

        // Unused
        public string BranchId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Membership Membership { get; set; }
        public string Namespace { get; set; }
        public string Phone { get; set; }
        public string Type { get; set; }

        IMembership IMember.Membership { get { return Membership; } set { Membership = (Membership)value; } }
    }
}