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
    public partial class Predmety : Form
    {
        MySqlQueries MySqlQueries = null;
        MySqlOperations MySqlOperations = null;
        string ID = null;

        public Predmety(MySqlQueries mySqlQueries, MySqlOperations mySqlOperations, string iD = null)
        {
            InitializeComponent();
            MySqlQueries = mySqlQueries;
            MySqlOperations = mySqlOperations;
            this.ID = iD;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                MySqlOperations.Insert_Update_Delete(MySqlQueries.Insert_Predmety, null, textBox1.Text);
                this.Close();
            }
            else
            {
                MessageBox.Show("Поля не заполнены", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                MySqlOperations.Insert_Update_Delete(MySqlQueries.Update_Predmety, ID, textBox1.Text);
                this.Close();
            }
            else
            {
                MessageBox.Show("Поля не заполнены", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Auditorii_FormClosed(object sender, FormClosedEventArgs e)
        {
            Auditorii_Closed(this, EventArgs.Empty);
        }
        public event EventHandler Auditorii_Closed;
    }
}
