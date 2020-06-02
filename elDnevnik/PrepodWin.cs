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
    public partial class PrepodWin : Form
    {
        MySqlQueries MySqlQueries = null;
        MySqlOperations MySqlOperations = null;
        string ID_Prepoda = null;

        public PrepodWin(string ID)
        {
            InitializeComponent();
            MySqlQueries = new MySqlQueries();
            MySqlOperations = new MySqlOperations(MySqlQueries);
            this.ID_Prepoda = ID;
        }

        private void PrepodWin_Load(object sender, EventArgs e)
        {
            MySqlOperations.OpenConnection();
            MySqlOperations.Select_ComboBox(MySqlQueries.Select_Klassy_ComboBox, comboBox1);
            MessageBox.Show("Добро пожаловать "+MySqlOperations.Select_Text(MySqlQueries.Select_FIO_Prepod, ID_Prepoda)+'.', "Приветствие", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Load_Raspisanie(ID_Prepoda);
        }

        private void Load_Raspisanie(string ID)
        {
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Uroki_Prepoda, dataGridView2, ID, "Понедельник");
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Uroki_Prepoda, dataGridView3, ID, "Вторник");
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Uroki_Prepoda, dataGridView4, ID, "Среда");
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Uroki_Prepoda, dataGridView5, ID, "Четверг");
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Uroki_Prepoda, dataGridView6, ID, "Пятница");
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Uroki_Prepoda, dataGridView7, ID, "Суббота");
        }

        private void PrepodWin_FormClosed(object sender, FormClosedEventArgs e)
        {
            MySqlOperations.CloseConnection();
        }
    }
}
