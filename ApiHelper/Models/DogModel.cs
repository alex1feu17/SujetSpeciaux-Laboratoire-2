using ApiHelper.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DogFetchApp.Models
{
    public class DogModel
    {

        public string Name { get; set; }

        [JsonProperty("message")]
        public string img { get; set; }
    }
}
