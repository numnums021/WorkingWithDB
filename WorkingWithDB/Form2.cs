using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkingWithDB
{
   
    public partial class Form2 : Form
    {
        private int IdGroup = -1;
        private String NameGroup = "";
        public Form2(string data)
        {
            InitializeComponent();
            this.data = data;
        }
        string data;
        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text += Level.Name;
            GroupKategory.IdKat = "-1";
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void пользователиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Level.LevelDost == "3")
                OpenUsers();
            else
                MessageBox.Show("Доступ закрыт.");
        }
        private void OpenUsers()
        {
            this.Hide();
            var GastOverzicht = new Users();
            GastOverzicht.ShowDialog();
            this.Show();
        }

        private void справочникиToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void товарыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (data == "Admin")
                OpenGoods(IdGroup, NameGroup);
            else
                MessageBox.Show("Доступ закрыт.");
        }
        private void OpenGoods(int IdGroup, string NameGroup)
        {
            this.Hide();
            var GastOverzicht = new goods();
            GastOverzicht.ShowDialog();
            this.Show();
        }

        private void движениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
           OpenMove();
        }

        private void OpenMove()
        {
            this.Hide();
            var GastOverzicht = new Move();
            GastOverzicht.ShowDialog();
            this.Show();

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if ((Level.LevelDost == "3")|| (Level.LevelDost == "2"))
                OpenGroupGoods();
            else
                MessageBox.Show("Доступ закрыт.");
            //OpenGroupGoods();
        }
        private void OpenGroupGoods()
        {
            this.Hide();
            var GastOverzicht = new GroupGoods();
            GastOverzicht.ShowDialog();
            this.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    public static class GroupKategory
    {
        public static string IdKat = "-1"; // -1 - все товары
    }
}
