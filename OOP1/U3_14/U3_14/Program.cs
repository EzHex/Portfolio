using System;
using System.IO;

namespace U3_14
{
    class Program
    {
        static void Main(string[] args)
        {
            const string resTxt = @"rezultatai.txt";
            const string inspectionTxt = @"Apžiūra.txt";
            File.Delete(resTxt);
            File.Delete(inspectionTxt);

            Register bothFilials = new Register();

            //6 Seniausios mašinos, 11 benzininių mašinų, apžiūros faile 1 mašina iš pirmo filialo ir antrame filiale nėra, 6 automobiliai pasikartoja per filialus.
            // Autos.csv ir Autos2.csv

            //1 Seniausia mašina, nėra benzininių mašinų, apžiūros faile 4 mašinos iš pirmo filialo ir 1 iš antro, klaidų nerasta.
            // Autos3.csv ir Autos.4csv

            Register register = InOut.ReadAutos(@"Autos.csv");
            Register register2 = InOut.ReadAutos(@"Autos2.csv");

            string a = string.Format("Pradiniai duomeys, {0}", register.FilialoInfo[0]);
            string b = string.Format("Pradiniai duomeys, {0}", register2.FilialoInfo[0]);
            InOut.PrintAutosToTxt(resTxt, a, register);
            InOut.PrintAutosToTxt(resTxt, b, register2);

            bothFilials.Merge(register, register2);

            InOut.PrintAutos("Seniausios mašinos" , bothFilials.FindOldestCar());

            bothFilials.FindGasCars("Benzininės mašinos");

            register.FindErrors(register2);

            register.CheckInspection(inspectionTxt);
            register2.CheckInspection(inspectionTxt);

            Console.ReadKey();
        }
    }
}
