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
    public partial class Kayıt : Form
    {
        public Kayıt()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\MELİH\Documents\TaÇiYa.mdf;Integrated Security=True;Connect Timeout=30");

        private void Temizle()
        {
            comboBox1.Text = "Mevki";
            textBox1.Text = "";
            textBox2.Text = "";
        }

        int AdminSay = 0;
        int EmpSay = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Yönetici")
            {
                if (textBox1.Text == "admin" && textBox2.Text == "55555")
                {
                    AdminSay = 0;
                    Yönetici sm = new Yönetici();
                    sm.Show();
                    this.Hide();
                }
                else if (textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("Lütfen Tüm Alanları Doldurunuz !");
                }

                else
                {
                    AdminSay += 1;
                    MessageBox.Show("Kullanıcı Adı Veya Şifre Yanlış !");
                    Temizle();
                }
            }
            else if (comboBox1.Text == "Çalışan")
            {
                conn.Open();
                string user;
                string password;
                user = textBox1.Text;
                password = textBox2.Text;

                SqlCommand komut = new SqlCommand("select * from CalisanTbl where EmpAd='" + user + "' and Sifre='" + password + "'", conn);
                SqlDataReader oku = komut.ExecuteReader();
                if (oku.Read())
                {
                    EmpSay = 0;
                    Tavuklar sm = new Tavuklar();
                    sm.Show();
                    this.Hide();
                }
                else if (textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("Lütfen Tüm Alanları Doldurunuz !");
                }
                else
                {
                    EmpSay += 1;
                    MessageBox.Show("Kullanıcı Adı veya Şifre Hatalı");
                    Temizle();
                }

                conn.Close();
            }
            else
            {
                MessageBox.Show("Lütfen Mevki Seçiniz !");
            }
            if(AdminSay == 3 || EmpSay == 3)
            {
                MessageBox.Show("Çok Fazla Hatalı Deneme Yaptınız");
                Application.Exit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Temizle();
        }
    }
}
