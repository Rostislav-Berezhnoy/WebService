using System.Collections.Generic;
using System.Text.Json.Serialization;

//Точка измерения электроенергии
namespace WebService.Models
{
    public class ElectricityMeteringPoint : Point
    {
        
        public int? ConsumptionObjectID { get; set; }
        public string ConsumptionObjectName { get; set; }

        [JsonIgnore]
        public virtual ConsumptionObject ConsumptionObject { get; set; }

        [JsonIgnore]
        public int? VoltageTransformerID { get; set; }

        [JsonIgnore]
        public virtual VoltageTransformer VoltageTransformers { get; set; }

        public ICollection<VoltageTransformer> VoltageTransformer { get; set; } = new List<VoltageTransformer>();

        [JsonIgnore]
        public int? ElectricalTransformerID { get; set; }

        [JsonIgnore]
        public virtual ElectricalTransformer ElectricalTransformers { get; set; }

        public ICollection<ElectricalTransformer> ElectricalTransformer { get; set; } = new List<ElectricalTransformer>();

        [JsonIgnore]
        public int? ElectricityMeterID { get; set; }

        [JsonIgnore]
        public virtual ElectricityMeter ElectricityMeters { get; set; }

        public ICollection<ElectricityMeter> ElectricityMeter { get; set; } = new List<ElectricityMeter>();
    
        public ICollection<MeteringDevice> MeteringDevice { get; set; } = new List<MeteringDevice>();
    }
}