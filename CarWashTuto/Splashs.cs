using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarWashTuto
{
    public partial class Splashs : Form
    {
        public Splashs()
        {
            InitializeComponent();
        }
        int startp = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startp += 1;
            Myprogress.Value = startp;
            if(Myprogress.Value == 100)
            {
                Myprogress.Value = 0;
                timer1.Stop();
                Logins Mylogin = new Logins();
                this.Hide();
                Mylogin.Show();
            }
        }

        private void Splashs_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
