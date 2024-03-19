namespace PAPI_Libs.Models;

/* (3/17/24, 14) Man... I have SO little faith in what I am trying to do, but I have no other options 
at this point in regard to deserializing JSON I am getting back from APIs. I obtained the below "code" 
for the `Fruit` class from https://wtools.io/convert-json-to-csharp-class: */
public class Nutritions
{
        public int calories { get; set; }
        public double fat { get; set; }
        public double sugar { get; set; }
        public int carbohydrates { get; set; }
        public double protein { get; set; }
}

public class Fruit
{
        public string name { get; set; }
        public int id { get; set; }
        public string family { get; set; }
        public string order { get; set; }
        public string genus { get; set; }
        public Nutritions nutritions { get; set; }
}