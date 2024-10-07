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
    public partial class YumurtaSatış : Form
    {
        public YumurtaSatış()
        {
            InitializeComponent();
            EmpId_doldur();
            TabloDoldur();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MELİH\Documents\TaÇiYa.mdf;Integrated Security=True;Connect Timeout=30");

        private void EmpId_doldur()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select EmpId from CalisanTbl", conn);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpId", typeof(int));
            dt.Load(dr);
            EmpIdTb.ValueMember = "EmpId";
            EmpIdTb.DataSource = dt;
            conn.Close();
        }
        private void TabloDoldur()
        {
            conn.Open();
            string komut = "select * from YumurtaSatTbl";
            SqlDataAdapter adt = new SqlDataAdapter(komut, conn);
            SqlCommandBuilder cmd = new SqlCommandBuilder(adt);
            var ds = new DataSet();
            adt.Fill(ds);
            YumurtaSatDGV.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void Temizle()
        {
            EmpIdTb.Text = "";
            MusteriAdTb.Text = "";
            SatisTarihTb.Text = "";
            SatisFiyatTb.Text = "";
            MiktarTb.Text = "";
            ToplamTb.Text = "";
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

        private void label12_Click(object sender, EventArgs e)
        {
            Kuluçka sm = new Kuluçka();
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
            if (EmpIdTb.SelectedIndex == -1 || MusteriAdTb.Text == "" || SatisFiyatTb.Text == "" || MiktarTb.Text == "" || ToplamTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else if (SatisTarihTb.Value.Date > DateTime.Today.Date)
            {
                MessageBox.Show("Yanlış Tarih");
            }
            else
            {
                try
                {
                    conn.Open();
                    string Sorgu = "insert into YumurtaSatTbl (EmpId, MusteriAd, SatisTarih, SatisFiyat, Miktar, Toplam) values (@EmpId, @MusteriAd, @SatisTarih, @SatisFiyat, @Miktar, @Toplam)";
                    string Sorgu2 = "insert into GelirTbl ( GelirTarih, Tip, Miktar, EmpId) values ( @GelirTarih, @Tip, @Miktar, @EmpId)";
                    SqlCommand cmd = new SqlCommand(Sorgu, conn);
           

                    //satış
                    cmd.Parameters.AddWithValue("@EmpId", EmpIdTb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@MusteriAd", MusteriAdTb.Text);
                    cmd.Parameters.AddWithValue("@SatisTarih", SatisTarihTb.Value);
                    cmd.Parameters.AddWithValue("@SatisFiyat", Convert.ToDecimal(SatisFiyatTb.Text));
                    cmd.Parameters.AddWithValue("@Miktar", Convert.ToInt32(MiktarTb.Text));
                    cmd.Parameters.AddWithValue("@Toplam", Convert.ToDecimal(ToplamTb.Text));
                    cmd.ExecuteNonQuery();

                    
                    //gelir
                    SqlCommand cmd2 = new SqlCommand(Sorgu2, conn);                             
                    cmd2.Parameters.AddWithValue("@GelirTarih", SatisTarihTb.Value);
                    cmd2.Parameters.AddWithValue("@Tip", "Yumurta Satış");
                    cmd2.Parameters.AddWithValue("@Miktar", Convert.ToInt32(ToplamTb.Text));
                    cmd2.Parameters.AddWithValue("@EmpId", EmpIdTb.SelectedValue.ToString());
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Satışlar Başarıyla Kaydedildi");
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
            if (EmpIdTb.SelectedIndex == -1 || MusteriAdTb.Text == "" || SatisFiyatTb.Text == "" || MiktarTb.Text == "" || ToplamTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else if (SatisTarihTb.Value.Date > DateTime.Today.Date)
            {
                MessageBox.Show("Yanlış Tarih");
            }
            else
            {
                try
                {
                    conn.Open();
                    string Sorgu = "update YumurtaSatTbl set EmpId = @EmpId, MusteriAd = @MusteriAd, SatisTarih = @SatisTarih, SatisFiyat = @SatisFiyat, Miktar = @Miktar, Toplam = @Toplam where YsId = @YsId";
                    SqlCommand cmd = new SqlCommand(Sorgu, conn);
                    cmd.Parameters.AddWithValue("@EmpId", EmpIdTb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@MusteriAd", MusteriAdTb.Text);
                    cmd.Parameters.AddWithValue("@SatisTarih", SatisTarihTb.Value);
                    cmd.Parameters.AddWithValue("@SatisFiyat", Convert.ToDecimal(SatisFiyatTb.Text));
                    cmd.Parameters.AddWithValue("@Miktar", Convert.ToInt32(MiktarTb.Text));
                    cmd.Parameters.AddWithValue("@Toplam", Convert.ToDecimal(ToplamTb.Text));
                    cmd.Parameters.AddWithValue("@YsId", anahtar);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Satış Bilgileri Değiştirildi");
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
                MessageBox.Show("Silinecek Satış'ı Seçiniz");
            }
            else
            {
                try
                {
                    conn.Open();
                    string Sorgu = "delete from YumurtaSatTbl where YsId=" + anahtar + ";";
                    SqlCommand cmd = new SqlCommand(Sorgu, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Satış Başarıyla Silindi");
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

        private void YumurtaSatDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            YumurtaSatDGV.CurrentRow.Selected = true;
            EmpIdTb.SelectedValue = YumurtaSatDGV.SelectedRows[0].Cells[1].Value.ToString();
            MusteriAdTb.Text = YumurtaSatDGV.SelectedRows[0].Cells[2].Value.ToString();
            SatisTarihTb.Text = YumurtaSatDGV.SelectedRows[0].Cells[3].Value.ToString();
            SatisFiyatTb.Text = YumurtaSatDGV.SelectedRows[0].Cells[4].Value.ToString();
            MiktarTb.Text = YumurtaSatDGV.SelectedRows[0].Cells[5].Value.ToString();
            ToplamTb.Text = YumurtaSatDGV.SelectedRows[0].Cells[6].Value.ToString();
            if (MusteriAdTb.Text == "")
            {
                anahtar = 0;
            }
            else
            {
                anahtar = Convert.ToInt32(YumurtaSatDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void MiktarTb_TextChanged(object sender, EventArgs e)
        {
            if(SatisFiyatTb.Text != "" && MiktarTb.Text != "")
            {
                int toplam = Convert.ToInt32(SatisFiyatTb.Text) * Convert.ToInt32(MiktarTb.Text);
                ToplamTb.Text = "" + toplam;
            }
            else {}
        }

        private void SatisFiyatTb_TextChanged(object sender, EventArgs e)
        {
            if (MiktarTb.Text != "" && SatisFiyatTb.Text != "")
            {
                int toplam = Convert.ToInt32(SatisFiyatTb.Text) * Convert.ToInt32(MiktarTb.Text);
                ToplamTb.Text = "" + toplam;
            }
            else {}
        }

    }
}
