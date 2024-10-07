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
    public partial class Göstergeler : Form
    {
        public Göstergeler()
        {
            InitializeComponent();
            deger_doldur();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MELİH\Documents\TaÇiYa.mdf;Integrated Security=True;Connect Timeout=30");

        public void deger_doldur()
        {
            int YumStok = 0;  int Kazanç = 0;
            conn.Open();
            SqlCommand komut1 = new SqlCommand("select count(*) from TavukTbl",conn);
            label18.Text = komut1.ExecuteScalar().ToString();
            SqlCommand komut2 = new SqlCommand("select sum(ToplananYumurta) from YumurtaUretTbl",conn);
            SqlCommand komut3 = new SqlCommand("select sum(Miktar) from YumurtaSatTbl", conn);
            YumStok = Convert.ToInt32(komut2.ExecuteScalar()) - Convert.ToInt32(komut3.ExecuteScalar());
            label17.Text = YumStok.ToString();
            SqlCommand komut4 = new SqlCommand("select count(*) from CalisanTbl",conn);
            label21.Text = komut4.ExecuteScalar().ToString();
            SqlCommand komut5 = new SqlCommand("select sum(Miktar) from GelirTbl",conn);
            label8.Text = komut5.ExecuteScalar().ToString();
            SqlCommand komut6 = new SqlCommand("select sum(Tutar) from GiderTbl",conn);
            label7.Text = komut6.ExecuteScalar().ToString();
            Kazanç = Convert.ToInt32(komut5.ExecuteScalar()) - Convert.ToInt32(komut6.ExecuteScalar());
            label23.Text = Kazanç.ToString();
            SqlCommand komut7 = new SqlCommand("select max(Miktar) from GelirTbl", conn);
            label27.Text = komut7.ExecuteScalar().ToString();
            SqlCommand komut8 = new SqlCommand("select max(Tutar) from GiderTbl",conn);
            label25.Text = komut8.ExecuteScalar().ToString();
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

        private void label14_Click(object sender, EventArgs e)
        {
            Finans sm = new Finans();
            sm.Show();
            this.Hide();
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

    }
}
