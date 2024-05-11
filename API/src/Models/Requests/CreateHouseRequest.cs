namespace HouseHistory.Models.Requests
{
    public class CreateHouseRequest
    {
        public required string AddressName { get; set; }
        public required DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; } = null;
        public required string FirstLine { get; set; }
        public required string SecondLine { get; set; }
        public required string Country { get; set; }
        public required string StateRegion { get; set; }
        public required string Postcode { get; set; }
    }
}