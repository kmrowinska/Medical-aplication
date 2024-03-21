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
    public partial class Recepty : Form
    {
        public string connection = "Data Source = KINGA\\SQLEXPRESS;Initial Catalog = PROJEKT;Integrated Security=True; TrustServerCertificate=true";
        public Recepty()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int numer_rp;
                using (SqlConnection con = new SqlConnection(connection))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("GenerujNumerRecepty", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlParameter wygenerowanaLiczbaParam = new SqlParameter("@NUMER", SqlDbType.Int);
                        wygenerowanaLiczbaParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(wygenerowanaLiczbaParam);
                        command.ExecuteNonQuery();
                        numer_rp = (int)wygenerowanaLiczbaParam.Value;
                    }
                }
                
                string peselPacjenta = textBox2.Text;
                int idLekarza = Convert.ToInt32(textBox3.Text);
                string dawkowanie = textBox4.Text; 
                DateTime dataWystawienia = dateTimePicker1.Value;

                using (SqlConnection con = new SqlConnection(connection))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("DodajRecepte", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NumerRp", numer_rp);
                        command.Parameters.AddWithValue("@PeselPacjenta", peselPacjenta);
                        command.Parameters.AddWithValue("@IDLekarza", idLekarza);
                        command.Parameters.AddWithValue("@Dawkowanie", dawkowanie);
                        command.Parameters.AddWithValue("@Data_Wystawienia", dataWystawienia);
                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show($"Dodano receptę do systemu.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas wywoływania procedury: {ex.Message}");
            }

        }
    }
}
