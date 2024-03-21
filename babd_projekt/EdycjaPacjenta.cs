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
    public partial class EdycjaPacjenta : Form
    {
        public bool Zapisano { get;  private set; }
        public string NoweImie { get; private set; } = string.Empty;
        public string NoweNazwisko { get; private set; } = string.Empty;
        public string NowyPesel { get;  private set; } = string.Empty;
        public string NowyNrUbz { get;  private set; } = string.Empty;
        public char NowaPlec { get; private set; }
        public DateTime NowaDataUr { get;  private set; }
        public EdycjaPacjenta(string imie, string nazwisko, string pesel, string nrUbz, DateTime dataUr)//, char plec)
        {
            InitializeComponent();
            textBox4.Text = imie;
            textBox2.Text = nazwisko;
            textBox3.Text = pesel;
            textBox1.Text = nrUbz;
            //comboBox1.SelectedItem = plec;
            dateTimePicker1.Value = dataUr;
        }

        private void textBox4_TextChanged(object sender, EventArgs e){}

        private void textBox2_TextChanged(object sender, EventArgs e){}

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox2.Text) ||
            string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox1.Text) == null)
            //comboBox1.SelectedItem )
            {
                MessageBox.Show("Wszystkie pola muszą być uzupełnione.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            NoweImie = textBox4.Text;
            NoweNazwisko = textBox2.Text;
            NowyPesel = textBox3.Text;
            //NowaPlec = (char)comboBox1.SelectedItem;
            NowaDataUr = dateTimePicker1.Value;
            NowyNrUbz = textBox1.Text;

            Zapisano = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
