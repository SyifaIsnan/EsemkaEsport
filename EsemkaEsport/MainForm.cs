using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EsemkaEsport
{
    public partial class MainForm: Form
    {
        public MainForm()
        {
            InitializeComponent();
            tampildata();
 

        }

        private void tampildata()
        {
            using (var conn = Properti.conn())
            {
                SqlCommand cmd = new SqlCommand("select schedule.id, concat(HomeTeam.team_name, ' vs ' , AwayTeam.team_name) as Match , schedule.time from [schedule] inner join [team] as AwayTeam on schedule.away_team_id = AwayTeam.id inner join [team] as HomeTeam on schedule.home_team_id = HomeTeam.id;", conn);
                conn.Open();
                cmd.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);

                DataGridViewButtonColumn b = new DataGridViewButtonColumn();
                b.Text = "Book";
                b.Name = "Book";
                b.UseColumnTextForButtonValue = true;
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["id"].Visible = false;               
                dataGridView1.Columns.Add(b);
                conn.Close();



            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                string id = dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();
                BookForm bookForm = new BookForm(id);
                bookForm.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MyTcket myTcket = new MyTcket();
            myTcket.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var mess = MessageBox.Show("Apakah anda ingin logout?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (mess == DialogResult.Yes)
            {
                this.Hide();
                FormLogin loginForm = new FormLogin();
                loginForm.ShowDialog();

            }
        }
    }
}
