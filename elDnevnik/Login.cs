using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace elDnevnik
{
    public partial class Login : Form
    {
        MySqlQueries MySqlQueries = null;
        MySqlOperations MySqlOperations = null;
        public string ID = null;

        public Login()
        {
            InitializeComponent();
            MySqlQueries = new MySqlQueries();
            MySqlOperations = new MySqlOperations(MySqlQueries);
        }

        private void Login_Load(object sender, EventArgs e)
        {
            MySqlOperations.OpenConnection();
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            MySqlOperations.CloseConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
            this.AcceptButton = button3;
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
            this.AcceptButton = button4;
            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Ucheniki ucheniki = new Ucheniki(MySqlQueries, MySqlOperations);
            ucheniki.button1.Visible = true;
            ucheniki.button3.Visible = false;
            ucheniki.AcceptButton = ucheniki.button1;
            ucheniki.Ucheniki_Closed += Ucheniki_Ucheniki_Closed;
            ucheniki.Show();
        }

        private void Ucheniki_Ucheniki_Closed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Prepod prepod = new Prepod(MySqlQueries,MySqlOperations);
            prepod.button1.Visible = true;
            prepod.button3.Visible = false;
            prepod.AcceptButton = prepod.button1;
            prepod.Prepod_Closed += Prepod_Prepod_Closed;
            prepod.Show();
        }

        private void Prepod_Prepod_Closed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "admin" && textBox4.Text != "1234")
                if (MySqlOperations.Select_Text(MySqlQueries.Exists_Prepod, null, textBox3.Text, textBox4.Text) == "1")
                {
                    ID = MySqlOperations.Select_Text(MySqlQueries.Select_ID_Prepod, null, textBox3.Text, textBox4.Text);
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
                else
                    MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MySqlOperations.Select_Text(MySqlQueries.Exists_Ucheniki, null, textBox1.Text, textBox2.Text) == "1")
            {
                ID = MySqlOperations.Select_Text(MySqlQueries.Select_ID_Ucheniki, null, textBox1.Text, textBox2.Text);
                this.DialogResult = DialogResult.No;
                this.Close();
            }
            else
                MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
