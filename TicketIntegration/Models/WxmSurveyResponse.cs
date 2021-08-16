using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketIntegration.Models
{
    public class WxmSurveyResponse
    {
        [JsonProperty("notification")]
        public string Notification { get; set; }

        [JsonProperty("answer")]
        public Answer Answer { get; set; }
    }
}
