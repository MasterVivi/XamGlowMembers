using System;
namespace members.Core.Models
{
    public class Member
    {
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }

        public Member(bool active = true, string name = "Test", string email = "j@jj", string imageUrl = "")
        {
            IsActive = active;
            Name = name;
            Email = email;
            ImageUrl = imageUrl;
        }
    }
}
