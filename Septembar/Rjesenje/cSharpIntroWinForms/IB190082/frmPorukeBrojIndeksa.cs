using cSharpIntroWinForms.P10;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cSharpIntroWinForms.IB190082
{
    public partial class frmPorukeBrojIndeksa : Form
    {
        Korisnik korisnik;
        public frmPorukeBrojIndeksa()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }
        public frmPorukeBrojIndeksa(Korisnik korisnik) : this ()
        {
            this.korisnik = korisnik;
        }
        private void UcitajPoruke()
        {
            dataGridView1.DataSource = null;
            List<KorisniciPoruke> poruke = new List<KorisniciPoruke>();
            foreach (var poruka in korisnik.KorisniciPoruke)
            {
                if (!poruka.sakrij)
                {
                    poruke.Add(poruka);
                }
            }
            dataGridView1.DataSource = poruke;
        }

        private void frmPorukeBrojIndeksa_Load(object sender, EventArgs e)
        {
            lblKomunikacija.Text = $"Komunikacija sa: {korisnik.ToString()}";
            UcitajPoruke();
        }

        private void btnNP_Click(object sender, EventArgs e)
        {
            var forma = new frmNovaPorukeBrojIndeks(korisnik);
            forma.ShowDialog();
            UcitajPoruke();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            var forma = new frmNovaPorukeBrojIndeks(dataGridView1.SelectedRows[0].DataBoundItem as KorisniciPoruke);
            forma.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            var poruka = dataGridView1.SelectedRows[0].DataBoundItem as KorisniciPoruke;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                poruka.sakrij = true;
                //if (MessageBox.Show("Da li ste sigurni de zelite izbirsati poruku?") == DialogResult.OK)
                //{
                //    DLWMS.DB.KorisniciPoruke.Remove(poruka);
                //    DLWMS.DB.SaveChanges();
                //    UcitajPoruke();
                //}
                DLWMS.DB.SaveChanges();
                UcitajPoruke();
            }
        }

        private void btnPrintaj_Click(object sender, EventArgs e)
        {
            var forma = new frmIzvjestaj(korisnik, dataGridView1.DataSource as List<KorisniciPoruke>);
            forma.ShowDialog();
        }
    }
}
