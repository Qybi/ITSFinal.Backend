using TrivillinRaffaele.DataAccess.Abstractions.UnitOfWork;
using TrivillinRaffaele.Models.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace TrivillinRaffaele.API.Endpoints;

public static class SensorDataEndpoints
{
    public static IEndpointRouteBuilder MapSensorDataEndPoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/api/sensors-data").WithTags("SensorData");

        group.MapGet("/edit/{id:int}", GetSensorDataByIdAsync)
            .WithName("GetSensorDataByIdAsync")
            .WithDescription("Get a SensorData by its ID");

        group.MapGet("/", GetSensorDataBySensorIdAsync)
            .WithName("GetSensorDataById")
            .WithDescription("Get a SensorData by its ID");

        group.MapPost("/", CreateSensorDataAsync)
            .WithName("CreateSensorData")
            .WithDescription("Create a new SensorData");

        group.MapPut("/{id:int}", UpdateSensorDataAsync)
            .WithName("UpdateSensorData")
            .WithDescription("Update an existing SensorData");

        group.MapDelete("/{id:int}", DeleteSensorDataAsync)
            .WithName("DeleteSensorData")
            .WithDescription("Delete a SensorData by its ID");

        return group;
    }

    private static async Task<Results<Ok<SensorData?>, NotFound, BadRequest>> GetSensorDataByIdAsync(IUnitOfWork _uow, [FromRoute] int id)
    {
        try
        {
            var sd = await _uow.SensorsData.GetByIdAsync(id);
            if (sd == null)
                return TypedResults.NotFound();
            return TypedResults.Ok(sd);
        }
        catch (Exception)
        {
            return TypedResults.BadRequest();
        }
    }

    private static async Task<Results<Ok<IEnumerable<SensorData>>, NotFound, BadRequest>> GetSensorDataBySensorIdAsync(IUnitOfWork _uow, [FromQuery] int sensorId)
    {
        try
        {
            var data = await _uow.SensorsData.GetAllBySensorId(sensorId);
            if (data == null)
                return TypedResults.NotFound();
            return TypedResults.Ok(data);
        }
        catch (Exception)
        {
            return TypedResults.BadRequest();
        }
    }

    private static async Task<Results<Ok<SensorData>, BadRequest>> CreateSensorDataAsync(IUnitOfWork _uow, SensorData sensorData)
    {
        try
        {
            if (sensorData == null)
                throw new ArgumentNullException(nameof(sensorData));
            var createdSensorData = _uow.SensorsData.Add(sensorData);
            await _uow.SaveChangesAsync();
            return TypedResults.Ok(createdSensorData);
        }
        catch (Exception)
        {
            return TypedResults.BadRequest();
        }
    }

    private static async Task<Results<Ok<SensorData>, NotFound, BadRequest>> UpdateSensorDataAsync(IUnitOfWork _uow, int id, SensorData sensorData)
    {
        try
        {
            if (sensorData == null)
                throw new ArgumentNullException(nameof(sensorData));

            var existingSensorData = await _uow.SensorsData.GetByIdAsync(id);
            if (existingSensorData == null)
                return TypedResults.NotFound();

            _uow.SensorsData.Update(sensorData);
            await _uow.SaveChangesAsync();

            return TypedResults.Ok(sensorData);
        }
        catch (Exception)
        {
            return TypedResults.BadRequest();
        }
    }

    private static async Task<Results<Ok<bool>, BadRequest, NotFound>> DeleteSensorDataAsync(IUnitOfWork _uow, int id)
    {
        try
        {
            var existingSensorData = await _uow.SensorsData.GetByIdAsync(id);
            if (existingSensorData == null)
                return TypedResults.NotFound();

            _uow.SensorsData.Delete(existingSensorData);

            await _uow.SaveChangesAsync();

            return TypedResults.Ok(true);
        }
        catch (Exception)
        {
            return TypedResults.BadRequest();
        }
    }
}
