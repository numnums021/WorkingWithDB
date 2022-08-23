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
    public partial class Coming : Form
    {
        string connectionString = Level.Direct;
        SqlConnection sqlConnection;
        public Coming()
        {
            InitializeComponent();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private async void Coming_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            label15.Text += Level.Name;
            await sqlConnection.OpenAsync();// !       
            Clear();
        }
        private async void Clear()
        {
            {
                listBox1.Items.Clear();
                SqlDataReader sqlReader = null;
                SqlCommand command;
                if (GroupKategory.IdKat == "-1")
                {
                    command = new SqlCommand("SELECT * FROM [Coming] Order BY [IdGroup]", sqlConnection);
                }
                else
                {
                    command = new SqlCommand("SELECT * FROM [Coming] WHERE [IdGroup]=" + GroupKategory.IdKat + " Order BY [Name] ", sqlConnection);
                }
                try
                {
                    sqlReader = await command.ExecuteReaderAsync();
                    while (await sqlReader.ReadAsync())
                    {
                        listBox1.Items.Add(Convert.ToString(sqlReader["Id"]) + " " + Convert.ToString(sqlReader["Name"])
                            + " " + Convert.ToString(sqlReader["IdGroup"]) + " " + Convert.ToString(sqlReader["Amount"]) + " " + Convert.ToString(sqlReader["Price"]) 
                            + " " + Convert.ToString(sqlReader["Date"]) + " " + Convert.ToString(sqlReader["IdGoods"]));
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

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();// !


            if (!string.IsNullOrEmpty(b2.Text) && !string.IsNullOrWhiteSpace(b2.Text) &&
                !string.IsNullOrEmpty(b3.Text) && !string.IsNullOrWhiteSpace(b3.Text) &&
                !string.IsNullOrEmpty(b4.Text) && !string.IsNullOrWhiteSpace(b4.Text) &&
                !string.IsNullOrEmpty(b5.Text) && !string.IsNullOrWhiteSpace(b5.Text) &&
                !string.IsNullOrEmpty(b6.Text) && !string.IsNullOrWhiteSpace(b6.Text)&&
                !string.IsNullOrEmpty(b7.Text) && !string.IsNullOrWhiteSpace(b7.Text))

            {
                SqlCommand command = new SqlCommand("INSERT INTO [Coming] (Name, IdGroup, Amount, Price, Date, IdGoods) VALUES (@Name, @IdGroup, @Amount, @Price, @Date, @IdGoods)", sqlConnection);

                command.Parameters.AddWithValue("Name", b2.Text);
                command.Parameters.AddWithValue("IdGroup", b3.Text);
                command.Parameters.AddWithValue("Amount", b4.Text);
                command.Parameters.AddWithValue("Price", b5.Text);
                command.Parameters.AddWithValue("Date", b6.Text);
                command.Parameters.AddWithValue("IdGoods", b7.Text);

                await command.ExecuteNonQueryAsync();
                MessageBox.Show("Добавлено!");
            }
            else
            {
                MessageBox.Show("Поля должны быть заполнены");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(listBox1.SelectedItem) != "")
            {
                String[] words = (Convert.ToString(listBox1.SelectedItem)).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                line0.Text = words[0];
                line1.Text = words[1];
                line2.Text = words[2];
                line3.Text = words[3];
                line4.Text = words[4];
                line5.Text = words[5];
                line6.Text = words[6];
            }
        }

        private async void DellUser_Click(object sender, EventArgs e)
        {
            DialogResult resualt = MessageBox.Show("Вы действительно хотите удалить выбранного пользователя?", "Предупреждение", MessageBoxButtons.YesNo);
            if (resualt == DialogResult.Yes)
            {
                SqlCommand command = new SqlCommand("DELETE FROM [Coming] WHERE [Id]=@Id", sqlConnection);
                String[] words = (Convert.ToString(listBox1.SelectedItem)).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                //words[0] = line0.Text;
                //selected = (Convert.ToString(listBox1.SelectedItem)).Split().First();


                command.Parameters.AddWithValue("Id", words[0]); // !

                await command.ExecuteNonQueryAsync();
                MessageBox.Show("Удалено!");
            }
            //if (resualt == DialogResult.Yes)
            Clear();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            DialogResult resualt = MessageBox.Show("Вы действительно хотите изменить данные пользователя?", "Предупреждение", MessageBoxButtons.YesNo);
            if (resualt == DialogResult.Yes)
            {
                SqlCommand command = new SqlCommand("UPDATE [Coming] SET [Name]=@Name, [IdGroup]=@IdGroup, [Amount]=@Amount, [Price]=@Price, [Date]=@Date, [IdGoods]=@IdGoods WHERE [Id]=@Id", sqlConnection);
                String[] words = (Convert.ToString(listBox1.SelectedItem)).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                //words[0] = line0.Text;
                words[1] = line1.Text;
                words[2] = line2.Text;
                words[3] = line3.Text;
                words[4] = line4.Text;
                words[5] = line5.Text;
                words[6] = line6.Text;

                //selected = (Convert.ToString(listBox1.SelectedItem)).Split().First();

                command.Parameters.AddWithValue("Id", words[0]);
                command.Parameters.AddWithValue("Name", words[1]);
                command.Parameters.AddWithValue("IdGroup", words[2]);
                command.Parameters.AddWithValue("Amount", words[3]);
                command.Parameters.AddWithValue("Price", words[4]);
                command.Parameters.AddWithValue("Date", words[5]);
                command.Parameters.AddWithValue("IdGoods", words[6]);

                await command.ExecuteNonQueryAsync();
                MessageBox.Show("Обновлено!");
            }
            Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenExpense();
        }

        private void OpenExpense()
        {
            this.Hide();
            var GastOverzicht = new expense();
            GastOverzicht.ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
