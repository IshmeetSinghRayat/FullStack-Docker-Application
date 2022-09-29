using AccountsManagerAPIs.Data;
using AccountsManagerAPIs.DTOs;
using AccountsManagerAPIs.Models;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Connection With SQL Database
var server = builder.Configuration["DBServer"] ?? "localhost";
var port = builder.Configuration["DBPort"] ?? "1433";
var user = builder.Configuration["DBUser"] ?? "sa";
var password = builder.Configuration["DBPassword"] ?? "1StrongPwd!!";
var database = builder.Configuration["Database"] ?? "AccountsDb";
builder.Services.AddDbContext<AppDbContext>(Opt => Opt.UseSqlServer($"Server={server},{port};Initial Catalog={database}; User Id={user}; Password={password}; TrustServerCertificate=True"));

//builder.Services.AddDbContext<AppDbContext>(Opt => Opt.UseSqlServer($"Server={server},{port};Initial Catalog={database}; User Id={user}; Password={password}"));

builder.Services 
  .AddAutoMapper(Assembly.GetEntryAssembly());

builder.Services.AddScoped<IAccountRepository, AccountRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapGet("api/v1/Accounts", async (IAccountRepository repo, IMapper mapper) =>
{
    var account = await repo.GetAllAccounts();
    return Results.Ok(mapper.Map<IEnumerable<ReadAccountDetailsDto>>(account));
});

app.MapGet("api/v1/Accounts/{id}", async (IAccountRepository accountRepo, IMapper mapper, Guid id) =>
{
    var accountDetails = await accountRepo.GetAccountDetailsById(id);
    if (accountDetails != null)
    {
        return Results.Ok(mapper.Map<ReadAccountDetailsDto>(accountDetails));
    }
    return Results.NotFound();
});

app.MapPost("api/v1/Accounts/{id}", async (IAccountRepository accountRepo, 
                                           IMapper mapper, 
                                           ReadAccountDetailsDto accountDetails) =>
{
    var details = mapper.Map<Accounts>(accountDetails);
    if (details != null)
    {
        await accountRepo.CreateAccount(details);
        await accountRepo.SaveChanges();
        var SavedData = mapper.Map<ReadAccountDetailsDto>(details);
        return Results.Created($"api.v1/commands/{SavedData.Id}", SavedData);
    }
    return Results.NotFound();
});

app.Run();

