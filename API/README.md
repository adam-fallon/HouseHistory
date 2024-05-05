## House History API

A RESTful API that allows a user to create a history of the houses they've lived in.

## Environment
You need to set the following variables;
N.b the weird naming is because this is how configuration works in dotnet. See more [here](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-8.0#naming-of-environment-variables)
    
    ```bash
    export SUPABASE__0__Url="<SUPABASE_URL>"
    export SUPABASE__0__Key="<SUPABASE_ANON_KEY>"
    ```

## Usage

1. Open a terminal.
2. Run the following command to execute the `run.sh` script:

    ```bash
    ./run.sh
    ```
3. Go to `http://localhost:8081` in your web browser.
