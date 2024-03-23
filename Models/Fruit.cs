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
        /* (3/20/24, 5) For reasons partially lost to me, Ernesto recommended that I TRY to make the data type of each 
        property in my API response models match the (perceived) data type of each "member" (correct term?) of the JSON-
        structured response that these models are designed to imitate in my program. As such, while ORIGINALLY the 
        `carbohydrates` property WAS of type `int`, we switched it to type `double` after looking at the returned JSON 
        object and seeing that the value assigned to `carbohydrates` had a decimal point: */
        [JsonPropertyName("carbohydrates")]
        public double Carbohydrates { get; set; }

        [JsonPropertyName("protein")]
        public double Protein { get; set; }
}

public class Fruit
{
        /* (3/20/24, 1) THANK GOODNESS you tested out capitalizing the first letter of each property in this class as 
        Ernesto recommended per C# naming rules! It is rather inconvenient, but what happened was that when you changed 
        the `name` attribute below from "name" to "Name", the code in your `POST` action STOPPED functioning correctly! 
        As such, re-lower-casing all the property names in this model just to be safe. Again, rather inconvenient, but 
        VERY glad that I figured this out now and NOT later (!) -> (3/21/24, 8) It SEEMS like you've got this figured 
        out now (for reference, see your comments in "Artwork.cs"): */
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