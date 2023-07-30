using ApiProducer.Services;
using ApiProducer.Services.Interfaces;
using Confluent.Kafka;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var producerConfig = new ProducerConfig();

builder.Configuration.Bind("ProducerConfig", producerConfig);
builder.Services.AddSingleton<ProducerConfig>(producerConfig);

builder.Services.AddScoped<IProducerService, ProducerService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
