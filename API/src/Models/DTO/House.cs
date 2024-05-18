using HouseHistory.Models.Requests;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

[Table("Houses")]
class House : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }

    [Column("addressName")]
    public string AddressName { get; set; }

    [Column("toDate")]
    public DateTime? ToDate { get; set; }

    [Column("fromDate")]
    public DateTime FromDate { get; set; }

    [Column("firstLine")]
    public string FirstLine { get; set; }

    [Column("secondLine")]
    public string SecondLine { get; set; }

    [Column("country")]
    public string Country { get; set; }

    [Column("stateRegion")]
    public string StateRegion { get; set; }

    [Column("postcode")]
    public string Postcode { get; set; }

    public static House Map(CreateHouseRequest request)
    {
        return new House
        {
            AddressName = request.AddressName,
            ToDate = request.ToDate,
            FromDate = request.FromDate,
            FirstLine = request.FirstLine,
            SecondLine = request.SecondLine,
            Country = request.Country,
            StateRegion = request.StateRegion,
            Postcode = request.Postcode
        };
    }
}

