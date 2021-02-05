using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenAPI.Models.SearchModels;
using RestSharp;

namespace OpenAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        /// <summary>
        /// Gets both the Chuck Norris and Star Wars APIs
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Invalid status value</response> 
        [HttpGet("{query}")]
        [ProducesResponseType(typeof(SearchResponse), 200)]
        [Produces("application/json")]
        public IActionResult Index(string query)
        {
            if (string.IsNullOrEmpty(query))
                return BadRequest();

            var model = new SearchResponse();

            var chuckClient = new RestClient($"https://api.chucknorris.io/jokes/search?query={query}");
            var chuckRequest = new RestRequest(Method.GET);
            var chuckResponse = chuckClient.Execute(chuckRequest);
            model.Chuck_results = chuckResponse.IsSuccessful ? JsonConvert.DeserializeObject<ChuckSearchResponse>(chuckResponse.Content) : new ChuckSearchResponse();

            var swapiClient = new RestClient($"https://swapi.dev/api/people/?search={query}");
            var swapiRequest = new RestRequest(Method.GET);
            var swapiResponse = swapiClient.Execute(swapiRequest);
            model.Swapi_results = swapiResponse.IsSuccessful ? JsonConvert.DeserializeObject<SwapiSearchResponse>(swapiResponse.Content) : new SwapiSearchResponse();

            return Ok(model);
        }
    }
}