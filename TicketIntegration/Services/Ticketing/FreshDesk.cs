using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TicketIntegration.Models;
using TicketIntegration.Settings;

namespace TicketIntegration.Services.Ticketing
{
    public class FreshDesk:ITicketing
    {
        public   bool  Create( WxmSurveyResponse res)
        {


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(AppSettingsProvider.TicketingEndpointSettings.Host + AppSettingsProvider.TicketingEndpointSettings.ApiPath);

            request.ContentType = "application/json";
            request.Method = "POST";
            string json = GetPayload(res);
            byte[] byteArray = Encoding.UTF8.GetBytes(json);
            request.ContentLength = byteArray.Length;
            string authInfo = AppSettingsProvider.TicketingEndpointSettings.ApiKey + ":X"; // It could be your username:password also.
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            request.Headers["Authorization"] = "Basic " + authInfo;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            try
            {
                Console.WriteLine("Submitting Request");
                WebResponse response = request.GetResponse();
                // Get the stream containing content returned by the server.
                //Send the request to the server by calling GetResponse. 
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access. 
                StreamReader reader = new StreamReader(dataStream);
                // Read the content. 
                string Response = reader.ReadToEnd();
                //return status code
                Console.WriteLine("Status Code: {1} {0}", ((HttpWebResponse)response).StatusCode, (int)((HttpWebResponse)response).StatusCode);
                //return location header
                Console.WriteLine("Location: {0}", response.Headers["Location"]);
                //return the response 
                Console.Out.WriteLine(Response);
                return true;
            }
            catch (WebException ex)
            {
                Console.WriteLine("API Error: Your request is not successful. If you are not able to debug this error properly, mail us at support@freshdesk.com with the follwing X-Request-Id");
                Console.WriteLine("X-Request-Id: {0}", ex.Response.Headers["X-Request-Id"]);
                Console.WriteLine("Error Status Code : {1} {0}", ((HttpWebResponse)ex.Response).StatusCode, (int)((HttpWebResponse)ex.Response).StatusCode);
                using (var stream = ex.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    Console.Write("Error Response: ");
                    Console.WriteLine(reader.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine(ex.Message);
            }

            return false;
        }


        private string GetPayload(WxmSurveyResponse res)
        {
            var pl = new Payload
            {
                Subject = res.Answer.User + "_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_sss")
                ,Description = res.Answer.LocationId
                ,Email = AppSettingsProvider.TicketingEndpointSettings.Email
                ,Status = (int)Status.Open
                ,Priority = (int)Priority.Medium
                ,CcEmails = new List<string>()
            };


            return JsonConvert.SerializeObject(pl);


        }


        public  class Payload
        {
            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("subject")]
            public string Subject { get; set; }

            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("priority")]
            public long Priority { get; set; }

            [JsonProperty("status")]
            public long Status { get; set; }

            [JsonProperty("cc_emails")]
            public List<string> CcEmails { get; set; }
            //[JsonProperty("custom_fields")]
            //public WxmSurveyResponse CustomFields { get; set; }
        }

        public enum Source {
            Email=	1
            ,Portal=	2
            ,Phone=	3
            ,Chat=	7
            ,FeedbackWidget	=9
            ,OutboundEmail=	10
        }

        public enum Status
        {
            Open	=2
            ,Pending	=3
            ,Resolved =4
            ,Closed	=5
        }

        public enum Priority
        {
            Low	=1
            ,Medium	= 2
            ,High	= 3
            ,Urgent	= 4

        }
    }
}
