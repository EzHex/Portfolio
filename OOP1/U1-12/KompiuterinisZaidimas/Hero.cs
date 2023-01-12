using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KompiuterinisZaidimas
{
    class Hero
    {
        public string Name { get; set; }
        public string Race { get; set; }
        public string Cls { get; set; } //Class ( Klasė )
        public double HealthPoints { get; set; }
        public double Mana { get; set; }
        public double DamagePoints { get; set; }
        public double DefensePoints { get; set; }
        public double Power { get; set; }
        public double Agility { get; set; }
        public double Intelligence { get; set; }
        public string SpecialPower { get; set; }

        public double Characteristics { get; set; } //geriausių savybių suma

        /// <summary>
        /// Sukuriamas herojus ir užpildoma informacija apie jį
        /// </summary>
        /// <param name="name"> vardas </param>
        /// <param name="race"> rasė </param>
        /// <param name="cls"> klasė </param>
        /// <param name="healthPoints"> gyvybių taškai </param>
        /// <param name="mana"> manos taškai </param>
        /// <param name="damagePoints"> žalos taškai </param>
        /// <param name="defensePoints"> gynybiniai taškai </param>
        /// <param name="power"> jėgos taškai </param>
        /// <param name="agility"> vikrumo taškai </param>
        /// <param name="intelligence"> intelekto taškai </param>
        /// <param name="specialPower"> speciali galia </param>
        public Hero (string name, string race, string cls,  double healthPoints, double mana,
            double damagePoints, double defensePoints, double power, double agility, double intelligence, string specialPower)
        {
            this.Name = name;
            this.Race = race;
            this.Cls = cls;
            this.HealthPoints = healthPoints;
            this.Mana = mana;
            this.DamagePoints = damagePoints;
            this.DefensePoints = defensePoints;
            this.Power = power;
            this.Agility = agility;
            this.Intelligence = intelligence;
            this.SpecialPower = specialPower;

            this.Characteristics = power + agility + intelligence;
        }
    }
}
