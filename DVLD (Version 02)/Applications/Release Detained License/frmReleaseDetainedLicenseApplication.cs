using DVLD__Version_02_.Classes;
using DVLD__Version_02_.Licenses;
using DVLD__Version_02_.Licenses.Local_Licenses;
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

namespace DVLD__Version_02_.Applications.Release_Detained_License
{
    public partial class frmReleaseDetainedLicenseApplication : Form
    {
        int SelectedLicense = -1;

        public frmReleaseDetainedLicenseApplication(int LicenseID)
        {
            InitializeComponent();
            ctrlDriverLicenseInfoWithFilter2.LoadLicenseInfo(LicenseID);
            ctrlDriverLicenseInfoWithFilter2.FilterEnabled = false;
        }

        public frmReleaseDetainedLicenseApplication()
        {
            InitializeComponent();
        }

        private void frmReleaseDetainedLicenseApplication_Load(object sender, EventArgs e)
        {
            lbDetainDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            lbApplicationFees.Tag = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicense).Fees.ToString();
            lbApplicationFees.Text = lbApplicationFees.Tag + " $";
            lbCreatedBy.Text = clsGlobal.CurrentUser.UserName;

        }

        private void ctrlDriverLicenseInfoWithFilter2_OnPersonSelected(int obj)
        {
            SelectedLicense = obj;
            llshowlicenseinfo.Enabled = llshowlicensehistory.Enabled = (SelectedLicense != -1);

            if (!ctrlDriverLicenseInfoWithFilter2.SelectedLicense.IsDetained)
            {
                MessageBox.Show("This License Already Released License", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //After we Check If It Is Detained We Fill Detained Information

            LbDetainID.Text = ctrlDriverLicenseInfoWithFilter2.SelectedLicense.DetainedLicenseInfo.DetainID.ToString();
            lbFineFees.Tag = ctrlDriverLicenseInfoWithFilter2.SelectedLicense.DetainedLicenseInfo.FineFees.ToString();
            lbDetainDate.Text = ctrlDriverLicenseInfoWithFilter2.SelectedLicense.DetainedLicenseInfo.DetainDate.ToString("dd-MMM-yyyy");
            lbFineFees.Text = lbFineFees.Tag + " $";
            lbTotalFees.Text = (Convert.ToSingle(lbApplicationFees.Tag) + Convert.ToSingle(lbFineFees.Tag)).ToString() + " $";
            lbLicenseid.Text = ctrlDriverLicenseInfoWithFilter2.LicensesID.ToString();
            btnRelease.Enabled = true;
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure To Detain This License?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                   == DialogResult.Cancel)
            {
                return;
            }

            int ReleaseApplicationID = -1;
            bool IsReleased = ctrlDriverLicenseInfoWithFilter2.SelectedLicense.ReleaseDetainedLicesne(clsGlobal.CurrentUser.UserID, ref ReleaseApplicationID);
            if (!IsReleased)
            {
                MessageBox.Show("Can't Release This License", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnRelease.Enabled = false;
            ctrlDriverLicenseInfoWithFilter2.FilterEnabled = false;
            lbApplicationID.Text = ReleaseApplicationID.ToString();
            MessageBox.Show("License Released Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void llshowlicensehistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter2.SelectedLicense.DriverInfo.PersonID);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void llshowlicenseinfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(SelectedLicense);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }
    }
}
