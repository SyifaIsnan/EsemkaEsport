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
    public partial class BookForm: Form
    {
        private string scheduleid;
        public BookForm(string scheduleid)
        {
            InitializeComponent();

            this.scheduleid = scheduleid;

            tampildata();
            nicknamehometeam();
            nicknameawayteam();           
            sisatiket();
        }

        private void sisatiket()
        {
            using (var conn = Properti.conn())
            {
                SqlCommand cmd = new SqlCommand("select 60 - coalesce(sum(total_ticket),0) from [schedule_detail] where schedule_id = @schedule_id;", conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                cmd.Parameters.AddWithValue("@schedule_id", scheduleid);
                DataTable dt = new DataTable();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                string sisatiket = dt.Rows[0][0].ToString();
                label6.Text = sisatiket + " Tickets";
                conn.Close();
            }
        }

        private void nicknameawayteam()
        {
            using (var conn = Properti.conn())
            {
                SqlCommand cmd = new SqlCommand("select player.nick_name from [schedule] inner join [team] on schedule.away_team_id = team.id inner join [team_detail] on team.id = team_detail.team_id inner join [player] on team_detail.player_id = player.id where schedule.id = @scheduleid", conn);
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@scheduleid", scheduleid);
                DataTable dt = new DataTable();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                conn.Close();
                dataGridView2.DataSource = dt;
            }
        }

        private void nicknamehometeam()
        {
            using (var conn = Properti.conn())
            {
                SqlCommand cmd = new SqlCommand("select player.nick_name from [schedule] inner join [team] on schedule.home_team_id = team.id inner join [team_detail] on team.id = team_detail.team_id inner join [player] on team_detail.player_id = player.id where schedule.id = @scheduleid", conn);
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@scheduleid", scheduleid);
                DataTable dt = new DataTable();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                conn.Close();
                dataGridView1.DataSource = dt;
            }
        }

        private void tampildata()
        {
            using (var conn = Properti.conn())
            {
                SqlCommand teamHome = new SqlCommand("select team.team_name from [schedule] inner join [team] on schedule.home_team_id = team.id where schedule.id = @scheduleid", conn);
                conn.Open();
                teamHome.CommandType = CommandType.Text;
                teamHome.Parameters.AddWithValue("@scheduleid", scheduleid);
                DataTable dt = new DataTable();
                SqlDataReader dr = teamHome.ExecuteReader();
                dt.Load(dr);
                string hometeamname = dt.Rows[0][0].ToString();
                label1.Text = hometeamname;

                SqlCommand teamAway = new SqlCommand("select team.team_name from [schedule] inner join [team] on schedule.away_team_id = team.id where schedule.id = @scheduleid", conn);               
                teamAway.CommandType = CommandType.Text;
                teamAway.Parameters.AddWithValue("@scheduleid", scheduleid);
                DataTable dt1 = new DataTable();
                SqlDataReader dr1 = teamAway.ExecuteReader();
                dt1.Load(dr1);
                string awayteamname = dt1.Rows[0][0].ToString();
                label2.Text = awayteamname;


                SqlCommand companyHome = new SqlCommand("select team.company_name from [schedule] inner join [team] on schedule.home_team_id = team.id where schedule.id = @scheduleid", conn);
                companyHome.CommandType = CommandType.Text;
                companyHome.Parameters.AddWithValue("@scheduleid", scheduleid);
                DataTable dt2 = new DataTable();
                SqlDataReader dr2 = companyHome.ExecuteReader();
                dt2.Load(dr2);
                string companyhome = dt2.Rows[0][0].ToString();
                label7.Text = companyhome;

                SqlCommand companyAway = new SqlCommand("select team.company_name from [schedule] inner join [team] on schedule.away_team_id = team.id where schedule.id = @scheduleid", conn);
                companyAway.CommandType = CommandType.Text;
                companyAway.Parameters.AddWithValue("@scheduleid", scheduleid);
                DataTable dt3 = new DataTable();
                SqlDataReader dr3 = companyAway.ExecuteReader();
                dt3.Load(dr3);
                string companyaway = dt3.Rows[0][0].ToString();
                label8.Text = companyaway;

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            using (var conn = Properti.conn())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("insert into [Schedule_detail] (schedule_id, user_id, total_ticket, created_at) values (@schedule_id, @user_id, @total_ticket, @created_at)", conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@schedule_id", scheduleid);
                    cmd.Parameters.AddWithValue("@user_id", Properti.userid);
                    cmd.Parameters.AddWithValue("@total_ticket", numericUpDown1.Value);
                    cmd.Parameters.AddWithValue("@created_at", DateTime.Now);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tiket berhasil dipesan!");
                    sisatiket();
                    clear();
                    conn.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void clear()
        {
            numericUpDown1.Value = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm();
            mainForm.ShowDialog();
        }
    }
}
