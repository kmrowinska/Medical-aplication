using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace babd_projekt
{
    public class ZarzadzanieLekarzem
    {
        private string connectionString = "Data Source = KINGA\\SQLEXPRESS;Initial Catalog = PROJEKT;Integrated Security=True; TrustServerCertificate=true";

        public ZarzadzanieLekarzem(string connectionString)
        {

        }
        public void DodajLekarza(Lekarz lekarz)
        {
            if (string.IsNullOrEmpty(lekarz.Imie) || !lekarz.Imie.All(char.IsLetter))
            {
                throw new ArgumentException("Imię musi składać się z liter.");
            }

            if (string.IsNullOrEmpty(lekarz.Nazwisko) || !lekarz.Nazwisko.All(char.IsLetter))
            {
                throw new ArgumentException("Nazwisko musi składać się z liter.");
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                    try
                    {
                        using (SqlCommand command = new SqlCommand("DodajLekarza", connection))
                        {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Imie", lekarz.Imie);
                        command.Parameters.AddWithValue("@Nazwisko", lekarz.Nazwisko);
                        command.Parameters.AddWithValue("@Specjalizacja", lekarz.Specjalizacja);
                        command.Parameters.AddWithValue("@Oddzial", lekarz.Oddzial);
                        SqlParameter lekarzIDparam = new SqlParameter("@NowyLekarzID", SqlDbType.Int);
                        lekarzIDparam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(lekarzIDparam);
                        command.ExecuteNonQuery();

                        int nowyLekarzID = (int)lekarzIDparam.Value;
                        lekarz.ID = nowyLekarzID;
                        }
                    }
                    catch (SqlException ex)
                    {
                        string errorMessage = "Błąd podczas dodawania lekarza do bazy danych. Komunikat błędu: " + ex.Message;
                        MessageBox.Show(errorMessage);
                        throw new Exception(errorMessage, ex);
                    }
                
            }
        }

        public void UsunLekarza(Lekarz lekarz)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("UsunLekarza", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IDLekarza", lekarz.ID);
                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Lekarz został usunięty.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas usuwania lekarza: {ex.Message}");
            }
        }
    }

    
}
