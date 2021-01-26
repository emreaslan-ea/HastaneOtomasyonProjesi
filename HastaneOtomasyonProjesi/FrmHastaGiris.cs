using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HastaneOtomasyonProjesi
{
    public partial class FrmHastaGiris : Form
    {
        public FrmHastaGiris()
        {
            InitializeComponent();
            randompin();
        }

        public void randompin()
        {
            string[] sembol1 = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "R", "S", "T", "U", "V", "Y", "Z", "Q", "W", "X" };
            string[] sembol2 = { "a", "b", "c", "d", "e", "f", "g", "h", "ı", "i", "j", "k", "l", "m", "n", "o", "p", "r", "s", "ş", "t", "u", "v", "y", "z", "q", "w", "x" };

            Random rndm = new Random();
            int s1, s2, s3, s4;
            s1 = rndm.Next(0, sembol1.Length);
            s2 = rndm.Next(0, sembol2.Length);
            s3 = rndm.Next(0, 10);
            s4 = rndm.Next(0, 10);
            lblPin.Text = sembol1[s1] + s3.ToString() + sembol2[s2] + s4.ToString();

        }

        SqlBaglatisiClass bgl = new SqlBaglatisiClass();

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaKayit fr = new FrmHastaKayit();
            fr.Show();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * From Tbl_Hastalar Where HastaTC=@p1 and HastaSifre=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", msktxtTC.Text);
            komut.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read() && lblPin.Text == txtrndmpin.Text)
            {
                FrmHastaDetay frm = new FrmHastaDetay();
                frm.tc = msktxtTC.Text;
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC kimlik numarası ya da şifre...\nLütfen kontrol ediniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) 
            {
                txtSifre.UseSystemPasswordChar = false;
            }
            if (checkBox1.Checked == false)
            {
                txtSifre.UseSystemPasswordChar=true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           randompin();
        }

        private void FrmHastaGiris_Load(object sender, EventArgs e)
        {

        }
    }
}
