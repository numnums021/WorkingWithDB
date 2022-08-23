using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WorkingWithDB
{
    public partial class Move : Form
    {
        string connectionString = Level.Direct;
        SqlConnection sqlConnection;
        public Move()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        private async void Clear()
        {
            listBox1.Items.Clear();
            SqlDataReader sqlReader = null;
            SqlCommand command;

            string str1 = tb1.Text;
            string str2 = tb2.Text;
            {
                {

                    //command = new SqlCommand("SELECT SUM(Price) as sum " +
                    //    "FROM Coming " +
                    //    "WHERE Date >= '" + tb1.Text + "' AND Date < '" + tb2.Text + "'", sqlConnection);

                    command = new SqlCommand("SELECT [Date] FROM SELECT [Date] FROM Coming", sqlConnection);
                    
                    try
                    {
                        sqlReader = await command.ExecuteReaderAsync();
                        //string SumPrice = Convert.ToString(sqlReader["sum"]);

                        while (await sqlReader.ReadAsync())
                        {
                            // listBox1.Items.Add(Convert.ToString(sqlReader["Date"]) + " " + Convert.ToString(sqlReader["Login"]) + " " + Convert.ToString(sqlReader["Password"]) + " " + Convert.ToString(sqlReader["Accesslevel"]));

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        if (sqlReader != null)
                            sqlReader.Close();
                    }
                }
            }
            //else
            //{
            //    MessageBox.Show("Поля должны быть заполнены");
            //}
        }

        private async void Move_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            label4.Text += Level.Name;
            //tb1.Text = "21.12.2019";
            //tb2.Text = "24.12.2019";
            await sqlConnection.OpenAsync();// !       
            //Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
