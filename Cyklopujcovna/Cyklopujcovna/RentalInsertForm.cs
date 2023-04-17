using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data;

namespace cyklopujcovna
{
    
    public partial class RentalInsertForm : Form
    {
        private Bike bike;
        private MainForm main;
        public RentalInsertForm(Bike bike, MainForm main)
        {
            InitializeComponent();
            this.bike = bike;
            this.main = main;
            labelKolo.Text = bike.BikeName;
            textBoxPrice.Text = bike.BikePrice.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private async void btnSave_Click(object sender, EventArgs e)
        {

            if (textBoxName.Text.Length != 0 && textBoxEmail.Text.Length != 0 && textBoxPrice.Text.Length != 0 && textBoxTelefon.Text.Length != 0)
            {
                DialogResult dialogResult = MessageBox.Show( "Vytvořit novou výpůjčku?","Cyklopůjčovna", MessageBoxButtons.YesNo);
                if(dialogResult == DialogResult.Yes)
                {
                    Rental rent = new Rental()
                    {
                        BikeId = bike.Id,
                        Start = dateTimePickerStart.Value.Date,
                        End = dateTimePickerEnd.Value.Date,
                        Price = Double.Parse(textBoxPrice.Text),
                        CustomerName = textBoxName.Text,
                        CustomerEmail = textBoxEmail.Text,
                        CustomerPhoneNumber = textBoxTelefon.Text
                    };

                    await DataMapper.Insert(rent);
                    DialogResult dialog = MessageBox.Show("Výpůjčka byla úspěšně vytvořena!");
                    main.OpenChildForm(new RentalForm(main));
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }
            else
            {
                MessageBox.Show("Vyplňte všechna pole");
            }

           
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            main.OpenChildForm(new BicyclesForm(main));
        }
    }
}
