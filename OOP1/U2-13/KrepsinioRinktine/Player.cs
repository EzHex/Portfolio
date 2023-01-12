using System;

namespace KrepsinioRinktine
{
    class Player
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public int Height { get; set; }
        public string Position { get; set; }
        public string Club { get; set; }
        public Invited Invite { get; set; }
        public Captain Captain { get; set; }

        /// <summary>
        /// Žaidėjo konstruktorius, priskiriamos savybės.
        /// </summary>
        /// <param name="name"> Vardas </param>
        /// <param name="surname"> Pavardė </param>
        /// <param name="birthDate"> Gimimo data </param>
        /// <param name="height"> Ūgis </param>
        /// <param name="position"> Pozicija </param>
        /// <param name="club"> Klubas </param>
        /// <param name="invite"> Ar pakviestas į rinktinę </param>
        /// <param name="captain"> Ar yra kapitonas </param>
        public Player(string name, string surname, DateTime birthDate,
            int height, string position, string club, Invited invite,
            Captain captain)
        {
            this.Name = name;
            this.Surname = surname;
            this.BirthDate = birthDate;
            this.Height = height;
            this.Position = position;
            this.Club = club;
            this.Invite = invite;
            this.Captain = captain;
        }
        /// <summary>
        /// Žaidėju tikrinimo metu yra tikrinami tik vardai, vardai unikalus (nėra vienodu).
        /// </summary>
        public override bool Equals(object obj)
        {
            return this.Name == ((Player)obj).Name;
        }

        /// <summary>
        /// Taip kaip ir equals, taip ir hashcode žiūrima pagal vardą.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        /// <summary>
        /// Užklojamas "daugiau" ir jam atvirkščias operatorius, kad būtų galima lyginti žaidėjo ūgį su kitų žaidėjų ūgiais.
        /// </summary>
        /// <param name="player1"> Žaidėjas </param>
        /// <param name="maxi"> Aukščiausio žaidėjo ūgis</param>
        public static bool operator > (Player player1, int maxi)
        {
            return player1.Height > maxi;
        }
        public static bool operator <(Player player1, int maxi)
        {
            return player1.Height < maxi;
        }


        /// <summary>
        /// Užklojamas "ar lygų" ir jam atvirkščias operatorius, kad būtų galima lyginti žaidėjo ūgį su aukščiausio žaidėjo ūgiu.
        /// </summary>
        /// <param name="player1"> Žaidėjas </param>
        /// <param name="maxi"> Aukščiausio žaidėjo ūgis </param>
        public static bool operator ==(Player player1, int maxi)
        {
            return player1.Height == maxi;
        }

        public static bool operator !=(Player player1, int maxi)
        {
            return player1.Height != maxi;
        }

        /// <summary>
        /// Apskaičiuojamas žaidėjo amžius.
        /// </summary>
        public int Age
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

    }
}
