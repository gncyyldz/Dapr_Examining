using Dapr;
using Shared.Events;
using System.Text.Json.Serialization;

#region SDK
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddDapr();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapPost("/get-event", [Topic("rabbitmq-pubsub", "example.event")] (Event @event) =>
{
    Console.WriteLine(@event.Text);
});

app.UseHttpsRedirection();

app.UseCloudEvents();
app.MapControllers();
app.MapSubscribeHandler();
app.Run("https://localhost:2000");
#endregion
#region HTTP
//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();

//app.MapGet("/dapr/subscribe", () =>
//{
//    return Results.Json(new[] {new
//    {
//        PubSubName = "pubsub",
//        Topic = "example.event",
//        Route = "/get-event"
//    } });
//});

//app.MapPost("/get-event", (DaprData<Event> @event) =>
//{
//    Console.WriteLine(@event);
//});

//app.Run("https://localhost:2000");
//record DaprData<T>(T Data);
#endregion