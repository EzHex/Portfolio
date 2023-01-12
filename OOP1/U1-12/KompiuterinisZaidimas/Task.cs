using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KompiuterinisZaidimas
{
    class Task
    {
        /// <summary>
        /// Randami geriausias charakteristikas turintys herojai.
        /// </summary>
        /// <param name="heroes"> visų herojų sąrašas </param>
        /// <returns> Gražina geriausias charakteristikas turinčių herojų sąrašą </returns>
        public static List<Hero> BestCharacteristics (List<Hero> heroes)
        {
            List<Hero> BestHeroes = new List<Hero>();

            double max = 0;

            foreach ( Hero hero in heroes)
            {
                if (hero.Characteristics > max) max = hero.Characteristics;
            }

            foreach (Hero hero in heroes)
            {
                if (hero.Characteristics == max)
                {
                    BestHeroes.Add(hero);
                }
            }

            return BestHeroes;
        }
        /// <summary>
        /// Surušiuoja herojus pagal rases ir sudaro sąrašą kiek kokios rasės herojų yra.
        /// </summary>
        /// <param name="heroes"> visi herojai </param>
        /// <returns> gražina rasių sąrašą </returns>
        public static List<Races> SortRaces (List<Hero> heroes)
        {
            List<Races> Races = new List<Races>();

            foreach (Hero hero in heroes)
            {
                bool newRace = true;
                string h = hero.Race;
                foreach (Races r in Races )
                {
                    if ( r.RaceName == h )
                    {
                        r.NumberOfMembers++;
                        newRace = false;
                    }
                }

                if (newRace)
                {
                    Races newRaces = new Races(hero.Race);
                    Races.Add(newRaces);
                }
            }

            return Races;
        }

        /// <summary>
        /// Randa kuri rasė turi daugiausiai herojų
        /// </summary>
        /// <param name="races"> visos rasės ir narių skaičius </param>
        /// <returns> gražina didžiausios rasės narių skaičių </returns>
        public static int BiggestGroup(List<Races> races)
        {
            int maxi = 0;

            foreach (Races r in races)
            {
                if (r.NumberOfMembers > maxi) maxi = r.NumberOfMembers;
            }

            return maxi;
        }
        
    }
}
