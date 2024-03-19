namespace PAPI_Libs.Models;

/* (3/18/24, 3) I ended up using "https://wtools.io/convert-json-to-csharp-class" to "reverse-engineer" this C# class from an example JSON object 
returned from The Metropolitan Museum of Art's Collection API. Let's PRAY that it works (...): */

public class ConstituentsItem
{
        public int constituentID { get; set; }
        public string role { get; set; }
        public string name { get; set; }
        public string constituentULAN_URL { get; set; }
        public string constituentWikidata_URL { get; set; }
        public string gender { get; set; }
}

public class ElementMeasurements
{
        public double Height { get; set; }
}

public class MeasurementsItem
{
        public string elementName { get; set; }
        public string elementDescription { get; set; }
        public ElementMeasurements elementMeasurements { get; set; }
}

public class Artwork
{
        public int objectID { get; set; }
        public string isHighlight { get; set; }
        public string accessionNumber { get; set; }
        public string accessionYear { get; set; }
        public string isPublicDomain { get; set; }
        public string primaryImage { get; set; }
        public string primaryImageSmall { get; set; }
        public List <string> additionalImages { get; set; }
        public List <ConstituentsItem> constituents { get; set; }
        public string department { get; set; }
        public string objectName { get; set; }
        public string title { get; set; }
        public string culture { get; set; }
        public string period { get; set; }
        public string dynasty { get; set; }
        public string reign { get; set; }
        public string portfolio { get; set; }
        public string artistRole { get; set; }
        public string artistPrefix { get; set; }
        public string artistDisplayName { get; set; }
        public string artistDisplayBio { get; set; }
        public string artistSuffix { get; set; }
        public string artistAlphaSort { get; set; }
        public string artistNationality { get; set; }
        public string artistBeginDate { get; set; }
        public string artistEndDate { get; set; }
        public string artistGender { get; set; }
        public string artistWikidata_URL { get; set; }
        public string artistULAN_URL { get; set; }
        public string objectDate { get; set; }
        public int objectBeginDate { get; set; }
        public int objectEndDate { get; set; }
        public string medium { get; set; }
        public string dimensions { get; set; }
        public List <MeasurementsItem> measurements { get; set; }
        public string creditLine { get; set; }
        public string geographyType { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string county { get; set; }
        public string country { get; set; }
        public string region { get; set; }
        public string subregion { get; set; }
        public string locale { get; set; }
        public string locus { get; set; }
        public string excavation { get; set; }
        public string river { get; set; }
        public string classification { get; set; }
        public string rightsAndReproduction { get; set; }
        public string linkResource { get; set; }
        public string metadataDate { get; set; }
        public string repository { get; set; }
        public string objectURL { get; set; }
        public string tags { get; set; }
        public string objectWikidata_URL { get; set; }
        public string isTimelineWork { get; set; }
        public string GalleryNumber { get; set; }
}
