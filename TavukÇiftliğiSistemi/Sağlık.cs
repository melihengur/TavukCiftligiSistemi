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

namespace TavukÇiftliğiSistemi
{
    public partial class Sağlık : Form
    {
        public Sağlık()
        {
            InitializeComponent();
            TavukId_doldur();
            TabloDoldur();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MELİH\Documents\TaÇiYa.mdf;Integrated Security=True;Connect Timeout=30");

        private void TavukId_doldur()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select TavukId from TavukTbl", conn);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TavukId", typeof(int));
            dt.Load(dr);
            TavukIdTb.ValueMember = "TavukId";
            TavukIdTb.DataSource = dt;
            conn.Close();
        }
        private void TabloDoldur()
        {
            conn.Open();
            string komut = "select * from SaglikTbl";
            SqlDataAdapter adt = new SqlDataAdapter(komut, conn);
            SqlCommandBuilder cmd = new SqlCommandBuilder(adt);
            var ds = new DataSet();
            adt.Fill(ds);
            SaglikDGV.DataSource = ds.Tables[0];
            conn.Close();
        }

        private void Temizle()
        {
            TavukIdTb.Text = "";
            TavukAdTb.Text = "";
            RaporTarihTb.Text = "";
            TeshisTb.Text = "";
            TedaviTb.Text = "";
            TedaviUcretTb.Text = "";
            anahtar = 0;
        }

