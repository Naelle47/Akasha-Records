using AkashaRecords.Data.Repositories.BuildRepo;
using AkashaRecords.Data.Repositories.CharacterRepo;
using AkashaRecords.Data.Repositories.ReferenceRepo;
using Npgsql;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// -------------------------
// Dapper — mapping automatique snake_case (BDD) → PascalCase (C#)
// -------------------------
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

// -------------------------
// Connexion PostgreSQL via Npgsql
// -------------------------
builder.Services.AddScoped<IDbConnection>(_ =>
    new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

// -------------------------
// Repositories
// -------------------------
builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
builder.Services.AddScoped<IReferenceRepository, ReferenceRepository>();
builder.Services.AddScoped<IBuildRepository, BuildRepository>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();