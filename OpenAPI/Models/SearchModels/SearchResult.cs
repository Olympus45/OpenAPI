using OpenAPI.Models.SwapiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenAPI.Models.SearchModels
{
    public class SearchResult
    {
        public ChuckSearchResponse Chuck_results { get; set; }
        public SwapiSearchResponse Swapi_results { get; set; }
    }
}
