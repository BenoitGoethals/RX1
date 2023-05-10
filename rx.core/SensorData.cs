using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rx.core
{
    public class SensorData
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int Temp { get; set; }
        public int WindSpeed { get; set; }
        public int Humidy { get; set; }
        public string Sensor { get; set; }

        protected bool Equals(SensorData other)
        {
            return Guid.Equals(other.Guid);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SensorData)obj);
        }

        public override int GetHashCode()
        {
            return Guid.GetHashCode();
        }

        public override string ToString()
        {
            return $"{nameof(Guid)}: {Guid}, {nameof(Temp)}: {Temp}, {nameof(WindSpeed)}: {WindSpeed}, {nameof(Humidy)}: {Humidy}, {nameof(Sensor)}: {Sensor}";
        }
    }
}
