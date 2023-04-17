using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cyklopujcovna
{
    public partial class BicyclesForm : Form
    {
        private MainForm main;
        private BindingList<Bike> _viewBikes;
        private BindingList<Bike> _filteredView;
    public BicyclesForm(MainForm main)
        {
            this.main = main;
            InitializeComponent();
            dataGridViewBicycles.AutoGenerateColumns = false;
            dataGridViewBicycles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridViewBicycles.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Název",
                DataPropertyName = nameof(Bike.BikeName)

            });
            dataGridViewBicycles.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Typ",
                DataPropertyName = nameof(Bike.BikeType)
            });
            dataGridViewBicycles.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Cena/den",
                DataPropertyName = nameof(Bike.BikePrice)
            });

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.BackColor = Color.FromArgb(91,187,91);


            dataGridViewBicycles.Columns.Add(new DataGridViewButtonColumn()
            {
                HeaderText = "",
                UseColumnTextForButtonValue = true,
                Text = "Vypůjčit",
                Name = "vypujcit",
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = style
            });

            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            style2.BackColor = Color.DeepSkyBlue;

            dataGridViewBicycles.Columns.Add(new DataGridViewButtonColumn()
            {
                HeaderText = "",
                UseColumnTextForButtonValue = true,
                Text = "Upravit",
                Name = "upravit",
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = style2
            });

            DataGridViewCellStyle style3 = new DataGridViewCellStyle();
            style3.BackColor = Color.Red;

            dataGridViewBicycles.Columns.Add(new DataGridViewButtonColumn()
            {
                HeaderText = "",
                UseColumnTextForButtonValue = true,
                Text = "Smazat",
                Name = "smazat",
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = style3
            });


            SetData();
            dataGridViewBicycles.DataSource = _viewBikes;
            _filteredView = new BindingList<Bike>(_viewBikes);


            comboBox1.DataSource = new List<string>()
            {
                "Všechna","Horské","Silniční","Cyklokrosové"
            };
        }

   

        private async void dataGridViewBicycles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            var col = dataGridViewBicycles.Columns[e.ColumnIndex];
            Bike b = this._filteredView[e.RowIndex];
            if (col.Name == "vypujcit")
            {
                main.OpenChildForm(new RentalInsertForm(b,main));

            }
            else if (col.Name == "upravit")
            {
                main.OpenChildForm(new BikeEditForm(b,main));
            }
            else if (col.Name == "smazat")
            {
                DialogResult dialogResult = MessageBox.Show( "Opravdu chcete odstranit tuto položku?","Cyklopůjčovna", MessageBoxButtons.YesNo);
                if(dialogResult == DialogResult.Yes)
                {
                    await DataMapper.Delete(b);
                    DialogResult dialog = MessageBox.Show("Položka byla odstraněna!");
                    main.OpenChildForm(new BicyclesForm(main));
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
                
            }
        }

        private async void SetData()
        {
            _viewBikes = await DataMapper.Select<Bike>();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            if (textBoxNameFil.Text.Length == 0)
            {
                if (comboBox1.SelectedItem.ToString() == "Všechna")
                {
                    dataGridViewBicycles.DataSource = _viewBikes;
                    _filteredView = new BindingList<Bike>(_viewBikes);
                }
                else
                {
                    _filteredView= new BindingList<Bike>(_viewBikes.Where(x=>x.BikeType.Contains(comboBox1.SelectedItem.ToString())).ToList());
                    dataGridViewBicycles.DataSource = _filteredView;
                }
                
            }
            else if (textBoxNameFil.Text.Length != 0)
            {
                if (comboBox1.SelectedItem.ToString() == "Všechna")
                {
                    _filteredView = new BindingList<Bike>(_viewBikes.Where(item => item.BikeName.Contains(textBoxNameFil.Text)).ToList());
                    dataGridViewBicycles.DataSource = _filteredView;
                }
                else
                {
                    _filteredView = new BindingList<Bike>(_viewBikes.Where(item => item.BikeName.Contains(textBoxNameFil.Text)).Where(x=>x.BikeType.Contains(comboBox1.SelectedItem.ToString())).ToList());
                    dataGridViewBicycles.DataSource = _filteredView;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Bike b = new Bike();
            main.OpenChildForm(new BikeInsertForm(b,main));
        }
    }
}
