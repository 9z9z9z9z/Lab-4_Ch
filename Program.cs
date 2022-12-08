using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.ComponentModel;

namespace Lab_4
{
    public delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);
    public delegate TKey KeySelector<TKey>(ResearchTeam rt);
    public delegate void ResearchTeamChangedHandler<TKey>(object source, ResearchTeamChangedEventArgs<TKey> args);

    public class Program
    {
        
        static public KeyValuePair<int, string> generator(int i)
        {
            string value = (i * i * i * i * i * i * i).ToString();
            int key = i;
            return new KeyValuePair<int, string>(key, value);
        }

        static public KeySelector<string> keySelector = delegate (ResearchTeam rt)
        {
            return rt.GetHashCode().ToString();
        };
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            List<Person> persons1 = new List<Person>(2);
            persons1.Add(new Person("Emil", "Markov", DateTime.Parse("14.11.2003")));
            persons1.Add(new Person("Kolya", "Homyzhenko", DateTime.Parse("11.06.2003")));
            List<Paper> paper1 = new List<Paper>(2);
            paper1.Add(new Paper("Title1", new Person("Egor", "Teterchev", DateTime.Parse("10.08.2003")), DateTime.Parse("10.08.2003")));
            paper1.Add(new Paper("Title2" ,new Person("Dima", "Panfilov", DateTime.Parse("09.07.2003")), DateTime.Parse("09.07.2003")));
            ResearchTeam t1 = new ResearchTeam("MIET", "SPinteh", TimeFrame.Long);
            t1.AddPersons(persons1);
            t1.AddPapers(paper1);

            List<Person> persons2 = new List<Person>(2);
            persons1.Add(new Person(Inputs.RandomString(4), Inputs.RandomString(4), DateTime.Parse("14.11.2003")));
            persons1.Add(new Person(Inputs.RandomString(4), Inputs.RandomString(4), DateTime.Parse("11.06.2003")));
            List<Paper> paper2 = new List<Paper>(2);
            paper1.Add(new Paper(Inputs.RandomString(4), new Person(Inputs.RandomString(4), Inputs.RandomString(4), DateTime.Parse("10.08.2003")), DateTime.Parse("10.08.2003")));
            paper1.Add(new Paper(Inputs.RandomString(4), new Person(Inputs.RandomString(4), Inputs.RandomString(4), DateTime.Parse("09.07.2003")), DateTime.Parse("09.07.2003")));
            ResearchTeam t2 = new ResearchTeam(Inputs.RandomString(4), Inputs.RandomString(4), TimeFrame.Long);
            t2.AddPersons(persons2);
            t2.AddPapers(paper2);
            List<Person> persons3 = new List<Person>(2);
            persons1.Add(new Person(Inputs.RandomString(4), Inputs.RandomString(4), DateTime.Parse("14.11.2003")));
            persons1.Add(new Person(Inputs.RandomString(4), Inputs.RandomString(4), DateTime.Parse("11.06.2003")));
            List<Paper> paper3 = new List<Paper>(2);
            paper1.Add(new Paper(Inputs.RandomString(4), new Person(Inputs.RandomString(4), Inputs.RandomString(4), DateTime.Parse("10.08.2003")), DateTime.Parse("10.08.2003")));
            paper1.Add(new Paper(Inputs.RandomString(4), new Person(Inputs.RandomString(4), Inputs.RandomString(4), DateTime.Parse("09.07.2003")), DateTime.Parse("09.07.2003")));
            ResearchTeam t3 = new ResearchTeam(Inputs.RandomString(4), Inputs.RandomString(4), TimeFrame.Long);
            t3.AddPersons(persons3);
            t3.AddPapers(paper3);
            List<Person> persons4 = new List<Person>(2);
            persons1.Add(new Person(Inputs.RandomString(4), Inputs.RandomString(4), DateTime.Parse("14.11.2003")));
            persons1.Add(new Person(Inputs.RandomString(4), Inputs.RandomString(4), DateTime.Parse("11.06.2003")));
            List<Paper> paper4 = new List<Paper>(2);
            paper1.Add(new Paper(Inputs.RandomString(4), new Person(Inputs.RandomString(4), Inputs.RandomString(4), DateTime.Parse("10.08.2003")), DateTime.Parse("10.08.2003")));
            paper1.Add(new Paper(Inputs.RandomString(4), new Person(Inputs.RandomString(4), Inputs.RandomString(4), DateTime.Parse("09.07.2003")), DateTime.Parse("09.07.2003")));
            ResearchTeam t4 = new ResearchTeam(Inputs.RandomString(4), Inputs.RandomString(4), TimeFrame.Long);
            t4.AddPersons(persons4);
            t4.AddPapers(paper4);

            ResearchTeamCollection<string> rtc1 = new ResearchTeamCollection<string>(keySelector);
            rtc1.AddDefaults(t1);
            rtc1.AddDefaults(t2);
            ResearchTeamCollection<string> rtc2 = new ResearchTeamCollection<string>(keySelector);
            rtc2.AddDefaults(t3);
            rtc2.AddDefaults(t4);

            TeamsJournal tj = new TeamsJournal();
            rtc1.reseachTeamChanged += tj.AddChanges;
            rtc2.reseachTeamChanged += tj.AddChanges;

            List<Person> persons5 = new List<Person>(2);
            persons1.Add(new Person(Inputs.RandomString(4), Inputs.RandomString(4), DateTime.Parse("14.11.2003")));
            persons1.Add(new Person(Inputs.RandomString(4), Inputs.RandomString(4), DateTime.Parse("11.06.2003")));
            List<Paper> paper5 = new List<Paper>(2);
            paper1.Add(new Paper(Inputs.RandomString(4), new Person(Inputs.RandomString(4), Inputs.RandomString(4), DateTime.Parse("10.08.2003")), DateTime.Parse("10.08.2003")));
            paper1.Add(new Paper(Inputs.RandomString(4), new Person(Inputs.RandomString(4), Inputs.RandomString(4), DateTime.Parse("09.07.2003")), DateTime.Parse("09.07.2003")));
            ResearchTeam t5 = new ResearchTeam(Inputs.RandomString(4), Inputs.RandomString(4), TimeFrame.Long);
            t5.AddPersons(persons5);
            t5.AddPapers(paper5);

            // Add element
            rtc1.AddDefaults(t5);
            rtc2.AddDefaults(t5);
            // Replace prop
            t1.Number = 10;

            // Replace elem
            rtc1.Replace(t1, t5);
            rtc2.Replace(t2, t5);

            // Delete 
            rtc1.Remove(t1);
            t1.Name = "Emil";

            // Change in replaced
            t2.Number = 8;

            Console.WriteLine(tj.ToString());

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
