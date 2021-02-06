using cSharpIntroWinForms.P10;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cSharpIntroWinForms.IB190082
{
    public partial class frmPretragaBrojIndeksa : Form
    {
        public frmPretragaBrojIndeksa()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }
        private void UcitajPodatke(List<KorisniciPredmeti> predmeti=null)
        {
            var rezultat = predmeti ?? DLWMS.DB.KorisniciPredmeti.ToList();
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = rezultat;
            }
            catch (Exception ex)
            {

                MboxHelper.PrikaziGresku(ex);
            }
        }
        private void frmPretragaBrojIndeksa_Load(object sender, EventArgs e)
        {
            UcitajPodatke();
            izracunajProsjek(dataGridView1.DataSource as List<KorisniciPredmeti>);
        }

        private void tbPretraga_TextChanged(object sender, EventArgs e)
        {
            var tekst = tbPretraga.Text.ToLower();
            List<KorisniciPredmeti> ocjene = new List<KorisniciPredmeti>();
            foreach(var predmet in DLWMS.DB.KorisniciPredmeti)
            {
                if (predmet.Predmet.Naziv.ToLower().Contains(tekst))
                {
                    ocjene.Add(predmet);
                }
            }
            UcitajPodatke(ocjene);
            izracunajProsjek(ocjene);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            var korisnik = dataGridView1.SelectedRows[0].DataBoundItem as KorisniciPredmeti;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                var forma = new frmPorukeBrojIndeksa(korisnik.Korisnik);
                forma.ShowDialog();
            }
        }
        private void izracunajProsjek (List<KorisniciPredmeti> ocjene = null)
        {
            if (ocjene != null)
            {
                int suma = 0;
                foreach (var ocjena in ocjene)
                {
                    suma += ocjena.Ocjena;
                }
                lblProsjek.Text = $"Prosjek prikazanih ocjena: {suma/ocjene.Count()}";
            }
            else
                lblProsjek.Text = $"Prosjek prikazanih ocjena: 0";
        }
        private void btnSuma_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(Sumiranje);
            long n = long.Parse(tbSuma.Text);
            thread.Start(n);
        }

        private void Sumiranje(object obj)
        {
            long n = long.Parse(obj.ToString());
            long suma = 0;
            for (int i = 0; i <= n; i++)
            {
                suma += i;
            }
            Action akcija = () => lblRezultat.Text = suma.ToString();
            BeginInvoke(akcija);
        }
    }
}
