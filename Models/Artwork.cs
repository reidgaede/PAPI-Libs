using System.Text.Json.Serialization;

namespace PAPI_Libs.Models;

/* (3/18/24, 3) I ended up using "https://wtools.io/convert-json-to-csharp-class" to "reverse-engineer" this C# class from an example JSON object 
returned from The Metropolitan Museum of Art's Collection API. Let's PRAY that it works (...): */

public class ConstituentsItem
{
        [JsonPropertyName("constituentID")]
        public int ConstituentID { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("constituentULAN_URL")]
        public string ConstituentULAN_URL { get; set; }

        [JsonPropertyName("constituentWikidata_URL")]
        public string ConstituentWikidata_URL { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }
}

public class ElementMeasurements
{
        [JsonPropertyName("Height")]
        public double Height { get; set; }
}

public class MeasurementsItem
{
        [JsonPropertyName("elementName")]
        public string ElementName { get; set; }

        [JsonPropertyName("elementDescription")]
        public string ElementDescription { get; set; }

        [JsonPropertyName("elementMeasurements")]
        public ElementMeasurements ElementMeasurements { get; set; }
}

/* (3/21/24, 7) VERY, VERY, VERY IMPORTANT! See your notes in the Word document "(3-16-24) Selected ID Values 
from The Metropolitan Museum of Art’s Collection API" to see WHY you had to (partially) "re-reverse-engineer" 
the `Artwork` model in specific regard to the `tags` property (!): */
public class TagsItem
{
        [JsonPropertyName("term")]
        public string Term { get; set; }

        [JsonPropertyName("AAT_URL")]
        public string AAT_URL { get; set; }

        [JsonPropertyName("Wikidata_URL")]
        public string Wikidata_URL { get; set; }
}

public class Artwork
{
        [JsonPropertyName("objectID")]
        public int ObjectID { get; set; }

        /* (3/21/24, 4) Yeah... When testing the PAPI_Lib API, IF a given PAPI_Lib generation involves a `GET` call to The 
        Metropolitan Museum of Art's Collection API, it is returning some error in the Swagger UI saying that the value
        `True` or `False` for `isHighlight` returned from the API cannot be represented as a string. My guess is that the 
        data type for this property SHOULD instead be `bool` (and that I will have to REPEATEDLY test the API now just to 
        make sure that no other properties of the `Artwork` class were assigned errant data types during the JSON-to-C# 
        reverse-engineering process...): */
        [JsonPropertyName("isHighlight")]
        public bool IsHighlight { get; set; }

        [JsonPropertyName("accessionNumber")]
        public string AccessionNumber { get; set; }

        [JsonPropertyName("accessionYear")]
        public string AccessionYear { get; set; }
        /* (3/21/24, 5) Yup... Looks like the fear I expressed in the above comment may be true. AS A PRECAUTION, 
        PRE-EMPTIVELY changing the data type of ANY property in the `Artwork` class prefaced by "is" from whatever it is 
        listed as now (`string`, `int`, etc.) to `bool`. HOPEFULLY this should fix the issue(s): */
        public bool IsPublicDomain { get; set; }

        [JsonPropertyName("primaryImage")]
        public string PrimaryImage { get; set; }

        [JsonPropertyName("primaryImageSmall")]
        public string PrimaryImageSmall { get; set; }

        [JsonPropertyName("additionalImages")]
        public List <string> AdditionalImages { get; set; }

        [JsonPropertyName("constituents")]
        public List <ConstituentsItem> Constituents { get; set; }

        [JsonPropertyName("department")]
        public string Department { get; set; }

        [JsonPropertyName("objectName")]
        public string ObjectName { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("culture")]
        public string Culture { get; set; }

        [JsonPropertyName("period")]
        public string Period { get; set; }

        [JsonPropertyName("dynasty")]
        public string Dynasty { get; set; }

        [JsonPropertyName("reign")]
        public string Reign { get; set; }

        [JsonPropertyName("portfolio")]
        public string Portfolio { get; set; }

        [JsonPropertyName("artistRole")]
        public string ArtistRole { get; set; }

        [JsonPropertyName("artistPrefix")]
        public string ArtistPrefix { get; set; }

        [JsonPropertyName("artistDisplayName")]
        public string ArtistDisplayName { get; set; }

        [JsonPropertyName("artistDisplayBio")]
        public string ArtistDisplayBio { get; set; }

        [JsonPropertyName("artistSuffix")]
        public string ArtistSuffix { get; set; }

        [JsonPropertyName("artistAlphaSort")]
        public string ArtistAlphaSort { get; set; }

        [JsonPropertyName("artistNationality")]
        public string ArtistNationality { get; set; }

        [JsonPropertyName("artistBeginDate")]
        public string ArtistBeginDate { get; set; }

        [JsonPropertyName("artistEndDate")]
        public string ArtistEndDate { get; set; }

        [JsonPropertyName("artistGender")]
        public string ArtistGender { get; set; }

        [JsonPropertyName("artistWikidata_URL")]
        public string ArtistWikidata_URL { get; set; }

        [JsonPropertyName("artistULAN_URL")]
        public string ArtistULAN_URL { get; set; }

        [JsonPropertyName("objectDate")]
        public string ObjectDate { get; set; }

        [JsonPropertyName("objectBeginDate")]
        public int ObjectBeginDate { get; set; }

        [JsonPropertyName("objectEndDate")]
        public int ObjectEndDate { get; set; }
        
        [JsonPropertyName("medium")]
        public string Medium { get; set; }

        [JsonPropertyName("dimensions")]
        public string Dimensions { get; set; }
        
        [JsonPropertyName("measurements")]
        public List <MeasurementsItem> Measurements { get; set; }

        [JsonPropertyName("creditLine")]
        public string CreditLine { get; set; }

        [JsonPropertyName("geographyType")]
        public string GeographyType { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("county")]
        public string County { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("subregion")]
        public string Subregion { get; set; }

        [JsonPropertyName("locale")]
        public string Locale { get; set; }

        [JsonPropertyName("locus")]
        public string Locus { get; set; }

        [JsonPropertyName("excavation")]
        public string Excavation { get; set; }

        [JsonPropertyName("river")]
        public string River { get; set; }

        [JsonPropertyName("classification")]
        public string Classification { get; set; }

        [JsonPropertyName("rightsAndReproduction")]
        public string RightsAndReproduction { get; set; }

        [JsonPropertyName("linkResource")]
        public string LinkResource { get; set; }

        [JsonPropertyName("metadataDate")]
        public string MetadataDate { get; set; }

        [JsonPropertyName("repository")]
        public string Repository { get; set; }

        [JsonPropertyName("objectURL")]
        public string ObjectURL { get; set; }

        /* (3/21/24, 7) VERY, VERY, VERY IMPORTANT! See your notes in the Word document "(3-16-24) Selected ID Values 
        from The Metropolitan Museum of Art’s Collection API" to see WHY you had to (partially) "re-reverse-engineer" 
        the `Artwork` model in specific regard to this `tags` property (!): */
        [JsonPropertyName("tags")]
        public List<TagsItem>? Tags { get; set; }

        [JsonPropertyName("objectWikidata_URL")]
        public string ObjectWikidata_URL { get; set; }

        /* (3/21/24, 6) Changing the data type for this class property from `string` to `bool` as described in previous 
        comments in this file: */
        [JsonPropertyName("isTimelineWork")]
        public bool IsTimelineWork { get; set; }

        [JsonPropertyName("GalleryNumber")]
        public string GalleryNumber { get; set; }
}
