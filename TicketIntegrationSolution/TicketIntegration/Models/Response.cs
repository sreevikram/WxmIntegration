using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketIntegration.Models
{
    public class Response
    {
        [JsonProperty("questionId")]
        public string QuestionId { get; set; }

        [JsonProperty("questionText")]
        public string QuestionText { get; set; }

        [JsonProperty("textInput")]
        public string TextInput { get; set; }

        [JsonProperty("numberInput")]
        public long NumberInput { get; set; }
    }
}
