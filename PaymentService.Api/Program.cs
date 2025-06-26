using Microsoft.EntityFrameworkCore;
using PaymentService.Api.Configuration;
using PaymentService.Application.Interfaces;
using PaymentService.Application.Service;
using PaymentService.Application.UseCases;
using PaymentService.Domain.Interfaces;
using PaymentService.Infrastructure.Data.Context;
using PaymentService.Infrastructure.Data.Repository;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Ajuste no SQL Server - adiciona DbContext e registra repositório
builder.Services.AddDbContext<PaymentsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));

// Registro DI - Scoped = uma instância criada por requisição HTTP
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBoletoUseCase, BoletoUseCase>();
builder.Services.AddScoped<IUsuarioUseCase, UsuarioUseCase>();

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IBoletoRepository, BoletoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// Registra config do appsettings
builder.Services.Configure<Config>(builder.Configuration.GetSection("Config"));

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