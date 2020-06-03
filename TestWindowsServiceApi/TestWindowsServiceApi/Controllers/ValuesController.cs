using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestWindowsServiceApi.BackgroundService;
using TestWindowsServiceApi.Logging;

namespace TestWindowsServiceApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public TestResponse Post([FromBody]TestRequest request)
        {
            LogBackgroundService.LogsInstance.Add(Guid.NewGuid(), new Log
            {
                CorrelationId = Guid.NewGuid(),
                RequestBody = "test request body",
                RequestMethod = "TestRequestMethod",
                RequestPath = "Path"
            });


            return new TestResponse
            {
                CorrelationId = Guid.NewGuid(),
                Text = "Testing the post method",
                OutputText = request.InputText
            };
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class TestRequest
    {
        public string InputText { get; set; }
    }

    public class TestResponse
    {
        public Guid CorrelationId { get; set; }

        public string Text { get; set; }

        public string OutputText { get; set; }
    }
}
