using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lab_4
{
    public class ResearchTeam : Team, IEnumerable, INotifyPropertyChanged
    {
        private string theme; 
        private TimeFrame timeLong;
        private List<Person> persons;
        private List<Paper> papers;
        
        public ResearchTeam() : this("Try programming", "Homyzhenko", TimeFrame.Long) { }
        public ResearchTeam(string theme, string name, TimeFrame timeFrame)
        {
            this.theme = theme;
            this.name = name;
            this.timeLong = timeFrame;
            this.persons = new List<Person>();
            this.papers = new List<Paper>();
        }
        public string Theme
        {
            get { return theme; }
            set { theme = value; }
        }
        public Team team
        {
            get
            {
                Team team = new Team(this.name, this.number);
                return team;
            }
            set
            {
                this.name = value.Name;
                this.number = value.Number;
            }
        }
        public DateTime data
        {
            get
            {
                return papers.Max(obj => obj.date);
            }
        }
        public TimeFrame GetTime()
        {
            return timeLong;
        }
        public Paper findPaper()
        {
            if (papers == null) return null;
            Paper answer = new Paper();
            foreach (Paper point in papers)
            {
                if (DateTime.Compare(point.date, answer.date) == 1)
                {
                    answer = point;
                }
            }
            return answer;
        }
        public List<Paper> Papers() { return papers; }
        public List<Person> Persons() { return persons; }
        public void AddPersons(params Person[] items)
        {
            foreach (Person person in items)
            {
                persons.Add(person);
            }
        }
        public bool this[TimeFrame frame]
        {
            get { return (frame == timeLong); }
        }

        public void AddPersons(List<Person> persons)
        {
            foreach (Person person in persons)
            {
                this.persons.Add(person);
            }
        }
        public void AddPapers(List<Paper> papers)
        {
            foreach (Paper paper in papers)
            {
                this.papers.Add(paper);
            }
        }
        public void addPapers(params Paper[] items)
        {
            foreach (Paper paper in items)
            {
                papers.Add(paper);
            }
        }
        public override string ToString()
        {
            string list = "---------------------------------\n";
            foreach (Paper paper in papers)
            {
                list += paper.ToString() + "---------------------------------\n";
            }

            return "Thheme:\t" + theme + "\nAuthor:\t" + name + "\nNumber:\t" + number
                + '\n' + list + "=================================\n";
        }
        public string ToShortString()
        {
            return "Thheme:\t" + theme + "\nAuthor:\t" + name + "\nNumber:\t" + number
                + "\n=================================\n";
        }
        public object DeepCopy()
        {
            ResearchTeam tmp = new ResearchTeam();
            tmp.theme = theme;
            tmp.name = name;
            tmp.number = number;
            tmp.timeLong = timeLong;
            tmp.papers = papers;
            return tmp;
        }

        public void sortByData()
        {
            papers.Sort();
        }
        public void sortByAuthor()
        {
            PaperComparerBySurname comparer = new PaperComparerBySurname();
            papers.Sort(comparer);
        }
        public void sortByTitle()
        {
            IComparer<Paper> comparer = (IComparer<Paper>)new Paper();
            papers.Sort(comparer);
        }

        IEnumerable<Person> GetEnumerator()
        {
            for (int i = 0; i < persons.Count; i++)
            {
                bool flag = false;
                Person tmp_person = (Person)persons[i];
                for (int j = 0; j < papers.Count; j++)
                {
                    Paper tmp_paper = (Paper)papers[j];
                    if (tmp_person == tmp_paper.author) flag = true;
                }
                if (!flag) {
                    yield return tmp_person; 
                }
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return persons.GetEnumerator();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void CirculationChanged(string value)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(value));
        }
        public void DateChanged(string value)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(value));
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
