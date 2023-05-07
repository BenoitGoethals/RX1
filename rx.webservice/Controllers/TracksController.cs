using Microsoft.AspNetCore.Mvc;
using rx.core;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace rx.webservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TracksController : ControllerBase
    {
        private readonly TrackEngine _trackEngine; 
        private ILogger<TracksController> _logger;
        public TracksController(TrackEngine trackEngine,ILogger<TracksController> logger)
        {
            this._trackEngine = trackEngine;
            _logger = logger;
        }

        // GET: api/<TracksController>
        [HttpGet(Name = "Start")]
        public async  Task<string[]> Start()
        {
            if (!_trackEngine.IsRunning)
            {
                await Task.Run(() => _trackEngine.Start());
            }
            return  new string[] { "value1", "value2" };
        }

    

        // GET api/<TracksController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TracksController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TracksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TracksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
