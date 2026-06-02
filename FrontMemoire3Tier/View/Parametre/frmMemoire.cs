using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrontMemoire3Tier.View.Parametre
{
    public partial class frmMemoire : Form
    {
        ServiceMemoire.Service1Client service = new ServiceMemoire.Service1Client();  
        public frmMemoire()
        {
            InitializeComponent();
        }
        private void Effacer()
        {
            txtAnneeMemoire.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtSujet.Text = string.Empty;
            dgMemoire.DataSource = service.GetAllMemoire();
            txtSujet.Focus();
        }

        private void frmMemoire_Load(object sender, EventArgs e)
        {
            Effacer();
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            ServiceMemoire.Memoire memoire = new ServiceMemoire.Memoire();
            memoire.AnneeMemoire = int.Parse(txtAnneeMemoire.Text).ToString();
            memoire.DescriptionMemoire = txtDescription.Text;
            memoire.SujetMemoire = txtSujet.Text;
            service.AddMemoire(memoire);
            Effacer();

        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            ServiceMemoire.Memoire memoire = new ServiceMemoire.Memoire();
            memoire.AnneeMemoire = int.Parse(txtAnneeMemoire.Text).ToString();
            memoire.DescriptionMemoire = txtDescription.Text;
            memoire.SujetMemoire = txtSujet.Text;
            service.UpdateMemoire(memoire);
            Effacer();
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (dgMemoire.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgMemoire.CurrentRow.Cells[0].Value);
                service.DeleteMemoire(id);
                Effacer();
            }

        }
    }
}
