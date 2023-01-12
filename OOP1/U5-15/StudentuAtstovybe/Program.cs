using System;
using System.Collections.Generic;
using System.IO;

namespace StudentuAtstovybe
{
    class Program
    {
        static void Main(string[] args)
        {
            const string Year1 = "Nariai2015.csv";
            const string Year2 = "Nariai2016.csv";
            const string Year3 = "Nariai2017.csv";

            Task Members = new Task();
            
            List<Member> year1Members = InOut.ReadMembers(Year1);
            List<Member> year2Members = InOut.ReadMembers(Year2);
            List<Member> year3Members = InOut.ReadMembers(Year3);

            Members.Add(year1Members);
            Members.Add(year2Members);
            Members.Add(year3Members);

            File.Delete("duomenys.txt");
            InOut.PrintToTxt(year1Members, "duomenys.txt");
            InOut.PrintToTxt(year2Members, "duomenys.txt");
            InOut.PrintToTxt(year3Members, "duomenys.txt");

            Graduate oldestMember = new Graduate("", "", DateTime.Today, "", "");
            oldestMember = Task.FindOldestGraduate(Members.GetMembers(), oldestMember);
            
            List<Member> oldestMembers = new List<Member>();
            oldestMembers = Task.FindOldestGraduates(Members.GetMembers(), oldestMember, oldestMembers);

            InOut.PrintToConsole(oldestMembers, "Vyriausi atsovybės nariai");

            List<Member> oldMembers = new List<Member>();
            oldMembers = Task.FindOldMembers(Members.GetMembers(), oldMembers);
            
            InOut.PrintToConsole(oldMembers, "Senių sąrašas");

            List<Member> ATEAWorkers = new List<Member>();
            ATEAWorkers = Task.FindAteaWorkers(Members.GetMembers(), ATEAWorkers);     

            Task.Sort(ATEAWorkers);
            InOut.PrintToCSV(ATEAWorkers, "Atea.csv");

            List<Member> NotMembersAnymore = new List<Member>();
            NotMembersAnymore = Task.CheckIfMemberLeft(year1Members, year2Members, NotMembersAnymore);
            NotMembersAnymore = Task.CheckIfMemberLeft(year2Members, year3Members, NotMembersAnymore);

            InOut.PrintToCSV(NotMembersAnymore, "Buvo.csv");

            Console.ReadKey();
        }
    }
}
