using BackendScout.Data;
using BackendScout.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuestPDF.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using System.Security.Claims;
using Microsoft.Extensions.FileProviders;

// ✅ Activar licencia QuestPDF
QuestPDF.Settings.License = LicenseType.Community;

var builder = WebApplication.CreateBuilder(args);

// ✅ Clave JWT desde appsettings.json o valor por defecto
var jwtKey = builder.Configuration["Jwt:Key"] ?? "clave-secreta-super-segura-scout";

// ✅ CORS para React
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ✅ Servicios base
builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "BackendScout",
        Version = "v1"
    });

    // 🔐 Configuración del esquema de seguridad para JWT
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Ingrese su token JWT en este formato: Bearer {token}"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
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

// ✅ Servicios personalizados
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UnidadService>();
builder.Services.AddScoped<FichaMedicaService>();
builder.Services.AddScoped<ObjetivoService>();
builder.Services.AddScoped<CargaObjetivosService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<PdfObjetivosService>();
builder.Services.AddScoped<PdfService>();
builder.Services.AddScoped<MensajeService>();
builder.Services.AddScoped<EspecialidadService>();
builder.Services.AddTransient<EspecialidadImporter>();
builder.Services.AddScoped<EventoService>();
builder.Services.AddScoped<DocumentoEventoService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<PasswordResetService>();
builder.Services.AddScoped<RegistroGestionService>();
builder.Services.AddScoped<GestionService>();

// ✅ Obtener cadena de conexión (desde entorno o appsettings)
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
string connectionString;

if (!string.IsNullOrEmpty(databaseUrl))
{
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');
    connectionString =
        $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true;";
}
else
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// ✅ Configuración JWT
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            RoleClaimType = ClaimTypes.Role
        };
    });

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();

// ✅ Migración segura con try-catch
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        Console.WriteLine("Migrando base de datos...");
        db.Database.Migrate();
        Console.WriteLine("✅ Migración completada correctamente.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ Error durante la migración de la base de datos:");
        Console.WriteLine(ex.Message);
        Console.WriteLine("⚠️ La aplicación continuará ejecutándose.");
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseDefaultFiles();
app.UseStaticFiles();

// Crear carpeta si no existe
Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "ArchivosMensajes"));

// ✅ Exponer carpeta ArchivosMensajes
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "ArchivosMensajes")),
    RequestPath = "/archivos"
});

// ✅ CORS
app.UseCors("AllowReactApp");

// ✅ Seguridad
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
