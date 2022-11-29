using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace google_server_to_server_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreNotificationController : ControllerBase
    {
        // GET: api/<StoreNotificationController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<StoreNotificationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StoreNotificationController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StoreNotificationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StoreNotificationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
