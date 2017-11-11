using System;
namespace members.Core.Models.Interfaces
{
    public interface IMember : IModelIdentifier
    {
        bool IsActive { get; set; }
        string BranchId { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        IMembership Membership { get; set; }
        string Namespace { get; set; }
        string Phone { get; set; }
        string Type { get; set; }
        string Name { get; set; }
        string ImageUrl { get; set; }
    }
}