using DVLD__Version_02_.Licenses;
using DVLD__Version_02_.Licenses.Local_Licenses;
using DVLD__Version_02_.Tests;
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

namespace DVLD__Version_02_.Applications.LocalDrivingLicense
{
    public partial class frmListLocalDrivingLicenseApplications : Form
    {
        private DataTable _dtAllApplications;

        public frmListLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            frmListLocalDrivingLicenseApplications_Load(null, null);
        }

        private void frmListLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            txtFilterByvalue.Text = "";
            _dtAllApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvLocalDrivingLicenseApplications.DataSource = _dtAllApplications;
            lbNumberOfApplications.Text = "# " + dgvLocalDrivingLicenseApplications.Rows.Count.ToString() + " Record";

            if (dgvLocalDrivingLicenseApplications.Rows.Count > 0) 
            {
                dgvLocalDrivingLicenseApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalDrivingLicenseApplications.Columns[0].Width = 120;

                dgvLocalDrivingLicenseApplications.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplications.Columns[1].Width = 300;

                dgvLocalDrivingLicenseApplications.Columns[2].HeaderText = "National No.";
                dgvLocalDrivingLicenseApplications.Columns[2].Width = 150;

                dgvLocalDrivingLicenseApplications.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplications.Columns[3].Width = 350;

                dgvLocalDrivingLicenseApplications.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplications.Columns[4].Width = 170;

                dgvLocalDrivingLicenseApplications.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApplications.Columns[5].Width = 150;

            }

        }

        private void ShowApplicationDetails_Click(object sender, EventArgs e)
        {
            int LDLAID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            frmShowLocalDrivingLicenseApplicationInfo frm = new frmShowLocalDrivingLicenseApplicationInfo(LDLAID);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void EditApplication_Click(object sender, EventArgs e)
        {
            int LDLAID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            frmAddUpdateLocalDrivingLicenseApplication EditApplication = new frmAddUpdateLocalDrivingLicenseApplication(LDLAID);
            EditApplication.StartPosition = FormStartPosition.CenterParent;
            EditApplication.ShowDialog();

            frmListLocalDrivingLicenseApplications_Load(null, null);
        }

        private void deleteAppliicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Are You Sure, You Want To Delete This Application?", "Are You Sure", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
           
            if (Result == DialogResult.OK)  
            {
                int CurrentApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

                clsLocalDrivingLicenseApplication CurrentApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(CurrentApplicationID);

                if (CurrentApplication.Delete())  
                {
                    MessageBox.Show("Application Deleted Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            frmListLocalDrivingLicenseApplications_Load(null, null);
        }

        private void CancelApplication_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Are You Sure, You Want To Cancel This Application?", "Are You Sure", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (Result == DialogResult.OK)
            {
                int CurrentApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

                clsLocalDrivingLicenseApplication CurrentApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(CurrentApplicationID);

                if (CurrentApplication.Cancel())
                {
                    MessageBox.Show("Application Canceled Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            frmListLocalDrivingLicenseApplications_Load(null, null);
        }

        private void cmbfilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterByvalue.Visible = (cbfilter.Text != "None");
            txtFilterByvalue.Text = "";
        }

        private void txtFilterByvalue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbfilter.Text == "L.D.L.A.ID")
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
            string FilterColumn = "";

            switch (cbfilter.Text)
            {
                case "L.D.L.A.ID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "National No":
                    FilterColumn = "NationalNo";
                    break;


                case "Status":
                    FilterColumn = "Status";
                    break;
            }

            if (txtFilterByvalue.Text == "")
            {
                _dtAllApplications.DefaultView.RowFilter = "";
                lbNumberOfApplications.Text = "# " + dgvLocalDrivingLicenseApplications.Rows.Count.ToString() + " Record";
                return;
            }
            else if (FilterColumn != "LocalDrivingLicenseApplicationID")
            {
                _dtAllApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterByvalue.Text.Trim());
            }
            else 
            {
                _dtAllApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterByvalue.Text.Trim());
            }
            lbNumberOfApplications.Text = "# " + dgvLocalDrivingLicenseApplications.Rows.Count.ToString() + " Records";
        }

        private void ScheduleTestAppointment(clsTestType.enTestType TestType)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            frmListTestAppointments frm = new frmListTestAppointments(LocalDrivingLicenseApplicationID,TestType);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            frmListLocalDrivingLicenseApplications_Load(null, null);
        }

        private void VisionTest_Click(object sender, EventArgs e)
        {
            ScheduleTestAppointment(clsTestType.enTestType.VisionTest);
        }

        private void WrittenTest_Click(object sender, EventArgs e)
        {
            ScheduleTestAppointment(clsTestType.enTestType.WrittenTest);
        }

        private void StreetTest_Click(object sender, EventArgs e)
        {
            ScheduleTestAppointment(clsTestType.enTestType.StreetTest);
        }

        private void IssueDrivingLicense_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            frmIssueLocalLicenseFirstTime frm = new frmIssueLocalLicenseFirstTime(LocalDrivingLicenseApplicationID);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            frmListLocalDrivingLicenseApplications_Load(null, null);
        }

        private void ShowLicense_Click(object sender, EventArgs e)
        {
            int LicenseID = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID
                ((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value).GetActiveLicenseID();

            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void ShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            int PersonID = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID
         ((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value).ApplicantPersonID;


            frmShowPersonLicenseHistory frmShowPersonLicenseHistory = new frmShowPersonLicenseHistory(PersonID);
            frmShowPersonLicenseHistory.StartPosition = FormStartPosition.CenterParent;
            frmShowPersonLicenseHistory.ShowDialog();
        }

        private void CMS_Operations_Opening(object sender, CancelEventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);

            int PassedTests = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[5].Value;
            bool IsLicenseIssued = LocalDrivingLicenseApplication.IsLicenseIssued();

            IssueDrivingLicenseMenueItem.Enabled = (!IsLicenseIssued) && (PassedTests == 3)
                && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enStatus.New);

            EditApplicationMenueItem.Enabled = (!IsLicenseIssued) && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enStatus.New);
            CancelApplicationMenueItem.Enabled = LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enStatus.New;
            DeleteAppliicationMenuItem.Enabled = LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enStatus.New;
            ShowLicenseMenueItem.Enabled = IsLicenseIssued;

            bool IsPassedVisonTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.VisionTest);
            bool IsPassedWrittenTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.WrittenTest);
            bool IsPassedStreetTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.StreetTest);

            ScheduleTestSubMenue.Enabled = (!IsPassedVisonTest || !IsPassedWrittenTest || !IsPassedStreetTest) &&
                (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enStatus.New);

            if (ScheduleTestSubMenue.Enabled)
            {
                VisionTestMenueItem.Enabled = !IsPassedVisonTest;
                WrittenTestMenueItem.Enabled = IsPassedVisonTest && !IsPassedWrittenTest;
                StreetTestMenueItem.Enabled = IsPassedWrittenTest && !IsPassedStreetTest;
            }
        }
    }
}
