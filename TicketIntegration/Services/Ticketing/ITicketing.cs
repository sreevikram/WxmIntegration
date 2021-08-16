using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketIntegration.Models;

namespace TicketIntegration.Services.Ticketing
{
    public interface ITicketing
    {
        public bool  Create(WxmSurveyResponse res);
    }
}
