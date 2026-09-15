using SagaReferenceArchitecture.BuildingBlocks.Messaging.DependencyInjection;
using SagaReferenceArchitecture.Ingestion.API.Features.Payment.Process;
using SagaReferenceArchitecture.Ingestion.API.Features.Policy.Issue;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMessaging(
    builder.Configuration,
    cfg =>
    {
      cfg.AddConsumer<ProcessPaymentConsumer>();
    });

builder.Services.AddScoped<ProcessPaymentHandler>();
builder.Services.AddScoped<IssuePolicyHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapIssuePolicy();

app.Run();
