using System;
namespace members.Core.Models.Interfaces
{
    public interface IMembership : IModelIdentifier
    {
        string MemberShipGroupId { get; set; }
        string Type { get; set; }
        DateTime StartDate { get; set; }
        DateTime ExpiryDate { get; set; }
        string PlanCode { get; set; }
        int BookedEvents { get; set; }
    }
}