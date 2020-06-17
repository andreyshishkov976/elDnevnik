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
            MessageBox.Show("Добро пожаловать, " + MySqlOperations.Select_Text(MySqlQueries.Select_FIO_Prepod, ID_Prepoda) + '.', "Приветствие", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MySqlOperations.Select_ComboBox(MySqlQueries.Select_Klassy_ComboBox, comboBox4);
            Load_Raspisanie(ID_Prepoda);
            Load_Fakultativy(this, EventArgs.Empty);
            DayOfWeek = DateTime.Now.ToString("dddd", new CultureInfo("ru-RU")).Substring(0, 1).ToUpper() + DateTime.Now.ToString("dddd", new CultureInfo("ru-RU")).Remove(0, 1).ToLower();
            LockDown_DataGrids();
            if (MySqlOperations.Select_Text(MySqlQueries.Exists_Zanyatiya_Today, ID_Prepoda, DayOfWeek) == "1")
                Exists = true;
            else
                Exists = false;
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
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Uroki_Prepoda, dataGridView2, ID, "Понедельник");
            dataGridView2.Columns[0].Visible = false;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Uroki_Prepoda, dataGridView3, ID, "Вторник");
            dataGridView3.Columns[0].Visible = false;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Uroki_Prepoda, dataGridView4, ID, "Среда");
            dataGridView4.Columns[0].Visible = false;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Uroki_Prepoda, dataGridView5, ID, "Четверг");
            dataGridView5.Columns[0].Visible = false;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Uroki_Prepoda, dataGridView6, ID, "Пятница");
            dataGridView6.Columns[0].Visible = false;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Uroki_Prepoda, dataGridView7, ID, "Суббота");
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
            DateTime Now = DateTime.Now;
            string date = Now.Year.ToString() + '-' + Now.Month.ToString() + '-' + Now.Day.ToString();
            if (Exists == true)
                if (MySqlOperations.Select_Text(MySqlQueries.Exists_Zanyatiya, null, dataGridView.SelectedRows[0].Cells[0].Value.ToString(), date, ID_Prepoda) == "0")
                {
                    if (MessageBox.Show("Хотите заполнить занятие?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        MySqlOperations.Insert_Update_Delete(MySqlQueries.Insert_Zanyatiya, null, dataGridView.SelectedRows[0].Cells[0].Value.ToString(), date, ID_Prepoda);
                        string ID_Zanyatiya = MySqlOperations.Select_Text(MySqlQueries.Select_Last_ID);
                        DataTable dataTable = MySqlOperations.Select_DataTable(MySqlQueries.Select_ID_Ucheniki_Klassa, null, dataGridView.SelectedRows[0].Cells[1].Value.ToString());
                        foreach (DataRow row in dataTable.Rows)
                            MySqlOperations.Insert_Update_Delete(MySqlQueries.Insert_Otmetki, null, row[0].ToString(), ID_Zanyatiya, "");
                        do
                            Now = Now.AddDays(1);
                        while (MySqlOperations.Select_Text(MySqlQueries.Exists_Zanyatiya_Today, ID_Prepoda, Now.ToString("dddd", new CultureInfo("ru-RU")).Substring(0, 1).ToUpper() + Now.ToString("dddd", new CultureInfo("ru-RU")).Remove(0, 1).ToLower()) != "1");
                        MySqlOperations.Insert_Update_Delete(MySqlQueries.Insert_Homework, null, ID_Zanyatiya, Now.Year.ToString() + '-' + Now.Month.ToString() + '-' + Now.Day.ToString(),
                            MySqlOperations.Select_Text(MySqlQueries.Select_Urok_Homework, ID_Prepoda, Now.ToString("dddd", new CultureInfo("ru-RU")).Substring(0, 1).ToUpper() + Now.ToString("dddd", new CultureInfo("ru-RU")).Remove(0, 1).ToLower(),
                            MySqlOperations.Select_Text(MySqlQueries.Select_ID_Klassy_ComboBox, null, dataGridView.SelectedRows[0].Cells[1].Value.ToString())));
                        string ID_Homework = MySqlOperations.Select_Text(MySqlQueries.Select_Last_ID);
                        Zanyatiya zanyatiya = new Zanyatiya(MySqlQueries, MySqlOperations, ID_Zanyatiya, ID_Homework);
                        zanyatiya.Text += ID_Zanyatiya;
                        zanyatiya.Zanyatiya_Closed += Load_Zanyatiya;
                        zanyatiya.Show();
                    }
                }
                else if(MessageBox.Show("Хотите отредактровать занятие?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Open_Zanyatiya(MySqlOperations.Select_Text(MySqlQueries.Select_ID_Zanyatiya, null, dataGridView.SelectedRows[0].Cells[0].Value.ToString(), date, ID_Prepoda), 
                        MySqlOperations.Select_Text(MySqlQueries.Select_ID_Homework, MySqlOperations.Select_Text(MySqlQueries.Select_ID_Zanyatiya, null, dataGridView.SelectedRows[0].Cells[0].Value.ToString(), date, ID_Prepoda)));
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e) => Insert_Zanyatiya(dataGridView2);

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e) => Insert_Zanyatiya(dataGridView3);

        private void dataGridView4_CellDoubleClick(object sender, DataGridViewCellEventArgs e) => Insert_Zanyatiya(dataGridView4);

        private void dataGridView5_CellDoubleClick(object sender, DataGridViewCellEventArgs e) => Insert_Zanyatiya(dataGridView5);

        private void dataGridView6_CellDoubleClick(object sender, DataGridViewCellEventArgs e) => Insert_Zanyatiya(dataGridView6);

        private void dataGridView7_CellDoubleClick(object sender, DataGridViewCellEventArgs e) => Insert_Zanyatiya(dataGridView7);

        private void dataGridView9_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(MessageBox.Show("Хотите отредактровать занятие?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Open_Zanyatiya(dataGridView9.SelectedRows[0].Cells[0].Value.ToString(), MySqlOperations.Select_Text(MySqlQueries.Select_ID_Homework, dataGridView9.SelectedRows[0].Cells[0].Value.ToString()));
        }

        private void Open_Zanyatiya(string ID, string ID2)
        {
            Zanyatiya zanyatiya = new Zanyatiya(MySqlQueries, MySqlOperations, ID, ID2);
            zanyatiya.Text += ID;
            zanyatiya.Zanyatiya_Closed += Load_Zanyatiya;
            zanyatiya.Show();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Fakultativy fakultativy = new Fakultativy(MySqlQueries, MySqlOperations);
            MySqlOperations.Search_In_ComboBox(MySqlOperations.Select_Text(MySqlQueries.Select_FIO_Prepod, ID_Prepoda), fakultativy.comboBox3);
            fakultativy.comboBox3.Enabled = false;
            MySqlOperations.Search_In_ComboBox(MySqlOperations.Select_Text(MySqlQueries.Select_Predmet_Prepoda, ID_Prepoda), fakultativy.comboBox1);
            fakultativy.comboBox1.Enabled = false;
            fakultativy.dateTimePicker1.Value = DateTime.Now;
            fakultativy.dateTimePicker1.MinDate = DateTime.Now;
            fakultativy.button1.Visible = true;
            fakultativy.button3.Visible = false;
            fakultativy.AcceptButton = fakultativy.button1;
            fakultativy.Fakultativy_Closed += Load_Fakultativy;
            fakultativy.Show();
        }

        private void Load_Fakultativy(object sender, EventArgs e)
        {
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Fakultativy_Prepoda, dataGridView10, ID_Prepoda);
            dataGridView10.Columns[0].Visible = false;
            dataGridView10.ClearSelection();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Delete_Fakultativy();
            Load_Fakultativy(this, EventArgs.Empty);
        }

        private void Delete_Fakultativy()
        {
            if (MessageBox.Show("Хотите удалить запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                foreach (DataGridViewRow row in dataGridView10.SelectedRows)
                    MySqlOperations.Insert_Update_Delete(MySqlQueries.Delete_Fakultativy, row.Cells[0].Value.ToString());
        }

        private void dataGridView10_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            Delete_Fakultativy();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Edit_Fakultativy();
        }

        private void Edit_Fakultativy()
        {
            foreach (DataGridViewRow row in dataGridView10.SelectedRows)
            {
                Fakultativy fakultativy = new Fakultativy(MySqlQueries, MySqlOperations, row.Cells[0].Value.ToString());
                MySqlOperations.Search_In_ComboBox(MySqlOperations.Select_Text(MySqlQueries.Select_FIO_Prepod, ID_Prepoda), fakultativy.comboBox3);
                fakultativy.comboBox3.Enabled = false;
                MySqlOperations.Search_In_ComboBox(MySqlOperations.Select_Text(MySqlQueries.Select_Predmet_Prepoda, ID_Prepoda), fakultativy.comboBox1);
                fakultativy.comboBox1.Enabled = false;
                fakultativy.dateTimePicker1.Value = DateTime.Parse(row.Cells[1].Value.ToString());
                fakultativy.dateTimePicker1.MinDate = fakultativy.dateTimePicker1.Value;
                MySqlOperations.Search_In_ComboBox(row.Cells[2].Value.ToString(), fakultativy.comboBox2);
                fakultativy.dateTimePicker2.Value = DateTime.Parse(row.Cells[3].Value.ToString());
                fakultativy.dateTimePicker3.Value = DateTime.Parse(row.Cells[4].Value.ToString());
                fakultativy.button3.Visible = true;
                fakultativy.button1.Visible = false;
                fakultativy.AcceptButton = fakultativy.button3;
                fakultativy.Fakultativy_Closed += Load_Fakultativy;
                fakultativy.Show();
            }
        }

        private void dataGridView10_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Edit_Fakultativy();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            Open_Zanyatiya(dataGridView9.SelectedRows[0].Cells[0].Value.ToString(), MySqlOperations.Select_Text(MySqlQueries.Select_ID_Homework, dataGridView9.SelectedRows[0].Cells[0].Value.ToString()));
        }

        private void dataGridView9_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            Delete_Zanyatiya();
        }

        private void Delete_Zanyatiya()
        {
            if (MessageBox.Show("Хотите удалить запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                foreach (DataGridViewRow row in dataGridView9.SelectedRows)
                    MySqlOperations.Insert_Update_Delete(MySqlQueries.Delete_Zanyatiya, row.Cells[0].Value.ToString());
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            Delete_Zanyatiya();
            Load_Zanyatiya(this, EventArgs.Empty);
        }

        private void Load_Uspevaemost()
        {
            dataGridView8.Rows.Clear();
            DataTable dt = MySqlOperations.Select_DataTable(MySqlQueries.Select_Jurnal_Ucheniki, null, MySqlOperations.Select_Text(MySqlQueries.Select_ID_Klassy_ComboBox, null, comboBox4.Text));
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
                            dt = MySqlOperations.Select_DataTable(MySqlQueries.Select_Jurnal_Daily, null, dateTimePicker2.Value.Year.ToString() + '-' + '0' + dateTimePicker2.Value.Month.ToString(), ID_Prepoda,
                            MySqlOperations.Select_Text(MySqlQueries.Select_ID_Klassy_ComboBox, null, comboBox4.Text), "0" + i.ToString());
                            for (int j = 0; j < dt.Rows.Count; j++)
                                dataGridView8.Rows[j].Cells[i].Value = dt.Rows[j][0];
                        }
                        else
                        {
                            dt = MySqlOperations.Select_DataTable(MySqlQueries.Select_Jurnal_Daily, null, dateTimePicker2.Value.Year.ToString() + '-' + '0' + dateTimePicker2.Value.Month.ToString(), ID_Prepoda,
                            MySqlOperations.Select_Text(MySqlQueries.Select_ID_Klassy_ComboBox, null, comboBox4.Text), i.ToString());
                            for (int j = 0; j < dt.Rows.Count; j++)
                                dataGridView8.Rows[j].Cells[i].Value = dt.Rows[j][0];
                        }
                    dt = MySqlOperations.Select_DataTable(MySqlQueries.Select_Jurnal_SrBal, null, dateTimePicker2.Value.Year.ToString() + '-' + '0' + dateTimePicker2.Value.Month.ToString(), ID_Prepoda,
                            MySqlOperations.Select_Text(MySqlQueries.Select_ID_Klassy_ComboBox, null, comboBox4.Text));
                    for (int j = 0; j < dt.Rows.Count; j++)
                        dataGridView8.Rows[j].Cells[32].Value = dt.Rows[j][0];
                    label6.Text = MySqlOperations.Select_Text(MySqlQueries.Select_SrBal_Klassa, null, dateTimePicker2.Value.Year.ToString() + '-' + '0' + dateTimePicker2.Value.Month.ToString(),
                        MySqlOperations.Select_Text(MySqlQueries.Select_ID_Klassy_ComboBox, null, comboBox4.Text));
                }
                else
                {
                    dataGridView8.Rows.Add(dt.Rows.Count);
                    for (int i = 0; i < dt.Rows.Count; i++)
                        dataGridView8.Rows[i].Cells[0].Value = dt.Rows[i][0];
                    for (int i = 1; i <= 31; i++)
                        if (i < 10)
                        {
                            dt = MySqlOperations.Select_DataTable(MySqlQueries.Select_Jurnal_Daily, null, dateTimePicker2.Value.Year.ToString() + '-' + dateTimePicker2.Value.Month.ToString(), ID_Prepoda,
                            MySqlOperations.Select_Text(MySqlQueries.Select_ID_Klassy_ComboBox, null, comboBox4.Text), "0" + i.ToString());
                            for (int j = 0; j < dt.Rows.Count; j++)
                                dataGridView8.Rows[j].Cells[i].Value = dt.Rows[j][0];
                        }
                        else
                        {
                            dt = MySqlOperations.Select_DataTable(MySqlQueries.Select_Jurnal_Daily, null, dateTimePicker2.Value.Year.ToString() + '-' + dateTimePicker2.Value.Month.ToString(), ID_Prepoda,
                            MySqlOperations.Select_Text(MySqlQueries.Select_ID_Klassy_ComboBox, null, comboBox4.Text), i.ToString());
                            for (int j = 0; j < dt.Rows.Count; j++)
                                dataGridView8.Rows[j].Cells[i].Value = dt.Rows[j][0];
                        }
                    dt = MySqlOperations.Select_DataTable(MySqlQueries.Select_Jurnal_SrBal, null, dateTimePicker2.Value.Year.ToString() + '-' + dateTimePicker2.Value.Month.ToString(), ID_Prepoda,
                            MySqlOperations.Select_Text(MySqlQueries.Select_ID_Klassy_ComboBox, null, comboBox4.Text));
                    for (int j = 0; j < dt.Rows.Count; j++)
                        dataGridView8.Rows[j].Cells[32].Value = dt.Rows[j][0];
                    label6.Text = MySqlOperations.Select_Text(MySqlQueries.Select_SrBal_Klassa, null, dateTimePicker2.Value.Year.ToString() + '-' + dateTimePicker2.Value.Month.ToString(),
                        MySqlOperations.Select_Text(MySqlQueries.Select_ID_Klassy_ComboBox, null, comboBox4.Text));

                }
            }
            else
            {
                dataGridView8.Rows.Clear();
                label6.Text = "";
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_Uspevaemost();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            Load_Uspevaemost();
        }

        private void поискToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 4)
                MySqlOperations.Search(toolStripTextBox1, dataGridView9);
            if (tabControl1.SelectedIndex == 5)
                MySqlOperations.Search(toolStripTextBox1, dataGridView10);
        }

        private void фильтрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 4)
            {
                MySqlOperations.Select_DataGridView(MySqlQueries.Select_Zanyatiya_Filter, dataGridView9, ID_Prepoda, toolStripTextBox1.Text);
                dataGridView9.Columns[0].Visible = false;
            }
            if (tabControl1.SelectedIndex == 5)
            {
                MySqlOperations.Select_DataGridView(MySqlQueries.Select_Fakultativy_Prepoda_Filter, dataGridView10, ID_Prepoda, toolStripTextBox1.Text);
                dataGridView10.Columns[0].Visible = false;
                dataGridView10.ClearSelection();
            }
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text == "")
            {
                    MySqlOperations.Select_DataGridView(MySqlQueries.Select_Zanyatiya, dataGridView9, ID_Prepoda);
                    dataGridView9.Columns[0].Visible = false;
                    MySqlOperations.Select_DataGridView(MySqlQueries.Select_Fakultativy_Prepoda, dataGridView10, ID_Prepoda);
                    dataGridView10.Columns[0].Visible = false;
                    dataGridView10.ClearSelection();
            }
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
            MySqlOperations.Print_Jurnal(comboBox4.Text, dateTimePicker2, ID_Prepoda, saveFileDialog1, dataTable);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_Uspevaemost();
            Load_Fakultativy(this, EventArgs.Empty);
            Load_Zanyatiya(this, EventArgs.Empty);
        }
    }
}
