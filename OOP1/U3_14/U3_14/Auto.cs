using System;

namespace U3_14
{
    class Auto
    {
        public string LicencePlate { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public DateTime YearOfManufacture { get; set; }
        public DateTime TechnicalInspection { get; set; }
        public string Fuel { get; set; }
        public double Consumption { get; set; }
        /// <summary>
        /// Konstruktorius automobiliui aprašyti.
        /// </summary>
        /// <param name="licencePlate">Valstybiniai numeriai</param>
        /// <param name="manufacturer">Gamintojas</param>
        /// <param name="model">Modelis</param>
        /// <param name="yearOfManufacture">Pagaminimo metai</param>
        /// <param name="technicalInspection">Techninės apžiūros galiojima pabaiga</param>
        /// <param name="fuel">Kuras</param>
        /// <param name="consumption">Kuro sąnaudos per 100km</param>
        public Auto(string licencePlate, string manufacturer, string model, DateTime yearOfManufacture,
            DateTime technicalInspection, string fuel, double consumption)
        {
            this.LicencePlate = licencePlate;
            this.Manufacturer = manufacturer;
            this.Model = model;
            this.YearOfManufacture = yearOfManufacture;
            this.TechnicalInspection = technicalInspection;
            this.Fuel = fuel;
            this.Consumption = consumption;
        }
        /// <summary>
        /// Apskaičiuojamas mašinos amžius
        /// </summary>
        public int CarAge
        {
            get
            {
                DateTime today = DateTime.Today;
                int age = today.Year - this.YearOfManufacture.Year;
                if (this.YearOfManufacture.Date > today.AddYears(-age))
                {
                    age--;
                }
                return age;
            }
        }
        /// <summary>
        /// Palyginamas objektus su kitu pagal tris kriterijus (gamintojas, modelis, valstybiniai numeriai)
        /// </summary>
        /// <param name="other">Kitas objektas</param>
        
        public int CompareTo(Auto other)
        {
            if (this.Manufacturer != other.Manufacturer)
            {
                return this.Manufacturer.CompareTo(other.Manufacturer);
            }

            if (this.Model != other.Model)
            {
                return this.Model.CompareTo(other.Model);
            }

            if (this.LicencePlate != other.LicencePlate)
            {
                return this.LicencePlate.CompareTo(other.LicencePlate);
            }

            return 0;
        }
        /// <summary>
        /// Užklotas lygu lygu operatorius ir jam atvirkščias.
        /// </summary>
        /// <param name="a">Pirma mašina</param>
        /// <param name="b">Antra mašina</param>
        /// <returns>Ar mašinos lygios</returns>
        public static bool operator ==(Auto a, Auto b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Auto a, Auto b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Suformatuota eilutė mašinos spausdinimui.
        /// </summary>
        public override string ToString()
        {
            return string.Format("| {0,-22} | {1,-10} | {2,-10} | {3,30:yyyy-MM-dd} | {4,35:yyyy-MM-dd} | {5,-10} | {6,32:F2} |", this.LicencePlate, this.Manufacturer, this.Model, this.YearOfManufacture,
                this.TechnicalInspection, this.Fuel, this.Consumption);
        }

        /// <summary>
        /// Tikrina mašinas pagal unikalius valstybinius numerius.
        /// </summary>
        /// <param name="other">Kita mašina</param>
        public override bool Equals(object other)
        {
            return this.LicencePlate == ((Auto)other).LicencePlate;
        }
        public override int GetHashCode()
        {
            return this.LicencePlate.GetHashCode();
        }
    }
}
