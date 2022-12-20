using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private BindingList<Product> view = new BindingList<Product>();
        public Form1()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Name",
                DataPropertyName = nameof(Product.Name)
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Price",
                DataPropertyName = nameof(Product.Price)
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
            {
                HeaderText = "Discount",
                DataPropertyName = nameof(Product.Discount)
            });
            dataGridView1.DataSource = view;

        }

        private async void btnLoadData_Click(object sender, EventArgs e)
        {
            using (SqliteConnection conn = new SqliteConnection("Data Source=products.db;"))
            {
                conn.Open(); 

                using (SqliteCommand cmd = new SqliteCommand()) 
                { 
                    cmd.Connection = conn; 
                    cmd.CommandText = "SELECT * FROM Product"; 

                    using (SqliteDataReader reader = await cmd.ExecuteReaderAsync()) 
                    {
                        while (await reader.ReadAsync())
                        {
                            Product product = new Product()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")), 
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Price = reader.GetDouble(reader.GetOrdinal("Price")),
                                Discount = 0
                            };
                            product.Discount = GetDiscount(product.Name, product.Price);
                            view.Add(product);
                        }
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            AddForm dialog = new AddForm(product);
            dialog.ShowDialog();
            view.Add(product);
        }

        public double GetDiscount(string productName, double price)
        {
            string path = @"Discounts.dll";
            string fullPath = Path.GetFullPath(path);
            Assembly asm = Assembly.LoadFile(fullPath);
            Type type = asm.GetTypes()[0];
            

            object obj = Activator.CreateInstance(type);

            MethodInfo methodInfo = type.GetMethod("GetDiscount");
            if (methodInfo == null)
            {
                FormError dialog = new FormError();
                dialog.ShowDialog();
                return 0;
            }
            ParameterInfo[] parameterInfo = methodInfo.GetParameters();
            //foreach (var parameter in parameterInfo) { }

            double result = (double)methodInfo.Invoke(obj, new object[] {productName, price});

            return result;
        }
    }
}
