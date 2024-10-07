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
using System.Runtime.Remoting.Contexts;

namespace TavukÇiftliğiSistemi
{
    public partial class Tavuklar : Form
    {
        public Tavuklar()
        {
            InitializeComponent();
            TabloDoldur();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MELİH\Documents\TaÇiYa.mdf;Integrated Security=True;Connect Timeout=30");

        private void TabloDoldur()
        {
            conn.Open();
            string komut = "select * from TavukTbl";
            SqlDataAdapter adt = new SqlDataAdapter(komut, conn);
            SqlCommandBuilder cmd = new SqlCommandBuilder(adt);
            var ds = new DataSet();
            adt.Fill(ds);
            TavukDGV.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void Temizle()
        {
            TavukAdTb.Text = "";
            DogumTarihTb.Text = "";
            CinsTb.Text = "";
            KaldigiYerTb.Text = "";
            OtlakTb.Text = "";
            anahtar = 0;
        }

        private void label10_Click(object sender, EventArgs e)
        {
            YumurtaÜretimi sm = new YumurtaÜretimi();
            sm.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Sağlık sm = new Sağlık();
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

        int Yas = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            if (TavukAdTb.Text == "" || YasTb.Text == "" || CinsTb.Text == "" || KaldigiYerTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else if(Yas < 0)
            {
                MessageBox.Show("Gelecekten Mi ?");
            }
            else
            {
                try
                {
                    conn.Open();
                    // Sütun adlarını belirtelim ve parametreli sorgu kullanarak kodu düzenleyelim
                    string Sorgu = "insert into TavukTbl (TavukAd, DogumTarih, Yas, Cins, KaldigiYer, Otlak) values (@TavukAd, @DogumTarih, @Yas, @Cins, @KaldigiYer, @Otlak)";
                    SqlCommand cmd = new SqlCommand(Sorgu, conn);

                    // Parametreleri ekleyin
                    cmd.Parameters.AddWithValue("@TavukAd", TavukAdTb.Text);
                    cmd.Parameters.AddWithValue("@DogumTarih", DogumTarihTb.Value);
                    cmd.Parameters.AddWithValue("@Yas", Convert.ToInt32(YasTb.Text));
                    cmd.Parameters.AddWithValue("@Cins", CinsTb.Text);
                    cmd.Parameters.AddWithValue("@KaldigiYer", KaldigiYerTb.Text);
                    cmd.Parameters.AddWithValue("@Otlak", OtlakTb.Text);

                    // Sorguyu çalıştırın
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tavuk Başarıyla Kaydedildi");
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
        
        private void button2_Click(object sender, EventArgs e)
        {
            if (TavukAdTb.Text == "" || YasTb.Text == "" || CinsTb.Text == "" || KaldigiYerTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else if (Yas < 0)
            {
                MessageBox.Show("Yanlış Tarih");
            }
            else
            {
                try
                {
                    conn.Open();
                    // Sorguyu parametreli hale getiriyoruz
                    string Sorgu = "update TavukTbl set TavukAd = @TavukAd, DogumTarih = @DogumTarih, Yas = @Yas, Cins = @Cins, KaldigiYer = @KaldigiYer, Otlak = @Otlak where TavukId = @TavukId";
                    SqlCommand cmd = new SqlCommand(Sorgu, conn);

                    // Parametreleri ekleyin
                    cmd.Parameters.AddWithValue("@TavukAd", TavukAdTb.Text);
                    cmd.Parameters.AddWithValue("@DogumTarih", DogumTarihTb.Value);
                    cmd.Parameters.AddWithValue("@Yas", Convert.ToInt32(YasTb.Text));
                    cmd.Parameters.AddWithValue("@Cins", CinsTb.Text);
                    cmd.Parameters.AddWithValue("@KaldigiYer", KaldigiYerTb.Text);
                    cmd.Parameters.AddWithValue("@Otlak", OtlakTb.Text);
                    cmd.Parameters.AddWithValue("@TavukId", anahtar); // TavukId'yi buradan alıyoruz

                    // Sorguyu çalıştırın
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tavuk Bilgileri Değiştirildi");
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
                MessageBox.Show("Silinecek Tavuk'u Seçiniz");
            }           
            else
            {
                try
                {
                    conn.Open();
                    string Sorgu = "delete from TavukTbl where TavukId="+ anahtar +";";
                    SqlCommand cmd = new SqlCommand(Sorgu, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tavuk Başarıyla Silindi");
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

        int anahtar = 0;
        private void TavukDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            TavukDGV.CurrentRow.Selected = true;
            TavukAdTb.Text = TavukDGV.SelectedRows[0].Cells[1].Value.ToString();
            DogumTarihTb.Text = TavukDGV.SelectedRows[0].Cells[2].Value.ToString();
            YasTb.Text = TavukDGV.SelectedRows[0].Cells[3].Value.ToString();
            CinsTb.Text = TavukDGV.SelectedRows[0].Cells[4].Value.ToString();
            KaldigiYerTb.Text = TavukDGV.SelectedRows[0].Cells[5].Value.ToString();
            OtlakTb.Text = TavukDGV.SelectedRows[0].Cells[6].Value.ToString();
            if(TavukAdTb.Text == "")
            {
                anahtar = 0;
                Yas = 0;
            }
            else
            {
                anahtar = Convert.ToInt32(TavukDGV.SelectedRows[0].Cells[0].Value.ToString());
                Yas = Convert.ToInt32(TavukDGV.SelectedRows[0].Cells[3].Value.ToString());
            }
        }

        private void DogumTarihTb_ValueChanged(object sender, EventArgs e)
        {
            Yas = Convert.ToInt32((DateTime.Today.Date-DogumTarihTb.Value.Date).Days)/365;
            if (Yas < 0)
            {
                YasTb.Text = "Not Found";
            }
            else 
            {
                YasTb.Text = "" + Yas; 
            }
            
        }

        private void Filtre1_TextChanged(object sender, EventArgs e)
        {
            TavukDGV.DataSource = null;
            conn.Open();
            SqlDataAdapter adt = new SqlDataAdapter("select * from TavukTbl where TavukAd like '" + Filtre1.Text + "%'", conn);
            var dt = new DataTable();
            adt.Fill(dt);
            TavukDGV.DataSource = dt;
            conn.Close();
            if (Filtre1.Text == "")
            {
                TavukDGV.DataSource = null;
                TabloDoldur();
            }
        }

    }
}
