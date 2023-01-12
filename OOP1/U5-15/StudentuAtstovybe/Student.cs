using System;

namespace StudentuAtstovybe
{
    class Student : Member
    {
        public string ID { get; set; }
        public int Course { get; set; }
        /// <summary>
        /// Studentų savybės su paveldėta nario klase
        /// </summary>
        /// <param name="surname">Pavardė</param>
        /// <param name="name">Vardas</param>
        /// <param name="birthDate">Gimimo data</param>
        /// <param name="phoneNumber">Telefono numeris</param>
        /// <param name="iD">Studento id</param>
        /// <param name="course">Kurso numeris</param>
        public Student(string surname, string name, DateTime birthDate, string phoneNumber,
            string iD, int course) : base(surname, name, birthDate, phoneNumber)
        {
            this.ID = iD;
            this.Course = course;
        }
        /// <summary>
        /// Amžius neskaičiuojamas
        /// </summary>
        public override int Age
        {
            get
            {
                return 0;
            }
        }
        /// <summary>
        /// Spausdinti studento informacija, suformatuota eilutė.
        /// </summary>
        public override string ToString()
        {
            return string.Format("| {0, -15} | {1, -15} | {2, 20:yyyy-MM-dd} | {3, -16} | {4, -15} | Kursas {5, 5} ", Surname, Name, BirthDate, PhoneNumber, ID, Course);
        }
    }
}
