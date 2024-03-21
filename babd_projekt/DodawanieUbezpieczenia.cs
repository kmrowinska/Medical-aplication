using Microsoft.Data.SqlClient;
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
    public partial class DodawanieUbezpieczenia : Form
    {
        public string connection = "Data Source = KINGA\\SQLEXPRESS;Initial Catalog = PROJEKT;Integrated Security=True; TrustServerCertificate=true";
        private string peselPacjenta;
        public DodawanieUbezpieczenia(string peselPacjenta)
        {
            InitializeComponent();
            this.peselPacjenta = peselPacjenta;
            textBox1.Text = peselPacjenta;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string numerUbz = textBox2.Text;
                string rodzajUbz = textBox3.Text;
                DateTime dataWaznosi = dateTimePicker1.Value;

                using (SqlConnection con = new SqlConnection(connection))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("DodajUbezpieczenie", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PESEL", peselPacjenta);
                        cmd.Parameters.AddWithValue("@NumerUbezpieczenia", numerUbz);
                        cmd.Parameters.AddWithValue("@RodzajUbezpieczenia", rodzajUbz);
                        cmd.Parameters.AddWithValue("@DataWaznosci", dataWaznosi);

                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmdDodajNumerUbezpieczenia = new SqlCommand("DodajNumerUbezpieczenia", con))
                    {
                        cmdDodajNumerUbezpieczenia.CommandType = CommandType.StoredProcedure;
                        cmdDodajNumerUbezpieczenia.Parameters.AddWithValue("@PESEL", peselPacjenta);
                        cmdDodajNumerUbezpieczenia.Parameters.AddWithValue("@Nr_Ubezpieczenia", numerUbz);

                        cmdDodajNumerUbezpieczenia.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Dane ubezpieczenia zapisane pomyślnie!");
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Błąd podczas zapisywania danych ubezpieczenia: {ex.Message}");
            }
        }
            
    }
}

