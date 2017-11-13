using System;
using members.Core.Models.Interfaces;

namespace members.Core.Models
{
    public class Membership : IMembership
    {
        public string Id { get; set; }
        public string MemberShipGroupId { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string PlanCode { get; set; }
        public int BookedEvents { get; set; }
    }
}