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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlOperations.Insert_Update_Delete(MySqlQueries.Insert_Predmety, null, textBox1.Text);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MySqlOperations.Insert_Update_Delete(MySqlQueries.Update_Predmety, ID, textBox1.Text);
            this.Close();
        }

        private void Fakultativy_FormClosed(object sender, FormClosedEventArgs e)
        {
            Fakultativy_Closed(this, EventArgs.Empty);
        }
        public event EventHandler Fakultativy_Closed;
    }
}
