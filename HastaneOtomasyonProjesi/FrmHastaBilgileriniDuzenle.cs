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
    public partial class FrmHastaBilgileriniDuzenle : Form
    {
        public FrmHastaBilgileriniDuzenle()
        {
            InitializeComponent();
        }
        SqlBaglatisiClass bgl = new SqlBaglatisiClass();
        public string TCno;
        private void FrmHastaBilgileriniDuzenle_Load(object sender, EventArgs e)
        {
            msktxtTC.Text = TCno;
            SqlCommand komut = new SqlCommand("SELECT * FROM TBL_HASTALAR WHERE HASTATC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",msktxtTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtAd.Text = dr[1].ToString();
                txtSoyad.Text = dr[2].ToString();
                msktxtTelefon.Text = dr[4].ToString();
                txtSifre.Text = dr[5].ToString();
                cmbCinsiyet.Text = dr[6].ToString();
            }
            bgl.baglanti().Close();
        }

        private void btnBilgilerimiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("Update TBL_HASTALAR SET HASTAAD=@P1,HASTASOYAD=@P2,HASTATELEFON=@P3,HASTASifre=@P4,HASTACinsiyet=@P5 WHERE HASTATC=@P6",bgl.baglanti());
            komut2.Parameters.AddWithValue("@P1", txtAd.Text);
            komut2.Parameters.AddWithValue("@P2", txtSoyad.Text);
            komut2.Parameters.AddWithValue("@P3", msktxtTelefon.Text);
            komut2.Parameters.AddWithValue("@P4", txtSifre.Text);
            komut2.Parameters.AddWithValue("@P5", cmbCinsiyet.Text);
            komut2.Parameters.AddWithValue("@p6", msktxtTC.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kişisel bilgileriniz başarıyla güncellenmiştir.","Güncelleme işlemi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
