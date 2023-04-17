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
    public partial class MainForm : Form
    {
        private Login login;
        public MainForm(Login login)
        {
            InitializeComponent();
            this.login = login;
            labelTop.Text = "Cyklopůjčovna";
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            login.Show();
            this.Close();
        }

        private Form _activeForm = null;
        public void OpenChildForm(Form childForm)
        {
            if (_activeForm != null) {_activeForm.Close();}

            _activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnBicycles_Click(object sender, EventArgs e)
        {
            labelTop.Text = "Nabídka kol";
            OpenChildForm(new BicyclesForm(this));
        }

        private void btnRentals_Click(object sender, EventArgs e)
        {
            labelTop.Text = "Výpůjčky";
            OpenChildForm(new RentalForm(this));
        }

        private void btnThreads_Click(object sender, EventArgs e)
        {
            labelTop.Text = "Vlákna";
            OpenChildForm(new PersonsThreadForm(this));
        }
    }
}
