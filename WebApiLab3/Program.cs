using Microsoft.EntityFrameworkCore;
using WebApiLab3;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true
        };
    });
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ModelDB>(options => options.UseSqlServer(connection));
var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapPost("/login", async (User loginData, ModelDB db) =>
{
    User? person = await db.Users.FirstOrDefaultAsync(p => p.Email == loginData.Email && p.Password == loginData.Password);
    if (person is null) return Results.Unauthorized();

    var claims = new List<Claim> { new Claim(ClaimTypes.Name, person.Email!) };
    var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
    var response = new
    {
        access_token = encodedJwt,
        username = person.Email
    };

    return Results.Json(response);
});
app.MapGet("api/tariff", [Authorize] async (ModelDB db) => await db.Tariff.ToListAsync());
app.MapGet("api/tariff/{id:int}", [Authorize] async (int id, ModelDB db) =>
{
    Tariff? tariff = await db.Tariff.FirstOrDefaultAsync(x => x.Id == id);
    if (tariff == null) return Results.NotFound(new { message = "Tariff not found" });
    return Results.Json(tariff);
});
app.MapGet("api/parcel", [Authorize] async (ModelDB db) => await db.Parcel.ToListAsync());
app.MapGet("api/parcel/{id:int}", [Authorize] async (int id, ModelDB db) =>
{
    Parcel? parcel = await db.Parcel.FirstOrDefaultAsync(x => x.Id == id);
    if (parcel == null) return Results.NotFound(new { message = "Parcel not found" });
    return Results.Json(parcel);
});
app.MapPost("/api/tariff", [Authorize] async (Tariff tariff, ModelDB db) =>
{
    await db.Tariff.AddAsync(tariff);
    await db.SaveChangesAsync();
    return tariff;
});
app.MapPost("/api/parcel", [Authorize] async (Parcel parcel, ModelDB db) =>
{
    await db.Parcel.AddAsync(parcel);
    await db.SaveChangesAsync();
    return parcel;
});
app.MapDelete("api/tariff/{id:int}", [Authorize] async (int id, ModelDB db) =>
{
    Tariff? tariff = await db.Tariff.FirstOrDefaultAsync(x => x.Id == id);
    if (tariff == null) return Results.NotFound(new { message = "Tariff not found" });
    db.Tariff.Remove(tariff);
    await db.SaveChangesAsync();
    return Results.Json(tariff);
});
app.MapDelete("api/parcel/{id:int}", [Authorize] async (int id, ModelDB db) =>
{
    Parcel? parcel = await db.Parcel.FirstOrDefaultAsync(x => x.Id == id);
    if (parcel == null) return Results.NotFound(new { message = "Parcel not found" });
    db.Parcel.Remove(parcel);
    await db.SaveChangesAsync();
    return Results.Json(parcel);
});
app.MapPut("/api/tariff", [Authorize] async (Tariff tariffData, ModelDB db) =>
{
    Tariff? tariff = await db.Tariff.FirstOrDefaultAsync(u => u.Id == tariffData.Id);
    if (tariff == null) return Results.NotFound(new { message = "Tariff not found" });
    tariff.DepartureCode = tariffData.DepartureCode;
    tariff.DepartureName = tariffData.DepartureName;
    tariff.PricePerWeightUnit = tariffData.PricePerWeightUnit;
    await db.SaveChangesAsync();
    return Results.Json(tariff);
});
app.MapPut("/api/parcel", [Authorize] async (Parcel parcelData, ModelDB db) =>
{
    Parcel? parcel = await db.Parcel.FirstOrDefaultAsync(u => u.Id == parcelData.Id);
    if (parcel == null) return Results.NotFound(new { message = "Parcel not found" });
    parcel.SenderFullName = parcelData.SenderFullName;
    parcel.DepartureCode = parcelData.DepartureCode;
    parcel.Weight = parcelData.Weight;
    parcel.Destination = parcelData.Destination;
    parcel.Cost = parcelData.Cost;
    await db.SaveChangesAsync();
    return Results.Json(parcel);
});
app.Run();

