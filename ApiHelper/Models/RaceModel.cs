using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiHelper.Models
{
    public class RaceModel
    {
        [JsonProperty("message")]
        public Dictionary<string,List<string>> Races { get; set; }
    }
}
