using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace BooksWF
{
    public partial class Form1 : Form
    {
        private SqlConnection connection = null;
        private SqlDataAdapter adapter = null;
        private DataTable table = null;
        private DataSet dataset = null;


        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(@"Data Source=user-ПК\SQLEXPRESS;Initial Catalog=TEST2301;Integrated Security=True");
            connection.Open();
            adapter = new SqlDataAdapter("SELECT * FROM [dbo].[Table]", connection);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }


        //**********************************************//
        private DataSet CreateDataSet(string newSql = "")
        {
            string connectionString = @"Data Source=user-ПК\SQLEXPRESS;Initial Catalog=TEST2301;Integrated Security=True";


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Table]" + newSql, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    adapter.TableMappings.Add("Table", "[dbo].[Table]");
                    adapter.SelectCommand = cmd;
                    DataSet dataSet = new DataSet("[dbo].[Table]");
                    adapter.Fill(dataSet);
                    return dataSet;
                }
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            string selectedState1, selectedState2;
            try
            {
                selectedState1 = comboBox1.SelectedItem.ToString();
                selectedState2 = comboBox2.SelectedItem.ToString();
            }
            catch
            {
                return;
            }
            dataset = table.DataSet;
            dataset = CreateDataSet(" ORDER BY " + selectedState1 + " " + selectedState2);
            dataGridView1.DataSource = dataset.Tables["[dbo].[Table]"];

        }
    }
}
