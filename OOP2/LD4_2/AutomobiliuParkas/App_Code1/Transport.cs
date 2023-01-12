using System;

namespace AutomobiliuParkas
{
    public abstract class Transport : IComparable<Transport>, IEquatable<Transport>
    {
        public string LicensePlate { get; private set; }
        public string Manufactor { get; private set; }
        public string Model { get; private set; }
        public DateTime YearOfManufaction { get; private set; }
        public DateTime TechnicalInspection { get; private set; }
        public string Fuel { get; private set; }
        public double AverageConsumption { get; private set; }

        protected Transport(string licensePlate, string manufactor, string model, DateTime yearOfManufaction,
            DateTime technicalInspection, string fuel, double averageConsumption)
        {
            this.LicensePlate = licensePlate;
            this.Manufactor = manufactor;
            this.Model = model;
            this.YearOfManufaction = yearOfManufaction;
            this.TechnicalInspection = technicalInspection;
            this.Fuel = fuel;
            this.AverageConsumption = averageConsumption;
        }
        /// <summary>
        /// Calculate the age of the transport
        /// </summary>
        public int Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int age = today.Year - this.YearOfManufaction.Year;
                if (this.YearOfManufaction.Date > today.AddYears(-age))
                {
                    age--;
                }
                return age;
            }
        }
        /// <summary>
        /// Comparing by the rules first by Manufactor then by Model
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public virtual int CompareTo(Transport other)
        {
            if (this.Manufactor == other.Manufactor)
            {
                return this.Model.CompareTo(other.Model);
            }
            return this.Manufactor.CompareTo(other.Manufactor);
        }
        /// <summary>
        /// Checks if transports LicensePlate is identical, because license plates are unique
        /// </summary>
        public virtual bool Equals(Transport other)
        {
            return this.LicensePlate == other.LicensePlate;
        }
        /// <summary>
        /// Formating a line for writing to txt file
        /// </summary>
        public override string ToString()
        {
            return string.Format("| {0,-20} | {1,-10} | {2,-10} | {3,30} | {4,35} | {5,-10} | {6,25} |",
                this.LicensePlate, this.Manufactor, this.Model, this.YearOfManufaction.ToString("yyyy-MM"),
                this.TechnicalInspection.ToString("yyyy-MM-dd"), this.Fuel, this.AverageConsumption);
        }
        /// <summary>
        /// Formating a line for writing to txt file for best transport rules
        /// </summary>
        public string ToStringForBestTransports()
        {
            return string.Format("| {0,-10} | {1,-10} | {2,-20} | {3, 8} |", this.Manufactor, this.Model,
                this.LicensePlate, this.Age);
        }
        /// <summary>
        /// Formating a line for writing to txt file for technical inspection rules
        /// </summary>
        public string ToStringForTechnicalInspection()
        {
            return string.Format("| {0,-10} | {1,-10} | {2,-20} |", this.Manufactor, this.Model,
                this.LicensePlate);
        }
        /// <summary>
        /// Each group have different feature (odometer, volume, seats)
        /// </summary>
        public abstract string Feature();
        /// <summary>
        /// Each group have different technical inspection are valid
        /// </summary>
        /// <returns></returns>
        public abstract string EndOfTechnicalInspection();
        /// <summary>
        /// Finds days count for technical inspection validation
        /// </summary>
        public abstract TimeSpan TimeForTechnicalInspection { get; }
    }
}