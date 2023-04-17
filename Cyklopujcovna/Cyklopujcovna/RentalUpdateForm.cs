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
    
    public partial class RentalUpdateForm : Form
    {
        private Rental rent;
        private MainForm main;
        private Bike bk;
        public RentalUpdateForm(Rental rent, MainForm main)
        {
            InitializeComponent();
            this.rent = rent;
            this.main = main;
            
            bk = SetBike().Result;
           
            dateTimePickerStart.Value = this.rent.Start;
            dateTimePickerEnd.Value = this.rent.End;
            labelKolo.Text = bk.BikeName;
            textBoxName.Text = rent.CustomerName;
            textBoxEmail.Text = rent.CustomerEmail;
            textBoxTelefon.Text = rent.CustomerPhoneNumber;
            textBoxPrice.Text = rent.Price.ToString();

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private async Task<Bike> SetBike()
        {
           Bike bk =  await DataMapper.SelectById(new Bike()
            {
                Id = rent.BikeId
            });
           return bk;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {

            if (textBoxName.Text.Length != 0 && textBoxEmail.Text.Length != 0 && textBoxPrice.Text.Length != 0 && textBoxTelefon.Text.Length != 0)
            {
                DialogResult dialogResult = MessageBox.Show( "Chcete uložit změny?","Cyklopůjčovna", MessageBoxButtons.YesNo);
                if(dialogResult == DialogResult.Yes)
                {
                    Rental rent = new Rental()
                    {
                        Id = this.rent.Id,
                        BikeId = bk.Id,
                        Start = dateTimePickerStart.Value.Date,
                        End = dateTimePickerEnd.Value.Date,
                        Price = Double.Parse(textBoxPrice.Text),
                        CustomerName = textBoxName.Text,
                        CustomerEmail = textBoxEmail.Text,
                        CustomerPhoneNumber = textBoxTelefon.Text
                    };

                    await DataMapper.Update(rent);
                    DialogResult dialog = MessageBox.Show("Změny byly úspěšně uloženy!");
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
            main.OpenChildForm(new RentalForm(main));
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
