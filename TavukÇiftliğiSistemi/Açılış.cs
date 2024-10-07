using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TavukÇiftliğiSistemi
{
    public partial class Açılış : Form
    {
        public Açılış()
        {
            InitializeComponent();
        }

        private void Açılış_Load(object sender, EventArgs e)
        {
            this.timer1.Start();
        }
        int başlangıç = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            başlangıç += 2;
            progressBar1.Value = başlangıç;
            if(progressBar1.Value == 100)
            {
                progressBar1.Value = 0;
                timer1.Stop();
                Kayıt sm = new Kayıt();
                sm.Show();
                this.Hide();
            }
        }
    }
}
