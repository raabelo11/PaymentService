using PaymentService.Worker;
using PaymentService.Worker.Services;
using RabbitMQ.Client;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

// Registra config do appsettings
builder.Services.Configure<Config>(builder.Configuration.GetSection("Config"));

// Registra o serviço que consome RabbitMQ
builder.Services.AddSingleton<RabbitMQService>();

// Registra o Worker que roda em background
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
