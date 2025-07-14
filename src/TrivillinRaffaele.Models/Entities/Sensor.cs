using TrivillinRaffaele.Models;

namespace TrivillinRaffaele.Models.Entities;

public class Sensor : Entity
{
    public string Code { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public virtual ICollection<SensorData> SensorData { get; set; }
}
