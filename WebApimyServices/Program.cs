using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add Services to the container.
builder.Services.AddControllers();

// Add Expired Token Service
builder.Services.AddTransient<IJwtUtils, JwtUtils>();

// Add Categorey Service
builder.Services.AddTransient<ICategoryService, CategoryService>();

// Add Problem Service
builder.Services.AddTransient<IProblemService, ProblemService>();

// Add User Service
builder.Services.AddTransient<IUserService, UserService>();

// Add Role Policy With Handler
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("GlobalVerbRolePolicy", policy =>
        policy.Requirements.Add(new GlobalVerbRoleRequirement()));
});
builder.Services.AddSingleton<IAuthorizationHandler, GlobalVerbRoleHandler>();


// Identity userManger
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(services =>
{
    services.Password.RequireNonAlphanumeric = false;
    services.Password.RequireLowercase = false;
    services.Password.RequireUppercase = false;
    services.Password.RequireDigit = false;
    services.Password.RequiredUniqueChars = 0;
    services.Password.RequiredLength = 8;
    services.User.RequireUniqueEmail = true;
    services.Lockout.AllowedForNewUsers = true;
    services.Lockout.MaxFailedAccessAttempts = 5;
    services.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    services.SignIn.RequireConfirmedEmail = true;
    services.SignIn.RequireConfirmedAccount = true;
})
    .AddRoles<IdentityRole>()
    .AddUserValidator<EmailValidator>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// DBContext Connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
});


// [Authoriz] use JWT Token In Check Authanttiaction
var jwtOptions = builder.Configuration.GetSection("JWT").Get<JwtOptions>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = jwtOptions.Issuer,
        ValidateAudience = false,
        ValidAudience = jwtOptions.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey))
    };
});

// Reset Token Life 1 Hour
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(1);
});

// Add Custom Cors
builder.Services.AddCors(corsOptions =>
{
    corsOptions.AddPolicy("MyPolicy", corsPolicyBuilder =>
    {
        corsPolicyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Add Automapper
builder.Services.AddAutoMapper(typeof(Program));

// Add Auth Service
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JWT"));
builder.Services.AddScoped<IAuthService, AuthService>();

// Add Email Configuration
builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));
builder.Services.AddTransient<IEmailService, EmailService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(service =>
{
    service.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,$"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

    service.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
        Title = "myService Application APIs",
        Description = "API Documentation For myService Project"
    });

    service.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter The JWT Key !",
    });

    service.AddSecurityRequirement(new OpenApiSecurityRequirement() {
    {
        new OpenApiSecurityScheme()
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
            Name = "Bearer",
            In = ParameterLocation.Header
        },
        new List<string>()
        } });
});

var app = builder.Build();

app.UseSwagger();
if (app.Environment.IsDevelopment())
    app.UseSwaggerUI();
else
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });

app.UseStaticFiles();
 
app.UseCors("MyPolicy");

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();