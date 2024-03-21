using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace babd_projekt
{
    public class ZarzadzaniePacjentem
    {
        private string connectionString = "Data Source = KINGA\\SQLEXPRESS;Initial Catalog = PROJEKT;Integrated Security=True; TrustServerCertificate=true";

        public ZarzadzaniePacjentem(string connectionString)
        {
            //this.connectionString = connectionString;
        }

        public void DodajPacjenta(Pacjent pacjent)
        {
            if (string.IsNullOrEmpty(pacjent.Pesel) || pacjent.Pesel.Length != 11 || !pacjent.Pesel.All(char.IsDigit))
            {
                throw new ArgumentException("PESEL musi składać się z 11 cyfr.");
            }

            if (string.IsNullOrEmpty(pacjent.Imie) || !pacjent.Imie.All(char.IsLetter))
            {
                throw new ArgumentException("Imię musi składać się z liter.");
            }

            if (string.IsNullOrEmpty(pacjent.Nazwisko) || !pacjent.Nazwisko.All(char.IsLetter))
            {
                throw new ArgumentException("Nazwisko musi składać się z liter.");
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("DodajPacjenta", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PESEL", pacjent.Pesel);
                    command.Parameters.AddWithValue("@Imie", pacjent.Imie);
                    command.Parameters.AddWithValue("@Nazwisko", pacjent.Nazwisko);
                    command.Parameters.AddWithValue("@DataUrodzenia", pacjent.DataUrodzenia);
                    //command.Parameters.AddWithValue("@NrUbz", pacjent.NrUbz);
                    command.Parameters.AddWithValue("@Plec", pacjent.Plec);
                    command.Parameters.AddWithValue("@Wiek", pacjent.Wiek);

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("Błąd podczas dodawania pacjenta do bazy danych.", ex);
                    }
                }
            }
        }

        public void UsunPacjenta(Pacjent pacjent)
        {
            if (string.IsNullOrEmpty(pacjent.Pesel))
            {
                throw new ArgumentException("PESEL nie może być pusty.");
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UsunPacjenta", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PESEL", pacjent.Pesel);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            throw new Exception("Nie znaleziono pacjenta o podanym numerze PESEL.");
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("Błąd podczas usuwania pacjenta z bazy danych.", ex);
                    }
                }
            }
        }

        public void EdytujPacjenta(Pacjent pacjent)
        {
        }
    }
}
