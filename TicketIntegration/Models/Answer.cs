using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketIntegration.Models
{
    public class Answer
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("locationId")]
        public string LocationId { get; set; }

        [JsonProperty("responseDateTime")]
        public DateTimeOffset ResponseDateTime { get; set; }

        [JsonProperty("responseDuration")]
        public long ResponseDuration { get; set; }

        [JsonProperty("surveyClient")]
        public string SurveyClient { get; set; }

        [JsonProperty("responses")]
        public List<Response> Responses { get; set; }

        [JsonProperty("archived")]
        public bool Archived { get; set; }

        [JsonProperty("notes")]
        public object Notes { get; set; }

        [JsonProperty("openTicket")]
        public object OpenTicket { get; set; }

        [JsonProperty("deviceId")]
        public object DeviceId { get; set; }
    }
}
