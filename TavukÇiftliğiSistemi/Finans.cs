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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TavukÇiftliğiSistemi
{
    public partial class Finans : Form
    {
        public Finans()
        {
            InitializeComponent();
            TabloDoldur();
            EmpId_doldur();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MELİH\Documents\TaÇiYa.mdf;Integrated Security=True;Connect Timeout=30");

        private void TabloDoldur()
        {
            conn.Open();
            string komut = "select * from GiderTbl";
            string komut2 = "select * from GelirTbl";
            SqlDataAdapter adt = new SqlDataAdapter(komut, conn);
            SqlDataAdapter adt2 = new SqlDataAdapter(komut2, conn);
            SqlCommandBuilder cmd = new SqlCommandBuilder(adt);
            SqlCommandBuilder cmd2 = new SqlCommandBuilder(adt2);
            var ds = new DataSet();
            var ds2 = new DataSet();
            adt.Fill(ds);
            adt2.Fill(ds2);
            GiderDGV.DataSource = ds.Tables[0];
            GelirDGV.DataSource = ds2.Tables[0];
            conn.Close();
        }

        private void Gider_Tbl_Temizle()
        {
            GiderTarihTb.Text = "";
            GiderCesidiTb.Text = "";
            TutarTb.Text = "";
        }

        private void Gelir_Tbl_Temizle()
        {
            GelirTarihTb.Text = "";
            TipTb.Text = "";
            MiktarTb.Text = "";
        }

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

        private void label13_Click(object sender, EventArgs e)
        {
            YumurtaSatış sm = new YumurtaSatış();
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
            if ( GiderCesidiTb.Text == "" || TutarTb.Text == "" )
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else if (GiderTarihTb.Value.Date > DateTime.Today.Date)
            {
                MessageBox.Show("Yanlış Tarih");
            }
            else
            {
                try
                {
                    conn.Open();
                    string Sorgu = "insert into GiderTbl (GiderTarih, GiderCesidi, Tutar, EmpId ) values(@GiderTarih, @GiderCesidi, @Tutar, @EmpId)";
                    SqlCommand cmd = new SqlCommand(Sorgu, conn);
                    cmd.Parameters.AddWithValue("@GiderTarih", GiderTarihTb.Value);
                    cmd.Parameters.AddWithValue("@GiderCesidi", GiderCesidiTb.Text);
                    cmd.Parameters.AddWithValue("@Tutar", Convert.ToInt32(TutarTb.Text));
                    cmd.Parameters.AddWithValue("@EmpId", EmpIdTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Gider Kaydedildi");
                    conn.Close();
                    TabloDoldur();
                    Gider_Tbl_Temizle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (GiderCesidiTb.Text == "" || TutarTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else if (GiderTarihTb.Value.Date > DateTime.Today.Date)
            {
                MessageBox.Show("Yanlış Tarih");
            }
            else
            {
                try
                {
                    conn.Open();
                    string Sorgu = "update GiderTbl set GiderTarih=@GiderTarih,GiderCesidi = @GiderCesidi, Tutar = @Tutar, EmpId = @EmpId where GiderId = @GiderId ";
                    SqlCommand cmd = new SqlCommand(Sorgu, conn);
                    cmd.Parameters.AddWithValue("@GiderId", anahtar1);
                    cmd.Parameters.AddWithValue("@GiderTarih", GiderTarihTb.Value);
                    cmd.Parameters.AddWithValue("@GiderCesidi", GiderCesidiTb.Text);
                    cmd.Parameters.AddWithValue("@Tutar", Convert.ToInt32(TutarTb.Text));
                    cmd.Parameters.AddWithValue("@EmpId", EmpIdTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Gider Kaydedildi");
                    conn.Close();
                    TabloDoldur();
                    Gider_Tbl_Temizle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (anahtar1 == 0)
            {
                MessageBox.Show("Silinecek Gider'i Seçiniz");
            }
            else
            {
                try
                {
                    conn.Open();
                    string Sorgu = "delete from GiderTbl where GiderId=" + anahtar1 + ";";
                    SqlCommand cmd = new SqlCommand(Sorgu, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Gider Silindi");
                    conn.Close();
                    TabloDoldur();
                    Gider_Tbl_Temizle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }       

        private void button4_Click(object sender, EventArgs e)
        {
            if (TipTb.Text == "" || MiktarTb.Text == "" || EmpIdTb.SelectedIndex == -1)
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else if (GelirTarihTb.Value.Date > DateTime.Today.Date)
            {
                MessageBox.Show("Yanlış Tarih");
            }
            else
            {
                try
                {
                    conn.Open();
                    string Sorgu = "insert into GelirTbl (GelirTarih, Tip, Miktar, EmpId ) values(@GelirTarih, @Tip, @Miktar, @EmpId)";
                    SqlCommand cmd = new SqlCommand(Sorgu, conn);
                    cmd.Parameters.AddWithValue("@GelirTarih", GelirTarihTb.Value);
                    cmd.Parameters.AddWithValue("@Tip", TipTb.Text);
                    cmd.Parameters.AddWithValue("@Miktar", Convert.ToInt32(MiktarTb.Text));
                    cmd.Parameters.AddWithValue("@EmpId", Convert.ToInt32(EmpIdTb.Text));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Gelir Kaydedildi");
                    conn.Close();
                    TabloDoldur();
                    Gelir_Tbl_Temizle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (TipTb.Text == "" || MiktarTb.Text == "" || EmpIdTb.SelectedIndex == -1)
            {
                MessageBox.Show("Eksik Bilgi");
            }
            else if (GelirTarihTb.Value.Date > DateTime.Today.Date)
            {
                MessageBox.Show("Yanlış Tarih");
            }
            else
            {
                try
                {
                    conn.Open();
                    string Sorgu = "update GelirTbl set GelirTarih=@GelirTarih,Tip = @Tip, Miktar = @Miktar, EmpId = @EmpId where GelirId = @GelirId ";
                    SqlCommand cmd = new SqlCommand(Sorgu, conn);
                    cmd.Parameters.AddWithValue("@GelirId", anahtar2);
                    cmd.Parameters.AddWithValue("@GelirTarih", GelirTarihTb.Value);
                    cmd.Parameters.AddWithValue("@Tip", TipTb.Text);
                    cmd.Parameters.AddWithValue("@Miktar", Convert.ToInt32(MiktarTb.Text));
                    cmd.Parameters.AddWithValue("@EmpId", Convert.ToInt32(EmpIdTb.Text));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Gelir Değiştirildi");
                    conn.Close();
                    TabloDoldur();
                    Gelir_Tbl_Temizle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (anahtar2 == 0)
            {
                MessageBox.Show("Silinecek Gelir'i Seçiniz");
            }
            else
            {
                try
                {
                    conn.Open();
                    string Sorgu = "delete from GelirTbl where GelirId=" + anahtar2 + ";";
                    SqlCommand cmd = new SqlCommand(Sorgu, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Gelir Silindi");
                    conn.Close();
                    TabloDoldur();
                    Gelir_Tbl_Temizle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Kayıt sm = new Kayıt();
            sm.Show();
            this.Hide();
        }

        int anahtar1 = 0;
        private void GiderDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            GiderDGV.CurrentRow.Selected = true;          
            GiderTarihTb.Text = GiderDGV.SelectedRows[0].Cells[1].Value.ToString();
            GiderCesidiTb.Text = GiderDGV.SelectedRows[0].Cells[2].Value.ToString();
            TutarTb.Text = GiderDGV.SelectedRows[0].Cells[3].Value.ToString();
            EmpIdTb.Text = GiderDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (GiderCesidiTb.Text == "")
            {
                anahtar1 = 0;
            }
            else
            {
                anahtar1 = Convert.ToInt32(GiderDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
        int anahtar2 = 0;
        private void GelirDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            GelirDGV.CurrentRow.Selected = true;
            GelirTarihTb.Text = GelirDGV.SelectedRows[0].Cells[1].Value.ToString();
            TipTb.Text = GelirDGV.SelectedRows[0].Cells[2].Value.ToString();
            MiktarTb.Text = GelirDGV.SelectedRows[0].Cells[3].Value.ToString();
            EmpIdTb.Text = GelirDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (TipTb.Text == "")
            {
                anahtar2 = 0;
            }
            else
            {
                anahtar2 = Convert.ToInt32(GelirDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void Filtre1_TextChanged(object sender, EventArgs e)
        {
            GiderDGV.DataSource = null;
            conn.Open();
            SqlDataAdapter adt = new SqlDataAdapter("select * from GiderTbl where GiderCesidi like '" + Filtre1.Text + "%'", conn);
            var dt = new DataTable();
            adt.Fill(dt);
            GiderDGV.DataSource = dt;
            conn.Close();
            if (Filtre1.Text == "")
            {
                GiderDGV.DataSource = null;
                TabloDoldur();
            }
        }

        private void Filtre2_TextChanged(object sender, EventArgs e)
        {
            GelirDGV.DataSource = null;
            conn.Open();
            SqlDataAdapter adt = new SqlDataAdapter("select * from GelirTbl where Tip like '" + Filtre2.Text + "%'", conn);
            var dt = new DataTable();
            adt.Fill(dt);
            GelirDGV.DataSource = dt;
            conn.Close();
            if (Filtre2.Text == "")
            {
                GelirDGV.DataSource = null;
                TabloDoldur();
            }
        }
    }
}
