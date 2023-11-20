using HttpClientTestProject;

var builder = WebApplication.CreateBuilder(args);

const string ExternalApiUrl = "https://jsonplaceholder.typicode.com";

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<PlaceholderService>((client) => 
{
    client.BaseAddress = new Uri(ExternalApiUrl);
});

builder.Services.AddEndpointDefinitions(typeof(Program));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseEndpointDefinitions();

app.Run();