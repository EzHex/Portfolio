using System;

namespace AutomobiliuParkas
{
    public class Truck : Transport
    {
        public double Volume { get; private set; }

        public Truck(string licensePlate, string manufactor, string model, DateTime yearOfManufaction,
            DateTime technicalInspection, string fuel, double averageConsumption, double volume) :
            base(licensePlate, manufactor, model, yearOfManufaction, technicalInspection, fuel, averageConsumption)
        {
            this.Volume = volume;
        }

        public Truck(string licensePlate, string manufactor, string model, DateTime yearOfManufaction,
            DateTime technicalInspection, string fuel, double averageConsumption) :
            base(licensePlate, manufactor, model, yearOfManufaction, technicalInspection, fuel, averageConsumption)
        {
            this.Volume = 0;
        }

        public override string Feature()
        {
            return string.Format(" {0,17} |", this.Volume + " litrų");
        }

        public override string EndOfTechnicalInspection()
        {
            DateTime time = this.TechnicalInspection;
            time.AddYears(1);
            if (TimeForTechnicalInspection < TimeSpan.Parse("0")) return string.Format(" {0, 25} |", time.ToString("yyyy-MM-dd") + "*");
            return string.Format(" {0, 25} |", time.ToString("yyyy-MM-dd"));
        }

        public override TimeSpan TimeForTechnicalInspection
        {
            get
            {
                DateTime time = this.TechnicalInspection;
                time = time.AddYears(1);
                return time - DateTime.Today;
            }
        }
    }
}