using System;
using members.Core.Models.Interfaces;
using members.Core.Utilities.Converters;
using Newtonsoft.Json;

namespace members.Core.Models.DTOs
{
    public class MembershipDTO : IMembership
    {
        /*
         * "membership": {
                "_id": "57ffdb86edc46090728b4567",
                "type": "time_classes",
                "start_date": 1491177600,
                "membership_group_id": null,
                "plan_code": 1476385641150,
                "boooked_events": 0,
                "expiry_date": 1522713600
            },
            */

        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("start_date")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime StartDate { get; set; }

        [JsonProperty("membership_group_id")]
        public string MemberShipGroupId { get; set; }

        [JsonProperty("plan_code")]
        public string PlanCode { get; set; }

        [JsonProperty("booked_events")]
        public int BookedEvents { get; set; }

        [JsonProperty("expiry_date")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime ExpiryDate { get; set; }
    }
}