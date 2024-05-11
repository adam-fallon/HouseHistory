
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
                    // TODO: Add RLS
                    // TODO: Auth user based on headers here.
                    var house = House.Map(createHouseRequest);
                    
                    var result = await client
                        .From<House>()
                        .Insert(house);
                    return result;
                });
            });
        }
    }
}