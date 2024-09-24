

using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFetchingService, FetchingService>();
builder.Services.AddScoped<IPlayerServices, PlayerServices>();
builder.Services.AddScoped<ITeamsServices, TeamServices>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddHttpClient();

/////////// logger ////////////

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)  // Read the configuration from appsettings.json
    .CreateLogger();


builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/fantasy", async ([FromServices] IFetchingService fantasyService) =>
{
    var url = "https://fantasy.premierleague.com/api/bootstrap-static/";
    var data = await fantasyService.FetchDataAsync(3);  // Adjust model as necessary
    return Results.Ok(data);
});


















app.MapGet("/stat", async ([FromServices] IFetchingService fantasyService) =>
{
    var url = "https://fantasy.premierleague.com/api/event/3/live/";
    var data = await fantasyService.FetchPerformAsync(1);  // Adjust model as necessary
    return Results.Ok(data);
});


app.MapGet("/fantasy/players", async ([FromServices] IPlayerServices playerServices) =>
{
    
    var data = await playerServices.GetPlayersAsync();  // Adjust model as necessary
    return Results.Ok(data);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
