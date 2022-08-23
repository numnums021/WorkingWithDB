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
    public partial class goods : Form
    {
        string connectionString = Level.Direct;
        SqlConnection sqlConnection;
        public goods()
        {
            InitializeComponent();
            //this.tmp = tmp;
        }
        private async void goods_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            label8.Text += Level.Name;
            await sqlConnection.OpenAsync();// !       
            Clear();
        }

        private async void Clear()
        {
            {
                listBox1.Items.Clear(); //SqlCommand("UPDATE [Users] SET [Login]=@Login, [Password]=@Password, [Accesslevel]=@Accesslevel WHERE [Id]=@Id", sqlConnection);
                SqlDataReader sqlReader = null; // ("SELECT * FROM [Users] Order BY [Login]", sqlConnection);
                SqlCommand command;
                if (GroupKategory.IdKat == "-1")
                {
                    command = new SqlCommand("SELECT * FROM [Table] Order BY [Name]", sqlConnection);
                }
                else
                {
                    command = new SqlCommand("SELECT * FROM [Table] WHERE [Sort]=" + GroupKategory.IdKat + " Order BY [Name] ", sqlConnection);
                }
                try
                {
                    sqlReader = await command.ExecuteReaderAsync();
                    while (await sqlReader.ReadAsync())
                    {
                        listBox1.Items.Add(Convert.ToString(sqlReader["Id"]) + " " + Convert.ToString(sqlReader["Name"]) + " " + Convert.ToString(sqlReader["Amount"]) + " " + Convert.ToString(sqlReader["Price"]) + " " + Convert.ToString(sqlReader["Sort"]));
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
        }

        private void tabPage1_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Consumpation();
        }
        private void Consumpation()
        {
            this.Hide();
            var GastOverzicht = new goods();
            GastOverzicht.ShowDialog();
            this.Show();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult resualt = MessageBox.Show("Вы действительно хотите изменить данные пользователя?", "Предупреждение", MessageBoxButtons.YesNo);
            if (resualt == DialogResult.Yes)
            {
                SqlCommand command = new SqlCommand("UPDATE [Table] SET [Name]=@Name, [Amount]=@Amount, [Price]=@Price, [Sort]=@Sort WHERE [Id]=@Id", sqlConnection);
                String[] words = (Convert.ToString(listBox1.SelectedItem)).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                //words[0] = line0.Text;
                words[1] = line1.Text;
                words[2] = line2.Text;
                words[3] = line3.Text;
                words[4] = line4.Text;
                //selected = (Convert.ToString(listBox1.SelectedItem)).Split().First();

                command.Parameters.AddWithValue("Id", words[0]);
                command.Parameters.AddWithValue("Name", words[1]);
                command.Parameters.AddWithValue("Amount", words[2]);
                command.Parameters.AddWithValue("Price", words[3]);
                command.Parameters.AddWithValue("Sort", words[4]);

                await command.ExecuteNonQueryAsync();
                MessageBox.Show("Обновлено!");
            }
            Clear();
        }

        private async void DellUser_Click(object sender, EventArgs e)
        {
            DialogResult resualt = MessageBox.Show("Вы действительно хотите удалить выбранного пользователя?", "Предупреждение", MessageBoxButtons.YesNo);
            if (resualt == DialogResult.Yes)
            {
                SqlCommand command = new SqlCommand("DELETE FROM [Table] WHERE [Id]=@Id", sqlConnection);
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
            }
        }

        private void AddUsers_Click(object sender, EventArgs e)
        {
            
        }

        private async void button2_Click_1(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();// !

            if (!string.IsNullOrEmpty(b0.Text) && !string.IsNullOrWhiteSpace(b0.Text) &&
                !string.IsNullOrEmpty(b1.Text) && !string.IsNullOrWhiteSpace(b1.Text) &&
                !string.IsNullOrEmpty(b2.Text) && !string.IsNullOrWhiteSpace(b2.Text) &&
                !string.IsNullOrEmpty(b3.Text) && !string.IsNullOrWhiteSpace(b3.Text))

            {
                SqlCommand command = new SqlCommand("INSERT INTO [Table] (Name, Amount, Price, Sort) VALUES (@Name, @Amount, @Price, @Sort)", sqlConnection);

                command.Parameters.AddWithValue("Name", b0.Text);
                command.Parameters.AddWithValue("Amount", b1.Text);
                command.Parameters.AddWithValue("Price", b2.Text);
                command.Parameters.AddWithValue("Sort", b3.Text);

                await command.ExecuteNonQueryAsync();
                MessageBox.Show("Добавлено!");
            }
            else
            {
                MessageBox.Show("Поля должны быть заполнены");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenComing();
        }

        private void OpenComing()
        {
            this.Hide();
            var GastOverzicht = new Coming();
            GastOverzicht.ShowDialog();
            this.Show();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Clear();
        }

    }
}
