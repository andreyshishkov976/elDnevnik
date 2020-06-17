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
    public partial class Zanyatiya : Form
    {
        MySqlQueries MySqlQueries = null;
        MySqlOperations MySqlOperations = null;
        string ID_Zanyatiya = null;
        string ID_Homework = null;

        public Zanyatiya(MySqlQueries mySqlQueries, MySqlOperations mySqlOperations, string iD, string iD2)
        {
            InitializeComponent();
            MySqlQueries = mySqlQueries;
            MySqlOperations = mySqlOperations;
            ID_Zanyatiya = iD;
            ID_Homework = iD2;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Otmetki_Zanyatiya, dataGridView1, ID_Zanyatiya);
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].ReadOnly = true;
            richTextBox1.Text = MySqlOperations.Select_Text(MySqlQueries.Select_Homework, ID_Homework);
        }
        private void Zanyatiya_FormClosed(object sender, FormClosedEventArgs e)
        {
            Zanyatiya_Closed(this, EventArgs.Empty);
        }
        public event EventHandler Zanyatiya_Closed;

        private void button1_Click(object sender, EventArgs e)
        {
            int a = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
                if (int.TryParse(row.Cells[2].Value.ToString(), out a) == true)
                    MySqlOperations.Insert_Update_Delete(MySqlQueries.Update_Otmetki, row.Cells[0].Value.ToString(), row.Cells[2].Value.ToString());
                else
                {
                    MessageBox.Show("В качестве отметки для " + row.Cells[1].Value.ToString() + " было выставлено не число.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }
            MySqlOperations.Insert_Update_Delete(MySqlQueries.Update_Homework, ID_Homework, richTextBox1.Text);
            this.Close();
        }
    }
}
