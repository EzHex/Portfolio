using System;
using System.IO;
using System.Text;

namespace U3_14
{
    class Register
    {
        public string[] FilialoInfo = new string[3];
        private Container AutoContainer;
        /// <summary>
        /// Sukuriamas mašinų registras.
        /// </summary>
        public Register()
        {
            this.AutoContainer = new Container();
        }

        public Register(Container autoContainer)
        {
            this.AutoContainer = autoContainer;
        }

        public Container GetContainer()
        {
            return this.AutoContainer;
        }
       
        
        /// <summary>
        /// Randama seniausia mašina.
        /// </summary>
        /// <returns>Gražinamas konteineris seniausių mašinų</returns>
        public Container FindOldestCar()
        {
            Container oldestCars = new Container();

            int maxi = AutoContainer.Get(0).CarAge;

            for (int i = 1; i < AutoContainer.Count; i++)
            {
                Auto auto = AutoContainer.Get(i);
                if (auto.CarAge > maxi) maxi = auto.CarAge;
            }

            for (int i = 0; i < AutoContainer.Count; i++)
            {
                Auto auto = AutoContainer.Get(i);
                if (maxi == auto.CarAge) oldestCars.Add(auto);
            }

            return oldestCars;
        }
        /// <summary>
        /// Randamos ir spausdinamos benzininės mašinos.
        /// </summary>
        /// <param name="header">Antraštė</param>
        public void FindGasCars (string header)
        {
            Container gasCars = new Container();
            int temp = 0;

            for (int i = 0; i < this.AutoContainer.Count; i++)
            {
                Auto auto = AutoContainer.Get(i);
                if (auto.Fuel.ToLower() == "benzinas")
                {
                    gasCars.Add(auto);
                    temp++;
                }
            }

            if ( temp != 0)
            {
                gasCars.Sort();
                InOut.PrintGasCars(header, gasCars);
            }
            else
            {
                Console.WriteLine("Benzininių automobilių nėra");
            }
        }
        
        /// <summary>
        /// Suliejami du registrai. Mašinos nepasikartoja.
        /// </summary>
        /// <param name="a">Pirmas registras</param>
        /// <param name="b">Antras registras</param>
        public void Merge (Register a, Register b)
        {
            for (int i = 0; i < a.AutoContainer.Count; i++)
            {
                Auto auto = a.AutoContainer.Get(i);
                if ( !this.AutoContainer.Contains(auto))
                {
                    this.AutoContainer.Add(auto);
                }
            }

            for (int i = 0; i < b.AutoContainer.Count; i++)
            {
                Auto auto = b.AutoContainer.Get(i);
                if (!this.AutoContainer.Contains(auto))
                {
                    this.AutoContainer.Add(auto);
                }
            }
        }
        /// <summary>
        /// Ieškomos klaidos filialuose.
        /// </summary>
        /// <param name="b">Antras registras</param>
        public void FindErrors ( Register b)
        {
            const string erorrFile = @"Klaidos.csv";
            File.Delete(erorrFile);
            string[] Lines = new string[1];
            Lines[0] = string.Format("{0};{1};{2}", "Valstybiniai numeriai", "Modelis", "Filialai");
            InOut.AppendToTxt(erorrFile, Lines);

            int temp = 0;

            for (int i = 0; i < this.AutoContainer.Count; i++)
            {
                Auto auto1 = this.AutoContainer.Get(i);
                for (int i2 = 0; i2 < b.AutoContainer.Count; i2++)
                {
                    Auto auto2 = b.AutoContainer.Get(i2);
                    if( auto1 == auto2)
                    {
                        temp++;
                        InOut.AppendToCsv(auto1, this.FilialoInfo[0], b.FilialoInfo[0], erorrFile);
                    }
                }
            }
            if ( temp == 0 )
            {
                File.Delete(erorrFile);
                string[] a = new string[1];
                a[0] = "Klaidų nerasta.";
                InOut.AppendToTxt(erorrFile, a);
            }      
        }
        
        /// <summary>
        /// Patikrinamos techninės apžiūros datos ir išspausdinamos mašinos į failą kurioms pasibaigs techninė apžiūra per ateinančius 3 mėnesius.
        /// </summary>
        /// <param name="fileName">Failo pavadinimas</param>
        public void CheckInspection(string fileName)
        {
            string[] Lines = new string[5];
            Lines[0] = new string('-', 85);
            Lines[1] = string.Format("| {0,-81} |", this.FilialoInfo[0]);
            Lines[2] = new string('-', 85);
            Lines[3] = string.Format("| {0,-12} | {1,-10} | {2,-22} | {3,-28} |", "Gamintojas", "Modelis", "Valstybiniai numeriai", "Techninės apžiūros pabaiga");
            Lines[4] = new string('-', 85);
            InOut.AppendToTxt(fileName, Lines);

            int temp = 0;
            for (int i = 0; i < this.AutoContainer.Count; i++)
            {
                Auto auto = this.AutoContainer.Get(i);
                DateTime today = DateTime.Today;
                if (auto.TechnicalInspection.Month >= today.Month && auto.TechnicalInspection.Month < today.Month + 3)
                {
                    InOut.AppendToTxt(fileName, auto);
                    temp++;
                }
            }

            if (temp == 0)
            {
                string[] a = new string[3];
                a[0] = "Nėra tokių mašinų";
                a[1] = new string('-', 85);
                a[2] = new string(' ', 1);
                InOut.AppendToTxt(fileName, a);
            }
            else 
            {
                string[] a = new string[2];
                a[0] = new string('-', 85);
                a[1] = new string(' ', 1);
                InOut.AppendToTxt(fileName, a);
            }
        }

    }
}
