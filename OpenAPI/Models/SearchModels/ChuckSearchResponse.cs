using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenAPI.Models.SearchModels
{
    public class ChuckSearchResponse
    {
        public int Total { get; set; }
        public List<Joke> Result { get; set; }
    }
}
