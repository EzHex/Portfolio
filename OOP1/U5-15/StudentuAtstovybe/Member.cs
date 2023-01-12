using System;

namespace StudentuAtstovybe
{
    abstract class Member
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Atstovybės nario abstraktūs duomenys
        /// </summary>
        /// <param name="surname">Pavardė</param>
        /// <param name="name">Vardas</param>
        /// <param name="birthDate">Gimimo diena</param>
        /// <param name="phoneNumber">Telefono numeris</param>
        public Member(string surname, string name, DateTime birthDate, string phoneNumber)
        {
            this.Surname = surname;
            this.Name = name;
            this.BirthDate = birthDate;
            this.PhoneNumber = phoneNumber;
        }
        /// <summary>
        /// Amžius
        /// </summary>
        public abstract int Age { get; }
        /// <summary>
        /// Užklotas Equals metodas, kad būtų lyginami pagal unikalius vardus.
        /// </summary>
        public override bool Equals(object obj)
        {
            return this.Name == ((Member)obj).Name;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
        /// <summary>
        /// Rušiavimui pagal pavardę ir vardą skirtas metodas
        /// </summary>
        /// <param name="other">Kitas narys su kuriuo yra lyginama</param>
        public int CompareTo(Member other)
        {
            if (this.Surname != other.Surname)
            {
                return this.Surname.CompareTo(other.Surname);
            }

            if (this.Name != other.Name)
            {
                return this.Name.CompareTo(other.Name);
            }

            return 0;
        }

    }
}
