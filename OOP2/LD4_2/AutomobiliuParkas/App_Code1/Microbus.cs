using System;

namespace AutomobiliuParkas
{
    public class Microbus : Transport
    {
        public int Seats { get; private set; }

        public Microbus(string licensePlate, string manufactor, string model, DateTime yearOfManufaction,
            DateTime technicalInspection, string fuel, double averageConsumption, int seats) :
            base(licensePlate, manufactor, model, yearOfManufaction, technicalInspection, fuel, averageConsumption)
        {
            this.Seats = seats;
        }

        public Microbus(string licensePlate, string manufactor, string model, DateTime yearOfManufaction,
           DateTime technicalInspection, string fuel, double averageConsumption) :
           base(licensePlate, manufactor, model, yearOfManufaction, technicalInspection, fuel, averageConsumption)
        {
            this.Seats = 0;
        }

        public override string Feature()
        {
            return string.Format(" {0,17} |", this.Seats + " vietos");
        }

        public override string EndOfTechnicalInspection()
        {
            DateTime time = this.TechnicalInspection;
            time.AddMonths(6);
            if (TimeForTechnicalInspection < TimeSpan.Parse("0")) return string.Format(" {0, 25} |", time.ToString("yyyy-MM-dd") + "*");
            return string.Format(" {0, 25} |", time.ToString("yyyy-MM-dd"));
        }

        public override TimeSpan TimeForTechnicalInspection
        {
            get
            {
                DateTime time = this.TechnicalInspection;
                time = time.AddMonths(6);
                return time - DateTime.Today;
            }
        }
    }
}