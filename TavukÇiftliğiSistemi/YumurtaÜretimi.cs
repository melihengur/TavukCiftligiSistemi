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
    public partial class YumurtaÜretimi : Form
    {
        public YumurtaÜretimi()
        {
            InitializeComponent();
            TavukId_doldur();
            TabloDoldur();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MELİH\Documents\TaÇiYa.mdf;Integrated Security=True;Connect Timeout=30");
        private void TavukId_doldur()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select TavukId from TavukTbl",conn);
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
            string komut = "select * from YumurtaUretTbl";
            SqlDataAdapter adt = new SqlDataAdapter(komut, conn);
            SqlCommandBuilder cmd = new SqlCommandBuilder(adt);
            var ds = new DataSet();
            adt.Fill(ds);
            YumurtaUretDGV.DataSource = ds.Tables[0];
            conn.Close();
        }

        private void Temizle()
        {
            TavukIdTb.Text = "";
            TavukAdTb.Text = "";
            UretimTarihTb.Text = "";
            ToplananYumurtaTb.Text = "";
            DepolanmaYeriTb.Text = "";
            anahtar = 0;
        }

        private void TavukAdTb_doldur()
        {
            conn.Open();
            string komut = "select * from TavukTbl where TavukId="+TavukIdTb.SelectedValue.ToString()+"";
            SqlCommand cmd = new SqlCommand(komut,conn);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (TavukIdTb.SelectedIndex == -1 || TavukAdTb.Text == "" || ToplananYumurtaTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi");
            }    
            else if(UretimTarihTb.Value.Date > DateTime.Today.Date)
            {
                MessageBox.Show("Yanlış Tarih");
            }
            else
            {
                try
                {
                    conn.Open();
                    string Sorgu = "insert into YumurtaUretTbl (TavukId, TavukAd, UretimTarih, ToplananYumurta, DepolanmaYeri) values (@TavukId, @TavukAd, @UretimTarih, @ToplananYumurta, @DepolanmaYeri)";
                    SqlCommand cmd = new SqlCommand(Sorgu, conn);

                    // Parametreleri ekleyin
                    cmd.Parameters.AddWithValue("@TavukId", TavukIdTb.SelectedValue);
                    cmd.Parameters.AddWithValue("@TavukAd", TavukAdTb.Text);
                    cmd.Parameters.AddWithValue("@UretimTarih", UretimTarihTb.Value);
                    cmd.Parameters.AddWithValue("@ToplananYumurta", Convert.ToInt32(ToplananYumurtaTb.Text));
                    cmd.Parameters.AddWithValue("@DepolanmaYeri", DepolanmaYeriTb.Text);

                    // Sorguyu çalıştırın
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Üretimler Başarıyla Kaydedildi");
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
            if (TavukIdTb.SelectedIndex == -1 || TavukAdTb.Text == "" || ToplananYumurtaTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else if (UretimTarihTb.Value.Date > DateTime.Today.Date)
            {
                MessageBox.Show("Yanlış Tarih");
            }
            else
            {
                try
                {
                    conn.Open();
                    string Sorgu = "update YumurtaUretTbl set TavukAd = @TavukAd, UretimTarih = @UretimTarih, ToplananYumurta = @ToplananYumurta, DepolanmaYeri = @DepolanmaYeri where YuId = @YuId";
                    SqlCommand cmd = new SqlCommand(Sorgu, conn);

                    // Parametreleri ekleyin
                    cmd.Parameters.AddWithValue("@TavukAd", TavukAdTb.Text);
                    cmd.Parameters.AddWithValue("@UretimTarih", UretimTarihTb.Value);
                    cmd.Parameters.AddWithValue("@ToplananYumurta", Convert.ToInt32(ToplananYumurtaTb.Text));
                    cmd.Parameters.AddWithValue("@DepolanmaYeri", DepolanmaYeriTb.Text);
                    cmd.Parameters.AddWithValue("@YuId", anahtar);

                    // Sorguyu çalıştırın
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Üretim Bilgileri Değiştirildi");
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
                MessageBox.Show("Silinecek Üretim'i Seçiniz");
            }
            else
            {
                try
                {
                    conn.Open();
                    string Sorgu = "delete from YumurtaUretTbl where YuId=" + anahtar + ";";
                    SqlCommand cmd = new SqlCommand(Sorgu, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Üretim Başarıyla Silindi");
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

        int anahtar;
        private void YumurtaUretDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            YumurtaUretDGV.CurrentRow.Selected = true;
            TavukIdTb.SelectedValue = YumurtaUretDGV.SelectedRows[0].Cells[1].Value.ToString();
            TavukAdTb.Text = YumurtaUretDGV.SelectedRows[0].Cells[2].Value.ToString();
            UretimTarihTb.Text = YumurtaUretDGV.SelectedRows[0].Cells[3].Value.ToString();
            ToplananYumurtaTb.Text = YumurtaUretDGV.SelectedRows[0].Cells[4].Value.ToString();
            DepolanmaYeriTb.Text = YumurtaUretDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (TavukAdTb.Text == "")
            {
                anahtar = 0;
            }
            else
            {
                anahtar = Convert.ToInt32(YumurtaUretDGV.SelectedRows[0].Cells[0].Value.ToString());               
            }
        }

    }
}
