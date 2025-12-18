using DVLD__Version_02_.Applications.LocalDrivingLicense;
using DVLD__Version_02_.Licenses;
using DVLD__Version_02_.Licenses.InternationalLicense;
using DVLD__Version_02_.People;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD__Version_02_.Applications.International_License
{
    public partial class frmListInternationalLicenseApplications : Form
    {
        private DataTable _dtAllInternationalLicenseApplications;

        public frmListInternationalLicenseApplications()
        {
            InitializeComponent();
        }

        private void frmListInternationalLicenseApplications_Load(object sender, EventArgs e)
        {
            _dtAllInternationalLicenseApplications = clsInternationalLicense.GetAllInternationalLicenses();
            dgvInternationalLicenseApplications.DataSource = _dtAllInternationalLicenseApplications;
            lbNumberOfApplications.Text = "# " + dgvInternationalLicenseApplications.Rows.Count.ToString() + " Records";

            if (dgvInternationalLicenseApplications.Rows.Count > 0)
            {
                dgvInternationalLicenseApplications.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicenseApplications.Columns[0].Width = 160;

                dgvInternationalLicenseApplications.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicenseApplications.Columns[1].Width = 150;

                dgvInternationalLicenseApplications.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicenseApplications.Columns[2].Width = 130;

                dgvInternationalLicenseApplications.Columns[3].HeaderText = "L.License ID";
                dgvInternationalLicenseApplications.Columns[3].Width = 130;

                dgvInternationalLicenseApplications.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicenseApplications.Columns[4].Width = 180;

                dgvInternationalLicenseApplications.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLicenseApplications.Columns[5].Width = 180;

                dgvInternationalLicenseApplications.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicenseApplications.Columns[6].Width = 120;
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None") && (cbFilterBy.Text != "Is Active");
            cbIsActive.Visible = (cbFilterBy.Text == "Is Active");
            txtFilterValue.Clear();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;

                case "Application ID":
                    FilterColumn = "ApplicationID";
                    break;

                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;


                default:
                    FilterColumn = "InternationalLicenseID";
                    break;
            }


            if (txtFilterValue.Text == "")
            {
                _dtAllInternationalLicenseApplications.DefaultView.RowFilter = "";
                lbNumberOfApplications.Text = "# " + dgvInternationalLicenseApplications.Rows.Count.ToString() + " Records";
                return;
            }

            _dtAllInternationalLicenseApplications.DefaultView.RowFilter = $"{FilterColumn}={txtFilterValue.Text}";
            lbNumberOfApplications.Text = "# " + dgvInternationalLicenseApplications.Rows.Count.ToString() + " Records";
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbIsActive.Text)
            {
                case "All":
                    _dtAllInternationalLicenseApplications.DefaultView.RowFilter = "";
                    break;

                case "Yes":
                    _dtAllInternationalLicenseApplications.DefaultView.RowFilter = "IsActive=1";
                    break;


                case "No":
                    _dtAllInternationalLicenseApplications.DefaultView.RowFilter = "IsActive=0";
                    break;
            }
            lbNumberOfApplications.Text = "# " + dgvInternationalLicenseApplications.Rows.Count.ToString() + " Records";

        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvInternationalLicenseApplications.CurrentRow.Cells[2].Value;
            int PersonID = clsDriver.FindByDriverID(DriverID).PersonID;

            frmPersonInfo frm = new frmPersonInfo(PersonID);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvInternationalLicenseApplications.CurrentRow.Cells[0].Value;
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(LicenseID);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmAddNewInternationalLicense  frm = new frmAddNewInternationalLicense();
            frm.StartPosition= FormStartPosition.CenterParent;
            frm.ShowDialog();

            frmListInternationalLicenseApplications_Load(null, null);
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsDriver.FindByDriverID((int)dgvInternationalLicenseApplications.CurrentRow.Cells[2].Value).PersonID;
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.StartPosition= FormStartPosition.CenterParent;
            frm.ShowDialog();
        }
    }
}
