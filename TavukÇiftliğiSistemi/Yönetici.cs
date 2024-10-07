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
    public partial class Yönetici : Form
    {
        public Yönetici()
        {
            InitializeComponent();
            TabloDoldur();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MELİH\Documents\TaÇiYa.mdf;Integrated Security=True;Connect Timeout=30");

        private void TabloDoldur()
        {
            conn.Open();
            string komut = "select * from CalisanTbl";
            SqlDataAdapter adt = new SqlDataAdapter(komut, conn);
            SqlCommandBuilder cmd = new SqlCommandBuilder(adt);
            var ds = new DataSet();
            adt.Fill(ds);
            CalisanDGV.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void Temizle()
        {
            EmpAdTb.Text = "";
            EmpDogTarihTb.Text = "";
            CinsiyetTb.Text = "";
            TelefonTb.Text = "";
            AdresTb.Text = "";
            SifreTb.Text = "";
            anahtar = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (EmpAdTb.Text == "" || CinsiyetTb.Text == "" || TelefonTb.Text == "" || AdresTb.Text == "" || SifreTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else if (EmpDogTarihTb.Value.Date > DateTime.Today.Date)
            {
                MessageBox.Show("Yanlış Tarih");
            }
            else
            {
                try
                {
                    conn.Open();
                    // Sütun adlarını belirtelim
                    string Sorgu = "insert into CalisanTbl (EmpAd, EmpDogTarih, Cinsiyet, Telefon, Adres, Sifre) values (@EmpAd, @EmpDogTarih, @Cinsiyet, @Telefon, @Adres, @Sifre)";
                    SqlCommand cmd = new SqlCommand(Sorgu, conn);

                    // Parametreleri ekleyin
                    cmd.Parameters.AddWithValue("@EmpAd", EmpAdTb.Text);
                    cmd.Parameters.AddWithValue("@EmpDogTarih", EmpDogTarihTb.Value);
                    cmd.Parameters.AddWithValue("@Cinsiyet", CinsiyetTb.Text);
                    cmd.Parameters.AddWithValue("@Telefon", TelefonTb.Text);
                    cmd.Parameters.AddWithValue("@Adres", AdresTb.Text);
                    cmd.Parameters.AddWithValue("@Sifre", SifreTb.Text);

                    // Sorguyu çalıştırın
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Çalışan Kaydedildi");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message); // Hata mesajını göster
                }
                finally
                {         
                    conn.Close();   // Bağlantıyı kapatın
                    TabloDoldur(); // Tabloyu yeniden doldurun
                    Temizle(); // Formu temizleyin
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (EmpAdTb.Text == "" || CinsiyetTb.Text == "" || TelefonTb.Text == "" || AdresTb.Text == "" || SifreTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else if (EmpDogTarihTb.Value.Date > DateTime.Today.Date)
            {
                MessageBox.Show("Yanlış Tarih");
            }
            else
            {
                try
                {
                    conn.Open();
                    // Sorguyu parametreli hale getiriyoruz
                    string Sorgu = "update CalisanTbl set EmpAd = @EmpAd, EmpDogTarih = @EmpDogTarih, Cinsiyet = @Cinsiyet, Telefon = @Telefon, Adres = @Adres, Sifre = @Sifre where EmpId = @EmpId";
                    SqlCommand cmd = new SqlCommand(Sorgu, conn);

                    // Parametreleri ekleyin
                    cmd.Parameters.AddWithValue("@EmpAd", EmpAdTb.Text);
                    cmd.Parameters.AddWithValue("@EmpDogTarih", EmpDogTarihTb.Value);
                    cmd.Parameters.AddWithValue("@Cinsiyet", CinsiyetTb.Text);
                    cmd.Parameters.AddWithValue("@Telefon", TelefonTb.Text);
                    cmd.Parameters.AddWithValue("@Adres", AdresTb.Text);
                    cmd.Parameters.AddWithValue("@Sifre", SifreTb.Text);
                    cmd.Parameters.AddWithValue("@EmpId", anahtar); // EmpId değerini buradan alıyoruz

                    // Sorguyu çalıştırın
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Çalışan Bilgileri Değiştirildi");
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
                MessageBox.Show("Silinecek Çalışan'ı Seçiniz");
            }
            else
            {
                try
                {
                    conn.Open();
                    string Sorgu = "delete from CalisanTbl where EmpId=" + anahtar + ";";
                    SqlCommand cmd = new SqlCommand(Sorgu, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Çalışan Silindi");
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
        private void CalisanDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CalisanDGV.CurrentRow.Selected = true;
            EmpAdTb.Text = CalisanDGV.SelectedRows[0].Cells[1].Value.ToString();
            EmpDogTarihTb.Text = CalisanDGV.SelectedRows[0].Cells[2].Value.ToString();
            CinsiyetTb.Text = CalisanDGV.SelectedRows[0].Cells[3].Value.ToString();
            TelefonTb.Text = CalisanDGV.SelectedRows[0].Cells[4].Value.ToString();
            AdresTb.Text = CalisanDGV.SelectedRows[0].Cells[5].Value.ToString();
            SifreTb.Text = CalisanDGV.SelectedRows[0].Cells[6].Value.ToString();
            if (EmpAdTb.Text == "")
            {
                anahtar = 0;
            }
            else
            {
                anahtar = Convert.ToInt32(CalisanDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

    }
}
