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
    public partial class Fakultativy : Form
    {
        MySqlQueries MySqlQueries = null;
        MySqlOperations MySqlOperations = null;
        string ID = null;

        public Fakultativy(MySqlQueries mySqlQueries, MySqlOperations mySqlOperations, string iD = null)
        {
            InitializeComponent();
            MySqlQueries = mySqlQueries;
            MySqlOperations = mySqlOperations;
            this.ID = iD;
            MySqlOperations.Select_ComboBox(MySqlQueries.Select_Predmety_ComboBox, comboBox1);
            MySqlOperations.Select_ComboBox(MySqlQueries.Select_Auditorii_ComboBox, comboBox2);
            MySqlOperations.Select_ComboBox(MySqlQueries.Select_Prepod_ComboBox, comboBox2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value < dateTimePicker3.Value)
            {
                string date = dateTimePicker1.Value.Year.ToString() + '-' + dateTimePicker1.Value.Month.ToString() + '-' + dateTimePicker1.Value.Day.ToString();
                string time1 = dateTimePicker2.Value.Hour.ToString() + ':' + dateTimePicker2.Value.Minute.ToString();
                string time2 = dateTimePicker3.Value.Hour.ToString() + ':' + dateTimePicker3.Value.Minute.ToString();
                MySqlOperations.Insert_Update_Delete(MySqlQueries.Insert_Fakultativy, null, MySqlOperations.Select_Text(MySqlQueries.Select_ID_Predmety_ComboBox, null, comboBox1.Text), MySqlOperations.Select_Text(MySqlQueries.Select_ID_Prepod_ComboBox, null, comboBox3.Text), date, MySqlOperations.Select_Text(MySqlQueries.Select_ID_Auditorii_ComboBox, null, comboBox2.Text), time1, time2);
                this.Close();
            }
            else
            {
                MessageBox.Show("Факультатив не может начаться позже времени его окончания.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value < dateTimePicker3.Value)
            {
                string date = dateTimePicker1.Value.Year.ToString() + '-' + dateTimePicker1.Value.Month.ToString() + '-' + dateTimePicker1.Value.Day.ToString();
                string time1 = dateTimePicker2.Value.Hour.ToString() + ':' + dateTimePicker2.Value.Minute.ToString();
                string time2 = dateTimePicker3.Value.Hour.ToString() + ':' + dateTimePicker3.Value.Minute.ToString();
                MySqlOperations.Insert_Update_Delete(MySqlQueries.Update_Fakultativy, ID, MySqlOperations.Select_Text(MySqlQueries.Select_ID_Predmety_ComboBox, null, comboBox1.Text), MySqlOperations.Select_Text(MySqlQueries.Select_ID_Prepod_ComboBox, null, comboBox3.Text), date, MySqlOperations.Select_Text(MySqlQueries.Select_ID_Auditorii_ComboBox, null, comboBox2.Text), time1, time2);
                this.Close();
            }
            else
            {
                MessageBox.Show("Факультатив не может начаться позже времени его окончания.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Fakultativy_FormClosed(object sender, FormClosedEventArgs e)
        {
            Fakultativy_Closed(this, EventArgs.Empty);
        }
        public event EventHandler Fakultativy_Closed;
    }
}
