using Microsoft.Reporting.WinForms;
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
    public partial class frmIzvjestaj : Form
    {
        Korisnik korisnik;
        List<KorisniciPoruke> poruke;
        public frmIzvjestaj()
        {
            InitializeComponent();
        }
        public frmIzvjestaj(Korisnik korisnik, List<KorisniciPoruke> poruke): this()
        {
            this.korisnik = korisnik;
            this.poruke = poruke;
        }

        private void frmIzvjestaj_Load(object sender, EventArgs e)
        {
            ReportParameterCollection rpc = new ReportParameterCollection();
            rpc.Add(new ReportParameter("ImePrezime", korisnik.ToString()));
            reportViewer1.LocalReport.SetParameters(rpc);
            DataSet1.PorukeDataTable tabela = new DataSet1.PorukeDataTable();
            foreach(var poruka in poruke)
            {
                var red = tabela.NewPorukeRow();
                red.Datum = poruka.Datum.ToString();
                red.Sadrzaj = poruka.Sadrzaj;
                red.Slika = poruka.Slika;
                tabela.AddPorukeRow(red);
            }
            ReportDataSource rds = new ReportDataSource();

            rds.Name = "DataSet1";
            rds.Value = tabela;

            reportViewer1.LocalReport.DataSources.Add(rds);


            this.reportViewer1.RefreshReport();
        }
    }
}
