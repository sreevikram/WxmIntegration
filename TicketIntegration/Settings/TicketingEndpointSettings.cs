using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketIntegration.Settings
{
    public class TicketingEndpointSettings
    {
        public string Host { set; get; }
        public string ApiPath { set; get; }
        public string ApiKey { set; get; }
        public string UserName { set; get; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
