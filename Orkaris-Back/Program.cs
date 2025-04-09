using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Orkaris_Back.Mapping;
using Orkaris_Back.Models.DataManager;
using Orkaris_Back.Models.EntityFramework;
using Orkaris_Back.Models.Repository;
using Orkaris_Back.Services;

var builder = WebApplication.CreateBuilder(args);

//JWT
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        ClockSkew = TimeSpan.Zero
    };
});

//Auto Mapping
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Data Mangers
builder.Services.AddScoped<IDataRepositoryString<User>, UserManager>();
builder.Services.AddScoped<IDataRepository<Category>, CategoryManager>();
builder.Services.AddScoped<IDataRepository<ExerciseGoal>, ExerciseGoalManager>();
builder.Services.AddScoped<IDataRepository<ExerciseGoalPerformance>, ExerciseGoalPerformanceManager>();
builder.Services.AddScoped<IDataRepository<Exercise>, ExerciseManager>();
builder.Services.AddScoped<IDataRepository<ExerciseCategory>, ExerciseCategoryManager>();
builder.Services.AddScoped<IDataRepository<SessionExercise>, SessionExerciseManager>();
builder.Services.AddScoped<IDataRepository<Session>, SessionManager>();
builder.Services.AddScoped<IDataRepository<SessionPerformance>, SessionPerformanceManager>();
builder.Services.AddScoped<IDataRepository<Sport>, SportManager>();
builder.Services.AddScoped<IDataRepository<Workout>, WorkoutManager>();
builder.Services.AddScoped<IDataRepositoryString<EmailConfirmationToken>, EmailManager>();




builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WorkoutDBContext>(options =>
               options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAny", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mon API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseCors(
        options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
    );


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
