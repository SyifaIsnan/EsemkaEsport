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
    public partial class FormLogin: Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            FormRegister fr = new FormRegister();
            fr.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var conn = Properti.conn())
            {
                try
                {
                    if (Properti.validasi(this.Controls))
                    {
                        MessageBox.Show("Data yang ingin diinput tidak boleh kosong!");
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("select id from [User] where username = @username and password = @password ", conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@username", textBox1.Text);
                        cmd.Parameters.AddWithValue("@password", textBox2.Text);
                        DataTable dt = new DataTable();
                        int userid = Convert.ToInt32(cmd.ExecuteScalar());

                        if (userid != 0)
                        {
                            this.Hide();
                            Properti.userid = Convert.ToInt32(userid);
                            MainForm mf = new MainForm();
                            mf.ShowDialog();
                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
            }
            else { 
                textBox2.PasswordChar = '*';
            }
        }
    }
}
