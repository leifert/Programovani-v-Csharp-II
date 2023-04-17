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
    
    public partial class BikeEditForm : Form
    {
        private Bike bike;
        private MainForm main;
        public BikeEditForm(Bike bike, MainForm main)
        {
            InitializeComponent();
            this.bike = bike;
            this.main = main;

            textBoxName.Text = bike.BikeName;
            textBoxType.Text = bike.BikeType;
            textBoxPrice.Text = bike.BikePrice.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private async void btnSave_Click(object sender, EventArgs e)
        {

            if (textBoxName.Text.Length != 0 && textBoxType.Text.Length != 0 && textBoxPrice.Text.Length != 0)
            {
                DialogResult dialogResult = MessageBox.Show( "Chcete uložit změny?","Cyklopůjčovna", MessageBoxButtons.YesNo);
                if(dialogResult == DialogResult.Yes)
                {
                    bike.BikeName = textBoxName.Text;
                    bike.BikeType = textBoxType.Text;
                    bike.BikePrice = Convert.ToDouble(textBoxPrice.Text);

                    await DataMapper.Update(bike);
                    DialogResult dialog = MessageBox.Show("Změna byla uožena!");
                    main.OpenChildForm(new BicyclesForm(main));
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
