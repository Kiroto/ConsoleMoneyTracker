using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.controller
{
    public class HttpWrapper
    {
        public static Task<string> HttpGet(string uri)
        {
            HttpClient client = new HttpClient();
            return client.GetStringAsync(uri);
        }

        public static Task<string> HttpPost(string uri, Dictionary<string, string>? requestBody = null)
        {
            if (requestBody == null) requestBody = new Dictionary<string, string>();
            Task<string> task = new Task<string>(delegate
            {
                HttpClient client = new HttpClient();
                HttpContent body = new FormUrlEncodedContent(requestBody);
                Task<HttpResponseMessage> postTask = client.PostAsync(uri, body);
                HttpResponseMessage message = postTask.Result;
                HttpContent messageContent = message.Content;
                return messageContent.ReadAsStringAsync().Result;
            });
            task.Start();
            return task;
        }
    }
}
