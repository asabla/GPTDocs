using GPTDocs.API.Integrations.MSLearn;

using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Register search services
builder.ConfigureMSLearnSearch();

// Setup OpenAPI
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(e => e.EnableAnnotations());
}
else
{
    // TODO: read from configuration/environment variables
    builder.Services.AddSwaggerGen(option =>
    {
        option.AddServer(new OpenApiServer
        {
            Url = "https://localhost:5001",
            Description = "Local Development"
        });
    });
}

var app = builder.Build();
app.UseSwagger();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

// Setup search endpoints
var searchGroup = app.MapGroup("/search");
searchGroup.MapMSLearnEndpoints();

app.Run();