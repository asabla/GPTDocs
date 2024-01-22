using GPTDocs.API.Extensions;
using GPTDocs.API.Integrations.MSLearn;
using GPTDocs.API.Integrations.MozillaMdn;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddEndpointsApiExplorer();

// (Extension) Register and configure swagger generation
builder.RegisterSwagger();

// Register search services
builder.ConfigureMSLearnSearch();
builder.ConfigureMozillaMdnSearch();

var app = builder.Build();

app.UseSwagger();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

// Setup search endpoints
var searchGroup = app.MapGroup("/search");
searchGroup.MapMSLearnEndpoints();
searchGroup.MapMozillaMdnEndpoints();

app.Run();