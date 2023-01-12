using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StudentuAtstovybe
{
    class InOut
    {
        /// <summary>
        /// Nuskaitomi nariai iš duomenų failo
        /// </summary>
        /// <param name="fd">Failo pavadinimas</param>
        /// <returns>Gražina sąrašą su nariais</returns>
        public static List<Member> ReadMembers(string fd)
        {
            List<Member> Members = new List<Member>();
            string[] Lines = File.ReadAllLines(fd);
            for (int i = 1; i < Lines.Length; i++)
            {
                string[] value = Lines[i].Split(';');
                string surname = value[0];
                string name = value[1];
                DateTime birthDate = DateTime.Parse(value[2]);
                string phoneNumber = value[3];

                if (value.Length == 6)// student
                {
                    string iD = value[4];
                    int course = int.Parse(value[5]);

                    Student student = new Student(surname, name, birthDate, phoneNumber, iD, course);

                    if (!Members.Contains(student)) Members.Add(student);
                }
                else // graduate
                {
                    string workPlace = value[4];

                    Graduate graduate = new Graduate(surname, name, birthDate, phoneNumber, workPlace);

                    if (!Members.Contains(graduate)) Members.Add(graduate);
                }
            }

            return Members;
        }
        /// <summary>
        /// Išspausdinami pradiniai duomenys
        /// </summary>
        public static void PrintToTxt ( List<Member> members, string file)
        {
            string[] lines = new string[members.Count + 4];
            lines[0] = new string('-', 110);
            lines[1] = string.Format("| {0,-15} | {1,-15} | {2,-20} | {3,-16} | {4,-15} |",
                "Pavardė", "Vardas", "Gimimo data", "Telefono numeris", "ID/darbovietė");
            lines[2] = new string('-', 110);

            for (int i = 0; i < members.Count; i++)
            {
                lines[i + 3] = members[i].ToString();
            }

            lines[members.Count+3] = new string('-', 110);

            File.AppendAllLines(file, lines);
        }

        /// <summary>
        /// Išspausdinti narius į konsolę.
        /// </summary>
        /// <param name="members">Nariai</param>
        /// <param name="header">Antraštė</param>
        public static void PrintToConsole(List<Member> members, string header)
        {
            if ( members.Count != 0)
            {
                Separator();
                Console.WriteLine(header);
                Separator();
                foreach (Member member in members)
                {
                    Console.WriteLine(member.ToString());
                }
                Separator();
            }
            else
            {
                Console.WriteLine("\"Senių\" nėra.");
            }
            
        }
        /// <summary>
        /// Išspausdinti narius į csv failą
        /// </summary>
        /// <param name="members">Nariai</param>
        /// <param name="file">Failo pavadinimas</param>
        public static void PrintToCSV(List<Member> members, string file)
        {
            if (members.Count != 0)
            {
                string[] lines = new string[members.Count + 1];
                lines[0] = string.Format("{0};{1};{2};{3}",
                "Pavardė", "Vardas", "Gimimo data", "Telefono numeris");
                for (int i = 0; i < members.Count; i++)
                {
                    lines[i + 1] = string.Format("{0};{1};{2:yyyy-MM-dd};{3}", members[i].Surname, members[i].Name,
                        members[i].BirthDate, members[i].PhoneNumber);
                }
                File.WriteAllLines(file, lines, Encoding.UTF8);
            }
            else
            {
                string[] lines = new string[1];
                lines[0] = "Nėra";
                File.WriteAllLines(file, lines, Encoding.UTF8);
            }
        }
        /// <summary>
        /// Brūkšnys lentelėms.
        /// </summary>
        private static void Separator()
        {
            Console.WriteLine(new string('-', 110));
        }

    }
}
