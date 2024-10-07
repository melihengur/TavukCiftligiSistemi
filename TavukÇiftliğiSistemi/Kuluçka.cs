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
    public partial class Kuluçka : Form
    {
        public Kuluçka()
        {
            InitializeComponent();
            TabloDoldur();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MELİH\Documents\TaÇiYa.mdf;Integrated Security=True;Connect Timeout=30");

        private void TabloDoldur()
        {
            conn.Open();
            string komut = "select * from KuluckaTbl";
            SqlDataAdapter adt = new SqlDataAdapter(komut, conn);
            SqlCommandBuilder cmd = new SqlCommandBuilder(adt);
            var ds = new DataSet();
            adt.Fill(ds);
            KuluckaDGV.DataSource = ds.Tables[0];
            conn.Close();
        }

        private void Temizle()
        {
            KulBasTarihTb.Text = "";
            KulYumSayTb.Text = "";
            KulYeriTb.Text = "";
            KulBitTarihTb.Text = "";
            CatYumSayTb.Text = "";
            KulMakTb.Text = "";
            anahtar = 0;
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

        private void label11_Click(object sender, EventArgs e)
        {
            Sağlık sm = new Sağlık();
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
            if (KulYumSayTb.Text == "" || KulYeriTb.Text == "" || CatYumSayTb.Text == "" || KulMakTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else if (KulBasTarihTb.Value.Date > DateTime.Today.Date)
            {
                MessageBox.Show("Kuluçkaya Başlama Tarihi Yanlış");
            }
            else if (KulBitTarihTb.Value.Date <= KulBasTarihTb.Value.Date)
            {
                MessageBox.Show("Kuluçka Süresi Negatif Olamaz");
            }
            else if(Convert.ToInt16(KulYumSayTb.Text) < Convert.ToInt16(CatYumSayTb.Text))
            {
                MessageBox.Show("Çatlayan Yumurtalar , Yumurta Sayısından Fazla Olamaz");
            }
            else
            {
                try
                {
                    conn.Open();
                    string Sorgu = "insert into KuluckaTbl (KulBasTarih, KulYumSay, KulYeri, KulBitTarih, CatYumSay, KulMak) values (@KulBasTarih, @KulYumSay, @KulYeri, @KulBitTarih, @CatYumSay, @KulMak)";
                    SqlCommand cmd = new SqlCommand(Sorgu, conn);
                    cmd.Parameters.AddWithValue("@KulBasTarih", KulBasTarihTb.Value);
                    cmd.Parameters.AddWithValue("@KulYumSay", KulYumSayTb.Text);
                    cmd.Parameters.AddWithValue("@KulYeri", KulYeriTb.Text);
                    cmd.Parameters.AddWithValue("@KulBitTarih", KulBitTarihTb.Value);
                    cmd.Parameters.AddWithValue("@CatYumSay", CatYumSayTb.Text);
                    cmd.Parameters.AddWithValue("@KulMak", KulMakTb.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Kuluçka Başarıyla Kaydedildi");
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (KulYumSayTb.Text == "" || KulYeriTb.Text == "" || CatYumSayTb.Text == "" || KulMakTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else if (KulBasTarihTb.Value.Date > DateTime.Today.Date)
            {
                MessageBox.Show("Kuluçkaya Başlama Tarihi Yanlış");
            }
            else if (KulBitTarihTb.Value.Date < KulBasTarihTb.Value.Date)
            {
                MessageBox.Show("Kuluçka Süresi Negatif Olamaz");
            }
            else if (Convert.ToInt16(KulYumSayTb.Text) < Convert.ToInt16(CatYumSayTb.Text))
            {
                MessageBox.Show("Çatlayan Yumurtalar , Yumurta Sayısından Fazla Olamaz");
            }
            else
            {
                try
                {
                    conn.Open();
                    string Sorgu = "update KuluckaTbl set KulBasTarih=@KulBasTarih, KulYumSay=@KulYumSay, KulYeri=@KulYeri, KulBitTarih=@KulBitTarih, CatYumSay=@CatYumSay, KulMak=@KulMak where KulId=@KulId";
                    SqlCommand cmd = new SqlCommand(Sorgu, conn);
                    cmd.Parameters.AddWithValue("@KulBasTarih", KulBasTarihTb.Value);
                    cmd.Parameters.AddWithValue("@KulYumSay", KulYumSayTb.Text);
                    cmd.Parameters.AddWithValue("@KulYeri", KulYeriTb.Text);
                    cmd.Parameters.AddWithValue("@KulBitTarih", KulBitTarihTb.Value);
                    cmd.Parameters.AddWithValue("@CatYumSay", CatYumSayTb.Text);
                    cmd.Parameters.AddWithValue("@KulMak", KulMakTb.Text);
                    cmd.Parameters.AddWithValue("@KulId", anahtar);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Kuluçka Bilgileri Değiştirildi");
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
                    string Sorgu = "delete from KuluckaTbl where KulId=" + anahtar + ";";
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

        int anahtar = 0;

        private void KuluckaDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            KuluckaDGV.CurrentRow.Selected = true;
            KulBasTarihTb.Text = KuluckaDGV.SelectedRows[0].Cells[1].Value.ToString();
            KulYumSayTb.Text = KuluckaDGV.SelectedRows[0].Cells[2].Value.ToString();
            KulYeriTb.Text = KuluckaDGV.SelectedRows[0].Cells[3].Value.ToString();
            KulBitTarihTb.Text = KuluckaDGV.SelectedRows[0].Cells[4].Value.ToString();
            CatYumSayTb.Text = KuluckaDGV.SelectedRows[0].Cells[5].Value.ToString();
            KulMakTb.Text = KuluckaDGV.SelectedRows[0].Cells[6].Value.ToString();
            if (KulBasTarihTb.Text == "")
            {
                anahtar = 0;
            }
            else
            {
                anahtar = Convert.ToInt32(KuluckaDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

    }
}
