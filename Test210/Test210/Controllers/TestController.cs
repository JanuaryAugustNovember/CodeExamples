
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Test210.Infrastructure;
using Test210.Logging;

namespace Test210.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IElasticRepository _elasticRepo;
        private readonly IApplication _application;

        public TestController(IElasticRepository elasticRepo, IApplication application)
        {
            _elasticRepo = elasticRepo;
            _application = application;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return BadRequest();

            //TODO: remove the above? I think this was here to test a response
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            var test = _application.TestTuples("john", "doe");

            //TODO: this was previously returning a string? maybe forgot
            return test.FirstName;
        }

        // POST api/values
        [HttpPost]
        public TestResponse Post([FromBody]TestRequest request)
        {
            return new TestResponse
            {
                CorrelationId = Guid.NewGuid(),
                Text = "Testing the post method",
                OutputText = request.InputText
            };
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost("log")]
        public void SaveLog([FromBody]string inputText)
        {
            var testLog = new ApplicationLog
            {
                CorrelationId = Guid.NewGuid(),
                log = new TestRequest
                {
                    InputText = inputText
                },
                Exception = "test exception",
                ExceptionLevel = "Controller",
                LogType = LogType.Default,
                Timestamp = DateTime.Now

            };

            _elasticRepo.SaveApplicationLog(testLog);
        }

        [HttpPost("testclient")]
        public TestResponse TestRestClient([FromBody]TestRequest request)
        {
            var restService = new RestService("http://localhost:5003/api/Test"); //Values

            var response = restService.SendRequest<TestRequest, TestResponse>(new TestRequest
            {
                InputText = request.InputText
            }, RestSharp.Method.POST);

            return response;
        }
    }

    public class TestRequest
    {
        public string InputText { get; set; }

        public string User { get; set; }
    }

    public class TestResponse
    {
        public Guid CorrelationId { get; set; }

        public string Text { get; set; }

        public string OutputText { get; set; }
    }
}
