using ApiHelper.Models;
using DogFetchApp.Models;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using static DogFetchApp.Models.DogModel;

namespace ApiHelper
{
    public class DogApiProcessor
    {

        public static async Task<List<DogModel>> LoadBreedList()
        {
            ///TODO : À compléter LoadBreedList
            /// Attention le type de retour n'est pas nécessairement bon
            /// J'ai mis quelque chose pour avoir une base
            /// TODO : Compléter le modèle manquant
            /// 
           

            string url;

            url = $"https://dog.ceo/api/breeds/list/all";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<DogModel> dog_model = new List<DogModel>();

                    RaceModel result = await response.Content.ReadAsAsync<RaceModel>();

                    var famile = result.Races.Keys.ToList();

                    foreach(var race in famile)
                    {
                        DogModel breed = new DogModel();
                        breed.Name = race;
                        dog_model.Add(breed);
                    }
                    return dog_model;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }



            return new List<DogModel>();
        }
        public static string PrettyJson(string unPrettyJson)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            var jsonElement = System.Text.Json.JsonSerializer.Deserialize<DogModel>(unPrettyJson);

            return System.Text.Json.JsonSerializer.Serialize(jsonElement, options);
        }
        public static async Task<DogModel> GetImageUrl(string breed)
        {
            string url = " ";

            url = $"https://dog.ceo/api/breed/{breed}/images/random";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    DogModel result = await response.Content.ReadAsAsync<DogModel>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
            /// TODO : GetImageUrl()
            /// TODO : Compléter le modèle manquant
            
        }
    }
}
