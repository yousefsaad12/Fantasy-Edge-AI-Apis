

using System.Text.Json.Serialization;
using Api.Models.UserModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

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

//Log.Logger = new LoggerConfiguration()
    //.ReadFrom.Configuration(builder.Configuration)  // Read the configuration from appsettings.json
    //.CreateLogger();


builder.Logging.ClearProviders();
builder.Logging.AddSerilog();


builder.Services.AddIdentity<User,IdentityRole>(options => {
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 12;
    
}).AddEntityFrameworkStores<AppDbContext>()
  .AddDefaultTokenProviders();

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
    var data = await fantasyService.FetchDataAsync(6);  // Adjust model as necessary
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


builder.Services.AddAuthentication(options => {

    options.DefaultAuthenticateScheme = 
    options.DefaultChallengeScheme = 
    options.DefaultForbidScheme = 
    options.DefaultScheme = 
    options.DefaultSignInScheme = 
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options => {

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey 
                            (System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        
    };
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
