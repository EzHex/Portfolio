using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KompiuterinisZaidimas
{
    class Races
    {
        public string RaceName { get; set; }
        public int NumberOfMembers { get; set; }
        /// <summary>
        /// Sukuriama rasė, jos pavadinimas ir narių skaičius
        /// </summary>
        /// <param name="raceName"> rasės pavadinimas </param>
        public Races ( string raceName )
        {
            this.RaceName = raceName;
            this.NumberOfMembers = 1;
        }
    }
}
