using System;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using TrivillinRaffaele.DataAccess.Abstractions.UnitOfWork;
using TrivillinRaffaele.Models.Entities;

namespace TrivillinRaffaele.Dequeuer;

public class Dequeuer
{
    private readonly ILogger<Dequeuer> _logger;
    private readonly IUnitOfWork _uow;

    public Dequeuer(ILogger<Dequeuer> logger, IUnitOfWork uow)
    {
        _logger = logger;
        _uow = uow;
    }

    [Function(nameof(Dequeuer))]
    public async Task Run(
        [ServiceBusTrigger("cled", Connection = "ConnectionStrings:ServiceBus")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        _logger.LogInformation("Message ID: {id}", message.MessageId);
        _logger.LogInformation("Message Body: {body}", message.Body);
        _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

        var sensorData = new SensorData();  

        try
        {
            sensorData = JsonSerializer.Deserialize<SensorData>(message.Body.ToString());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while deserializing the message body");
            throw;
        }

        if (sensorData == null)
        {
            _logger.LogError("Deserialized sensor data is null");
            throw new InvalidOperationException("Deserialized sensor data is null");
        }

        try
        {
            _uow.SensorsData.Add(sensorData);
            await _uow.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while saving sensor data to the database");
            throw;
        }


        await messageActions.CompleteMessageAsync(message);
    }
}