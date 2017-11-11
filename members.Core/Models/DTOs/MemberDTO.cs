using members.Core.Models.Interfaces;
using Newtonsoft.Json;

namespace members.Core.Models.DTOs
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MemberDTO : IMember
    {
        /*
         * "_id": "56cddd2b5c46bb12a2b924ae",
            "active": true,
            "branch_id": "56cdc0155c46bb176bb92582",
            "email": "test@zappy.ie",
            "first_name": "Test",
            "last_name": "User",
            "membership": {
                "_id": "57ffdb86edc46090728b4567",
                "type": "time_classes",
                "start_date": 1491177600,
                "membership_group_id": null,
                "plan_code": 1476385641150,
                "boooked_events": 0,
                "expiry_date": 1522713600
            },
            "namespace": "thewodfactory",
            "phone": "9999999999",
            "type": "member",
            "name": "Test User",
            "image_url": "https://s3-eu-west-1.amazonaws.com/glofox/staging/thewodfactory/branches/56cdc0155c46bb176bb92582/users/56cddd2b5c46bb12a2b924ae.png"
         */

        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("active")]
        public bool IsActive { get; set; }

        [JsonProperty("branch_id")]
        public string BranchId { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("membership")]
        public MembershipDTO Membership { get; set; }

        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        IMembership IMember.Membership { get { return Membership; } set { Membership = (MembershipDTO)value; } }
    }
}