﻿using System;
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
    public partial class Ucheniki : Form
    {
        MySqlQueries MySqlQueries = null;
        MySqlOperations MySqlOperations = null;
        string ID = null;

        public Ucheniki(MySqlQueries mySqlQueries, MySqlOperations mySqlOperations, string iD = null)
        {
            InitializeComponent();
            MySqlQueries = mySqlQueries;
            MySqlOperations = mySqlOperations;
            this.ID = iD;
            MySqlOperations.Select_ComboBox(MySqlQueries.Select_Klassy_ComboBox, comboBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "")
                if (MySqlOperations.Select_Text(MySqlQueries.Exists_Ucheniki, null, textBox4.Text, textBox5.Text) != "1")
                {
                    MySqlOperations.Insert_Update_Delete(MySqlQueries.Insert_Ucheniki, null, textBox1.Text, textBox2.Text, textBox3.Text, MySqlOperations.Select_Text(MySqlQueries.Select_ID_Klassy_ComboBox, null, comboBox1.Text), textBox4.Text, textBox5.Text);
                    this.Close();
                }
                else
                    MessageBox.Show("Введенный вами Логин и(или) Пароль уже заняты.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Поля не заполнены.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "")
                if (MySqlOperations.Select_Text(MySqlQueries.Exists_Ucheniki, null, textBox4.Text, textBox5.Text) != "1")
                {
                    MySqlOperations.Insert_Update_Delete(MySqlQueries.Update_Ucheniki, ID, textBox1.Text, textBox2.Text, textBox3.Text, MySqlOperations.Select_Text(MySqlQueries.Select_ID_Klassy_ComboBox, null, comboBox1.Text), textBox4.Text, textBox5.Text);
                    this.Close();
                }
                else
                    MessageBox.Show("Введенный вами Логин и(или) Пароль уже заняты.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Поля не заполнены.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void Ucheniki_FormClosed(object sender, FormClosedEventArgs e)
        {
            Ucheniki_Closed(this, EventArgs.Empty);
        }
        public event EventHandler Ucheniki_Closed;
    }
}
