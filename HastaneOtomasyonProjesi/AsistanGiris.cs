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
    public partial class AsistanGiris : Form
    {
        public AsistanGiris()
        {
            InitializeComponent();
        }
        SqlBaglatisiClass bgl = new SqlBaglatisiClass();
        private void AsistanGiris_Load(object sender, EventArgs e)
        {

        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * From Tbl_Asistan where AsistanTC=@p1 and AsistanSifre=@p2",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",msktxtTC.Text);
            komut.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                FrmAsistanDetay fr = new FrmAsistanDetay();
                fr.tckimlik = msktxtTC.Text;
                fr.Show();
            }
            else
            {
                MessageBox.Show("Yanlış şifre ya da kimlik numarası\nLütfen kontrol ediniz...", "Hatalı giriş denemesi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
