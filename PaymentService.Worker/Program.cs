using PaymentService.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

// Registra config do appsettings
builder.Services.Configure<Config>(builder.Configuration.GetSection("Config"));

builder.Services.AddSingleton<RabbitMQService>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
