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
    public partial class Users : Form
    {
        string connectionString = Level.Direct;
        SqlConnection sqlConnection;
        private string selected = "-1";
        public Users()
        {
            InitializeComponent();
        }



        private async void Users_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            label7.Text += Level.Name;
            await sqlConnection.OpenAsync();// !

            Clear();
            
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            
        }

        private async void AddUsers_Click(object sender, EventArgs e)
        {
            { 
            //Accesslevel.Text = Accesslevel.Text.Replace(" ", "");
            //if ((Accesslevel.Text != "1") || (Accesslevel.Text != "2") || (Accesslevel.Text != "3"))
            //{
            //    MessageBox.Show("Неправильно введён уровень доступа!\nИмеется 3 уровня!\n1 уровень - хозяин\n2 уровень - работник\n3 уровень - администратор");
            //}
            //else {
                sqlConnection = new SqlConnection(connectionString);

                await sqlConnection.OpenAsync();// !

                if (!string.IsNullOrEmpty(Login.Text) && !string.IsNullOrWhiteSpace(Login.Text) &&
                    !string.IsNullOrEmpty(Password.Text) && !string.IsNullOrWhiteSpace(Password.Text) &&
                    !string.IsNullOrEmpty(Accesslevel.Text) && !string.IsNullOrWhiteSpace(Accesslevel.Text))

                {
                    SqlCommand command = new SqlCommand("INSERT INTO [Users] (Login,Password,Accesslevel) VALUES (@Login,@Password,@Accesslevel)", sqlConnection);

                    command.Parameters.AddWithValue("Login", Login.Text);
                    command.Parameters.AddWithValue("Password", Password.Text);
                    command.Parameters.AddWithValue("Accesslevel", Accesslevel.Text);

                    await command.ExecuteNonQueryAsync();
                    MessageBox.Show("Добавлено!");
                }
                else
                {
                    MessageBox.Show("Поля должны быть заполнены");
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private async void Clear()
        {
            listBox1.Items.Clear();
            SqlDataReader sqlReader = null;//"SELECT * FROM [Users] WHERE []="+VIdTostring +" ORDER BY[name_tovara]"
            SqlCommand command = new SqlCommand("SELECT * FROM [Users] Order BY [Login]", sqlConnection);
            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["Id"]) + " " + Convert.ToString(sqlReader["Login"]) + " " + Convert.ToString(sqlReader["Password"]) + " " + Convert.ToString(sqlReader["Accesslevel"]));
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
        private async void DellUser_Click(object sender, EventArgs e)
        {
            DialogResult resualt = MessageBox.Show("Вы действительно хотите удалить выбранного пользователя?", "Предупреждение", MessageBoxButtons.YesNo);
            if (resualt == DialogResult.Yes)
            {
                SqlCommand command = new SqlCommand("DELETE FROM [Users] WHERE [Id]=@Id", sqlConnection);
                selected = (Convert.ToString(listBox1.SelectedItem)).Split().First();
                
                command.Parameters.AddWithValue("Id", selected); // !

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
                SqlCommand command = new SqlCommand("UPDATE [Users] SET [Login]=@Login, [Password]=@Password, [Accesslevel]=@Accesslevel WHERE [Id]=@Id", sqlConnection);
                String[] words = (Convert.ToString(listBox1.SelectedItem)).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                //words[0] = line0.Text;
                words[1] = line1.Text;
                words[2] = line2.Text;
                words[3] = line3.Text;
                //selected = (Convert.ToString(listBox1.SelectedItem)).Split().First();

                command.Parameters.AddWithValue("Id", words[0]);
                command.Parameters.AddWithValue("Login", words[1]);
                command.Parameters.AddWithValue("Password", words[2]);
                command.Parameters.AddWithValue("Accesslevel", words[3]);

                await command.ExecuteNonQueryAsync();
                MessageBox.Show("Обновлено!");
            }
            Clear();
            }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(listBox1.SelectedItem) != "")
            {
                String[] words = (Convert.ToString(listBox1.SelectedItem)).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                line0.Text = words[0]; line1.Text = words[1]; line2.Text = words[2]; line3.Text = words[3];
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
