namespace Scraping_Egy_Bus.Entities
{
    public class AdditionalTripData : BaseModel
    {
        public int TripDataId { get; set; }
        public int TripId { get; set; }



        public string Data { get; set; } = "{}";       //Json Object

        //  public JsonValue? Data { get; set; }

        //public JsonArray? Features { get; set; } = new JsonArray();

        public string Features { get; set; } = "[]";  //JsonArray

        // Foreign Key Navigation
        public Trip Trip { get; set; } = null!;
    }
}
