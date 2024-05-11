
using HouseHistory.Models.Requests;
using HouseHistory.Dependencies;
using Supabase.Gotrue;

namespace HouseHistory.Routes.Auth
{
    public static class HouseHistoryHousesRoutes
    {
        public static IApplicationBuilder RegisterHousesRoutes(this IApplicationBuilder builder)
        {
            return builder.UseEndpoints(endpoints =>
            {


                endpoints.MapPost($"/api/v1/houses", async (ISupabaseService supabaseService, CreateHouseRequest createHouseRequest) =>
                {
                    var supabase = builder.ApplicationServices.GetRequiredService<ISupabaseService>();
                    var client = await supabase.GetClient();

                    var house = House.Map(createHouseRequest);

                    var query = await client
                        .From<House>()
                        .Get();
                    Console.WriteLine(query);

                    // var result = await client
                    //     .From<House>()
                    //     .Insert(house);
                    // Console.WriteLine(house);
                    // return result;
                });
            });
        }
    }
}