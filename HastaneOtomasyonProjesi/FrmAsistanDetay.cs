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
    public partial class FrmAsistanDetay : Form
    {
        public FrmAsistanDetay()
        {
            InitializeComponent();
        }
        public string tckimlik;
        SqlBaglatisiClass bgl = new SqlBaglatisiClass();
        private void FrmAsistanDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = tckimlik;

            //AD SOYAD
            SqlCommand komut = new SqlCommand("Select AsistanAdSoyad From Tbl_Asistan Where AsistanTC=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",lblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();

            //Branşları Datagride aktarma
            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select BRANSAD FROM TBL_BRANSLAR", bgl.baglanti());
            da.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //Doktorları Datagride aktarma 
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select (DoktorAd + ' ' + DoktorSoyad) as 'Doktorlar',DoktorBrans From Tbl_Doktorlar", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            //Branşları comboboxa aktarma
            SqlCommand komut3 = new SqlCommand("Select BransAd From Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                cmbBrans.Items.Add(dr3[0]);
            }
            bgl.baglanti().Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            // Yeni Randevu kaydetme 
            SqlCommand komutkaydet = new SqlCommand("insert into Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktorAd) values (@p1,@p2,@p3,@p4)", bgl.baglanti());
            komutkaydet.Parameters.AddWithValue("@p1", mskTarih.Text);
            komutkaydet.Parameters.AddWithValue("@p2", mskSaat.Text);
            komutkaydet.Parameters.AddWithValue("@p3", cmbBrans.Text);
            komutkaydet.Parameters.AddWithValue("@p4", cmbDoktorAd.Text);
            komutkaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Yeni randevunuz listeye alınmıştır\nLütfen randevu taarihinde hastanede olamaya özen gösteriniz.\nSağlıcakla kalın, sağlıklı kalın...","Yeni randevu",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Branşlara göre comboboxa doktor getirme
            cmbDoktorAd.Items.Clear();
            SqlCommand komut4 = new SqlCommand("Select DoktorAd,DoktorSoyad From Tbl_Doktorlar where DoktorBrans=@p1", bgl.baglanti());
            komut4.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                cmbDoktorAd.Items.Add(dr4[0] + " " + dr4[1]);
            }
            bgl.baglanti().Close();
        }

        private void btnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            //Duyuru yayınlama paneli
            SqlCommand komut = new SqlCommand("insert into Tbl_Duyurular (Duyuru) values (@p1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",rchDuyuru.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyurunuz başarılı bir şekilde doktor arkadaşlarla paylaşılmıştır.","Duyuru yayını",MessageBoxButtons.OK,MessageBoxIcon.Information);
            rchDuyuru.Text = " ";
        }

        private void btnDoktorlist_Click(object sender, EventArgs e)
        {
            FrmDoktorListesi fr = new FrmDoktorListesi();
            fr.Show();
        }

        private void btnBransList_Click(object sender, EventArgs e)
        {
            FrmBranslarListesi fr = new FrmBranslarListesi();
            fr.Show();
        }

        private void btnRandevuList_Click(object sender, EventArgs e)
        {
            FrmRandevuListelesi fr = new FrmRandevuListelesi();
            fr.Show();
        }

        private void btnDuyuruListesi_Click(object sender, EventArgs e)
        {
            FrmDuyurular fr = new FrmDuyurular();
            fr.Show();
        }
    }
}
