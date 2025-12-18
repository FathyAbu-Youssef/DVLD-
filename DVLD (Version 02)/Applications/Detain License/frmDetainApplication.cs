using DVLD__Version_02_.Classes;
using DVLD__Version_02_.Licenses;
using DVLD__Version_02_.Licenses.Local_Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD__Version_02_.Applications.Detain_License
{
    public partial class frmDetainApplication : Form
    {
        private int _SelectedLicense = -1;

        public frmDetainApplication()
        {
            InitializeComponent();
        }

        private void frmDetainApplication_Load(object sender, EventArgs e)
        {
            lbDetainDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
         lbCreatedBy.Text = clsGlobal.CurrentUser.UserName;
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnPersonSelected(int obj)
        {
            _SelectedLicense = obj;
            llshowlicensehistory.Enabled = llshowlicenseinfo.Enabled = (_SelectedLicense != -1);

            if (_SelectedLicense == -1) 
            {
                return;
            }

            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicense.IsDetained) 
            {
                MessageBox.Show("This License Is Already Detained", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btndetain.Enabled = true;
        }

        private void llshowlicensehistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicense.DriverInfo
                .PersonID);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void llshowlicenseinfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_SelectedLicense);
            frm.StartPosition = FormStartPosition.CenterParent; 
            frm.ShowDialog();
        }
        private void btndetain_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("Are You Sure To Detain This License", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (Result == DialogResult.OK)
            {
                int DetainID = ctrlDriverLicenseInfoWithFilter1.SelectedLicense.Detain(1, Single.Parse(txtFineFees.Text.Trim()));

                if (DetainID == -1)
                {
                    MessageBox.Show("Failed To Detain This License", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("License Detained Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btndetain.Enabled = false;
                ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
                txtFineFees.Enabled = false;
            }
        }
    }
}
