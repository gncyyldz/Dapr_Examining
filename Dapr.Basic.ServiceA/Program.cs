using Dapr.Client;
using Shared.Events;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
#region SDK
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDaprClient();

var app = builder.Build();

app.MapGet("/send-event/{eventText}", async (string eventText, DaprClient client) =>
{
    await client.PublishEventAsync(pubsubName: "rabbitmq-pubsub", topicName: "example.event", new Event(eventText));
    Console.WriteLine(eventText);
});

app.UseHttpsRedirection();
app.MapControllers();
app.Run("https://localhost:1000");
#endregion
#region HTTP

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddDaprClient();

//var app = builder.Build();

//app.MapGet("/send-event/{eventText}", async (string eventText, DaprClient client) =>
//{
//    HttpClient httpClient = new();

//    Event @event = new(eventText);
//    string jsonData = JsonSerializer.Serialize(@event, options: new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

//    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
//    StringContent body = new(jsonData, Encoding.UTF8, MediaTypeNames.Application.Json);
//    var response = await httpClient.PostAsync($"http://localhost:2001/v1.0/publish/pubsub/example.event", body);

//    return Results.Ok($"Status Code : {response.StatusCode} | Event : {@event.Text}\"");
//});

//app.UseHttpsRedirection();
//app.MapControllers();
//app.Run("https://localhost:1000");
#endregion
