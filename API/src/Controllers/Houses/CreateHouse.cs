
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


                endpoints.MapPost($"/api/v1/houses", async (HttpContext context, ISupabaseService supabaseService, CreateHouseRequest createHouseRequest) =>
                {
                    var supabase = builder.ApplicationServices.GetRequiredService<ISupabaseService>();
                    var client = await supabase.GetClient();

                    Console.WriteLine(createHouseRequest);
                    var accessToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                    var refreshToken = context.Request.Headers["X-RefreshToken"].ToString();

                    var user = await client.Auth.SetSession(accessToken, refreshToken);

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