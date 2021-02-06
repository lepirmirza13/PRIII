using cSharpIntroWinForms.P10;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cSharpIntroWinForms.IB190082
{
    public partial class frmNovaPorukeBrojIndeks : Form
    {
        public Korisnik korisnik;
        public KorisniciPoruke poruka = new KorisniciPoruke();
        public frmNovaPorukeBrojIndeks()
        {
            InitializeComponent();
        }
        //public frmNovaPorukeBrojIndeks(KorisniciPoruke poruka) : this()
        //{
        //    this.poruka = poruka;
        //    tbPrimalac.ReadOnly = true;
        //    tbSadrzaj.ReadOnly = true;
        //    button1.Hide();
        //}
        public frmNovaPorukeBrojIndeks(Korisnik korisnik): this ()
        {
            this.korisnik = korisnik;
            tbPrimalac.Enabled = false;
            tbPrimalac.Text = korisnik.ToString();
        }
        public frmNovaPorukeBrojIndeks(KorisniciPoruke poruka) : this()
        {
            this.poruka = poruka;
            pbSlika.Enabled = false;
            tbPrimalac.Text = poruka.Korisnik.ToString();
            tbPrimalac.Enabled = false;
            tbSadrzaj.Enabled = false;
            tbSadrzaj.Text = poruka.Sadrzaj;
            button1.Enabled = false;
            if (poruka.Slika!=null)
            pbSlika.Image = Image.FromStream(new MemoryStream(poruka.Slika));
        }
        public byte[] ImageToByteArray(System.Drawing.Image img)
        {
            using (var ms = new MemoryStream())
            {
                img.Save(ms, img.RawFormat);
                return ms.ToArray();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbSadrzaj.Text))
            {
                er.SetError(tbSadrzaj, "Mora imati tekst!");
                return;
            }
            var poruke = new KorisniciPoruke();
            poruke.Datum = DateTime.Now;
            poruke.Korisnik = korisnik;
            poruke.Sadrzaj = tbSadrzaj.Text;
            if (pbSlika.Image!=null)
            poruke.Slika = ImageToByteArray(pbSlika.Image);
            DLWMS.DB.KorisniciPoruke.Add(poruke);
            DLWMS.DB.SaveChanges();
            Close();
        }

        private void frmNovaPorukeBrojIndeks_Load(object sender, EventArgs e)
        {
            
        }

        private void pbSlika_Click(object sender, EventArgs e)
        {
            var opf = new OpenFileDialog();
            if (opf.ShowDialog()==DialogResult.OK)
            {
                var putanja = opf.FileName;
                Image slika = Image.FromFile(putanja);
                pbSlika.Image = slika;
            }
        }
    }
}
