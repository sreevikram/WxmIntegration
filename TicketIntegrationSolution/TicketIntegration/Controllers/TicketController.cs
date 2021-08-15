using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TicketIntegration.Models;
using TicketIntegration.Services.Ticketing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicketIntegration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private ITicketing _ticketing;

        public TicketController(ITicketing ticketing)
        {
            _ticketing = ticketing; 
        }

        

        // POST api/<TicketController>
        [HttpPost]
        public IActionResult Post([FromBody] WxmSurveyResponse response)
        {

           var res =_ticketing.Create(response);
           if (!res)
           {
               return BadRequest();
           }
           else
           {
               return Ok("Success");
           }
        }

       
        
    }
}
