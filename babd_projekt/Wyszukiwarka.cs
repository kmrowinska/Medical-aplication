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
    public partial class Wyszukiwarka : Form
    {
        public string connectionString = "Data Source = KINGA\\SQLEXPRESS;Initial Catalog = PROJEKT;Integrated Security=True; TrustServerCertificate=true";
        public Wyszukiwarka()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string pesel = textBox1.Text;
                if (pesel.Length == 11 && pesel.All(char.IsDigit))
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand("WszystkieInfoPacjencie", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@PESEL", pesel);

                            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                            {
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);
                                dataGridView1.DataSource = dataTable;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("PESEL musi składać się z dokładnie 11 cyfr.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas pobierania informacji: {ex.Message}");
            }
        }
    }
}
