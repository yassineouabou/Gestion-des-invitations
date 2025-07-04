using api.Data;
using api.Repository;
using api.Repository.Interfaces;
using api.Services;
using api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Ajout d'une politique CORS nomm�e
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.Local.json", optional: true, reloadOnChange: true) 
    .AddEnvironmentVariables();


// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());// Afficher enum string
}); 


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "db";
var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "gestion_invitation";
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD") ?? "yassine@2021";
var dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "1433";

var connectionString = $"Server={dbHost},{dbPort};Database={dbName};User Id=sa;Password={dbPassword};TrustServerCertificate=True;";
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));



//builder.Services.AddDbContext<AppDbContext>(
//    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
//    );

builder.Services.AddScoped<IOrganisateurRepository, OrganisateurRepository>();
builder.Services.AddScoped<IOrganisateurService, OrganisateurService>();

builder.Services.AddScoped<IEvenementRepository, EvenementRepository>();
builder.Services.AddScoped<IEvenementService, EvenementService>();

builder.Services.AddScoped<IVisiteurRepository, VisiteurRepository>();
builder.Services.AddScoped<IVisiteurService, VisiteurService>();


builder.Services.AddScoped<IVerificationRepository, VerificationRepository>();
builder.Services.AddScoped<IVerificationService, VerificationService>();


builder.Services.AddScoped<IEmailServiceWithAI, EmailServiceWithAI>();

var app = builder.Build();


// Utilisation de la politique CORS
app.UseCors("AllowAngularApp");


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate(); // Applique les migrations automatiquement
}

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
