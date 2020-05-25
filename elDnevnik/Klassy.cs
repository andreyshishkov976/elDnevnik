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
    public partial class Klassy : Form
    {
        MySqlQueries MySqlQueries = null;
        MySqlOperations MySqlOperations = null;
        string ID = null;

        public Klassy(MySqlQueries mySqlQueries, MySqlOperations mySqlOperations, string iD = null)
        {
            InitializeComponent();
            MySqlQueries = mySqlQueries;
            MySqlOperations = mySqlOperations;
            this.ID = iD;
            comboBox1.SelectedItem = comboBox1.Items[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MySqlOperations.Select_Text(MySqlQueries.Exists_Klassy, null, numericUpDown1.Value.ToString(), comboBox1.Text) == "0")
            {
                MySqlOperations.Insert_Update_Delete(MySqlQueries.Insert_Klassy, null, numericUpDown1.Value.ToString(), comboBox1.Text, numericUpDown2.Value.ToString());
                this.Close();
            }
            else
                MessageBox.Show("Класс " + numericUpDown1.Value.ToString() + comboBox1.Text + " уже присутствует в базе.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MySqlOperations.Select_Text(MySqlQueries.Exists_Klassy, null, numericUpDown1.Value.ToString(), comboBox1.Text) == "0")
            {
                MySqlOperations.Insert_Update_Delete(MySqlQueries.Update_Klassy, ID, numericUpDown1.Value.ToString(), comboBox1.Text, numericUpDown2.Value.ToString());
                this.Close();
            }
            else
                MessageBox.Show("Класс " + numericUpDown1.Value.ToString() + comboBox1.Text + " уже присутствует в базе.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void Klassy_FormClosed(object sender, FormClosedEventArgs e)
        {
            Klassy_Closed(this, EventArgs.Empty);
        }
        public event EventHandler Klassy_Closed;
    }
}
