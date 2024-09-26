using Account.API.Infrastructure;
using AccountInfrastructure.Command;
using AccountInfrastructure.Query;
using Application.Interfaces.IAccountModel;
using Application.Interfaces.IAccountType;
using Application.Interfaces.IPersonalAccount;
using Application.Interfaces.IStateAccount;
using Application.Interfaces.ITypeCurrency;
using Application.Mappers.IMappers;
using Application.Mappers;
using Application.UseCases;
using Infrastructure.Command;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<AccountContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IGenericMapper, GenericMapper>();

builder.Services.AddScoped<IAccountCommand, AccountCommand>();
builder.Services.AddScoped<IAccountQuery, AccountQuery>();
builder.Services.AddScoped<IAccountServices, AccountServices>();

builder.Services.AddScoped<IPersonalAccountCommand, PersonalAccountCommand>();
builder.Services.AddScoped<IPersonalAccountQuery, PersonalAccountQuery>();
builder.Services.AddScoped<IPersonalAccountServices, PersonalAccountServices>();

builder.Services.AddScoped<IAccountTypeQuery, AccountTypeQuery>();
builder.Services.AddScoped<IAccountTypeServices, AccountTypeServices>();

builder.Services.AddScoped<IStateAccountQuery, StateAccountQuery>();
builder.Services.AddScoped<IStateAccountServices, StateAccountServices>();

builder.Services.AddScoped<ITypeCurrencyQuery, TypeCurrencyQuery>();
builder.Services.AddScoped<ITypeCurrencyServices, TypeCurrencyServices>();


var app = builder.Build();

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
