

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFetchingService, FetchingService>();
builder.Services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));

builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


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
    var data = await fantasyService.FetchDataAsync(url);  // Adjust model as necessary
    return Results.Ok(data);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
