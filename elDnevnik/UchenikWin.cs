using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace elDnevnik
{
    public partial class UchenikWin : Form
    {
        MySqlQueries MySqlQueries = null;
        MySqlOperations MySqlOperations = null;
        string ID_Uchenika = null;
        DateTime[] oldTimes;

        public UchenikWin(string ID)
        {
            InitializeComponent();
            MySqlQueries = new MySqlQueries();
            MySqlOperations = new MySqlOperations(MySqlQueries);
            this.ID_Uchenika = ID;
            oldTimes = new DateTime[2] { dateTimePicker1.Value, dateTimePicker1.Value };
            
        }

        private void PrepodWin_Load(object sender, EventArgs e)
        {
            MySqlOperations.OpenConnection();
            DateTime dateTime = DateTime.Now;
            while (dateTime.DayOfWeek != System.DayOfWeek.Monday)
                dateTime = dateTime.AddDays(-1);
            dateTimePicker1.Value = dateTime;
            MessageBox.Show("Добро пожаловать, " + MySqlOperations.Select_Text(MySqlQueries.Select_FIO_Ucheniki, ID_Uchenika) + '.', "Приветствие", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Load_Raspisanie(MySqlOperations.Select_Text(MySqlQueries.Select_ID_Klassa_Ucheniki, ID_Uchenika));
            Load_Uspevaemost();
        }

        private void Load_Raspisanie(string ID)
        {
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Uroki_Uchenika, dataGridView2, ID, "Понедельник");
            dataGridView2.Columns[0].Visible = false;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Uroki_Uchenika, dataGridView3, ID, "Вторник");
            dataGridView3.Columns[0].Visible = false;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Uroki_Uchenika, dataGridView4, ID, "Среда");
            dataGridView4.Columns[0].Visible = false;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Uroki_Uchenika, dataGridView5, ID, "Четверг");
            dataGridView5.Columns[0].Visible = false;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Uroki_Uchenika, dataGridView6, ID, "Пятница");
            dataGridView6.Columns[0].Visible = false;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Uroki_Uchenika, dataGridView7, ID, "Суббота");
            dataGridView7.Columns[0].Visible = false;
            dataGridView2.ClearSelection();
            dataGridView3.ClearSelection();
            dataGridView4.ClearSelection();
            dataGridView5.ClearSelection();
            dataGridView6.ClearSelection();
            dataGridView7.ClearSelection();
        }

        private void Load_Homework(string ID, DateTimePicker dateTimePicker)
        {
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Homework_Uchenika, dataGridView13, ID, dateTimePicker1.Value.ToString());
            dataGridView13.Columns[0].Visible = false;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Homework_Uchenika, dataGridView12, ID, dateTimePicker1.Value.AddDays(1).ToString());
            dataGridView12.Columns[0].Visible = false;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Homework_Uchenika, dataGridView11, ID, dateTimePicker1.Value.AddDays(2).ToString());
            dataGridView11.Columns[0].Visible = false;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Homework_Uchenika, dataGridView10, ID, dateTimePicker1.Value.AddDays(3).ToString());
            dataGridView10.Columns[0].Visible = false;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Homework_Uchenika, dataGridView9, ID, dateTimePicker1.Value.AddDays(4).ToString());
            dataGridView9.Columns[0].Visible = false;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Homework_Uchenika, dataGridView1, ID, dateTimePicker1.Value.AddDays(5).ToString());
            dataGridView1.Columns[0].Visible = false;
            dataGridView13.ClearSelection();
            dataGridView12.ClearSelection();
            dataGridView11.ClearSelection();
            dataGridView10.ClearSelection();
            dataGridView9.ClearSelection();
            dataGridView1.ClearSelection();
        }

        private void UchenikWin_FormClosed(object sender, FormClosedEventArgs e)
        {
            MySqlOperations.CloseConnection();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Load_Uspevaemost()
        {
            DataTable dt = null;
            if (dateTimePicker2.Value.Month < 10)
                dt = MySqlOperations.Select_DataTable(MySqlQueries.Select_Uspevaemost_Predmety, ID_Uchenika, dateTimePicker2.Value.Year.ToString() + '-' + '0' + dateTimePicker2.Value.Month.ToString());
            else
                dt = MySqlOperations.Select_DataTable(MySqlQueries.Select_Uspevaemost_Predmety, ID_Uchenika, dateTimePicker2.Value.Year.ToString() + '-' + dateTimePicker2.Value.Month.ToString());
            if (dt.Rows.Count > 0)
            {
                if (dateTimePicker2.Value.Month < 10)
                {
                    dataGridView8.Rows.Add(dt.Rows.Count);
                    for (int i = 0; i < dt.Rows.Count; i++)
                        dataGridView8.Rows[i].Cells[0].Value = dt.Rows[i][0];
                    for (int i = 1; i <= 31; i++)
                        if (i < 10)
                        {
                            dt = MySqlOperations.Select_DataTable(MySqlQueries.Select_Uspevaemost_Daily, ID_Uchenika, dateTimePicker2.Value.Year.ToString() + '-' + '0' + dateTimePicker2.Value.Month.ToString(), "0" + i.ToString());
                            for (int j = 0; j < dt.Rows.Count; j++)
                                dataGridView8.Rows[j].Cells[i].Value = dt.Rows[j][0];
                        }
                        else
                        {
                            dt = MySqlOperations.Select_DataTable(MySqlQueries.Select_Uspevaemost_Daily, ID_Uchenika, dateTimePicker2.Value.Year.ToString() + '-' + '0' + dateTimePicker2.Value.Month.ToString(), i.ToString());
                            for (int j = 0; j < dt.Rows.Count; j++)
                                dataGridView8.Rows[j].Cells[i].Value = dt.Rows[j][0];
                        }
                    dt = MySqlOperations.Select_DataTable(MySqlQueries.Select_Uspevaemost_SrBal, ID_Uchenika, dateTimePicker2.Value.Year.ToString() + '-' + '0' + dateTimePicker2.Value.Month.ToString());
                    for (int j = 0; j < dt.Rows.Count; j++)
                        dataGridView8.Rows[j].Cells[32].Value = dt.Rows[j][0];
                    label6.Text = MySqlOperations.Select_Text(MySqlQueries.Select_SrBal_Uchenika, ID_Uchenika, dateTimePicker2.Value.Year.ToString() + '-' + '0' + dateTimePicker2.Value.Month.ToString());
                }
                else
                {
                    dataGridView8.Rows.Add(dt.Rows.Count);
                    for (int i = 0; i < dt.Rows.Count; i++)
                        dataGridView8.Rows[i].Cells[0].Value = dt.Rows[i][0];
                    for (int i = 1; i <= 31; i++)
                        if (i < 10)
                        {
                            dt = MySqlOperations.Select_DataTable(MySqlQueries.Select_Uspevaemost_Daily, ID_Uchenika, dateTimePicker2.Value.Year.ToString() + '-' + dateTimePicker2.Value.Month.ToString(), "0" + i.ToString());
                            for (int j = 0; j < dt.Rows.Count; j++)
                                dataGridView8.Rows[j].Cells[i].Value = dt.Rows[j][0];
                        }
                        else
                        {
                            dt = MySqlOperations.Select_DataTable(MySqlQueries.Select_Uspevaemost_Daily, ID_Uchenika, dateTimePicker2.Value.Year.ToString() + '-' + dateTimePicker2.Value.Month.ToString(), i.ToString());
                            for (int j = 0; j < dt.Rows.Count; j++)
                                dataGridView8.Rows[j].Cells[i].Value = dt.Rows[j][0];
                        }
                    dt = MySqlOperations.Select_DataTable(MySqlQueries.Select_Uspevaemost_SrBal, ID_Uchenika, dateTimePicker2.Value.Year.ToString() + '-' + dateTimePicker2.Value.Month.ToString());
                    for (int j = 0; j < dt.Rows.Count; j++)
                        dataGridView8.Rows[j].Cells[32].Value = dt.Rows[j][0];
                    label6.Text = MySqlOperations.Select_Text(MySqlQueries.Select_SrBal_Uchenika, ID_Uchenika, dateTimePicker2.Value.Year.ToString() + '-' + dateTimePicker2.Value.Month.ToString());
                }
                for (int i = 0; i < dataGridView8.Rows.Count; i++)
                {
                    for (int j = 1; j < dataGridView8.Columns.Count - 1; j++)
                    {
                        if (dataGridView8.Rows[i].Cells[j].Value != null)
                            if (int.Parse(dataGridView8.Rows[i].Cells[j].Value.ToString()) < 4)
                            {
                                dataGridView8.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                                break;
                            }
                    }
                }
                dataGridView8.ClearSelection();
            }
            else
            {
                dataGridView8.Rows.Clear();
                label6.Text = "";
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            Load_Uspevaemost();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            for (int i = 0; i < dataGridView8.Rows.Count; i++)
                dataTable.Rows.Add();
            for (int i = 0; i < dataGridView8.Columns.Count; i++)
                dataTable.Columns.Add();
            for (int i = 0; i < dataGridView8.Rows.Count; i++)
                for (int j = 0; j < dataGridView8.Columns.Count; j++)
                    dataTable.Rows[i][j] = dataGridView8.Rows[i].Cells[j].Value;
            MySqlOperations.Print_Uspevaemost(dateTimePicker2, ID_Uchenika, saveFileDialog1, dataTable);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            oldTimes[0] = oldTimes[1];
            oldTimes[1] = dateTimePicker1.Value;
            if (dateTimePicker1.Value.Day == oldTimes[0].AddDays(1).Day)
                dateTimePicker1.Value = dateTimePicker1.Value.AddDays(6);
            if (dateTimePicker1.Value.Day == oldTimes[0].AddDays(-1).Day)
                dateTimePicker1.Value = dateTimePicker1.Value.AddDays(-6);
            Load_Homework(MySqlOperations.Select_Text(MySqlQueries.Select_ID_Klassa_Ucheniki, ID_Uchenika), dateTimePicker1);
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа " + '"' + "elDnevnik" + '"' + " разработана по индивидуальному заданию на дипломной проект. Программа позволяет осуществлять функции добавления, удаления, редактирования записей таблиц. Предусмотрен вывод на печать следующих документов: Ведомость отметок за месяц по предмету, Ведомость отметок учащегося, Ведомость четвертных отметок. Предусмотрены проверки нежелательных действий пользователя. Программу разработала учащаяся группы ПО-41 Синенок Ангелина Олеговна.", "О программе...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
