using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace OpenAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ChuckController : ControllerBase
    {
        /// <summary>
        /// Returns all joke categories
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Invalid status value</response> 
        [HttpGet]
        [ProducesResponseType(typeof(List<string>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult Categories()
        {
            var client = new RestClient("https://api.chucknorris.io/jokes/categories");
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);
            if (!response.IsSuccessful)
                return BadRequest();

            var model = JsonConvert.DeserializeObject<List<string>>(response.Content);
            return Ok(model);
        }
    }
}