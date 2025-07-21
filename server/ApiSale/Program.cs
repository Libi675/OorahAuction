

using ApiSale;
using ApiSale.BL;
using ApiSale.DAL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// הוספת שירותים
builder.Services.AddScoped<ICategotyDal, CategoryDal>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IDonorDal, DonorDal>();
builder.Services.AddScoped<IDonorService, DonorService>();
builder.Services.AddScoped<IGiftDal, GiftDal>();
builder.Services.AddScoped<IGiftService, GiftService>();
builder.Services.AddScoped<IAuthorizeDal, AuthorizeDal>();
builder.Services.AddScoped<IAuthorizeService, AuthorizeService>();
builder.Services.AddScoped<IOrderSevice, OrderService>();
builder.Services.AddScoped<IOrderDal, OrderDal>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// הוספת קונפיגורציות
builder.Services.AddControllers();

// הוספת Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // הגדרת Bearer Authentication
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid JWT token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// הוספת Database ו-Authentication
//builder.Services.AddDbContext<ChainaSaleDBContext>(option =>
   // option.UseSqlServer("Data Source=localhost;Initial Catalog=Chaina;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"));
//builder.Services.AddDbContext<ChainaSaleDBContext>(option => option.UseSqlServer("Data Source=SRV2\\PUPILS;Initial Catalog=Oorah;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"));
builder.Services.AddDbContext<ChainaSaleDBContext>(option => option.UseSqlServer("Data Source=localhost;Initial Catalog=Chaina;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod().AllowCredentials();
    });
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "AuthToken"; // שם הקוקי שלך
        options.LoginPath = "/Login";  // הנתיב שמפנים אליו במקרה של צורך בהתחברות מחדש
        options.LogoutPath = "/Logout"; // תוכל להוסיף גם נתיב זה אם תרצה להגדיר נתיב יציאה מותאם
    });

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
    };
    // Add custom logic to retrieve the token from cookies
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            if (context.Request.Cookies.TryGetValue("AuthToken", out var token))
            {
                context.Token = token;
            }
            return Task.CompletedTask;
        }
    };
});

var app = builder.Build();

// Middleware ו-Request Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.UseMiddleware<CookieToBearerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();
app.UseLoggerMiddlere();
app.MapControllers();

app.Run();



