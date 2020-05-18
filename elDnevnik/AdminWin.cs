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
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Auditorii, dataGridView1);
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.ClearSelection();
            identify = "auditorii";
        }

        private void предметыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Predmety, dataGridView1);
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.ClearSelection();
            identify = "predmety";
        }

        private void вставкаToolStripMenuItem_Click(object sender, EventArgs e)
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
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
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
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MessageBox.Show("Хотите отредактировать запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Хотите удалить запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (identify == "auditorii")
                {
                    MySqlOperations.Insert_Update_Delete(MySqlQueries.Delete_Auditorii, dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    MySqlOperations.Select_DataGridView(MySqlQueries.Select_Auditorii, dataGridView1);
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.ClearSelection();
                    identify = "auditorii";
                }
                if (identify == "predmety")
                {
                    MySqlOperations.Insert_Update_Delete(MySqlQueries.Delete_Predmety, dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    MySqlOperations.Select_DataGridView(MySqlQueries.Select_Predmety, dataGridView1);
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.ClearSelection();
                    identify = "predmety";
                }
            }
        }
    }
}
