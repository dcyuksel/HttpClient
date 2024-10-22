using HttpClient.DelegatingHandlers;
using HttpClient.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<LoggingDelegatingHandler>();
builder.Services.AddScoped<RetryDelegatingHandler>();

builder.Services
    .AddHttpClient<ICatService, CatService>(client =>
    {
        client.BaseAddress = new Uri(builder.Configuration["CatApiAddress"]!);
    })
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        return new HttpClientHandler()
        {
            UseDefaultCredentials = true,
        };
    })
    .AddHttpMessageHandler<LoggingDelegatingHandler>()
    .AddHttpMessageHandler<RetryDelegatingHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapGet("/facts",
    async (ICatService catHttpService) =>
    {
        return await catHttpService.GetFacts();
    })
    .WithName("GetFacts")
    .WithOpenApi();

app.Run();