using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using TrivillinRaffaele.Models.Entities;

namespace TrivillinRaffaele.Simulator;

public class GetSensorData
{
    private readonly ILogger<GetSensorData> _logger;
    private readonly string _serviceBusConnectionString;

    public GetSensorData(ILogger<GetSensorData> logger, IConfiguration configuration)
    {
        _logger = logger;
        _serviceBusConnectionString = configuration.GetConnectionString("ServiceBus") ?? string.Empty;
    }

    [Function("getsensordata")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
    {
        SensorData data = new SensorData();
        try
        {
            data = await JsonSerializer.DeserializeAsync<SensorData>(req.Body, new JsonSerializerOptions()
            {
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while deserializing the request body");
            return new BadRequestResult();
        }

        if (data == null)
        {
            _logger.LogError("Deserialized data is null");
            return new BadRequestResult();
        }

        var client = new ServiceBusClient(_serviceBusConnectionString);
        var sender = client.CreateSender("cled");

        try
        {
            var message = new ServiceBusMessage(JsonSerializer.Serialize(data));
            await sender.SendMessageAsync(message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while sending message to Service Bus");
            throw;
        }

        _logger.LogInformation("Sensor data has been enqueued successfully");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}