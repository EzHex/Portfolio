using System.Collections.Generic;

namespace KrepsinioRinktine
{
    class Task
    {
        /// <summary>
        /// Rušiuojami žaidėjai ir randami žalgirio žaidėjai.
        /// </summary>
        /// <param name="players"> Sąrašas į stovyklą atvykusių žaidėjų </param>
        /// <returns> Žalgirio žaidėjų sąrašas </returns>
        public static List<Player> SortZalgirisPlayers ( List<Player> players)
        {
            List<Player> sortedPlayers = new List<Player>();

            sortedPlayers = FindZalgirisPlayers(sortedPlayers, players);

            return sortedPlayers;
        }

        /// <summary>
        /// Randami žalgirio žaidėjai iš duoto sąrašo.
        /// </summary>
        /// <param name="sortedPlayers"> Surušiuotas žaidėjų sąrašas </param>
        /// <param name="players"> Pradinis sąrašas </param>
        /// <returns> Gražinamas surušiuotas sąrašas </returns>
        private static List<Player> FindZalgirisPlayers(List<Player> sortedPlayers, List<Player> players)
        {
            foreach (Player p in players)
            {
                if (p.Club.ToLower() == "žalgiris")
                {
                    if (!sortedPlayers.Contains(p)) sortedPlayers.Add(p);
                }
            }

            return sortedPlayers;
        }

        /// <summary>
        /// Randamas aukščiausių žaidėjų sąrašas.
        /// </summary>
        /// <param name="players"> Sąrašas į stovyklą atvykusių žaidėjų </param>
        /// <returns> Gražinamas aukščiausių žaidėjų sąrašas </returns>
        public static List<Player> HighestPlayers(List<Player> players)
        {
            List<Player> highestPlayers = new List<Player>();

            int temp = 0;
            temp = MaxHeight(players, temp);

            highestPlayers = SortMaxHeightPlayers(highestPlayers, players, temp);

            return highestPlayers;
        }

        /// <summary>
        /// Randamas aukščiausio žaidėjo ūgis.
        /// </summary>
        /// <param name="players"> Pradinis sąrašas </param>
        /// <param name="maxi"> Didžiausias ūgis </param>
        /// <returns> Gražinamas didžiausias ūgis </returns>
        private static int MaxHeight ( List<Player> players, int maxi)
        {
            foreach (Player p in players)
            {
                if (p > maxi) maxi = p.Height;
            }
            return maxi;
        }

        /// <summary>
        /// Rušiuojami žaidėjai, gaunamas aukščiausių žaidėjų sąrašas.
        /// </summary>
        /// <param name="highestPlayers"> Aukščiausių žaidėjų sąrašas </param>
        /// <param name="players"> Pradinis sąrašas </param>
        /// <param name="maxi"> Didžiasias ūgis </param>
        /// <returns> Gražinamas aukščiausių žaidėjų sąrašas </returns>
        private static List<Player> SortMaxHeightPlayers ( List<Player> highestPlayers, List<Player> players, int maxi )
        {
            foreach (Player p in players)
            {
                if (p == maxi && !highestPlayers.Contains(p)) highestPlayers.Add(p);
            }
            return highestPlayers;
        }

        /// <summary>
        /// Randami žaidėjai 2 metrų ūgio ir aukštesni.
        /// </summary>
        /// <param name="players"> Sąrašas į stovyklą atvykusių žaidėjų </param>
        /// <returns> Gražinamas sąrašas žaidėjų nemažesnių kaip 2 metrai </returns>
        public static List<Player> PlayersOver2M ( List<Player> players)
        {
            List<Player> sortedPlayers = new List<Player>();

            sortedPlayers = FindPlayersOver2M(sortedPlayers, players);

            return sortedPlayers;
        }

        /// <summary>
        /// Randami žaidėjai aukštesni už 2 metrus.
        /// </summary>
        /// <param name="sortedPlayers"> Išrušiuotų žaidėjų sąrašas </param>
        /// <param name="players"> Pradinis žaidėjų sąrašas </param>
        /// <returns> Gražinamas surušiuotas sąrašas </returns>
        private static List<Player> FindPlayersOver2M ( List<Player> sortedPlayers, List<Player> players)
        {
            foreach (Player p in players)
            {
                if (p.Height >= 200 && !sortedPlayers.Contains(p)) sortedPlayers.Add(p);
            }

            return sortedPlayers;
        }
    }
}
