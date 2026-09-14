using SagaReferenceArchitecture.BuildingBlocks.Messaging.DependencyInjection;
using SagaReferenceArchitecture.Ingestion.API.Features.Payment;
using SagaReferenceArchitecture.Ingestion.API.Features.Policy;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddMessaging(builder.Configuration);

builder.Services.AddScoped<ProcessPaymentHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapIssuePolicy();

app.Run();
