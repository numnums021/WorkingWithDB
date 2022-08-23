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
    public partial class LoginForm : Form
    {
        string connectionString = Level.Direct;
        SqlConnection sqlConnection;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private async void CheckDB(string login, string password)
        {
            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();// !

            SqlDataReader sqlReader = null;
            SqlCommand command;

            //command = new SqlCommand("SELECT (Login, Password) FROM [Users] ", sqlConnection);
            command = new SqlCommand("SELECT * FROM [Users] Order BY [Login]", sqlConnection);
            //sqlReader = await command.ExecuteReaderAsync();
            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    string userLogin = sqlReader["Login"].ToString();
                    string passwordDB = sqlReader["Password"].ToString();
                    userLogin = userLogin.Replace(" ", "");
                    passwordDB = passwordDB.Replace(" ", "");
                    if ((login == userLogin) && (password == passwordDB))
                    {
                        Level.LevelDost = sqlReader["Accesslevel"].ToString();
                        Level.Name = sqlReader["Login"].ToString();
                    }
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
                if (Level.LevelDost != "0")
                {
                    Level.LevelDost = Level.LevelDost.Replace(" ", "");
                    Level.Name = Level.Name.Replace(" ", "");
                    OpenOverview();
                }
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            String loginUser = LoginField.Text;
            String PassUser = PassField.Text;
            
            CheckDB(loginUser, PassUser);

        }
        private void OpenOverview()
        {
            this.Hide();
            var GastOverzicht = new Form2(this.LoginField.Text);
            GastOverzicht.ShowDialog();
            this.Show();
        }
        

        private void PassField_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    public static class Level
    {
        public static string LevelDost = "0";
        public static string Name = "0";
        public static string Direct = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|Warehouse.mdf';Integrated Security=True";
    }

}
