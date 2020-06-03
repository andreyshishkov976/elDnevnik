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
        string ID = null;

        public Zanyatiya(MySqlQueries mySqlQueries, MySqlOperations mySqlOperations, string iD)
        {
            InitializeComponent();
            MySqlQueries = mySqlQueries;
            MySqlOperations = mySqlOperations;
            ID = iD;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Otmetki_Zanyatiya, dataGridView1, ID);
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].ReadOnly = true;
        }
        private void Zanyatiya_FormClosed(object sender, FormClosedEventArgs e)
        {
            Zanyatiya_Closed(this, EventArgs.Empty);
        }
        public event EventHandler Zanyatiya_Closed;
    }
}
