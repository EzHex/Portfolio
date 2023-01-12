using System;

namespace AutomobiliuParkas
{
    public class Car : Transport
    {
        public double Odometer { get; private set; }

        public Car(string licensePlate, string manufactor, string model, DateTime yearOfManufaction,
            DateTime technicalInspection, string fuel, double averageConsumption, double odometer) :
            base(licensePlate, manufactor, model, yearOfManufaction, technicalInspection, fuel, averageConsumption)
        {
            this.Odometer = odometer;
        }

        public Car(string licensePlate, string manufactor, string model, DateTime yearOfManufaction,
            DateTime technicalInspection, string fuel, double averageConsumption) :
            base(licensePlate, manufactor, model, yearOfManufaction, technicalInspection, fuel, averageConsumption)
        {
            this.Odometer = double.MaxValue;
        }

        public override string Feature()
        {
            return string.Format(" {0,17} |", this.Odometer + " km");
        }

        public override string EndOfTechnicalInspection()
        {
            DateTime time = this.TechnicalInspection;
            time = time.AddYears(2);
            if (TimeForTechnicalInspection < TimeSpan.Parse("0")) return string.Format(" {0, 25} |", time.ToString("yyyy-MM-dd") + "*");
            return string.Format(" {0, 25} |", time.ToString("yyyy-MM-dd"));
        }

        public override TimeSpan TimeForTechnicalInspection
        {
            get
            {
                DateTime time = this.TechnicalInspection;
                time = time.AddYears(2);
                return time - DateTime.Today;
            }
        }
    }
}