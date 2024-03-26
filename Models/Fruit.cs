using System.Text.Json.Serialization;

namespace PAPI_Libs.Models;

/* (3/17/24, 14) I obtained the below "code" for the `Fruit` class from https://wtools.io/convert-json-to-csharp-class: */
public class Nutritions
{
        [JsonPropertyName("calories")]
        public int Calories { get; set; }

        [JsonPropertyName("fat")]
        public double Fat { get; set; }

        [JsonPropertyName("sugar")]
        public double Sugar { get; set; }
        
        [JsonPropertyName("carbohydrates")]
        public double Carbohydrates { get; set; }

        [JsonPropertyName("protein")]
        public double Protein { get; set; }
}

public class Fruit
{
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("family")]
        public string Family { get; set; }

        [JsonPropertyName("order")]
        public string Order { get; set; }

        [JsonPropertyName("genus")]
        public string Genus { get; set; }

        [JsonPropertyName("nutritions")]
        public Nutritions Nutritions { get; set; }
}