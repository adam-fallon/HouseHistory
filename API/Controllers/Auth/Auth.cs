using HouseHistory.Models.Requests;
using HouseHistory.Dependencies;
using Supabase.Gotrue;

namespace HouseHistory.Routes.Auth
{
    public static class HouseHistoryAuthRoutes
    {
        public static IApplicationBuilder RegisterAuthRoutes(this IApplicationBuilder builder)
        {
            return builder.UseEndpoints(endpoints =>
            {
                endpoints.MapPost($"/api/v1/auth/login", async (ISupabaseService supabaseService, LoginRequest loginRequest) =>
                {
                    var supabase = builder.ApplicationServices.GetRequiredService<ISupabaseService>();
                    var client = await supabase.GetClient();
                    var session = await client.Auth.SignIn(loginRequest.Email, loginRequest.Password);
                    return session;
                });

                endpoints.MapPost($"/api/v1/auth/signup", async (ISupabaseService supabaseService, SignUpRequest signUpRequest) =>
                {
                    var supabase = builder.ApplicationServices.GetRequiredService<ISupabaseService>();
                    var client = await supabase.GetClient();
                    var options = new SignUpOptions
                    {
                        Data = new Dictionary<string, object>
                        {
                            { "username", signUpRequest.Username }
                        }
                    };

                    var result = await client
                       .Auth
                       .SignUp(signUpRequest.Email, signUpRequest.Password, options);

                    return result;
                });
            });
        }
    }
}