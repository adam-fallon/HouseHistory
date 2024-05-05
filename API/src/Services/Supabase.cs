namespace HouseHistory.Dependencies;

public interface ISupabaseService
{
    Task<Supabase.Client> GetClient();
}

public class SupabaseServiceImpl : ISupabaseService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;

    private Supabase.Client _client;

    public SupabaseServiceImpl(IConfiguration configuration, ILogger<SupabaseServiceImpl> logger)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<Supabase.Client> GetClient()
    {
        if (_client == null)
        {
            _logger.LogInformation("Creating new Supabase client");
            var url = _configuration["Supabase:0:Url"];
            var key = _configuration["Supabase:0:Key"];

            _logger.LogInformation($"Connecting to Supabase at {url}");

            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            _client = new Supabase.Client(url, key, options);
            await _client.InitializeAsync();
        } else {
            _logger.LogInformation("Reusing existing Supabase client");
        }

        return _client;


    }
}