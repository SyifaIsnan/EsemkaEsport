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
    public partial class FormRegister: Form
    {
        public FormRegister()
        {
            InitializeComponent();
        }

        private bool validasi()
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Silahkan isi username!");
                return false;
            }

            if (textBox2.Text == "")
            {
                MessageBox.Show("Silahkan isi password anda!");
            }

            if (textBox3.Text == "")
            {
                MessageBox.Show("Silahkan isi password anda!");
            }

            if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("Mohon masukkan password yang sama!");
            }

            if (textBox2.Text.Length < 6)
            {
                MessageBox.Show("Password harus lebih dari 6 karakter!");
            }

            if (!radioButton1.Checked && !radioButton2.Checked)
            {
                MessageBox.Show("Silahkan pilih gender anda!");
                return false;
            }

            return true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            FormLogin fl = new FormLogin();
            fl.ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var conn = Properti.conn())
            {
                if (validasi())
                {
                    try
                    {

                        //Mengecek apakah akun sudah ada atau belum
                        SqlCommand user = new SqlCommand("select count(*) from [User] where username = @username", conn);
                        user.CommandType = CommandType.Text;
                        conn.Open();
                        user.Parameters.AddWithValue("@username", textBox1.Text);
                        int username = (int)user.ExecuteScalar();
                        if (username > 0)
                        {
                            MessageBox.Show("Akun sudah terdaftar!");
                            return;
                        }
                        conn.Close();


                        //Membuat akun
                        SqlCommand bikinAkun = new SqlCommand("insert into [User] (username , password, birthdate, gender, role, created_at) values (@username , @password, @birthdate, @gender, @role, @created_at)", conn);
                        bikinAkun.CommandType = CommandType.Text;
                        conn.Open();
                        bikinAkun.Parameters.AddWithValue("@username", textBox1.Text);
                        bikinAkun.Parameters.AddWithValue("@password", textBox2.Text);
                        bikinAkun.Parameters.AddWithValue("@birthdate", dateTimePicker1.Value);
                        if (radioButton1.Checked)
                        {
                            bikinAkun.Parameters.AddWithValue("@gender", 1);
                        }
                        else if (radioButton2.Checked)
                        {
                            bikinAkun.Parameters.AddWithValue("@gender", 0);
                        }
                        bikinAkun.Parameters.AddWithValue("@role", 1);
                        bikinAkun.Parameters.AddWithValue("created_at", DateTime.Now);
                        bikinAkun.ExecuteNonQuery();

                        var mess = MessageBox.Show("Membuat akun berhasil, silahkan login dengan akun yang tersedia!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (mess == DialogResult.OK) { 
                            this.Hide();
                            FormLogin fl = new FormLogin(); 
                            fl.ShowDialog();
                        }


                        conn.Close();

                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        MessageBox.Show(ex.Message);
                    }
                }
            }

        }

        private void FormRegister_Load(object sender, EventArgs e)
        {

        }
    }
}
