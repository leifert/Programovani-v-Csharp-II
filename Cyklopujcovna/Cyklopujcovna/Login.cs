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
using Microsoft.Data.Sqlite;

namespace cyklopujcovna
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0)
            {
                MessageBox.Show("Vyplňte všechna pole");
            }
            else
            {
                Employee employee = new Employee();
                employee.Name = textBox1.Text;
                employee.Passwd = textBox2.Text;
                if (await DataMapper.Login(employee))
                {
                    MainForm main = new MainForm(this);
                    main.Show();
                    textBox1.Clear();
                    textBox2.Clear();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Špatné přihlašovací údaje!");
                }
            }
           
           
        }
    }
}
