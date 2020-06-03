using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
        string DayOfWeek = null;
        bool Exists = false;

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
            MessageBox.Show("Добро пожаловать " + MySqlOperations.Select_Text(MySqlQueries.Select_FIO_Prepod, ID_Prepoda) + '.', "Приветствие", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Load_Raspisanie(ID_Prepoda);
            DateTime dateValue = new DateTime(2008, 6, 11);
            DayOfWeek = DateTime.Now.ToString("dddd", new CultureInfo("ru-RU"));
            DayOfWeek = DayOfWeek.Substring(0, 1).ToUpper() + DayOfWeek.Remove(0, 1).ToLower();
            ConvertDayToIndex();
            LockDown_DataGrids();
            if (MySqlOperations.Select_Text(MySqlQueries.Exists_Zanyatiya, ID_Prepoda, DayOfWeek) == "1")
                Exists = true;
            else
                Exists = false;
        }

        private void ConvertDayToIndex()
        {
            if (DayOfWeek == "Понедельник")
                DayOfWeek = "0";
            if (DayOfWeek == "Вторник")
                DayOfWeek = "1";
            if (DayOfWeek == "Среда")
                DayOfWeek = "2";
            if (DayOfWeek == "Четверг")
                DayOfWeek = "3";
            if (DayOfWeek == "Пятница")
                DayOfWeek = "4";
            if (DayOfWeek == "Суббота")
                DayOfWeek = "5";
        }

        private void LockDown_DataGrids()
        {
            if (dataGridView2.Tag.ToString() != DayOfWeek)
                dataGridView2.Enabled = false;
            if (dataGridView3.Tag.ToString() != DayOfWeek)
                dataGridView3.Enabled = false;
            if (dataGridView4.Tag.ToString() != DayOfWeek)
                dataGridView4.Enabled = false;
            if (dataGridView5.Tag.ToString() != DayOfWeek)
                dataGridView5.Enabled = false;
            if (dataGridView6.Tag.ToString() != DayOfWeek)
                dataGridView6.Enabled = false;
            if (dataGridView7.Tag.ToString() != DayOfWeek)
                dataGridView7.Enabled = false;
        }

        private void Load_Raspisanie(string ID)
        {
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Uroki_Prepoda, dataGridView2, ID, "0");
            dataGridView2.Columns[0].Visible = false;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Uroki_Prepoda, dataGridView3, ID, "1");
            dataGridView3.Columns[0].Visible = false;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Uroki_Prepoda, dataGridView4, ID, "2");
            dataGridView4.Columns[0].Visible = false;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Uroki_Prepoda, dataGridView5, ID, "3");
            dataGridView5.Columns[0].Visible = false;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Uroki_Prepoda, dataGridView6, ID, "4");
            dataGridView6.Columns[0].Visible = false;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Uroki_Prepoda, dataGridView7, ID, "5");
            dataGridView7.Columns[0].Visible = false;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Zanyatiya, dataGridView9, ID);
            dataGridView9.Columns[0].Visible = false;
            dataGridView2.ClearSelection();
            dataGridView3.ClearSelection();
            dataGridView4.ClearSelection();
            dataGridView5.ClearSelection();
            dataGridView6.ClearSelection();
            dataGridView7.ClearSelection();
            dataGridView9.ClearSelection();
        }

        private void PrepodWin_FormClosed(object sender, FormClosedEventArgs e)
        {
            MySqlOperations.CloseConnection();
        }

        private void Load_Zanyatiya(object sender, EventArgs e)
        {
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Zanyatiya, dataGridView9, ID_Prepoda);
            dataGridView9.Columns[0].Visible = false;
        }

        private void Insert_Zanyatiya(DataGridView dataGridView)
        {
            if (Exists == true)
                if (MessageBox.Show("Хотите заполнить занятие?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string date = DateTime.Now.Year.ToString() + '-' + DateTime.Now.Month.ToString() + '-' + DateTime.Now.Day.ToString();
                    MySqlOperations.Insert_Update_Delete(MySqlQueries.Insert_Zanyatiya, null, dataGridView.SelectedRows[0].Cells[0].Value.ToString(), date, ID_Prepoda);
                    string ID = MySqlOperations.Select_Text(MySqlQueries.Select_Last_ID);
                    DataTable dataTable = MySqlOperations.Select_DataTable(MySqlQueries.Select_ID_Ucheniki_Klassa, null, dataGridView.SelectedRows[0].Cells[1].Value.ToString());
                    foreach (DataRow row in dataTable.Rows)
                        MySqlOperations.Insert_Update_Delete(MySqlQueries.Insert_Otmetki,null, row[0].ToString(),ID, null);
                    Zanyatiya zanyatiya = new Zanyatiya(MySqlQueries, MySqlOperations, ID);
                    zanyatiya.Text += ID;
                    zanyatiya.Zanyatiya_Closed += Load_Zanyatiya;
                    zanyatiya.Show();
                }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e) => Insert_Zanyatiya(dataGridView2);

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e) => Insert_Zanyatiya(dataGridView3);

        private void dataGridView4_CellDoubleClick(object sender, DataGridViewCellEventArgs e) => Insert_Zanyatiya(dataGridView4);

        private void dataGridView5_CellDoubleClick(object sender, DataGridViewCellEventArgs e) => Insert_Zanyatiya(dataGridView5);

        private void dataGridView6_CellDoubleClick(object sender, DataGridViewCellEventArgs e) => Insert_Zanyatiya(dataGridView6);

        private void dataGridView7_CellDoubleClick(object sender, DataGridViewCellEventArgs e) => Insert_Zanyatiya(dataGridView7);
    }
}
