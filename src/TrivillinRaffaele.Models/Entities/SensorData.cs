using TrivillinRaffaele.Models;

namespace TrivillinRaffaele.Models.Entities;

public class SensorData : Entity
{
    public int SensorId { get; set; }
    public decimal Magnitude { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public decimal Depth { get; set; }
    public DateTime Timestamp { get; set; }
    public string? Notes { get; set; }
    public virtual Sensor Sensor { get; set; }
}
