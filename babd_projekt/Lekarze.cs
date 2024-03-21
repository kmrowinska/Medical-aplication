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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace babd_projekt
{
    public partial class Lekarze : Form
    {
        private ZarzadzanieLekarzem zl;
        public string connection = "Data Source = KINGA\\SQLEXPRESS;Initial Catalog = PROJEKT;Integrated Security=True; TrustServerCertificate=true";
        public Lekarze()
        {
            InitializeComponent();
            Load_Data();
            zl = new ZarzadzanieLekarzem(connection);
        }

        public void Load_Data()
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();

                using (SqlCommand oddzialCmd = new SqlCommand("SELECT [NAZWA_ODDZIALU] FROM ODDZIAL", con))
                {
                    using (SqlDataReader oddzialReader = oddzialCmd.ExecuteReader())
                    {
                        comboBox1.Items.Clear();
                        while (oddzialReader.Read())
                        {
                            string nazwaOddzialu = oddzialReader["NAZWA_ODDZIALU"].ToString();
                            comboBox1.Items.Add(nazwaOddzialu);
                        }
                    }
                }


                using (SqlCommand cmd = new SqlCommand("WczytajLekarzy", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e){}
        //dodaj
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string imie = textBox1.Text;
                string nazwisko = textBox2.Text;
                string specjalizacja = textBox3.Text;
                string oddzial = comboBox1.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(imie) || string.IsNullOrEmpty(nazwisko) || string.IsNullOrEmpty(specjalizacja) || string.IsNullOrEmpty(oddzial))
                {
                    MessageBox.Show("Wszystkie pola muszą być uzupełnione.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Lekarz nowyLekarz = new Lekarz
                {
                    Imie = imie,
                    Nazwisko = nazwisko,
                    Specjalizacja = specjalizacja,
                    Oddzial = oddzial
                };

                zl.DodajLekarza(nowyLekarz);
                int nowyLekarzID = nowyLekarz.ID;
                MessageBox.Show($"Lekarz dodany pomyślnie! ID: {nowyLekarzID}", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Load_Data();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas dodawania lekarza: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //usun
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    if (selectedRow != null)
                    {
                        int lekarzID = (int)selectedRow.Cells["ID"].Value;

                        Lekarz lekarzDoUsuniecia = new Lekarz
                        {
                            ID = lekarzID
                        };

                        zl.UsunLekarza(lekarzDoUsuniecia);
                        Load_Data(); 
                    }
                }
                else
                {
                    MessageBox.Show("Nie wybrano lekarza do usunięcia.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas usuwania lekarza: {ex.Message}");
            }
        }

        //wypisz recepte
        private void button3_Click(object sender, EventArgs e)
        {
            Recepty rp = new Recepty();
            rp.ShowDialog();    

        }

        private void button4_Click(object sender, EventArgs e)
        {
            StronaGlowna sg = new StronaGlowna();
            sg.ShowDialog();
            this.Close();
        }
    }
}
