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
    public partial class AdminWin : Form
    {
        MySqlQueries MySqlQueries = null;
        MySqlOperations MySqlOperations = null;
        string identify = null;

        public AdminWin()
        {
            InitializeComponent();
            MySqlQueries = new MySqlQueries();
            MySqlOperations = new MySqlOperations(MySqlQueries);
        }

        private void AdminWin_Load(object sender, EventArgs e)
        {
            MySqlOperations.OpenConnection();
        }

        private void AdminWin_FormClosed(object sender, FormClosedEventArgs e)
        {
            MySqlOperations.CloseConnection();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MySqlOperations.Search(toolStripTextBox1, dataGridView1);
        }

        private void опрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("О программе...", "О программе...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void аудиторииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            identify = "auditorii";
            Load_Table();
        }

        private void предметыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            identify = "predmety";
            Load_Table();
        }

        private void факультативыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            identify = "fakultativy";
            Load_Table();
        }

        private void классыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            identify = "klassy";
            Load_Table();
        }

        private void преподавателиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            identify = "prepod";
            Load_Table();
        }

        private void ученикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            identify = "ucheniki";
            Load_Table();
        }

        private void расписаниеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            identify = "raspisanie";
            Load_Table();
        }

        private void вставкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Insert_String();
        }

        private void Insert_String()
        {
            if (identify == "auditorii")
            {
                Auditorii auditorii = new Auditorii(MySqlQueries, MySqlOperations);
                auditorii.button1.Visible = true;
                auditorii.button3.Visible = false;
                auditorii.AcceptButton = auditorii.button1;
                auditorii.Auditorii_Closed += аудиторииToolStripMenuItem_Click;
                auditorii.Show();
            }
            if (identify == "predmety")
            {
                Predmety predmety = new Predmety(MySqlQueries, MySqlOperations);
                predmety.button1.Visible = true;
                predmety.button3.Visible = false;
                predmety.AcceptButton = predmety.button1;
                predmety.Auditorii_Closed += предметыToolStripMenuItem_Click;
                predmety.Show();
            }
            if (identify == "fakultativy")
            {
                Fakultativy fakultativy = new Fakultativy(MySqlQueries, MySqlOperations);
                fakultativy.dateTimePicker1.Value = DateTime.Now;
                fakultativy.dateTimePicker1.MinDate = DateTime.Now;
                fakultativy.button1.Visible = true;
                fakultativy.button3.Visible = false;
                fakultativy.AcceptButton = fakultativy.button1;
                fakultativy.Fakultativy_Closed += факультативыToolStripMenuItem_Click;
                fakultativy.Show();
            }
            if (identify == "klassy")
            {
                Klassy klassy = new Klassy(MySqlQueries, MySqlOperations);
                klassy.button1.Visible = true;
                klassy.button3.Visible = false;
                klassy.AcceptButton = klassy.button1;
                klassy.Klassy_Closed += классыToolStripMenuItem_Click;
                klassy.Show();
            }
            if (identify == "prepod")
            {
                Prepod prepod = new Prepod(MySqlQueries, MySqlOperations);
                prepod.button1.Visible = true;
                prepod.button3.Visible = false;
                prepod.AcceptButton = prepod.button1;
                prepod.Prepod_Closed += преподавателиToolStripMenuItem_Click;
                prepod.Show();
            }
            if (identify == "ucheniki")
            {
                Ucheniki ucheniki = new Ucheniki(MySqlQueries, MySqlOperations);
                ucheniki.button1.Visible = true;
                ucheniki.button3.Visible = false;
                ucheniki.AcceptButton = ucheniki.button1;
                ucheniki.Ucheniki_Closed += ученикиToolStripMenuItem_Click;
                ucheniki.Show();
            }
            if (identify == "raspisanie")
            {
                Raspisanie raspisanie = new Raspisanie(MySqlQueries, MySqlOperations);
                raspisanie.button4.Visible = true;
                raspisanie.button5.Visible = false;
                raspisanie.AcceptButton = raspisanie.button1;
                raspisanie.Raspisanie_Closed += расписаниеToolStripMenuItem_Click;
                raspisanie.Show();
            }
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Update_String();
        }

        private void Update_String()
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (identify == "auditorii")
                {
                    Auditorii auditorii = new Auditorii(MySqlQueries, MySqlOperations, row.Cells[0].Value.ToString());
                    auditorii.textBox1.Text = row.Cells[1].Value.ToString();
                    auditorii.button3.Visible = true;
                    auditorii.button1.Visible = false;
                    auditorii.AcceptButton = auditorii.button3;
                    auditorii.Auditorii_Closed += аудиторииToolStripMenuItem_Click;
                    auditorii.Show();
                }
                if (identify == "predmety")
                {
                    Predmety predmety = new Predmety(MySqlQueries, MySqlOperations, row.Cells[0].Value.ToString());
                    predmety.textBox1.Text = row.Cells[1].Value.ToString();
                    predmety.button3.Visible = true;
                    predmety.button1.Visible = false;
                    predmety.AcceptButton = predmety.button3;
                    predmety.Auditorii_Closed += предметыToolStripMenuItem_Click;
                    predmety.Show();
                }
                if (identify == "fakultativy")
                {
                    Fakultativy fakultativy = new Fakultativy(MySqlQueries, MySqlOperations, row.Cells[0].Value.ToString());
                    MySqlOperations.Search_In_ComboBox(row.Cells[1].Value.ToString(), fakultativy.comboBox1);
                    fakultativy.dateTimePicker1.Value = DateTime.Parse(row.Cells[2].Value.ToString());
                    fakultativy.dateTimePicker1.MinDate = fakultativy.dateTimePicker1.Value;
                    MySqlOperations.Search_In_ComboBox(row.Cells[3].Value.ToString(), fakultativy.comboBox2);
                    MySqlOperations.Search_In_ComboBox(row.Cells[4].Value.ToString(), fakultativy.comboBox3);
                    fakultativy.dateTimePicker2.Value = DateTime.Parse(row.Cells[5].Value.ToString());
                    fakultativy.dateTimePicker3.Value = DateTime.Parse(row.Cells[6].Value.ToString());
                    fakultativy.button3.Visible = true;
                    fakultativy.button1.Visible = false;
                    fakultativy.AcceptButton = fakultativy.button3;
                    fakultativy.Fakultativy_Closed += факультативыToolStripMenuItem_Click;
                    fakultativy.Show();
                }
                if (identify == "klassy")
                {
                    Klassy klassy = new Klassy(MySqlQueries, MySqlOperations, row.Cells[0].Value.ToString());
                    klassy.numericUpDown1.Value = int.Parse(row.Cells[1].Value.ToString().Split(' ')[0]);
                    MySqlOperations.Search_In_ComboBox(row.Cells[1].Value.ToString().Split(' ')[1], klassy.comboBox1);
                    klassy.numericUpDown2.Value = int.Parse(row.Cells[2].Value.ToString());
                    klassy.button3.Visible = true;
                    klassy.button1.Visible = false;
                    klassy.AcceptButton = klassy.button3;
                    klassy.Klassy_Closed += классыToolStripMenuItem_Click;
                    klassy.Show();
                }
                if (identify == "prepod")
                {
                    Prepod prepod = new Prepod(MySqlQueries, MySqlOperations, row.Cells[0].Value.ToString());
                    prepod.textBox1.Text = row.Cells[1].Value.ToString().Split(' ')[0];
                    prepod.textBox2.Text = row.Cells[1].Value.ToString().Split(' ')[1];
                    prepod.textBox3.Text = row.Cells[1].Value.ToString().Split(' ')[2];
                    MySqlOperations.Search_In_ComboBox(row.Cells[2].Value.ToString(), prepod.comboBox1);
                    prepod.textBox4.Text = row.Cells[3].Value.ToString();
                    prepod.textBox5.Text = row.Cells[4].Value.ToString();
                    prepod.button3.Visible = true;
                    prepod.button1.Visible = false;
                    prepod.AcceptButton = prepod.button3;
                    prepod.Prepod_Closed += преподавателиToolStripMenuItem_Click;
                    prepod.Show();
                }
                if (identify == "ucheniki")
                {
                    Ucheniki ucheniki = new Ucheniki(MySqlQueries, MySqlOperations, row.Cells[0].Value.ToString());
                    ucheniki.textBox1.Text = row.Cells[1].Value.ToString().Split(' ')[0];
                    ucheniki.textBox2.Text = row.Cells[1].Value.ToString().Split(' ')[1];
                    ucheniki.textBox3.Text = row.Cells[1].Value.ToString().Split(' ')[2];
                    MySqlOperations.Search_In_ComboBox(row.Cells[2].Value.ToString(), ucheniki.comboBox1);
                    ucheniki.textBox4.Text = row.Cells[3].Value.ToString();
                    ucheniki.textBox5.Text = row.Cells[4].Value.ToString();
                    ucheniki.button3.Visible = true;
                    ucheniki.button1.Visible = false;
                    ucheniki.AcceptButton = ucheniki.button3;
                    ucheniki.Ucheniki_Closed += ученикиToolStripMenuItem_Click;
                    ucheniki.Show();
                }
                if (identify == "raspisanie")
                {
                    Raspisanie raspisanie = new Raspisanie(MySqlQueries, MySqlOperations, row.Cells[0].Value.ToString());
                    MySqlOperations.Search_In_ComboBox(row.Cells[1].Value.ToString(), raspisanie.comboBox1);
                    MySqlOperations.Search_In_ComboBox(row.Cells[2].Value.ToString(), raspisanie.comboBox2);
                    raspisanie.groupBox1.Visible = true;
                    MySqlOperations.Select_DataGridView(MySqlQueries.Select_Uroki_Raspisaniya, raspisanie.dataGridView1, row.Cells[0].Value.ToString());
                    raspisanie.dataGridView1.Columns[0].Visible = false;
                    raspisanie.button5.Visible = true;
                    raspisanie.button4.Visible = false;
                    raspisanie.AcceptButton = raspisanie.button3;
                    raspisanie.Raspisanie_Closed += расписаниеToolStripMenuItem_Click;
                    raspisanie.Show();
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MessageBox.Show("Хотите отредактировать запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Update_String();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Хотите удалить запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Delete_String();
                Load_Table();
            }
        }

        private void Load_Table()
        {
            if (identify == "auditorii")
            {
                MySqlOperations.Select_DataGridView(MySqlQueries.Select_Auditorii, dataGridView1);
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.ClearSelection();
            }
            if (identify == "predmety")
            {
                MySqlOperations.Select_DataGridView(MySqlQueries.Select_Predmety, dataGridView1);
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.ClearSelection();
            }
            if (identify == "fakultativy")
            {
                MySqlOperations.Select_DataGridView(MySqlQueries.Select_Fakultativy, dataGridView1);
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.ClearSelection();
            }
            if (identify == "klassy")
            {
                MySqlOperations.Select_DataGridView(MySqlQueries.Select_Klassy, dataGridView1);
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.ClearSelection();
            }
            if (identify == "prepod")
            {
                MySqlOperations.Select_DataGridView(MySqlQueries.Select_Prepod, dataGridView1);
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.ClearSelection();
            }
            if (identify == "ucheniki")
            {
                MySqlOperations.Select_DataGridView(MySqlQueries.Select_Ucheniki, dataGridView1);
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.ClearSelection();
            }
            if (identify == "raspisanie")
            {
                MySqlOperations.Select_DataGridView(MySqlQueries.Select_Raspisanie, dataGridView1);
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.ClearSelection();
            }
        }

        private void Delete_String()
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (identify == "auditorii")
                    MySqlOperations.Insert_Update_Delete(MySqlQueries.Delete_Auditorii, row.Cells[0].Value.ToString());
                if (identify == "predmety")
                    MySqlOperations.Insert_Update_Delete(MySqlQueries.Delete_Predmety, row.Cells[0].Value.ToString());
                if (identify == "fakultativy")
                    MySqlOperations.Insert_Update_Delete(MySqlQueries.Delete_Fakultativy, row.Cells[0].Value.ToString());
                if (identify == "klassy")
                    MySqlOperations.Insert_Update_Delete(MySqlQueries.Delete_Klassy, row.Cells[0].Value.ToString());
                if (identify == "prepod")
                    MySqlOperations.Insert_Update_Delete(MySqlQueries.Delete_Prepod, row.Cells[0].Value.ToString());
                if (identify == "ucheniki")
                    MySqlOperations.Insert_Update_Delete(MySqlQueries.Delete_Ucheniki, row.Cells[0].Value.ToString());
                if (identify == "raspisanie")
                    MySqlOperations.Insert_Update_Delete(MySqlQueries.Delete_Raspisanie, row.Cells[0].Value.ToString());
            }
        }
    }
}
