using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace U3_14
{
    class Container
    {
        private Auto[] AllAutos;
        private int Capacity;
        public int Count { get; private set; }
        /// <summary>
        /// Sukuriamas automobilių konteineris(sąrašas)
        /// </summary>
        /// <param name="capacity">Nurodomas dydis arba standartinis 20</param>
        public Container( int capacity = 20)
        {
            this.Capacity = capacity;
            AllAutos = new Auto[this.Capacity];
            this.Count = 0;
        }
        /// <summary>
        /// Pridedamas automobilis į konteinerį.
        /// </summary>
        /// <param name="auto">Automobilis</param>
        public void Add (Auto auto)
        {
            if (this.Count == this.Capacity) //container is full
            {
                EnsureCapacity(this.Capacity * 2);
            }
            this.AllAutos[this.Count++] = auto;
        }
        /// <summary>
        /// Nusiunčiamas reikalingas automobilis pagal indeksą.
        /// </summary>
        /// <param name="index">Indeksas</param>
        /// <returns>Gražinama mašina</returns>
        public Auto Get ( int index )
        {
            return this.AllAutos[index];
        }
        /// <summary>
        /// Automobilis padedamas į konteinerio vietą pagal indeksą.
        /// </summary>
        /// <param name="auto">Automobilis</param>
        /// <param name="index">Indeksas</param>
        public void Put(Auto auto, int index)
        {
            this.AllAutos[index] = auto;
        }

        /// <summary>
        /// Įterpiamas automobilis į konteinerį.
        /// </summary>
        /// <param name="newAuto">Automobilis</param>
        /// <param name="index">Indeksas</param>
        public void Insert(Auto newAuto, int index)
        {
            this.Count++;
            if (this.Count == this.Capacity)
            {
                EnsureCapacity(this.Capacity * 2);
            }

            for (int i = this.Count - 1; i > index; i--)
            {
                this.AllAutos[i] = this.AllAutos[i - 1];
            }

            this.AllAutos[index] = newAuto;

        }
        /// <summary>
        /// Pašalinamas automobilis iš konteinerio.
        /// </summary>
        /// <param name="auto">Automobilis</param>
        public void Remove(Auto auto)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this.AllAutos[i].Equals(auto))
                {
                    for (int i2 = i; i2 < this.Count; i2++)
                    {
                        this.AllAutos[i2] = this.AllAutos[i2 + 1];
                    }
                    this.Count--;
                }
            }

        }
        /// <summary>
        /// Pašalinamas automobilis pagal duota indeksą.
        /// </summary>
        /// <param name="index">Indeksas</param>
        public void RemoveAt(int index)
        {
            for (int i = index; i < this.Count; i++)
            {
                this.AllAutos[i] = this.AllAutos[i + 1];
            }
            this.Count--;
        }
        /// <summary>
        /// Patikrinama ar konteineryje yra duota mašina.
        /// </summary>
        /// <param name="auto">Mašina</param>
        public bool Contains(Auto auto)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this.AllAutos[i].Equals(auto))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Padidinamas konteinerio dydis, kad tiltpu automobiliai.
        /// </summary>
        /// <param name="minimumCapacity">Reikiama talpa</param>
        private void EnsureCapacity(int minimumCapacity)
        {
            if (minimumCapacity > this.Capacity)
            {
                Auto[] temp = new Auto[minimumCapacity];
                for (int i = 0; i < this.Count; i++)
                {
                    temp[i] = this.AllAutos[i];
                }
                this.Capacity = minimumCapacity;
                this.AllAutos = temp;
            }
        }

        /// <summary>
        /// Surušiuojami automobiliai išrinkimo metodu.
        /// </summary>
        /// <param name="container">Konteineris</param>
        public void Sort()
        {
            bool flag = true;
            while (flag)
            {
                flag = false;
                for (int i = 0; i < this.Count - 1; i++)
                {
                    Auto a = this.AllAutos[i];
                    Auto b = this.AllAutos[i+1];

                    if (a.CompareTo(b) > 0)
                    {
                        Swap(a, i, b, i + 1);
                        flag = true;
                    }
                }
            }
        }

        /// <summary>
        /// Sukeičiami automobiliai vietomis konteineryje.
        /// </summary>
        /// <param name="a">Pirmas automobilis</param>
        /// <param name="i1">Pirmo automobilio vieta</param>
        /// <param name="b">Antras automobilis</param>
        /// <param name="i2">Antro automobilio vieta</param>
        private void Swap(Auto a, int i1, Auto b, int i2)
        {
            this.AllAutos[i1] = b;
            this.AllAutos[i2] = a;
        }

    }
}
