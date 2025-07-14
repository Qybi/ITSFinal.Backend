using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TrivillinRaffaele.DataAccess.Abstractions.UnitOfWork;
using TrivillinRaffaele.Models.Entities;

namespace TrivillinRaffaele.API.Endpoints;

public static class SensorEndpoints
{
    public static IEndpointRouteBuilder MapSensorsEndPoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("/api/sensors").WithTags("Sensors");

        group.MapGet("/", GetSensorsAsync)
            .WithName("GetSensors")
            .WithDescription("Get all sensor data");

        group.MapGet("/{id:int}", GetSensorByIdAsync)
            .WithName("GetSensorById")
            .WithDescription("Get a sensor by its ID");

        group.MapPost("/", CreateSensorAsync)
            .WithName("CreateSensor")
            .WithDescription("Create a new sensor");

        group.MapPut("/{id:int}", UpdateSensorAsync)
            .WithName("UpdateSensor")
            .WithDescription("Update an existing sensor");

        group.MapDelete("/{id:int}", DeleteSensorAsync)
            .WithName("DeleteSensor")
            .WithDescription("Delete a sensor by its ID");

        return group;
    }

    private static async Task<Results<Ok<IEnumerable<Sensor>>, BadRequest>> GetSensorsAsync(IUnitOfWork _uow)
    {
        try
        {
            IEnumerable<Sensor> cats = await _uow.Sensors.GetAllAsync();
            return TypedResults.Ok(cats);
        }
        catch (Exception)
        {
            return TypedResults.BadRequest();
        }
    }

    private static async Task<Results<Ok<Sensor?>, NotFound, BadRequest>> GetSensorByIdAsync(IUnitOfWork _uow, [FromRoute] int id)
    {
        try
        {
            var c = await _uow.Sensors.GetByIdAsync(id);
            if (c == null)
                return TypedResults.NotFound();
            return TypedResults.Ok(c);
        }
        catch (Exception)
        {
            return TypedResults.BadRequest();
        }
    }

    private static async Task<Results<Ok<Sensor>, BadRequest>> CreateSensorAsync(IUnitOfWork _uow, Sensor sensor)
    {
        try
        {
            if (sensor == null)
                throw new ArgumentNullException(nameof(sensor));
            var createdSensor = _uow.Sensors.Add(sensor);
            await _uow.SaveChangesAsync();
            return TypedResults.Ok(createdSensor);
        }
        catch (Exception)
        {
            return TypedResults.BadRequest();
        }
    }

    private static async Task<Results<Ok<Sensor>, NotFound, BadRequest>> UpdateSensorAsync(IUnitOfWork _uow, int id, Sensor Sensor)
    {
        try
        {
            if (Sensor == null)
                throw new ArgumentNullException(nameof(Sensor));

            var existingSensor = await _uow.Sensors.GetByIdAsync(id);
            if (existingSensor == null)
                return TypedResults.NotFound();

            _uow.Sensors.Update(Sensor);
            await _uow.SaveChangesAsync();

            return TypedResults.Ok(Sensor);
        }
        catch (Exception)
        {
            return TypedResults.BadRequest();
        }
    }

    private static async Task<Results<Ok<bool>, BadRequest, NotFound>> DeleteSensorAsync(IUnitOfWork _uow, int id)
    {
        try
        {
            var existingSensor = await _uow.Sensors.GetByIdAsync(id);
            if (existingSensor == null)
                return TypedResults.NotFound();

            _uow.Sensors.Delete(existingSensor);

            await _uow.SaveChangesAsync();

            return TypedResults.Ok(true);
        }
        catch (Exception)
        {
            return TypedResults.BadRequest();
        }
    }
}
