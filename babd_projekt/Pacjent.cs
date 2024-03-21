using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace babd_projekt
{
    public class Pacjent
    {
        public string Pesel { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public DateTime DataUrodzenia { get; set; }
        public string NrUbz { get; set; }
        public char Plec { get; set; }
        public int Wiek { get; set; }
    }
}
