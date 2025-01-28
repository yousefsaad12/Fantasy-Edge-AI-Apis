
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFetchingService, FetchingService>();
builder.Services.AddScoped<IPlayerServices, PlayerServices>();
builder.Services.AddScoped<ITeamsServices, TeamServices>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<ITokenServices, TokenServices>();
builder.Services.AddScoped<IModelServices, ModelServices>();
builder.Services.AddScoped<ICacheServices, CacheServices>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepo>();

builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddStackExchangeRedisCache(option => {
    option.Configuration = builder.Configuration.GetConnectionString("Redis");
    option.InstanceName = "Predictions";
});

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddHttpClient();

// /////////// logger ////////////

// Log.Logger = new LoggerConfiguration()
//     .ReadFrom.Configuration(builder.Configuration)  // Read the configuration from appsettings.json
//     .CreateLogger();


builder.Logging.ClearProviders();
builder.Logging.AddSerilog();


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


builder.Services.AddIdentity<User,IdentityRole>(options => {
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 12;
    
}).AddEntityFrameworkStores<AppDbContext>()
  .AddDefaultTokenProviders();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("https://fantasy-edge-ai.vercel.app") // Replace with your frontend's URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();



app.UseCors("AllowSpecificOrigin");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
