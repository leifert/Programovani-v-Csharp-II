using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cyklopujcovna
{
  

    public partial class PersonsThreadForm : Form
    {
        private MainForm main;
        private List<Person> _viewPersons = new List<Person>();
        private readonly object _objLock = new object();
        private PeopleData peopleData = new PeopleData();
        public PersonsThreadForm(MainForm main)
        {
            this.main = main;
            InitializeComponent();
            dataGridViewPersons.AutoGenerateColumns = false;
            dataGridViewPersons.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridViewPersons.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Jméno",
                DataPropertyName = nameof(Person.Name)

            });
            dataGridViewPersons.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Pohlaví",
                DataPropertyName = nameof(Person.Sex)
            });
            dataGridViewPersons.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Věk",
                DataPropertyName = nameof(Person.Age)
            });
            dataGridViewPersons.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Výška",
                DataPropertyName = nameof(Person.Height)
            });
            dataGridViewPersons.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Váha",
                DataPropertyName = nameof(Person.Weight)
            });
            UpdateGrid();
            
           

        }

        private void UpdateGrid()
        {
            dataGridViewPersons.Rows.Clear();

            foreach (var person in peopleData.Data)
            {
                dataGridViewPersons.Rows.Add(person.Name, person.Sex, person.Age, person.Height, person.Weight);
            }
        }

        private void Data_ListChanged(object sender, ListChangedEventArgs e)
        {
            var data = sender as List<Person>;

            Action action = delegate { _viewPersons.Add(data.Last()); };
            if (this.IsHandleCreated)
                this.BeginInvoke(action);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            label1.Text = "Generování dat...";
            peopleData.Start();
        }

       

        private void btnStop_Click(object sender, EventArgs e)
        {
            label1.Text = "Hotovo!";
            peopleData.Stop();
            UpdateGrid();
        }

       
    }

    public enum Sex
    {
        Man,
        Woman
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Sex Sex { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
    }


    public class PeopleData
    {
        private readonly object _objLock = new object();

        private readonly string[] womenNames = new[]
        {
            "Jana", "Marie", "Eva", "Eva", "Hana", "Anna", "Lenka", "Kateřina", "Lucie", "Věra", "Petra", "Veronika",
            "Jaroslava", "Tereza", "Martina", "Michaela", "Jitka", "Helena", "Ludmila", "Zdeňka", "Ivana", "Monika",
            "Eliška", "Zuzana"
        };

        private readonly string[] menNames = new[]
        {
            "Jiří", "Jan", "Petr", "Josef", "Pavel", "Martin", "Tomáš", "Jaroslav", "Miroslav", "Zdeněk", "Václav",
            "Michal", "František", "Jakub", "Milan", "Karel", "Lukáš", "David", "Vladimír", "Ondřej", "Ladislav",
            "Roman", "Marek", "Stanislav", "Daniel", "Radek", "Antonín",
            "Vojtěch", "Filip", "Adam", "Matěj"
        };
        Random random = new Random();
        public List<Person> Data { get; } = new List<Person>() {};
        bool running;
       

        public void Add(Person p)
        {
            lock (_objLock)
            {
                Data.Add(p);
            }
           
        }
        public void Start()
        {
            running = true;
            new Task(() =>
            {
                while (running)
                {
                    Add(Generate(Sex.Man));
                    int max = random.Next(10);
                    for (int i = 0; i < max; i++)
                    {
                        Thread.Sleep(1000);
                    }
                }
            }).Start();
            new Task(() =>
            {
                while (running)
                {
                    Add(Generate(Sex.Woman));
                    int max = random.Next(10);
                    for (int i = 0; i < max; i++)
                    {
                        Thread.Sleep(1000);
                    }
                }
            }).Start();
        }

        public void Stop()
        {
            running = false;
        }

        private Person Generate(Sex sex) => new Person()
        {
            
            Name = sex.ToString() == "Woman" ? womenNames[random.Next(womenNames.Length)] : menNames[random.Next(menNames.Length)],
            Sex = sex,
            Age = random.Next(18,99),
            Weight = random.Next(45, 130),
            Height = random.Next(150, 200)
        };
    }
}
