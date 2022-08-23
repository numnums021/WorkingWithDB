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

    public partial class GroupGoods : Form
    {
        private int IdName = -1;
        private string NameGroup ="";
        string connectionString = Level.Direct;
        SqlConnection sqlConnection;
        public GroupGoods()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private async void GroupGoods_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);
            label7.Text += Level.Name;
            await sqlConnection.OpenAsync();// !

            Clear();
        }

        private async void Clear()
        {
            {
                listBox1.Items.Clear();
                SqlDataReader sqlReader = null;//[GroupGoods] Order BY[Goods]
                SqlCommand command = new SqlCommand("SELECT * FROM [GroupGoods] Order BY[IdGoods]", sqlConnection);
                try
                {
                    sqlReader = await command.ExecuteReaderAsync();
                    while (await sqlReader.ReadAsync())
                    {
                        listBox1.Items.Add(Convert.ToString(sqlReader["Id"])+ " " + Convert.ToString(sqlReader["Goods"])+" " + Convert.ToString(sqlReader["IdGoods"]));
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

        private async void AddUsers_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();// !

            if (!string.IsNullOrEmpty(Goods.Text) && !string.IsNullOrWhiteSpace(Goods.Text)&&
                (!string.IsNullOrEmpty(IdGoods.Text) && !string.IsNullOrWhiteSpace(IdGoods.Text)))
            {
                SqlCommand command = new SqlCommand("INSERT INTO [GroupGoods] (Goods, IdGoods) VALUES (@Goods, @IdGoods)", sqlConnection);

                command.Parameters.AddWithValue("Goods", Goods.Text);
                command.Parameters.AddWithValue("IdGoods", IdGoods.Text);

                await command.ExecuteNonQueryAsync();
                MessageBox.Show("Добавлено!");
            }
            else
            {
                MessageBox.Show("Поля должны быть заполнены");
            }
        }

        private async void DellUser_Click(object sender, EventArgs e)
        {
            DialogResult resualt = MessageBox.Show("Вы действительно хотите удалить выбранного пользователя?", "Предупреждение", MessageBoxButtons.YesNo);
            if (resualt == DialogResult.Yes)
            {
                SqlCommand command = new SqlCommand("DELETE FROM [GroupGoods] WHERE [Id]=@Id", sqlConnection);
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
                SqlCommand command = new SqlCommand("UPDATE [GroupGoods] SET [Goods]=@Goods, [IdGoods]=@IdGoods WHERE [Id]=@Id", sqlConnection);
                String[] words = (Convert.ToString(listBox1.SelectedItem)).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                //words[0] = line0.Text;
                words[1] = line3.Text;
                words[2] = line4.Text;

                command.Parameters.AddWithValue("Id", words[0]);
                command.Parameters.AddWithValue("Goods", words[1]);
                command.Parameters.AddWithValue("IdGoods", words[2]);

                await command.ExecuteNonQueryAsync();
                MessageBox.Show("Обновлено!");
            }
            Clear();
        }

        private void line3_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(listBox1.SelectedItem)!="") 
            {
                String[] words = (Convert.ToString(listBox1.SelectedItem)).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                line0.Text = words[0];
                line3.Text = words[1];
                line4.Text = words[2];

                //IdName = Convert.ToInt32(words[4]);
            } 
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (line4.Text != "")
                //line4.Text = line4.Text.Replace(" ", "");
            GroupKategory.IdKat = line4.Text;// Информация о категории товаров

            OpenGoods();
        }

        private void OpenGoods()
        {
            {
                this.Hide();
                var GastOverzicht = new goods();
                GastOverzicht.ShowDialog();
                this.Show();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
