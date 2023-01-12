using System.Collections.Generic;

namespace KrepsinioRinktine
{
    class PlayersRegister
    {
        private List<Player> AllPlayers;

        public PlayersRegister()
        {
            AllPlayers = new List<Player>();
        }
        /// <summary>
        /// Pridedami pirmieji žaidėjai į sąrašą
        /// </summary>
        public PlayersRegister( List<Player> players)
        {
            this.AllPlayers = players;
        }

        /// <summary>
        /// Pridedami nesikartojantys žaidėjai į sąrašą.
        /// </summary>
        public void Add(List<Player> players)
        {
            foreach (Player p in players)
            {
                if (!this.AllPlayers.Contains(p)) this.AllPlayers.Add(p);
            }
        }

        /// <summary>
        /// Gražinamas žaidėjų sąrašas.
        /// </summary>
        public List<Player> GetList ()
        {
            return AllPlayers;
        }
    }
}
