using System.Collections.Generic;

namespace StudentuAtstovybe
{
    class Task
    {
        private List<Member> AllMembers { get; set; }
        /// <summary>
        /// Visų 3 metų nariai
        /// </summary>
        /// <param name="allMembers"></param>
        public Task(List<Member> allMembers)
        {
            this.AllMembers = allMembers;
        }

        public Task()
        {
            this.AllMembers = new List<Member>();
        }
        /// <summary>
        /// Pridėti narius į visų narių konteinerį.
        /// </summary>
        /// <param name="members">Nariai</param>
        public void Add (List<Member> members)
        {
            foreach (Member member in members)
            {
                if (!this.AllMembers.Contains(member))
                {
                    this.AllMembers.Add(member);
                }
                else
                {
                    this.AllMembers.Remove(member);
                    this.AllMembers.Add(member);
                }
            } 
        }
        /// <summary>
        /// Gauti visų narių sąraša
        /// </summary>
        /// <returns></returns>
        public List<Member> GetMembers()
        {
            return this.AllMembers;
        }

        /// <summary>
        /// Rasti vyriausia atstovybės narį.
        /// </summary>
        /// <param name="members">Nariai</param>
        /// <param name="temp">Vyriausias narys</param>
        public static Graduate FindOldestGraduate(List<Member> members, Graduate temp)
        {
            foreach (Member member in members)
            {
                if (member.Age > temp.Age && member is Graduate)
                {
                    temp = (Graduate)member;
                }
            }
            return temp;
        }
        /// <summary>
        /// Suranda visus vyriausius narius.
        /// </summary>
        /// <param name="members">Nariai</param>
        /// <param name="temp">Vyriausias narys</param>
        public static List<Member> FindOldestGraduates (List<Member> members, Graduate temp, List<Member> OldestGraduates)
        {
            foreach (Member member in members)
            {
                if( member.Age == temp.Age)
                {
                    if (!OldestGraduates.Contains(member))
                    {
                        OldestGraduates.Add(member);
                    }
                }
            }

            return OldestGraduates;
        }

        /// <summary>
        /// Rasti narius vyresnius už 30
        /// </summary>
        /// <param name="members">Nariai</param>
        /// <param name="oldMembers">"Senių" sąrašas</param>
        public static List<Member> FindOldMembers(List<Member> members, List<Member> oldMembers)
        {
            foreach (Member member in members)
            {
                if (member.Age > 30)
                {
                    if (!oldMembers.Contains(member))
                    {
                        oldMembers.Add(member);
                    }
                }
            }

            return oldMembers;
        }
        /// <summary>
        /// Burbuliuko būdo rūšiavimas
        /// </summary>
        /// <param name="members">Nariai</param>
        /// <returns>Surušiuotas sąrašas</returns>
        public static List<Member> Sort(List<Member> members)
        {
            bool flag = true;
            while (flag)
            {
                flag = false;
                for (int i = 0; i < members.Count - 1; i++)
                {
                    Member a = members[i];
                    Member b = members[i + 1];

                    if (a.CompareTo(b) > 0)
                    {
                        members[i] = b;
                        members[i + 1] = a;
                        flag = true;
                    }
                }
            }

            return members;
        }
        /// <summary>
        /// Rasti Atea darbuotojus
        /// </summary>
        /// <param name="members">Nariai</param>
        /// <param name="workers">Atea darbuotojų sąrašas</param>
        public static List<Member> FindAteaWorkers(List<Member> members, List<Member> workers)
        {
            foreach (Member member in members)
            {
                if (member is Graduate)
                {
                    if (((Graduate)member).WorkPlace == "ATEA")
                    {
                        if (!workers.Contains(member))
                        {
                            workers.Add(member);
                        }
                    }
                }
            }

            return workers;
        }
        /// <summary>
        /// Randa narius kurie išėjo iš narių komiteto
        /// </summary>
        /// <param name="membersYearAgo">Nariai metai anksčiau</param>
        /// <param name="membersNow">Nariai dabar</param>
        /// <param name="notMembersAnymore">Išėja nariai</param>
        public static List<Member> CheckIfMemberLeft(List<Member> membersYearAgo,
            List<Member> membersNow, List<Member> notMembersAnymore)
        {
            foreach (Member member in membersYearAgo)
            {
                if (!membersNow.Contains(member))
                {
                    if (!notMembersAnymore.Contains(member))
                    {
                        notMembersAnymore.Add(member);
                    }
                }
            }

            return notMembersAnymore;
        }
    }
}
