using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add Services to the container.
builder.Services.AddControllers();

// Add Revoked Token Service
builder.Services.AddTransient<IRevokedTokensService, RevokedTokensService>();

// Add Categorey Service
builder.Services.AddTransient<ICategoryService, CategoryService>();

// Add Problem Service
builder.Services.AddTransient<IProblemService, ProblemService>();

// Add User Service
builder.Services.AddTransient<IUserService, UserService>();

// Add Rate Service
builder.Services.AddTransient<IRateService, RateService>();

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

// test
builder.Services.AddHttpContextAccessor();

// Reset Token Life 1 Hour
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(1);
});

// Identity Provider
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Tokens.PasswordResetTokenProvider = "Email";
    options.Tokens.EmailConfirmationTokenProvider = "Email";
});

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
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtOptions.Issuer,
        ValidAudience = jwtOptions.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey))
    };
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

// Email Service
builder.Services.AddTransient<IEmailService, EmailService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(service =>
{
    // Add XML Files
    service.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,$"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

    service.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
        Title = "HELIOS Application APIs",
        Description = "API Documentation For HELIOS Project"
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

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Configure Hangfire
builder.Services.AddHangfire(config => config
   .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
   .UseSimpleAssemblyNameTypeSerializer()
   .UseRecommendedSerializerSettings()
   .UseSqlServerStorage(builder.Configuration.GetConnectionString("cs"), new SqlServerStorageOptions
   {
       CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
       SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
       QueuePollInterval = TimeSpan.Zero,
       UseRecommendedIsolationLevel = true,
       DisableGlobalLocks = true
   }));

builder.Services.AddHangfireServer();
builder.Services.AddScoped<HangfireService>();

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
 
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("MyPolicy");

app.UseHangfireDashboard("/dashborad");

// Handfire Job Remove Account
RecurringJob.AddOrUpdate<HangfireService>(
    "CheckAndRemoveUnconfirmedUsers",
    service => service.CheckAndRemoveUnconfirmedUsers(),
    Cron.HourInterval(1));

//Validate the token in middleware
app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value;

    // List of endpoints to bypass token validation
    var excludedEndpoints = new List<string>
    {
        "/Auth/FactorRegistration",
        "/Auth/CustomerRegistration",
        "/Address",
        "/Category",
        "/Problem/GetProblems",
        "/Problem/GetProblemsById",
        "/Users/GetUserInCustomerRole",
        "/Users/GetUsersInFactorRole",
        "/Auth/Login",
        "/Auth/RefreshToken",
        "/Auth/SendEmailResetPassword",
        "/Auth/ResetPassword",
        "/Auth/ConfirmEmail"
    };

    if (EndpointValidator.IsExcludedEndpoint(path, excludedEndpoints))
    {
        await next();
        return;
    }

    var authorizationHeader = context.Request.Headers["Authorization"].ToString();
    if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Invalid Authorization header format.");
        return;
    }

    var token = authorizationHeader.Substring("Bearer ".Length).Trim();
    var tokenHandler = new JwtSecurityTokenHandler();

    try
    {
        var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey)),
            ValidateIssuer = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtOptions.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        }, out SecurityToken validatedToken);

        var jwtToken = validatedToken as JwtSecurityToken;
        var tokenId = jwtToken?.Id;

        var revokedTokensService = context.RequestServices.GetRequiredService<IRevokedTokensService>();

        if (revokedTokensService.IsTokenRevoked(tokenId))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Token is revoked.");
            return;
        }

        context.User = claimsPrincipal;
    }
    catch (SecurityTokenException)
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Invalid token.");
        return;
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("An unexpected error occurred.");
        // Log the exception
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while validating the token.");
        return;
    }

    await next();
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();