        private void TavukAdTb_doldur()
        {
            conn.Open();
            string komut = "select * from TavukTbl where TavukId=" + TavukIdTb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(komut, conn);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                TavukAdTb.Text = dr["TavukAd"].ToString();
            }
            conn.Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Tavuklar sm = new Tavuklar();
            sm.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            YumurtaÜretimi sm = new YumurtaÜretimi();
            sm.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Kuluçka sm = new Kuluçka();
            sm.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            YumurtaSatış sm = new YumurtaSatış();
            sm.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Finans sm = new Finans();
            sm.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Göstergeler sm = new Göstergeler();
            sm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TavukIdTb.SelectedIndex == -1 || TavukAdTb.Text == "" || TeshisTb.Text == "" || TedaviTb.Text == "" || TedaviUcretTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else if (RaporTarihTb.Value.Date > DateTime.Today.Date)
            {
                MessageBox.Show("Yanlış Tarih");
            }
            else
            {
                try
                {
                    conn.Open();

                    // İlk Sorgu: Sağlık Raporu Ekleme
                    string Sorgu = "insert into SaglikTbl (TavukId, TavukAd, RaporTarih, Teshis, Tedavi, TedaviUcret) values(@TavukId, @TavukAd, @RaporTarih, @Teshis, @Tedavi, @TedaviUcret)";
                    SqlCommand cmd = new SqlCommand(Sorgu, conn);

                    cmd.Parameters.AddWithValue("@TavukId", TavukIdTb.Text);
                    cmd.Parameters.AddWithValue("@TavukAd", TavukAdTb.Text);
                    cmd.Parameters.AddWithValue("@RaporTarih", RaporTarihTb.Value);
                    cmd.Parameters.AddWithValue("@Teshis", TeshisTb.Text);
                    cmd.Parameters.AddWithValue("@Tedavi", TedaviTb.Text);
                    cmd.Parameters.AddWithValue("@TedaviUcret", Convert.ToDecimal(TedaviUcretTb.Text));

                    cmd.ExecuteNonQuery();

                    // İkinci Sorgu: Gider Ekleme
                    string Sorgu2 = "insert into GiderTbl (GiderTarih, GiderCesidi, Tutar ) values(@GiderTarih, @GiderCesidi, @Tutar)";
                    SqlCommand cmd2 = new SqlCommand(Sorgu2, conn);

                    cmd2.Parameters.AddWithValue("@GiderTarih", RaporTarihTb.Value);
                    cmd2.Parameters.AddWithValue("@GiderCesidi", "Tedavi Ücret");
                    cmd2.Parameters.AddWithValue("@Tutar", Convert.ToDecimal(TedaviUcretTb.Text)); // Tutarlılık için decimal olarak değiştirdim

                    cmd2.ExecuteNonQuery();

                    MessageBox.Show("Sağlık Raporu Kaydedildi");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message); // Hata mesajını göster
                }
                finally
                {
                    conn.Close(); // Bağlantıyı kapat
                    TabloDoldur(); // Tabloyu yeniden doldur
                    Temizle(); // Formu temizle
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (TavukIdTb.SelectedIndex == -1 || TavukAdTb.Text == "" || TeshisTb.Text == "" || TedaviTb.Text == "" || TedaviUcretTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else if (RaporTarihTb.Value.Date > DateTime.Today.Date)
            {
                MessageBox.Show("Yanlış Tarih");
            }
            else
            {
                try
                {
                    conn.Open();

                    // Sorguyu parametreli hale getiriyoruz
                    string Sorgu = "update SaglikTbl set TavukAd = @TavukAd, RaporTarih = @RaporTarih, Teshis = @Teshis, Tedavi = @Tedavi, TedaviUcret = @TedaviUcret where SaglikId = @SaglikId";
                    SqlCommand cmd = new SqlCommand(Sorgu, conn);

                    // Parametreleri ekleyin
                    cmd.Parameters.AddWithValue("@TavukAd", TavukAdTb.Text);
                    cmd.Parameters.AddWithValue("@RaporTarih", RaporTarihTb.Value);
                    cmd.Parameters.AddWithValue("@Teshis", TeshisTb.Text);
                    cmd.Parameters.AddWithValue("@Tedavi", TedaviTb.Text);
                    cmd.Parameters.AddWithValue("@TedaviUcret", Convert.ToDecimal(TedaviUcretTb.Text));
                    cmd.Parameters.AddWithValue("@SaglikId", anahtar); // SaglikId'yi buradan alıyoruz

                    // Sorguyu çalıştırın
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sağlık Bilgileri Değiştirildi");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message); // Hata mesajını göster
                }
                finally
                {
                    // Bağlantıyı kapatın
                    conn.Close();
                    TabloDoldur(); // Tabloyu yeniden doldurun
                    Temizle(); // Formu temizleyin
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (anahtar == 0)
            {
                MessageBox.Show("Silinecek Rapor'u Seçiniz");
            }
            else
            {
                try
                {
                    conn.Open();
                    string Sorgu = "delete from SaglikTbl where SaglikId=" + anahtar + ";";
                    SqlCommand cmd = new SqlCommand(Sorgu, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Rapor Başarıyla Silindi");
                    conn.Close();
                    TabloDoldur();
                    Temizle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Kayıt sm = new Kayıt();
            sm.Show();
            this.Hide();
        }

        private void TavukIdTb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            TavukAdTb_doldur();
        }

        int anahtar = 0;

        private void SaglikDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SaglikDGV.CurrentRow.Selected = true;
            TavukIdTb.SelectedValue = SaglikDGV.SelectedRows[0].Cells[1].Value.ToString();
            TavukAdTb.Text = SaglikDGV.SelectedRows[0].Cells[2].Value.ToString();
            RaporTarihTb.Text = SaglikDGV.SelectedRows[0].Cells[3].Value.ToString();
            TeshisTb.Text = SaglikDGV.SelectedRows[0].Cells[4].Value.ToString();
            TedaviTb.Text = SaglikDGV.SelectedRows[0].Cells[5].Value.ToString();
            TedaviUcretTb.Text = SaglikDGV.SelectedRows[0].Cells[6].Value.ToString();
            if (TavukAdTb.Text == "")
            {
                anahtar = 0;
            }
            else
            {
                anahtar = Convert.ToInt32(SaglikDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

    }
}
