using DVLD__Version_02_.Classes;
using DVLD__Version_02_.Licenses;
using DVLD__Version_02_.Licenses.InternationalLicense;
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
    public partial class frmAddNewInternationalLicense : Form
    {
        private int _NewLicenseID = 0;
        public frmAddNewInternationalLicense()
        {
            InitializeComponent();
        }

        private void frmAddNewInternationalLicense_Load(object sender, EventArgs e)
        {
            lbApplicationDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            lbIssueDate.Text =lbApplicationDate.Text;
            lbExpirationDate.Text = DateTime.Now.AddYears(1).ToString("dd-MMM-yyyy");
            lbApplicationFees.Tag = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInterNationalLicense).Fees.ToString();
            lbApplicationFees.Text = lbApplicationFees.Tag + " $";
            lbCreatedBy.Text = clsGlobal.CurrentUser.UserName;
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnPersonSelected(int SelectedLicenseID)
        {
            lbLicenseHistory.Enabled = (SelectedLicenseID != -1);

            if (SelectedLicenseID == -1)
            {
                lbLocalLicenseID.Text = "No License With This ID";
                return;
            }

            lbLocalLicenseID.Text = SelectedLicenseID.ToString();

            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicense.LicenseClass != 3)
            {
                MessageBox.Show("You Must Choose License On Class 3", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int ActiveInternationalLicenseID = clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID
                (ctrlDriverLicenseInfoWithFilter1.SelectedLicense.DriverID);

            if (ActiveInternationalLicenseID != -1)
            {
                MessageBox.Show($"This Person Already Has An Active International License With ID : {ActiveInternationalLicenseID}",
                    "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _NewLicenseID = ActiveInternationalLicenseID;
                llLicenseInfo.Enabled = true;
                return;
            }
            btnIssue.Enabled = ctrlDriverLicenseInfoWithFilter1.SelectedLicense.IsActive;
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Are You Sure To Issue This International License", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (Result == DialogResult.Cancel)
            {
                return;
            }

            clsInternationalLicense InternationalLicense = new clsInternationalLicense();
           
            //First We Fill Application Information
            InternationalLicense.ApplicantPersonID = ctrlDriverLicenseInfoWithFilter1.SelectedLicense.DriverInfo.PersonID;
            InternationalLicense.ApplicationStatus = clsApplication.enStatus.Complete;
            InternationalLicense.ApplicationDate= DateTime.Now;
            InternationalLicense.LastStatusDate = DateTime.Now;
            InternationalLicense.PaidFees = Convert.ToSingle(lbApplicationFees.Tag);
            InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            //Seconed We Fill International License Information

            InternationalLicense.DriverID = ctrlDriverLicenseInfoWithFilter1.SelectedLicense.DriverID;
            InternationalLicense.IssuedUsingLocalLicenseID = ctrlDriverLicenseInfoWithFilter1.SelectedLicense.LicenseID;
            InternationalLicense.IssueDate =DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);

            if (!InternationalLicense.Save())
            {
                MessageBox.Show("Failed To Issue International License", "Falied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lbInternationalApplicationID.Text = InternationalLicense.ApplicationID.ToString();
            lbInternationalLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
            MessageBox.Show($"International License Issued Successfully With ID {InternationalLicense.InternationalLicenseID}", "Success", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            _NewLicenseID = InternationalLicense.InternationalLicenseID;
            llLicenseInfo.Enabled = true;
            lbLicenseHistory.Enabled = true;
            btnIssue.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
        }

        private void llLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(_NewLicenseID);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void lbLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ctrlDriverLicenseInfoWithFilter1.SelectedLicense.DriverInfo.PersonID;
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }
    }
}
