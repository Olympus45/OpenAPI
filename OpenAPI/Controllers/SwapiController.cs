using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenAPI.Models.SwapiModels;
using RestSharp;

namespace OpenAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class SwapiController : ControllerBase
    {
        /// <summary>
        /// Gets all the Star Wars people
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Invalid status value</response> 
        [HttpGet]
        [ProducesResponseType(typeof(SwapiResponse), 200)]
        [Produces("application/json")]
        public IActionResult People()
        {
            var client = new RestClient("https://swapi.dev/api/people/");
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);
            if (!response.IsSuccessful)
                return BadRequest();

            var model = JsonConvert.DeserializeObject<SwapiResponse>(response.Content);
            return Ok(model);
        }
    }
}