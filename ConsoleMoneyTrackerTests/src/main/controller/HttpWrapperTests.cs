using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleMoneyTracker.src.main.controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMoneyTracker.src.main.controller.Tests
{
    [TestClass()]
    public class HttpWrapperTests
    {
        [TestMethod()]
        public void HttpGetTest()
        {
            Task<string> getTask = HttpWrapper.HttpGet("https://www.example.com");
            string result = getTask.Result;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void HttpPostTest()
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                { "q", "This data will be echoed!" }
            };

            Task<string> getTask = HttpWrapper.HttpPost("https://httpbin.org/post", data);
            string result = getTask.Result;
            Assert.IsNotNull(result);
        }
    }
}