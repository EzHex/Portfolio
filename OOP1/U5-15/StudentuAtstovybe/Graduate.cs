using System;

namespace StudentuAtstovybe
{
    class Graduate : Member
    {
        public string WorkPlace { get; set; }
        /// <summary>
        /// Baigusio universitetą nario savybės su paveldėtom pagrindinėmis savybėmis
        /// </summary>
        /// <param name="surname">Pavardė</param>
        /// <param name="name">Vardas</param>
        /// <param name="birthDate">Gimimo diena</param>
        /// <param name="phoneNumber">Telefono numeris</param>
        /// <param name="workPlace">Darbovietė</param>
        public Graduate(string surname, string name, DateTime birthDate, string phoneNumber,
            string workPlace) : base(surname, name, birthDate, phoneNumber)
        {
            this.WorkPlace = workPlace;
        }
        /// <summary>
        /// Amžius
        /// </summary>
        public override int Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int age = today.Year - this.BirthDate.Year;
                if (this.BirthDate.Date > today.AddYears(-age))
                {
                    age--;
                }
                return age;
            }
        }
        /// <summary>
        /// Spausdinti nario informacija, suformatuota eilutė.
        /// </summary>
        public override string ToString()
        {
            return string.Format("| {0,-15} | {1,-15} | {2, 20:yyyy-MM-dd} | {3,-16} | {4, -15} | Amžius {5, 5} ", Surname, Name, BirthDate, PhoneNumber, WorkPlace, Age);
        }
    }
}
