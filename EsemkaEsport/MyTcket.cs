using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EsemkaEsport
{
    public partial class MyTcket: Form
    {
        public MyTcket()
        {
            InitializeComponent();
            tampildata();
        }

        private void tampildata()
        {
            using (var conn = Properti.conn())
            {
                SqlCommand cmd = new SqlCommand("select concat(homeTeam.team_name , ' vs ' , awayTeam.team_name) as Match, schedule.time as Time, schedule_detail.total_ticket as TotalTicket from [schedule_detail] \r\ninner join [schedule] on schedule_detail.schedule_id = schedule.id \r\ninner join [team] as homeTeam on schedule.home_team_id = homeTeam.id  \r\ninner join [team] as awayTeam on schedule.away_team_id = awayTeam.id  \r\n\r\nwhere user_id=@user_id;", conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                cmd.Parameters.AddWithValue("@user_id", Properti.userid);
                DataTable dt = new DataTable();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                dataGridView1.DataSource = dt;

                DataGridViewLinkColumn Print = new DataGridViewLinkColumn();
                Print.Text = "Print";
                Print.HeaderText = "";
                Print.UseColumnTextForLinkValue = true;
               

                dataGridView1.Columns["Time"].DefaultCellStyle.Format = "dddd, dd MMMM yyyy (HH:mm)";
                dataGridView1.Columns.Add(Print);
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm();
            mainForm.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int rowIndex = e.RowIndex;
                int columnIndex = e.ColumnIndex;
                MessageBox.Show($"Row Index: {rowIndex}, Column Index: {columnIndex}");
            }

            int printid = 0;
            int matchid = 1;
            int timeid = 2;
            int totalticketid = 3;
            
            if (e.ColumnIndex == printid)
            {
                string match = dataGridView1.Rows[e.RowIndex].Cells[matchid].Value.ToString();
                string time = dataGridView1.Rows[e.RowIndex].Cells[timeid].Value.ToString();
                string totalticket = dataGridView1.Rows[e.RowIndex].Cells[totalticketid].Value.ToString();               
                Print p = new Print(match, time, totalticket);
                p.ShowDialog();
            }
        }
    }
}
