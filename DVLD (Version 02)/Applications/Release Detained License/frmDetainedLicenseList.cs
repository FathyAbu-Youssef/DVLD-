using DVLD__Version_02_.Applications.Detain_License;
using DVLD__Version_02_.Licenses;
using DVLD__Version_02_.Licenses.Local_Licenses;
using DVLD__Version_02_.People;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD__Version_02_.Applications.Release_Detained_License
{
    public partial class frmDetainedLicenseList : Form
    {
        private DataTable _dtDetainedLicenses;
        public frmDetainedLicenseList()
        {
            InitializeComponent();
        }
 
        private void btnAddNewDetainedLicense_Click(object sender, EventArgs e)
        {
            frmDetainApplication frm = new frmDetainApplication();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            //Refresh
            frmDetainedLicenseList_Load(null, null);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            //Refresh
            frmDetainedLicenseList_Load(null, null);
        }

        private void cmbfilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterByvalue.Visible = (cbfilter.Text != "None") && (cbfilter.Text != "Is Released");
            cbIsReleased.Visible = (cbfilter.Text == "Is Released");
            txtFilterByvalue.Text = "";
            cbIsReleased.SelectedIndex = 0;
        }

        private void txtFilterByvalue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbfilter.Text == "Detain ID" || cbfilter.Text == "Release Application ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
            else
            {
                e.Handled = !char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar);
            }
        }

        private void txtFilterByvalue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = txtFilterByvalue.Text;

            switch (cbfilter.Text)
            {
                case "Detain ID":
                    FilterColumn = "DetainID";
                    break;

                case "National No":
                    FilterColumn = "NationalNo";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Release Application ID":
                    FilterColumn = "ReleaseApplicationID";
                    break;
            }

            if (string.IsNullOrEmpty(txtFilterByvalue.Text))
            {
                _dtDetainedLicenses.DefaultView.RowFilter = "";
                lbCountOfRecords.Text = "# " + dgvDetainedLicenses.Rows.Count.ToString() + " Records";
                return; ;
            }

            if (FilterColumn == "DetainID" || FilterColumn == "ReleaseApplicationID")
            {
                _dtDetainedLicenses.DefaultView.RowFilter = $"{FilterColumn}={txtFilterByvalue.Text}";
            }
            else
            {
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterByvalue.Text.Trim());
            }

            lbCountOfRecords.Text = "# " + dgvDetainedLicenses.Rows.Count.ToString() + " Records";
        }

        private void frmDetainedLicenseList_Load(object sender, EventArgs e)
        {
            _dtDetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();
            dgvDetainedLicenses.DataSource = _dtDetainedLicenses;
            lbCountOfRecords.Text = "# " + dgvDetainedLicenses.Rows.Count.ToString() + " Records";

            if (dgvDetainedLicenses.Rows.Count > 0)
            {
                dgvDetainedLicenses.Columns[0].HeaderText = "D.ID";
                dgvDetainedLicenses.Columns[0].Width = 90;

                dgvDetainedLicenses.Columns[1].HeaderText = "L.ID";
                dgvDetainedLicenses.Columns[1].Width = 90;

                dgvDetainedLicenses.Columns[2].HeaderText = "D.Date";
                dgvDetainedLicenses.Columns[2].Width = 160;

                dgvDetainedLicenses.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicenses.Columns[3].Width = 110;

                dgvDetainedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicenses.Columns[4].Width = 110;

                dgvDetainedLicenses.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicenses.Columns[5].Width = 160;

                dgvDetainedLicenses.Columns[6].HeaderText = "N.No.";
                dgvDetainedLicenses.Columns[6].Width = 90;

                dgvDetainedLicenses.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicenses.Columns[7].Width = 330;

                dgvDetainedLicenses.Columns[8].HeaderText = "Rlease App.ID";
                dgvDetainedLicenses.Columns[8].Width = 150;
            }
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsReleased";

            switch (cbIsReleased.Text)
            {
                case "All":
                    _dtDetainedLicenses.DefaultView.RowFilter = $"{FilterColumn}=0 or {FilterColumn}=1";
                    break;

                case "Yes":
                    _dtDetainedLicenses.DefaultView.RowFilter = $"{FilterColumn}=1";
                    break;

                case "No":
                    _dtDetainedLicenses.DefaultView.RowFilter = $"{FilterColumn}=0";
                    break;
            }
            lbCountOfRecords.Text = "# " + dgvDetainedLicenses.Rows.Count.ToString() + " Records";

        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            int PersonID = clsLicense.Find(LicenseID).DriverInfo.PersonID;
            frmPersonInfo frm = new frmPersonInfo(PersonID);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void ShowPersonLicenseHistroyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            int PersonID = clsLicense.Find(LicenseID).DriverInfo.PersonID;

            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void cmOptions_Opening(object sender, CancelEventArgs e)
        {
            ReleaseLicenseToolStripMenuItem.Enabled = ((Boolean)dgvDetainedLicenses.CurrentRow.Cells[3].Value == false);
        }

        private void ReleaseLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication(LicenseID);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            //Refresh
            frmDetainedLicenseList_Load(null, null);
        }

        private void lbCountOfRecords_Click(object sender, EventArgs e)
        {

        }
    }
}
