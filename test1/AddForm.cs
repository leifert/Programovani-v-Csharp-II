using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class AddForm : Form
    {
        private Product _product;
        public AddForm(Product product)
        {
            InitializeComponent();
            this._product = product;
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text.Length == 0 && textBoxPrice.Text.Length == 0)
            {
                return;
            }
            _product.Name = textBoxName.Text;
            _product.Price = double.Parse(textBoxPrice.Text);
            this.Close();
        }
    }
}
