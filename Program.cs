using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ReadoraProject.Data;
using ReadoraProject.Interface;
using ReadoraProject.Repository;
using ReadoraProject.Services;
using System.Text;
using static ReadoraProject.Data.ReadoraDbContext;

var builder = WebApplication.CreateBuilder(args);
// Add Repository
builder.Services.AddScoped<IUserInterface, UserRepository>();
//profile 
builder.Services.AddScoped<IProfileInterface, ProfileRepository>();
builder.Services.AddScoped<ProfileApiService>();
//Interaction
builder.Services.AddScoped<IInteractionInterface, InteractionRepository>();

// CATEGORY MODULE START
builder.Services.AddScoped<ICategoryInterface, CategoryRepository>();
builder.Services.AddScoped<CategoryApiService>();
//content
builder.Services.AddScoped<IContentInterface, ContentRepository>();
builder.Services.AddScoped<ContentApiService>();
// ---
builder.Services.AddScoped<IAdminService, AdminRepository>();
builder.Services.AddHttpClient<AdminApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7293");
});
// Register ApiSettings
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
// Add HttpClient with BaseUrl from apisettings.json
builder.Services.AddHttpClient("MyApi", (sp, client) => 
{ 
    var settings = sp.GetRequiredService<IOptions<ApiSettings>>().Value; 
    client.BaseAddress = new Uri(settings.BaseUrl);
});
// Register API Service for Razor Pages
builder.Services.AddScoped<UserApiService>(); 

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen(c =>
{
    c.TagActionsBy(api => new[] { api.GroupName ?? "Default" });
    c.DocInclusionPredicate((docName, apiDesc) => true);
});


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Ye sabse important hai!
});



builder.Services.AddScoped<SupportRepository>();
builder.Services.AddHttpClient<ISupportService, SupportApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7293");
});
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7293"); // ?? Change port if needed
}); ;
builder.Services.AddControllers();
// Add DbContext
builder.Services.AddDbContext<ReadoraDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer",
        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Enter: Bearer {your token}"
        });

    options.AddSecurityRequirement(
        new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
});

var jwtKey = builder.Configuration["Jwt:Key"];

if (string.IsNullOrEmpty(jwtKey))
{
    throw new Exception("JWT Key is missing in appsettings.json");
}
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        //IssuerSigningKey =
        //    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };


});
var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();

app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();


app.Run();
