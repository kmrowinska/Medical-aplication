using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace babd_projekt
{
    public partial class StronaGlowna : Form
    {
        public StronaGlowna()
        {
            InitializeComponent();
        }

        //pacjent
        private void button1_Click(object sender, EventArgs e)
        {
            Pacjenci f1 = new Pacjenci();
            f1.ShowDialog();
            this.Close();
        }

        //lekarz
        private void button2_Click(object sender, EventArgs e)
        {
            Lekarze l1 = new Lekarze();
            l1.ShowDialog();
            this.Close();
        }

        //wyszukiwarka
        private void button3_Click(object sender, EventArgs e)
        {
            Wyszukiwarka wk = new Wyszukiwarka();
            wk.ShowDialog();

        }

        //badanie
        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
