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
    public partial class RentalForm : Form
    {
        private MainForm main;
        private BindingList<Rental> _viewRentals;
        private BindingList<Rental> _filteredView;
    public RentalForm(MainForm main)
        {
            this.main = main;
            InitializeComponent();
            dataGridViewRantals.AutoGenerateColumns = false;
            dataGridViewRantals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridViewRantals.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Výpůjčka č.",
                DataPropertyName = nameof(Rental.Id)

            });
            dataGridViewRantals.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Id kola",
                DataPropertyName = nameof(Rental.BikeId)
               
            });
            dataGridViewRantals.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Datum vypůjčení",
                DataPropertyName = nameof(Rental.Start)
            });
            dataGridViewRantals.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Datum vrácení",
                DataPropertyName = nameof(Rental.End)
            });
            dataGridViewRantals.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Cena",
                DataPropertyName = nameof(Rental.Price)
            });
            dataGridViewRantals.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Jméno vypůjčitele",
                DataPropertyName = nameof(Rental.CustomerName)
            });
            dataGridViewRantals.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Email",
                DataPropertyName = nameof(Rental.CustomerEmail)
            });
            dataGridViewRantals.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Telefon",
                DataPropertyName = nameof(Rental.CustomerPhoneNumber)
            });


            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            style2.BackColor = Color.DeepSkyBlue;

            dataGridViewRantals.Columns.Add(new DataGridViewButtonColumn()
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

            dataGridViewRantals.Columns.Add(new DataGridViewButtonColumn()
            {
                HeaderText = "",
                UseColumnTextForButtonValue = true,
                Text = "Smazat",
                Name = "smazat",
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = style3
            });


            SetData();
            dataGridViewRantals.DataSource = _viewRentals;
            _filteredView = new BindingList<Rental>(_viewRentals);


            comboBox1.DataSource = new List<string>()
            {
                "","Č. výpůjčky","Jméno","Email","Telefon"
            };
        }

        private async void dataGridViewBicycles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            var col = dataGridViewRantals.Columns[e.ColumnIndex];
            Rental rental = this._filteredView[e.RowIndex];
            
            if (col.Name == "upravit")
            {
               main.OpenChildForm(new RentalUpdateForm(rental,main));
            }
            else if (col.Name == "smazat")
            {
                DialogResult dialogResult = MessageBox.Show( "Opravdu chcete odstranit tuto položku?","Cyklopůjčovna", MessageBoxButtons.YesNo);
                if(dialogResult == DialogResult.Yes)
                {
                    await DataMapper.Delete(rental);
                    DialogResult dialog = MessageBox.Show("Položka byla odstraněna!");
                    main.OpenChildForm(new RentalForm(main));
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
                
            }
        }

        
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private async void SetData()
        {
            _viewRentals = await DataMapper.Select<Rental>();
        }
        private void buttonFind_Click(object sender, EventArgs e)
        {
            if (textBoxNameFil.Text.Length == 0)
            {
                if (comboBox1.SelectedItem.ToString() == "")
                {
                    dataGridViewRantals.DataSource = _viewRentals;
                    _filteredView = new BindingList<Rental>(_viewRentals);
                }
                
            }
            else if (textBoxNameFil.Text.Length != 0)
            {
                if (comboBox1.SelectedItem.ToString() == "")
                {
                    dataGridViewRantals.DataSource = _viewRentals;
                    _filteredView = new BindingList<Rental>(_viewRentals);
                }
                else if(comboBox1.SelectedItem.ToString() == "Č. výpůjčky")
                {
                    var isNumeric = int.TryParse(textBoxNameFil.Text, out _);
                    if (!isNumeric)
                    {
                        MessageBox.Show("Zadejte číslo");
                        return;

                    }
                    _filteredView = new BindingList<Rental>(_viewRentals.Where(item => item.Id.Equals(Int32.Parse(textBoxNameFil.Text))).ToList());
                    dataGridViewRantals.DataSource = _filteredView;
                }
                else if(comboBox1.SelectedItem.ToString() == "Jméno")
                {
                    _filteredView = new BindingList<Rental>(_viewRentals.Where(item => item.CustomerName.Contains(textBoxNameFil.Text)).ToList());
                    dataGridViewRantals.DataSource = _filteredView;
                }
                else if(comboBox1.SelectedItem.ToString() == "Email")
                {
                    _filteredView = new BindingList<Rental>(_viewRentals.Where(item => item.CustomerEmail.Contains(textBoxNameFil.Text)).ToList());
                    dataGridViewRantals.DataSource = _filteredView;
                }
                else if(comboBox1.SelectedItem.ToString() == "Telefon")
                {
                    _filteredView = new BindingList<Rental>(_viewRentals.Where(item => item.CustomerPhoneNumber.Contains(textBoxNameFil.Text)).ToList());
                    dataGridViewRantals.DataSource = _filteredView;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
           // Bike b = new Bike();
           // main.OpenChildForm(new BikeInsertForm(b,main));
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
