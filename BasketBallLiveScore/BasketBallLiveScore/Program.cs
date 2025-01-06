using BasketBallLiveScore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuration de la chaîne de connexion à la base de données
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Ajouter les services au conteneur, y compris CORS et Entity Framework
builder.Services.AddDbContext<BasketballContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration JWT pour l'authentification
var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(jwtKey) || jwtKey.Length < 32)
{
    throw new Exception("La clé JWT doit comporter au moins 32 caractères pour garantir la sécurité.");
}

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

// Configuration CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("http://localhost:4200") // Origine Angular
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// Utilisation de CORS avant les autres middlewares
app.UseCors("AllowSpecificOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Ajoute cette ligne pour servir des fichiers statiques

app.UseRouting();

// Active l'authentification
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// Redirige toutes les requêtes non gérées vers Angular
app.MapFallbackToFile("index.html");

app.Run();
