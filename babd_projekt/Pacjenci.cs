using Microsoft.Data.SqlClient;
using System.Data;

namespace babd_projekt
{
    public partial class Pacjenci : Form
    {
        private ZarzadzaniePacjentem zp;
        public string connection = "Data Source = KINGA\\SQLEXPRESS;Initial Catalog = PROJEKT;Integrated Security=True; TrustServerCertificate=true";
        
        public Pacjenci()
        {
            InitializeComponent();
            Load_Data();
            zp = new ZarzadzaniePacjentem(connection);
            button4.Visible = false;
        }

        private void Load_Data()
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("dbo.GetAllPacjent", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
        }
         
        private int ObliczWiek(DateTime dataUrodzenia)
        {
            int wiek = DateTime.Now.Year - dataUrodzenia.Year;
            if (DateTime.Now < dataUrodzenia.AddYears(wiek))
            {
                wiek--;
            }

            return wiek;
        }
        //dodawanie
        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                string pesel = textBox3.Text;
                string imie = textBox1.Text;
                string nazwisko = textBox2.Text;
                DateTime dataUrodzenia = dateTimePicker1.Value;
                //string nrUbz = textBoxNrUbz.Text;
                char plec = comboBox2.SelectedItem.ToString().First();
                int wiek = ObliczWiek(dataUrodzenia);

                Pacjent nowyPacjent = new Pacjent
                {
                    Pesel = pesel,
                    Imie = imie,
                    Nazwisko = nazwisko,
                    DataUrodzenia = dataUrodzenia,
                    Plec = plec,
                    Wiek = wiek
                };

                zp.DodajPacjenta(nowyPacjent);
                MessageBox.Show("Pacjent dodany pomyœlnie!");
                //Load_Data();
                button4.Visible = true;

                DodawanieUbezpieczenia dUbz = new DodawanieUbezpieczenia(pesel);
                dUbz.ShowDialog();
                Load_Data();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"B³¹d podczas dodawania pacjenta: {ex.Message}");
            }
        }

        //usuwanie
        private void button3_Click(object sender, EventArgs e)
        {
            string peselDoUsuniecia = null;
            try
            {
                if(dataGridView1.SelectedRows.Count > 0)
{
                    peselDoUsuniecia = dataGridView1.SelectedRows[0].Cells["PESEL"].Value?.ToString();
                }

                if (!string.IsNullOrEmpty(peselDoUsuniecia))
                {
                    Pacjent pacjentDoUsuniecia = new Pacjent
                    {
                        Pesel = peselDoUsuniecia
                    };

                    zp.UsunPacjenta(pacjentDoUsuniecia);

                    MessageBox.Show("Pacjent usuniêty pomyœlnie!");
                    Load_Data();
                }
                else
                {
                    MessageBox.Show("Nie wybrano pacjenta do usuniêcia.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"B³¹d podczas usuwania pacjenta: {ex.Message}");
            }
        }

        //edycja
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string imie = selectedRow.Cells["P_IMIE"].Value?.ToString() ?? string.Empty;
                string nazwisko = selectedRow.Cells["P_NAZWISKO"].Value?.ToString() ?? string.Empty;
                string pesel = selectedRow.Cells["PESEL"].Value?.ToString() ?? string.Empty;
                string nr_ubz = selectedRow.Cells["NR_UBEZPIECZENIA"].Value?.ToString() ?? string.Empty;

                //object plecValue = selectedRow.Cells["PLEC"].Value;

                //char plec = plecValue != null && plecValue is string plecString && plecString.Length > 0 ? plecString[0] : '\0';
                DateTime data_ur = (DateTime)selectedRow.Cells["DATA_UR"].Value;

                EdycjaPacjenta ep = new EdycjaPacjenta(imie, nazwisko, pesel, nr_ubz, data_ur);//, plec);
                ep.ShowDialog();

                if (ep.Zapisano)
                {
                    selectedRow.Cells["P_IMIE"].Value = ep.NoweImie;
                    selectedRow.Cells["P_NAZWISKO"].Value = ep.NoweNazwisko;
                    selectedRow.Cells["PESEL"].Value = ep.NowyPesel;
                    selectedRow.Cells["DATA_UR"].Value = ep.NowaDataUr;
                    //selectedRow.Cells["PLEC"].Value = ep.NowaPlec;
                    selectedRow.Cells["NR_UBEZPIECZENIA"].Value = ep.NowyNrUbz;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e){}

        private void button4_Click(object sender, EventArgs e){}

        private void button5_Click(object sender, EventArgs e)
        {
            StronaGlowna sg = new StronaGlowna();
            sg.ShowDialog();
            this.Close();
        }
    }
}