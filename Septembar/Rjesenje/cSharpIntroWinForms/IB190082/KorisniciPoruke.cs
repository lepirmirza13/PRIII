using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharpIntroWinForms.IB190082
{
    public class KorisniciPoruke
    {
        public int Id { get; set; }
        public Korisnik Korisnik { get; set; }
        public string Sadrzaj { get; set; }
        public DateTime Datum { get; set; }
        public byte[] Slika { get; set; }
        public bool sakrij { get; set; } = false;
    }
}
