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
    public partial class Raspisanie : Form
    {
        MySqlQueries MySqlQueries = null;
        MySqlOperations MySqlOperations = null;
        string ID = null;
        string ID_Uroka = null;

        public Raspisanie(MySqlQueries mySqlQueries, MySqlOperations mySqlOperations, string iD = null)
        {
            InitializeComponent();
            MySqlQueries = mySqlQueries;
            MySqlOperations = mySqlOperations;
            ID = iD;
            MySqlOperations.Select_ComboBox(MySqlQueries.Select_Klassy_ComboBox, comboBox1);
            MySqlOperations.Select_ComboBox(MySqlQueries.Select_Predmety_ComboBox, comboBox3);
            MySqlOperations.Select_ComboBox(MySqlQueries.Select_Auditorii_ComboBox, comboBox4);
            comboBox2.SelectedItem = comboBox2.Items[0];
            comboBox5.SelectedItem = comboBox5.Items[0];
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MySqlOperations.Insert_Update_Delete(MySqlQueries.Insert_Raspisanie, null, MySqlOperations.Select_Text(MySqlQueries.Select_ID_Klassy_ComboBox, null, comboBox1.Text), comboBox2.Text);
            ID = MySqlOperations.Select_Text(MySqlQueries.Select_Last_ID);
            groupBox1.Visible = true;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Uroki, dataGridView1);
            dataGridView1.Columns[0].Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MySqlOperations.Insert_Update_Delete(MySqlQueries.Update_Raspisanie, ID, MySqlOperations.Select_Text(MySqlQueries.Select_ID_Klassy_ComboBox, null, comboBox1.Text), comboBox2.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlOperations.Insert_Update_Delete(MySqlQueries.Insert_Uroki, null, ID, MySqlOperations.Select_Text(MySqlQueries.Select_ID_Predmety_ComboBox, comboBox3.Text), MySqlOperations.Select_Text(MySqlQueries.Select_ID_Auditorii_ComboBox, comboBox4.Text), comboBox5.Text);
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Uroki, dataGridView1, ID);
            dataGridView1.Columns[0].Visible = false;
            comboBox3.SelectedItem = comboBox3.Items[0];
            comboBox4.SelectedItem = comboBox4.Items[0];
            comboBox5.SelectedItem = comboBox5.Items[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlOperations.Insert_Update_Delete(MySqlQueries.Update_Uroki, ID_Uroka, ID, MySqlOperations.Select_Text(MySqlQueries.Select_ID_Predmety_ComboBox, comboBox3.Text), MySqlOperations.Select_Text(MySqlQueries.Select_ID_Auditorii_ComboBox, comboBox4.Text), comboBox5.Text);
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Uroki, dataGridView1, ID);
            dataGridView1.Columns[0].Visible = false;
            comboBox3.SelectedItem = comboBox3.Items[0];
            comboBox4.SelectedItem = comboBox4.Items[0];
            comboBox5.SelectedItem = comboBox5.Items[0];
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ID_Uroka = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            MySqlOperations.Search_In_ComboBox(dataGridView1.SelectedRows[0].Cells[1].Value.ToString(), comboBox3);
            MySqlOperations.Search_In_ComboBox(dataGridView1.SelectedRows[0].Cells[2].Value.ToString(),comboBox4);
            MySqlOperations.Search_In_ComboBox(dataGridView1.SelectedRows[0].Cells[3].Value.ToString(), comboBox5);
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                MySqlOperations.Insert_Update_Delete(MySqlQueries.Delete_Uroki, row.Cells[0].Value.ToString());
        }

        private void Raspisanie_FormClosed(object sender, FormClosedEventArgs e)
        {
            Raspisanie_Closed(this, EventArgs.Empty);
        }
        public event EventHandler Raspisanie_Closed;
    }
}
